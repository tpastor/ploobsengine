using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PloobsEngine.SceneControl;
using PloobsEngine.Utils;
using PloobsEngine.Physics.Bepu;
using PloobsEngine;

namespace EngineTestes
{
    /// <summary>
    /// Handles input and movement of a character in the game.
    /// Acts as a simple 'front end' for the bookkeeping and math of the character controller.
    /// </summary>
    public class SphereCharacterControllerInput : IScreenUpdateable
    {
        /// <summary>
        /// Physics representation of the character.
        /// </summary>
        public SphereCharacterObject Characterobj;        

        Keys frente = Keys.T;
        Keys tras = Keys.G;
        Keys direita = Keys.F;
        Keys esquerda = Keys.H;
        Keys pulo = Keys.R;

        public SphereCharacterControllerInput(IScene scene, Vector3 position, float radius, float scale, float Yalignement = 0)
            : base(scene)
        {
            Characterobj = new SphereCharacterObject(position, Matrix.Identity, radius, scale, Yalignement);
            this.Start();
        }

        protected override void  Update(GameTime gameTime)        
        {
            Update(gameTime.ElapsedGameTime.Milliseconds);
        }

        public void Update(float dt )
        {
                KeyboardState keyboardInput = Keyboard.GetState();                
                       
                Vector2 totalMovement = Vector2.Zero;
                Vector2 mv = VectorUtils.ToVector2(Characterobj.FaceVector);
                Vector2 lado = VectorUtils.Perpendicular2DNormalized(mv);

                ///TO SLIDE MOVEMENT USE
                //totalMovement += lado;
                //totalMovement -= lado;

                if (keyboardInput.IsKeyDown(frente))
                {                    
                    totalMovement -= mv;
                }
                if (keyboardInput.IsKeyDown(tras))
                {
                    totalMovement += mv;
                }
                if (keyboardInput.IsKeyDown(esquerda))
                {                 
                 
                    Characterobj.RotateYByAngleDegrees(-1);
                }
                if (keyboardInput.IsKeyDown(direita))
                {                    
                    Characterobj.RotateYByAngleDegrees(1);
                }
                //if (totalMovement == Vector2.Zero)
                //    Characterobj.MoveToDirection(Vector2.Zero);
                //else
                {
                    Characterobj.CharacterController.HorizontalMotionConstraint.MovementDirection = Vector2.Normalize(totalMovement);
                }

                //Jumping
                if (keyboardInput.IsKeyDown(pulo))
                {
                    Characterobj.Jump();
                }

            }
        }
    }
