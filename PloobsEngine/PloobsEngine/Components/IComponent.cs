#region License
/*
    PloobsEngine Game Engine Version 0.3 Beta
    Copyright (C) 2011  Ploobs

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Commands;
using PloobsEngine.Entity;
using PloobsEngine.Engine;
using PloobsEngine.SceneControl;

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
        /// <param name="render">The render.</param>
        /// <param name="gt">The gt.</param>
        /// <param name="activeView">The active view.</param>
        /// <param name="activeProjection">The active projection.</param>
        protected virtual void PreDraw(RenderHelper render,GameTime gt, Matrix activeView, Matrix activeProjection) { }
        internal void iPreDraw(RenderHelper render,GameTime gt, Matrix activeView, Matrix activeProjection)
        {
            PreDraw(render, gt, activeView, activeProjection);
        }


        /// <summary>
        /// Pos With Depth draw.
        /// Its called deppending of the ComponentType
        /// </summary>
        /// <param name="render">The render.</param>
        /// <param name="gt">The gt.</param>
        /// <param name="activeView">The active view.</param>
        /// <param name="activeProjection">The active projection.</param>
        protected virtual void PosWithDepthDraw(RenderHelper render, GameTime gt, Matrix activeView, Matrix activeProjection) { }
        /// <summary>
        /// Is the pos with depth draw.
        /// </summary>
        /// <param name="render">The render.</param>
        /// <param name="gt">The gt.</param>
        /// <param name="activeView">The active view.</param>
        /// <param name="activeProjection">The active projection.</param>
        internal void iPosWithDepthDraw(RenderHelper render, GameTime gt, Matrix activeView, Matrix activeProjection)
        {
            PosWithDepthDraw(render, gt, activeView, activeProjection);
        }

        /// <summary>
        /// Afters draw.
        /// Its called deppending of the ComponentType
        /// </summary>
        /// <param name="render">The render.</param>
        /// <param name="gt">The gt.</param>
        /// <param name="activeView">The active view.</param>
        /// <param name="activeProjection">The active projection.</param>
        protected virtual void AfterDraw(RenderHelper render,GameTime gt, Matrix activeView, Matrix activeProjection) { }
        internal void iAfterDraw(RenderHelper render,GameTime gt, Matrix activeView, Matrix activeProjection)
        {
            AfterDraw(render,gt, activeView, activeProjection);
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="GraphicInfo">The graphic info.</param>
        /// <param name="factory">The factory.</param>
        protected virtual void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory) { }
        internal void iLoadContent(GraphicInfo GraphicInfo,GraphicFactory factory)
        {
            LoadContent(GraphicInfo, factory);
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
        private long id;
        /// <summary>
        /// return the entity id
        /// </summary>
        /// <returns>
        /// the id
        /// </returns>
        public long GetId()
        {
            return id;
        }

        /// <summary>
        /// sets the id
        /// </summary>
        /// <param name="id"></param>
        public void SetId(long id)
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
        /// Only the Pos With Depth Draw and Update will be called
        /// </summary>
        POS_WITHDEPTH_DRAWABLE_AND_UPDATEABLE,
        /// <summary>
        /// Only the Pos With Depth Draw will be called
        /// </summary>
        POS_WITHDEPTH_DRAWABLE,
        /// <summary>
        /// None of the methods will be called
        /// </summary>
        NONE
    }
}
