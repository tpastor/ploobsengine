using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Draw Helper Specification    
    /// </summary>
    public interface IRenderHelper
    {
        /// <summary>
        /// Gets or sets a scene with the specified name.
        /// </summary>
        Texture2D this[String scene]
        {
            get;
            set;
        }

        /// <summary>
        /// Sets up render target.
        /// When this render target is removed, it restores the state to the last RenderTarget
        /// </summary>
        /// <param name="target">The target.</param>
        void SetUpRenderTarget(RenderTarget2D target);
        
        /// <summary>
        /// Sets up render target.
        /// When this render target is removed, it restores the state to the source RenderTarget
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        void SetUpRenderTarget(RenderTarget2D source, RenderTarget2D target);
        
        /// <summary>
        /// Sets up render target, clean
        /// When this render target is removed, it restores the state to the source RenderTarget
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        /// <param name="opts">The opts.</param>
        /// <param name="color">The color.</param>
        /// <param name="z">The z.</param>
        /// <param name="stencil">The stencil.</param>
        void SetUpRenderTarget(RenderTarget2D source, RenderTarget2D target,ClearOptions opts, Color color, float z,int stencil);

        /// <summary>
        /// Gets the texture of the current render target.
        /// Every call to this function should has a SetUpRenderTarget pair (you need to call SetUpRenderTarget before)
        /// In this function that the Render Target is unset, and the older state is restored
        /// </summary>
        /// <returns></returns>
        Texture2D GetResultsRenderTarget();


        /// <summary>
        /// Renders the texture to full screen using vertex and pixel shader .
        /// </summary>
        /// <param name="effect">The effect.</param>
        void RenderTextureToFullScreenVertexPixel(Effect effect);

        /// <summary>
        /// Renders the texture to full screen using sprite batch.
        /// </summary>
        /// <param name="scene">The scene.</param>
        /// <param name="effect">The effect.</param>
        void RenderTextureToFullScreenSpriteBatch(Texture2D scene, Effect effect);

        /// <summary>
        /// Renders the texture to full screen using sprite batch.
        /// </summary>
        /// <param name="scene">The scene name.</param>
        /// <param name="effect">The effect.</param>
        void RenderTextureToFullScreenSpriteBatch(String scene,Effect effect);
        /// <summary>
        /// Renders the texture to rectangle portion of the screen using sprite batch.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="effect">The effect.</param>
        /// <param name="rectangle">The rectangle.</param>
        void RenderTextureToFullScreenSpriteBatch(String texture, Effect effect , Rectangle rectangle);

        /// <summary>
        /// Renders the scene without material.
        /// Dont Draw Components, just the scene
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="view">The view.</param>
        /// <param name="projection">The projection.</param>
        void RenderSceneWithoutMaterial(IWorld world, Matrix view, Matrix projection);
        /// <summary>
        /// Renders the scene without material.
        /// Dont Draw Components, just the scene
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="objListException">The obj list exception. (objects in this list wont be rendered)</param>
        /// <param name="view">The view.</param>
        /// <param name="projection">The projection.</param>
        void RenderSceneWithoutMaterial(IWorld world,  List<IObject> objListException, Matrix view, Matrix projection);
        
        /// <summary>
        /// Renders the scene without material.
        /// The parameter
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="objListException">The obj list exception.(objects in this list wont be rendered)</param>
        /// <param name="view">The view.</param>
        /// <param name="projection">The projection.</param>
        /// <param name="drawComponentsPreDraw">if set to <c>true</c> [draw components with pre draw setting also].</param>
        void RenderSceneWithoutMaterial(IWorld world, List<IObject> objListException, Matrix view, Matrix projection,bool drawComponentsPreDraw);
       
    }

}
