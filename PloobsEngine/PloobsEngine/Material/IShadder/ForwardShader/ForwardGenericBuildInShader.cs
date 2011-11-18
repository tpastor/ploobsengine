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
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Engine;
using PloobsEngine.Modelo;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using PloobsEngine.Light;
using PloobsEngine.Cameras;

namespace PloobsEngine.Material
{

    public delegate void drawFunction(ICamera ICamera, GameTime GameTime, RenderHelper RenderHelper, IObject IObject, IList<ILight> Lights);        
    /// <summary>    
    /// Wrapper to xna effects
    /// </summary>
    public class ForwardGenericBuildInShader<effect> : IShader where effect : Effect
    {

        drawFunction draw;
        Action<GameTime, IObject,IList<ILight>> up;

        public ForwardGenericBuildInShader(effect effect, drawFunction drawFunction ,Action<GameTime, IObject, IList<ILight>> onUpdate = null)            
        {
            Debug.Assert(effect != null);
            //Debug.Assert(onDraw != null);
            this.up = onUpdate;
            this.draw = drawFunction;
            this.eff = effect;
        }

        private effect eff;

        public effect Effect
        {
            get { return eff; }            
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <param name="ginfo"></param>
        /// <param name="factory"></param>
        /// <param name="obj"></param>
        public override void Initialize(GraphicInfo ginfo, GraphicFactory factory, IObject obj)
        {            
            base.Initialize(ginfo,factory,obj);
        }

        public override void Update(GameTime gt, IObject ent, IList<Light.ILight> lights)
        {
            base.Update(gt, ent, lights);

            if(up != null)
                up(gt, ent,lights);
        }             

        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="gt">gametime</param>
        /// <param name="obj">the obj</param>
        /// <param name="render">The render.</param>
        /// <param name="cam">The cam.</param>
        /// <param name="lights"></param>
        public override void Draw(GameTime gt, IObject obj, RenderHelper render, Cameras.ICamera cam, IList<Light.ILight> lights)
        {
            base.Draw(gt, obj, render, cam, lights);
            draw(cam, gt, render, obj, lights);            
        }

        /// <summary>
        /// Gets the type of the material.
        /// </summary>
        /// <value>
        /// The type of the material.
        /// </value>
        public override MaterialType MaterialType
        {
            get { return Material.MaterialType.FORWARD; }
        }
    }
}
