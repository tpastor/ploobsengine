#if WINDOWS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Engine;

namespace PloobsEngine.TestSuite
{
    public interface IAlgoTest
    {
        EngineStuff EngineStuff
        {
            set;
            get;
        }
    }
}
#endif