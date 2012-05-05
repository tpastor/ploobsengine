using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

///Credits to Mathias Gehlhaar
namespace PloobsEngine.Utils
{
    public abstract class Writer
    {
        public abstract void Write(char separator = '=', char endOfLine = ';');
        public abstract void AddKeyValue(string key, string value);
    }
}
