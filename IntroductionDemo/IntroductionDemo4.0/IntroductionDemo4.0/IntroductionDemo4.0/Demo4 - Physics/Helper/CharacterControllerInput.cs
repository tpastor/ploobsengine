using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PloobsEngine.SceneControl;
using PloobsEngine.Utils;
using PloobsEngine.Physics.Bepu;
using PloobsEngine;

namespace IntroductionDemo4._0
{
    /// <summary>
    /// Handles input and movement of a character in the game.
    /// Acts as a simple 'front end' for the bookkeeping and math of the character controller.
    /// </summary>
    public class CharacterControllerInput : IScreenUpdateable
    {
        /// <summary>
        /// Physics representation of the character.
        /// </summary>
        public CharacterObject Characterobj;

        /// <summary>
        /// Whether or not to use the character controller's input.
        /// </summary>
        public bool IsActive = true;

        public Keys AheadKey = Keys.T;
        public Keys BackKey = Keys.G;
        public Keys LeftKey = Keys.F;
        public Keys RightKey = Keys.H;
        public Keys JumpKey = Keys.R;
        
        public CharacterControllerInput(IScene scene, Vector3 position, float characterHeight, float characterWidth, float mass, Vector3 scale,float Yalignement = 0)
            : base(scene)
        {
            Characterobj = new CharacterObject(position, Matrix.Identity, characterHeight, characterWidth, mass, 1f, scale, Yalignement);
            this.Start();
            
        }

        protected override void  Update(GameTime gameTime)        
        {
            Update(gameTime.ElapsedGameTime.Milliseconds);
        }

        public void Update(float dt )
        {
            if (IsActive)
            {
                KeyboardState keyboardInput = Keyboard.GetState();                
                       
                Vector2 totalMovement = Vector2.Zero;
                Vector2 mv = VectorUtils.ToVector2(Characterobj.FaceVector);
                Vector2 lado = VectorUtils.Perpendicular2DNormalized(mv);

                ///TO SLIDE MOVEMENT USE
                //totalMovement += lado;
                //totalMovement -= lado;

                if (keyboardInput.IsKeyDown(AheadKey))
                {                    
                    totalMovement -= mv;
                }
                if (keyboardInput.IsKeyDown(BackKey))
                {
                    totalMovement += mv;
                }
                if (keyboardInput.IsKeyDown(RightKey))
                {                 
                 
                    Characterobj.RotateYByAngleDegrees(-1);
                }
                if (keyboardInput.IsKeyDown(LeftKey))
                {                    
                    Characterobj.RotateYByAngleDegrees(1);
                }
                if (totalMovement == Vector2.Zero)
                    Characterobj.MoveToDirection(Vector2.Zero);
                else
                {                    
                    Characterobj.MoveToDirection(Vector2.Normalize(totalMovement));
                }

                //Jumping
                if (keyboardInput.IsKeyDown(JumpKey))
                {
                    Characterobj.Jump();
                }

            }
        }
    }
}