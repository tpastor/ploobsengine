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
    /// Dummy 2D culler system 
    /// </summary>
    public class Simple2DCuller : I2DCuller
    {        
        public Simple2DCuller()
        {
            MaterialSortedObjects = new Dictionary<Type, List<I2DObject>>();
        }

        public override Dictionary<Type, List<I2DObject>> GetNotCulledObjectsList()
        {
            return MaterialSortedObjects;
        }

        public override void StartFrame(Microsoft.Xna.Framework.Matrix view, Microsoft.Xna.Framework.Matrix projection, Microsoft.Xna.Framework.BoundingFrustum frustrum)
        {            
            
            
        }

        public override void onObjectAdded(I2DObject obj)
        {
            if (!MaterialSortedObjects.ContainsKey(obj.Material.GetType()))
            {
                MaterialSortedObjects[obj.Material.GetType()] = new List<I2DObject>();
            }
            MaterialSortedObjects[obj.Material.GetType()].Add(obj);                
        }

        public override void onObjectRemoved(I2DObject obj)
        {            
            MaterialSortedObjects[obj.Material.GetType()].Remove(obj);                
        }
        
        public override int RenderedObjectThisFrame
        {
            get { return world.Objects.Count; }
        }

        private Dictionary<Type, List<I2DObject>> MaterialSortedObjects
        {
            get;
            set;
        }
        
    }
}