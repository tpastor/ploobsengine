using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace BehaviorTrees
{
    /// <summary>
    /// Execute decorated subnode
    /// </summary>
    /// <typeparam name="E"></typeparam>
    public class RepeaterUntil<E> : Decorator<E> where E : Entity
    {
        ConditionDelegate<E> condition;
        /// <summary>
        /// Instantiates repeater and set amount of repeats.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="node"></param>
        public RepeaterUntil(ConditionDelegate<E> condition, Node<E> node)
            : base(node)
        {
            this.condition = condition;
        }

        /// <summary>
        /// Creates task that will be executing decorated subnode, but will returning Running status until
        /// it will be executed specified amount of times.
        /// </summary>
        /// <returns>TaskResult.Success</returns>
        public override Task<E> Evaluate(E entity)
        {
            return new RepeaterUntilTask<E>(condition, node, entity);
        }
    }
    /// <summary>
    /// Class responsible for running Repeater node.
    /// </summary>
    /// <typeparam name="E"></typeparam>
    class RepeaterUntilTask<E> : BehaviorTrees.DecoratorTask<E> where E : Entity
    {
        ConditionDelegate<E> condition;
        public RepeaterUntilTask(ConditionDelegate<E> condition, Node<E> node, E entity)
            : base(node, entity)
        {
            this.condition = condition;
        }

        TaskResult TaskResult = TaskResult.Success;

        /// <summary>
        /// Executes decorated subnode
        /// </summary>
        public override TaskResult Execute(E entity, GameTime gameTime)
        {
            //if (TaskResult == BehaviorTrees.TaskResult.Success)
            //{
            //    TaskResult = condition(entity, gameTime) ;
            //    if (TaskResult != BehaviorTrees.TaskResult.Running)
            //        return TaskResult;
            //}

            //if (TaskResult == BehaviorTrees.TaskResult.Running)
            //{
            //    TaskResult tr = task.Execute(entity, gameTime);                
            //    if (tr == BehaviorTrees.TaskResult.Failure)
            //        return TaskResult.Failure;
            //    else
            //        return TaskResult.Running;
            //}

            //return TaskResult.Failure;

            if (condition(entity, gameTime) == TaskResult.Running)
            {
                if (task.Execute(entity, gameTime) != TaskResult.Failure)
                    return TaskResult.Running;
                return TaskResult.Failure;
            }
            else
            {
                return TaskResult.Success;
            }
        }
    }
}
