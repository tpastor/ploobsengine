using Microsoft.Xna.Framework;
using PloobsEngine.Cameras;
using PloobsEngine.Light;
using PloobsEngine.Physics;
using PloobsEngine.SceneControl;
using PloobsEngine.Features.DebugDraw;
using PloobsEngine.Modelo;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.Material;
using PloobsEngine.Commands;
using PloobsEngine.Input;
using Lidgren.Network;
using PloobsEngine.NetWorking;
using EngineTestes.Networking;

namespace EngineTestes
{
    /// <summary>
    /// Basic Deferred Scene
    /// </summary>
    public class NetworkingScreen : IScene
    {

        /// <summary>
        /// Sets the world and render technich.
        /// </summary>
        /// <param name="renderTech">The render tech.</param>
        /// <param name="world">The world.</param>
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(-9f,false,1),new SimpleCuller());            

            ///Create the deferred description
            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
            ///Some custom parameter, this one allow light saturation. (and also is a pre requisite to use hdr)
            desc.UseFloatingBufferForLightMap = true;
            ///set background color, default is black
            desc.BackGroundColor = Color.CornflowerBlue;
            ///create the deferred technich
            renderTech = new DeferredRenderTechnic(desc);
        }
              
        
        /// <summary>
        /// Load content for the screen.
        /// </summary>
        /// <param name="GraphicInfo"></param>
        /// <param name="factory"></param>
        /// <param name="contentManager"></param>
        protected override void LoadContent(PloobsEngine.Engine.GraphicInfo GraphicInfo, PloobsEngine.Engine.GraphicFactory factory, IContentManager contentManager)
        {
            ///must be called before all
            base.LoadContent(GraphicInfo, factory, contentManager);

            client = new NetworkCliente();
            client.AddMessageHandler(NetMessageType.PhysicInternalSync, (World.PhysicWorld as BepuPhysicWorld).StartRecieveSyncPhysicMessages);

            ///Create a simple object
            ///Geomtric Info and textures (this model automaticaly loads the texture)
            SimpleModel simpleModel = new SimpleModel(factory, "Model//cenario");
            ///Physic info (position, rotation and scale are set here)
            TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
            ///Shader info (must be a deferred type)
            DeferredNormalShader shader = new DeferredNormalShader();
            ///Material info (must be a deferred type also)
            DeferredMaterial fmaterial = new DeferredMaterial(shader);
            ///The object itself
            IObject obj = new IObject(fmaterial, simpleModel, tmesh);
            ///Add to the world
            this.World.AddObject(obj);
            client.SendTriangleMeshCreationOrder(obj.GetId(), "Model//cenario", Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());

            ///Add some directional lights to completely iluminate the world
            #region Lights
            DirectionalLightPE ld1 = new DirectionalLightPE(Vector3.Left, Color.White);
            DirectionalLightPE ld2 = new DirectionalLightPE(Vector3.Right, Color.White);
            DirectionalLightPE ld3 = new DirectionalLightPE(Vector3.Backward, Color.White);
            DirectionalLightPE ld4 = new DirectionalLightPE(Vector3.Forward, Color.White);
            DirectionalLightPE ld5 = new DirectionalLightPE(Vector3.Down, Color.White);
            float li = 0.4f;
            ld1.LightIntensity = li;
            ld2.LightIntensity = li;
            ld3.LightIntensity = li;
            ld4.LightIntensity = li;
            ld5.LightIntensity = li;
            this.World.AddLight(ld1);
            this.World.AddLight(ld2);
            this.World.AddLight(ld3);
            this.World.AddLight(ld4);
            this.World.AddLight(ld5);
            #endregion
                        
            ///add a camera
            this.World.CameraManager.AddCamera(new CameraFirstPerson(false,GraphicInfo.Viewport));

            SimpleConcreteKeyboardInputPlayable key = new SimpleConcreteKeyboardInputPlayable(StateKey.PRESS, Microsoft.Xna.Framework.Input.Keys.Space);
            key.KeyStateChange += new KeyStateChange(key_KeyStateChange);
            this.BindInput(key);

            
            //LightThrowBepu lt = new LightThrowBepu(
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            client.ProcessMessageSync();
        }

        NetworkCliente client;
        void key_KeyStateChange(InputPlayableKeyBoard ipk)
        {
            SimpleModel simpleModel = new SimpleModel(this.GraphicFactory, "Model//ball");
            ///Physic info (position, rotation and scale are set here)
            SphereObject tmesh = new SphereObject(new Vector3(50,50,10),1,1, 10, MaterialDescription.DefaultBepuMaterial());
            ///Shader info (must be a deferred type)
            DeferredNormalShader shader = new DeferredNormalShader();
            ///Material info (must be a deferred type also)
            DeferredMaterial fmaterial = new DeferredMaterial(shader);
            ///The object itself
            IObject obj = new IObject(fmaterial, simpleModel, tmesh);
            ///Add to the world
            this.World.AddObject(obj);
            client.SendSphereCreationOrder(obj.GetId(), new Vector3(50, 50, 10), 1, 1, 10, MaterialDescription.DefaultBepuMaterial());
        }

    }
}

