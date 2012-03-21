using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BehaviorTrees
{
	/// <summary>
	/// Composite node executing its subnodes one after another.
	/// </summary>
	/// <typeparam name="E">Type of the entity.</typeparam>
	public class Sequence<E> : Composite<E> where E: Entity
	{
		// which node should be executed
		int step = 0;

		public Sequence()
			: base()
		{
		}
		/// <summary>
		/// Creates task that will run the sequence.
		/// </summary>
		public override Task<E> Evaluate(E entity)
		{
			if (children.Count == 0)
				throw new Exception("No children in sequence");

			return new SequenceTask<E>(children);
		}
	}
	/// <summary>
	/// Task responsible for running sequence.
	/// </summary>
	/// <typeparam name="E">Type of entity.</typeparam>
	class SequenceTask<E> : CompositeTask<E> where E : Entity
	{
		// which task should be run
		int step = 0;
		// curently evaluated task
		Task<E> task = null;

		public SequenceTask(List<Node<E>> children)
			: base(children)
		{
		}
		/// <summary>
		/// Evaluates current node and executes its task, after success
		/// moves to the next node in sequence, evaluates, executes until failure or end of sequence.
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="gameTime"></param>
		/// <returns></returns>
		public override TaskResult Execute(E entity, GameTime gameTime)
		{
			if(task == null)
				task = children[step].Evaluate(entity);
			TaskResult result = task.Execute(entity, gameTime);

			switch (result)
			{
				// in case of success we run next task in the sequence(if any)
				case TaskResult.Success:
					step++;
					task = null;
					if (step >= children.Count)
					{
						// whole task was a success
						step = 0;
						return TaskResult.Success;
					}
					else
						// still running
						return TaskResult.Running;
				case TaskResult.Running:
					return TaskResult.Running;
				case TaskResult.Failure:
					return TaskResult.Failure;
				default:
					throw new Exception("You shouldn't reach that code");
			}
		}
	}
}
