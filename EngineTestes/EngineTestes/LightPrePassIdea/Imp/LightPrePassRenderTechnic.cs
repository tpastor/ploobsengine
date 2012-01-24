using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using PloobsEngine.Engine;

namespace EngineTestes.LightPrePassIdea.Imp
{
    class LightPrePassRenderTechnic : IRenderTechnic
    {
        public LightPrePassRenderTechnic()
            :base(PostEffectType.Forward3D)
        {
        }

        protected override void ExecuteTechnic(Microsoft.Xna.Framework.GameTime gameTime, RenderHelper render, IWorld world)
        {
            GenerateGBuffer.RenderScene(render, world.CameraManager.ActiveCamera, world, gameTime,ginfo);
        }

        GraphicInfo ginfo;
        protected override void AfterLoadContent(IContentManager manager, PloobsEngine.Engine.GraphicInfo ginfo, PloobsEngine.Engine.GraphicFactory factory)
        {
            this.ginfo = ginfo;
            GenerateGBuffer = new GenerateGBuffer(factory, ginfo, ginfo.BackBufferWidth, ginfo.BackBufferHeight);
            base.AfterLoadContent(manager, ginfo, factory);
        }

        GenerateGBuffer GenerateGBuffer;

        private const String name = "LightPrePass";
        public override string TechnicName
        {
            get { return name; }
        }

        public override void CleanUp()
        {
        }
    }
}
