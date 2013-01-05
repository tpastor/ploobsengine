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
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Cameras;

namespace EngineTestes
{
    public class FirstScreen : IScene
    {

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            BepuPhysicWorld BepuPhysicWorld = new BepuPhysicWorld(-0.98f,true,1,false);            
            world = new IWorld(BepuPhysicWorld, new SimpleCuller());

            ForwardRenderTecnichDescription desc = ForwardRenderTecnichDescription.Default();
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new ForwardRenderTecnich(desc);
        }

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            //SimpleModel simpleModel = new SimpleModel(factory, "Model//block");
            //simpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Red, false), TextureType.DIFFUSE);
            //BoxObject tmesh = new BoxObject(Vector3.Zero, 1, 1, 1, 10, new Vector3(500, 5, 500), Matrix.Identity, MaterialDescription.DefaultBepuMaterial());
            //tmesh.isMotionLess = true;
            //GhostObject tmesh = new GhostObject();

            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//cenario");
                TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
                
                ForwardXNABasicShaderDescription ForwardXNABasicShaderDescription = ForwardXNABasicShaderDescription.Default();
                ForwardXNABasicShaderDescription.EnableLightning = true;
                ForwardXNABasicShaderDescription.DefaultLightning = true;
                ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription);
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);
            }
        
            BallThrowBepu b = new BallThrowBepu(this.World,GraphicFactory);
            this.World.CameraManager.AddCamera(new CameraFirstPerson(true,GraphicInfo));
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {        
            base.Draw(gameTime, render);         
        }

    }
}
