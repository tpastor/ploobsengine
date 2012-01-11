using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace EntitySystemGUI.Importers
{
    public class CSharpComponentImporter : IComponentImporter
    {
        #region IComponentImporter Members

        public ShapeConnectors.Component ImportfromFile(string filename)
        {
            ShapeConnectors.Component ret = new ShapeConnectors.Component();


            TextReader reader = new StreamReader(filename);

            string input;
            bool stepclass=false;
            bool isacomponent = false;


            while ((input = reader.ReadLine()) != null)
            {
                if (!stepclass &&  input.Contains("class"))
                {
                    stepclass = true;

                    if (input.Contains("Component"))
                    {
                        isacomponent = true;
                    }

                    ret.Name = input.Replace("class", "").Replace("Component","").Replace(" ","").Replace(":","").Replace("public","");
                    
                }
                if (stepclass && isacomponent)
                {
                    
                    if (input.Contains("private") && !input.Contains("(") )
                    {
                        string prop = input.Replace("private", "").Replace(";","");
                        string[] v = prop.Split(' ');
                        ret.Items.Add(new ShapeConnectors.ComponentItem(v[v.Length-1], v[v.Length-2]));
                        
                    }

                }


            }


           
            reader.Close();

            return ret;
        }

        #endregion
    }
}
