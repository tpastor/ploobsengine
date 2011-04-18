using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PloobsEngine.SceneControl
{
    public class DepthCueingPostEffect : IPostEffect
    {
        public DepthCueingPostEffect()
            : base(PostEffectType.Deferred)
        {
        }

        Effect effect = null;

        public override void Draw(Texture2D ImageToProcess, RenderHelper rHelper, GameTime gt, Engine.GraphicInfo GraphicInfo, IWorld world, bool useFloatingBuffer)
        {
             effect.Parameters["Z_Near"].SetValue(world.CameraManager.ActiveCamera.NearPlane);
             effect.Parameters["Z_Far"].SetValue(world.CameraManager.ActiveCamera.FarPlane);
             effect.Parameters["depth"].SetValue(rHelper[PrincipalConstants.dephRT]);
             effect.Parameters["color"].SetValue(ImageToProcess);
             effect.Parameters["InvertViewProjection"].SetValue(Matrix.Invert(world.CameraManager.ActiveCamera.View * world.CameraManager.ActiveCamera.Projection));
             effect.Parameters["halfPixel"].SetValue(GraphicInfo.HalfPixel);
             effect.Parameters["cameraPosition"].SetValue(world.CameraManager.ActiveCamera.Position);

             if (useFloatingBuffer)
                 rHelper.RenderFullScreenQuadVertexPixel(effect, SamplerState.PointClamp);
             else
                 rHelper.RenderFullScreenQuadVertexPixel(effect, SamplerState.AnisotropicClamp);        
         
        }

        public override void Init(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory)
        {
            effect = factory.GetEffect("CuingDepth",false,true);            
        }

    }
}


