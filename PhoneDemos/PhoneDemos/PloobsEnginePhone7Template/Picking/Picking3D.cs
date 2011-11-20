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

namespace PloobsEnginePhone7Template
{
    public class Picking3D : IScene
    {

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            BepuPhysicWorld BepuPhysicWorld = new BepuPhysicWorld(-0.97f);
            SimpleCuller SimpleCuller = new SimpleCuller();
            world = new IWorld(BepuPhysicWorld, SimpleCuller);

            ForwardRenderTecnichDescription desc = ForwardRenderTecnichDescription.Default();
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new ForwardRenderTecnich(desc);
        }

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//cenario");
                TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
                ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                obj.Name = "Cenario";
                this.World.AddObject(obj);
            }

            Picking pick = new Picking(this, Microsoft.Xna.Framework.Input.Touch.GestureType.DoubleTap);
            pick.Start();
            pick.OnPickedGesture += new OnPicked(pick_OnPickedGesture);

            var newCameraFirstPerson = new CameraFirstPerson(GraphicInfo.Viewport);
            this.World.CameraManager.AddCamera(newCameraFirstPerson);
         
        }

        String result = null;
        void pick_OnPickedGesture(SegmentInterceptInfo SegmentInterceptInfo, Microsoft.Xna.Framework.Input.Touch.TouchLocation TouchLocation)
        {
            result = "Position " + SegmentInterceptInfo.ImpactPosition + " - " + SegmentInterceptInfo.PhysicObject.ObjectOwner.Name;
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {        
            base.Draw(gameTime, render);
            render.RenderTextComplete("PloobsEngine Picking 3D on Windows Phone7", new Vector2(20, 10), Color.Red, Matrix.Identity);
            render.RenderTextComplete("Use DoubleTap on the scene", new Vector2(20, 10), Color.Red, Matrix.Identity);
            
            if(result != null)
                render.RenderTextComplete(result, new Vector2(20, 30), Color.Red, Matrix.Identity);
        }

    }
}
