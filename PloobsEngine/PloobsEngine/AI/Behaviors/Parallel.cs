using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace BehaviorTrees
{
	/// <summary>
	/// Composite node executing its subnodes in parallel.
	/// </summary>
	/// <typeparam name="E">Entity type.</typeparam>
	public class Parallel<E> : Composite<E> where E : Entity
	{
        // master node is the one whose result have higher priority than others
        // ie if master node succeeds, than whole parallel does
        protected Node<E> masterNode;

		public Parallel()
			: base()
		{
		}
        /// <summary>
        /// Instantiates parallel node with a given master node. Provided master node
		/// is added to the collection as well.
        /// </summary>
        /// <remarks>
        /// Master node is the one whose result have higher priority than others
        /// ie if master node succeeds, than whole parallel does
        /// </remarks>
        /// <param name="masterNode"></param>
        public Parallel(Node<E> masterNode)
        {
            this.masterNode = masterNode;
			Add(masterNode);
        }
        /// <summary>
        /// Creates task responsible for running the children nodes in parallel.
        /// </summary>
        /// <param name="entity">Entity which is used in the process of executing task.</param>
        /// <returns></returns>
		public override Task<E> Evaluate(E entity)
		{
			return new ParallelTask<E>(children, masterNode, entity);
		}
	}
	/// <summary>
	/// Task responsible for running parallel node.
	/// </summary>
	/// <typeparam name="E"></typeparam>
	class ParallelTask<E> : CompositeTask<E> where E : Entity
	{
		// tasks created from nodes
		List<Task<E>> tasks = new List<Task<E>>();
		Task<E> masterTask;

		public ParallelTask(List<Node<E>> children, Node<E> masterNode, E entity)
			: base(children)
		{
			Task<E> task;
			
			foreach (Node<E> node in children)
			{
				task = node.Evaluate(entity);
				if(node == masterNode)
					masterTask = task;
				tasks.Add(task);
			}
		}
		/// <summary>
		/// Executes children nodes' task in parallel.
		/// </summary>
		/// <param name="entity">Entity which is used in the process of executing task.</param>
		/// <returns>
		/// Success if master node succeeds or all of the children succeeds.
		/// Failure if one of the children fails.
		/// Running otherwise.
		/// </returns>
		public override TaskResult Execute(E entity, GameTime gameTime)
		{
			TaskResult result;
			bool success = true;

			// execute tasks
			foreach (Task<E> task in tasks)
			{
				result = task.Execute(entity, gameTime);
				if (result == TaskResult.Failure)
					// if one fails, then everything fails
					return TaskResult.Failure;
				if ((task == masterTask) && result == TaskResult.Success)
					// master task success is the whole parallel task success
					return TaskResult.Success;
				// if one of them is stil running, then there is no success
				if (result == TaskResult.Running)
					success = false;
			}
			if (success)
				// otherwise, success
				return TaskResult.Success;
			else
				return TaskResult.Running;
		}		
	}
}
