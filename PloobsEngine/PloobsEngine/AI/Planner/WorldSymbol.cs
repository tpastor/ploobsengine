using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.IA
{
    public class WorldSymbol
    {
        public WorldSymbol(String name, ValueType obj)
        {
            Name = name;
            this.obj = obj;
        }

        public WorldSymbol(String name, String obj)
        {
            Name = name;
            this.obj = obj;
        }
                
        protected WorldSymbol()
        {            
        }

        internal WorldSymbol(String name, object obj)
        {
            Name = name;
            this.obj = obj;
        }


        public readonly String Name;

        public void SetSymbol<T>(T value) where T : struct
        {
            obj = value;
        }

        public void SetSymbol(String value) 
        {
            obj = value;
        }


        public T GetSymbol<T>() where T : struct
        {
            return (T)obj;
        }

        public String GetSymbol() 
        {
            return (String)obj;
        }

        public virtual bool CompareTo(WorldSymbol WorldSymbol)
        {
            return WorldSymbol.obj.Equals(this.obj);
        }

        object obj;
               

        public virtual WorldSymbol Clone()
        {
            WorldSymbol WorldSymbol = new WorldSymbol(Name,obj);
            WorldSymbol.obj = obj;
            return WorldSymbol; 
                 
        }
        public override string ToString()
        {
            return obj.ToString();
        }
    }
}
