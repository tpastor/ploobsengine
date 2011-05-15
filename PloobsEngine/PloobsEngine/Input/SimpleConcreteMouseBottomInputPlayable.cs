using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.Input
{
    /// <summary>
    /// InputPlaybleMousePosition Implementation for mouse position
    /// </summary>
    public class SimpleConcreteMousePositionInputPlayable : InputPlaybleMousePosition
    {
        private InputMask mask;


        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleConcreteMousePositionInputPlayable"/> class.
        /// </summary>
        /// <param name="mst">The MST.</param>
        /// <param name="mask">The mask.</param>
        /// <param name="et">The et.</param>
        public SimpleConcreteMousePositionInputPlayable(MouseStateChangeComplete mst = null, InputMask mask = InputMask.GSYSTEM, EntityType et = Input.EntityType.TOOLS)
        {
            this.mst = mst;
            this.et = et;
            this.mask = mask;
        }

        #region InputPlaybleMouseBottom Members

        
        private EntityType et;
        private MouseStateChangeComplete mst;

        public override EntityType EntityType
        {
            get { return et; }
        }


        public override InputMask InputMask
        {
            get { return mask; }
        }

        #endregion
    }
}
