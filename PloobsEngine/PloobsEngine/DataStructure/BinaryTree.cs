using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.DataStructure
{
    /// <summary>
    /// Determines the pattern used to visit nodes in a binary tree.
    /// </summary>
    public enum BinaryTreeTraversal
    {
        /// <summary>
        /// Move into the left sub-tree.
        /// Visit the node.
        /// Move into the right sub-tree.
        /// </summary>
        Inorder = 0,

        /// <summary>
        /// Visit the node.
        /// Move into the left sub-tree.
        /// Move into the right sub-tree.
        /// </summary>
        Preorder,

        /// <summary>
        /// Move into the left sub-tree.
        /// Move into the right sub-tree.
        /// Visit the node.
        /// </summary>
        Postorder
    }

    /// <summary>
    /// Delegate used when visiting a node in the binary tree.
    /// </summary>
    /// <typeparam name="UserType">Type of data stored in the nodes.</typeparam>
    /// <param name="node">The node being visited.</param>
    /// <param name="cancel">True if the traversal should stop.</param>
    public delegate void BinaryTreeVisitDelegate<UserType>(BinaryNode<UserType> node, ref bool cancel)
    where UserType : class; 

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="UserType">Type of data stored in the nodes.</typeparam>
    public class BinaryTree<UserType> : IDisposable where UserType : class
    {
        protected BinaryNode<UserType> m_root;

        /// <summary>
        /// Number of nodes traversed from the root to the deepest part of the tree.
        /// </summary>
        public int Height { get { return GetHeight(m_root); } }
        /// <summary>
        /// Number of nodes in the tree.
        /// </summary>
        public int NodeCount { get { return GetNodeCount(m_root); } }
        /// <summary>
        /// Number of nodes without children in the tree.
        /// </summary>
        public int LeafCount { get { return GetLeafCount(m_root); } }
        /// <summary>
        /// Does the tree even have any nodes?
        /// </summary>
        public bool IsEmpty { get { return (m_root == null); } }

        /// <summary>
        /// Create a new binary tree.
        /// </summary>
        /// <param name="cloneSource">A binary tree to copy into this tree.</param>
        public BinaryTree(BinaryTree<UserType> cloneSource = null)
        {
            m_root = null;
            if (cloneSource != null)
                CopyTree(out m_root, cloneSource.m_root);
        }

        /// <summary>
        /// Traverses the binary tree calling a visit method on each node.
        /// </summary>
        /// <param name="visitMethod">A method to be called on each visited node.</param>
        /// <param name="traversalMode">Mode of traversal to perform.</param>
        public void Traverse(BinaryTreeVisitDelegate<UserType> visitMethod,
                                BinaryTreeTraversal traversalMode = BinaryTreeTraversal.Inorder)
        {
            if (visitMethod != null)
            {
                bool cancel = false;
                if (traversalMode == BinaryTreeTraversal.Inorder)
                    TraverseInorder(visitMethod, ref cancel, m_root);
                else if (traversalMode == BinaryTreeTraversal.Preorder)
                    TraversePreorder(visitMethod, ref cancel, m_root);
                else if (traversalMode == BinaryTreeTraversal.Postorder)
                    TraversePostorder(visitMethod, ref cancel, m_root);
            }
        }

        void CopyTree(out BinaryNode<UserType> copyRoot, BinaryNode<UserType> otherRoot)
        {
            copyRoot = null;
            if (otherRoot != null)
            {
                copyRoot = new BinaryNode<UserType>(otherRoot.UserData);
                CopyTree(out copyRoot.m_left, otherRoot.m_left);
                CopyTree(out copyRoot.m_right, otherRoot.m_right);
            }
        }

        void TraverseInorder(BinaryTreeVisitDelegate<UserType> visitMethod, ref bool cancel,
                                BinaryNode<UserType> node)
        {
            if (node != null && !cancel)
            {
                TraverseInorder(visitMethod, ref cancel, node.Left);
                if (!cancel) visitMethod(node, ref cancel);
                TraverseInorder(visitMethod, ref cancel, node.Right);
            }
        }
        void TraversePreorder(BinaryTreeVisitDelegate<UserType> visitMethod, ref bool cancel,
                                BinaryNode<UserType> node)
        {
            if (node != null && !cancel)
            {
                if (!cancel) visitMethod(node, ref cancel);
                TraversePreorder(visitMethod, ref cancel, node.Left);
                TraversePreorder(visitMethod, ref cancel, node.Right);
            }
        }
        void TraversePostorder(BinaryTreeVisitDelegate<UserType> visitMethod, ref bool cancel,
                                BinaryNode<UserType> node)
        {
            if (node != null && !cancel)
            {
                TraversePostorder(visitMethod, ref cancel, node.Left);
                TraversePostorder(visitMethod, ref cancel, node.Right);
                if (!cancel) visitMethod(node, ref cancel);
            }
        }

        int GetHeight(BinaryNode<UserType> node)
        {
            if (node != null)
                return 1 + Math.Max(GetHeight(node.m_left), GetHeight(node.m_right));
            return 0;
        }
        int GetNodeCount(BinaryNode<UserType> node)
        {
            if (node != null)
                return 1 + GetNodeCount(node.m_left) + GetNodeCount(node.m_right);
            return 0;
        }
        int GetLeafCount(BinaryNode<UserType> node)
        {
            if (node != null)
                return GetLeafCount(node.m_left) + GetLeafCount(node.m_right);
            return 1;
        }

        #region IDisposable Members

        ~BinaryTree() { Dispose(false); }

        /// <summary>
        /// Dispose of this binary tree, it's nodes and their data.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Called when this binary tree is being disposed of.
        /// </summary>
        /// <param name="disposing">Should managed resources be disposed of as well.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                Dispose(ref m_root);
        }

        // recursive disposal function, pre-order traversal
        void Dispose(ref BinaryNode<UserType> node)
        {
            if (node != null)
            {
                Dispose(ref node.m_left);
                Dispose(ref node.m_right);

                node.Dispose();
                node = null;
            }
        }

        #endregion
    }
}
