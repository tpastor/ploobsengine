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
using Microsoft.Xna.Framework.Graphics;

namespace Bnoerj.AI.Steering
{
	/// <summary>
	/// Pathway: a pure virtual base class for an abstract pathway in space, as for
	/// example would be used in path following.
	/// </summary>
	public abstract class Pathway
	{
		// Given an arbitrary point ("A"), returns the nearest point ("P") on
		// this path.  Also returns, via output arguments, the path tangent at
		// P and a measure of how far A is outside the Pathway's "tube".  Note
		// that a negative distance indicates A is inside the Pathway.
        public abstract Vector3 MapPointToPath(Vector3 point, out Vector3 tangent, out float outside);

		// given a distance along the path, convert it to a point on the path
        public abstract Vector3 MapPathDistanceToPoint(float pathDistance);

		// Given an arbitrary point, convert it to a distance along the path.
        public abstract float MapPointToPathDistance(Vector3 point);

		// is the given point inside the path tube?
        public bool IsInsidePath(Vector3 point)
		{
			float outside;
			Vector3 tangent;
			MapPointToPath(point, out tangent, out outside);
			return outside < 0;
		}

		// how far outside path tube is the given point?  (negative is inside)
        public float HowFarOutsidePath(Vector3 point)
		{
			float outside;
			Vector3 tangent;
			MapPointToPath(point, out tangent, out outside);
			return outside;
		}
	}
}
