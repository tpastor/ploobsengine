using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.Modelo;
using PloobsEngine.Engine;
using SkinnedModel;

namespace PloobsProjectTemplate
{
    public class SkinnedModel : SimpleModel
    {

        public AnimationPlayer AnimationPlayer
        {
            get;
            private set;
        }
        public SkinningData SkinningData
        {
            get;
            private set;
        }

        public SkinnedModel(GraphicFactory factory, String Name) :
            base(factory, Name)
        {
            // Look up our custom skinning information.
            SkinningData = this.model.Tag as SkinningData;

            if (SkinningData == null)
                throw new InvalidOperationException
                    ("This model does not contain a SkinningData tag.");

            // Create an animation player, and start decoding an animation clip.
            AnimationPlayer = new AnimationPlayer(SkinningData);
        }
    }
}
