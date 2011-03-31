using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.Input
{
    public class SimpleConcreteMousePositionInputPlayable : InputPlaybleMousePosition
    {
        private InputMask mask; 

        public SimpleConcreteMousePositionInputPlayable(EntityType et, MouseStateChangeComplete mst)
        {                        
            this.mst = mst;
            this.et = et;
            this.mask = InputMask.GSYSTEM;
        }

        public SimpleConcreteMousePositionInputPlayable(EntityType et, MouseStateChangeComplete mst,InputMask mask)
        {
            this.mst = mst;
            this.et = et;
            this.mask = mask;
        }

        #region InputPlaybleMouseBottom Members

        
        private EntityType et;
        private MouseStateChangeComplete mst;

        
        #endregion

        #region InputPlaybleMousePosition Members

        EntityType InputPlaybleMousePosition.EntityType
        {
            get { return et; }
        }

        MouseStateChangeComplete InputPlaybleMousePosition.KeyStateChange
        {
            get { return mst; }
        }

        #endregion

        #region InputPlaybleMousePosition Members


        public InputMask InputMask
        {
            get { return mask; }
        }

        #endregion
    }
}
