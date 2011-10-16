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
                        

        public override void AddAndInitializeParticleSystem(IParticleSystem particleSystem)
        {
            System.Diagnostics.Debug.Assert(particleSystem != null, "Particle System cannot be null");            

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
            System.Diagnostics.Debug.Assert(particleSystem != null, "Particle System cannot be null");            
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

        protected override void Update3D(GameTime gt, Matrix view, Matrix Projection, Vector3 camPosition)
        {
            // Set the World, View, and Projection Matrices for the Particle Systems before Drawing them
            manager.SetWorldViewProjectionMatricesForAllParticleSystems(Matrix.Identity, view, Projection);

            // Let all of the particle systems know of the Camera's current position            
            manager.SetCameraPositionForAllParticleSystems(camPosition);

            // Update all of the Particle Systems
            manager.UpdateAllParticleSystems((float)gt.ElapsedGameTime.TotalSeconds);
        }

        protected override void Update2D(GameTime gt, Matrix view, Matrix Projection)
        {
            // Set the World, View, and Projection Matrices for the Particle Systems before Drawing them
            manager.SetWorldViewProjectionMatricesForAllParticleSystems(Matrix.Identity, view, Projection);

            manager.SetTransformationMatrixForAllSpriteParticleSystems(view);

            // Update all of the Particle Systems
            manager.UpdateAllParticleSystems((float)gt.ElapsedGameTime.TotalSeconds);
        }

        protected override void Draw(GameTime gt, Matrix view, Matrix Projection, RenderHelper render)
        {
            manager.DrawAllParticleSystems();
        }

        public override IParticleSystem GetParticleSystem(string particleSystemName)
        {
            if (ParticleSystem.ContainsKey(particleSystemName))
            {
                return ParticleSystem[particleSystemName];
            }
            else
            {
                ActiveLogger.LogMessage("ParticleMessage: " + particleSystemName + " Not Found, returning NULL", LogLevel.Warning);
                return null;
            }
        }
    }
}
