using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShapeConnectors;

namespace EntitySystemGUI
{
    interface IComponentImporter
    {
        Component ImportfromFile(string filename);
    }
}
