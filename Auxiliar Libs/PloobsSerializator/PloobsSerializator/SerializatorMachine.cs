using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Globalization;
using System.Collections;
using System.Xml.Linq;
using System.Runtime.Remoting.Contexts;

namespace PloobsSerializator
{
    public class SerializatorMachine
    {
        public SerializatorMachine()
        {
            SerializerProxy.SerializatorMachine = this;
            DeSerializerProxy.SerializatorMachine = this;

            des.Add(typeof(String),
             (x, a, b) =>
             {
                 b.Serialize(x, a);
             }
         );

            des.Add(typeof(Int32),
                (x, a, b) =>
                {
                    b.Serialize(x, a);
                }
            );
            des.Add(typeof(Single),
                (x, a, b) =>
                {
                    b.Serialize(x, a);
                }
            );

            des.Add(typeof(Int16),
                (x, a, b) =>
                {
                    b.Serialize(x, a);
                }
            );

            des.Add(typeof(Int64),
                (x, a, b) =>
                {
                    b.Serialize(x, a);
                }
            );
            
         

            desserializador.Add(typeof(string),
             (a) =>
             {
                 return a.Value.Clone();
             }
         );

            desserializador.Add(typeof(Int32),
                (a) =>
                {
                    return Convert.ToInt32(a.Value);
                }
            );
            desserializador.Add(typeof(Single),
                (a) =>
                {
                    return Convert.ToSingle(a.Value);
                }
            );
            desserializador.Add(typeof(Int16),
                (a) =>
                {
                    return Convert.ToInt16(a.Value);
                }
            );
            desserializador.Add(typeof(Int64),
                (a) =>
                {
                    return Convert.ToInt16(a.Value);
                }
            );

         
        }

        SerializerProxy SerializerProxy = new SerializerProxy();
        DeSerializerProxy DeSerializerProxy = new DeSerializerProxy();

        public void Serialize(object obj, String path, Encoding Encoding, Dictionary<Type, Action<String,object, XmlSerializer>> helpers = null)
        {
            if (helpers != null)
            {
                foreach (var item in helpers)
                {
                    des[item.Key] = item.Value;
                }
            }

            XmlSerializer Serializer = new XmlSerializer();

            SerializerProxy.Serializer = Serializer;
            StreamWriter StreamWriter = new StreamWriter(path, false, Encoding);
            Serializer.Start(Encoding.Default);
            String name = obj.GetType().Name;
            name = GetStringNoAccents(name);
            SerializeType(name, obj, Serializer);
            Serializer.End(StreamWriter);
            StreamWriter.Close();
        }


        public T Desserialize<T>(String path, object Context = null, Dictionary<Type,Func<XElement, object>> des =null)
            where T : class             
        {   
            return (T)Desserialize(path,Context, des);
        }

        Dictionary<Type, Func<XElement, object>> desserializador = new Dictionary<Type, Func<XElement, object>>(); 
        public object Desserialize(String path,object Context = null,Dictionary<Type, Func<XElement, object>> helpers = null)         
        {
            if (helpers != null)
            {
                foreach (var item in helpers)
                {
                    desserializador[item.Key] = item.Value;
                }
            }

            DeSerializerProxy.Context = Context;
            XMLDeserializer desserializer = new XMLDeserializer(DeSerializerProxy, desserializador);
            DeSerializerProxy.XMLDeserializer = desserializer;
            return desserializer.Load(path);
        }

        Dictionary<Type, Action<String, object, XmlSerializer>> des = new Dictionary<Type, Action<String, object, XmlSerializer>>();


        internal void SerializeType(String fieldname, object obj, XmlSerializer Serializer, bool isrray = false)
        {

            if (obj.GetType().GetInterface("IList") != null && obj.GetType().IsArray == false)
            {
                Serializer.BeginType(fieldname, obj.GetType().AssemblyQualifiedName);
                System.Collections.IList l = obj as System.Collections.IList;
                foreach (var item in l)
                {
                    SerializeType("List",item,Serializer);   
                }
                Serializer.EndType(fieldname);
                return;
            }

            if (obj.GetType().GetInterface("IDictionary") != null && obj.GetType().IsArray == false)
            {
                Serializer.BeginType(fieldname, obj.GetType().AssemblyQualifiedName);
                System.Collections.IDictionary l = obj as System.Collections.IDictionary;
                foreach (var item in l)
                {
                    DictionaryEntry DictionaryEntry = (DictionaryEntry)item;
                    Serializer.BeginType("KeyValuePar", obj.GetType().AssemblyQualifiedName);
                    SerializeType("Key", DictionaryEntry.Key, Serializer);
                    SerializeType("Value", DictionaryEntry.Value, Serializer);
                    Serializer.EndType("KeyValuePar");
                }
                Serializer.EndType(fieldname);
                return;
            }


            if(des.ContainsKey(obj.GetType()))
            {
                des[obj.GetType()](fieldname,obj, Serializer);
                return;
            }


            if (!isrray)
            {
                if (obj.GetType().IsArray)
                {
                    Array array = obj as Array;
                    String baseType = array.GetType().AssemblyQualifiedName.Replace("]", "").Replace("[", "");
                    Serializer.BeginType(fieldname, obj.GetType().AssemblyQualifiedName, new KeyValuePair<String, String>("Count", array.Length.ToString()), new KeyValuePair<String, String>("BaseType", baseType));
                }
                else
                {
                    Serializer.BeginType(fieldname, obj.GetType().AssemblyQualifiedName);
                }
            }
            else if(!obj.GetType().IsPrimitive && obj.GetType().IsEnum == false)
            {
                Serializer.BeginType(fieldname, obj.GetType().AssemblyQualifiedName);
            }

            if (obj.GetType().GetInterface("ICustomSerializable") != null)
            {
                (obj as ICustomSerializable).Serialize(SerializerProxy);
            }
            else
            {

                if (obj.GetType().IsArray)
                {
                    Array array = obj as Array;
                    foreach (var arrElement in array)
                    {
                        SerializeType(arrElement.GetType().Name, arrElement, Serializer, true);
                    }
                }
                else if (obj.GetType().IsClass || (obj.GetType().IsValueType && !obj.GetType().IsPrimitive && !obj.GetType().IsEnum))
                {
                    bool checkall = obj.GetType().GetCustomAttributes(typeof(PloobsSerializeAll), true).Count() != 0;

                    foreach (var item in obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                    {
                        if (checkall || item.GetCustomAttributes(typeof(PloobsSerialize), true).Count() != 0 || item.IsStatic == true)
                        {
                            if (!item.FieldType.IsPrimitive && item.FieldType != typeof(String)
                                && item.FieldType != typeof(Char)
                                )
                            {
                                object par = item.GetValue(obj);
                                if (par != null)
                                {
                                    SerializeType(item.Name, par, Serializer);
                                }
                            }
                            else
                            {
                                Object o = item.GetValue(obj);
                                Serializer.Serialize(item.Name, o);
                            }
                        }
                    }

                    foreach (var item in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                    {
                        if (checkall || item.GetCustomAttributes(typeof(PloobsSerialize), true).Count() != 0)
                        {
                            Object o = item.GetValue(obj, null);
                            Serializer.Serialize(item.Name, o);
                        }

                    }
                }
                else if (obj.GetType().IsEnum)
                {
                    Object o = obj.GetType().GetEnumName(obj);
                    Serializer.Serialize(fieldname, o);
                }
                else if (obj.GetType().IsPrimitive)
                {
                    Serializer.Serialize(fieldname, obj);
                }
            }

            if (!isrray)
                Serializer.EndType(fieldname);
            else if (!obj.GetType().IsPrimitive && obj.GetType().IsEnum == false)
            {
                Serializer.EndType(fieldname);
            }
            
        }
        private string GetStringNoAccents(string str)
        {


            /** Troca os caracteres acentuados por não acentuados **/
            string[] acentos = new string[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û" };
            string[] semAcento = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };


            for (int i = 0; i < acentos.Length; i++)
            {
                str = str.Replace(acentos[i], semAcento[i]);
            }


            /** Troca os caracteres especiais da string por "" **/
            string[] caracteresEspeciais = { "\\.", ",","`", "-", ":", "\\(", "\\)", "ª", "\\|", "\\\\", "°" };


            for (int i = 0; i < caracteresEspeciais.Length; i++)
            {
                str = str.Replace(caracteresEspeciais[i], "");
            }


            /** Troca os espaços no início por "" **/
            str = str.Replace("^\\s+", "");
            /** Troca os espaços no início por "" **/
            str = str.Replace("\\s+$", "");
            /** Troca os espaços duplicados, tabulações e etc por  " " **/
            str = str.Replace("\\s+", " ");


            return str;


        }


    }

}
