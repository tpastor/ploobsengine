using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PloobsEngine.Engine.Logger;
using PloobsEngine.Engine;
using PloobsEngine.Material;
using PloobsEngine.Modelo;

namespace PloobsEngine.SceneControl
{
    public class LightPrePassCombination : IDeferredFinalCombination
    {                      
        private Effect finalCombineEffect;                 
        private RenderTarget2D target;
        GraphicInfo ginfo;
        private Color ambientColor = Color.Black;
        private bool saveToTexture;
        private bool useFloatBuffer;
        private Effect effect;

        public Color AmbientColor
        {
            get { return ambientColor; }
            set { ambientColor = value; }
        }

        public LightPrePassCombination(Color AmbientColor)
        {
            this.ambientColor = AmbientColor;
        }       
        

        #region IDeferredFinalCombination Members


        public Texture2D this[GBufferTypes type]
        {
            get
            {
                if (type == GBufferTypes.FINALIMAGE)
                {
                    return this.target;
                }
                else
                {
                    ActiveLogger.LogMessage("Wrong Image Type requested", LogLevel.FatalError);
                    throw new Exception("wrong type");
                }
            }
        }

        #endregion

        #region IDeferredFinalCombination Members

        public void SetFinalCombination(RenderHelper render)
        {
            if (saveToTexture)
            {
                render.PushRenderTarget(target);                
            }            
        }

        Vector2 half;

        public void DrawScene(GameTime gameTime, IWorld world, IDeferredGBuffer gbuffer, IDeferredLightMap lightmap, RenderHelper render)
        {            

            //if (useFloatBuffer)
            //{
            //    render.SetSamplerState(SamplerState.PointClamp, 0);            
            //}
            //else
            //{
            //    render.SetSamplerState(ginfo.SamplerState, 0);
            //    render.SetSamplerState(ginfo.SamplerState, 1);
            //    render.SetSamplerState(ginfo.SamplerState, 2);                
            //}

            render.Clear(Color.CornflowerBlue);
            effect.Parameters["halfPixel"].SetValue(half);
            effect.Parameters["View"].SetValue(world.CameraManager.ActiveCamera.View);
            effect.Parameters["Projection"].SetValue(world.CameraManager.ActiveCamera.Projection);
            effect.Parameters["light"].SetValue(lightmap[DeferredLightMapType.LIGHTMAP]);            
            {

                foreach (IObject item in world.Culler.GetNotCulledObjectsList(MaterialType.DEFERRED))
                {                    
                    for (int i = 0; i < item.Modelo.MeshNumber; i++)
                    {                        
                        BatchInformation[] bi = item.Modelo.GetBatchInformation(i);
                        for (int j = 0; j < bi.Count(); j++)
                        {
                            effect.Parameters["Texture"].SetValue(item.Modelo.getTexture(TextureType.DIFFUSE, i, j));
                            effect.Parameters["World"].SetValue(bi[j].ModelLocalTransformation * item.WorldMatrix);
                            render.RenderBatch(bi[j], effect);
                        }
                    }
                }
            }

            if (saveToTexture)
            {
                render.PopRenderTarget();                
            }
            
            
        }


        #endregion

        #region IDeferredFinalCombination Members


        public void LoadContent(IContentManager manager, Engine.GraphicInfo ginfo, Engine.GraphicFactory factory, bool useFloatBuffer, bool saveToTexture )
        {
            this.useFloatBuffer = useFloatBuffer;
            this.ginfo = ginfo;
            this.saveToTexture = saveToTexture;
            effect = factory.GetEffect("Effects//hibe");
            finalCombineEffect = manager.GetAsset<Effect>("CombineFinal",true);
            if (saveToTexture)
            {
                //if (useFloatBuffer)
                //    target = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.HdrBlendable, ginfo.UseMipMap, DepthFormat.None, ginfo.MultiSample, RenderTargetUsage.DiscardContents);
                //else
                    target = factory.CreateRenderTarget(ginfo.BackBufferWidth, ginfo.BackBufferHeight, SurfaceFormat.Color, ginfo.UseMipMap, DepthFormat.None, ginfo.MultiSample, RenderTargetUsage.DiscardContents);
            }

            half = ginfo.HalfPixel;
        }

        #endregion
    }
}
