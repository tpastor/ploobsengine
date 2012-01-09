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
#if !WINDOWS_PHONE && !REACH
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using PloobsEngine.Utils;
using PloobsEngine.Modelo.Animation;
using PloobsEngine.Modelo;
using PloobsEngine.SceneControl;
using PloobsEngine.Light;
using LTreesLibrary.Trees.Wind;
using LTreesLibrary.Trees;


namespace PloobsEngine.Material
{
    /// <summary>
    /// Tree Material
    /// </summary>
    public class TreeMaterial : IMaterial
    {
        private TreeWindAnimator animator;
        WindSource wsource;

        /// <summary>
        /// Gets or sets the wsource.
        /// </summary>
        /// <value>
        /// The wsource.
        /// </value>
        public WindSource Wsource
        {
            get { return wsource; }
            set
            {
                wsource = value;
                animator.WindSource = value;
            }
        }        

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeMaterial"/> class.
        /// </summary>
        /// <param name="shader">The shader.</param>
        public TreeMaterial(DeferredTreeShader shader)         
        {
            wsource = new WindStrengthSin();
            this.animator = new TreeWindAnimator(wsource);
            this.shader = shader;
            CanAppearOfReflectionRefraction = true;
            CanCreateShadow = true;
            IsVisible = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeMaterial"/> class.
        /// </summary>
        /// <param name="shader">The shader.</param>
        public TreeMaterial(ForwardTreeShader shader)
        {
            wsource = new WindStrengthSin();
            this.animator = new TreeWindAnimator(wsource);
            this.shader = shader;
            CanAppearOfReflectionRefraction = true;
            CanCreateShadow = true;
            IsVisible = true;
        }

        /// <summary>
        /// Cleans up.
        /// </summary>
        /// <param name="factory"></param>
        public void CleanUp(PloobsEngine.Engine.GraphicFactory factory)
        {
            shader.Cleanup(factory);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeMaterial"/> class.
        /// </summary>
        /// <param name="shader">The shader.</param>
        /// <param name="wsource">The wsource.</param>
        public TreeMaterial(DeferredTreeShader shader, WindSource wsource) 
        {
            this.wsource = wsource;
            this.shader = shader;
            this.animator = new TreeWindAnimator(wsource);
            CanAppearOfReflectionRefraction = true;
            CanCreateShadow = true;
            IsVisible = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeMaterial"/> class.
        /// </summary>
        /// <param name="shader">The shader.</param>
        /// <param name="wsource">The wsource.</param>
        public TreeMaterial(ForwardTreeShader shader, WindSource wsource)
        {
            this.wsource = wsource;
            this.shader = shader;
            this.animator = new TreeWindAnimator(wsource);
            CanAppearOfReflectionRefraction = true;
            CanCreateShadow = true;
            IsVisible = true;
        }

        IShader shader;
#region IMaterial Members

        public bool CanCreateShadow
        {
            get;
            set;
        }

        public void Drawn(Microsoft.Xna.Framework.GameTime gt, IObject obj, Cameras.ICamera cam, IList<ILight> lights, RenderHelper render)
        {
            shader.Draw(gt, obj, render, cam,lights);
        }

        public void Initialization(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory, IObject obj)
        {
            shader.Initialize(ginfo, factory, obj);
        }

        public MaterialType MaterialType
        {
            get { return shader.MaterialType; }
        }

        public void PosDrawnPhase(Microsoft.Xna.Framework.GameTime gt, IObject obj, Cameras.ICamera cam, IList<ILight> lights, RenderHelper render)
        {
            shader.PosDrawPhase(gt, obj, render, cam, lights);
        }

        public void PreDrawnPhase(Microsoft.Xna.Framework.GameTime gt, IWorld mundo, IObject obj, Cameras.ICamera cam, IList<ILight> lights, RenderHelper render)
        {
         
        }        

        public IShader Shadder
        {
            get
            {
                return shader;
            }
            set
            {
                throw new InvalidOperationException("cant change the shader of this material");
            }
        }

        public void Update(Microsoft.Xna.Framework.GameTime gametime, IObject obj, IList<ILight> lights)
        {
            shader.Update(gametime, obj, lights);
            wsource.Update(gametime);
            SimpleTree tree = (obj.Modelo as TreeModel).Tree;
            animator.Animate(tree.Skeleton, tree.AnimationState, gametime);            
        }

        #endregion

#region ISerializable Members

        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            
        }

        #endregion

#region IMaterial Members


        public bool CanAppearOfReflectionRefraction
        {
            get;
            set;
        }

        #endregion

#region IMaterial Members


        /// <summary>
        /// Gets or sets a value indicating whether this instance is visible.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsVisible
        {
            get;
            set;
        }

        #endregion
    }

        
}
#endif