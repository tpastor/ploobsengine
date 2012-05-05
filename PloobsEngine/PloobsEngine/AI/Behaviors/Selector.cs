using System;
using System.Collections.Generic;
using System.Text;

namespace BehaviorTrees
{
	public abstract class Selector<E> : Composite<E> where E: Entity
	{
		public Selector()
			: base()
		{
		}
	}
}
