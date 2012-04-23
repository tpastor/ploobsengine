#region License
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
#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using PloobsEngine.Cameras;
using PloobsEngine.Light;
using PloobsEngine.SceneControl;
using PloobsEngine.Modelo;
using PloobsEngine.Modelo.Animation;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace PloobsEngine.Material
{

    /// <summary>
    /// Base Class For all Shaders
    /// </summary>
#if WINDOWS
    public abstract class IShader : ISerializable
#else
    public abstract class IShader 
#endif
    {
        /// <summary>
        /// is fist time that this shader is updated
        /// </summary>
        protected bool firstTime = true;
        protected bool isInitialized = false;
        protected Effect basicDraw = null;
        protected Effect getDepth = null;
        protected OcclusionQuery OcclusionQuery;
        protected bool useOcclusionCulling = false;
        private static BlendState BlendState;
        private static IModelo Modelo;
        private static BasicEffect BasicEffect;
        GraphicFactory GraphicFactory;

        public  bool UseOcclusionCulling
        {
            get { return useOcclusionCulling; }
            set { useOcclusionCulling = value;

            if (isInitialized)
            {
                if (Modelo == null)
                    Modelo = new SimpleModel(GraphicFactory, "cube", true);

                if (BasicEffect == null)
                {
                    BasicEffect = GraphicFactory.GetBasicEffect();
                    BasicEffect.TextureEnabled = false;
                    BasicEffect.VertexColorEnabled = false;
                }

                if (BlendState == null)
                {
                    BlendState = new Microsoft.Xna.Framework.Graphics.BlendState();
                    BlendState.ColorWriteChannels = ColorWriteChannels.None;
                }
                OcclusionQuery = GraphicFactory.CreateOcclusionQuery();
            }
            }
        }
        
        /// <summary>
        /// Shader ID that the object rendered by this shader will have        
        /// </summary>
        /// <remarks>
        /// This parameter lets the object not be affect by light, or be recovered in a post proccess phase
        /// </remarks>
        protected int shaderId = 0;

        
        /// <summary>
        /// Gets the type of the material.
        /// </summary>
        /// <value>
        /// The type of the material.
        /// </value>
        public abstract MaterialType MaterialType
        {
            get;
        }

        /// <summary>
        /// Used to Recover the object in PostProcces
        /// </summary>
        public int ShaderId
        {
            get { return shaderId; }
            set { shaderId = value; }
        }        

        /// <summary>
        /// Initializes a new instance of the <see cref="IShader"/> class.
        /// </summary>
        public IShader()
        {            
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public virtual void Initialize(GraphicInfo ginfo, GraphicFactory factory, IObject obj)
        {
            #if !WINDOWS_PHONE && !REACH
            basicDraw = factory.GetEffect("clippingPlane", false, true);
            getDepth = factory.GetEffect("ShadowDepth",false,true);
            BasicDrawSamplerState = SamplerState.LinearWrap;            
            #endif
            if (useOcclusionCulling)
            {
                if(Modelo==null)
                    Modelo = new SimpleModel(factory, "cube", true);

                if (BasicEffect == null)
                {
                    BasicEffect = factory.GetBasicEffect();
                    BasicEffect.TextureEnabled = false;
                    BasicEffect.VertexColorEnabled = false;                    
                }

                if (BlendState == null)
                {
                    BlendState = new Microsoft.Xna.Framework.Graphics.BlendState();
                    BlendState.ColorWriteChannels = ColorWriteChannels.None;                    
                }
                OcclusionQuery = factory.CreateOcclusionQuery();
            }
            this.GraphicFactory = factory;
            isInitialized = true;    
        }

        /// <summary>
        /// Called Once only, before all draws
        /// </summary>
        /// <param name="ent">The current entity.</param>
        /// <param name="lights">All The lights </param>
        public virtual void PreUpdate(IObject ent, IList<ILight> lights) { }


        /// <summary>
        /// Updates this shader
        /// Called every frame once
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="ent">The ent.</param>
        /// <param name="lights">The lights.</param>
        public virtual void Update(GameTime gt, IObject ent, IList<ILight> lights)
        {
            if (firstTime)
            {             
                PreUpdate(ent, lights);
                firstTime = false;
            }            
        }


        /// <summary>
        /// Called every frame before the draw phase.
        /// In deferred it is called before the GBUFFER is setted
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="world">The world.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="render">The render.</param>
        /// <param name="cam">The camera</param>
        public virtual void PreDrawPhase(GameTime gt, IWorld world, IObject obj, RenderHelper render, ICamera cam)
        {            
        }

        /// <summary>
        /// Called after the draw phase.
        /// In deferred its responsible for the Forward Pass, in forward its not called
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="render">The render.</param>
        /// <param name="cam">The camera.</param>
        /// <param name="lights">The lights.</param>
        public virtual void PosDrawPhase(GameTime gt, IObject obj, RenderHelper render, ICamera cam, IList<ILight> lights)
        {
            
        }

        protected virtual void Draw(GameTime gt, IObject obj, RenderHelper render, ICamera cam, IList<ILight> lights)
        {
        }

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="render">The render.</param>
        /// <param name="cam">The cam.</param>
        /// <param name="lights">The lights.</param>
        public void iDraw(GameTime gt, IObject obj, RenderHelper render, ICamera cam, IList<ILight> lights)
        {
            if (OcclusionQuery != null)
            {
                if (OcclusionQuery.PixelCount > 0 || obj.PhysicObject.BoundingBox==null)
                {                    
                    //Enable rendering to screen.
                    //Enable or disable writing to depth buffer (depends on whether the object is translucent or opaque).
                    //Render the object itself.
                    OcclusionQuery.Begin();
                    this.Draw(gt, obj, render, cam, lights);
                    OcclusionQuery.End();
                }
                else
                {
                    //Disable rendering to screen.
                    //Disable writing to depth buffer.
                    //"Render" the object's bounding box.

                    OcclusionQuery.Begin();
                    render.PushBlendState(BlendState);
                    render.PushDepthStencilState(DepthStencilState.DepthRead);                    
                    BoundingBox BoundingBox = obj.PhysicObject.BoundingBox.Value;
                    BasicEffect.View = cam.View;
                    BasicEffect.Projection = cam.Projection;
                    Vector3 centerPos = (BoundingBox.Max + BoundingBox.Min) / 2;
                    Vector3 scale = BoundingBox.Max - centerPos;
                    Matrix world = Matrix.CreateScale(scale);
                    world.Translation = centerPos;
                    BasicEffect.World = world;
                    render.RenderBatch(Modelo.GetBatchInformation(0)[0], BasicEffect);
                    OcclusionQuery.End();
                    render.PopDepthStencilState();
                    render.PopBlendState();
                }
            }
            else
            {
                this.Draw(gt, obj, render, cam, lights);
            }

        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public virtual void Cleanup(GraphicFactory factory)
        {            
        }

#if !WINDOWS_PHONE && !REACH
        /// <summary>
        /// Exctract the depth from an object
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="View">The view.</param>
        /// <param name="projection">The projection.</param>
        /// <param name="render">The render.</param>
        public virtual void DepthExtractor(GameTime gt, IObject obj, ref Matrix View, ref Matrix projection, RenderHelper render)
        {
            Matrix wld = obj.WorldMatrix;            
            for (int i = 0; i < obj.Modelo.MeshNumber; i++)
            {
                BatchInformation[] bi = obj.Modelo.GetBatchInformation(i);
                for (int j = 0; j < bi.Count(); j++)
                {
                    Matrix w1 = Matrix.Multiply(wld, bi[j].ModelLocalTransformation);
                    this.getDepth.Parameters["WVP"].SetValue(w1 * View * projection);

                    render.RenderBatch(bi[j], getDepth);
                }
            }
        }


        /// <summary>
        /// Draw the object in a simple way (WITH MINIMUM EFFECTS,....)
        /// USED IN RELECTIONS, REFRACTION .....
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="obj">The obj.</param>
        /// <param name="view">The view.</param>
        /// <param name="projection">The projection.</param>
        /// <param name="lights">The lights.</param>
        /// <param name="render">The render.</param>
        /// <param name="clippingPlane">The clipping plane.</param>
        /// <param name="useAlphaBlending">if set to <c>true</c> [use alpha blending].</param>
        public virtual void BasicDraw(GameTime gt, IObject obj, ref Matrix view, ref Matrix projection, IList<ILight> lights, RenderHelper render,Plane? clippingPlane, bool useAlphaBlending = false)
        {
            Matrix wld = obj.WorldMatrix;
            if (clippingPlane != null)
            {
                basicDraw.Parameters["clippingPlane"].SetValue(new Vector4(clippingPlane.Value.Normal, clippingPlane.Value.D));
                basicDraw.Parameters["isClip"].SetValue(true);
            }
            else
            {
                basicDraw.Parameters["isClip"].SetValue(false);
            }            

            if(useAlphaBlending)
                render.PushBlendState(BlendState.AlphaBlend);

            SamplerState s0 =  render.SetSamplerState(BasicDrawSamplerState,0);

            for (int i = 0; i < obj.Modelo.MeshNumber; i++)
            {
                BatchInformation[] bi = obj.Modelo.GetBatchInformation(i);
                for (int j = 0; j < bi.Count(); j++)
                {
                    //basicDraw.Parameters["diffuse"].SetValue(obj.Modelo.getTexture(TextureType.DIFFUSE,i,j));
                    render.device.Textures[0] =  obj.Modelo.getTexture(TextureType.DIFFUSE, i, j);
                    Matrix w1 = Matrix.Multiply(wld, bi[j].ModelLocalTransformation);
                    this.basicDraw.Parameters["WVP"].SetValue(w1 * view * projection);

                    render.RenderBatch(bi[j], basicDraw);
                }
            }

            if(useAlphaBlending)
                render.PopBlendState();

            render.SetSamplerState(s0, 0);
        }

        public SamplerState BasicDrawSamplerState
        {
            get;
            set;
        }

        #endif


#if WINDOWS
              public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            
        }
  
#endif

    }
}
