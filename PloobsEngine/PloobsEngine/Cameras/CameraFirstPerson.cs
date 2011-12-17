#region License
/*
    PloobsEngine Game Engine Version 0.3 Beta
    Copyright (C) 2011  Ploobs

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion
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
using PloobsEngine.Engine;
#if WINDOWS_PHONE
using Microsoft.Xna.Framework.Input.Touch;
#endif

namespace PloobsEngine.Cameras
{
    /// <summary>
    /// First Person Camera
    /// To be used in Ploobs Demos and in Debug Mode.
    /// It is not a camera to be used in Production Environment
    /// </summary>
    public class CameraFirstPerson : ICamera
    {
#if WINDOWS_PHONE
        bool useAcelerometer = false;

        /// <summary>
        /// Gets or sets a value indicating whether [use acelerometer].
        /// Is true is passed, the acelerometer is also started
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use acelerometer]; otherwise, <c>false</c>.
        /// </value>
        public bool UseAcelerometer
        {
          get { return useAcelerometer; }
          set { 
              useAcelerometer = value;
              if (useAcelerometer)
              {
                  StartAcelerometer();
              }
          }
        }
#endif


        /// <summary>
        /// Initializes a new instance of the <see cref="CameraFirstPerson"/> class.
        /// </summary>
        /// <param name="graphicInfo">The graphic info.</param>
        public CameraFirstPerson(GraphicInfo graphicInfo)
            : this(0, 0, new Vector3(0, 100, 150), graphicInfo)
        {            
        }
#if WINDOWS
        /// <summary>
        /// Initializes a new instance of the <see cref="CameraFirstPerson"/> class.
        /// </summary>
        /// <param name="useMouse">if set to <c>true</c> [use mouse].</param>
        /// <param name="viewport">The viewport.</param>
        public CameraFirstPerson(bool useMouse, GraphicInfo graphicInfo)
            : this(0, 0, new Vector3(0, 100, 150), graphicInfo)
        {
            this.useMouse = useMouse;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraFirstPerson"/> class.
        /// </summary>
        /// <param name="useMouse">if set to <c>true</c> [use mouse].</param>
        /// <param name="position">The position.</param>        
        public CameraFirstPerson(bool useMouse, Vector3 position, GraphicInfo graphicInfo)
            : this(0, 0, position, graphicInfo)
        {
            this.useMouse = useMouse;
        }

        /// <summary>
        /// Enables the mouse control.
        /// </summary>
        /// <param name="status">if set to <c>true</c> [status].</param>
        public void EnableMouse(bool status)
        {
            this.useMouse = status;
        }
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraFirstPerson"/> class.
        /// </summary>
        /// <param name="lrRot">The leftright rotation.</param>
        /// <param name="udRot">The updown rotation.</param>
        /// <param name="startingPos">The starting pos.</param>
        /// <param name="viewport">The viewport.</param>        
#if WINDOWS_PHONE
        /// <param name="useAcelerometer">if set to <c>true</c> [use acelerometer].</param>
        public CameraFirstPerson(float lrRot, float udRot, Vector3 startingPos, GraphicInfo graphicInfo, bool useAcelerometer = false)
#else
            public CameraFirstPerson(float lrRot, float udRot, Vector3 startingPos, GraphicInfo graphicInfo)
#endif
        {            
            init(lrRot, udRot, startingPos,graphicInfo);

#if WINDOWS_PHONE
            this.useAcelerometer = useAcelerometer;
            if (useAcelerometer)
            {
                accelSensor = new Microsoft.Devices.Sensors.Accelerometer();
                // Start the accelerometer
                try
                {
                    accelSensor.Start();
                    accelActive = true;
                }
                catch (Microsoft.Devices.Sensors.AccelerometerFailedException e)
                {
                    // the accelerometer couldn't be started.  No fun!
                    accelActive = false;
                }
                catch (UnauthorizedAccessException e)
                {
                    // This exception is thrown in the emulator-which doesn't support an accelerometer.
                    accelActive = false;
                }
                accelSensor.ReadingChanged += new EventHandler<Microsoft.Devices.Sensors.AccelerometerReadingEventArgs>(accelSensor_ReadingChanged);
            }
#endif

        }

#if WINDOWS_PHONE
        void accelSensor_ReadingChanged(object sender, Microsoft.Devices.Sensors.AccelerometerReadingEventArgs e)
        {
            speedAcel.X = (float)e.X;
            speedAcel.Y = (float)e.Y;
            speedAcel.Z = (float)e.Z;
        }

        bool accelActive = false;        
        public void StartAcelerometer()
        {
            if (useAcelerometer)
            {
                if (accelActive == true)
                    return;

                try
                {
                    if (accelSensor == null)
                    {
                        accelSensor = new Microsoft.Devices.Sensors.Accelerometer();
                        accelSensor.ReadingChanged += new EventHandler<Microsoft.Devices.Sensors.AccelerometerReadingEventArgs>(accelSensor_ReadingChanged);
                    }

                    accelSensor.Start();
                    accelActive = true;
                }
                catch (Microsoft.Devices.Sensors.AccelerometerFailedException e)
                {
                    // the accelerometer couldn't be started.  No fun!
                    accelActive = false;
                }
                catch (UnauthorizedAccessException e)
                {
                    // This exception is thrown in the emulator-which doesn't support an accelerometer.
                    accelActive = false;
                }
            }
            else
            {
                ActiveLogger.LogMessage("need to enable acelerometer before trying to start it on the camerafirstperson", LogLevel.RecoverableError);
            }
        }   

        public void StopAcelerometer()
        {
            if (useAcelerometer)
            {
                // Stop the accelerometer if it's active.
                if (accelActive)
                {
                    try
                    {
                        accelSensor.Stop();
                    }
                    catch (Microsoft.Devices.Sensors.AccelerometerFailedException e)
                    {
                        // the accelerometer couldn't be stopped now.
                    }
                }
            }
        }

#endif

        private void init(float lrRot, float udRot, Vector3 startingPos, GraphicInfo graphicInfo)
        {
            this.ginfo = graphicInfo;
            this.leftrightRot = lrRot;
            this.updownRot = udRot;            
            _position = startingPos;
            _aspectRatio = graphicInfo.Viewport.AspectRatio;
            graphicInfo.OnGraphicInfoChange += new OnGraphicInfoChange(graphicInfo_OnGraphicInfoChange);
            UpdateViewMatrix();
#if WINDOWS
            Mouse.SetPosition(graphicInfo.Viewport.Width / 2, graphicInfo.Viewport.Height / 2);
            originalMouseState = Mouse.GetState();
#endif
            _projection = Matrix.CreatePerspectiveFieldOfView(_fieldOdView, _aspectRatio, _nearPlane, _farPlane);
            this._frustrum = new BoundingFrustum(_view * _projection);            
        }

        void graphicInfo_OnGraphicInfoChange(GraphicInfo newGraphicInfo)
        {
            _aspectRatio = newGraphicInfo.Viewport.AspectRatio;
            _projection = Matrix.CreatePerspectiveFieldOfView(_fieldOdView, _aspectRatio, _nearPlane, _farPlane);
            this._frustrum = new BoundingFrustum(_view * _projection);            
        }

        #region Fields

        private Matrix viewProjection;
        private Vector3 _position = Vector3.Right;
#if WINDOWS
        private bool useMouse = true;
        private MouseState originalMouseState;
#endif
        private GraphicInfo ginfo;
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
        private float moveSpeed = 1f;
#if WINDOWS_PHONE
        Microsoft.Devices.Sensors.Accelerometer accelSensor;
        Vector3 speedAcel = new Vector3();
#endif

        #endregion

        public float MoveSpeed
        {
            get { return moveSpeed; }
            set {
                if (value <= 0)
                {
                    ActiveLogger.LogMessage("CameraFirstPerson MoveSpeed cannot be <= 0, nothing changed", LogLevel.RecoverableError);
                }
                else
                {
                    moveSpeed = value;
                }
            }
        }

        public float RotationSpeed
        {
            set
            {
                if (value <= 0)
                {
                    ActiveLogger.LogMessage("CameraFirstPerson RotationSpeed cannot be <= 0, nothing changed", LogLevel.RecoverableError);                    
                }
                else
                {
                    this.rotationSpeed = value;
                }
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
                if (value <= 0)
                {
                    ActiveLogger.LogMessage("CameraFirstPerson Sensibility cannot be <= 0, nothing changed", LogLevel.RecoverableError);
                }
                else
                {
                    this.sensibility = value;
                }
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
                ActiveLogger.LogMessage("CameraFirstPerson Rotation cannot be setted directely, set lrRot or udRot instead", LogLevel.Warning);
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
                if (value <= 0)
                {
                    ActiveLogger.LogMessage("CameraFirstPerson Field of view cannot be <= 0, nothing changed", LogLevel.RecoverableError);
                }
                else
                {
                    this._fieldOdView = value;
                    _projection = Matrix.CreatePerspectiveFieldOfView(_fieldOdView, _aspectRatio, _nearPlane, _farPlane);
                }

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
                if (value <= 0)
                {
                    ActiveLogger.LogMessage("CameraFirstPerson AspectRation cannot be <= 0, nothing changed", LogLevel.RecoverableError);
                }
                else
                {
                    this._aspectRatio = value;
                    _projection = Matrix.CreatePerspectiveFieldOfView(_fieldOdView, _aspectRatio, _nearPlane, _farPlane);
                }
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
                if (value <= 0)
                {
                    ActiveLogger.LogMessage("CameraFirstPerson NearPlane cannot be <= 0, nothing changed", LogLevel.RecoverableError);
                }
                else
                {
                    this._nearPlane = value;
                    _projection = Matrix.CreatePerspectiveFieldOfView(_fieldOdView, _aspectRatio, _nearPlane, _farPlane);
                }
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
                if (value <= 0)
                {
                    ActiveLogger.LogMessage("CameraFirstPerson FarPLane cannot be <= 0, nothing changed", LogLevel.RecoverableError);
                }
                else
                {
                    this._farPlane = value;
                    _projection = Matrix.CreatePerspectiveFieldOfView(_fieldOdView, _aspectRatio, _nearPlane, _farPlane);
                }
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
            #if WINDOWS
            if (currentMouseState != originalMouseState && useMouse == true)
            {
                float xDifference = currentMouseState.X - originalMouseState.X;
                float yDifference = currentMouseState.Y - originalMouseState.Y;
                leftrightRot -= rotationSpeed * xDifference;
                updownRot -= rotationSpeed * yDifference;
                Mouse.SetPosition(ginfo.Viewport.Width / 2, ginfo.Viewport.Height / 2); //precisa zerar a posicao 
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
#else
            TouchCollection tc = TouchPanel.GetState();
            if (tc.Count > 0)
            {
                if (tc[0].State == TouchLocationState.Moved)
                {
                    if (tcpressed.Count != 0)
                    {
                        Vector2 Difference = tc[0].Position - tcpressed[0].Position;
                        leftrightRot -= rotationSpeed * Difference.X;
                        updownRot -= rotationSpeed * Difference.Y;
                        UpdateViewMatrix();
                    }
                    _hasmoved = true;                    
                    tcpressed = tc;
                }                
            }

            if (accelActive)
            {
                if (speedAcel != Vector3.Zero)
                {
                    AddToCameraPosition(speedAcel);
                    _hasmoved = true;     
                }
            }

        #endif

        }

        #endregion       
              
        #if WINDOWS_PHONE
        TouchCollection tcpressed;        
        #endif

        public float UpDownRot
        {
            get { return updownRot; }
            set {
                if (value <= 0)
                {
                    ActiveLogger.LogMessage("CameraFirstPerson UpDownRot cannot be <= 0, nothing changed", LogLevel.RecoverableError);
                }
                else
                {
                    updownRot = value;
                }
            }
        }

        public float LeftRightRot
        {
            get { return leftrightRot; }
            set {
                if (value <= 0)
                {
                    ActiveLogger.LogMessage("CameraFirstPerson LeftRightRot cannot be <= 0, nothing changed", LogLevel.RecoverableError);
                }
                else
                {
                    leftrightRot = value;
                }
            }
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
#if !WINDOWS_PHONE
	
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            ActiveLogger.LogMessage("Serialization not implemented yet", LogLevel.RecoverableError);
        }
#endif

        #endregion
    }
}
