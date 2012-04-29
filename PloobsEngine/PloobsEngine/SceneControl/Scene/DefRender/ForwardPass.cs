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
using PloobsEngine.Material;
using PloobsEngine.Cameras;

namespace PloobsEngine.SceneControl
{
    

    public struct ForwardPassDescription
    {
        public ForwardPassDescription(bool ForwardDrawPass, bool ForwarPosDrawPass, bool DeferredPosDrawPass, bool DeferredSortByCameraDistance, bool ForwardSortByCameraDistance)
        {
            this.ForwardDrawPass = ForwardDrawPass;
            this.ForwarPosDrawPass = ForwarPosDrawPass;
            this.DeferredPosDrawPass = DeferredPosDrawPass;
            this.ForwardSortByCameraDistance = ForwardSortByCameraDistance;
            this.DeferredSortByCameraDistance = DeferredSortByCameraDistance;
        }

        public static ForwardPassDescription Default()
        {
            return new ForwardPassDescription(true, true, true,false,false);            
        }

        public bool ForwardDrawPass;
        public bool ForwarPosDrawPass;
        public bool DeferredPosDrawPass;
        public bool ForwardSortByCameraDistance;
        public bool DeferredSortByCameraDistance;

    }


    public class ForwardPass : IForwardPass
    {
        ForwardPassDescription ForwardPassDescription;

        public ForwardPassDescription GetForwardPassDescription()
        {
            return ForwardPassDescription;
        }

        public void ApplyForwardPassDescription(ForwardPassDescription desc)
        {
            this.ForwardPassDescription = desc;
        }

        public ForwardPass(ForwardPassDescription ForwardPassDescription)
        {
            this.ForwardPassDescription = ForwardPassDescription;
        }

        ComparerBackToFront c = new ComparerBackToFront();

        public void Draw(GameTime gt, IWorld world, RenderHelper render, List<IObject> deferred, List<IObject> forward)        
        {            
            if (ForwardPassDescription.DeferredPosDrawPass)
            {
                
                if (ForwardPassDescription.DeferredSortByCameraDistance)
                {
                    c.CameraPosition = world.CameraManager.ActiveCamera.Position;
                    deferred.Sort(c);
                }

                foreach (IObject item in deferred)
                {
                    if(item.Material.IsVisible)
                        item.Material.PosDrawnPhase(gt, item, world.CameraManager.ActiveCamera, world.Lights, render);
                }
            }

            if (ForwardPassDescription.ForwardDrawPass || ForwardPassDescription.ForwarPosDrawPass)
            {                
                if (ForwardPassDescription.ForwardSortByCameraDistance)
                {
                    c.CameraPosition = world.CameraManager.ActiveCamera.Position;
                    forward.Sort(c);
                }

                if (ForwardPassDescription.ForwardDrawPass)
                {
                    foreach (IObject item in forward)
                    {
                        item.Material.Drawn(gt, item, world.CameraManager.ActiveCamera, world.Lights, render);
                    }
                }

                if (ForwardPassDescription.ForwarPosDrawPass)
                {
                    foreach (IObject item in forward)
                    {
                        item.Material.PosDrawnPhase(gt, item, world.CameraManager.ActiveCamera, world.Lights, render);
                    }
                }
            }

        }
        
    }
}
