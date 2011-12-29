using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Components
{
    /// <summary>
    /// Basic Component implementation
    /// Users should extend this class when making components instead of IComponent
    /// </summary>
    public abstract class BasicComponent : IComponent
    {
        #region IComponent Members

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public virtual void Initialize()
        {
            
        }

        /// <summary>
        /// Update
        /// Its called deppending of the ComponentType
        /// </summary>
        /// <param name="gt">The gt.</param>
        public virtual void Update(Microsoft.Xna.Framework.GameTime gt)
        {
        }

        /// <summary>
        /// Pres draw.
        /// Its called deppending of the ComponentType
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="activeView">The active view.</param>
        /// <param name="activeProjection">The active projection.</param>
        public virtual void PreDraw(Microsoft.Xna.Framework.GameTime gt, Matrix activeView, Matrix activeProjection)
        {            
        }

        /// <summary>
        /// Afters draw.
        /// Its called deppending of the ComponentType
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="activeView">The active view.</param>
        /// <param name="activeProjection">The active projection.</param>
        public virtual void AfterDraw(Microsoft.Xna.Framework.GameTime gt, Matrix activeView, Matrix activeProjection)
        {
        }

        
        public virtual void LoadContent()
        {
        }

        #endregion

        #region IReciever Members


        /// <summary>
        /// The name of the reciever
        /// MUST BE UNIQUE
        /// </summary>
        /// <returns></returns>
        public abstract string getMyName();

        #endregion

        #region IEntity Members
        private int id;
        /// <summary>
        /// return the entity id
        /// </summary>
        /// <returns>
        /// the id
        /// </returns>
        public int getId()
        {
            return id;
        }

        /// <summary>
        /// sets the id
        /// </summary>
        /// <param name="id"></param>
        public void setId(int id)
        {
            this.id = id;
        }

        #endregion   


        #region IComponent Members


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

        #endregion

    }
        
}
