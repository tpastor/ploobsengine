using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Xml;

namespace PloobsSerializator
{
    public class XmlSerializer 
    {

        XmlDocument doc;
        Stack<XmlElement> cur = new Stack<XmlElement>();

        public void Serialize(String name,object obj)
        {            
            XmlElement el = doc.CreateElement(name);            
            el.SetAttribute("Base",obj.GetType().Name);
            XmlText categoryText = doc.CreateTextNode(obj.ToString());
            el.AppendChild(categoryText);
            if(cur.Count == 0)
            {
                doc.AppendChild(el);
            }
            else
            {
                cur.Peek().AppendChild(el);                             
            }
        }

        
        public void BeginType(string fieldName, string typeName,params KeyValuePair<String,String>[] attributes)
        {
            XmlElement el;
            if (cur.Count != 0)
            {
                XmlElement father = cur.Peek();
                el = (XmlElement)father.AppendChild(doc.CreateElement(fieldName));
                
            }
            else
            {
                el = (XmlElement)doc.AppendChild(doc.CreateElement(fieldName));
            }            
            el.SetAttribute("Type", typeName);
            foreach (var item in attributes)
            {
                el.SetAttribute(item.Key, item.Value);
            }
            cur.Push(el);
        }

        public void EndType(string fieldName)
        {
            cur.Pop();
        }

        public void Start(Encoding encoding)
        {
            doc = new XmlDocument();            
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", encoding.EncodingName, null);
            doc.InsertBefore(xmlDeclaration, doc.DocumentElement);            
        }

        
        public void End(StreamWriter stream)
        {
            doc.Save(stream);
        }

     
    }
}
