#if WINDOWS_PHONE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Utils;
using PloobsEngine.MessageSystem;
using PloobsEngine.SceneControl;
using PloobsEngine.Engine.Logger;
using Microsoft.Xna.Framework.Input.Touch;
using PloobsEngine.Physics;
using PloobsEngine.Input;
using PloobsEngine.Cameras;


namespace EngineTestes
{
    /// <summary>
    /// DUMMY CAMERA
    /// NOT COMPLETE SHOULD NOT BE USED IN REAL APPS
    /// </summary>
    public class RotatingCamera : ICamera
    {             
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CameraFirstPerson"/> class.
        /// </summary>
        /// <param name="lrRot">The leftright rotation.</param>
        /// <param name="udRot">The updown rotation.</param>
        /// <param name="startingPos">The starting pos.</param>
        /// <param name="viewport">The viewport.</param>
        public RotatingCamera(IScreen IScreen, Vector3 center)
        {
            this.center = center;
            init(IScreen);
        }

        public RotatingCamera(IScreen IScreen)
        {
            init(IScreen);
        }

        Vector3 center = Vector3.Zero;

        private void init(IScreen IScreen)
        {

            _aspectRatio = IScreen.GraphicInfo.Viewport.AspectRatio;            
            _projection = Matrix.CreatePerspectiveFieldOfView(_fieldOdView, _aspectRatio, _nearPlane, _farPlane);
            this._frustrum = new BoundingFrustum(_view * _projection);
            
            {

                IScreen.BindInput(new SimpleConcreteGestureInputPlayable(Microsoft.Xna.Framework.Input.Touch.GestureType.FreeDrag,
                (sample) =>
                {
                    this.RotationInY += sample.Delta.Y * 0.001f;
                    this.RotationInX += sample.Delta.X * 0.001f;
                }
          ));
                IScreen.BindInput(new SimpleConcreteGestureInputPlayable(Microsoft.Xna.Framework.Input.Touch.GestureType.Pinch,
                      (sample) =>
                      {
                          // if (lastDistance != 0)
                          {
                              // get the current and previous locations of the two fingers
                              Vector2 a = sample.Position;
                              Vector2 aOld = sample.Position - sample.Delta;
                              Vector2 b = sample.Position2;
                              Vector2 bOld = sample.Position2 - sample.Delta2;

                              // figure out the distance between the current and previous locations
                              float d = Vector2.Distance(a, b);
                              float dOld = Vector2.Distance(aOld, bOld);

                              // calculate the difference between the two and use that to alter the scale
                              float scaleChange = (d - dOld) * .5f;
                              this.Radius -= scaleChange;

                              //cam.AspectRatio = GraphicInfo.BackBufferWidth / GraphicInfo.BackBufferHeight;

                              //float dist = (sample.Position - sample.Position2).Length();
                              //CameraUpdate.Radius += (dist - lastDistance) * 0.5f;
                              //lastDistance = dist;
                          }

                      }
                ));
            }


        }

        #region Fields

        private Matrix viewProjection;
        private Vector3 _position = Vector3.Right;        
        private Vector3 _target = Vector3.Zero;        
        private Vector3 _up = Vector3.Up;        
        private float _fieldOdView = MathHelper.PiOver4;
        private float _aspectRatio = 4f / 3f;
        private float _nearPlane = 1.0f;
        private float _farPlane = 2000f;
        private Matrix _view;
        private Matrix _projection;
        private BoundingFrustum _frustrum;        

        #endregion             

        public override bool Hasmoved
        {
            get { return true; }
        }
        
        
        public override Matrix View
        {
            get { return _view; }
        }

        public override Matrix Projection
        {
            get { return _projection; }
        }

        public override BoundingFrustum BoundingFrustum
        {
            get
            {
                //if (_hasmoved)
                //{
                this._frustrum = new BoundingFrustum(_view * _projection);
                return this._frustrum;
                //}
                //return this._frustrum;

            }
        }

        public float Radius = 100;
        public float RotationInY;
        public float RotationInX;        

        protected override void Update(Microsoft.Xna.Framework.GameTime gt)
        {
            _view = Matrix.CreateTranslation(center) * Matrix.CreateRotationY(RotationInX) * Matrix.CreateRotationX(RotationInY) * Matrix.CreateTranslation(0, 0, -Radius);            
            viewProjection = View * Projection;
        }

        public override Matrix ViewProjection
        {
            get { return viewProjection; }
        }

        public override Vector3 Position
        {
            get
            {
                Matrix viewIT = Matrix.Invert(Matrix.Transpose(_view));
                return new Vector3(viewIT.M14, viewIT.M24, viewIT.M34);

            }
            set
            {
               //// bla
            }
        }

        public override Vector3 Target
        {
            get
            {
                return center;
            }
            set
            {
               //// bla 2               
            }
        }

    }
}
#endif