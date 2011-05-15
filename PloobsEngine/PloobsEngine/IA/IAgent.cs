using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.SceneControl;

namespace PloobsEngine.IA
{
    public interface IAgent  
    {        
        void Update(GameTime gt);
        IObject Obj
        {
            get;
            set;

        }
    }
}
