using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine;
using PloobsEngine.SceneControl;
using PloobsEngine.Input;
using PloobsEngine.Commands;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using PloobsEngine.Modelo;
using PloobsEngine.Material;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.Physics;
using PloobsEngine.Engine;
using PloobsEngine.Particles;
using DPSF;

namespace AdvancedDemo4._0
{
    public class ObjectThrowSmokeParticle : IScreenUpdateable
    {
        IScene scene;
        int particleSystemNumber = 0;
        public ObjectThrowSmokeParticle(IScene scene)
            : base(scene)
        {
                this.scene = scene;  
                ///Register a function to be called when the the mouse is pressed
                InputPlaybleMouseBottom ip1 = new SimpleConcreteMouseBottomInputPlayable(StateKey.PRESS, EntityType.IOBJECT, MouseButtons.LeftButton, mousebuttonteste);
                scene.BindInput(ip1);            
        }

        public void mousebuttonteste(MouseState ms)
        {
            SmokeParticleSystem smoke = new SmokeParticleSystem();
            DPFSParticleSystem ps = new DPFSParticleSystem("smoke" + particleSystemNumber, smoke);
            scene.World.ParticleManager.AddAndInitializeParticleSystem(ps);


            ///Create an object
            ThrowParticlesObject physObj = SpawnPrimitive(smoke,this.scene.World.CameraManager.ActiveCamera.Position, Matrix.CreateRotationX(0.5f));
            physObj.PhysicObject.Velocity = (this.scene.World.CameraManager.ActiveCamera.Target - this.scene.World.CameraManager.ActiveCamera.Position) * 10.0f;            
            scene.World.AddObject(physObj);

        }
        /// <summary>
        /// Create a simple Sphere object
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="ori"></param>
        /// <returns></returns>
        private ThrowParticlesObject SpawnPrimitive(DefaultTexturedQuadParticleSystem particleSystem, Vector3 pos, Matrix ori)
        {
            ///Load a Model with a custom texture
            SimpleModel sm2 = new SimpleModel(scene.GraphicFactory, "Model\\ball");
            sm2.SetTexture(scene.GraphicFactory.CreateTexture2DColor(1, 1, Color.White, false), TextureType.DIFFUSE);
            DeferredNormalShader nd = new DeferredNormalShader();
            IMaterial m = new DeferredMaterial(nd);
            SphereObject pi2 = new SphereObject(pos, 1, 10, 1, MaterialDescription.DefaultBepuMaterial());
            ThrowParticlesObject o = new ThrowParticlesObject(particleSystem,m, sm2, pi2);
            return o;
        }

        protected override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            
        }
    }
}
