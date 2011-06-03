#if !WINDOWS_PHONE
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
    }

        
}
#endif