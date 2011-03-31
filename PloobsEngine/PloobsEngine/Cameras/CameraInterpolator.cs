using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Utils;
using PloobsEngine.SceneControl;
using PloobsEngine.MessageSystem;

namespace PloobsEngine.Cameras
{

    internal interface cameraInternalInterpolator
    {
        void Start(Vector3 p1, Vector3 p2, Vector3 t1, Vector3 t2, Vector3 u1, Vector3 u2, float field1, float field2 , float aspect1, float aspect2, float near1, float near2, float far1, float far2);        
        void Update(GameTime gt);
        Vector3 getPosistion();
        Vector3 getTarget();
        Vector3 getUp();
        float fieldOfView();
        float aspectRatio();
        float nearPlane();
        bool Ended();
        float farPlane();

    }

    internal class cameraStepInterpol : cameraInternalInterpolator
    {
        public cameraStepInterpol(float step)
        {
            this._step = step;
        }
        #region cameraInternalInterpolator Members

        private float _step;
        private Vec3InterpolatorConstantStep _pinterp;
        private Vec3InterpolatorConstantStep _tinterp;
        private Vec3InterpolatorConstantStep _uinterp;
        private FloatInterpolatorConstantStep _fieldinterp;
        private FloatInterpolatorConstantStep _aspectinterp;
        private FloatInterpolatorConstantStep _nearinterp;
        private FloatInterpolatorConstantStep _farinterp;

        public void Start(Vector3 p1, Vector3 p2, Vector3 t1, Vector3 t2, Vector3 u1, Vector3 u2, float field1, float field2, float aspect1, float aspect2, float near1, float near2, float far1, float far2)
        {
            _pinterp = new Vec3InterpolatorConstantStep(_step);
            _tinterp = new Vec3InterpolatorConstantStep(_step);
            _uinterp = new Vec3InterpolatorConstantStep(_step);
            _fieldinterp = new FloatInterpolatorConstantStep(_step);
            _aspectinterp = new FloatInterpolatorConstantStep(_step);
            _nearinterp = new FloatInterpolatorConstantStep(_step);
            _farinterp = new FloatInterpolatorConstantStep(_step);

            _pinterp.Start(p1, p2, _step);
            _tinterp.Start(t1, t2, _step);
            _uinterp.Start(u1, u2, _step);
            _fieldinterp.Start(field1, field2, _step);
            _aspectinterp.Start(aspect1, aspect2, _step);
            _nearinterp.Start(near1, near2, _step);
            _farinterp.Start(far1, far2, _step);
        }

        public void Update(GameTime gt)
        {
            if (_pinterp.IsActive)
            {
                _pinterp.Update(gt);                
                _tinterp.Update(gt);
                _uinterp.Update(gt);
                _aspectinterp.Update(gt);
                _fieldinterp.Update(gt);
                _nearinterp.Update(gt);
                _farinterp.Update(gt);                
            }
        }

        public Vector3 getPosistion()
        {
            return _pinterp.CurrentValue;
        }

        public Vector3 getTarget()
        {
            return _tinterp.CurrentValue;
        }

        public Vector3 getUp()
        {
            return _uinterp.CurrentValue;
        }

        public float fieldOfView()
        {
            return _fieldinterp.CurrentValue;
        }

        public float aspectRatio()
        {
            return _aspectinterp.CurrentValue;
        }

        public float nearPlane()
        {
            return _nearinterp.CurrentValue;
        }

        public float farPlane()
        {
            return _farinterp.CurrentValue;
        }
        public bool Ended()
        {
            return !_farinterp.IsActive;
        }


    }

    internal class cameraTimeInterpol : cameraInternalInterpolator
    {
        public cameraTimeInterpol(double timeInSeconds)
        {
            this._timeInSeconds = timeInSeconds;
        }
        #region cameraInternalInterpolator Members

        private double _timeInSeconds;
        private Vec3Interpolator _pinterp;
        private Vec3Interpolator _tinterp;
        private Vec3Interpolator _uinterp;
        private FloatInterpolator _fieldinterp;
        private FloatInterpolator _aspectinterp;
        private FloatInterpolator _nearinterp;
        private FloatInterpolator _farinterp;

        public void Start(Vector3 p1, Vector3 p2, Vector3 t1, Vector3 t2, Vector3 u1, Vector3 u2, float field1, float field2, float aspect1, float aspect2, float near1, float near2, float far1, float far2)
        {
            _pinterp = new Vec3Interpolator();
            _tinterp = new Vec3Interpolator();
            _uinterp = new Vec3Interpolator();
            _fieldinterp = new FloatInterpolator();
            _aspectinterp = new FloatInterpolator();
            _nearinterp = new FloatInterpolator();
            _farinterp = new FloatInterpolator();

            _pinterp.Start(p1,p2, _timeInSeconds);
            _tinterp.Start(t1,t2, _timeInSeconds);
            _uinterp.Start(u1, u2, _timeInSeconds);
            _fieldinterp.Start(field1, field2, _timeInSeconds);
            _aspectinterp.Start(aspect1, aspect2, _timeInSeconds);
            _nearinterp.Start(near1, near2, _timeInSeconds);
            _farinterp.Start(far1, far2, _timeInSeconds);
        }

        public void Update(GameTime gt)
        {
            if (_pinterp.IsActive)
            {
                _pinterp.Update(gt);                
                _tinterp.Update(gt);
                _uinterp.Update(gt);
                _aspectinterp.Update(gt);
                _fieldinterp.Update(gt);
                _nearinterp.Update(gt);
                _farinterp.Update(gt);                
            }
        }

        public Vector3 getPosistion()
        {
            return _pinterp.CurrentValue;
        }

        public Vector3 getTarget()
        {
            return _tinterp.CurrentValue;
        }

        public Vector3 getUp()
        {
            return _uinterp.CurrentValue;
        }

        public float fieldOfView()
        {
            return _fieldinterp.CurrentValue;
        }

        public float aspectRatio()
        {
            return _aspectinterp.CurrentValue;
        }

        public float nearPlane()
        {
            return _nearinterp.CurrentValue;
        }

        public float farPlane()
        {
            return _farinterp.CurrentValue;
        }
        public bool Ended()
        {
            return !_farinterp.IsActive;
        }


        #endregion
    }



    public class CameraInterpolator: ICamera
    {
        public delegate void InterpolationFinished(ICamera start, ICamera end);
 
        public void Reset(ICamera actualCamera, ICamera destinyCamera)
        {
            this._actualCamera = actualCamera;
            this._destinyCamera = destinyCamera;
            this._position = actualCamera.Position;
            this._target = actualCamera.Target;
            this._up = actualCamera.Up;

            this._fieldOdView = actualCamera.FieldOfView;
            this._aspectRatio = actualCamera.AspectRatio;
            this._nearPlane = actualCamera.NearPlane;
            this._farPlane = actualCamera.FarPlane;

            this._hasmoved = true;
            this.name = actualCamera.Name + destinyCamera.Name;
            cii.Start(actualCamera.Position, destinyCamera.Position, actualCamera.Target, destinyCamera.Target, actualCamera.Up, destinyCamera.Up, actualCamera.FieldOfView, destinyCamera.FieldOfView, actualCamera.AspectRatio, destinyCamera.AspectRatio, actualCamera.NearPlane, destinyCamera.NearPlane, actualCamera.FarPlane, destinyCamera.FarPlane);
            ended = false;
        }

        public CameraInterpolator(ICamera actualCamera,ICamera destinyCamera ,InterpolationType type , float data)
        {
            if (type == InterpolationType.BYSTEP)
            {
                cii = new cameraStepInterpol(data);                
            }
            else
            {
                cii = new cameraTimeInterpol(data);                
            }
            Reset(actualCamera, destinyCamera);
        }

        #region Fields

        ICamera _actualCamera;
        ICamera _destinyCamera;
        private Vector3 _position = Vector3.Right;
        private Vector3 _target = Vector3.Zero;        
        private Vector3 _up = Vector3.Up;
        private float _fieldOdView = MathHelper.PiOver4;
        private float _aspectRatio = 4f / 3f;
        private float _nearPlane = 1.0f;
        private float _farPlane = 1000.0f;
        private bool _hasmoved;        
        private bool ended;
        private Quaternion _rotation = Quaternion.Identity;        
        private Matrix _view;
        private Matrix _projection;
        private BoundingBox _box;
        private BoundingFrustum _frustrum;
        private int _id;
        public event InterpolationFinished OnInterpolationFinished = null;
        cameraInternalInterpolator cii;
        #endregion

        #region ICamera Members

        public bool Hasmoved
        {
            get { return _hasmoved; }
        }

        public Microsoft.Xna.Framework.Vector3 Position
        {
            get
            {
                return _position;
            }
            set
            {
                this._position = value;
                 _view = Matrix.CreateLookAt(value, _target, _up);// cria o lookat, com a posicao do objeto como target e o offset com a transgformacao do rigidbody como a posicao da camera

            }
        }

        public Microsoft.Xna.Framework.Vector3 Target
        {
            get
            {
                return this._target;                    
            }
            set
            {
                this._target = value;
                _view = Matrix.CreateLookAt(_position,value, Up);// cria o lookat, com a posicao do objeto como target e o offset com a transgformacao do rigidbody como a posicao da camera
            }
        }

        public Microsoft.Xna.Framework.Vector3 Up
        {
            get
            {
                return _up;
            }
            set
            {
                this._up = value;
                _view = Matrix.CreateLookAt(_position, _target, value);// cria o lookat, com a posicao do objeto como target e o offset com a transgformacao do rigidbody como a posicao da camera
            }
        }

        public Microsoft.Xna.Framework.Quaternion Rotation
        {
            get
            {
                //throw new Exception("not yet implemented");
                // _view = Matrix.CreateLookAt(_position, _target, _up);
               _rotation = Quaternion.CreateFromRotationMatrix(Matrix.Invert(_view));
               return _rotation; 
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public float FieldOfView
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

        public float AspectRatio
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

        public float NearPlane
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

        public float FarPlane
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

        public Microsoft.Xna.Framework.Matrix View
        {
            get { return _view; }
        }

        public Microsoft.Xna.Framework.Matrix Projection
        {
            get { return _projection; }
        }

        public Microsoft.Xna.Framework.BoundingFrustum BoundingFrustum
        {
            get 
            {                
                if (_hasmoved)
                {
                this._frustrum = new BoundingFrustum(_view * _projection);
                return this._frustrum;
                }
                return this._frustrum;

            }
        }

        public Microsoft.Xna.Framework.BoundingBox BoundingBox
        {
            get
            {
                this._box = BoundingBox.CreateFromPoints(BoundingFrustum.GetCorners());
                return this._box;                
            }
        }

        public void Update(Microsoft.Xna.Framework.GameTime gt)
        {            
            ended = cii.Ended();

            if (ended == false)
            {
                    cii.Update(gt);
                    _position = cii.getPosistion();
                    _target = cii.getTarget();
                    _up = cii.getUp();
                    _aspectRatio = cii.aspectRatio();
                    _fieldOdView = cii.fieldOfView();
                    _nearPlane = cii.nearPlane();
                    _farPlane = cii.farPlane();
                    _view = Matrix.CreateLookAt(_position, _target, _up);
                    _projection = Matrix.CreatePerspectiveFieldOfView(_fieldOdView, _aspectRatio, _nearPlane, _farPlane);
          }
         else
         {                    
                    if (OnInterpolationFinished != null)
                        OnInterpolationFinished(_actualCamera, _destinyCamera);
                }
            }        

        public bool Ended
        {
            get { return ended; }
            set { ended = value; }
        }

        #endregion

        #region IEntity Members

        public int getId()
        {
            return _id;
        }

        public void setId(int id)
        {
            this._id = id;
        }

        #endregion

        #region IRecieveMessageEntity Members

        public virtual bool HandleThisMessageType(SenderType type)
        {
            return true;
        }

        public virtual void HandleMessage(Message mes)
        {
            
        }

        #endregion                

        
        #endregion


        String name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                this.name = value;
            }
        }

        #region ISerializable Members

        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
