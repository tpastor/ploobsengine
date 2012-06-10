using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

//Credits to Mathias Gehlhaar
namespace PloobsEngine.Utils
{
    public abstract class Reader
    {
        /// <summary>
        /// Liest das gesamte Dokument
        /// </summary>
        public abstract void Read(char separator = '=', char endOfLine = ';', char comment = '%');

        /// <summary>
        /// Liest den Wert eines Keys aus dem Dokument heraus.
        /// </summary>
        /// <param name="key">der Schlüssel: "resolution = 'Wert'"</param>
        public abstract String ReadValue(string key);

    }
}
