using System;
using System.Collections.Generic;
using System.Text;

namespace BehaviorTrees
{
	/// <summary>
	/// Base class for nodes which decorate other nodes with additional functionality.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class Decorator<E> : Node<E> where E : Entity
	{
		// decorated node
		protected Node<E> node;

		public Decorator(Node<E> node)
			: base()
		{
			this.node = node;
		}
	}
	/// <summary>
	/// Base class for decorator tasks. These tasks create subtasks from provided nodes,
	/// and executes them with some other decorations.
	/// </summary>
	/// <typeparam name="E"></typeparam>
	abstract class DecoratorTask<E> : Task<E> where E : Entity
	{
		// reference to decorated node
		//protected Node<E> node;
		// task that will be created from decorated node evaluation
		protected Task<E> task;

		public DecoratorTask(Node<E> node, E entity)
			: base()
		{
			//this.node = node;
			task = node.Evaluate(entity);
		}
	}
}
