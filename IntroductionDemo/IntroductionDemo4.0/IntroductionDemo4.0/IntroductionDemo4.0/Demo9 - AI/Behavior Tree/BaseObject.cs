using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using PloobsEngine.Material;
using PloobsEngine.Physics;
using PloobsEngine.Modelo;
using BehaviorTrees;

namespace IntroductionDemo4._0
{
    public class BaseObject : IObject, BehaviorTrees.Entity
    {
        public BaseObject(IMaterial mat, IPhysicObject py, IModelo model)
            : base(mat,model,py)
        {
        }

        Node<BaseObject> behavior;
        Task<BaseObject> task;
        public Node<BaseObject> Behavior
        {
            get
            {
                return behavior;
            }

            set
            {
                behavior = value;
                task = behavior.Evaluate(this);
            }
        }

        protected override void UpdateObject(Microsoft.Xna.Framework.GameTime gt, PloobsEngine.Cameras.ICamera cam, IWorld world)
        {
            
            TaskResult tr = task.Execute(this, gt);
            base.UpdateObject(gt, cam, world);
        }
    }
}
