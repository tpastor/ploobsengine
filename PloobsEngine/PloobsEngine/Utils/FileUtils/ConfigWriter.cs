using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

///Credits to Mathias Gehlhaar
namespace PloobsEngine.Utils
{
    public class ConfigWriter : Writer
    {
        StreamWriter writer;
        string path;
        List<KeyValuePair<string, string>> keyvalues = new List<KeyValuePair<string, string>>();

        public ConfigWriter(string path)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new Exception("invalid Path");
            }
            this.path = path;                        
        }

        public override void Write(char separator = '=', char endOfLine = ';')
        {
            writer = new StreamWriter(path);
            foreach (var s in keyvalues)
            {                
               writer.WriteLine(s.Key + separator + s.Value + endOfLine);                
                
            }
            writer.Close();
        }

        

        public override void AddKeyValue(string key, string value)
        {
            keyvalues.Add(new KeyValuePair<String,String>(key, value));
        }
    }
}
