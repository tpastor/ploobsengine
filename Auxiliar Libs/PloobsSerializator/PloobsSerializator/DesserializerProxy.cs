using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PloobsSerializator
{
        public class DeSerializerProxy
        {
            public object Context;

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

            public T DeSerialize<T>(String fieldname)
            {
                var matches = from el in current.Elements()
                              where el.Name == fieldname
                              select el;
                var mat = matches.First();

                return (T)XMLDeserializer.populateobj(mat);
            }
        }
    }
