using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PloobsEngine.Utils
{
    /// <summary>
    /// Handles Bundle like files
    /// </summary>
    public class BundleHandler
    {
        /// <summary>
        /// Parse files info of the type a=b and t=return a dicionary with keys and values
        /// % is a comentary
        /// </summary>
        /// <param name="filename">file name</param>
        /// <returns>dic with the parse info</returns>
        public static IDictionary<String, String> getBundle(string filename)
        {            
            IDictionary<String,String> dic = new Dictionary<String,String>();
            StreamReader sr =  File.OpenText(filename);
            String line = sr.ReadLine();
            while ( line != null  )
            {
                ///% is a comentary
                if (!line.StartsWith("%") && line != "")
                {
                string word1 = line.Substring(0,line.IndexOf('='));
                string word2 = line.Substring(line.IndexOf('=') + 1);
                dic.Add(word1, word2);
                }
                line = sr.ReadLine();
                
            }
            sr.Close();
            return dic;
            
        }
    }

}
