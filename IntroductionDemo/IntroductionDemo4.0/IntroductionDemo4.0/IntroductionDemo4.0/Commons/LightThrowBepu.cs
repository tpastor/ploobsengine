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

namespace IntroductionDemo4._0
{
    public class LightThrowBepu
    {
        IWorld _mundo;                          
        Random rd = new Random();                
        BindMouseCommand mm0 = null;
        BindMouseCommand mm1 = null;
        GraphicFactory factory;
        IContentManager manager;

        
        public void CleanUp()
        {
            mm0.BindAction = BindAction.REMOVE;
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(mm0);

            mm1.BindAction = BindAction.REMOVE;
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(mm1);
        }        

        public LightThrowBepu(IWorld mundo,GraphicFactory factory,IContentManager manager)
        {
            this.manager = manager;
            this.factory = factory;
            _mundo = mundo;            
            {
                ///Register a function to be called when the the mouse is pressed
                InputPlaybleMouseBottom ip1 = new SimpleConcreteMouseBottomInputPlayable(StateKey.PRESS, EntityType.IOBJECT, MouseButtons.LeftButton, mousebuttonteste);
                mm0 = new BindMouseCommand(ip1, BindAction.ADD);
                CommandProcessor.getCommandProcessor().SendCommandAssyncronous(mm0);
            }
            {
                InputPlaybleMouseBottom ip = new SimpleConcreteMouseBottomInputPlayable(StateKey.PRESS, EntityType.IOBJECT, MouseButtons.RightButton, mousebuttontesteRight);
                mm1 = new BindMouseCommand(ip, BindAction.ADD);
                CommandProcessor.getCommandProcessor().SendCommandAssyncronous(mm1);
            }

        }

        public void mousebuttontesteRight(MouseState ms)
        {
            PointLight pl = new PointLight(_mundo.CameraManager.ActiveCamera.Position, StaticRandom.RandomColor(), 100, 5);
            pl.UsePointLightQuadraticAttenuation = true;
            _mundo.AddLight(pl);
        }
        
        int i = 0;
        public void mousebuttonteste(MouseState ms)
        {
            ///Create an object
            IObject physObj = SpawnPrimitive(_mundo.CameraManager.ActiveCamera.Position, Matrix.CreateRotationX(0.5f));
            physObj.PhysicObject.Velocity = (_mundo.CameraManager.ActiveCamera.Target - _mundo.CameraManager.ActiveCamera.Position) * 50.0f;
            
            ///Create a light that follow an object
            MoveablePointLight mvp = new MoveablePointLight(physObj.PhysicObject as SphereObject, new Color((float)rd.NextDouble(),(float) rd.NextDouble(),(float) rd.NextDouble()),25, 5);
            mvp.UsePointLightQuadraticAttenuation = true;                        
            ///Add them to the world
            _mundo.AddLight(mvp);
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
            SimpleModel sm2 = new SimpleModel(manager,"Model\\ball");
            sm2.SetTexture(factory.CreateTexture2DColor(1,1,Color.White,false), TextureType.DIFFUSE);            
            NormalDeferred nd = new NormalDeferred();                        
            IMaterial m = new DeferredMaterial(nd);
            SphereObject  pi2 = new SphereObject(pos, 1,1,1,MaterialDescription.DefaultBepuMaterial());
            IObject o = new IObject(m,sm2,pi2);
            return o;
        }

    }
}
