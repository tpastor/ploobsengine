using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.SceneControl;

namespace PloobsEngine.Engine
{
    /// <summary>
    /// Creates everything related to Graphics
    /// </summary>
    public class GraphicFactory
    {
        GraphicsDevice device;
        GraphicInfo ginfo;
        IContentManager contentManager;

        public GraphicFactory(GraphicInfo ginfo, GraphicsDevice device,IContentManager contentManager)
        {
            this.device = device;
            this.ginfo = ginfo;
            this.contentManager = contentManager;
        }

        public BasicEffect GetBasicEffect()
        {
            return new BasicEffect(device);
        }

        public Effect GetEffect(String name, bool clone = false)
        {
            Effect effect = contentManager.GetAsset<Effect>(name);
            if (clone)
                return effect.Clone();
            return effect;
        }

    }
}
