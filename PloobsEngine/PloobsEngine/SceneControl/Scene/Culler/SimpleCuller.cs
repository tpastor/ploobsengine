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
using System.Linq;
using System.Text;
using PloobsEngine.Physics;
using PloobsEngine.Material;
using PloobsEngine.Utils;
using PloobsEngine.Cameras;
//using PloobsEngine.Draw;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Culler that uses the bepu scene control
    /// </summary>
    public class SimpleCuller : ICuller
    {
        public SimpleCuller()
        {            
        }       

        #region ICuller Members
        List<IObject> deferred = new List<IObject>();
        List<IObject> forward = new List<IObject>();
        public override void StartFrame(Matrix view, Matrix projection, BoundingFrustum frustrum)
        {
            forward.Clear();
            deferred.Clear();
            
            foreach (var item in world.Objects)
            {
                if (item.Material.MaterialType == MaterialType.DEFERRED)
                {
                    if (item.PhysicObject.PhysicObjectTypes == PhysicObjectTypes.GHOST || item.PhysicObject.PhysicObjectTypes == PhysicObjectTypes.NONE)
                    {
                        deferred.Add(item);
                        continue;
                    }

                    if (frustrum.Contains(item.PhysicObject.BoundingBox) != Microsoft.Xna.Framework.ContainmentType.Disjoint)
                    {
                        deferred.Add(item);
                    }
                }
                else
                {
                    if (item.PhysicObject.PhysicObjectTypes == PhysicObjectTypes.GHOST || item.PhysicObject.PhysicObjectTypes == PhysicObjectTypes.NONE)
                    {
                        forward.Add(item);
                        continue;
                    }

                    if (frustrum.Contains(item.PhysicObject.BoundingBox) != Microsoft.Xna.Framework.ContainmentType.Disjoint)
                    {
                        forward.Add(item);
                    }    
                }
            }
            num = forward.Count + deferred.Count;            
        }

        public override List<IObject> GetNotCulledObjectsList(MaterialType? Filter)
        {
            if (Filter == PloobsEngine.Material.MaterialType.DEFERRED)
                return deferred.ToList();
            else if (Filter == PloobsEngine.Material.MaterialType.FORWARD)
                return forward.ToList();
            else
            {
                List<IObject> objs = new List<IObject>();
                objs.AddRange(deferred);
                objs.AddRange(forward);
                return objs;
            }
        }

        public override void onObjectAdded(IObject obj)
        {
            
        }

        public override void onObjectRemoved(IObject obj)
        {
            
        }

        #endregion

        #region ICuller Members
        int num;
        public override int RenderedObjectThisFrame
        {
            get { return num; }
        }

        #endregion        
    }
}
