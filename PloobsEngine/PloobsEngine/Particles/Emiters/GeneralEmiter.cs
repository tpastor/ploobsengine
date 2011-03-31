using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.Particle3D
{

    public  delegate void OnRemove(IEmiter em);

    public abstract class GeneralEmiter : IEmiter
    {
        public GeneralEmiter()
        {
            Toggle = true;
        }

        #region IEmiter Members

        protected ParticleManager particleManager;

        public Dictionary<string, ParticleSystem> ParticlesSystems
        {
            get;
            set;
        }

        public void Update(Microsoft.Xna.Framework.GameTime gt)
        {
            if (Toggle)
            {
                UpdateEmiter(gt);
            }
        }

        public abstract void UpdateEmiter(Microsoft.Xna.Framework.GameTime gt);
        public virtual void Initialize() { }
        public event OnRemove OnRemove;

        #endregion



        #region IEmiter Members


        public bool Toggle 
        {
            get;
            set;
        }

        #endregion

        #region IEmiter Members


        public void Remove()
        {
            particleManager.RemoveEmiter(this);
            if(OnRemove!=null)
            OnRemove(this);
        }

        #endregion

        #region IEmiter Members

        
        public void Init(ParticleManager pm)
        {
            this.particleManager = pm;
            Initialize();
        }

        #endregion

        #region IEmiter Members


        public abstract string[] ParticleSystemUsed();
        

        #endregion
    }
}
