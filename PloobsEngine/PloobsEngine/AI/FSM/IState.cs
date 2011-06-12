using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using PloobsEngine.Events;

namespace PloobsEngine.IA
{
    public interface IState
    {
        void Init(IObject obj );
        String NextState();
        void UpdateState();
        void Finish();                
    }
}
