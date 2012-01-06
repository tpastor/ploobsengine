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
using PloobsEngine.Input;

namespace PloobsEnginePhone7Template
{
    public class RotateCameraScreen : IScene
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
                this.World.AddObject(obj);
            }

            RotatingCamera CameraUpdate = new RotatingCamera(GraphicInfo.Viewport);            
            this.World.CameraManager.AddCamera(CameraUpdate);

            this.BindInput(new SimpleConcreteGestureInputPlayable(Microsoft.Xna.Framework.Input.Touch.GestureType.FreeDrag,
                  (sample) =>
                  {
                      CameraUpdate.RotationInY += sample.Delta.Y * 0.001f;
                      CameraUpdate.RotationInX += sample.Delta.X * 0.001f;
                  }
            ));

            this.BindInput(new SimpleConcreteGestureInputPlayable(Microsoft.Xna.Framework.Input.Touch.GestureType.Pinch,
                  (sample) =>
                  {
                      if (lastDistance != 0)
                      {
                          float dist = (sample.Position - sample.Position2).Length();
                          CameraUpdate.Radius += (dist - lastDistance) * 0.5f;
                          lastDistance = dist;
                      }
                      
                  }
            ));

            this.BindInput(new SimpleConcreteGestureInputPlayable(Microsoft.Xna.Framework.Input.Touch.GestureType.PinchComplete,
                  (sample) =>
                  {
                      lastDistance = 0;
                  }
            ));
        }

        float lastDistance = 0;

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {        
            base.Draw(gameTime, render);
            render.RenderTextComplete("PloobsEngine Camera Sample on Windows Phone7", new Vector2(20, 10), Color.Red, Matrix.Identity);
            render.RenderTextComplete("Use Drag and Pinch to move the camera around the scene", new Vector2(20, 30), Color.Red, Matrix.Identity);
        }

    }
}
