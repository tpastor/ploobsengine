﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using PloobsEngine.Physics;
using PloobsEngine.Modelo;
using PloobsEngine.Material;
using PloobsEngine.Engine;
using PloobsEngine.Physics.Bepu;
using Microsoft.Xna.Framework;
using PloobsEngine.Cameras;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using PloobsEngine.Input;
using Microsoft.Xna.Framework.Input;

namespace IntroductionDemo4._0
{
    public class SerializationScreen : IScene
    {

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(-9.8f), new SimpleCuller());

            ForwardRenderTecnichDescription desc = ForwardRenderTecnichDescription.Default();
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new ForwardRenderTecnich(desc);
        }

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);


            ///Serializing a basic type 
            SerializatorWrapper SerializatorWrapper = new SerializatorWrapper();
            SerializatorWrapper.Serialize(new Vector3(10, 20, 30), "vetor.xml");
            
            ///loading it
            Vector3 vec = (Vector3) SerializatorWrapper.Desserialize<Vector3>("vetor.xml", null);
            Debug.Assert(vec.X == 10);
            Debug.Assert(vec.Y == 20);
            Debug.Assert(vec.Z == 30);

            ///same to PloobsMockType
            ObjectMock ObjectMock = new ObjectMock(GraphicFactory, "Model/cenario", Vector3.Zero, Matrix.Identity, Vector3.One);
            SerializatorWrapper.Serialize(ObjectMock, "cena.xml");

            ObjectMock mloaded = (ObjectMock)SerializatorWrapper.Desserialize<ObjectMock>("cena.xml", GraphicFactory);
            this.World.AddObject(mloaded);

            
            IObject obj;
            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//ball");
                simpleModel.SetTexture(factory.CreateTexture2DColor(1,1,Color.Green),TextureType.DIFFUSE);
                SphereObject tmesh = new SphereObject(new Vector3(100, 200, 10), 1 ,5, 5, MaterialDescription.DefaultBepuMaterial());
                tmesh.isMotionLess = false;
                ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);
            }


            this.BindInput(new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS, Keys.Space,
                (a) =>
                {
                    this.World.RemoveObject(obj);
                    DynamicObjectSaver DynamicObjectSaver = new DynamicObjectSaver(obj.Modelo.Name, obj.PhysicObject.Position, obj.PhysicObject.Rotation, obj.PhysicObject.Scale, obj.PhysicObject.Velocity, obj.PhysicObject.AngularVelocity);
                    SerializatorWrapper.Serialize(DynamicObjectSaver, "dyn.xml");
                    
                    DynamicObjectSaver dloaded = (DynamicObjectSaver)SerializatorWrapper.Desserialize<DynamicObjectSaver>("dyn.xml");
                    SimpleModel simpleModel = new SimpleModel(factory, dloaded.modelName);
                    simpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Green), TextureType.DIFFUSE);
                    SphereObject tmesh = new SphereObject(dloaded.position, 1, 10, dloaded.scale.X, MaterialDescription.DefaultBepuMaterial());                    
                    tmesh.Rotation = dloaded.orientation;
                    tmesh.Velocity = dloaded.LinearVelocity;
                    tmesh.AngularVelocity = dloaded.AngularVelocity;
                    ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                    ForwardMaterial fmaterial = new ForwardMaterial(shader);
                    obj = new IObject(fmaterial, simpleModel, tmesh);
                    this.World.AddObject(obj);
                    saved++;
                }
                ));
            
            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));

        }
        int saved = 0;
        protected override void Draw(GameTime gameTime, RenderHelper render)
        {        
            base.Draw(gameTime, render);

            Texture2D logo = GraphicFactory.GetTexture2D("Textures\\engine_logo");
            int wd = 64;
            int hg = 48;
            render.RenderTextureComplete(logo, new Rectangle(this.GraphicInfo.BackBufferWidth - wd, this.GraphicInfo.BackBufferHeight - hg, wd, hg));

            render.RenderTextComplete("Demo Serialization: Press Space to Serialize/Desserialize the Ball Object", new Vector2(GraphicInfo.Viewport.Width - 715, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("Times Ball was Serialized/Desserialized: " + saved, new Vector2(GraphicInfo.Viewport.Width - 715, 35), Color.White, Matrix.Identity);
        }


    }
}
