using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BehaviorTrees
{
    /// <summary>
    /// Delegate for methods that will be used as actions.
    /// </summary>
    /// <typeparam name="E">Type of the entity.</typeparam>
    /// <param name="entity">Entity on which this method will be executed.</param>
    /// <returns></returns>
	public delegate TaskResult ActionDelegate<E>(E entity, GameTime gameTime) where E : Entity;

	/// <summary>
	/// Node that can perform some action.
	/// </summary>
    /// <typeparam name="E">Entity type.</typeparam>
	public class Action<E> : Node<E> where E : Entity
	{
		ActionDelegate<E> method;

        public Action(ActionDelegate<E> method)
			: base()
        {
            this.method = method;
        }
		/// <summary>
		/// Creates task that will be executing associated action method.
		/// </summary>
        public override Task<E> Evaluate(E entity)
        {
            return new ActionTask<E>(method);
        }
	}
	/// <summary>
	/// Task that is responsible for running the action on an entity.
	/// </summary>
	/// <typeparam name="E">Entity type/</typeparam>
	public class ActionTask<E> : Task<E> where E : Entity
	{
		ActionDelegate<E> method;

		/// <summary>
		/// Instantiates action task with a given method to be called.
		/// </summary>
		/// <param name="method">Delegate for a method to be called.</param>
		public ActionTask(ActionDelegate<E> method)
			: base()
		{
			this.method = method;
		}
		/// <summary>
		/// Calls method bound via action delegate.
		/// </summary>
		/// <returns></returns>
		public override TaskResult Execute(E entity, GameTime gameTime)
		{
			return method(entity, gameTime);
		}
	}
}
