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
	public interface IVehicle : ILocalSpace
	{
        // mass (defaults to unity so acceleration=force)
		float Mass { get; set; }

        // size of bounding sphere, for obstacle avoidance, etc.
		float Radius { get; set; }

        // velocity of vehicle
        Vector3 Velocity { get; }

		/// <summary>
		/// Gets the acceleration of the vehicle.
		/// </summary>
		Vector3 Acceleration { get; }
		
		// speed of vehicle (may be faster than taking magnitude of velocity)
		float Speed { get; set; }

        // predict position of this vehicle at some time in the future
        //(assumes velocity remains constant)
        Vector3 PredictFuturePosition(float predictionTime);

        // ----------------------------------------------------------------------
        // XXX this vehicle-model-specific functionality functionality seems out
        // XXX of place on the abstract base class, but for now it is expedient

        // the maximum steering force this vehicle can apply
		float MaxForce { get; set; }

        // the maximum speed this vehicle is allowed to move
		float MaxSpeed { get; set; }
	}
}
