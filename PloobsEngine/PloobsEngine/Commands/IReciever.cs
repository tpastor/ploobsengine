using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.Commands
{
    /// <summary>
    /// Specification for everyone that recieves ICommands    
    /// </summary>
    public  interface IReciever
    {
        /// <summary>
        /// The name of the reciever
        /// MUST BE UNIQUE
        /// </summary>
        /// <returns></returns>
        string getMyName();
        
    }
}
