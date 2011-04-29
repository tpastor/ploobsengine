using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.Input
{
    public class SimpleConcreteMouseBottomInputPlayable : InputPlaybleMouseBottom
    {
        private InputMask mask;

        public SimpleConcreteMouseBottomInputPlayable(StateKey sk , EntityType et, MouseButtons mb , MouseStateChangeComplete mst)
        {
            this.sk = sk;
            this.mb = mb;
            this.mst = mst;
            this.et = et;
            this.mask = InputMask.GSYSTEM;
        }
        public SimpleConcreteMouseBottomInputPlayable(StateKey sk, EntityType et, MouseButtons mb, MouseStateChangeComplete mst, InputMask mask)
        {
            this.sk = sk;
            this.mb = mb;
            this.mst = mst;
            this.et = et;
            this.mask = mask;
        }
        public InputMask InputMask
        {
            get { return mask; }
        }


        #region InputPlaybleMouseBottom Members

        private StateKey sk;
        private EntityType et;
        private MouseButtons mb;
        private MouseStateChangeComplete mst;

        public StateKey StateKey
        {
            get { return sk; }
        }

        public MouseButtons MouseButtons
        {
            get { return mb; }
        }

        public EntityType EntityType
        {
            get { return et; }
        }

        public MouseStateChangeComplete KeyStateChange
        {
            get {

                return mst;
            }
        }

        #endregion
    }
}
