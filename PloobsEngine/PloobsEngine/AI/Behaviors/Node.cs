using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BehaviorTrees
{
	/// <summary>
	/// Base class for nodes of the behavior tree.
	/// </summary>
    /// <typeparam name="E">
    /// Entity type. Each defined node may only apply to the specified types of the entitie.
    /// </typeparam>
	public abstract class Node<E> //: INode
		where E: Entity
	{
		/// <summary>
		/// Evaluates the node and returns task object for latent execution.
		/// </summary>
		/// <returns></returns>
		public abstract Task<E> Evaluate(E entity);
	}
}
