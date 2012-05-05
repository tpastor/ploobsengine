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
using BehaviorTrees;
using PloobsEngine.Utils;
using EngineTestes;

namespace IntroductionDemo4._0
{
    public class BehaviorTreeScreen : IScene
    {

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(-9.8f), new SimpleCuller());

            ForwardRenderTecnichDescription desc = ForwardRenderTecnichDescription.Default();
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new ForwardRenderTecnich(desc);
        }

        private Vector3 waytogo;

        private Vector3 vel;

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory, IContentManager contentManager)
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

            {
                SimpleModel sm = new SimpleModel(factory, "Model//block");
                sm.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Blue), TextureType.DIFFUSE);
                ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                //CharacterControllerInput character = new CharacterControllerInput(this, new Vector3(100, 150, 1), 1, 1, 50, Vector3.One * 10, 0.5f);
                BoxObject bb = new BoxObject(new Vector3(100, 0, 1), 1, 1, 1, 10, Vector3.One * 10, Matrix.Identity, MaterialDescription.DefaultBepuMaterial());
                BaseObject marine = new BaseObject(fmaterial, bb, sm);
                this.World.AddObject(marine);

                /////ever !!!
                //marine.Behavior = new Repeater<BaseObject>(

                //new Watch<BaseObject>(
                //    (a,b)=>
                //        {

                //            ///condition
                //            if(a.PhysicObject.Position.Length() < 100)
                //            {
                //                return TaskResult.Failure;
                //            }
                //            return TaskResult.Success;
                //        }
                //    ,

                //        ///chnage its color =P
                //        new BehaviorTrees.Action<BaseObject>( 
                //            (a,b)=>  
                //            {
                //                a.Modelo.SetTexture(factory.CreateTexture2DColor(1,1,StaticRandom.RandomColor()),TextureType.DIFFUSE);
                //                return TaskResult.Success; 
                //            }
                //            )				    
                //)                
                //);


                
                marine.Behavior = new Repeater<BaseObject>
                    (
                        new Sequence<BaseObject>()
                        {
                                                new Wait<BaseObject>(0.5f,
                                                    new BehaviorTrees.Action<BaseObject>(
                                                    (a, b) =>
                                                    {
                                                        return TaskResult.Success;
                                                    }
                                                        )
                                                )
                                                ,

                                                 new RepeaterUntil<BaseObject>(
                                        (a,b) => 
                                            {
                                                
                                                

                                                if ((a.PhysicObject.Position - waytogo).Length()>50)
                                                {                                                    
                                                    return TaskResult.Running;
                                                }
                                                else
                                                {
                                                    return TaskResult.Success;
                                                }
                                            }
                                    ,
                                                new BehaviorTrees.Action<BaseObject>(
                                                (a,b) =>
                                                        {
                                                            Vector3 direction = waytogo - a.PhysicObject.Position;
                                                            direction.Y = 0;
                                                            direction.Normalize();
                                                            a.PhysicObject.Velocity = direction*10;
                                                           // a.PhysicObject.Velocity += direction;
                                                            

                                                            return TaskResult.Success;                                                    
                                                        }                                                
                                                    ) 
                                                    ),
                                                              ///chnage its color =P
                                new BehaviorTrees.Action<BaseObject>( 
                                    (a,b)=>  
                                    {
                                        a.Modelo.SetTexture(factory.CreateTexture2DColor(1,1,StaticRandom.RandomColor()),TextureType.DIFFUSE);
                                        return TaskResult.Success; 
                                    }
                                    )	
                                                   
                    
                    
                        }
                    );





            }
            //this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));
            RotatingCamera cam = new RotatingCamera(this, new Vector3(0, -100, -200));
            this.World.CameraManager.AddCamera(cam);
        }

       

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);
            //render.RenderTextComplete("Demo Behavior Tree: Adding Behavior to an object", new Vector2(GraphicInfo.Viewport.Width - 715, 15), Color.White, Matrix.Identity);
            //render.RenderTextComplete("When the controlled object is NEAR the tree, it changes randomly its textures", new Vector2(GraphicInfo.Viewport.Width - 715, 35), Color.White, Matrix.Identity);
            //render.RenderTextComplete("To control the object use TFGH", new Vector2(GraphicInfo.Viewport.Width - 715, 55), Color.White, Matrix.Identity);
            //render.RenderTextComplete("There are LOTS of build in Behaviors Nodes, check the BehaviorTrees namespace", new Vector2(GraphicInfo.Viewport.Width - 715, 75), Color.White, Matrix.Identity);
        }
    }
}
