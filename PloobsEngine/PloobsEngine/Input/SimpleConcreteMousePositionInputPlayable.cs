using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.Input
{
    public class SimpleConcreteMouseBottomInputPlayable : InputPlaybleMouseBottom
    {
        private InputMask mask;

        public SimpleConcreteMouseBottomInputPlayable(StateKey sk, EntityType et, MouseButtons mb, MouseStateChangeComplete mst = null, InputMask mask = Input.InputMask.GSYSTEM)
        {
            this.sk = sk;
            this.mb = mb;
            this.KeyStateChange += mst;
            this.et = et;
            this.mask = mask;
        }
        public  override InputMask InputMask
        {
            get { return mask; }
        }


        #region InputPlaybleMouseBottom Members

        private StateKey sk;
        private EntityType et;
        private MouseButtons mb;        

        public override StateKey StateKey
        {
            get { return sk; }
        }

        public override MouseButtons MouseButtons
        {
            get { return mb; }
        }

        public override EntityType EntityType
        {
            get { return et; }
        }

        
        #endregion
    }
}
