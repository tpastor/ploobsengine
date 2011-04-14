using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.SceneControl;
using PloobsEngine.Engine;

namespace PloobsEngine.Particles
{
    public abstract class IParticleManager
    {
        public GraphicFactory GraphicFactory
        {
            get;
            internal set;
        }

        public GraphicInfo GraphicInfo
        {
            get;
            internal set;
        }

        public abstract void AddAndInitializeParticleSystem(IParticleSystem particleSystem);
        public abstract void RemoveParticleSystem(IParticleSystem particleSystem);

        protected abstract void Update(GameTime gt, Matrix view, Matrix Projection, Vector3 camPosition);
        internal void iUpdate(GameTime gt, Matrix view, Matrix Projection, Vector3 camPosition)
        {
            Update(gt,view,Projection,camPosition);
        }

        protected abstract void Draw(GameTime gt, Matrix view, Matrix Projection, RenderHelper render);
        internal void iDraw(GameTime gt, Matrix view, Matrix Projection, RenderHelper render)
        {
            Draw(gt,view,Projection,render);
        }

        public abstract bool Enabled
        {
            set;
            get;
        }
        

    }
}
