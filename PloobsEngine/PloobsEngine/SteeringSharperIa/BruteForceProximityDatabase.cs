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
	public class BruteForceProximityDatabase<ContentType> : IProximityDatabase<ContentType>
		where ContentType : class
	{
		// "token" to represent objects stored in the database
		public class TokenType : ITokenForProximityDatabase<ContentType>
		{
			BruteForceProximityDatabase<ContentType> bfpd;
			ContentType obj;
            Vector3 position;

			// constructor
			public TokenType(ContentType parentObject, BruteForceProximityDatabase<ContentType> pd)
			{
				// store pointer to our associated database and the obj this
				// token represents, and store this token on the database's vector
				bfpd = pd;
				obj = parentObject;
				bfpd.group.Add(this);
			}

			// destructor
			//FIXME: need to move elsewhere
			//~TokenType()
			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}
			protected virtual void Dispose(bool disposing)
			{
				if (obj != null)
				{
					// remove this token from the database's vector
                    //DANGEROUS LINE !!!
					bfpd.group.Remove(this);
					obj = null;
				}
			}

			// the client obj calls this each time its position changes
            public void UpdateForNewPosition(Vector3 newPosition)
			{
				position = newPosition;
			}

			// find all neighbors within the given sphere (as center and radius)
            public void FindNeighbors(Vector3 center, float radius, ref List<ContentType> results)
			{
				// loop over all tokens
				float r2 = radius * radius;
				for (int i = 0; i < bfpd.group.Count; i++)
				{
                    Vector3 offset = center - bfpd.group[i].position;
					float d2 = offset.LengthSquared();

					// push onto result vector when within given radius
					if (d2 < r2) results.Add(bfpd.group[i].obj);
				}
			}
		}

		// Contains all tokens in database
		List<TokenType> group;

		// constructor
		public BruteForceProximityDatabase()
		{
			group = new List<TokenType>();
		}

		// allocate a token to represent a given client object in this database
		public ITokenForProximityDatabase<ContentType> AllocateToken(ContentType parentObject)
		{
			return new TokenType(parentObject, this);
		}

		// return the number of tokens currently in the database
		public int Count
		{
			get { return group.Count; }
		}
	}
}
