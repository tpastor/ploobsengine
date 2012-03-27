
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


namespace PloobsEnginePhone7Template
{
    public class RotatingCamera : ICamera
    {             
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CameraFirstPerson"/> class.
        /// </summary>
        /// <param name="lrRot">The leftright rotation.</param>
        /// <param name="udRot">The updown rotation.</param>
        /// <param name="startingPos">The starting pos.</param>
        /// <param name="viewport">The viewport.</param>
        public RotatingCamera(Viewport viewport)
        {
            init(viewport);
        }

        private void init(Viewport viewport)
        {
            this.viewPort = viewport;            
            _aspectRatio = viewPort.AspectRatio;            
            _projection = Matrix.CreatePerspectiveFieldOfView(_fieldOdView, _aspectRatio, _nearPlane, _farPlane);
            this._frustrum = new BoundingFrustum(_view * _projection);
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
        private Viewport viewPort;

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
            
            _view = Matrix.CreateTranslation(Vector3.Zero) * Matrix.CreateRotationY(RotationInX) * Matrix.CreateRotationX(RotationInY) * Matrix.CreateTranslation(0, 0, -Radius);            
            viewProjection = View * Projection;
        }

        public override Matrix ViewProjection
        {
            get { return viewProjection; }
        }


    }
}
#endif