using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using PloobsEngine.Material;
using PloobsEngine.Modelo;
using PloobsEngine.Engine;
using PloobsEngine.Physics;
using SkinnedModel;
using Microsoft.Xna.Framework;

namespace PloobsProjectTemplate
{
    public class SkinnedIObject : IObject
    {
        public SkinnedIObject(GraphicFactory factory,String modelName, IPhysicObject phyobj)         
        {
            this.Modelo = SkinnedModel = new SkinnedModel(factory,modelName);
            ForwardSkinnedShader = new ForwardSkinnedShader();
            this.Material = new ForwardMaterial(ForwardSkinnedShader);
            this.PhysicObject = phyobj;
            IObjectAttachment = new List<IObjectAttachment>();
            Name = modelName;            
        }


        public ForwardSkinnedShader ForwardSkinnedShader
        {
            get;
            private set;
        }
        public SkinnedModel SkinnedModel
        {
            get;
            private set;
        }

        protected override void UpdateObject(Microsoft.Xna.Framework.GameTime gt, PloobsEngine.Cameras.ICamera cam, IWorld world)
        {
            SkinnedModel.AnimationPlayer.Update(gt.ElapsedGameTime, true, Matrix.Identity);
            Microsoft.Xna.Framework.Matrix[] bones;
            bones = SkinnedModel.AnimationPlayer.GetSkinTransforms();
            ///You can manipulate the bones here !!! The way you want
            ForwardSkinnedShader.SetBones(bones);
            base.UpdateObject(gt, cam, world);
        }
    }
}
