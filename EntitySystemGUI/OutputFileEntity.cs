using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace EntitySystemGUI
{
    public class OutputFileEntity
    {

        String filename;       

        String entityname;

        public String EntityName
        {
            get { return entityname; }
            set { entityname = value; }
        }

        List<DinamicComponentPresenter> components = new List<DinamicComponentPresenter>();

        public List<DinamicComponentPresenter> Components
        {
            get { return components; }
            set { components = value; }
        }


        public OutputFileEntity(String name)
        {
            entityname = name;
        
        }
        public DinamicComponentPresenter getDinamicComponentbyName(String name)
        {
            return components.Find(p => p.Name == name);
        
        }

        public void CreateXml(string filename)
        {

            

            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode rootNode = doc.CreateElement("Entity");


            XmlAttribute entityAttribute = doc.CreateAttribute("Name");
            entityAttribute.Value = entityname;
            rootNode.Attributes.Append(entityAttribute);


            doc.AppendChild(rootNode);

            

            XmlNode products = doc.CreateElement("Components");
            rootNode.AppendChild(products);

            foreach (var Comps in components)
            {
                XmlNode productsNode = doc.CreateElement("Component");
                XmlAttribute productAttribute = doc.CreateAttribute("Name");
                productAttribute.Value = Comps.Name;
                productsNode.Attributes.Append(productAttribute);
                products.AppendChild(productsNode);



                foreach (var item in Comps.val)
                {
                    XmlNode nameNode = doc.CreateElement(item.Key);
                    nameNode.AppendChild(doc.CreateTextNode(item.Value));
                    productsNode.AppendChild(nameNode);
                }

            }



            doc.Save(filename);

        }


    }
}
