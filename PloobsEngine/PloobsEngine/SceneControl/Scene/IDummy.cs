using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Specification of a Dummy
    /// Dummy is everything that can be put in a world and recovered later
    /// Its NOT USED by the engine for nothing (the engine just keep it there)
    /// Usefull to pass information from an editor to the game, cause dummies
    /// are serializable
    /// </summary>
    #if !WINDOWS_PHONE
    public interface IDummy : ISerializable
#else
    public interface IDummy 
#endif
    {
        String Name { get; set; }        
    }
}
