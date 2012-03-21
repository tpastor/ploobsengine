using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine;
using Microsoft.Xna.Framework.Input;
using PloobsEngine.SceneControl._2DScene;
using Microsoft.Xna.Framework;
using PloobsEngine.Physic2D.Farseer;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Dynamics;

namespace PloobsEnginePhone7Template
{
    public class JointUpdateable : IScreenUpdateable
    {
        FarseerWorld world;
        ICamera2D camera;
        private FixedMouseJoint _fixedMouseJoint;
        public JointUpdateable(I2DScene scene, FarseerWorld world,ICamera2D camera)
            : base(scene)
        {
            this.camera = camera;
            this.world = world;
            this.Start();
        }

        protected override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            Vector2 position = camera.ConvertScreenToWorld(new Vector2(ms.X,ms.Y));

            if (ms.LeftButton == ButtonState.Pressed && _fixedMouseJoint == null)
            {
                Fixture savedFixture = world.World.TestPoint(position);
                if (savedFixture != null)
                {
                    Body body = savedFixture.Body;
                    _fixedMouseJoint = new FixedMouseJoint(body, position);
                    _fixedMouseJoint.MaxForce = 1000.0f * body.Mass;
                    world.World.AddJoint(_fixedMouseJoint);
                    body.Awake = true;
                }
            }

            if (ms.LeftButton == ButtonState.Released && _fixedMouseJoint != null)
            {
                world.World.RemoveJoint(_fixedMouseJoint);
                _fixedMouseJoint = null;
            }

            if (_fixedMouseJoint != null)
            {
                _fixedMouseJoint.WorldAnchorB = position;
            }
        }        
    }
}
