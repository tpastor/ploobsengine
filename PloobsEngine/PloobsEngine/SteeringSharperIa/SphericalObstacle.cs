#region License
/*
    PloobsEngine Game Engine Version 0.3 Beta
    Copyright (C) 2011  Ploobs

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Bnoerj.AI.Steering
{
	/// <summary>
	/// SphericalObstacle a simple concrete type of obstacle.
	/// </summary>
	public class SphericalObstacle : IObstacle
	{
		SeenFromState seenFrom;

		public float Radius;
        public Vector3 Center;

		// constructors
		public SphericalObstacle()
			: this(1, Vector3.Zero)
		{
		}
        public SphericalObstacle(float r, Vector3 c)
		{
			Radius = r;
			Center = c;
		}

		public SeenFromState SeenFrom
		{
			get { return seenFrom; }
			set { seenFrom = value; }
		}

		// XXX 4-23-03: Temporary work around (see comment above)
		//
		// Checks for intersection of the given spherical obstacle with a
		// volume of "likely future vehicle positions": a cylinder along the
		// current path, extending minTimeToCollision seconds along the
		// forward axis from current position.
		//
		// If they intersect, a collision is imminent and this function returns
		// a steering force pointing laterally away from the obstacle's center.
		//
		// Returns a zero vector if the obstacle is outside the cylinder
		//
		// xxx couldn't this be made more compact using localizePosition?

        public Vector3 SteerToAvoid(IVehicle v, float minTimeToCollision)
		{
			// minimum distance to obstacle before avoidance is required
			float minDistanceToCollision = minTimeToCollision * v.Speed;
			float minDistanceToCenter = minDistanceToCollision + Radius;

			// contact distance: sum of radii of obstacle and vehicle
			float totalRadius = Radius + v.Radius;

			// obstacle center relative to vehicle position
			Vector3 localOffset = Center - v.Position;

			// distance along vehicle's forward axis to obstacle's center
            float forwardComponent = Vector3.Dot(localOffset, v.Forward);
			Vector3 forwardOffset = v.Forward * forwardComponent;

			// offset from forward axis to obstacle's center
			Vector3 offForwardOffset = localOffset - forwardOffset;

			// test to see if sphere overlaps with obstacle-free corridor
			bool inCylinder = offForwardOffset.Length() < totalRadius;
			bool nearby = forwardComponent < minDistanceToCenter;
			bool inFront = forwardComponent > 0;

			// if all three conditions are met, steer away from sphere center
			if (inCylinder && nearby && inFront)
			{
				return offForwardOffset * -1;
			}
			else
			{
                return Vector3.Zero;
			}
		}
	}
}
