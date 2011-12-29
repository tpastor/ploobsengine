using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using PloobsEngine.SceneControl;
using PloobsEngine;              
    

namespace Bnoerj.AI.Steering.Pedestrian
{
	using ProximityDatabase = IProximityDatabase<IVehicle>;
	using ProximityToken = ITokenForProximityDatabase<IVehicle>;
    

	public class PedestrianPlugIn : IPlugIn
	{
        public Func<Pedestrian, IObject> ObjectCreator;
        IWorld world;
        Path path;
        static Clock clock = new Clock();


		public PedestrianPlugIn(IWorld world, Path path, Func<Pedestrian,IObject> ObjectCreator)
			: base()
		{
            this.path = path;
			crowd = new List<Pedestrian>();
            this.ObjectCreator = ObjectCreator;
            this.world = world;            
		}

		public String Name { get { return "Pedestrians"; } }		

		public void Init()
		{
			// make the database used to accelerate proximity queries
			cyclePD = -1;
			NextPD();

			// create the specified number of Pedestrians
			population = 0;
            for (int i = 0; i < 10; i++)
            {
                population++;
			    Pedestrian pedestrian = new Pedestrian(pd,path);
                pedestrian.Reset();

                world.AddObject(ObjectCreator(pedestrian));
			    crowd.Add(pedestrian);			
            }
			
		}

		public void Update()
		{
            clock.Update();

			// update each Pedestrian
			for (int i = 0; i < crowd.Count; i++)
			{
                crowd[i].Update(clock.TotalSimulationTime, clock.ElapsedSimulationTime);
			}
		}
		
		void NextPD()
		{
			// save pointer to old PD
			ProximityDatabase oldPD = pd;

			// allocate new PD
			const int totalPD = 2;
			switch (cyclePD = (cyclePD + 1) % totalPD)
			{
			case 0:
				{
					Vector3 center = Vector3.Zero;
					float div = 20.0f;
					Vector3 divisions = new Vector3(div, 1.0f, div);
					float diameter = 80.0f; //XXX need better way to get this
					Vector3 dimensions = new Vector3(diameter, diameter, diameter);
					pd = new LocalityQueryProximityDatabase<IVehicle>(center, dimensions, divisions);
					break;
				}
			case 1:
				{
					pd = new BruteForceProximityDatabase<IVehicle>();
					break;
				}
			}

			// switch each boid to new PD
			for (int i = 0; i < crowd.Count; i++) crowd[i].NewPD(pd);

			// delete old PD (if any)
			oldPD = null;
		}

		public List<IVehicle> Vehicles
		{
			get { return crowd.ConvertAll<IVehicle>(delegate(Pedestrian p) { return (IVehicle)p; }); }
		}

		// crowd: a group of all Pedestrians
		List<Pedestrian> crowd;

		// pointer to database used to accelerate proximity queries
		ProximityDatabase pd;

		// keep track of current flock size
		int population;

		// which of the various proximity databases is currently in use
		int cyclePD = 0;
	}
}
