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
using PloobsEngine.Engine.Logger;
#if WINDOWS_PHONE
using PloobsEngine.Utils;
#endif

namespace PloobsEngine.Cameras
{
    public enum InterpolationType
    {
        BYTIME,BYSTEP
    }

    public enum TransitionMode
    {
        INTERPOLATED,IMEDIATELY
    }

    public enum State
    {
        INTERPOLATING,NORMAL
    }

    public struct CameraDescription
    {        
        public String name;
        public ICamera cam;
    };

    public class CameraManager
    {
        public static readonly String DEFAULTCAMERA  = "defaultCamera";
        public static readonly String INTERPOLATORCAMERA = "interpolatorCamera";        
        private List<CameraDescription> _cameras = new List<CameraDescription>();        
        private ICamera _activeCam = null;
        private int _activeCameraIndex = -1;
        private State activeCameraType;
        
        public CameraManager()
        {           
                 
        }

        /// <summary>
        /// Adiciona uma camera
        /// Se a camera tiver o nome DEFAULTCAMERA ela vira a camera atual tb
        /// </summary>
        /// <param name="cam"></param>
        /// <param name="name"></param>
        public void AddCamera(ICamera cam, String name)
        {
            if (cam == null || String.IsNullOrEmpty(name))
            {
                ActiveLogger.LogMessage("Can add null camera or invalid name, Skipping adding this Camera", LogLevel.RecoverableError);
                return;
            }
            
            CameraDescription cc;
            cc.cam = cam;
            cc.name = name;            
            _cameras.Add(cc);
        }

        
        /// <summary>
        /// Add a camera and set it active if not yet was setted
        /// </summary>
        /// <param name="cam"></param>
        public void AddCamera(ICamera cam)
        {
            if (cam == null )
            {
                ActiveLogger.LogMessage("Can add null camera, Skipping adding this Camera", LogLevel.RecoverableError);
                return;
            }

            if (String.IsNullOrEmpty(cam.Name))
            {
                AddCamera(cam, DEFAULTCAMERA);
                if (_activeCam == null)
                    SetActiveCamera(DEFAULTCAMERA);
            }
            else
            {
                AddCamera(cam, cam.Name);
                if (_activeCam == null)
                    SetActiveCamera(cam.Name);
            }
        }

        /// <summary>
        /// Remove uma Camera
        /// Cuidado, pois ela pode estar Ativa !!!
        /// </summary>
        /// <param name="name">nome da camera</param>
        public void RemoveCamera(String name)
        {
            CameraDescription a = _cameras.First(delegate(CameraDescription t) { return t.name == name; });            

            if (a.cam == _activeCam)
            {                
                throw new Exception("cant remove actual camera");                
            }           

            _cameras.Remove( a );
            
        }

        /// <summary>
        /// Determines whether the specified camera name was added.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        ///   <c>true</c> if the specified name exist; otherwise, <c>false</c>.
        /// </returns>
        public bool HasCamera(String name)
        {
            return  _cameras.Exists(delegate(CameraDescription t) { return t.name == name; });
        }
        
        /// <summary>
        /// Retorna a Camera ativa
        /// </summary>
        public ICamera ActiveCamera
        {
            get
            {                
                return _activeCam;
            }
        }

        /// <summary>
        /// Seta uma camera ativa
        /// modo de transicao Imediata
        /// </summary>
        /// <param name="name"></param>
        public void SetActiveCamera(String name)
        {
            activeCameraType = State.NORMAL;
            _activeCameraIndex = _cameras.FindIndex(delegate(CameraDescription t) { return t.name == name; });
            if (_activeCameraIndex == -1)
            {
                ActiveLogger.LogMessage("Camera Name Not found, maybe you did not added it to the camera manager", LogLevel.RecoverableError);
            }
            else
            {
                _activeCam = _cameras[_activeCameraIndex].cam;
            }
        }
        /// <summary>
        /// Seta uma camera ativa
        /// modo de transicao Imediata
        /// </summary>
        /// <param name="name"></param>
        /// <param name="time">tempo de demora ou velocidade  -> para controlar o interpolador</param>
        public void SetActiveCamera(String name,  InterpolationType type ,float timeOrStep)
        {
            if (activeCameraType == State.NORMAL)
            {
                activeCameraType = State.INTERPOLATING;
                int ind = _cameras.FindIndex(delegate(CameraDescription t) { return t.name == name; });
                if (ind == -1)
                {
                    ActiveLogger.LogMessage("Camera Name Not found, maybe you did not added it to the camera manager", LogLevel.RecoverableError);
                    return;
                }
                CameraInterpolator ci = new CameraInterpolator(_activeCam, _cameras[ind].cam, type, timeOrStep);               

                ci.OnInterpolationFinished += new CameraInterpolator.InterpolationFinished(ci_OnInterpolationFinished);
                ci.Name = INTERPOLATORCAMERA;
                CameraDescription cc = new CameraDescription();
                cc.cam = ci;
                cc.name = INTERPOLATORCAMERA;
                _cameras.Add(cc);
                int added = _cameras.FindIndex(delegate(CameraDescription t) { return t.name == INTERPOLATORCAMERA; });
                _activeCam = _cameras[added].cam;
            }
            else
            {
                CameraInterpolator ac = (_activeCam as CameraInterpolator);                
                int ind = _cameras.FindIndex(delegate(CameraDescription t) { return t.name == name; });
                if (ind == -1)
                {
                    ActiveLogger.LogMessage("Camera Name Not found, maybe you did not added it to the camera manager", LogLevel.RecoverableError);
                }
                ac.Reset(_activeCam, _cameras[ind].cam);                
            }
        }

        void ci_OnInterpolationFinished(ICamera start, ICamera end)
        {
            activeCameraType = State.NORMAL;
            SetActiveCamera(end.Name);
            RemoveCamera(INTERPOLATORCAMERA);
        }

        /// <summary>
        /// Retorna o nome da camera ativa
        /// </summary>
        /// <returns></returns>
        public String GetActiveCameraName()
        {
            if (_activeCameraIndex == -1)
            {
                ActiveLogger.LogMessage("No camera has been added yet, can access any, returning null", LogLevel.RecoverableError);
                return null;
            }
            return _cameras[_activeCameraIndex].name;
        }
        
        /// <summary>
        /// Retorna uma lista das cameras ativas
        /// </summary>
        /// <returns></returns>
        public List<CameraDescription> GetCamerasDescription()
        {
            return _cameras;
        }
        public State ActiveCameraType
        {
            get { return activeCameraType; }
        }

        public ICamera GetCamera(String name)
        {            
            int camIndex = _cameras.FindIndex(delegate(CameraDescription t) { return t.name == name; });
            if(camIndex  == -1)
            {
                ActiveLogger.LogMessage("Camera not found in Camera Manager, are you sure you added it ?!", LogLevel.RecoverableError);
                return null;
            }

            return _cameras[camIndex].cam;            
        }
    }
}
