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
    /// <summary>
    /// First Person Camera
    /// </summary>
    public class P3DCamera : ICamera
    {

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
            set
            {
                useAcelerometer = value;
                if (useAcelerometer)
                    StartAcelerometer();
            }
        }

        IWorld world;
        /// <summary>
        /// Initializes a new instance of the <see cref="CameraFirstPerson"/> class.
        /// </summary>
        /// <param name="lrRot">The leftright rotation.</param>
        /// <param name="udRot">The updown rotation.</param>
        /// <param name="startingPos">The starting pos.</param>
        /// <param name="viewport">The viewport.</param>
        public P3DCamera(IScene scene, float lrRot, float udRot, Vector3 startingPos, Viewport viewport)
        {
            this.world = scene.World;
            
            init(lrRot, udRot, startingPos, viewport);

            scene.BindInput(new SimpleConcreteGestureInputPlayable(Microsoft.Xna.Framework.Input.Touch.GestureType.FreeDrag,
                  (sample) =>
                  {
                      leftrightRot -= rotationSpeed * sample.Delta.X;
                      updownRot -= rotationSpeed * sample.Delta.Y;
                      UpdateViewMatrix();
                  }
              ));

            scene.BindInput(new SimpleConcreteGestureInputPlayable(Microsoft.Xna.Framework.Input.Touch.GestureType.DoubleTap,
                  (sample) =>
                  {
                            onunseek = true;
                            Matrix projection = world.CameraManager.ActiveCamera.Projection;
                            Matrix viewProjection = world.CameraManager.ActiveCamera.View * world.CameraManager.ActiveCamera.Projection;
                            Matrix viewInverse = Matrix.Invert(world.CameraManager.ActiveCamera.View);
                            Matrix projectionInverse = Matrix.Invert(world.CameraManager.ActiveCamera.Projection);
                            Matrix viewProjectionInverse = projectionInverse * viewInverse;

                            Vector3 v = new Vector3();
                            v.X = (((2.0f * sample.Position.X) / viewPort.Width) - 1);
                            v.Y = -(((2.0f * sample.Position.Y) / viewPort.Height) - 1);
                            v.Z = 0.0f;

                            Ray pickRay = new Ray();
                            pickRay.Position.X = viewInverse.M41;
                            pickRay.Position.Y = viewInverse.M42;
                            pickRay.Position.Z = viewInverse.M43;
                            pickRay.Direction = Vector3.Normalize(Vector3.Transform(v, viewProjectionInverse) - pickRay.Position);

                            SegmentInterceptInfo rti = world.PhysicWorld.SegmentIntersect(pickRay, (a) => true, 9999);
                            if (rti == null)
                            {
                             
                            }
                            else
                            {
                                Vector3 impact = rti.ImpactPosition;
                                Vector3 lookAt = Position - impact;
                                origem = Position;
                                destino = Position + lookAt * 0.8f;
                                passo = 0;
                            }
                        


                  }
              ));

            scene.BindInput(new SimpleConcreteGestureInputPlayable(Microsoft.Xna.Framework.Input.Touch.GestureType.Hold,
                  (sample) =>
                  {
                      onseek = true;
                      Matrix projection = world.CameraManager.ActiveCamera.Projection;
                      Matrix viewProjection = world.CameraManager.ActiveCamera.View * world.CameraManager.ActiveCamera.Projection;
                      Matrix viewInverse = Matrix.Invert(world.CameraManager.ActiveCamera.View);
                      Matrix projectionInverse = Matrix.Invert(world.CameraManager.ActiveCamera.Projection);
                      Matrix viewProjectionInverse = projectionInverse * viewInverse;

                      Vector3 v = new Vector3();
                      v.X = (((2.0f * sample.Position.X) / viewPort.Width) - 1);
                      v.Y = -(((2.0f * sample.Position.Y) / viewPort.Height) - 1);
                      v.Z = 0.0f;

                      Ray pickRay = new Ray();
                      pickRay.Position.X = viewInverse.M41;
                      pickRay.Position.Y = viewInverse.M42;
                      pickRay.Position.Z = viewInverse.M43;
                      pickRay.Direction = Vector3.Normalize(Vector3.Transform(v, viewProjectionInverse) - pickRay.Position);

                      SegmentInterceptInfo rti = world.PhysicWorld.SegmentIntersect(pickRay, (a) => true, 9999);
                      if (rti == null)
                      {
                         
                      }
                      else
                      {
                          Vector3 impact = rti.ImpactPosition;
                          Vector3 lookAt = impact - Position;
                          destino = Position + lookAt * 0.8f;
                          origem = Position;
                          passo = 0;
                      }
                  }
              ));


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


        }


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

        Microsoft.Devices.Sensors.Accelerometer accelSensor;
        Vector3 speedAcel = new Vector3();


        #endregion

        public float MoveSpeed
        {
            get { return moveSpeed; }
            set
            {
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
            UpdateCamera(Mouse.GetState(), Keyboard.GetState());
            viewProjection = View * Projection;
        }

        bool onseek = false;
        bool onunseek = false;

        private void UpdateCamera(MouseState currentMouseState, KeyboardState keyState)
        {
            //_hasmoved = false;

            if (onseek || onunseek)
            {
                Position = MathUtils.Interpolate(passo, origem, destino);
                passo += 0.05f;
                if (passo >= 1)
                {
                    onunseek = false;
                    onseek = false;
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



        }


        Vector3 origem;
        Vector3 destino;
        float passo;


        public float UpDownRot
        {
            get { return updownRot; }
            set
            {
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
            set
            {
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

    }
}
#endif