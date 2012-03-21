using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// A class which represents a binary node, which is a node that can have two child nodes,
/// which in turn can have two child nodes, and so on. Each node is capable of storing a
/// user data object, which could be anything from a number to a spatial node.
/// </summary>
/// <typeparam name="UserType">Type of data to store in this node.</typeparam>
/// 

namespace PloobsEngine.DataStructure
{
    public class BinaryNode<UserType> : IDisposable where UserType : class
    {
        UserType m_userData;
        internal BinaryNode<UserType> m_left, m_right;

        /// <summary>
        /// User data stored in this node, this could be anything from a number, to a spatial node.
        /// </summary>
        public UserType UserData { get { return m_userData; } set { m_userData = value; } }
        /// <summary>
        /// Gets the left child of this node.
        /// </summary>
        public BinaryNode<UserType> Left { get { return m_left; } }
        /// <summary>
        /// Gets the right child of this node.
        /// </summary>
        public BinaryNode<UserType> Right { get { return m_right; } }
        /// <summary>
        /// Gets a value indicating whether this node has child nodes.
        /// </summary>
        public bool IsLeaf { get { return (m_left == null && m_right == null); } }

        /// <summary>
        /// Create a new binary node.
        /// </summary>
        /// <param name="userData">Optional data to store in the node.</param>
        public BinaryNode(UserType userData = default(UserType))
        {
            m_userData = userData;
        }

        #region IDisposable Members

        ~BinaryNode() { Dispose(false); }

        /// <summary>
        /// Disposes of this node and it's user data.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Called when this node is being disposed of.
        /// </summary>
        /// <param name="disposing">Should managed resources be disposed of as well.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                IDisposable disposable = m_userData as IDisposable;
                if (disposable != null)
                    disposable.Dispose();
                m_userData = null;
            }
        }

        #endregion
    }
}