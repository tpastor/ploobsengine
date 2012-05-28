using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.IA;

namespace Agent.Test
{
    public class BeArmedAction : Action
    {
        public BeArmedAction(float cost = 0)            
        {
            this.Cost = cost;
            Name = "BeArmed";
            preWorldState.SetSymbol(new WorldSymbol("Armed", false));
            preWorldState.SetSymbol(new WorldSymbol("Place", "lugar1"));

            effectWorldState.SetSymbol(new WorldSymbol("Armed", true));
        }

    }
}
