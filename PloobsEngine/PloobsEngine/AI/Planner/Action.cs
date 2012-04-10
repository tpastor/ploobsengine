using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

namespace PloobsEngine.IA
{
    public abstract class Action
    {
        protected Action() { }

        public Action(WorldState preCond, WorldState effectCond)
        {
            preWorldState = preCond;
            effectWorldState = effectCond;
        }

        public virtual bool ProceduralPreConditions(WorldState WorldState)
        {
            return true;
        }

        public WorldState GetPreConditions(WorldState WorldState)
        {
            return preWorldState;
        }

        public WorldState GetEffects(WorldState WorldState)
        {
            return effectWorldState;
        }

        public virtual void ApplyEffects(WorldState WorldState)
        {
        }

        protected WorldState preWorldState = new WorldState();
        protected WorldState effectWorldState = new WorldState();       


        public String Name
        {
            get;
            set;
        }


        public float Cost
        {
            get;
            set;
        }


    }
}
