using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Commands;
using PloobsEngine.Entity;

namespace PloobsEngine.Components
{
    /// <summary>
    /// Component Specification
    /// Components can Recieve Commands and send messages
    /// Designed to be VERY low coupled
    /// </summary>
    public interface IComponent : IReciever , IEntity 
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        void Initialize();
        /// <summary>
        /// Update
        /// Its called deppending of the ComponentType
        /// </summary>
        /// <param name="gt">The gt.</param>
        void Update(GameTime gt);
        /// <summary>
        /// Pres draw.
        /// Its called deppending of the ComponentType
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="activeView">The active view.</param>
        /// <param name="activeProjection">The active projection.</param>
        void PreDraw(GameTime gt, Matrix activeView, Matrix activeProjection);
        /// <summary>
        /// Afters draw.
        /// Its called deppending of the ComponentType
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="activeView">The active view.</param>
        /// <param name="activeProjection">The active projection.</param>
        void AfterDraw(GameTime gt, Matrix activeView, Matrix activeProjection);
        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="engine">The engine.</param>
        void LoadContent();

        /// <summary>
        /// Gets the type of the component type.
        /// </summary>
        /// <value>
        /// The type of the component.
        /// </value>
        ComponentType ComponentType
        {
            get;
        }

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
