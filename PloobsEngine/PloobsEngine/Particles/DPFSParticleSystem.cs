using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DPSF;
using PloobsEngine.Engine.Logger;
using PloobsEngine.Engine;

namespace PloobsEngine.Particles
{
    public class DPFSParticleSystem : IParticleSystem
    {
        public DPFSParticleSystem(String name, IDPSFParticleSystem IDPSFParticleSystem)
            : base(name)
        {
            if (IDPSFParticleSystem == null)
            {
                ActiveLogger.LogMessage("IDPSFParticleSystem cannot be null", LogLevel.FatalError);
                throw new Exception("IDPSFParticleSystem cannot be null");
            }
            this.IDPSFParticleSystem = IDPSFParticleSystem;
        }

        public IDPSFParticleSystem IDPSFParticleSystem
        {
            get;
            set;
        }        
    }
}
