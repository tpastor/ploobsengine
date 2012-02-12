using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.DataStructure
{
    /// <summary>
    /// A binary tree which assist with inserting, removing, and finding whether data exist in
    /// the tree.
    /// </summary>
    /// <typeparam name="UserType">Type of data stored in the binary nodes.</typeparam>
    public class BinarySearchTree<UserType> : BinaryTree<UserType> where UserType : class
    {
        IComparer<UserType> m_comparer;

        /// <summary>
        /// Create a new binary search tree.
        /// </summary>
        /// <param name="comparer">A comparer to use for determining which nodes are greater/less
        /// than other nodes.</param>
        /// <param name="cloneSource">A binary tree to copy into this tree.</param>
        public BinarySearchTree(IComparer<UserType> comparer,
                                BinaryTree<UserType> cloneSource = null)
            : base(cloneSource)
        {
            if (m_comparer == null)
                throw new ArgumentNullException("comparer");

            m_comparer = comparer;
        }

        /// <summary>
        /// Checks whether the specified item exist in this binary tree.
        /// </summary>
        /// <param name="searchItem"></param>
        /// <returns>True if the item exist in this tree; false if it does not.</returns>
        public bool Contains(UserType searchItem)
        {
            BinaryNode<UserType> current = m_root;
            bool found = false;

            while (current != null && !found)
            {
                int comprand = m_comparer.Compare(current.UserData, searchItem);
                if (comprand > 0)
                    current = current.m_left;
                else if (comprand < 0)
                    current = current.m_right;
                else
                    found = true;
            }

            return found;
        }

        /// <summary>
        /// Inserts an item into this binary tree.
        /// </summary>
        /// <param name="insertItem"></param>
        public void Insert(UserType insertItem)
        {
            if (m_root == null)
                m_root = new BinaryNode<UserType>(insertItem);
            else
            {
                BinaryNode<UserType> current = m_root;
                BinaryNode<UserType> trailCurrent = null;
                int comparand = 0;

                while (current != null)
                {
                    trailCurrent = current;

                    comparand = m_comparer.Compare(current.UserData, insertItem);
                    if (comparand == 0)
                        throw new InvalidOperationException(
                            "The item being inserted already exist in the binary search tree.");
                    else
                        current = (comparand > 0) ? current.m_left : current.m_right;
                }

                comparand = m_comparer.Compare(trailCurrent.UserData, insertItem);
                if (comparand > 0)
                    trailCurrent.m_left = new BinaryNode<UserType>(insertItem);
                else
                    trailCurrent.m_right = new BinaryNode<UserType>(insertItem);
            }
        }

        /// <summary>
        /// Removes an itemf rom this binary tree.
        /// </summary>
        /// <param name="removeItem"></param>
        public bool Remove(UserType removeItem)
        {
            if (m_root != null)
            {
                BinaryNode<UserType> current = m_root;
                BinaryNode<UserType> trailCurrent = m_root;
                bool found = false;
                int comparand = 0;

                while (current != null && !found)
                {
                    comparand = m_comparer.Compare(current.UserData, removeItem);
                    if (comparand == 0)
                        found = true;
                    else
                    {
                        trailCurrent = current;
                        if (comparand > 0)
                            current = current.m_left;
                        else
                            current = current.m_right;
                    }
                }

                if (current != null && found)
                {
                    if (current == m_root)
                        RemoveFromTree(ref m_root);
                    else
                    {
                        comparand = m_comparer.Compare(trailCurrent.UserData, removeItem);
                        if (comparand > 0)
                            RemoveFromTree(ref trailCurrent.m_left);
                        else
                            RemoveFromTree(ref trailCurrent.m_right);
                    }
                }
                return found;
            }
            return false;
        }
        void RemoveFromTree(ref BinaryNode<UserType> node)
        {
            if (node != null)
            {
                BinaryNode<UserType> temp = node;
                if (node.m_left == null && node.m_right == null)
                {
                    node = null;
                    temp.Dispose();
                }
                else if (node.m_left == null)
                {
                    node = temp.m_right;
                    temp.Dispose();
                }
                else if (node.m_right == null)
                {
                    node = temp.m_left;
                    temp.Dispose();
                }
                else
                {
                    BinaryNode<UserType> current = node.m_left;
                    BinaryNode<UserType> trailCurrent = null;

                    while (current.m_right != null)
                    {
                        trailCurrent = current;
                        current = current.m_right;
                    }

                    node.UserData = current.UserData;

                    if (trailCurrent == null)
                        node.m_left = current.m_left;
                    else
                        trailCurrent.m_right = current.m_left;

                    current.Dispose();
                }
            }
        }
    }
}
