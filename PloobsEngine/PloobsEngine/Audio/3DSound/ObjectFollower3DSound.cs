using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using PloobsEngine.SceneControl;

namespace PloobsEngine.Audio
{
    public class ObjectFollower3DSound : ISoundEmitter3D
    {
        public ObjectFollower3DSound(IContentManager cmanager, string audioname,IObject follow)
            : base(cmanager,audioname)
        {
            System.Diagnostics.Debug.Assert(follow != null);
            this.follower = follow;
        }

        IObject follower;

        protected override void Update(Microsoft.Xna.Framework.GameTime gt, Cameras.ICamera camera)
        {            
            Emiter.Position = follower.Position;
            Emiter.Velocity = follower.PhysicObject.Velocity;
            Emiter.Forward = follower.PhysicObject.FaceVector;
            
            Listener.Position = camera.Position;
            Listener.Up = camera.Up;            

            SoundEngineInstance.Apply3D(Listener, Emiter);   
        }
    }
}
