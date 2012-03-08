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
using StillDesign.PhysX;
using Microsoft.Xna.Framework.Input;
using PloobsEngine.Utils;

namespace EngineTestes
{
    public class PhysxCharacter28Screen : IScene
    {

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            PhysxPhysicWorld PhysxPhysicWorld = new PhysxPhysicWorld(new Vector3(0,-10,0),true);            
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
                SimpleModel simpleModel = new SimpleModel(factory, "Model//cenario");

                PhysxTriangleMesh tmesh = new PhysxTriangleMesh(PhysxPhysicWorld, simpleModel,
                    Matrix.Identity, Vector3.One);

                ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);
            }

            
            {

                BoxControllerDescription desc = new BoxControllerDescription(1, 1, 1);                
                desc.UpDirection = Axis.Y;
                desc.SlopeLimit = 0;
                desc.SkinWidth = 0.2f;
                desc.StepOffset = 0.5f;
                desc.InteractionFlag = CCTInteractionFlag.Include | CCTInteractionFlag.UseFilter;                

                SimpleModel sm = new SimpleModel(factory, "..\\Content\\Model\\block");
                sm.SetTexture(factory.CreateTexture2DColor(1, 1, Color.Blue), TextureType.DIFFUSE);
                ForwardXNABasicShader shader = new ForwardXNABasicShader(ForwardXNABasicShaderDescription.Default());
                ForwardMaterial mat = new ForwardMaterial(shader);

                PhysxBoxCharacterObject PhysxCapsuleCharacterObject = new PhysxBoxCharacterObject(desc, new Vector3(50) ,Matrix.Identity, Vector3.One * 5);
                IObject marine = new IObject(mat, sm, PhysxCapsuleCharacterObject);
                this.World.AddObject(marine);
                marine.OnUpdate += new OnUpdate(marine_OnUpdate);
            }

            BallThrowPhysx28 BallThrowBullet = new BallThrowPhysx28(this.World, GraphicFactory);

            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));
        }

        void marine_OnUpdate(IObject obj, GameTime gt, ICamera cam)
        {
            PhysxBoxCharacterObject PhysxBoxCharacterObject = obj.PhysicObject as PhysxBoxCharacterObject;
            KeyboardState keyboardInput = Keyboard.GetState();
            if (keyboardInput.IsKeyDown(Keys.Y))
            {
                PhysxBoxCharacterObject.Controller.Move(PhysxBoxCharacterObject.FaceVector.AsPhysX());            
            }
            if (keyboardInput.IsKeyDown(Keys.H))
            {
                PhysxBoxCharacterObject.Controller.Move(-PhysxBoxCharacterObject.FaceVector.AsPhysX());
            }            
            if (keyboardInput.IsKeyDown(Keys.J))
            {
                PhysxBoxCharacterObject.RotateYByAngleDegrees(-1);
            }
            if (keyboardInput.IsKeyDown(Keys.G))
            {
                PhysxBoxCharacterObject.RotateYByAngleDegrees(1);
            }

        }

    }
}
