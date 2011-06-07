using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Audio
{
    public class Static3DSound : ISoundEmitter3D
    {
        public Static3DSound(IContentManager cmanager, string audioname, Vector3 Position)
            : base(cmanager,audioname)
        {
            EmiterPosition = Position;
        }

        public Vector3 EmiterPosition
        {
            get
            {
                return emiter.Position;
            }
            set
            {
                emiter.Position = value;
            }
        }

        protected override void Update(Microsoft.Xna.Framework.GameTime gt, Cameras.ICamera camera)
        {   
            Listener.Position = camera.Position;
            Listener.Up = camera.Up;            
            SoundEngineInstance.Apply3D(Listener, Emiter);   
        }
    }
}
