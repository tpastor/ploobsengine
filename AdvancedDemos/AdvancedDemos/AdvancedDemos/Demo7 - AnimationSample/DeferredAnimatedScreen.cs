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

namespace AdvancedDemo4._0
{
    /// <summary>
    /// Animation Screen
    /// </summary>
    public class DeferredAnimatedScreen : IScene
    {
        LightThrowBepu lt;

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(-0.98f,true,1), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();            
            desc.UseFloatingBufferForLightMap = true;
            renderTech = new DeferredRenderTechnic(desc);
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

            var a = AppDomain.CurrentDomain.GetAssemblies();

            IObject marine;
            AnimatedController marinecontroler;
            {
                ///carrega o Modelo
                AnimatedModel am = new AnimatedModel(factory, "..\\Content\\Model\\PlayerMarine", "..\\Content\\Textures\\PlayerMarineDiffuse");
                ///Inicializa o Controlador (Idle eh o nome da animacao inicial)
                marinecontroler = new AnimatedController(am, "Idle");

                ///Cria o shader e o material animados 
                DeferredSimpleAnimationShader sas = new DeferredSimpleAnimationShader(marinecontroler);
                DeferredAnimatedMaterial amat = new DeferredAnimatedMaterial(marinecontroler, sas);

                CharacterControllerInput gp = new CharacterControllerInput(this, new Vector3(100, 50, 1), 25, 10, 10, Vector3.One);

                marine = new IObject(amat, am, gp.Characterobj);
                ///Adiciona no mundo
                this.World.AddObject(marine);

                lt = new LightThrowBepu(this.World, factory);
            }

            {
                ///carrega o Modelo
                AnimatedModel am = new AnimatedModel(factory, "..\\Content\\Model\\WeaponMachineGun", "..\\Content\\Textures\\WeaponMachineGunDiffuse");
                ///Inicializa o Controlador (Idle eh o nome da animacao inicial)
                AnimatedController arobo = new AnimatedController(am);
                //arobo.isLoop = true;               

                ///Cria o shader e o material animados 
                DeferredSimpleAnimationShader sas = new DeferredSimpleAnimationShader(arobo, marine, marinecontroler, "R_Hand2");
                DeferredAnimatedMaterial amat = new DeferredAnimatedMaterial(arobo, sas);
                IObject gun = new IObject(amat, am, new GhostObject(new Vector3(0, 0, 0), Matrix.Identity, Vector3.One));

                ///Adiciona no mundo
                this.World.AddObject(gun);


            }

            {
                SimpleModel simpleModel = new SimpleModel(factory, "Model//cenario");
                TriangleMeshObject tmesh = new TriangleMeshObject(simpleModel, Vector3.Zero, Matrix.Identity, Vector3.One, MaterialDescription.DefaultBepuMaterial());
                DeferredNormalShader shader = new DeferredNormalShader();
                DeferredMaterial fmaterial = new DeferredMaterial(shader);
                IObject obj = new IObject(fmaterial, simpleModel, tmesh);
                this.World.AddObject(obj);
            }

            #region NormalLight
            DirectionalLightPE ld1 = new DirectionalLightPE(Vector3.Left, Color.White);
            DirectionalLightPE ld2 = new DirectionalLightPE(Vector3.Right, Color.White);
            DirectionalLightPE ld3 = new DirectionalLightPE(Vector3.Backward, Color.White);
            DirectionalLightPE ld4 = new DirectionalLightPE(Vector3.Forward, Color.White);
            DirectionalLightPE ld5 = new DirectionalLightPE(Vector3.Down, Color.White);
            float li = 0.5f;
            ld1.LightIntensity = li;
            ld2.LightIntensity = li;
            ld3.LightIntensity = li;
            ld4.LightIntensity = li;
            ld5.LightIntensity = li;
            this.World.AddLight(ld1);
            this.World.AddLight(ld2);
            this.World.AddLight(ld3);
            this.World.AddLight(ld4);
            this.World.AddLight(ld5);
            #endregion

            

            CameraFirstPerson cam = new CameraFirstPerson(MathHelper.ToRadians(-60), MathHelper.ToRadians(-20), new Vector3(30, 50, 50), GraphicInfo);
            cam.MoveSpeed *= 5;
            this.World.CameraManager.AddCamera(cam);

            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//grasscube");
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);
        }

        protected override void CleanUp(EngineStuff engine)
        {
            lt.CleanUp();
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
