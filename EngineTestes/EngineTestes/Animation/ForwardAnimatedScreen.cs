using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine.SceneControl;
using PloobsEngine.Physics;
using PloobsEngine.Modelo;
using PloobsEngine.Material;
using PloobsEngine.Engine;
using PloobsEngine.Physics.Bepu;
using Microsoft.Xna.Framework;
using PloobsEngine.Cameras;
using PloobsEngine.Light;
using Microsoft.Xna.Framework.Input;
using PloobsEngine.Features;
using PloobsEngine.Commands;
using PloobsEngine.Loader;
using PloobsEngine.Modelo.Animation;
using PloobsEngine.Input;
using EngineTestes;

namespace AdvancedDemo4._0
{
    /// <summary>
    /// Animation Screen
    /// </summary>
    public class ForwardAnimatedScreen : IScene
    {
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            BepuPhysicWorld BepuPhysicWorld = new BepuPhysicWorld(-9.7f);
            SimpleCuller SimpleCuller = new SimpleCuller();
            world = new IWorld(BepuPhysicWorld, SimpleCuller);

            ForwardRenderTecnichDescription desc = ForwardRenderTecnichDescription.Default();
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new ForwardRenderTecnich(desc);
        }

        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);

            SkyBox skybox = new SkyBox();
            engine.AddComponent(skybox);
        }

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            IObject marine;
            AnimatedController marinecontroler;
            {
                ///carrega o Modelo
                AnimatedModel am = new AnimatedModel(factory, "..\\Content\\Model\\PlayerMarine", "..\\Content\\Textures\\PlayerMarineDiffuse");
                ///Inicializa o Controlador (Idle eh o nome da animacao inicial)
                marinecontroler = new AnimatedController(am, "Idle");

                ///Cria o shader e o material animados 
                ForwardSimpleAnimationShader sas = new ForwardSimpleAnimationShader(marinecontroler);
                ForwardAnimatedMaterial amat = new ForwardAnimatedMaterial(marinecontroler, sas);
                marine = new IObject(amat, am, new GhostObject(new Vector3(0, -30, 0), Matrix.CreateRotationX(MathHelper.ToRadians(30)), Vector3.One * 0.4f));

                ///Adiciona no mundo
                this.World.AddObject(marine);                  

                CharacterControllerInput gp = new CharacterControllerInput(this, new Vector3(100, 50, 1), 25, 10, 10, Vector3.One);
                marine = new IObject(amat, am, gp.Characterobj);
                ///Adiciona no mundo
                this.World.AddObject(marine);

            }

            {
                ///carrega o Modelo
                AnimatedModel am = new AnimatedModel(factory, "..\\Content\\Model\\WeaponMachineGun", "..\\Content\\Textures\\WeaponMachineGunDiffuse");
                ///Inicializa o Controlador (Idle eh o nome da animacao inicial)
                AnimatedController arobo = new AnimatedController(am);
                //arobo.isLoop = true;               
                                
                ///Cria o shader e o material animados 
                ForwardSimpleAnimationShader sas = new ForwardSimpleAnimationShader(arobo, marine, marinecontroler, "R_Hand2");
                ForwardAnimatedMaterial amat = new ForwardAnimatedMaterial(arobo, sas);
                IObject gun = new IObject(amat, am, new GhostObject(new Vector3(0, 0, 0), Matrix.Identity, Vector3.One));

                ///Adiciona no mundo
                this.World.AddObject(gun);


            }

            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//cenario");
                TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
                ForwardXNABasicShader shader = new ForwardXNABasicShader();
                ForwardMaterial fmaterial = new ForwardMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);
            }


            CameraFirstPerson cam = new CameraFirstPerson(MathHelper.ToRadians(-60), MathHelper.ToRadians(-20), new Vector3(30, 50, 50), GraphicInfo);
            cam.MoveSpeed *= 5;
            this.World.CameraManager.AddCamera(cam);

            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//grasscube");
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);
        }

        protected override void CleanUp(EngineStuff engine)
        {
            engine.RemoveComponent("SkyBox");
            base.CleanUp(engine);
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);

            render.RenderTextComplete("Demo 14-22:Animation Sample - Not Sync with the Walk cycle", new Vector2(10, 15), Color.White, Matrix.Identity);
            render.RenderTextComplete("Use TFGH to control the character", new Vector2(10, 35), Color.White, Matrix.Identity);            
            render.RenderTextComplete("R to jump", new Vector2(10, 55), Color.White, Matrix.Identity);
        }

    }
}
