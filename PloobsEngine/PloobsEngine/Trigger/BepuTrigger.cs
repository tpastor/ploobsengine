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

        public const string FireBeginsTouching = "FBT";
        public const string FireEndsTouching = "FET";
        public const string FireBeginsContaining = "FBC";
        public const string FireEndsContaining = "FEC";

        /// <summary>
        /// Trigger creation
        /// </summary>
        /// <param name="triangleMesh">Triangle mesh representing the Volume of the Trigger</param>
        /// <param name="evt">Event to be fired</param>
        /// <param name="fireBeginsTouching">if set to <c>true</c> [fire begins touching].</param>
        /// <param name="fireEndsTouching">if set to <c>true</c> [fire ends touching].</param>
        /// <param name="fireBeginsContaining">if set to <c>true</c> [fire begins containing].</param>
        /// <param name="fireEndsContaining">if set to <c>true</c> [fire ends containing].</param>        
        public BepuTrigger(TriangleMeshObject triangleMesh, TriggerEvent evt, bool fireBeginsTouching, bool fireEndsTouching, bool fireBeginsContaining, bool fireEndsContaining)
        {
            this.volumeobject = new DetectorVolumeObject(triangleMesh);
            DetectorVolume detectorVolume = this.volumeobject.DetectorVolume;
            if(fireBeginsTouching)
                detectorVolume.EntityBeginsTouching += new EntityBeginsTouchingVolumeEventHandler(detectorVolume_EntityBeginsTouching);
            if(fireEndsTouching)
                detectorVolume.EntityStopsTouching += new EntityStopsTouchingVolumeEventHandler(detectorVolume_EntityStopsTouching);
            if(fireBeginsContaining)
                detectorVolume.VolumeBeginsContainingEntity += new VolumeBeginsContainingEntityEventHandler(detectorVolume_VolumeBeginsContainingEntity);
            if(fireEndsContaining)
                detectorVolume.VolumeStopsContainingEntity += new VolumeStopsContainingEntityEventHandler(detectorVolume_VolumeStopsContainingEntity);            
            this.evt = evt;
        }        

        void detectorVolume_VolumeStopsContainingEntity(DetectorVolume volume, BEPUphysics.Entities.Entity entity)
        {
            contactEntity = (entity.Tag as IPhysicObject).ObjectOwner;
            evt.Code = FireEndsContaining;            
            evt.FireEvent(this);
        }

        void detectorVolume_VolumeBeginsContainingEntity(DetectorVolume volume, BEPUphysics.Entities.Entity entity)
        {
            contactEntity = (entity.Tag as IPhysicObject).ObjectOwner;
            evt.Code = FireBeginsContaining;
            evt.FireEvent(this);
        }

        void detectorVolume_EntityStopsTouching(BEPUphysics.Entities.Entity toucher, DetectorVolume volume)
        {
            contactEntity = (toucher.Tag as IPhysicObject).ObjectOwner;
            evt.Code = FireEndsTouching;
            evt.FireEvent(this);
        }

        void detectorVolume_EntityBeginsTouching(BEPUphysics.Entities.Entity toucher, DetectorVolume volume)
        {
            contactEntity = (toucher.Tag as IPhysicObject).ObjectOwner;
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
