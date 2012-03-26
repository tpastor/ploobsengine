using System;
using PloobsEngine.TestSuite;

namespace AdvancedDemo4._0
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                TestProcessor TestProcessor = new TestProcessor();
                TestProcessor.EvaluateTestes("AdvancedDemos.exe");
            }
            else
            {
                Demos game = new Demos();
            }
            
        }
    }
}

