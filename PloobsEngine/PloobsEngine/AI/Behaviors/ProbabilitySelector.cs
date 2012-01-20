using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace BehaviorTrees
{
	/// <summary>
	/// Node which selects the children to be run randomly,
	/// basing on assigned weights.
	/// </summary>
	/// <typeparam name="E">Entity type.</typeparam>
	public class ProbabilitySelector<E> : Composite<E> where E : Entity
	{
		// sum of the weights
		float sum = 0f;
		// distribution of the weights
		// (if first node has weight 0.5, and second 0.2, then the list will look in that way: {0.5, 0.7})
		LinkedList<float> weights = new LinkedList<float>();
		Random rand = new Random();

		public ProbabilitySelector()
			: base()
		{
		}

		/// <summary>
		/// Adds subnode with assigned weight.
		/// </summary>
		public void Add(float weight, Node<E> node)
		{
			// add weight
			if (weights.Last != null)
			    weights.AddLast(weight + weights.Last.Value);
			else
				weights.AddLast(weight);
			// recalculate sum
			sum += weight;
			base.Add(node);
		}

		/// <summary>
		/// Adds subnode with default weight.
		/// </summary>
		public new void Add(Node<E> node)
		{
			// or maybe disallow such usage?
			throw new Exception("You MUST specify weight");
		}

		/// <summary>
		/// Execute one of the child nodes, randomly selected.
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public override Task<E> Evaluate(E entity)
		{
			// change the range
			float val = (float)rand.NextDouble() * sum;

			// TODO: it's rather dirty way of choosing the node...
			// but the list containing children have O(n) complexity of retrieving at a given index
			// so it's not much of a problem here
			LinkedListNode<float> w = weights.First;
			foreach (Node<E> node in children)
			{
				Debug.Assert(w != null, "No weight to compare to");
				if (val < w.Value)
					return node.Evaluate(entity);
				w = w.Next;
			}
			throw new Exception("Something went terribly wrong in probability selector node");
		}
	}
}
