#region License
/*
    PloobsEngine Game Engine Version 0.3 Beta
    Copyright (C) 2011  Ploobs

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.DataStructure
{
    /// <summary>
    /// Tree Node Implementatio
    /// </summary>
    /// <typeparam name="E"></typeparam>
    class TreeNode<E>
    {
        #region Fields
        /// <summary>
        /// The value of this particular Tree Node.
        /// </summary>
        private E value;
 
        /// <summary>
        /// The array of subnodes.
        /// </summary>
        private TreeNode<E>[] listOfSubNodes;
        #endregion
 
        #region Properties
        /// <summary>
        /// Gets and sets the value of this Tree Node.
        /// </summary>
        public E Value
        {
            get { return value; }
            set { this.value = value; }
        }
 
        /// <summary>
        /// Gets and sets SubNodes.
        /// </summary>
        public TreeNode<E>[] SubNodes
        {
            get { return listOfSubNodes; }
            set { listOfSubNodes = value; }
        }
        #endregion
 
        #region Constructors
        /// <summary>
        /// Default Constructor.
        /// </summary>
        public TreeNode()
        {
            value = default(E);
            listOfSubNodes = null;
        }
 
        /// <summary>
        /// Constructs a new Tree Node.
        /// </summary>
        /// <param name="value">Value for this Node.</param>
        /// <param name="subNodes">Array of SubNode values.</param>
        public TreeNode(E value, E [] subNodes)
        {
            this.value = value;
            if (subNodes != null)
            {
                listOfSubNodes = new TreeNode<E> [subNodes.Length];
                for (int i = 0; i < subNodes.Length; i++)
                    listOfSubNodes [i] = new TreeNode<E>(subNodes [i], null);
            }
            else
                listOfSubNodes = null;
 
        }
        #endregion
 
        #region Methods
 
        /// <summary>
        /// Returns one TreeNode at a specific index in this TreeNode's Sub Nodes.
        /// </summary>
        /// <param name="index">Index of TreeNode to return.</param>
        /// <returns>TreeNode at index specified.</returns>
        public TreeNode<E> Get(int index)
        {
            return listOfSubNodes [index];
        }
 
        /// <summary>
        /// Returns a String representation of this TreeNode and all the Sub Nodes.
        /// </summary>
        public override String ToString()
        {
            return GetString(this);
        }
 
        /// <summary>
        /// Helper method for ToString().
        /// </summary>
        private String GetString(TreeNode<E> node)
        {
            String val = node.Value.ToString() + "\n";
            foreach (TreeNode<E> nextNode in node.SubNodes)
                val += GetString(nextNode);
            return val;
        }
        #endregion
    }
}