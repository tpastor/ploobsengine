using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BehaviorTrees
{
	/// <summary>
	/// Execute decorated node but only specified amount of times.
	/// </summary>
	/// <typeparam name="E"></typeparam>
	public class Limiter<E> : Decorator<E> where E : Entity
	{
		int limit;

		/// <summary>
		/// Instantiates limiting decorator with a given limit amount.
		/// </summary>
		/// <param name="limit">Amount of times the subnode will be executed.</param>
		/// <param name="node"></param>
		public Limiter(int limit, Node<E> node)
			: base(node)
		{
			this.limit = limit;
		}
		/// <summary>
		/// Executes decorated node but stops after the count of execution
		/// will hit the limit. After that it returns success.
		/// </summary>
		/// <returns>TaskResult.Success</returns>
		public override Task<E> Evaluate(E entity)
		{
			return new LimiterTask<E>(limit, node, entity);
		}
	}
	/// <summary>
	/// Class responsible for running Limiter decorator.
	/// </summary>
	/// <typeparam name="E"></typeparam>
	class LimiterTask<E> : DecoratorTask<E> where E : Entity
	{
		int counter = 0;
		int limit;

		public LimiterTask(int limit, Node<E> node, E entity)
			: base(node, entity)
		{
			this.limit = limit;
			task = node.Evaluate(entity);
		}
		/// <summary>
		/// Executes decorated node but stops after the count of execution
		/// will hit the limit. After that it returns success.
		/// </summary>
		public override TaskResult Execute(E entity, GameTime gameTime)
		{
			if (counter >= limit)
				return TaskResult.Success;

			counter++;
			return task.Execute(entity, gameTime);
		}
	}
}
