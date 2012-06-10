using System;
using PloobsEngine.SceneControl;
using PloobsEngine;
using PloobsEngine.Input;
using Microsoft.Xna.Framework;
using PloobsEngine.Physics.Bepu;
using PloobsEngine.Modelo;
using PloobsEngine.Material;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Utils;
using PloobsEngine.Commands;
using Microsoft.Xna.Framework.Input;
using PloobsEngine.Light;
using PloobsEngine.Engine;
using PloobsEngine.Physics;

namespace EngineTestes
{
    public class LightThrowForward : ICleanupAble
    {
        IWorld _mundo;                          
        Random rd = new Random();                
        BindMouseCommand mm0 = null;
        GraphicFactory factory;
        
        public void CleanUp()
        {
            mm0.BindAction = BindAction.REMOVE;
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(mm0);

        }

        public LightThrowForward(IWorld mundo, GraphicFactory factory)
        {            
            this.factory = factory;
            _mundo = mundo;            
            {
                ///Register a function to be called when the the mouse is pressed
                InputPlaybleMouseBottom ip1 = new SimpleConcreteMouseBottomInputPlayable(StateKey.PRESS, EntityType.IOBJECT, MouseButtons.LeftButton, mousebuttonteste);
                mm0 = new BindMouseCommand(ip1, BindAction.ADD);
                CommandProcessor.getCommandProcessor().SendCommandAssyncronous(mm0);
            }           

        }

        
        int i = 0;
        public void mousebuttonteste(MouseState ms)
        {
            ///Create an object
            IObject physObj = SpawnPrimitive(_mundo.CameraManager.ActiveCamera.Position, Matrix.CreateRotationX(0.5f));
            physObj.PhysicObject.Velocity = (_mundo.CameraManager.ActiveCamera.Target - _mundo.CameraManager.ActiveCamera.Position) * 15.0f;                        
            physObj.Name = "FlyingBall " + ++i;
            _mundo.AddObject(physObj);
      
        }
        /// <summary>
        /// Create a simple Sphere object
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="ori"></param>
        /// <returns></returns>
        private IObject SpawnPrimitive(Vector3 pos, Matrix ori)
        {
            ///Load a Model with a custom texture
            SimpleModel sm2 = new SimpleModel(factory,"Model\\ball");
            sm2.SetTexture(factory.CreateTexture2DColor(1,1,Color.White,false), TextureType.DIFFUSE);
            ForwardXNABasicShader nd = new ForwardXNABasicShader();                        
            IMaterial m = new ForwardMaterial(nd);
            SphereObject  pi2 = new SphereObject(pos, 1,0.5f,1,MaterialDescription.DefaultBepuMaterial());
            IObject o = new IObject(m,sm2,pi2);            
            return o;
        }


        #region ICleanupAble Members

        public void CleanUp(GraphicFactory factory)
        {
            this.CleanUp();
        }

        #endregion
    }
}
