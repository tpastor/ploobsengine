using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BehaviorTrees
{
	/// <summary>
	/// Decorator node which waits certain amount of time and then executes
	/// decorated node.
	/// </summary>
	/// <typeparam name="E"></typeparam>
	public class Wait<E> : Decorator<E> where E : Entity
	{
		double seconds;
		/// <summary>
		/// Instantiates wait decorator node with a given amount of
		/// seconds to wait
		/// </summary>
		public Wait(double seconds, Node<E> node)
			: base(node)
		{
			this.seconds = seconds;
		}
		/// <summary>
		/// Creates task that will return TaskResult.Running until the specified amount of seconds
		/// doesn't pass.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <returns></returns>
		public override Task<E> Evaluate(E entity)
		{
			return new WaitTask<E>(seconds, node, entity);
		}
	}
	/// <summary>
	/// Task responsible for running Wait node.
	/// </summary>
	/// <typeparam name="E"></typeparam>
	class WaitTask<E> : DecoratorTask<E> where E : Entity
	{
		double seconds;
		double start = 0;

		public WaitTask(double seconds, Node<E> node, E entity)
			: base(node, entity)
		{
			this.seconds = seconds;
		}
		/// <summary>
		/// Return TaskResult.Running until the specified amount of seconds
		/// doesn't pass.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <returns></returns>
		public override TaskResult Execute(E entity, GameTime gameTime)
		{
			if (start == 0)
			{
				start = gameTime.TotalGameTime.TotalSeconds;
				return TaskResult.Running;
			}

			if (gameTime.TotalGameTime.TotalSeconds - start > seconds)
			{
				TaskResult result = task.Execute(entity, gameTime);
				if (result != TaskResult.Running)
					// task done, reset
					start = 0;
				return result;
			}
			return TaskResult.Running;
		}
	}
}
