using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BehaviorTrees
{
	// note that code below is exactly the same as for actions
	// this is because conditions are passing back results
	// via TaskResult and this is making them the same as actions.
	// we need however somehow tell the difference between them
    // that is why i use different delegate types, nodes and task

    /// <summary>
    /// Delegate for condition, i.e. a method that should not have side effects
    /// but the result is important cause it checks whether condition is met or not.
    /// </summary>
    /// <typeparam name="E">Type of the entity.</typeparam>
    /// <param name="entity">Entity object whhose method is refered..</param>
    /// <returns></returns>
	public delegate TaskResult ConditionDelegate<E>(E entity, GameTime gameTime) where E : Entity;

	/// <summary>
	/// Node that can check some condition.
	/// </summary>
    /// <typeparam name="E">Entity type.</typeparam>
	public abstract class Condition<E> : Node<E> where E : Entity
	{
		ConditionDelegate<E> condition;

        public Condition(ConditionDelegate<E> condition)
			: base()
        {
            this.condition = condition;
        }

		public override Task<E> Evaluate(E entity)
        {
            return new ConditionTask<E>(condition);
        }
	}
	/// <summary>
	/// Task responsible for executing a condition node.
	/// </summary>
	/// <typeparam name="E">Entity type.</typeparam>
	class ConditionTask<E> : Task<E> where E : Entity
	{
		ConditionDelegate<E> condition;
		/// <summary>
		/// Instantiates condition task with a given condition delegate method.
		/// </summary>
		/// <param name="entity">Entity used in execution.</param>
		/// <param name="condition">Condition method.</param>
		public ConditionTask(ConditionDelegate<E> condition)
			: base()
		{
			this.condition = condition;
		}
		/// <summary>
		/// Calls method bound via action delegate.
		/// </summary>
		/// <returns></returns>
		public override TaskResult Execute(E entity, GameTime gameTime)
		{
			return condition(entity, gameTime);
		}
	}
}
