using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Components;
using Microsoft.Xna.Framework.Graphics;

namespace EngineTestes.Decal
{
    public class DecalComponent : IComponent
    {
        public DecalComponent()
            :base(int.MinValue)
        {
        }

        public List<Decal> Decals = new List<Decal>();
        
        public override ComponentType ComponentType
        {
            get { return PloobsEngine.Components.ComponentType.POS_WITHDEPTH_DRAWABLE; }
        }

        public static readonly String MyName = "Decal";
        public override string getMyName()
        {
            return MyName;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void PosWithDepthDraw(PloobsEngine.SceneControl.RenderHelper render, Microsoft.Xna.Framework.GameTime gt, Microsoft.Xna.Framework.Matrix activeView, Microsoft.Xna.Framework.Matrix activeProjection)
        {
            render.PushBlendState(BlendState.Additive);
            foreach (var item in Decals)
            {
                
            }
            render.PopBlendState();
            base.PosWithDepthDraw(render, gt, activeView, activeProjection);
        }
    }
}
