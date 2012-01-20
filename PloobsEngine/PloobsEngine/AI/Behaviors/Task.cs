using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BehaviorTrees
{
	/// <summary>
	/// The result of executing the node.
	/// </summary>
	public enum TaskResult
	{
		Success,
		Running,
		Failure
	}

	/// <summary>
	/// Base class for tasks - objects which are obtained during tree evaluation,
	/// and then can be executed.
	/// </summary>
	/// <typeparam name="E"></typeparam>
	public abstract class Task<E> where E : Entity
	{
		public abstract TaskResult Execute(E entity, GameTime gameTime);
	}
}
