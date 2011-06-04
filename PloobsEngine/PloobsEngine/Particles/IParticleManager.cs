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
        public abstract IParticleSystem GetParticleSystem(String particleSystemName);

        protected abstract void Update3D(GameTime gt, Matrix view, Matrix Projection, Vector3 camPosition);
        internal void iUpdate3D(GameTime gt, Matrix view, Matrix Projection, Vector3 camPosition)
        {
            Update3D(gt,view,Projection,camPosition);
        }

        protected abstract void Update2D(GameTime gt, Matrix view, Matrix Projection);
        internal void iUpdate2D(GameTime gt, Matrix view, Matrix Projection)
        {
            Update2D(gt, view, Projection);
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
