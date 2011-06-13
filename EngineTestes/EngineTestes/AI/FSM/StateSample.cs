using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.IA;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework;

namespace EngineTestes.AI.FSM
{
    public delegate void Hanldler<P>(P p);
    public delegate void HanldlerUp<P,Q>(P p,Q q);

    class StateSample : IState
    {
        public StateSample(String Name,IObject Object)
        {
            this.Object = Object;
            this.name = Name;
        }

        IObject Object;
        #region IState Members

        public Func<IObject, string> NextStateFunc;
        public Hanldler<IObject> InitFunc;
        public Hanldler<IObject> FinishFunc;
        public HanldlerUp<GameTime, IObject> UpdateFunc;

        public void Init()
        {
            if (InitFunc != null)
                InitFunc(Object);
        }

        public string NextState()
        {
            if (NextStateFunc != null)
                return NextStateFunc(Object);
            return null;
        }

        public void UpdateState(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (UpdateFunc != null)
                UpdateFunc(gameTime, Object);
        }

        public void Finish()
        {
            if (FinishFunc != null)
                FinishFunc(Object);
        }

        #endregion

        #region IState Members

        String name;
        public string Name
        {
            get { return name; }
        }

        #endregion
    }
}
