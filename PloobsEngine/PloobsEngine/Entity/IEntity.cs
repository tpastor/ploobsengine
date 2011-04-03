using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.Entity
{
    /// <summary>
    /// Represents a generic entity
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// return the entity id
        /// </summary>
        /// <returns>the id</returns>
        int GetId();

        /// <summary>
        /// sets the id
        /// </summary>
        /// <param name="id"></param>
        void SetId(int id);
    }
}
