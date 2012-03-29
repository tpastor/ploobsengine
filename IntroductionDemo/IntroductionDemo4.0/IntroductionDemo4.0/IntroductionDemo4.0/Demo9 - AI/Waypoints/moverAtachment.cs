using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework;
using PloobsEngine.IA;

namespace EngineTestes.AI
{
    class MoverAtachment : IObjectAttachment
    {
        Curve cx = new Curve();
        Curve cy = new Curve();
        Curve cz = new Curve();

        public MoverAtachment(LinkedList<Waypoint>  waypoints, float timeAdded = 300)
        {
            float time = 0;
            foreach (var item in waypoints)
            {
                cx.Keys.Add(new CurveKey(time, item.WorldPos.X));
                cy.Keys.Add(new CurveKey(time, item.WorldPos.Y));
                cz.Keys.Add(new CurveKey(time, item.WorldPos.Z));
                time += timeAdded;
            }

            cx.ComputeTangents(CurveTangent.Smooth);
            cy.ComputeTangents(CurveTangent.Smooth);
            cz.ComputeTangents(CurveTangent.Smooth);
        }

        int t = 0;
        protected override void Update(IObject obj, Microsoft.Xna.Framework.GameTime gt)
        {       
            t++;
            obj.PhysicObject.Position = new Vector3(cx.Evaluate(t), cy.Evaluate(t) + 2.5f, cz.Evaluate(t));
        }
    }
}
