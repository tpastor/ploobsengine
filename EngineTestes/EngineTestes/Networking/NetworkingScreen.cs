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
using PloobsEngine.Entity;
using System;

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
            world = new IWorld(new BepuPhysicWorld(-9f,true,1),new SimpleCuller());            

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

            client = new NetworkCliente(World);
            client.RegisterMessageSync(PhysicObjectTypes.SPHEREOBJECT,
                (mes, obj) =>
                {
                    BEPUphysics.Entities.Entity ent = (obj.PhysicObject as BepuEntityObject).Entity;
                    ent.Position = mes.ReadVector3();
                    ent.Orientation = mes.ReadRotation();
                    ent.LinearVelocity = mes.ReadVector3();
                    ent.AngularVelocity = mes.ReadVector3();
                }
            );
            
            NetWorkClientObject no = new NetWorkClientObject("tmesh",
                
                (mes) => 
                    {
                            mes.WriteTrianglemesh("Model//cenario", Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
                            return mes;
                    },
                (mes,id) => 
                    {
                        SimpleModel model;
                        TriangleMeshObject triangleMesh = mes.ReadTrianglemesh(GraphicFactory, out model);
                        DeferredNormalShader shader = new DeferredNormalShader();                        
                        DeferredMaterial fmaterial = new DeferredMaterial(shader);
                        IObject obj =  new IObject(fmaterial, model, triangleMesh);                        
                        obj.SetId(id);
                        return obj;
                    }
            );
            client.CreateNetWorkObject(no);
            

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
            
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            client.ProcessMessageSync();
        }

        NetworkCliente client;
        void key_KeyStateChange(InputPlayableKeyBoard ipk)
        {
            NetWorkClientObject no = new NetWorkClientObject("simpleball",

                (mes) =>
                {
                    mes.WriteSphere(new Vector3(50, 50, 10), 1, 1, 10, MaterialDescription.DefaultBepuMaterial());
                    return mes;
                },
                (mes,id) =>
                {
                        SimpleModel simpleModel = new SimpleModel(this.GraphicFactory, "Model//ball");
                        SphereObject sphere =  mes.ReadSphere();
                        DeferredNormalShader shader = new DeferredNormalShader();                        
                        DeferredMaterial fmaterial = new DeferredMaterial(shader);
                        IObject obj = new IObject(fmaterial, simpleModel, sphere);
                        obj.SetId(id);
                        return obj;
                }
            );

            client.CreateNetWorkObject(no);
        }
         

    }
}

