#if WINDOWS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Physics;
using StillDesign.PhysX;
using PloobsEngine.Engine.Logger;
using PloobsEngine.Trigger;
using PloobsEngine.SceneControl;

namespace PloobsEngine.Physics
{
    public class PhysxTrigger : ITrigger
    {

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
        public const string FireContaining = "FBC";
        
        public PhysxTrigger(ShapeDescription ShapeDescription, Matrix worldTransformation, TriggerEvent evt,
            String Name = null,bool FireOnBeginsTouching = false,bool FireOnEndsTouching = false,bool FireOnContaining = false)
        {
            this.Name = Name;
            this.FireOnBeginsTouching = FireOnBeginsTouching;
            this.FireOnEndsTouching = FireOnEndsTouching;
            this.FireOnContaining = FireOnContaining;
            ShapeDescription.Flags |= ShapeFlag.TriggerEnable | ShapeFlag.TriggerOnEnter | ShapeFlag.TriggerOnLeave | ShapeFlag.TriggerOnStay;
            ShapeDescription.UserData = this;
            Physics.PhysxPhysicObject obj = new Physics.PhysxPhysicObject(ShapeDescription, worldTransformation, Vector3.One);
            obj.BeTrigger();
            GhostObject = obj;
            this.Event = evt;            
        }

        public bool FireOnBeginsTouching = false;
        public bool FireOnEndsTouching = false;
        public bool FireOnContaining = false;
        

        #region ITrigger Members

        public IObject ContactEntity
        {
            get;
            set;
        }

        internal void fireEvent(TriggerFlag status,IPhysicObject obj)
        {
            switch (status)
            {
                case TriggerFlag.OnEnter:
                    if (FireOnBeginsTouching)
                    {
                        ContactEntity = obj.ObjectOwner;
                        Event.Code = FireBeginsTouching;
                        Event.FireEvent(this);
                    }
                    break;
                case TriggerFlag.OnLeave:
                    if (FireOnEndsTouching)
                    {
                        ContactEntity = obj.ObjectOwner;
                        Event.Code = FireEndsTouching;
                        Event.FireEvent(this);
                    }
                    break;
                case TriggerFlag.OnStay:
                    if (FireOnContaining)
                    {
                        ContactEntity = obj.ObjectOwner;
                        Event.Code = FireContaining;
                        Event.FireEvent(this);
                    }
                    break;
                default:
                    break;
            }
        }

        public Events.IEvent<ITrigger> Event
        {
            get;
            set;

        }

        public string Name
        {
            get;
            set;
        }

        public IPhysicObject GhostObject
        {
            set;
            get;
        }

        #endregion
    }
}
#endif