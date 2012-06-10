using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.IA
{
    /// <summary>
    /// TriggerMultiCondition 
    /// Checks for more than one condition to fire
    /// </summary>
    public class TriggerMultiCondition : Trigger
    {
        Func<BlackBoard, TriggerState, bool> Condition;
        Action<TriggerState> onFire;
        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerMultiCondition"/> class.
        /// </summary>
        /// <param name="Condition">The condition.</param>
        /// <param name="onFire">The on fire.</param>
        /// <param name="Names">The names.</param>
        public TriggerMultiCondition(Func<BlackBoard, TriggerState, bool> Condition, Action<TriggerState> onFire = null, params String[] Names)
        {
            this.WorldPropertiesMonitored.AddRange(Names);
            this.Condition = Condition;
            this.onFire = onFire;
        }

        /// <summary>
        /// Removes this trigger form the blackboard
        /// </summary>
        public void RemoveThisTrigger()
        {
            BlackBoard.RemoveTrigger(this);
        }

        /// <summary>
        /// Checks the conditions to fire.
        /// </summary>
        /// <returns></returns>
        protected override bool CheckConditionToFire()
        {
            return Condition(BlackBoard, TriggerState);
        }

        /// <summary>
        /// Called on triggers fire.
        /// </summary>
        /// <param name="TriggerState">State of the trigger.</param>
        protected override void CalledOnFire(TriggerState TriggerState)
        {
            if (onFire != null)
                onFire(TriggerState);
        }
    }
}
