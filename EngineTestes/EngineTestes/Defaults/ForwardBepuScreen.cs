using System;
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
using EngineTestes.ScreenTests;
using PloobsEngine.TestSuite;
using PloobsEngine.Features;
using PloobsEngine.Commands;

namespace EngineTestes
{
    public abstract class ForwardBepuScreen : IScene
    {
        public ForwardBepuScreen(ForwardRenderTecnichDescription? forwardRenderTecnichDescription = null, float gravity = -9.98f, bool multiThread = false)
        {
            this.gravity=gravity;
            this.multiThread = multiThread;
            this.ForwardRenderTecnichDescription = forwardRenderTecnichDescription.HasValue ? forwardRenderTecnichDescription.Value : ForwardRenderTecnichDescription.Default();
        }

        bool multiThread;
        float gravity;
        protected EngineStuff EngineStuff;
        ForwardRenderTecnichDescription ForwardRenderTecnichDescription;

        protected IObject DefaultTriangleMeshObj(String modelName, Vector3 position, Vector3? scale = null, Matrix? orientation = null)
        {
            SimpleModel simpleModel = new SimpleModel(GraphicFactory, modelName);
            TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, position, orientation, scale);
            ForwardMaterial fmaterial = ForwardMaterial.DefaultForwardMaterial();
            return new IObject(fmaterial, simpleModel, tmesh);            
        }

        protected IObject DefaultTriangleMeshObj(String modelName, Color color, Vector3 position, Vector3? scale = null, Matrix? orientation = null)
        {
            SimpleModel simpleModel = new SimpleModel(GraphicFactory, modelName);
            simpleModel.SetTexture(color);
            TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, position, orientation, scale);
            ForwardMaterial fmaterial = ForwardMaterial.DefaultForwardMaterial();
            return new IObject(fmaterial, simpleModel, tmesh);
        }

        protected IObject DefaultBoxObj(String modelName, Vector3 position, Vector3? scale = null, float mass = 10, Matrix? orientation = null)
        {
            SimpleModel simpleModel = new SimpleModel(GraphicFactory, modelName);
            BoxObject tmesh = new BoxObject(position, 1, 1, 1, mass, scale, orientation);
            ForwardMaterial fmaterial = ForwardMaterial.DefaultForwardMaterial();
            return new IObject(fmaterial, simpleModel, tmesh);
        }

        protected IObject DefaultBoxObj(String modelName, Color color, Vector3 position, Vector3? scale = null, float mass = 10, Matrix? orientation = null)
        {
            SimpleModel simpleModel = new SimpleModel(GraphicFactory, modelName);
            simpleModel.SetTexture(color);
            BoxObject tmesh = new BoxObject(position, 1, 1, 1, mass, scale, orientation);
            ForwardMaterial fmaterial = ForwardMaterial.DefaultForwardMaterial();
            return new IObject(fmaterial, simpleModel, tmesh);
        }

        protected IObject DefaultSphereObj(String modelName, Color color, Vector3 position, float radius, float scale = 1, float mass = 10)
        {
            SimpleModel simpleModel = new SimpleModel(GraphicFactory, modelName);
            simpleModel.SetTexture(color);
            SphereObject tmesh = new SphereObject(position, radius,mass,scale);
            ForwardMaterial fmaterial = ForwardMaterial.DefaultForwardMaterial();
            return new IObject(fmaterial, simpleModel, tmesh);
        }

        protected IObject DefaultSphereObj(String modelName, Vector3 position, float radius, float scale = 1, float mass = 10)
        {
            SimpleModel simpleModel = new SimpleModel(GraphicFactory, modelName);            
            SphereObject tmesh = new SphereObject(position, radius, mass, scale);
            ForwardMaterial fmaterial = ForwardMaterial.DefaultForwardMaterial();
            return new IObject(fmaterial, simpleModel, tmesh);
        }


        protected void AddSkyBox(String CubetextureName)
        {
            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube(CubetextureName);
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);
        }
        
        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);
            this.EngineStuff = engine;

            SkyBox skybox = new SkyBox();
            engine.AddComponent(skybox);
        }

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(gravity,false,1,multiThread), new SimpleCuller());
            renderTech = new ForwardRenderTecnich(ForwardRenderTecnichDescription);
        }

    }
}
