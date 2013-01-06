#if !WINRT
#region License
/*
    PloobsEngine Game Engine Version 0.3 Beta
    Copyright (C) 2011  Ploobs

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion
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
                //% is a comentary
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
#endif