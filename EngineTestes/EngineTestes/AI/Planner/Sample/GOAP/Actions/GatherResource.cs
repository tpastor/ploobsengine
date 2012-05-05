using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.IA;

namespace Settlers.BlackBoardControl.GOAP.Actions
{
        public class GatherResource : PloobsEngine.IA.Action
        {
            public GatherResource(String resourceName, int Quantity)
            {
                this.Cost = 3;
                Name = "GatherResource";
                this.Quantity = Quantity;
                this.resourceName = resourceName;
            }

            String resourceName;
            int Quantity;            
            public override void ApplyEffects(PloobsEngine.IA.WorldState WorldState)
            {
                ////add the resource
                {
                    WorldSymbol WorldSymbol2 = WorldState.GetSymbol(resourceName);
                    WorldSymbol2.SetSymbol<int>(WorldSymbol2.GetSymbol<int>() + Quantity);
                    WorldState.SetSymbol(WorldSymbol2);
                }


            }

        }
    }
