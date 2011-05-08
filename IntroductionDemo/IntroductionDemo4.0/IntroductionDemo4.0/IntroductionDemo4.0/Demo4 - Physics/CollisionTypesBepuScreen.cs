using PloobsEngine.SceneControl;
using PloobsEngine;
using System.Collections.Generic;
using PloobsEngine.Physics;
using PloobsEngine.Input;
using PloobsEngine.Modelo;
using Microsoft.Xna.Framework;
using PloobsEngine.Material;
using PloobsEngine.Commands;
using PloobsEngine.Cameras;
using PloobsEngine.Light;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Utils;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.Engine;

namespace IntroductionDemo4._0
{
    /// <summary>
    /// Screen showing some of the Bepu collision Skins 
    /// Same as JigLibX, only the constructor order of the physics objects chances (intentionaly !!!)
    /// </summary>
    public class CollisionTypesBepuScreen : IScene
    {        
        private List<IObject> objects = new List<IObject>();
        BindKeyCommand mm;
        LightThrowBepu lightThrow;

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(-0.98f,true,1,true), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
            desc.UseFloatingBufferForLightMap = true;
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new DeferredRenderTechnic(desc);
        }

        protected override void InitScreen(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);

            ///Add the Input Component
            ///InputAdvanced is responsible for abstracting the xna input layer.            
            InputAdvanced inp = new InputAdvanced();
            engine.AddComponent(inp);
        }

        protected override void LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory, IContentManager contentManager)
        {
            ///Must be called before everything in the LoadContent
            base.LoadContent(GraphicInfo, factory, contentManager);            

            ///Create a Simple Model
            IModelo sm = new SimpleModel(factory,"..\\Content\\Model\\cenario");            
            ///Create a Physic Object
            IPhysicObject pi = new TriangleMeshObject(sm, Vector3.Zero,Matrix.Identity,Vector3.One,MaterialDescription.DefaultBepuMaterial());
            pi.isMotionLess = true;
            ///Create a shader 
            IShader shader = new DeferredNormalShader();            
            ///Create a Material
            IMaterial mat = new DeferredMaterial(shader);
            ///Create a an Object that englobs everything and add it to the world
            IObject obj4 = new IObject(mat, sm,pi);
            this.World.AddObject(obj4);


            ///Create the Physic Objects
            {
                for (int i = 0; i < 15; i++)
                {
                    CreateThrash(new Vector3(-70 + i * 5, 50, 10));
                }

                for (int i = 0; i < 15; i++)
                {
                    CreateBox(new Vector3(-70 + i * 7, 100, 50));
                }

                for (int i = 0; i < 15; i++)
                {
                    CreateThrash(new Vector3(-70 + i * 5, 80, 50));
                }

                for (int i = 0; i < 15; i++)
                {
                    CreateBall(new Vector3(-70 + i * 5, 50, 30));
                }

                for (int i = 0; i < 15; i++)
                {
                    CreateBox(new Vector3(-70 + i * 7, 130, -20));
                }

                for (int i = 0; i < 15; i++)
                {
                    CreateBox(new Vector3(-70 + i * 7, 60, -50));
                }


                ///Create A Ghost Object (Do Not Collide)
                {
                    ///Create a Simple Model
                    SimpleModel model = new SimpleModel(factory, "..\\Content\\Model\\ball");
                    model.SetTexture(factory.CreateTexture2DColor(1,1,Color.Purple), TextureType.DIFFUSE);                    
                    ///Create a Physic Object
                    IPhysicObject pobj = new GhostObject(new Vector3(50, 13f, 50), Matrix.Identity, Vector3.One * 5);
                    pobj.isMotionLess = true;
                    ///Create a shader   
                    IShader nd = new DeferredNormalShader();
                    ///Create a Material
                    IMaterial material = new DeferredMaterial(shader);
                    ///Create a an Object that englobs everything and add it to the world
                    IObject obj = new IObject(material, model, pobj);
                    this.World.AddObject(obj);

                }

            }

            ///Call the function releaseObjects when Space key is pressed
            InputPlayableKeyBoard ip1 = new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS, Microsoft.Xna.Framework.Input.Keys.Space, releaseObjects, EntityType.TOOLS);
            mm = new BindKeyCommand(ip1, BindAction.ADD);
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(mm);

            ///Create a FirstPerson Camera
            ///This is a special camera, used in the development
            ///You can move around using wasd / qz / and the mouse
            CameraFirstPerson cam = new CameraFirstPerson(GraphicInfo.Viewport);            
            this.World.CameraManager.AddCamera(cam);

            ///Create some directionals lights and add to the world
            DirectionalLightPE ld = new DirectionalLightPE(Vector3.Forward, Color.White);
            DirectionalLightPE ld2 = new DirectionalLightPE(Vector3.Left, Color.White);
            DirectionalLightPE ld3 = new DirectionalLightPE(Vector3.Down, Color.White);
            ld.LightIntensity = 0.5f;
            ld2.LightIntensity = 0.5f;
            ld3.LightIntensity = 0.5f;
            this.World.AddLight(ld);
            this.World.AddLight(ld2);
            this.World.AddLight(ld3);

            lightThrow = new LightThrowBepu(this.World,factory);

            this.RenderTechnic.AddPostEffect(new AntiAliasingPostEffect());
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);        
            render.RenderTextComplete("Demo: Collision Types (BEPU)", new Vector2(GraphicInfo.Viewport.Width - 315, 15), Color.White,Matrix.Identity);
            render.RenderTextComplete("Space = Drop objects", new Vector2(GraphicInfo.Viewport.Width - 315, 40), Color.White,Matrix.Identity);
                        
        }

        private void CreateBall(Vector3 pos)
        {
            ///Create a Simple Model
            SimpleModel model = new SimpleModel(GraphicFactory,"..\\Content\\Model\\ball");
            model.SetTexture(GraphicFactory.CreateTexture2DColor(1, 1, StaticRandom.RandomColor()), TextureType.DIFFUSE);            

            ///Create a Physic Object
            IPhysicObject pobj = new SphereObject(pos,1, 10,1,MaterialDescription.DefaultBepuMaterial());
            pobj.isMotionLess = true;
            ///Create a shader   
            IShader nd = new DeferredNormalShader();
            
            ///Create a Material                
            IMaterial material = new DeferredMaterial(nd);
            ///Create a an Object that englobs everything and add it to the world
            IObject obj = new IObject(material, model, pobj);
            this.World.AddObject(obj);
            objects.Add(obj);
        }

        private void CreateThrash(Vector3 pos)
        {
            ///Create a Simple Model
            ///thash Has a Texture Inside the .X file (not the texture itself, just the name of it) 
            ///The simpleModel will find it and will attach to the Object
            IModelo model = new SimpleModel(GraphicFactory,"..\\Content\\Model\\trash");
            
            ///Create a Physic Object
            IPhysicObject pobj = new CapsuleObject(pos,2,1,10,Matrix.Identity,MaterialDescription.DefaultBepuMaterial());
            pobj.isMotionLess = true;
            ///Create a shader 
            IShader nd = new DeferredNormalShader();

            ///Create a Material                
            IMaterial material = new DeferredMaterial(nd);
            ///Create a an Object that englobs everything and add it to the world
            IObject obj = new IObject(material, model, pobj);
            this.World.AddObject(obj);
            objects.Add(obj);            
        }

        private void CreateBox(Vector3 pos)
        {
            ///Create a Simple Model
            SimpleModel model = new SimpleModel(GraphicFactory,"..\\Content\\Model\\cubo");
            model.SetTexture(GraphicFactory.CreateTexture2DColor(1,1, StaticRandom.RandomColor()), TextureType.DIFFUSE);
            
            ///Create a Physic Object
            IPhysicObject pobj = new BoxObject(pos,1,1,1, 10, new Vector3(2),Matrix.Identity,MaterialDescription.DefaultBepuMaterial());
            pobj.isMotionLess = true;
            ///Create a shader   
            IShader nd = new DeferredNormalShader();

            ///Create a Material                
            IMaterial material = new DeferredMaterial(nd);
            ///Create a an Object that englobs everything and add it to the world
            IObject obj = new IObject(material, model,pobj);
            this.World.AddObject(obj);
            objects.Add(obj);
        }

        /// <summary>
        /// Release the objects
        /// </summary>
        /// <param name="ipk"></param>
        private void releaseObjects(InputPlayableKeyBoard ipk)
        {
            foreach (var item in objects)
            {
                item.PhysicObject.isMotionLess = false;
            }
        }

        protected override void  CleanUp(EngineStuff engine)
        {
         	base.CleanUp(engine);

            lightThrow.CleanUp();

            mm.BindAction = BindAction.REMOVE;
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(mm);
        }
    }
}
