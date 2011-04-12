using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine.Logger;
using PloobsEngine.Engine;

namespace PloobsEngine.Particles
{
    public abstract class IParticleSystem
    {        

        public IParticleSystem(String name)
        {
            if (String.IsNullOrEmpty(name))
            {
                ActiveLogger.LogMessage("ParticleSystem name cannot be null/empty", LogLevel.FatalError);
                throw new Exception("ParticleSystem name cannot be null/empty");
            }
            
            this.Name = name;
        }

        public String Name
        {
            get;
            set;
        }
    }
}
