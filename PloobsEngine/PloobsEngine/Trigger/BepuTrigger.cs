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
using BEPUphysics;
using PloobsEngine.Physics;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.Events;
using PloobsEngine.SceneControl;
using BEPUphysics.UpdateableSystems;

#if MONO || MONODX
    using DetectorVolume = BEPUphysics.Collidables.DetectorVolume;
#endif


namespace PloobsEngine.Trigger
{
    /// <summary>
    /// Bepu Implementation of a trigger
    /// </summary>
    public class BepuTrigger : ITrigger
    {   
        private DetectorVolumeObject volumeobject;
        private IObject contactEntity;
        private TriggerEvent evt;

        /// <summary>
        /// FireBeginsTouching 
        /// </summary>
        public const string FireBeginsTouching = "FBT";
        /// <summary>
        /// FireEndsTouching 
        /// </summary>
        public const string FireEndsTouching = "FET";
        /// <summary>
        /// FireBeginsContaining 
        /// </summary>
        public const string FireBeginsContaining = "FBC";
        /// <summary>
        /// FireEndsContaining
        /// </summary>
        public const string FireEndsContaining = "FEC";

        /// <summary>
        /// Trigger creation
        /// </summary>
        /// <param name="physicWorld">The physic world.</param>
        /// <param name="triangleMesh">Triangle mesh representing the Volume of the Trigger</param>
        /// <param name="evt">Event to be fired</param>
        /// <param name="fireBeginsTouching">if set to <c>true</c> [fire begins touching].</param>
        /// <param name="fireEndsTouching">if set to <c>true</c> [fire ends touching].</param>
        /// <param name="fireBeginsContaining">if set to <c>true</c> [fire begins containing].</param>
        /// <param name="fireEndsContaining">if set to <c>true</c> [fire ends containing].</param>
#if !MONO && !MONODX
        public BepuTrigger(BepuPhysicWorld physicWorld, TriangleMeshObject triangleMesh, TriggerEvent evt, bool fireBeginsTouching, bool fireEndsTouching, bool fireBeginsContaining, bool fireEndsContaining)
        {        
            this.volumeobject = new DetectorVolumeObject(physicWorld,triangleMesh);            
            DetectorVolume detectorVolume = this.volumeobject.DetectorVolume;
            if(fireBeginsTouching)
                detectorVolume.EntityBeginsTouching += new EntityBeginsTouchingVolumeEventHandler(detectorVolume_EntityBeginsTouching);
            if(fireEndsTouching)
                detectorVolume.EntityStopsTouching += new EntityStopsTouchingVolumeEventHandler(detectorVolume_EntityStopsTouching);
            if(fireBeginsContaining)
                detectorVolume.VolumeBeginsContainingEntity += new VolumeBeginsContainingEntityEventHandler(detectorVolume_VolumeBeginsContainingEntity);
            if(fireEndsContaining)
                detectorVolume.VolumeStopsContainingEntity += new VolumeStopsContainingEntityEventHandler(detectorVolume_VolumeStopsContainingEntity);            
#else
        public BepuTrigger(TriangleMeshObject triangleMesh, TriggerEvent evt, bool fireBeginsTouching, bool fireEndsTouching, bool fireBeginsContaining, bool fireEndsContaining)
        {        
            this.volumeobject = new DetectorVolumeObject(triangleMesh);            
            BEPUphysics.Collidables.DetectorVolume detectorVolume = this.volumeobject.DetectorVolume;
            if(fireBeginsTouching)
                detectorVolume.EntityBeganTouching += detectorVolume_EntityBeginsTouching;
            if(fireEndsTouching)
                detectorVolume.EntityStoppedTouching+= detectorVolume_EntityStopsTouching;
            if(fireBeginsContaining)
                detectorVolume.VolumeBeganContainingEntity+=  detectorVolume_VolumeBeginsContainingEntity;
            if(fireEndsContaining)
                detectorVolume.VolumeStoppedContainingEntity += detectorVolume_VolumeStopsContainingEntity;            
            
#endif

            this.evt = evt;
        }        

        void detectorVolume_VolumeStopsContainingEntity(DetectorVolume volume, BEPUphysics.Entities.Entity entity)
        {
            contactEntity = BepuEntityObject.RecoverObjectFromEntity(entity);
            evt.Code = FireEndsContaining;            
            evt.FireEvent(this);
        }

        void detectorVolume_VolumeBeginsContainingEntity(DetectorVolume volume, BEPUphysics.Entities.Entity entity)
        {
            contactEntity = BepuEntityObject.RecoverObjectFromEntity(entity);
            evt.Code = FireBeginsContaining;
            evt.FireEvent(this);
        }

#if !MONO && !MONODX
        void detectorVolume_EntityStopsTouching(BEPUphysics.Entities.Entity toucher, DetectorVolume volume)
#else
        void detectorVolume_EntityStopsTouching(DetectorVolume volume,BEPUphysics.Entities.Entity toucher)
#endif
        {
            contactEntity = BepuEntityObject.RecoverObjectFromEntity(toucher);
            evt.Code = FireEndsTouching;
            evt.FireEvent(this);
        }

#if !MONO && !MONODX
        void detectorVolume_EntityBeginsTouching(BEPUphysics.Entities.Entity toucher, DetectorVolume volume)
#else
        void detectorVolume_EntityBeginsTouching(DetectorVolume volume,BEPUphysics.Entities.Entity toucher)
#endif
        
        {
            contactEntity = BepuEntityObject.RecoverObjectFromEntity(toucher);
            evt.Code = FireBeginsTouching;
            evt.FireEvent(this);
        }

        #region ITrigger Members

        /// <summary>
        /// Gets the contact entity.
        /// </summary>
        public IObject ContactEntity
        {
            get { return contactEntity; }            
        }

        /// <summary>
        /// Gets or sets the name of the trigger.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get;
            set;

        }

        #endregion

        #region ITrigger Members

        /// <summary>
        /// Gets or sets the event fired when the trigger is activated.
        /// </summary>
        /// <value>
        /// The event.
        /// </value>
        public IEvent<ITrigger> Event
        {
            get
            {
                return evt;
            }
            set
            {
                this.evt = value as TriggerEvent;
            }
        }

        #endregion

        #region ITrigger Members


        /// <summary>
        /// Gets or sets the ghost object that gives the SHAPE of the trigger.
        /// </summary>
        /// <value>
        /// The ghost object.
        /// </value>
        public IPhysicObject GhostObject
        {
            get
            {
                return volumeobject;
            }
            set
            {
                this.volumeobject = value as DetectorVolumeObject;
            }
        }

        #endregion
    }
}
