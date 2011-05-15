using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Engine.Physic
{
    public interface IPhysicObject
    {
        Vector3 Position { set; get; }
        Vector3 Scale { set; get; }
        Quaternion Rotation { set; get; }
        Matrix WorldMatrix { set; get; }
    }
}
