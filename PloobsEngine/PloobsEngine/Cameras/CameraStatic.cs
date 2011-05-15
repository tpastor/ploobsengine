using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Utils;
using PloobsEngine.MessageSystem;
using PloobsEngine.SceneControl;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Cameras
{
    /// <summary>
    /// Static camera
    /// </summary>
    public class CameraStatic : ICamera
    {

        public CameraStatic()
        {
            _view = Matrix.CreateLookAt(_position, _target, _up);
            _projection = Matrix.CreatePerspectiveFieldOfView(_fieldOdView, _aspectRatio, _nearPlane, _farPlane);
            vp = View * Projection;
            _hasmoved = true;
        }

        /// <summary>
        /// Static Camera Constructor
        /// </summary>
        /// <param name="position">Position</param>
        /// <param name="Target">Target Vector (Used to create the view matriz - LookAt method)</param>
        public CameraStatic(Vector3 position, Vector3 Target)
        {
            this.init(position, Target);
        }

        private void init(Vector3 position, Vector3 Target)
        {
            this._target = Target;
            this._position = position;
            _view = Matrix.CreateLookAt(_position, _target, _up);
            _projection = Matrix.CreatePerspectiveFieldOfView(_fieldOdView, _aspectRatio, _nearPlane, _farPlane);
            vp = View * Projection;
            _hasmoved = true;
        }

        #region Fields
        private Matrix vp;             
        private Vector3 _position = Vector3.Right;
        private Vector3 _target = Vector3.Zero;
        private bool _hasmoved = true;
        private Vector3 _up = Vector3.Up;
        private Quaternion _rotation = Quaternion.Identity;
        private float _fieldOdView = MathHelper.PiOver4;
        private float _aspectRatio = 4f / 3f;
        private float _nearPlane = 1.0f;
        private float _farPlane = 1000.0f;
        private Matrix _view;
        private Matrix _projection;        
        private BoundingFrustum _frustrum;        

        #endregion

        #region ICamera Members

        public override bool Hasmoved
        {
            get { return _hasmoved; }
        }

        public override Microsoft.Xna.Framework.Vector3 Position
        {
            get
            {
                return _position;
            }
            set
            {
                 this._position = value;
                 _view = Matrix.CreateLookAt(value, _target, _up);// cria o lookat, com a posicao do objeto como target e o offset com a transgformacao do rigidbody como a posicao da camera
                 _hasmoved = true;

            }
        }

        public override Microsoft.Xna.Framework.Vector3 Target
        {
            get
            {
                return this._target;                    
            }
            set
            {
                this._target = value;
                _view = Matrix.CreateLookAt(_position,value, Up);// cria o lookat, com a posicao do objeto como target e o offset com a transgformacao do rigidbody como a posicao da camera
                _hasmoved = true;
            }
        }

        public override Microsoft.Xna.Framework.Vector3 Up
        {
            get
            {
                return _up;
            }
            set
            {
                this._up = value;
                _view = Matrix.CreateLookAt(_position, _target, value);// cria o lookat, com a posicao do objeto como target e o offset com a transgformacao do rigidbody como a posicao da camera
                _hasmoved = true;
            }
        }

        public override Microsoft.Xna.Framework.Quaternion Rotation
        {
            get
            {                
               _rotation = Quaternion.CreateFromRotationMatrix(Matrix.Invert(_view));
               return _rotation; 
            }
            set
            {
                ActiveLogger.LogMessage("Cant set rotation in this camera: " + Name, LogLevel.RecoverableError);
            }
        }

        public override float FieldOfView
        {
            get
            {
                return this._fieldOdView;
            }
            set
            {
                this._fieldOdView = value;
                _projection = Matrix.CreatePerspectiveFieldOfView(_fieldOdView,_aspectRatio, _nearPlane, _farPlane);
                _hasmoved = true;

            }
        }

        public override float AspectRatio
        {
            get
            {
                return _aspectRatio;
            }
            set
            {
                this._aspectRatio = value;
                _projection = Matrix.CreatePerspectiveFieldOfView(_fieldOdView, _aspectRatio, _nearPlane, _farPlane);
                _hasmoved = true;
            }
        }

        public override float NearPlane
        {
            get
            {
                return this._nearPlane;
            }
            set
            {
                this._nearPlane = value;
                _projection = Matrix.CreatePerspectiveFieldOfView(_fieldOdView, _aspectRatio, _nearPlane, _farPlane);
                _hasmoved = true;
            }
        }

        public override float FarPlane
        {
            get
            {
                return this._farPlane;
            }
            set
            {
                this._farPlane = value;
                _projection = Matrix.CreatePerspectiveFieldOfView(_fieldOdView, _aspectRatio, _nearPlane, _farPlane);
                _hasmoved = true;
            }
        }

        public override Microsoft.Xna.Framework.Matrix View
        {
            get { return _view; }
        }

        public override Microsoft.Xna.Framework.Matrix Projection
        {
            get { return _projection; }
        }

        public override Microsoft.Xna.Framework.BoundingFrustum BoundingFrustum
        {
            get 
            {               
                if (_hasmoved)
                {
                this._frustrum = new BoundingFrustum(ViewProjection);
                _hasmoved = false;
                return this._frustrum;                
                }
                return this._frustrum;

            }
        }

        #endregion       
        
    
        public override Matrix ViewProjection
        {
            get { return vp; }
        }

        protected override void Update(GameTime gt)
        {
         
        }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            ActiveLogger.LogMessage("Serialization not implemented yet", LogLevel.RecoverableError);
        }
    }
}
