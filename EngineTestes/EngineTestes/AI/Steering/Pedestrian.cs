// Copyright (c) 2002-2003, Sony Computer Entertainment America
// Copyright (c) 2002-2003, Craig Reynolds <craig_reynolds@playstation.sony.com>
// Copyright (C) 2007 Bjoern Graf <bjoern.graf@gmx.net>
// Copyright (C) 2007 Michael Coles <michael@digini.com>
// All rights reserved.
//
// This software is licensed as described in the file license.txt, which
// you should have received as part of this distribution. The terms
// are also available at http://www.codeplex.com/SharpSteer/Project/License.aspx.

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Bnoerj.AI.Steering.Pedestrian
{
	using ProximityDatabase = IProximityDatabase<IVehicle>;
	using ProximityToken = ITokenForProximityDatabase<IVehicle>;
    using PloobsEngine.Utils;
    
	public class Pedestrian : SimpleVehicle
	{

        Path hpath;
		// constructor
        public Pedestrian(ProximityDatabase pd, Path hpath ) : base(false)
		{
            this.hpath = hpath;            
			// allocate a token for this boid in the proximity database
			proximityToken = null;
			NewPD(pd);

			// reset Pedestrian state
			Reset();
		}

		// reset all instance state
		public override void Reset()
		{
			// reset the vehicle 
			base.Reset();

			// max speed and max steering force (maneuverability) 
			MaxSpeed = 2.0f;
			MaxForce = 8.0f;

			// initially stopped
			Speed = 0;

			// size of bounding sphere, for obstacle avoidance, etc.
			Radius = 0.5f; // width = 0.7, add 0.3 margin, take half
            
            // set initial position
			// (random point on path + random horizontal offset)
            float d = hpath.PolyPath.TotalPathLength * StaticRandom.Random();
            float r = hpath.PolyPath.radius;
			Vector3 randomOffset = VectorUtils.RandomVectorOnUnitRadiusXZDisk() * r;
            Position = (hpath.PolyPath.MapPathDistanceToPoint(d) + randomOffset);

			// randomize 2D heading
			RandomizeHeadingOnXZPlane();

			// pick a random direction for path following (upstream or downstream)
            pathDirection = (StaticRandom.Random() > 0.5) ? -1 : +1;
			
			// notify proximity database that our position has changed
			if (proximityToken != null) proximityToken.UpdateForNewPosition(Position);
		}

		// per frame simulation update
		public void Update(float currentTime, float elapsedTime)
		{
			// apply steering force to our momentum
			ApplySteeringForce(DetermineCombinedSteering(elapsedTime), elapsedTime);            

			// reverse direction when we reach an endpoint
			if (Globals.UseDirectedPathFollowing)
			{
                if (Vector3.Distance(Position, hpath.PolyPath.points[0]) < hpath.PolyPath.radius)
				{
					pathDirection = +1;					
				}
                if (Vector3.Distance(Position, hpath.PolyPath.points[hpath.PolyPath.pointCount - 1]) < hpath.PolyPath.radius)
				{
					pathDirection = -1;					
				}
			}
		
			// notify proximity database that our position has changed
			proximityToken.UpdateForNewPosition(Position);            
		}

		// compute combined steering force: move forward, avoid obstacles
		// or neighbors if needed, otherwise follow the path and wander
		public Vector3 DetermineCombinedSteering(float elapsedTime)
		{
			// move forward
			Vector3 steeringForce = Forward;

			// probability that a lower priority behavior will be given a
			// chance to "drive" even if a higher priority behavior might
			// otherwise be triggered.
			const float leakThrough = 0.1f;

			// determine if obstacle avoidance is required
			Vector3 obstacleAvoidance = Vector3.Zero;
            if (leakThrough < StaticRandom.Random())
			{
				const float oTime = 6; // minTimeToCollision = 6 seconds
                obstacleAvoidance = SteerToAvoidObstacles(oTime, hpath.Obstacles);
			}

			// if obstacle avoidance is needed, do it
			if (obstacleAvoidance != Vector3.Zero)
			{
				steeringForce += obstacleAvoidance;
			}
			else
			{
				// otherwise consider avoiding collisions with others
				Vector3 collisionAvoidance = Vector3.Zero;
				const float caLeadTime = 3;

				// find all neighbors within maxRadius using proximity database
				// (radius is largest distance between vehicles traveling head-on
				// where a collision is possible within caLeadTime seconds.)
				float maxRadius = caLeadTime * MaxSpeed * 2;
				neighbors.Clear();
				proximityToken.FindNeighbors(Position, maxRadius, ref neighbors);

                if (neighbors.Count > 0 && leakThrough < StaticRandom.Random())
					collisionAvoidance = SteerToAvoidNeighbors(caLeadTime, neighbors) * 10;

				// if collision avoidance is needed, do it
				if (collisionAvoidance != Vector3.Zero)
				{
					steeringForce += collisionAvoidance;
				}
				else
				{
					// add in wander component (according to user switch)
					    if (Globals.WanderSwitch)
						steeringForce += SteerForWander(elapsedTime);

					// do (interactively) selected type of path following
					const float pfLeadTime = 3;
					Vector3 pathFollow =
						(Globals.UseDirectedPathFollowing ?
						 SteerToFollowPath(pathDirection, pfLeadTime, hpath.PolyPath) :
						 SteerToStayOnPath(pfLeadTime, hpath.PolyPath));

					// add in to steeringForce
					steeringForce += pathFollow * 0.5f;
				}
			}

			// return steering constrained to global XZ "ground" plane
            steeringForce.Y = 0;
			return steeringForce;
		}

		// switch to new proximity database -- just for demo purposes
		public void NewPD(ProximityDatabase pd)
		{
			// delete this boid's token in the old proximity database
			if (proximityToken != null)
			{
				proximityToken.Dispose();
				proximityToken = null;
			}

			// allocate a token for this boid in the proximity database
			proximityToken = pd.AllocateToken(this);
		}

		// a pointer to this boid's interface object for the proximity database
		ProximityToken proximityToken;

		// allocate one and share amoung instances just to save memory usage
		// (change to per-instance allocation to be more MP-safe)
		static List<IVehicle> neighbors = new List<IVehicle>();


		// direction for path following (upstream or downstream)
		int pathDirection;
	}
}
