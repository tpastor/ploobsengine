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
using Microsoft.Xna.Framework;
using PloobsEngine.Physic2D;

namespace PloobsEngine.SceneControl._2DScene.Culler
{
    /// <summary>
    /// 2D physic based Culler
    /// Very effective
    /// </summary>
    public class Physic2DCuller : I2DCuller
    {
        List<I2DObject> ghosts = new List<I2DObject>();
        int rend = 0;
        public Physic2DCuller()
        {
            MaterialSortedObjects = new Dictionary<Type, List<I2DObject>>();
        }

        public override Dictionary<Type, List<I2DObject>> GetNotCulledObjectsList()
        {
            return MaterialSortedObjects;
        }

        public override void StartFrame(Microsoft.Xna.Framework.Matrix view, Microsoft.Xna.Framework.Matrix projection, Microsoft.Xna.Framework.BoundingFrustum frustrum)
        {
            rend = 0;
            MaterialSortedObjects.Clear();
            
            Vector3[] corners = new Vector3[8];
            frustrum.GetCorners(corners);            
            List<I2DPhysicObject> objs = world.PhysicWorld.TestAABB(new Vector2(corners[0].X, corners[0].Y), new Vector2(corners[2].X, corners[2].Y));

            foreach (var item in objs)
            {
                I2DObject obj = item.Owner;
                if (!MaterialSortedObjects.ContainsKey(obj.Material.GetType()))
                {
                    MaterialSortedObjects[obj.Material.GetType()] = new List<I2DObject>();
                }                

                MaterialSortedObjects[obj.Material.GetType()].Add(obj);
                rend++;
            }

            foreach (var obj  in ghosts)
            {
                if (!MaterialSortedObjects.ContainsKey(obj.Material.GetType()))
                {
                    MaterialSortedObjects[obj.Material.GetType()] = new List<I2DObject>();
                }
                MaterialSortedObjects[obj.Material.GetType()].Add(obj);                
            }
            rend += ghosts.Count;
        }

        public override void onObjectAdded(I2DObject obj)
        {
            switch (obj.PhysicObject.Physic2DType)
            {
                case Physic2DType.Ghost:
                    ghosts.Add(obj);
                    break;                
                default:
                    break;
            }
        }

        public override void onObjectRemoved(I2DObject obj)
        {
            switch (obj.PhysicObject.Physic2DType)
            {
                case Physic2DType.Ghost:
                    ghosts.Remove(obj);
                    break;
                default:
                    break;
            }
        }
        
        public override int RenderedObjectThisFrame
        {
            get { return rend; }
        }

        private Dictionary<Type, List<I2DObject>> MaterialSortedObjects
        {
            get;
            set;
        }
        
    }
}