using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

namespace PloobsEngine.IA
{
    public sealed class WorldState : IEquatable<WorldState>
    {
        public WorldState Clone()
        {
            WorldState WorldState = new WorldState();
            foreach (var item in this.Symbols)
        	{
		        WorldState.Symbols.Add(item.Key,item.Value.Clone());
        	}
            return WorldState;
        }

        public bool isEqual(WorldState WorldState)
        {
            if (WorldState.Symbols.Count != this.Symbols.Count)
                return false;

            foreach (var item in WorldState.Symbols.Keys)
            {
                if (this.Symbols[item].CompareTo(WorldState.Symbols[item]) == false)
                    return false;
            }
            return true;
        }


        public bool isCompatibleSource(WorldState WorldState)
        {            
            foreach (var item in this.Symbols.Keys)
            {
                if (WorldState.GetSymbol(item).CompareTo(Symbols[item]) == false)
                    return false;                
            }
            return true;
        }

        public bool isCompatibleDestiny(WorldState WorldState)
        {
            foreach (var item in WorldState.GetSymbols())
            {
                if (WorldState.GetSymbol(item.Name).CompareTo(Symbols[item.Name]) == false)
                    return false;
            }
            return true;
        }

        public WorldState GetDiff(WorldState WorldState)
        {
            WorldState w = new WorldState();
            foreach (var item in WorldState.GetSymbols())
            {
                if (!item.CompareTo(this.Symbols[item.Name]))
                {
                    w.SetSymbol(item.Clone());
                }
            }
            return w;
        }

        Dictionary<string, WorldSymbol> Symbols = new Dictionary<string, WorldSymbol>();

        public IEnumerable<WorldSymbol> GetSymbols()
        {
            return Symbols.Values;
        }

        public void SetSymbol(WorldSymbol WorldSymbol)
        {
            Symbols[WorldSymbol.Name] = WorldSymbol;
        }

        public void SetSymbolValue<T>(String name,T value) where T : struct
        {
            WorldSymbol WorldSymbol = new WorldSymbol(name, value);
            Symbols[WorldSymbol.Name] = WorldSymbol;
        }

        public void SetSymbolValue<T>(String name, String value) 
        {
            WorldSymbol WorldSymbol = new WorldSymbol(name, value);
            Symbols[WorldSymbol.Name] = WorldSymbol;
        }

        public WorldSymbol GetSymbol(String name)
        {
            return Symbols[name];
        }

        public T GetSymbolValue<T>(String name) where T : struct
        {
            return Symbols[name].GetSymbol<T>();
        }

        public string GetSymbolValue(String name) 
        {
            return Symbols[name].GetSymbol();
        }

        public object GetGenericSymbolValue(String name)
        {
            return Symbols[name].GetSymbol();
        }

        public bool Equals(WorldState other)
        {
            return other.isEqual(this);
        }

        public override string ToString()
        {
            StringBuilder StringBuilder = new StringBuilder();
            foreach (var item in this.Symbols.Keys)
            {
                StringBuilder.AppendLine(item + "  " + Symbols[item]);
            }
            return StringBuilder.ToString();
        }
    }
}
