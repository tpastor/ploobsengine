using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.IA;

namespace Agent.Test
{
    public class KillAction : Action
    {
        public KillAction(float cost = 0)
        {
            this.Cost = cost;
            Name = "KillAction";
            preWorldState.SetSymbol(new WorldSymbol("EnemyKilled", false));
            preWorldState.SetSymbol(new WorldSymbol("Place", "lugar2"));
            preWorldState.SetSymbol(new WorldSymbol("Armed", true));
            preWorldState.SetSymbol(new WorldSymbol("Protected", true));

            effectWorldState.SetSymbol(new WorldSymbol("EnemyKilled", true));
        }

    }
}
