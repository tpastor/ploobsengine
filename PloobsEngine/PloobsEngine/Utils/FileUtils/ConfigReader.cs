#if !WINRT
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PloobsEngine.Engine.Logger;

///Credits to Mathias Gehlhaar
namespace PloobsEngine.Utils
{
    public class ConfigReader : Reader
    {
        StreamReader reader;        
        string path;
        Dictionary<string,string> dic;

        public ConfigReader(string path)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new Exception("invalid Path");
            }

            this.path = path;            
            dic = new Dictionary<string, string>();
        }

        public override void Read(char separator = '=', char endOfLine = ';', char comment = '%')
        {
            reader = new StreamReader(path);
            String readerContent = reader.ReadToEnd();
            foreach (var item in readerContent.Split(endOfLine))
            {
                String line = item.Replace("\n","").Replace("\r","");
                if (line.StartsWith(new String(new char[] {comment})) || String.IsNullOrEmpty(line))
                    continue;
                string word1 = line.Substring(0, line.IndexOf(separator)).TrimEnd().TrimStart();
                string word2 = line.Substring(line.IndexOf(separator) + 1).TrimEnd().TrimStart();
                dic.Add(word1, word2);
            }           
            
            reader.Close();
        }

        public override string ReadValue(string key)
        {            
            return dic[key];
        }

    }
}

#endif