using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntitySystemGUI
{

    public class ComponentProperty
    {

         //To modify the visualizer properties
        public DinamicComponentPresenter presenter { get; set; }

        public ComponentProperty(DinamicComponentPresenter pres)
        {
            this.presenter = pres;
        
        }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    
    }

  
    public class EntityProperty
    {
       
        public EntityProperty()
        {
          
        
        }

        public string Name { get; set; }
        
        public List<ComponentProperty> Components { get; set; }

        

    }
}
