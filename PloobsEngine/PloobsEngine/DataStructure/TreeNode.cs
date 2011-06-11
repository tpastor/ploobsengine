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