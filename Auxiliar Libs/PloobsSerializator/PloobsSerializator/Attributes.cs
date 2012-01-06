using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsSerializator
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class PloobsSerialize : Attribute
    {
       public PloobsSerialize() 
       {             
       }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = true)]
    public sealed class PloobsSerializeAll : Attribute
    {
        public PloobsSerializeAll()
        {
        }
    }    
    
  }
