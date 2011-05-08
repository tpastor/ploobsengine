using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Light;
using PloobsEngine.Physics;

namespace AdvancedDemo4._0
{
    public class MoveablePointLight : PointLightPE
    {

        IPhysicObject ob;

        public MoveablePointLight(IPhysicObject obj, Color color, float lightRadius, float lightIntensity)
            : base(obj.Position, color, lightRadius, lightIntensity)
        {
            this.ob = obj;
        }

        public override Vector3 LightPosition
        {
            get
            {
                return ob.Position;
            }
            set
            {
                ob.Position = value;
            }
        }
        
    }
}
