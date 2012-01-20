using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BehaviorTrees
{
	/// <summary>
	/// Executing this node will always fails no matter what are the
	/// results of the decorated node execution.
	/// </summary>
	/// <typeparam name="E"></typeparam>
	public class ForceFailure<E> : Decorator<E> where E : Entity
	{
		public ForceFailure(Node<E> node)
			: base(node)
		{
		}
		/// <summary>
		/// Creates task that will execute the subnode but will always return failure.
		/// </summary>
		public override Task<E> Evaluate(E entity)
		{
			return new ForceFailureTask<E>(node, entity);
		}
	}
	/// <summary>
	/// Task responsible for executing ForceFailure node.
	/// </summary>
	/// <typeparam name="E"></typeparam>
	class ForceFailureTask<E> : DecoratorTask<E> where E : Entity
	{
		public ForceFailureTask(Node<E> node, E entity)
			: base(node, entity)
		{
		}
		/// <summary>
		/// Executes decorated node's task, but forces returning failure.
		/// </summary>
		/// <returns>TaskResult.Failure</returns>
		public override TaskResult Execute(E entity, GameTime gameTime)
		{
			task.Execute(entity, gameTime);
			return TaskResult.Failure;
		}
	}
}
