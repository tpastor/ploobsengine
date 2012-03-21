using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PloobsSerializator
{
    public class SerializerProxy
    {
        internal SerializatorMachine SerializatorMachine ;
        internal XmlSerializer Serializer;

        public void Serialize(String fieldname, object obj)
        {
            SerializatorMachine.SerializeType(fieldname, obj, Serializer);
        }
    }

    
}
