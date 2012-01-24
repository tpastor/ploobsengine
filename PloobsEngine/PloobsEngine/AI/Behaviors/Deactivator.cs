using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BehaviorTrees
{
	/// <summary>
	/// Doesn't execute decorated subnode, but return success.
	/// Useful for temporary disabling nodes.
	/// </summary>
	/// <typeparam name="E"></typeparam>
	public class Deactivator<E> : Decorator<E> where E : Entity
	{
		public Deactivator(Node<E> node)
			: base(node)
		{
		}
		/// <summary>
		/// Creates a task that will deactivate subnode.
		/// (Executing it will be always returning success).
		/// </summary>
		public override Task<E> Evaluate(E entity)
		{
			return new DeactivatorTask<E>(node, entity);
		}
	}
	/// <summary>
	/// Class responsible for running deactivator task which is simple
	/// TaskResult.Success returning.
	/// </summary>
	/// <typeparam name="E"></typeparam>
	class DeactivatorTask<E> : DecoratorTask<E> where E : Entity
	{
		public DeactivatorTask(Node<E> node, E entity)
			: base(node, entity)
		{
		}
		/// <summary>
		/// Doesn't execute node's task, but returns success.
		/// </summary>
		/// <returns>TaskResult.Success</returns>
		public override TaskResult Execute(E entity, GameTime gameTime)
		{
			return TaskResult.Success;
		}
	}
}
