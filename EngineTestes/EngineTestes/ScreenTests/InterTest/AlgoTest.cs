using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Engine;
using PloobsEngine.TestSuite;

namespace EngineTestes.ScreenTests
{
    [TesteAlgorithmClass]
    public class AlgoTest : IAlgoTest
    {
        public EngineStuff EngineStuff
        {
            set;
            get;
        }

        Random r = new Random();

        [TesteAlgorithmMethod]
        public void teste()
        {
            for (int i = 0; i < 1000; i++)
            {
                double x = Math.Pow(Math.Cos(Math.Cosh(r.Next())),r.Next());                
            }
        }
    }
}
