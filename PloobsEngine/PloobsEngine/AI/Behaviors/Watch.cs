using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BehaviorTrees
{
	/// <summary>
	/// Watch decorator is a node which executes its subnode unless some condition is met.
	/// </summary>
	/// <typeparam name="E"></typeparam>
	public class Watch<E> : Decorator<E> where E : Entity
	{
		ConditionDelegate<E> condition;

		/// <summary>
		/// Instantiates watch decorator with a given condition
		/// </summary>
		/// <param name="condition"></param>
		public Watch(ConditionDelegate<E> condition, Node<E> node)
			: base(node)
		{
			this.condition = condition;
		}

		/// <summary>
		/// Checks the condition, and if it doesn't met then executes subnode, otherwise
		/// force success.
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="gameTime"></param>
		/// <returns></returns>
		public override Task<E> Evaluate(E entity)
		{
			return new WatchTask<E>(condition, node, entity);
		}
	}
	/// <summary>
	/// Task responsible for running Watch node.
	/// </summary>
	/// <typeparam name="E"></typeparam>
	class WatchTask<E> : DecoratorTask<E> where E : Entity
	{
		ConditionDelegate<E> condition;

		public WatchTask(ConditionDelegate<E> condition, Node<E> node, E entity)
			: base(node, entity)
		{
			this.condition = condition;
		}
		/// <summary>
		/// Checks the condition, and if it doesn't met then executes subnode's task, otherwise
		/// force success.
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="gameTime"></param>
		/// <returns></returns>
		public override TaskResult Execute(E entity, GameTime gameTime)
		{
			if (condition(entity, gameTime) == TaskResult.Success)
				return TaskResult.Success;
			else
			{
				// TODO: here's an example of late evaluation which could preserve some memory
				//task = node.Evaluate(entity);
				// TODO: or maybe return only failure, otherwise running?!?
				return task.Execute(entity, gameTime);
			}
		}
	}
}
