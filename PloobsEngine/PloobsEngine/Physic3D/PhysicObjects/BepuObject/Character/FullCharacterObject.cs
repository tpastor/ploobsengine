﻿#region License
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
using BEPUphysics.Collidables.MobileCollidables;
using Microsoft.Xna.Framework;
using PloobsEngine.Engine.Logger;
using BEPUphysics.Entities.Prefabs;

namespace PloobsEngine.Physics.Bepu
{
    public class FullCharacterObject : BepuEntityObject
    {
        BEPUphysicsDemos.AlternateMovement.Character.CharacterController characterController;
        float YAlignement;
        Matrix rotation;

        public FullCharacterObject(Vector3 position, Matrix rotation, float height, float radius, Vector3 scale, float YAlignement = 0)
        {
            this.scale = scale;
            this.rotation = rotation;
            this.YAlignement = YAlignement * scale.Y;
            this.characterController = new BEPUphysicsDemos.AlternateMovement.Character.CharacterController(new Cylinder(position,height* scale.Y,radius * scale.X));            
            this.entity = characterController.Body;
        }

        public BEPUphysicsDemos.AlternateMovement.Character.CharacterController CharacterController
        {
            get { return characterController; }
            set { characterController = value; }
        }       
        

        public override PhysicObjectTypes PhysicObjectTypes
        {
            get
            {
                return PhysicObjectTypes.CHARACTEROBJECT;
            }            
        }

#if !WINDOWS_PHONE

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            ActiveLogger.LogMessage("Serialization not implemented yet", LogLevel.RecoverableError);
        }
#endif
        
        public override Vector3 Position
        {
            get
            {
                return characterController.Body.Position;
            }
            set
            {
                characterController.Body.Position = value;
            }
        }

        Vector3 scale;
        public override Vector3 Scale
        {
            get
            {
                return scale;
            }
            set
            {
                ActiveLogger.LogMessage("cant set scele of a fullcharacter", LogLevel.Warning);
            }
        }

        public override Matrix Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
            }
        }

        public void RotateYByAngleRadians(float angle)
        {
            rotation = Matrix.CreateRotationY(angle) * rotation;
        }

        public void RotateYByAngleDegrees(float angle)
        {
            rotation = Matrix.CreateRotationY(MathHelper.ToRadians(angle)) * rotation;
        }

        public void MoveToDirection(Vector2 movementDirection)
        {
            CharacterController.HorizontalMotionConstraint.MovementDirection = movementDirection;
        }

        public void Jump()
        {
            CharacterController.Jump();
        }

        private bool isducking = false;
        public void Duck()
        {
            CharacterController.StanceManager.DesiredStance = BEPUphysicsDemos.AlternateMovement.Character.Stance.Crouching ;
        }
        public void StandUp()
        {
            CharacterController.StanceManager.DesiredStance = BEPUphysicsDemos.AlternateMovement.Character.Stance.Standing;
        }
        public override Vector3 FaceVector
        {
            get { return rotation.Forward; }
        }

        public override Matrix WorldMatrix
        {
            get
            {
                Vector3 pos = new Vector3(Position.X, Position.Y + this.YAlignement, Position.Z);
                return Matrix.CreateScale(Scale) * Rotation * Matrix.CreateTranslation(pos);                
            }
        }

        public override Vector3 AngularVelocity
        {
            get { return characterController.Body.AngularVelocity; }
            set
            {
                ActiveLogger.LogMessage("cant Set Velocity in Character Object", LogLevel.Warning);
            }
        }

        public override Vector3 Velocity
        {
            get
            {
                return characterController.Body.LinearVelocity;
            }
            set
            {
                ActiveLogger.LogMessage("cant Set Velocity in Character Object", LogLevel.Warning);
            }
        }

        public override bool isMotionLess
        {
            get
            {
                return true;
            }
            set
            {
                ActiveLogger.LogMessage("Character object is always mobile", LogLevel.Warning);
            }
        }

        public override SceneControl.IObject ObjectOwner
        {
            get;
            set;
        }

        
        public override void ApplyImpulse(Vector3 position, Vector3 force)
        {
            ActiveLogger.LogMessage("Cant Apply impulse in Character Object", LogLevel.Warning);   
        }

        public override BoundingBox BoundingBox
        {
            get { return characterController.Body.CollisionInformation.BoundingBox; }
        }
    }
}


/*
              //Collect the movement impulses.

                Vector3 movementDir;

                if (keyboardInput.IsKeyDown(Keys.E))
                {
                    movementDir = Camera.WorldMatrix.Forward;
                    totalMovement += Vector2.Normalize(new Vector2(movementDir.X, movementDir.Z));
                }
                if (keyboardInput.IsKeyDown(Keys.D))
                {
                    movementDir = Camera.WorldMatrix.Forward;
                    totalMovement -= Vector2.Normalize(new Vector2(movementDir.X, movementDir.Z));
                }
                if (keyboardInput.IsKeyDown(Keys.S))
                {
                    movementDir = Camera.WorldMatrix.Left;
                    totalMovement += Vector2.Normalize(new Vector2(movementDir.X, movementDir.Z));
                }
                if (keyboardInput.IsKeyDown(Keys.F))
                {
                    movementDir = Camera.WorldMatrix.Right;
                    totalMovement += Vector2.Normalize(new Vector2(movementDir.X, movementDir.Z));
                }
                if (totalMovement == Vector2.Zero)
                    CharacterController.HorizontalMotionConstraint.MovementDirection = Vector2.Zero;
                else
                    CharacterController.HorizontalMotionConstraint.MovementDirection = Vector2.Normalize(totalMovement);

                CharacterController.StanceManager.DesiredStance = keyboardInput.IsKeyDown(Keys.Z) ? Stance.Crouching : Stance.Standing;

                //Jumping
                if (previousKeyboardInput.IsKeyUp(Keys.A) && keyboardInput.IsKeyDown(Keys.A))
                {
                    CharacterController.Jump();
                }
#endif

   

*/