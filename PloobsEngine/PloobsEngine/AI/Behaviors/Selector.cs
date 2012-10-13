using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace BehaviorTrees
{
	/// <summary>
	/// Node which selects the children to be run	
	/// </summary>
	/// <typeparam name="E">Entity type.</typeparam>
	public class Selector<E> : Composite<E> where E : Entity
	{
        Func<E, int> selector;
		public Selector(Func<E,int> selector)
			: base()
		{
            this.selector = selector;
		}
		
		/// <summary>
		/// Execute one of the child nodes
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public override Task<E> Evaluate(E entity)
		{
            int selection = selector(entity);
            if (selection < 0 || selection >= children.Count)
			    throw new Exception("Selection out of range ...");
            return children[selection].Evaluate(entity);
		}
	}
}
