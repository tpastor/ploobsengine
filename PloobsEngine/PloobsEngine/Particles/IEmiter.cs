using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Particle3D
{
    public interface IEmiter
    {

        void Init(ParticleManager pm);

        Dictionary<String, ParticleSystem> ParticlesSystems
        {
            set;
            get;
        }

        void Update(GameTime gt);  
        /// <summary>
        /// Turn on or off
        /// </summary>
        /// <param name="ligar"></param>
        bool Toggle
        {
            set;
            get;
        }

        void Remove();

        string[] ParticleSystemUsed();
        
    }
}
