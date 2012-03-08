using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using PloobsEngine.Modelo;
using PloobsEngine.Material;
using PloobsEngine.Engine;
using Microsoft.Xna.Framework;
using PloobsEngine.Cameras;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Physics;
using StillDesign.PhysX.Samples;
using Microsoft.Xna.Framework.Input;
using StillDesign.PhysX;

namespace EngineTestes
{
    public class Physx28VehicleScreen : IScene
    {

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            PhysxPhysicWorld PhysxPhysicWorld = new PhysxPhysicWorld(new Vector3(0,-10,0));            
            world = new IWorld(PhysxPhysicWorld, new SimpleCuller());

            ForwardRenderTecnichDescription desc = ForwardRenderTecnichDescription.Default();
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new ForwardRenderTecnich(desc);
        }

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            PhysxPhysicWorld PhysxPhysicWorld = World.PhysicWorld as PhysxPhysicWorld;

            base.LoadContent(GraphicInfo, factory, contentManager);
            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//block");
                simpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Blue), TextureType.DIFFUSE);

                BoxShapeDescription SphereGeometry = new BoxShapeDescription(1000, 5, 1000);
                PhysxPhysicObject PhysxPhysicObject = new PhysxPhysicObject(SphereGeometry,
                    Matrix.Identity, new Vector3(1000, 5, 1000));

                ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, PhysxPhysicObject);
                this.World.AddObject(obj);

                shader.BasicEffect.EnableDefaultLighting();
            }

            {
                ///very basic vehicle !!!
                ///no wheels also =P
                Vehicle Vehicle = new Vehicle(PhysxPhysicWorld.Scene);
                SimpleModel simpleModel = new SimpleModel(factory, "Model//block");
                simpleModel.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Green));
                PhysxPhysicObject tmesh = new PhysxPhysicObject(Vehicle.VehicleBodyActor, new Vector3(5, 3, 7));

                ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                UserObject<Vehicle> obj = new UserObject<Vehicle>(fmaterial, simpleModel, tmesh,Vehicle);
                obj.OnUserUpdate += new Action<UserObject<StillDesign.PhysX.Samples.Vehicle>>(obj_OnUserUpdate);
                this.World.AddObject(obj);

            }

            BallThrowPhysx28 BallThrowBullet = new BallThrowPhysx28(this.World, GraphicFactory);

            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));
        }

        void obj_OnUserUpdate(UserObject<Vehicle> obj)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                obj.UserData.Accelerate(10);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
            {
                obj.UserData.Accelerate(-10);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.J))
            {
                obj.UserData.Turn(MathHelper.ToRadians(1));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.H))
            {
                obj.UserData.Turn(MathHelper.ToRadians(-1));
            }
        }

    }
}
