#if !WINDOWS_PHONE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PloobsEngine.Commands;
using PloobsEngine.Input;
using PloobsEngine.SceneControl;
using PloobsEngine.Cameras;
using PloobsEngine.Physics;
using PloobsEngine.Components;
using PloobsEngine.Engine;

namespace PloobsEngine.SceneControl
{
    /// <summary>
    /// Fired when Picking something
    /// </summary>
    /// <param name="SegmentInterceptInfo">The segment intercept info.</param>
    public delegate void OnPicked(SegmentInterceptInfo SegmentInterceptInfo);

    /// <summary>
    /// Picking Helper
    /// </summary>
    public class Picking : IScreenUpdateable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Picking"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="pickingRayDistance">The picking ray distance.</param>
        public Picking(IScene owner, float pickingRayDistance = 500)
            : base(owner)
        {
            leftButtonIntercept = rightButtonIntercept = noneButtonIntercept = CullNothing;
            this.pickingRayDistance = pickingRayDistance;
            world = owner.World;
            info = owner.GraphicInfo;

            {
                SimpleConcreteMouseBottomInputPlayable pbLeft = new SimpleConcreteMouseBottomInputPlayable(StateKey.PRESS, EntityType.COMPONENT, MouseButtons.LeftButton, MouseBottomLeft, InputMask.GPICKING);
                bmc1 = new BindMouseCommand(pbLeft, BindAction.ADD);
                CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bmc1);
            }

            {
                SimpleConcreteMouseBottomInputPlayable pbRight = new SimpleConcreteMouseBottomInputPlayable(StateKey.PRESS, EntityType.COMPONENT, MouseButtons.RightButton, MouseBottomRight, InputMask.GPICKING);
                bmc2 = new BindMouseCommand(pbRight, BindAction.ADD);
                CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bmc2);
            }
        }
        BindMouseCommand bmc1;
        BindMouseCommand bmc2;
        private GraphicInfo info;
        private IWorld world;
        private Ray ray;
        private InputPlaybleMouseBottom pbLeft = null;
        private InputPlaybleMouseBottom pbRight = null;
        private float pickingRayDistance;

        public event OnPicked OnPickedLeftButton = null;
        public event OnPicked OnPickedRighttButton = null;
        public event OnPicked OnPickedNoneButton = null;

        private Func<IPhysicObject, bool> noneButtonIntercept;
        private Func<IPhysicObject, bool> rightButtonIntercept;
        private Func<IPhysicObject, bool> leftButtonIntercept;

        public Func<IPhysicObject, bool> LeftButtonIntercept
        {
            get { return leftButtonIntercept; }
            set { leftButtonIntercept = value; }
        }

        public Func<IPhysicObject, bool> RightButtonIntercept
        {
            get { return rightButtonIntercept; }
            set { rightButtonIntercept = value; }
        }

        public Func<IPhysicObject, bool> NoneButtonIntercept
        {
            get { return noneButtonIntercept; }
            set { noneButtonIntercept = value; }
        }

        private bool CullNothing(IPhysicObject phy)
        {
            return true;
        }

        public float PickingRayDistance
        {
            get { return pickingRayDistance; }
            set { pickingRayDistance = value; }
        }

        protected override void Update(GameTime gt)
        {
            if (OnPickedNoneButton != null)
            {

                MouseState ms = Mouse.GetState();
                UpdatePickRay(ms);

                SegmentInterceptInfo rti = world.PhysicWorld.SegmentIntersect(ray, noneButtonIntercept, pickingRayDistance);
                if (rti == null)
                    return;
                OnPickedNoneButton(rti);
            }
        }

        private void UpdatePickRay(MouseState ms)
        {
            Matrix projection = world.CameraManager.ActiveCamera.Projection;
            Matrix viewProjection = world.CameraManager.ActiveCamera.View * world.CameraManager.ActiveCamera.Projection;
            Matrix viewInverse = Matrix.Invert(world.CameraManager.ActiveCamera.View);
            Matrix projectionInverse = Matrix.Invert(world.CameraManager.ActiveCamera.Projection);
            Matrix viewProjectionInverse = projectionInverse * viewInverse;

            Vector3 v = new Vector3();
            v.X = (((2.0f * ms.X) / info.Viewport.Width) - 1);
            v.Y = -(((2.0f * ms.Y) / info.Viewport.Height) - 1);
            v.Z = 0.0f;

            Ray pickRay = new Ray();
            pickRay.Position.X = viewInverse.M41;
            pickRay.Position.Y = viewInverse.M42;
            pickRay.Position.Z = viewInverse.M43;
            pickRay.Direction = Vector3.Normalize(Vector3.Transform(v, viewProjectionInverse) - pickRay.Position);
            this.ray = pickRay;
        }


        private void MouseBottomLeft(MouseState ms)
        {
            if (OnPickedLeftButton != null)
            {
                UpdatePickRay(ms);

                SegmentInterceptInfo rti = world.PhysicWorld.SegmentIntersect(ray, noneButtonIntercept, pickingRayDistance);
                if (rti == null)
                    return;
                if (OnPickedLeftButton != null)
                    OnPickedLeftButton(rti);
            }

        }

        private void MouseBottomRight(MouseState ms)
        {
            if (OnPickedRighttButton != null)
            {
                UpdatePickRay(ms);

                SegmentInterceptInfo rti = world.PhysicWorld.SegmentIntersect(ray, noneButtonIntercept, pickingRayDistance);
                if (rti == null)
                    return;
                if (OnPickedRighttButton != null)
                    OnPickedRighttButton(rti);
            }

        }

        protected override void CleanUp()
        {
            bmc1.BindAction = BindAction.REMOVE;
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bmc1);

            bmc2.BindAction = BindAction.REMOVE;
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(bmc2);
            base.CleanUp();
        }
    }
}
#endif