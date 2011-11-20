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
    public class InputGestureScreen : IScene
    {
        int doubleTapCount = 0;
        bool doubleTapDisabled = false;
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

            var newCameraFirstPerson = new CameraStatic(new Vector3(100,100,100), - new Vector3(100,100,100) );
            this.World.CameraManager.AddCamera(newCameraFirstPerson);

            SimpleConcreteGestureInputPlayable SimpleConcreteGestureInputPlayable = new SimpleConcreteGestureInputPlayable(Microsoft.Xna.Framework.Input.Touch.GestureType.DoubleTap, (sample) => doubleTapCount++);
            this.BindInput(SimpleConcreteGestureInputPlayable);

            this.BindInput(new SimpleConcreteGestureInputPlayable(Microsoft.Xna.Framework.Input.Touch.GestureType.Hold, 
                (sample) => 
                    {
                        this.RemoveInputBinding(SimpleConcreteGestureInputPlayable);
                        doubleTapDisabled = true;
                    }
            ));
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {        
            base.Draw(gameTime, render);
            render.RenderTextComplete("PloobsEngine Gestures with Windows Phone7", new Vector2(20, 10), Color.Red, Matrix.Identity);
            render.RenderTextComplete("DoubleTap Count " + doubleTapCount, new Vector2(20, 30), Color.Red, Matrix.Identity);
            if(doubleTapDisabled)
                render.RenderTextComplete("DoubleTap Disabled" , new Vector2(20, 50), Color.Red, Matrix.Identity);
        
        }

    }
}
