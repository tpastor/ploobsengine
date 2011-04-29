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
