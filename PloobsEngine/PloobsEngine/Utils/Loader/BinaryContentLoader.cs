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
#if !WINDOWS_PHONE	
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace PloobsEngine.Utils
{
    /// <summary>
    /// Binary Content Loader
    /// </summary>
    public class BinaryContentLoader
    {

        /// <summary>
        /// Saves the content of the object in a binary format.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="tipo">The type.</param>
        /// <param name="Path">The path.</param>
        public static void SaveBinaryContent(Object obj, Type tipo, String Path)
        {
            //FileStream saveFile = File.Open(Path, FileMode.Create);
            Stream stream = new FileStream(Path, System.IO.FileMode.Create);
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
            stream.Close();

        }

        /// <summary>
        /// Loads the content of the bynary file.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="tipo">The type.</param>
        /// <returns></returns>
        public static Object LoadBynaryContent(String name, Type tipo)
        {
            if (File.Exists(name))
            {
                Stream stream = new FileStream(name, System.IO.FileMode.Open);
                IFormatter formatter = new BinaryFormatter();
                Object itemsDeserialized = formatter.Deserialize(stream);
                stream.Close();
                return itemsDeserialized;
            }
            else
            {
                throw new Exception("Arquivo nao encontrado");
            }
            
        }

    }
}
#endif