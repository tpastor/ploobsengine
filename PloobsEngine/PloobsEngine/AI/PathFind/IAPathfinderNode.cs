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
using System.Text;

namespace PloobsEngine.IA
{
    #if !WINDOWS_PHONE
    [Serializable]
#endif
    public class IAPathfinderNode
    {
        public float F;
        public float G;
        public float H;  // f = gone + heuristic
        public float X;
        public float Y;
        public float Z;
        public float PX; // Parent
        public float PY;
        public float PZ;
        public int parentId;
    }
}
