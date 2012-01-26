using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using EntitySystemGUI;

namespace ShapeConnectors
{
    [Serializable]
    public class EntityContainer
    {
        private String entity;


        

        public String Entity
        {
            get { return entity; }
            set { entity = value; }
        }
        
        private List<Component> components = new List<Component>();

        public List<Component> Components
        {
            get { return components; }
            set { components = value; }
        }
       
    }
}
