using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DPSF;
using Microsoft.Xna.Framework;
using PloobsEngine.SceneControl;
using PloobsEngine.Engine.Logger;

namespace PloobsEngine.Particles
{
    public class DPSFParticleManager : IParticleManager
    {
        public DPSFParticleManager()
        {
            manager = new ParticleSystemManager();
        }

        private ParticleSystemManager manager;

        public ParticleSystemManager ParticleSystemManager
        {
            get { return manager; }            
        }

        public readonly Dictionary<String, IParticleSystem> ParticleSystem = new Dictionary<string,IParticleSystem>();
                        

        public override void AddParticleSystem(IParticleSystem particleSystem)
        {
            if(ParticleSystem.ContainsKey(particleSystem.Name))
                ActiveLogger.LogMessage("Particle System already exist, overwriting", LogLevel.Warning);            
            ParticleSystem[particleSystem.Name] = particleSystem;
            if(particleSystem is DPFSParticleSystem)
            {
                DPFSParticleSystem ps = particleSystem as DPFSParticleSystem;
                ps.IDPSFParticleSystem.AutoInitialize(GraphicFactory.device, GraphicFactory.contentManager.externalContentManager, GraphicFactory.SpriteBatch);
                manager.AddParticleSystem(ps.IDPSFParticleSystem);            
            }
            else
            {
                ActiveLogger.LogMessage("You can only add DPFS particles in this manager, operation ignored", LogLevel.RecoverableError);
            }
        }

        public override void RemoveParticleSystem(IParticleSystem particleSystem)
        {
            if (particleSystem is DPFSParticleSystem)
            {
                if (ParticleSystem.ContainsKey(particleSystem.Name))
                    manager.RemoveParticleSystem((particleSystem as DPFSParticleSystem).IDPSFParticleSystem);
                else
                {
                    ActiveLogger.LogMessage("Particle System do not Exist",LogLevel.Warning);
                }
            }
            else
            {
                ActiveLogger.LogMessage("You can only remove DPFS particles in this manager, operation ignored", LogLevel.RecoverableError);
            }

        }

        public override bool Enabled
        {
            get
            {
                return manager.Enabled;
            }
            set
            {
                manager.Enabled = value;
            }
        }

        protected override void Update(GameTime gt, Matrix view, Matrix Projection, Vector3 camPosition)
        {
            // Set the World, View, and Projection Matrices for the Particle Systems before Drawing them
            manager.SetWorldViewProjectionMatricesForAllParticleSystems(Matrix.Identity, view, Projection);

            // Let all of the particle systems know of the Camera's current position
            manager.SetCameraPositionForAllParticleSystems(camPosition);

            // Update all of the Particle Systems
            manager.UpdateAllParticleSystems((float)gt.ElapsedGameTime.TotalSeconds);
        }

        protected override void Draw(GameTime gt, Matrix view, Matrix Projection, RenderHelper render)
        {
            manager.DrawAllParticleSystems();
        }
    }
}
