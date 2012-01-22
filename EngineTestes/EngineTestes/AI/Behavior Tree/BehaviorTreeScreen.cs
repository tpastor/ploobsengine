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
using EngineTestes.AI.Behavior_Tree;
using BehaviorTrees;
using PloobsEngine.Utils;

namespace EngineTestes
{
    public class BehaviorTreeScreen : IScene
    {

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

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

            {
                SimpleModel sm = new SimpleModel(factory, "..\\Content\\Model\\block");
                sm.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Blue), TextureType.DIFFUSE);
                ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                CharacterControllerInput character = new CharacterControllerInput(this, new Vector3(100, 150, 1), 1, 1, 50, Vector3.One * 10, 0.5f);
                BaseObject marine = new BaseObject(fmaterial, character.Characterobj,sm);
                this.World.AddObject(marine);

                ///ever !!!
                marine.Behavior = new Repeater<BaseObject>(

				new Watch<BaseObject>(
                    (a,b)=>
                        {
                            ///condition
                            if(a.PhysicObject.Position.Length() < 100)
                            {
                                return TaskResult.Failure;
                            }
                            return TaskResult.Success;
                        }
                    ,
				    
                        ///chnage its color =P
				        new BehaviorTrees.Action<BaseObject>( 
                            (a,b)=>  
                            {
                                a.Modelo.SetTexture(factory.CreateTexture2DColor(1,1,StaticRandom.RandomColor()),TextureType.DIFFUSE);
                                return TaskResult.Success; 
                            }
                            )				    
				)                
                );

                
            }

            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));

        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {        
            base.Draw(gameTime, render);         
        }

    }
}
