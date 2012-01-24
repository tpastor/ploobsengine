using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ShapeConnectors
{
    //Representa a classe entidade com as características
    [Serializable]
    public class ComponentItem
    {
         public String name;
         public String type;

         public ComponentItem(String name, String type)
         {
             this.name = name;
             this.type = type;
         
         }


    }

    public class Tag
    {
        public String Name;
    }


    public class Component
    {
        public String Name;
        
        //public ComponentItem[] Items;
        public List<ComponentItem> Items = new List<ComponentItem>();

        public Component()
        { }
        
       
    }
    
    
}
