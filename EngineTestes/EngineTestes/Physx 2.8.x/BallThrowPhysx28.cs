using System;
using PloobsEngine.SceneControl;
using PloobsEngine.Input;
using Microsoft.Xna.Framework;
using PloobsEngine.Modelo;
using PloobsEngine.Material;
using PloobsEngine.Commands;
using Microsoft.Xna.Framework.Input;
using PloobsEngine.Engine;
using PloobsEngine.Physics;
using StillDesign.PhysX;

namespace EngineTestes
{
    public class BallThrowPhysx28
    {
        IWorld _mundo;                          
        Random rd = new Random();                
        BindMouseCommand mm0 = null;
        BindMouseCommand mm1 = null;
        GraphicFactory factory;
        
        public void CleanUp()
        {
            mm0.BindAction = BindAction.REMOVE;
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(mm0);

            mm1.BindAction = BindAction.REMOVE;
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(mm1);
        }

        bool forward = true;
        public BallThrowPhysx28(IWorld mundo, GraphicFactory factory, bool forwardrender = true)
        {
            this.forward = forwardrender;
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
            _mundo.AddObject(physObj);
            physObj.PhysicObject.Velocity = (_mundo.CameraManager.ActiveCamera.Target - _mundo.CameraManager.ActiveCamera.Position) * 100.0f;
            physObj.Name = "FlyingBall " + ++i;
            
      
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
            IMaterial m;
            if (forward)
            {
                ForwardXNABasicShader nd = new ForwardXNABasicShader();
                m = new ForwardMaterial(nd);
            }
            else
            {
                DeferredNormalShader nd = new DeferredNormalShader();
                m = new DeferredMaterial(nd);
            }

            PhysxPhysicWorld PhysxPhysicWorld = _mundo.PhysicWorld as PhysxPhysicWorld;

            SphereShapeDescription SphereGeometry = new SphereShapeDescription(5f);
            PhysxPhysicObject PhysxPhysicObject = new PhysxPhysicObject(SphereGeometry,
                0.5f, Matrix.CreateTranslation(pos), Vector3.One * 5f);
            
            IObject o = new IObject(m, sm2, PhysxPhysicObject);
            return o;
        }

    }
}
