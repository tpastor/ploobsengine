using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Cameras;
using PloobsEngine.SceneControl;


namespace PloobsEngine.Particle3D
{
    public class ParticleManager
    {
        private Dictionary<String, ParticleSystem> _particleSystems = new Dictionary<string, ParticleSystem>();
        private List<ParticleSystem> _active = new List<ParticleSystem>();
        private List<IEmiter> _emiters = new List<IEmiter>();
        private List<ParticleSystem> toBeRemoved = new List<ParticleSystem>();
        bool AddLock = false;        
        private List<IEmiter> wlistAdd = new List<IEmiter>();
        private List<IEmiter> wlistRemove = new List<IEmiter>();   

        public void AddEmiter(IEmiter emiter)
        {
            if (AddLock == false)
            {
                emiter.Init(this);
                Dictionary<String, ParticleSystem> pss = new Dictionary<string, ParticleSystem>();
                foreach (var item in emiter.ParticleSystemUsed())
                {
                    ParticleSystem p = _particleSystems[item].Clone();
                    pss.Add(item, p);
                    _active.Add(p);
                }
                emiter.ParticlesSystems = pss;
                _emiters.Add(emiter);
            }
            else
            {
                wlistAdd.Add(emiter);
            }
        }

        public void RemoveEmiter(IEmiter emiter)
        {
                foreach (var item in emiter.ParticlesSystems.Values)
                {
                    item.ToRemove = true;
                }
                wlistRemove.Add(emiter);
         
        }

        public void AddParticleSystem(ParticleSystem ps)
        {
            _particleSystems.Add(ps.Name, ps);
        }

        public void RemoveParticleSystem(ParticleSystem ps)
        {
            _particleSystems.Remove(ps.Name);
        }

        public void RemoveParticleSystem(String name)
        {
            _particleSystems.Remove(name);
        }

        public void Update(GameTime gt)
        {
            toBeRemoved.Clear();

            foreach (var item in _emiters)
            {
                AddLock = true;
                item.Update(gt);
                AddLock = false;
            }

            foreach (ParticleSystem item in _active)
            {
                if (item.Update(gt) == true)
                {
                    toBeRemoved.Add(item);
                }
            }

            foreach (var item in toBeRemoved)
            {
                _active.Remove(item);
            }

            foreach (var item in wlistAdd)
            {
                this.AddEmiter(item);
            }

            foreach (var item in wlistRemove)
            {
                _emiters.Remove(item);
            }

            wlistRemove.Clear();
            wlistAdd.Clear();
        }

        public void Draw(IRenderHelper render, GameTime gt, ICamera cam)
        {
            foreach (ParticleSystem item in _active)
            {
                item.SetCamera(cam.View, cam.Projection);
                item.Draw(render, gt);
            }
        }


    }
}
