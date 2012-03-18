using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using Microsoft.Xna.Framework;
using PloobsEngine.IA;
using Bnoerj.AI.Steering;

namespace EngineTestes.AI
{
    class SteerAtachment : IObjectAttachment
    {
        IVehicle vehicle;

        public IVehicle Vehicle
        {
            get { return vehicle; }
            set { vehicle = value; }
        }
        public SteerAtachment(IVehicle Vehicle)
        {
            this.vehicle = Vehicle;
        }

        protected override void Update(IObject obj, Microsoft.Xna.Framework.GameTime gt)
        {
            obj.PhysicObject.Position = vehicle.Position;
        }
    }
}
