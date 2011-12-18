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
using Microsoft.Xna.Framework.Input.Touch;

namespace EngineTestes
{
    public class JointUpdateable : IScreenUpdateable
    {
        FarseerWorld world;
        ICamera2D camera;

        public ICamera2D Camera
        {
            get { return camera; }
            set { camera = value; }
        }
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
            // Process touch events
            TouchCollection touchCollection = TouchPanel.GetState();
            if (touchCollection.Count > 0)
            {
                TouchLocation tl = touchCollection[0];

                Vector2 position = camera.ConvertScreenToWorld(new Vector2(tl.Position.X,
                         tl.Position.Y));

                if (tl.State == TouchLocationState.Pressed && _fixedMouseJoint == null)
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

                if (tl.State == TouchLocationState.Released && _fixedMouseJoint != null)
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
}
