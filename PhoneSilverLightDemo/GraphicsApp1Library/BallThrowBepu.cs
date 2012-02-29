using System;
using PloobsEngine.SceneControl;
using PloobsEngine;
using PloobsEngine.Input;
using Microsoft.Xna.Framework;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.Modelo;
using PloobsEngine.Material;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Utils;
using PloobsEngine.Commands;
using Microsoft.Xna.Framework.Input;
using PloobsEngine.Light;
using PloobsEngine.Engine;
using PloobsEngine.Physics;
using Microsoft.Xna.Framework.Input.Touch;

namespace EngineTestes
{
    public class BallThrowBepu : IScreenUpdateable
    {
        IWorld _mundo;
        Random rd = new Random();
        GraphicFactory factory;

        protected override void Update(GameTime gameTime)
        {            
        }

        public BallThrowBepu(IScene scene, GraphicFactory factory, GestureType type)
            : base(scene)
        {
            this.Start();
            this.factory = factory;
            _mundo = scene.World;

            SimpleConcreteGestureInputPlayable SimpleConcreteGestureInputPlayable = new SimpleConcreteGestureInputPlayable(type,
               (sample) =>
               {
                   IObject physObj = SpawnPrimitive(_mundo.CameraManager.ActiveCamera.Position, Matrix.Identity);
                   physObj.PhysicObject.Velocity = (_mundo.CameraManager.ActiveCamera.Target - _mundo.CameraManager.ActiveCamera.Position) * 10.0f;
                   physObj.Name = "FlyingBall " + ++i;
                   _mundo.AddObject(physObj);
                   (physObj.Material.Shader as ForwardXNABasicShader).BasicEffect.EnableDefaultLighting();
               }
           );
            scene.BindInput(SimpleConcreteGestureInputPlayable);
        }
        


        int i = 0;
      

        /// <summary>
        /// Create a simple Sphere object
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="ori"></param>
        /// <returns></returns>
        private IObject SpawnPrimitive(Vector3 pos, Matrix ori)
        {
            ///Load a Model with a custom texture
            SimpleModel sm2 = new SimpleModel(factory, "Model\\ball");
            sm2.SetTexture(factory.CreateTexture2DColor(1, 1, Color.White, false), TextureType.DIFFUSE);
            ForwardXNABasicShader nd = new ForwardXNABasicShader();
            IMaterial m = new ForwardMaterial(nd);
            SphereObject pi2 = new SphereObject(pos, 1, 0.5f, 1, MaterialDescription.DefaultBepuMaterial());
            IObject o = new IObject(m, sm2, pi2);
            
            return o;
        }

    }
}
