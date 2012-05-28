using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace BehaviorTrees
{
	/// <summary>
	/// Base class for nodes that are containing a children nodes.
	/// </summary>
	/// <typeparam name="E">Entity type.</typeparam>
	public abstract class Composite<E> : 
        Node<E>,
        IEnumerable
        where E : Entity
	{
        // children nodes
		protected List<Node<E>> children = new List<Node<E>>();

		public Composite()
			: base()
		{
		}
        /// <summary>
        /// Add child node.
        /// </summary>
        /// <param name="child">Node to add.</param>
		public void Add(Node<E> child)
		{
			children.Add(child);
		}

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (Node<E> node in children)
            {
                yield return node;
            }
        }
    }
	/// <summary>
	/// Base class for tasks that are created from composite nodes and has
	/// to reference other nodes that will be evaluated if the previous task finishes.
	/// </summary>
	/// <typeparam name="E"></typeparam>
	abstract class CompositeTask<E> :
		Task<E>,
		IEnumerable
		where E : Entity
	{
		// references to childredn nodes(CompositeTask doesn't own them)
		protected List<Node<E>> children;

		public CompositeTask(List<Node<E>> children)
			: base()
		{
			this.children = children;
		}

		public IEnumerator GetEnumerator()
		{
			foreach (Node<E> node in children)
			{
				yield return node;
			}
		}
	}
}
