using System; 
using System.Collections.Generic; 
using Microsoft.Xna.Framework; 
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.SceneControl;
using PloobsEngine.Engine;
using PloobsEngine.Cameras;
using PloobsEngine.Physics;
using EngineTestes;
using PloobsEngine.Input;
using PloobsEngine.Modelo;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.Material;
using PloobsEngine.Features;
using PloobsEngine.Commands;

namespace PloobsEnginePhone7Template
{
    public class SkyBoxScreen : IScene
    {
        SkyBox SkyBox;
        RotatingCamera cam;        

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            BepuPhysicWorld BepuPhysicWorld = new BepuPhysicWorld(0);
            SimpleCuller SimpleCuller = new SimpleCuller();
            world = new IWorld(BepuPhysicWorld, SimpleCuller);

            ForwardRenderTecnichDescription desc = ForwardRenderTecnichDescription.Default();
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new ForwardRenderTecnich(desc);
        }

        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);

            SkyBox = new SkyBox();
            engine.AddComponent(SkyBox);

        }



        protected override void CleanUp(EngineStuff engine)
        {
            engine.RemoveComponent(SkyBox.MyName);
            base.CleanUp(engine);
        }

        ForwardEnvironmentShader shader;

        public void SetAmountDiffuse(float amount)
        {
            shader.EnvironmentMapEffect.EnvironmentMapAmount  = amount;            
        }

        public void ChangeColor()
        {
            simpleModel.SetTexture(GraphicFactory.CreateTexture2DRandom(1, 1), TextureType.DIFFUSE);                
        }

        SimpleModel simpleModel;
        protected override void LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory, IContentManager contentManager)
        {            
            base.LoadContent(GraphicInfo, factory, contentManager);

            #region Models
            {
                simpleModel = new SimpleModel(factory, "..\\Content\\Model\\teapot");
                simpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Red), TextureType.DIFFUSE);                

                GhostObject tmesh = new GhostObject(Vector3.Zero, Matrix.Identity, Vector3.One * 2);
                ///Environment Map Shader, there are 2 options, the first is a fully reflective surface (dont use the object texture) and the second
                ///is a mix of the object texture and the environment texture
                ///Used to fake ambient reflection, give metal appearence to an object ....
                shader = new ForwardEnvironmentShader(factory.GetTextureCube("Textures\\cubeMap"),1,false);
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                IObject tea = new IObject(fmaterial, simpleModel, tmesh);

                tea.OnUpdate += new OnUpdate(tea_OnUpdate);
                this.World.AddObject(tea);
            }

            //{
            //    SimpleModel simpleModel = new SimpleModel(factory, "Model//block");
            //    simpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.White), TextureType.DIFFUSE);
            //    BoxObject tmesh = new BoxObject(new Vector3(0,-20,0), 1, 1, 1, 10, new Vector3(200, 1, 200), Matrix.Identity, MaterialDescription.DefaultBepuMaterial());
            //    tmesh.isMotionLess = true;
            //    DeferredNormalShader shader = new DeferredNormalShader();
            //    DeferredMaterial fmaterial = new DeferredMaterial(shader);
            //    IObject obj = new IObject(fmaterial, simpleModel, tmesh);
            //    this.World.AddObject(obj);
            //}


            #endregion
            
            cam = new RotatingCamera(this);
            this.World.CameraManager.AddCamera(cam);
            
            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//cubeMap");
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);
           
        }

        void tea_OnUpdate(IObject obj, GameTime gt, ICamera cam)
        {
            obj.PhysicObject.Rotation *= Matrix.CreateRotationY(0.02f);
            obj.PhysicObject.Rotation *= Matrix.CreateRotationZ(0.02f);

        }

    }

    
} 
