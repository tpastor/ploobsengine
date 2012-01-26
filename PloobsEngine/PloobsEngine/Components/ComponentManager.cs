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
using System.Reflection;
using PloobsEngine.Commands;
using PloobsEngine.Engine;
using PloobsEngine.SceneControl;

namespace PloobsEngine.Components
{
    /// <summary>
    /// Handles the Components
    /// </summary>
    public class ComponentManager 
    {

        private class DrawComparer : IComparer<IComponent>
        {
            public int Compare(IComponent x, IComponent y)
            {
                if (x.DrawPriority > y.DrawPriority)
                    return 1;
                else if (x.DrawPriority < y.DrawPriority)
                    return -1;
                else
                    return 0;
            }
        }

        private class UpdateComparer : IComparer<IComponent>
        {
            public int Compare(IComponent x, IComponent y)
            {
                if (x.UpdatePriority > y.UpdatePriority)
                    return 1;
                else if (x.UpdatePriority < y.UpdatePriority)
                    return -1;
                else
                    return 0;
            }
        }

        private UpdateComparer updateComparer = new UpdateComparer();
        private DrawComparer drawComparer = new DrawComparer();
        private IDictionary<String, IComponent> _comps = new Dictionary<String, IComponent>();
        private List<IComponent> _updateables = new List<IComponent>();        
        private List<IComponent> _preDrawables = new List<IComponent>();
        private List<IComponent> _posDrawables = new List<IComponent>();
        private List<IComponent> _posWithDepthDrawables = new List<IComponent>();
        private List<IComponent> updateAUX = new List<IComponent>();
        private GraphicInfo GraphicInfo;
        GraphicFactory factory;



        public ComponentManager(GraphicInfo GraphicInfo, GraphicFactory factory)
        {
            this.GraphicInfo = GraphicInfo;
            this.factory = factory;
        }

        
        /// <summary>
        /// Updates 
        /// </summary>
        /// <param name="gt">The gt.</param>
        internal void Update(GameTime gt)
        {   
            updateAUX.Clear();
            foreach (var item in _updateables)
	        {
        		 updateAUX.Add(item);
	        }

            while (updateAUX.Count > 0)
            {
                IComponent comp = updateAUX[updateAUX.Count - 1];
                comp.iUpdate(gt);                
                updateAUX.Remove(comp);                
            }
                        
        }

        /// <summary>
        /// Gets all the pre draw components.
        /// </summary>
        /// <returns></returns>
        internal IList<IComponent> GetPreDraw()
        {
            return _preDrawables;
        }

        /// <summary>
        /// Gets all the pos draw components.
        /// </summary>
        /// <returns></returns>
        internal IList<IComponent> GetPosDraw()
        {
            return _posDrawables;
        }

        /// <summary>
        /// Gets the pos with Depth draw.
        /// </summary>
        /// <returns></returns>
        internal IList<IComponent> GetPosWithDepthDraw()
        {
            return _posWithDepthDrawables;
        }
        

        /// <summary>
        /// Draw the Pre Draw components
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="activeView">The active view.</param>
        /// <param name="activeProjection">The active projection.</param>
        internal void PreDraw(RenderHelper render,GameTime gt, ref Matrix activeView, ref Matrix activeProjection)
        {
            foreach (IComponent item in _preDrawables)
            {
                item.iPreDraw(render,gt,ref activeView,ref activeProjection);
            }
        }
        /// <summary>
        /// Draw the Afters draw.
        /// </summary>
        /// <param name="render">The render.</param>
        /// <param name="gt">The gt.</param>
        /// <param name="activeView">The active view.</param>
        /// <param name="activeProjection">The active projection.</param>
        internal void AfterDraw(RenderHelper render,GameTime gt, ref Matrix activeView, ref Matrix activeProjection)
        {
            foreach (IComponent item in _posDrawables)
            {
                item.iAfterDraw(render,gt,ref activeView,ref activeProjection);
            }
        }

        /// <summary>
        /// Draw the Afters with depth draw.
        /// </summary>
        /// <param name="render">The render.</param>
        /// <param name="gt">The gt.</param>
        /// <param name="activeView">The active view.</param>
        /// <param name="activeProjection">The active projection.</param>
        internal void PosWithDepthDraw(RenderHelper render, GameTime gt, ref Matrix activeView, ref Matrix activeProjection)
        {
            foreach (IComponent item in _posWithDepthDrawables)
            {
                item.iPosWithDepthDraw(render, gt, ref activeView, ref activeProjection);
            }
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="GraphicInfo">The graphic info.</param>
        internal void LoadContent(ref GraphicInfo GraphicInfo)
        {
            foreach (IComponent item in _comps.Values)
            {
                item.iLoadContent(GraphicInfo,factory);
            }
        }

        /// <summary>
        /// Determines whether the component is already added
        /// </summary>
        /// <param name="componentName">Name of the component.</param>
        /// <returns>
        ///   <c>true</c> if the specified component name has component; otherwise, <c>false</c>.
        /// </returns>
        public bool HasComponent(String componentName)
        {
            return _comps.ContainsKey(componentName);            
        }

        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <param name="comp">The comp.</param>
        /// <returns>FALSE if the component already exist, false otherwise </returns>
        public bool AddComponent(IComponent comp)
        {
            if (HasComponent(comp.getMyName()))
                return false;

                    switch (comp.ComponentType)
                    {
                        case ComponentType.PRE_DRAWABLE:
                            _preDrawables.Add(comp);
                            break;
                        case ComponentType.POS_DRAWABLE:
                            _posDrawables.Add(comp);
                            break;
                        case ComponentType.UPDATEABLE:
                            _updateables.Add(comp);
                            break;
                        case ComponentType.PRE_DRAWABLE_AND_UPDATEABLE:
                            _preDrawables.Add(comp);
                            _updateables.Add(comp);
                            break;
                        case ComponentType.POS_DRAWABLE_AND_UPDATEABLE:
                            _posDrawables.Add(comp);
                            _updateables.Add(comp);
                            break;
                        case ComponentType.POS_WITHDEPTH_DRAWABLE:
                            _posWithDepthDrawables.Add(comp);
                            break;
                        case ComponentType.POS_WITHDEPTH_DRAWABLE_AND_UPDATEABLE:
                            _posWithDepthDrawables.Add(comp);
                            _updateables.Add(comp);
                            break;
                        case ComponentType.NONE:
                            break;
                        default:
                            break;
                    }   
                             
                  _comps.Add(comp.getMyName(), comp);
                  _comps[comp.getMyName()].iInitialize();
                  _comps[comp.getMyName()].iLoadContent(this.GraphicInfo,factory);
                  CommandProcessor.getCommandProcessor().Register(_comps[comp.getMyName()]);

                ///cold code 
                ///does not need to be optimized
                _preDrawables.Sort(this.drawComparer);
                _updateables.Sort(this.updateComparer);
                _preDrawables.Sort(this.drawComparer);
                _posDrawables.Sort(this.drawComparer);
                _posWithDepthDrawables.Sort(this.drawComparer);

                  return true;
        }


        /// <summary>
        /// Gets the component.
        /// </summary>
        /// <param name="componentName">Name of the component.</param>
        /// <returns></returns>
        public IComponent GetComponent(String componentName)
        {            
            return _comps[componentName];
        }

        /// <summary>
        /// Return all the Components Names
        /// </summary>
        /// <returns></returns>
        public ICollection<String> GetComponentsNames()
        {
            return _comps.Keys;
        }

        /// <summary>
        /// Removes the component.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>TRUE is the component exist and was remove, false otherwise</returns>
        public bool RemoveComponent(String name)
        {
            if (!HasComponent(name))
                return false;

            switch (_comps[name].ComponentType)
            {
                case ComponentType.PRE_DRAWABLE:
                    _preDrawables.Remove(_comps[name]);
                    break;
                case ComponentType.POS_DRAWABLE:
                    _posDrawables.Remove(_comps[name]);
                    break;
                case ComponentType.UPDATEABLE:
                    _updateables.Remove(_comps[name]);
                    break;
                case ComponentType.PRE_DRAWABLE_AND_UPDATEABLE:
                    _preDrawables.Remove(_comps[name]);
                    _updateables.Remove(_comps[name]);
                    break;
                case ComponentType.POS_DRAWABLE_AND_UPDATEABLE:
                    _posDrawables.Remove(_comps[name]);
                    _updateables.Remove(_comps[name]);
                    break;
                case ComponentType.POS_WITHDEPTH_DRAWABLE:
                    _posWithDepthDrawables.Remove(_comps[name]);
                    break;
                case ComponentType.POS_WITHDEPTH_DRAWABLE_AND_UPDATEABLE:
                    _posWithDepthDrawables.Remove(_comps[name]);
                    _updateables.Remove(_comps[name]);
                    break;
                case ComponentType.NONE:
                    break;
                default:
                    break;
            }                     
            CommandProcessor.getCommandProcessor().UnRegister(_comps[name]);
            _comps.Remove(name);
            return true;
        }

    }
}
