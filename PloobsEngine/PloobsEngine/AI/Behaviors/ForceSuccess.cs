using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BehaviorTrees
{
	/// <summary>
	/// Executing this node will always succeed no matter what will be the
	/// results of the decorated node execution.
	/// </summary>
	/// <typeparam name="E"></typeparam>
	public class ForceSuccess<E> : Decorator<E> where E:Entity
	{
		public ForceSuccess(Node<E> node)
			: base(node)
		{
		}
		/// <summary>
		/// Creates task that will execute decorated node, but force returning success.
		/// </summary>
		public override Task<E> Evaluate(E entity)
		{
			return new ForceSuccessTask<E>(node, entity);
		}
	}
	/// <summary>
	/// Task responsible for executing ForceSuccess node.
	/// </summary>
	/// <typeparam name="E"></typeparam>
	class ForceSuccessTask<E> : DecoratorTask<E> where E : Entity
	{
		public ForceSuccessTask(Node<E> node, E entity)
			: base(node, entity)
		{
		}
		/// <summary>
		/// Executes decorated node's task, but forces returning success.
		/// </summary>
		/// <returns>TaskResult.Failure</returns>
		public override TaskResult Execute(E entity, GameTime gameTime)
		{
			task.Execute(entity, gameTime);
			return TaskResult.Success;
		}
	}
}
