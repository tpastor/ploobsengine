using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Physics2D.Farseer
{
    public class FarseerWorld : I2DPhysicWorld
    {
        public FarseerWorld(Vector2 gravity)
        {
            world = new World(gravity);

#if WINDOWS || XBOX
            ConvertUnits.SetDisplayUnitToSimUnitRatio(24f);
      
#elif WINDOWS_PHONE
            ConvertUnits.SetDisplayUnitToSimUnitRatio(16f);      
#endif
        }

        private World world;

        public World World
        {
            get { return world; }
            set { world = value; }
        }

        protected override void Update(Microsoft.Xna.Framework.GameTime gt)
        {            
            // variable time step but never less then 30 Hz
            world.Step(Math.Min((float)gt.ElapsedGameTime.TotalSeconds, (1f / 30f)));               
        }

        public override void AddObject(I2DPhysicObject obj)
        {
            if (obj is FarseerObject)
            {
                FarseerObject ob = obj as FarseerObject;
                ob.Body.UserData = obj;
                ob.Body.Enabled = true;                
            }
        }

        public override void RemoveObject(I2DPhysicObject obj)
        {
            if (obj is FarseerObject)
            {
                FarseerObject ob = obj as FarseerObject;
                world.RemoveBody(ob.Body);
            }
        }
    }
}
