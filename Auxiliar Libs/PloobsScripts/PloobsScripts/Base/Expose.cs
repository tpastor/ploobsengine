using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsScripts
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class Expose : Attribute
    {
        public Expose(string positionalString)
        {
        }
    }
}
