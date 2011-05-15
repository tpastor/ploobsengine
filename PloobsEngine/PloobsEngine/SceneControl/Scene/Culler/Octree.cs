using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using PloobsEngine.Features.DebugDraw;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Represents an octree spatial partioning system.
    /// </summary>
    public class Octree<T>
    {
        /// <summary>
        /// The number of children in an octree.
        /// </summary>
        private const int ChildCount = 8;

        /// <summary>
        /// The octree's looseness value.
        /// </summary>
        private float looseness = 0;

        /// <summary>
        /// The octree's depth.
        /// </summary>
        private int depth = 0;

        private static DebugShapesDrawer debugDraw = null;

        public DebugShapesDrawer DebugDraw
        {
            get { return debugDraw; }
            set { debugDraw = value; }
        }

        /// <summary>
        /// The octree's center coordinates.
        /// </summary>
        private Vector3 center = Vector3.Zero;

        /// <summary>
        /// The octree's length.
        /// </summary>
        private float length = 0f;

        /// <summary>
        /// The bounding box that represents the octree.
        /// </summary>
        private BoundingBox bounds = default(BoundingBox);

        /// <summary>
        /// The objects in the octree.
        /// </summary>
        private List<T> objects = new List<T>();

        /// <summary>
        /// The octree's child nodes.
        /// </summary>
        private Octree<T>[] children = null;

        /// <summary>
        /// The octree's world size.
        /// </summary>
        private float worldSize = 0f;

        /// <summary>
        /// Creates a new octree.
        /// </summary>    
        /// <param name="worldSize">The octree's world size.</param>
        /// <param name="looseness">The octree's looseness value.</param>
        /// <param name="depth">The octree recursion depth.</param>
        public Octree(float worldSize, float looseness, int depth)
            : this(worldSize, looseness, depth, 0, Vector3.Zero)
        {
        }

        public Octree(float worldSize, float looseness, int depth, Vector3 center)
            : this(worldSize, looseness, depth, 0, center)
        {
        }

        /// <summary>
        /// Creates a new octree.
        /// </summary>    
        /// <param name="worldSize">The octree's world size.</param>
        /// <param name="looseness">The octree's looseness value.</param>
        /// <param name="maxDepth">The maximum depth to recurse to.</param>
        /// <param name="depth">The octree recursion depth.</param>
        /// <param name="center">The octree's center coordinates.</param>
        private Octree(float worldSize, float looseness,
            int maxDepth, int depth, Vector3 center)
        {
            this.worldSize = worldSize;
            this.looseness = looseness;
            this.depth = depth;
            this.center = center;
            this.length = this.looseness * this.worldSize / (float)Math.Pow(2, this.depth);
            float radius = this.length / 2f;

            // Create the bounding box.
            Vector3 min = this.center + new Vector3(-radius);
            Vector3 max = this.center + new Vector3(radius);
            this.bounds = new BoundingBox(min, max);

            // Split the octree if the depth hasn't been reached.
            if (this.depth < maxDepth)
            {
                this.Split(maxDepth);
            }
        }

        public void Remove(T obj)
        {
            objects.Remove(obj);
        }

        public bool HasChanged(T obj, BoundingBox transformebbox)
        {
            return this.bounds.Contains(transformebbox) == ContainmentType.Contains;
        }

        public bool StillInside(T o, Vector3 center, float radius)
        {
            Vector3 min = center - new Vector3(radius);
            Vector3 max = center + new Vector3(radius);
            BoundingBox bounds = new BoundingBox(min, max);

            if (this.children != null)
                return false;

            if (this.bounds.Contains(bounds) == ContainmentType.Contains)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Adds the given object to the octree.
        /// </summary>
        /// <param name="o">The object to add.</param>
        /// <param name="center">The object's center coordinates.</param>
        /// <param name="radius">The object's radius.</param>
        public Octree<T> Add(T o, Vector3 center, float radius)
        {
            Vector3 min = center - new Vector3(radius);
            Vector3 max = center + new Vector3(radius);
            BoundingBox bounds = new BoundingBox(min, max);

            if (this.bounds.Contains(bounds) == ContainmentType.Contains)
            {
                return this.Add(o, bounds, center, radius);
            }
            return null;
        }


        /// <summary>
        /// Adds the given object to the octree.
        /// </summary>    
        public Octree<T> Add(T o, BoundingBox transformebbox)
        {
            float radius = (transformebbox.Max - transformebbox.Min).Length();
            Vector3 center = (transformebbox.Max + transformebbox.Min) / 2;

            if (this.bounds.Contains(transformebbox) == ContainmentType.Contains)
            {
                return this.Add(o, bounds, center, radius / 2);
            }
            return null;
        }


        /// <summary>
        /// Adds the given object to the octree.
        /// </summary>
        /// <param name="o">The object to add.</param>
        /// <param name="bounds">The object's bounds.</param>
        /// <param name="center">The object's center coordinates.</param>
        /// <param name="radius">The object's radius.</param>
        private Octree<T> Add(T o, BoundingBox bounds, Vector3 center, float radius)
        {
            if (this.children != null)
            {
                // Find which child the object is closest to based on where the
                // object's center is located in relation to the octree's center.
                int index = (center.X <= this.center.X ? 0 : 1) +
                    (center.Y >= this.center.Y ? 0 : 4) +
                    (center.Z <= this.center.Z ? 0 : 2);

                // Add the object to the child if it is fully contained within
                // it.
                if (this.children[index].bounds.Contains(bounds) == ContainmentType.Contains)
                {
                    return this.children[index].Add(o, bounds, center, radius);

                }
            }
            this.objects.Add(o);
            return this;
        }

        /// <summary>
        /// Draws the octree.
        /// </summary>
        /// <param name="view">The viewing matrix.</param>
        /// <param name="projection">The projection matrix.</param>
        /// <param name="objects">The objects in the octree.</param>
        /// <returns>The number of octrees drawn.</returns>
        public int Draw(Matrix view, Matrix projection, List<T> objects)
        {
            BoundingFrustum frustum = new BoundingFrustum(view * projection);
            ContainmentType containment = frustum.Contains(this.bounds);

            return this.Draw(frustum, view, projection, containment, objects);
        }

        /// <summary>
        /// Draws the octree.
        /// </summary>
        /// <param name="frustum">The viewing frustum used to determine if the octree is in view.</param>
        /// <param name="view">The viewing matrix.</param>
        /// <param name="projection">The projection matrix.</param>
        /// <param name="containment">Determines how much of the octree is visible.</param>
        /// <param name="objects">The objects in the octree.</param>
        /// <returns>The number of octrees drawn.</returns>
        private int Draw(BoundingFrustum frustum, Matrix view, Matrix projection,
            ContainmentType containment, List<T> objects)
        {
            int count = 0;

            if (containment != ContainmentType.Contains)
            {
                containment = frustum.Contains(this.bounds);
            }

            // Draw the octree only if it is atleast partially in view.
            if (containment != ContainmentType.Disjoint)
            {
                // Draw the octree's bounds if there are objects in the octree.
                if (this.objects.Count > 0)
                {                    
                    if (DebugDraw != null)
                        DebugDraw.AddShape(new Box(this.bounds,Color.White));
                    objects.AddRange(this.objects);
                    count++;
                }

                // Draw the octree's children.
                if (this.children != null)
                {
                    foreach (Octree<T> child in this.children)
                    {
                        count += child.Draw(frustum, view, projection, containment, objects);
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// Splits the octree into eight children.
        /// </summary>
        /// <param name="maxDepth">The maximum depth to recurse to.</param>
        private void Split(int maxDepth)
        {
            this.children = new Octree<T>[Octree<T>.ChildCount];
            int depth = this.depth + 1;
            float quarter = this.length / this.looseness / 4f;

            this.children[0] = new Octree<T>(this.worldSize, this.looseness,
                maxDepth, depth, this.center + new Vector3(-quarter, quarter, -quarter));
            this.children[1] = new Octree<T>(this.worldSize, this.looseness,
                maxDepth, depth, this.center + new Vector3(quarter, quarter, -quarter));
            this.children[2] = new Octree<T>(this.worldSize, this.looseness,
                maxDepth, depth, this.center + new Vector3(-quarter, quarter, quarter));
            this.children[3] = new Octree<T>(this.worldSize, this.looseness,
                maxDepth, depth, this.center + new Vector3(quarter, quarter, quarter));
            this.children[4] = new Octree<T>(this.worldSize, this.looseness,
                maxDepth, depth, this.center + new Vector3(-quarter, -quarter, -quarter));
            this.children[5] = new Octree<T>(this.worldSize, this.looseness,
                maxDepth, depth, this.center + new Vector3(quarter, -quarter, -quarter));
            this.children[6] = new Octree<T>(this.worldSize, this.looseness,
                maxDepth, depth, this.center + new Vector3(-quarter, -quarter, quarter));
            this.children[7] = new Octree<T>(this.worldSize, this.looseness,
                maxDepth, depth, this.center + new Vector3(quarter, -quarter, quarter));
        }

    }
}