using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.IA;

namespace Agent.Test
{
    public class BeProtectedAction : Action
    {
        public BeProtectedAction(float cost = 0)
        {
            this.Cost = cost;
            Name = "BeProtected";
            preWorldState.SetSymbol(new WorldSymbol("Place", "lugar0"));
            preWorldState.SetSymbol(new WorldSymbol("Protected", false));
            effectWorldState.SetSymbol(new WorldSymbol("Protected", true));
        }    
    }
}
