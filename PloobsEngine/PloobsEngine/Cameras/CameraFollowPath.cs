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
using Microsoft.Xna.Framework;
using PloobsEngine.SceneControl;
using PloobsEngine.MessageSystem;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Cameras
{
    public delegate void OnPathEnded(CameraFollowPath cam);

    public class CameraFollowPath : ICamera
    {
        private CameraPathData data;
        private String nextCam;
        private IWorld world;
        private bool ended = false;
        private bool onLoop = false;
        private Matrix vp;

        public event OnPathEnded OnPathEnded = null;

        public bool OnLoop
        {
            get { return onLoop; }
            set { onLoop = value; }
        }

        public bool Ended
        {
            get { return ended; }
            set { ended = value; }
        }
        
        public void Restart()     
        {
            ended = false;
            time = 0;
        }

        public CameraFollowPath(CameraPathData data,IWorld world , String nextCam )
        {            
            this.world = world;
            this.nextCam = nextCam;
            this.data = data;            
            _projection = Matrix.CreatePerspectiveFieldOfView(_fieldOdView, _aspectRatio, _nearPlane, _farPlane);
        }
        

        private void init(Vector3 position, Vector3 Target)
        {
            this._target = Target;
            this._position = position;
            _view = Matrix.CreateLookAt(_position, _target, _up);            
        }

        #region Fields

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
        private BoundingBox _box;
        private BoundingFrustum _frustrum;
        private int _id;

        #endregion

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
                this._position = value;                 

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
            }
        }

        public override Microsoft.Xna.Framework.Quaternion Rotation
        {
            get
            {
                ActiveLogger.LogMessage("this camera cant rotate: " + Name, LogLevel.RecoverableError);
                return Quaternion.Identity;
            }
            set
            {
                ActiveLogger.LogMessage("this camera cant rotate", LogLevel.RecoverableError);
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
                return this._frustrum;
                }
                return this._frustrum;

            }
        }

        private float time;
        protected override void Update(Microsoft.Xna.Framework.GameTime gt)
        {
            time += (float)  gt.ElapsedGameTime.TotalMilliseconds;
            if (data.getMaxTime() < time)
            {
                if (onLoop == true)
                {
                    if (OnPathEnded != null)
                        OnPathEnded(this);
                    Restart();
                    return;
                }
                
                ICamera cnext = world.CameraManager.GetCamera(nextCam);

                cnext.Position = this.Position;
                cnext.Target = this.Target;
                cnext.Up = this.Up;

                String old_active = world.CameraManager.GetActiveCameraName();
                world.CameraManager.SetActiveCamera(cnext.Name);
                world.CameraManager.RemoveCamera(old_active);                
                ended = true;

                if (OnPathEnded != null)
                    OnPathEnded(this);                

                return;
            }
            _target = data.GetTarget(time);
            _up = data.GetUp(time);
            _position = data.GetHead(time);
            _view = Matrix.CreateLookAt(_position, _target, _up);
            vp = View * Projection;
        }

        #endregion        
        
        public override Matrix ViewProjection
        {
            get { return vp; }
        }

#if !WINDOWS_PHONE
	
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            ActiveLogger.LogMessage("Serialization not implemented yet", LogLevel.RecoverableError);
        }
#endif
    }
}
