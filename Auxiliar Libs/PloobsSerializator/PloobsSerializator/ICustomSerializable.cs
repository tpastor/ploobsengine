using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PloobsSerializator
{
    public interface ICustomSerializable
    {
        object Deserialize(DeSerializerProxy DeSerializerProxy);
        void Serialize(SerializerProxy SerializerProxy);
    }

    public interface ICustomDeserializable
    {
        void Deserialize(DeSerializerProxy DeSerializerProxy);
    }
}
