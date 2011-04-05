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

namespace PloobsEngine.Cameras
{
    public class CameraFirstPerson : ICamera
    {
        public CameraFirstPerson(Viewport viewport)
            : this(0, 0, new Vector3(0, 100, 150), viewport)
        {            
        }
        public CameraFirstPerson(bool useMouse, Viewport viewport)
            : this(0, 0, new Vector3(0, 100, 150), viewport)
        {
            this.useMouse = useMouse;
        }
        public CameraFirstPerson(bool useMouse, Vector3 position, Viewport viewport)
            : this(0, 0, position, viewport)
        {
            this.useMouse = useMouse;
        }


        public void EnableMouse(bool status)
        {
            this.useMouse = status;
        }


        public CameraFirstPerson(float lrRot, float udRot, Vector3 startingPos, Viewport viewport)
        {
            init(lrRot, udRot, startingPos,viewport);
        }

        private void init(float lrRot, float udRot, Vector3 startingPos, Viewport viewport)
        {
            this.leftrightRot = lrRot;
            this.updownRot = udRot;
            this.viewPort = viewport;
            _position = startingPos;            
            _aspectRatio = viewPort.AspectRatio;            
            UpdateViewMatrix();
            Mouse.SetPosition(viewPort.Width / 2, viewPort.Height / 2);
            originalMouseState = Mouse.GetState();
            //_view = Matrix.CreateLookAt(_position, _target, _up);
            _projection = Matrix.CreatePerspectiveFieldOfView(_fieldOdView, _aspectRatio, _nearPlane, _farPlane);
            this._frustrum = new BoundingFrustum(_view * _projection);            
        }

        #region Fields

        private Matrix viewProjection;
        private Vector3 _position = Vector3.Right;
        private bool useMouse = true;
        private Vector3 _target = Vector3.Zero;
        private bool _hasmoved = true;
        private Vector3 _up = Vector3.Up;
        private Quaternion _rotation = Quaternion.Identity;
        private float _fieldOdView = MathHelper.PiOver4;
        private float _aspectRatio = 4f / 3f;
        private float _nearPlane = 1.0f;
        private float _farPlane = 2000f;
        private Matrix _view;
        private Matrix _projection;        
        private BoundingFrustum _frustrum;
        private float leftrightRot;
        private float updownRot;
        private float rotationSpeed = 0.005f;        
        private float sensibility = 0.5f;
        private MouseState originalMouseState;
        private Viewport viewPort;
        private float moveSpeed = 1f;

        #endregion

        public float MoveSpeed
        {
            get { return moveSpeed; }
            set { moveSpeed = value; }
        }

        public float RotationSpeed
        {
            set
            {
                this.rotationSpeed = value;
            }
            get
            {
                return rotationSpeed;
            }
        }



        public float Sensibility
        {
            set
            {
                this.sensibility = value;
            }
            get
            {
                return sensibility;
            }
        }

        

        #region ICamera Members

        public override bool Hasmoved
        {
            get { return _hasmoved; }
        }

        public override Vector3 Position
        {

            get 
            {
                return _position; 
            }
            set
            {
                _position = value;
                UpdateViewMatrix();
            }
        }

        public override Vector3 Up
        {
            get
            {
                return this._up;
            }
            set
            {
                this._up = value;
                UpdateViewMatrix();
            }
        }

        public override Quaternion Rotation
        {
            get
            {
                _rotation = Quaternion.CreateFromRotationMatrix(Matrix.CreateRotationX(UpDownRot) * Matrix.CreateRotationY(LeftRightRot));
               return _rotation; 
            }
            set
            {
                throw new NotImplementedException();
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
            }
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

        private void UpdateViewMatrix()
        {
            Matrix cameraRotation = Matrix.CreateRotationX(updownRot) * Matrix.CreateRotationY(leftrightRot);
            
            Vector3 cameraOriginalTarget = new Vector3(0, 0, -1);
            Vector3 cameraOriginalUpVector = new Vector3(0, 1, 0);

            Vector3 cameraRotatedTarget = Vector3.Transform(cameraOriginalTarget, cameraRotation);
            _target = _position + cameraRotatedTarget;

            _up = Vector3.Transform(cameraOriginalUpVector, cameraRotation);
            //Vector3 cameraFinalUpVector = _position + _up;

            _view = Matrix.CreateLookAt(_position, _target, _up);
        }


        protected override void Update(Microsoft.Xna.Framework.GameTime gt)
        {
             UpdateCamera(Mouse.GetState(),Keyboard.GetState());
             viewProjection = View * Projection;
        }
        
        private void UpdateCamera(MouseState currentMouseState, KeyboardState keyState)
        {
            _hasmoved = false;
            if (currentMouseState != originalMouseState && useMouse == true)
            {
                float xDifference = currentMouseState.X - originalMouseState.X;
                float yDifference = currentMouseState.Y - originalMouseState.Y;
                leftrightRot -= rotationSpeed * xDifference;
                updownRot -= rotationSpeed * yDifference;
                Mouse.SetPosition(viewPort.Width / 2, viewPort.Height / 2); //precisa zerar a posicao 
                UpdateViewMatrix();
                _hasmoved = true;
            }

            if (keyState.IsKeyDown(Keys.Up) || keyState.IsKeyDown(Keys.W))      //Forward
            {
                AddToCameraPosition(new Vector3(0, 0, -sensibility));
                _hasmoved = true;
            }
            if (keyState.IsKeyDown(Keys.Down) || keyState.IsKeyDown(Keys.S))    //Backward
            {
                AddToCameraPosition(new Vector3(0, 0, sensibility));
                _hasmoved = true;
            }
            if (keyState.IsKeyDown(Keys.Right) || keyState.IsKeyDown(Keys.D))   //Right
            {
                AddToCameraPosition(new Vector3(sensibility, 0, 0));
                _hasmoved = true;
            }
            if (keyState.IsKeyDown(Keys.Left) || keyState.IsKeyDown(Keys.A))    //Left
            {
                AddToCameraPosition(new Vector3(-sensibility, 0, 0));
                _hasmoved = true;
            }
            if (keyState.IsKeyDown(Keys.Q))                                     //Up
            {
                AddToCameraPosition(new Vector3(0, sensibility, 0));
                _hasmoved = true;
            }
            if (keyState.IsKeyDown(Keys.Z))                                     //Down
            {
                AddToCameraPosition(new Vector3(0, -sensibility, 0));
                _hasmoved = true;
            }

        }

        #endregion       
                

        public float UpDownRot
        {
            get { return updownRot; }
            set { updownRot = value; }
        }

        public float LeftRightRot
        {
            get { return leftrightRot; }
            set { leftrightRot = value; }
        }

        public override Matrix ViewProjection
        {
            get { return viewProjection; }
        }

        public override Vector3 Target
        {
            get
            {
                return _target;
            }
            set
            {
                Vector3 floorProjection = new Vector3(value.X, 0, value.Z);
                float directionLength = floorProjection.Length();
                updownRot = (float)Math.Atan2(value.Y, value.Length());
                leftrightRot = -(float)Math.Atan2(value.X, -value.Z);                
            }
        }
        public Vector3 Forward
        {
            get
            {
                Matrix cameraRotation = Matrix.CreateRotationX(updownRot) * Matrix.CreateRotationY(leftrightRot);
                Vector3 cameraForward = new Vector3(0, 0, -1);
                Vector3 cameraRotatedForward = Vector3.Transform(cameraForward, cameraRotation);
                return cameraRotatedForward;
            }
        }
        public Vector3 SideVector
        {
            get
            {
                Matrix cameraRotation = Matrix.CreateRotationX(updownRot) * Matrix.CreateRotationY(leftrightRot);
                Vector3 cameraOriginalSide = new Vector3(1, 0, 0);
                Vector3 cameraRotatedSide = Vector3.Transform(cameraOriginalSide, cameraRotation);
                return cameraRotatedSide;
            }
        }
        private void AddToCameraPosition(Vector3 vectorToAdd)
        {
            Matrix cameraRotation = Matrix.CreateRotationX(updownRot) * Matrix.CreateRotationY(leftrightRot);
            Vector3 rotatedVector = Vector3.Transform(vectorToAdd, cameraRotation);
            _position += moveSpeed * rotatedVector;
            UpdateViewMatrix();
        }               



        #region ISerializable Members

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            
        }

        #endregion
    }
}
