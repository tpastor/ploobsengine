using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using PloobsEngine.Events;
using Microsoft.Xna.Framework;

namespace PloobsEngine.IA
{
    public interface IState
    {
        void Init();
        String NextState();
        void UpdateState(GameTime gameTime);
        void Finish();
        String Name
        {
            get;
        }
    }
}
