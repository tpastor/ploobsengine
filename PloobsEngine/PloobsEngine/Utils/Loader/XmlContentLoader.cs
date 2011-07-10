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
using System.Xml.Serialization;

namespace PloobsEngine.Utils
{
    /// <summary>
    /// XML content loader
    /// </summary>
    public class XmlContentLoader
    {
        /// <summary>
        /// Saves the content of the obj in XML.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="tipo">The type.</param>
        /// <param name="Path">The path.</param>
        public static void SaveXmlContent(Object obj, Type tipo, String Path)
        {
                FileStream saveFile = File.Open(Path, FileMode.Create );
                XmlSerializer xmlSerializer = new XmlSerializer(tipo);
                xmlSerializer.Serialize(saveFile, obj);
                saveFile.Flush();
                saveFile.Close();

        }


        /// <summary>
        /// Loads the content of the XML.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="tipo">The type.</param>
        /// <returns>object</returns>
        public static Object LoadXmlContent(String name, Type tipo)
        {
            if (File.Exists(name))
            {

                FileStream saveFile = File.Open(name, FileMode.Open);                
                XmlSerializer xmlSerializer = new XmlSerializer(tipo);
                Object obj = xmlSerializer.Deserialize(saveFile);
                saveFile.Close();
                return obj;
            }
            else
            {
                throw new Exception("Arquivo nao encontrado");
            }
        }

    }
}
