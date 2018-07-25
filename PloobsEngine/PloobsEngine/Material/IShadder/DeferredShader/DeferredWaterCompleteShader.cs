﻿#region License
/*
    PloobsEngine Game Engine Version 0.3 Beta
    Copyright (C) 2011  Ploobs

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion
#if !WINDOWS_PHONE && !REACH  && !MONODX
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Material;
using PloobsEngine;
using PloobsEngine.Modelo;
using PloobsEngine.SceneControl;
using PloobsEngine.Cameras;
using PloobsEngine.Utils;

namespace PloobsEngine.Material
{

    /// <summary>
    /// Water with Reflection, refraction and animation shader
    /// </summary>
    public class DeferredWaterCompleteShader : IShader
    {
        
        private float specularIntensity = 0f;          
        private RenderTarget2D refractionRT;
        private RenderTarget2D reflectionRT;
        private Texture2D refractionMap;
        private Texture2D reflectionMap;
        private Texture2D normal0;
        private Texture2D normal1;
        private Effect _shader;          
        private float planeHeight;
        private int WIDTH;
        private int HEIGHT;
        private Plane plane;        
        private Matrix reflectiveViewMatrix;                
        private float timeModulation = 100;        

        /// <summary>
        /// Default 100
        /// </summary>
        /// <value>
        /// The time modulation.
        /// </value>
        public float TimeModulation
        {
            get { return timeModulation; }
            set { timeModulation = value; }
        }
        private float waveLength = 0.2f;

        /// <summary>
        /// Default 0.2f
        /// </summary>
        /// <value>
        /// The length of the wave.
        /// </value>
        public float WaveLength
        {
            get { return waveLength; }
            set { waveLength = value; }
        }
        private float waveHeight = 0.02f;

        /// <summary>
        /// Default 0.02f
        /// </summary>
        /// <value>
        /// The height of the wave.
        /// </value>
        public float WaveHeight
        {
            get { return waveHeight; }
            set { waveHeight = value; }
        }

        private float windForce = 0.002f;

        /// <summary>
        /// Default 0.002f
        /// </summary>
        /// <value>
        /// The wind force.
        /// </value>
        public float WindForce
        {
            get { return windForce; }
            set { windForce = value; }
        }
        private Vector4 waterColor = new Vector4(0.5f, 0.8f, 0.8f, 1.0f);

        /// <summary>
        /// Default Vector4(0.5f, 0.8f, 0.8f, 1.0f);
        /// </summary>
        /// <value>
        /// The color of the water.
        /// </value>
        public Color WaterColor
        {
            get { return new Color(waterColor); }
            set { waterColor = value.ToVector4(); }
        }
        private Vector3 windDirection = new Vector3(0, 0, 1);

        /// <summary>
        /// Default Vector3(0, 0, 1);
        /// </summary>
        /// <value>
        /// The wind direction.
        /// </value>
        public Vector3 WindDirection
        {
            get { return windDirection; }
            set { windDirection = value; }
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DeferredWaterCompleteShader"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="plane">The plane.</param>
        /// <param name="Height">The height.</param>
        /// <param name="affectedByLight">if set to <c>true</c> the watter is [affected by light].</param>
        public DeferredWaterCompleteShader(int width, int height, Plane plane, float Height,bool affectedByLight = true)
        {
            this.WIDTH = width;
            this.HEIGHT = height;
            this.plane = plane;
            this.planeHeight = Height;
            if (affectedByLight)
                this.shaderId = 0;
            else
                this.shaderId = ShaderUtils.CreateSpecificBitField(true,false);
        }

        /// <summary>
        /// Gets or sets the specular intensity.
        /// </summary>
        /// <value>
        /// The specular intensity.
        /// </value>
        public float SpecularIntensity
        {
            get { return specularIntensity; }
            set { specularIntensity = value; }
        }
        private float specularPower = 0f;

        /// <summary>
        /// Gets or sets the specular power.
        /// </summary>
        /// <value>
        /// The specular power.
        /// </value>
        public float SpecularPower
        {
            get { return specularPower; }
            set { specularPower = value; }
        }

        private Plane CreatePlane(float height, Vector3 planeNormalDirection, Matrix currentViewMatrix, bool clipSide,ICamera cam)
        {
            planeNormalDirection.Normalize();
            Vector4 planeCoeffs = new Vector4(planeNormalDirection, height);
            if (clipSide)
                planeCoeffs *= -1;

            Matrix worldViewProjection = currentViewMatrix * cam.Projection;
            Matrix inverseWorldViewProjection = Matrix.Invert(worldViewProjection);
            inverseWorldViewProjection = Matrix.Transpose(inverseWorldViewProjection);

            planeCoeffs = Vector4.Transform(planeCoeffs, inverseWorldViewProjection);
            Plane finalPlane = new Plane(planeCoeffs);
            return finalPlane;
        }
        private Plane CreateReflectionPlane(float height, ref Vector3 planeNormalDirection, ref Matrix currentViewMatrix, ref Matrix camProj, bool clipSide)
        {
            planeNormalDirection.Normalize();
            Vector4 planeCoeffs = new Vector4(planeNormalDirection, height);
            if (clipSide)
                planeCoeffs *= -1;

            Matrix worldViewProjection = currentViewMatrix * camProj;
            Matrix inverseWorldViewProjection = Matrix.Invert(worldViewProjection);
            inverseWorldViewProjection = Matrix.Transpose(inverseWorldViewProjection);

            planeCoeffs = Vector4.Transform(planeCoeffs, inverseWorldViewProjection);
            Plane finalPlane = new Plane(planeCoeffs);

            return finalPlane;
        }



        public override void PreDrawPhase(GameTime gt, IWorld world, IObject obj, RenderHelper render, ICamera cam)
        {
            render.ValidateSamplerStates();

            Matrix view = cam.View;
            Matrix projection = cam.Projection;
            //REFRACAO
            Plane refractionClipPlane;
            if (cam.Position.Y > -plane.D)
            {
                refractionClipPlane = CreateReflectionPlane(plane.D, ref plane.Normal, ref view, ref projection, true);
            }
            else
            {
                refractionClipPlane = CreateReflectionPlane(plane.D, ref plane.Normal, ref view, ref projection, false);
            }            
            render.PushRenderTarget(refractionRT);
            render.Clear(Color.Black);
            render.RenderSceneReflectionRefration(world, gt, new List<IObject>() { obj }, ref view, ref projection, true, true,refractionClipPlane);
            refractionMap = render.PopRenderTarget()[0].RenderTarget as Texture2D;            

            //REFLEXAO
            Matrix m = Matrix.CreateReflection(plane);
            Vector3 pos;
            Vector3 target;
            Vector3 up;
            pos = Vector3.Transform(cam.Position, m);
            target = Vector3.Transform(cam.Target, m);
            up = Vector3.Transform(cam.Up, m);
            reflectiveViewMatrix = Matrix.CreateLookAt(pos, target, up);
            Plane reflectionClipPlane = CreateReflectionPlane(plane.D, ref plane.Normal, ref reflectiveViewMatrix, ref projection, false);
            render.PushRenderTarget(reflectionRT);
            render.Clear(Color.Black);
            render.PushRasterizerState(RasterizerState.CullClockwise);
            render.RenderSceneReflectionRefration(world, gt, new List<IObject>() { obj }, ref reflectiveViewMatrix, ref projection, true, true,reflectionClipPlane);
            render.PopRasterizerState();
            reflectionMap = render.PopRenderTarget()[0].RenderTarget as Texture2D;                                   

        }

        protected override void Draw(GameTime gt, IObject obj, RenderHelper render, ICamera camera, IList<Light.ILight> lights)
        {
                float time = (float) gt.TotalGameTime.TotalMilliseconds / timeModulation;

                this._shader.Parameters["specularIntensity"].SetValue(specularIntensity);
                this._shader.Parameters["specularPower"].SetValue(specularPower);
                this._shader.Parameters["View"].SetValue(camera.View);
                this._shader.Parameters["camPos"].SetValue(camera.Position);
                this._shader.Parameters["ReflectionView"].SetValue(reflectiveViewMatrix);
                this._shader.Parameters["RefractionView"].SetValue(camera.View);

                //this._shader.Parameters["ReflectionMap"].SetValue(reflectionMap);
                //this._shader.Parameters["RefractionMap"].SetValue(refractionMap);
                //this._shader.Parameters["normalMap0"].SetValue(normal0);
                //this._shader.Parameters["normalMap1"].SetValue(normal1);
                render.Textures[0] = refractionMap;
                render.Textures[1] = reflectionMap;
                render.Textures[2] = normal0;
                render.Textures[3] = normal1;


                this._shader.Parameters["xWaveLength"].SetValue(waveLength);
                this._shader.Parameters["xWaveHeight"].SetValue(waveHeight);
                this._shader.Parameters["Time"].SetValue(time);
                this._shader.Parameters["WindForce"].SetValue(windForce);
                this._shader.Parameters["waterColor"].SetValue(waterColor);
                this._shader.Parameters["WindDirection"].SetValue(windDirection);
                this._shader.Parameters["Projection"].SetValue(camera.Projection);
                this._shader.Parameters["id"].SetValue(shaderId);

                render.PushRasterizerState(RasterizerState.CullNone);

                SamplerState s0 = render.SetSamplerState(SamplerState.LinearWrap, 0);
                SamplerState s1 = render.SetSamplerState(SamplerState.LinearWrap, 1);

                SamplerState s2 = render.SetSamplerState(SamplerState.LinearWrap, 2);
                SamplerState s3 = render.SetSamplerState(SamplerState.LinearWrap, 3);
                
                Matrix wld = obj.WorldMatrix;
                for (int i = 0; i < obj.Modelo.MeshNumber; i++)
                {
                    BatchInformation[] bi = obj.Modelo.GetBatchInformation(i);                    
                    
                    for (int j = 0; j < bi.Count(); j++)
                    {
                        Matrix w1 = Matrix.Multiply(wld, bi[j].ModelLocalTransformation);
                        this._shader.Parameters["World"].SetValue(w1);
                        render.RenderBatch(bi[j], _shader);
                    }
                }
                render.PopRasterizerState();

                render.SetSamplerState(s0, 0);
                render.SetSamplerState(s1, 1);
                render.SetSamplerState(s2, 2);
                render.SetSamplerState(s3, 3);
        }

        public override void  Initialize(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory, PloobsEngine.SceneControl.IObject obj)
        {
 	        base.Initialize(ginfo, factory, obj);
            this._shader = factory.GetEffect("WaterComplete",true,true);         
            refractionRT = factory.CreateRenderTarget(WIDTH,HEIGHT);
            reflectionRT = factory.CreateRenderTarget(WIDTH, HEIGHT);
            normal0 = factory.GetTexture2D("wave0", true);
            normal1 = factory.GetTexture2D("wave1", true);                                    
        }        

        /// <summary>
        /// Gets the type of the material.
        /// </summary>
        /// <value>
        /// The type of the material.
        /// </value>
        public override MaterialType MaterialType
        {
            get { return MaterialType.DEFERRED; }
        }
    }
}
#endif