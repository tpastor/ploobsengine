using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BehaviorTrees
{
	/// <summary>
	/// Execute decorated subnode a specified amount of times.
	/// </summary>
	/// <typeparam name="E"></typeparam>
	public class Repeater<E> : Decorator<E> where E : Entity
	{
		int amount = 0;
		/// <summary>
		/// Instantiates repeater and set amount of repeats.
		/// </summary>
		/// <param name="amount"></param>
		/// <param name="node"></param>
		public Repeater(int amount, Node<E> node)
			: base(node)
		{
			this.amount = amount;
		}
		/// <summary>
		/// Instantiates infinite repeater
		/// </summary>
		/// <param name="node"></param>
		public Repeater(Node<E> node)
			: base(node)
		{
		}
		/// <summary>
		/// Creates task that will be executing decorated subnode, but will returning Running status until
		/// it will be executed specified amount of times.
		/// </summary>
		/// <returns>TaskResult.Success</returns>
		public override Task<E> Evaluate(E entity)
		{
			return new RepeaterTask<E>(amount, node, entity);
		}
	}
	/// <summary>
	/// Class responsible for running Repeater node.
	/// </summary>
	/// <typeparam name="E"></typeparam>
	class RepeaterTask<E> : DecoratorTask<E> where E : Entity
	{
		int counter = 0;
		int amount = 0;

		public RepeaterTask(int amount, Node<E> node, E entity)
			: base(node, entity)
		{
			this.amount = amount;
		}
		/// <summary>
		/// Executes decorated subnode, but return Running status until
		/// it will be executed specified amount of times.
		/// </summary>
		public override TaskResult Execute(E entity, GameTime gameTime)
		{
			TaskResult result;
			if (amount != 0)
			{
				if (counter + 1 >= amount)
					// last execution
					return task.Execute(entity, gameTime);
			}
			result = task.Execute(entity, gameTime);
			// if fails, then don't repeat
			if (result == TaskResult.Failure)
				return result;
			counter++;
			return TaskResult.Running;
		}
	}
}
