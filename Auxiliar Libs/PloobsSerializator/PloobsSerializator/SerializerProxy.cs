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

    public class DeSerializerProxy
    {
        internal XElement current;
        internal SerializatorMachine SerializatorMachine;
        internal XMLDeserializer XMLDeserializer;

        public Object DeSerialize(String fieldname)
        {
            var matches = from el in current.Elements()
                          where el.Name == fieldname
                          select el;
            var mat = matches.First();

            return XMLDeserializer.populateobj(mat);            
        }
    }
}
