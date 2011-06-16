using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using PloobsEngine.Material;
using PloobsEngine.Engine;
using PloobsEngine.Modelo;
using PloobsEngine.Physics.Bepu;
using Microsoft.Xna.Framework;
using PloobsEngine.Physics;

namespace EngineTestes.Bolt
{
    public class BoltObject: IObject
    {
        public BoltObject(GraphicFactory factory, Vector3 Postion, Vector3 direction, float speed = 150)
            : base(new ForwardMaterial(new BoltShader(factory)), new SimpleModel(factory,"Model/LaserBolt2"), new CylinderObject(Postion, 5, 1,Vector3.One,10, Matrix.Identity, MaterialDescription.DefaultBepuMaterial()))
        {
            this.PhysicObject.Velocity = direction * speed;
            Vector3 vec = Vector3.Normalize(direction);
            //Vector3 front = this.Modelo.GetBatchInformation(0)[0].ModelLocalTransformation.Forward;
            Vector3 front = Vector3.Up;
            Quaternion q = GetRotation(front, vec);
            (this.PhysicObject as BepuEntityObject).Entity.Orientation = q;
            if (Vector3.Transform(front, q) == vec)
            {
            }
        }
        private Quaternion GetRotation(Vector3 src, Vector3 dest)
        {
            src.Normalize();
            dest.Normalize();

            float d = Vector3.Dot(src, dest);

            if (d >= 1f)
            {
                return Quaternion.Identity;
            }
            else if (d < (1e-6f - 1.0f))
            {
                Vector3 axis = Vector3.Cross(Vector3.UnitX, src);

                if (axis.LengthSquared() == 0)
                {
                    axis = Vector3.Cross(Vector3.UnitY, src);
                }

                axis.Normalize();
                return Quaternion.CreateFromAxisAngle(axis, MathHelper.Pi);
            }
            else
            {
                float s = (float)Math.Sqrt((1 + d) * 2);
                float invS = 1 / s;

                Vector3 c = Vector3.Cross(src, dest);
                Quaternion q = new Quaternion(invS * c, 0.5f * s);
                q.Normalize();

                return q;
            }
        }
    }
}
