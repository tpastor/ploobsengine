using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.IA;

namespace Settlers
{
    public class Patrol : PloobsEngine.IA.Action
    {
        public Patrol()
        {
            this.Cost = 10;
            Name = "Patrol";                        
        }

    }
}
