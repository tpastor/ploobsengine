using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.IA;
using System;

namespace Agent.Test
{
    public class GoToAction : PloobsEngine.IA.Action
    {
        public GoToAction(String from, String to, float cost = 0)
        {
            this.Cost = 0;
            this.from = from;
            this.to = to;
            Name = "GOTO: " + from + " -> " + to;
            
            preWorldState.SetSymbol(new WorldSymbol("Place", from));
            effectWorldState.SetSymbol(new WorldSymbol("Place", to));

        }
        String from;
        String to;

    }
}
