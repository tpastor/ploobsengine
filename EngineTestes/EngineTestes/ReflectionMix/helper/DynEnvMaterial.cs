using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Material;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PloobsProjectTemplate.TemplateScreens
{
    public class DynEnvMaterial : IMaterial
    {        
        public DynEnvMaterial()
        {
            IsVisible = true;
        }

        ForwardEnvironmentShader ForwardEnvironmentShader;
        public void AfterAdded(PloobsEngine.SceneControl.IObject obj)
        {
            
        }

        public void CleanUp(PloobsEngine.Engine.GraphicFactory factory)
        {
            ForwardEnvironmentShader.Cleanup(factory);
            if (RenderTargetCube != null && !RenderTargetCube.IsDisposed)
                RenderTargetCube.Dispose();
        }

        public void Drawn(Microsoft.Xna.Framework.GameTime gt, PloobsEngine.SceneControl.IObject obj, PloobsEngine.Cameras.ICamera cam, IList<PloobsEngine.Light.ILight> lights, PloobsEngine.SceneControl.RenderHelper render)
        {
            ForwardEnvironmentShader.iDraw(gt, obj, render, cam, lights);
        }

        public void Initialization(PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory, PloobsEngine.SceneControl.IObject obj)
        {
            RenderTargetCube = factory.CreateRenderTargetCube(1024,SurfaceFormat.Color,false,DepthFormat.Depth24Stencil8,0,RenderTargetUsage.DiscardContents);
            ForwardEnvironmentShader = new ForwardEnvironmentShader(RenderTargetCube,1,false);
            ForwardEnvironmentShader.Initialize(ginfo, factory, obj);
            
        }

        public bool IsVisible
        {
            get;
            set;
        }

        public MaterialType MaterialType
        {
            get { return PloobsEngine.Material.MaterialType.FORWARD; }
        }

        public void PosDrawnPhase(Microsoft.Xna.Framework.GameTime gt, PloobsEngine.SceneControl.IObject obj, PloobsEngine.Cameras.ICamera cam, IList<PloobsEngine.Light.ILight> lights, PloobsEngine.SceneControl.RenderHelper render)
        {            
        }

        
        RenderTargetCube RenderTargetCube;
        public void PreDrawnPhase(Microsoft.Xna.Framework.GameTime gt, PloobsEngine.SceneControl.IWorld mundo, PloobsEngine.SceneControl.IObject obj, PloobsEngine.Cameras.ICamera cam, IList<PloobsEngine.Light.ILight> lights, PloobsEngine.SceneControl.RenderHelper render)
        {
            Vector3 pos = obj.PhysicObject.Position;            
            
            render.RenderSceneToTextureCube(RenderTargetCube,
                Color.AliceBlue, mundo, ref pos,gt, true, false, new List<PloobsEngine.SceneControl.IObject>() { obj });
            
        }

        public IShader Shader
        {
            get
            {
                return ForwardEnvironmentShader;
            }
            set
            {
                this.ForwardEnvironmentShader = value as ForwardEnvironmentShader;
            }
        }

        public void Update(Microsoft.Xna.Framework.GameTime gametime, PloobsEngine.SceneControl.IObject obj, PloobsEngine.SceneControl.IWorld world)
        {
            ForwardEnvironmentShader.Update(gametime, obj, world.Lights);
        }

        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
        
        }

        #region IMaterial Members


        public bool CanCreateShadow
        {
            get;
            set;
        }

        public bool CanAppearOfReflectionRefraction
        {
            get;
            set;
        }

        #endregion
    }
}
