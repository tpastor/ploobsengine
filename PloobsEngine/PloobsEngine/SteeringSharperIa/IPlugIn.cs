// Copyright (c) 2002-2003, Sony Computer Entertainment America
// Copyright (c) 2002-2003, Craig Reynolds <craig_reynolds@playstation.sony.com>
// Copyright (C) 2007 Bjoern Graf <bjoern.graf@gmx.net>
// All rights reserved.
//
// This software is licensed as described in the file license.txt, which
// you should have received as part of this distribution. The terms
// are also available at http://www.codeplex.com/SharpSteer/Project/License.aspx.

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace Bnoerj.AI.Steering
{
	public interface IPlugIn
	{		
		void Update();
		void Init();
		
		// return a pointer to this instance's character string name
		String Name { get; }

		// return an AVGroup (an STL vector of AbstractVehicle pointers) of
		// all vehicles(/agents/characters) defined by the PlugIn
		List<IVehicle> Vehicles { get; }
	}
}
