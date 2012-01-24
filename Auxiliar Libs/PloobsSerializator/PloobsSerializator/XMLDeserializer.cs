using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;

namespace PloobsSerializator
{
    public class XMLDeserializer
    {
        Dictionary<Type, Func<XElement, object>> des ; 
        public XMLDeserializer(DeSerializerProxy  DeSerializerProxy ,Dictionary<Type, Func<XElement, object>> des)
        {
            this.des = des;
            this.DeSerializerProxy = DeSerializerProxy;

        }
        DeSerializerProxy DeSerializerProxy;
        #region IDeSerializer Members

        public object Load(string path)
        {           
            XDocument xdoc = XDocument.Load(path);
            XElement element = xdoc.Root;
            return populateobj( element);
        }

        public object populateobj(XElement element)
        {
            XAttribute att = element.Attribute("Type");
            Type type;
            if (att == null)
            {
                XAttribute basetype = element.Attribute("Base");
                type = Type.GetType("System." + basetype.Value);
            }
            else
            {
                type = Type.GetType(att.Value);
            }

            if (type.GetInterface("IList") != null && type.IsArray == false)
            {
                var matches = from el in element.Elements()
                              where el.Name == "List"
                              select el;

                System.Collections.IList l = GetInstace(type) as System.Collections.IList;
                foreach (var item in matches)
                {
                    l.Add(populateobj(item));                   
                }
                return l;                
            }

            if (type.GetInterface("IDictionary") != null && type.IsArray == false)
            {
                var matches = from el in element.Elements()
                              where el.Name == "KeyValuePar"
                              select el;

                System.Collections.IDictionary l = GetInstace(type) as System.Collections.IDictionary;
                foreach (var item in matches)
                {
                    var key = (from el in item.Elements()
                                  where el.Name == "Key"
                                  select el).First();
                    object keyobj = populateobj(key);

                    var value = (from el in item.Elements()
                               where el.Name == "Value"
                               select el).First();
                    object valueobj = populateobj(value);

                    l.Add(keyobj, valueobj);
                }
                return l;                
            }

            if (des.ContainsKey(type))
            {
                return des[type](element);
            }


            Type baseType = null;

            object root = null;
            if (type.IsArray)
            {                
                XAttribute count = element.Attribute("Count");
                XAttribute basetype = element.Attribute("BaseType");
                baseType = Type.GetType(basetype.Value);                
                root=  Array.CreateInstance(baseType , int.Parse(count.Value));                             
            }
            else
            {
                root = GetInstace(type);
            }

            if (root.GetType().GetInterface("ICustomSerializable") != null)
            {
                DeSerializerProxy.current = element;
                return (root as ICustomSerializable).Deserialize(DeSerializerProxy);
            }


            int arrayiterator = 0;
            foreach (var item in element.Nodes())
            {
                XElement elem = item as XElement;
                XAttribute baseatt = elem.Attribute("Base");
                XAttribute typeatt = elem.Attribute("Type");
                if (baseatt == null)
                {                    
                    Type sontype = Type.GetType(typeatt.Value);
                    if (sontype.IsEnum)
                    {
                        object eobj = Enum.Parse(sontype, elem.Value);
                        type.GetField(elem.Name.LocalName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).SetValue(root, eobj);
                    }
                    else
                    {
                        object o = populateobj(elem);
                        if (type.IsArray)
                        {
                            Array ar =  root as Array;
                            ar.SetValue(o, arrayiterator++);                                                                                
                        }
                        else
                        {
                            type.GetField(elem.Name.LocalName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).SetValue(root, o);
                        }
                    }
                }
                else // base type
                {
                    object o = null;
                    if (baseType  != null && baseType.IsEnum)
                    {
                        o = Enum.Parse( baseType ,elem.Value);                        
                    }
                    else
                    {
                        o = Convert.ChangeType(elem.Value, Type.GetType("System." + baseatt.Value));
                    }
                    
                    if (type.IsArray)
                    {
                        Array ar =  root as Array;
                        ar.SetValue(o, arrayiterator++);                        
                    }
                    else
                    {
                        FieldInfo fi = type.GetField(elem.Name.LocalName, BindingFlags.Default | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                        if (fi != null)
                            fi.SetValue(root, o );
                        else
                        {
                            PropertyInfo pi = type.GetProperty(elem.Name.LocalName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                            if (pi != null)
                                pi.SetValue(root, o, null);
                            else if (type.IsPrimitive)
                            {
                                return o;
                            }
                            else if (type.IsEnum)
                            {
                                return Enum.Parse(type, elem.Value); ;
                            }
                            else
                            {
                                throw new InvalidOperationException();
                            }
                        }
                    }
                }
            }

            if (root.GetType().GetInterface("ICustomDeserializable") != null)
            {
                DeSerializerProxy.current = element;
                (root as ICustomDeserializable).Deserialize(DeSerializerProxy);
            }            
            
            
            return root;

        }

        
        private object Getunitialized(Type type)
        {            
            return System.Runtime.Serialization.FormatterServices.GetSafeUninitializedObject(type);
            
        }

        private object GetInstace(Type type)
        {
            if (type.GetConstructor(Type.EmptyTypes) != null || (type.IsValueType && !type.IsPrimitive && !type.IsEnum))
            {
                return Activator.CreateInstance(type);
            }            
            else
            {
                return Getunitialized(type);
            }
        }

        #endregion

    }
}
