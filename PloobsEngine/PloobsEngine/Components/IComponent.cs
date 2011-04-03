using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Commands;
using PloobsEngine.Entity;
using PloobsEngine.Engine;

namespace PloobsEngine.Components
{
    /// <summary>
    /// Component Specification
    /// Components can Recieve Commands and send messages
    /// Designed to be VERY low coupled
    /// </summary>
    public abstract class IComponent : IReciever , IEntity 
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        protected virtual void Initialize() { }
        internal void iInitialize()
        {
            Initialize();
        }
        /// <summary>
        /// Update
        /// Its called deppending of the ComponentType
        /// </summary>
        /// <param name="gt">The gt.</param>
        protected virtual void Update(GameTime gt) { }
        internal void iUpdate(GameTime gt)
        {
            Update(gt);
        }
        /// <summary>
        /// Pres draw.
        /// Its called deppending of the ComponentType
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="activeView">The active view.</param>
        /// <param name="activeProjection">The active projection.</param>
        protected virtual void PreDraw(GameTime gt, Matrix activeView, Matrix activeProjection) { }
        internal void iPreDraw(GameTime gt, Matrix activeView, Matrix activeProjection)
        {
            iPreDraw(gt, activeView, activeProjection);
        }

        /// <summary>
        /// Afters draw.
        /// Its called deppending of the ComponentType
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="activeView">The active view.</param>
        /// <param name="activeProjection">The active projection.</param>
        protected virtual void AfterDraw(GameTime gt, Matrix activeView, Matrix activeProjection) { }
        internal void iAfterDraw(GameTime gt, Matrix activeView, Matrix activeProjection)
        {
            AfterDraw(gt, activeView, activeProjection);
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="engine">The engine.</param>
        protected virtual void LoadContent(ref GraphicInfo GraphicInfo) { }
        internal void iLoadContent(ref GraphicInfo GraphicInfo)
        {
            LoadContent(ref GraphicInfo);
        }

        /// <summary>
        /// Gets the type of the component type.
        /// </summary>
        /// <value>
        /// The type of the component.
        /// </value>
        public abstract ComponentType ComponentType
        {
            get;
        }

        #region IEntity Members
        private int id;
        /// <summary>
        /// return the entity id
        /// </summary>
        /// <returns>
        /// the id
        /// </returns>
        public int GetId()
        {
            return id;
        }

        /// <summary>
        /// sets the id
        /// </summary>
        /// <param name="id"></param>
        public void SetId(int id)
        {
            this.id = id;
        }

        #endregion   

    
        #region IReciever Members

        public abstract string getMyName();        

        #endregion
    }

    /// <summary>
    /// The component Type
    /// Its very important, The type decides what methods of the component will be called
    /// </summary>
    public enum ComponentType
    {
        /// <summary>
        /// Only the Pre Draw method will be call
        /// </summary>
        PRE_DRAWABLE,
        /// <summary>
        /// Only the Pos draw method will be called
        /// </summary>
        POS_DRAWABLE,
        /// <summary>
        /// Only the Update method will be called
        /// </summary>
        UPDATEABLE,
        /// <summary>
        /// Only the Pre Draw and Update will be called
        /// </summary>
        PRE_DRAWABLE_AND_UPDATEABLE,
        /// <summary>
        /// Only the Pos Draw and Update will be called
        /// </summary>
        POS_DRAWABLE_AND_UPDATEABLE,
        /// <summary>
        /// None of the methods will be called
        /// </summary>
        NONE
    }
}
