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
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Modelo.Animation;
using EngineTestes;
using PloobsEngine.Input;

namespace PloobsEnginePhone7Template
{
    public class AnimationScreen1 : IScene
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

        AnimatedController arobo;        
        RasterizerState RasterizerState;
        String[] anims = new string[] { "Idle", "Aim"};
        int aindex = 1;
        private bool isWireFrame
        {
            set;
            get;
        }

        public void MultiplySpeed(float value)
        {
            arobo.Speed = value ;
        }

        public void TogleWireFrame()
        {
            isWireFrame = !isWireFrame;
        }
        public void TogleAnimation()
        {
            if (mindex == 0)
            {
                arobo.ChangeAnimation(anims[aindex], AnimationChangeMode.Blend);
                aindex = (aindex + 1) % 2;
            }       
        }       

        public void TogleRotation()
        {
            rotate = !rotate;
        }

        int lightindexstup =  1;
        public void TogleIllumination()
        {
            sas.SkinnedEffect.PreferPerPixelLighting = true;

            switch (lightindexstup)
            {
                case 0:
                    {
                        sas.SkinnedEffect.EnableDefaultLighting();
                        break;
                    }
                case 1:
                    {
                        sas.SkinnedEffect.DirectionalLight0.Enabled = true; // turn on the lighting subsystem.
                        sas.SkinnedEffect.DirectionalLight0.DiffuseColor = new Vector3(0.1f, 0.02f, 0); // a red light
                        sas.SkinnedEffect.DirectionalLight0.Direction = new Vector3(1, 0, 0);  // coming along the x-axis
                        sas.SkinnedEffect.DirectionalLight0.SpecularColor = new Vector3(0, 0, 1); // with green highlights
                        sas.SkinnedEffect.DiffuseColor = Color.White.ToVector3();
                        sas.SkinnedEffect.AmbientLightColor = new Vector3(0.1f, 0.2f, 0.1f);
                        sas.SkinnedEffect.EmissiveColor = new Vector3(0.3f, 0, 0);
                        break;
                    }
                case 2:
                    {
                        sas.SkinnedEffect.DirectionalLight0.Enabled = true; // turn on the lighting subsystem.
                        sas.SkinnedEffect.DirectionalLight0.DiffuseColor = new Vector3(0.1f, 0, 0); // a red light
                        sas.SkinnedEffect.DirectionalLight0.Direction = new Vector3(1, 0, 0);  // coming along the x-axis
                        sas.SkinnedEffect.DirectionalLight0.SpecularColor = new Vector3(0, 1, 0); // with green highlights
                        sas.SkinnedEffect.DiffuseColor = Color.White.ToVector3();
                        sas.SkinnedEffect.AmbientLightColor = new Vector3(0.1f, 0.1f, 0.1f);
                        sas.SkinnedEffect.EmissiveColor = new Vector3(0.2f, 0, 0);
                        break;
                    }
                case 3:
                    {
                        sas.SkinnedEffect.DirectionalLight0.Enabled = false; // turn on the lighting subsystem.
                        sas.SkinnedEffect.DirectionalLight0.DiffuseColor = new Vector3(0.3f, 0, 0); // a red light
                        sas.SkinnedEffect.DirectionalLight0.Direction = new Vector3(1, 0, 0);  // coming along the x-axis
                        sas.SkinnedEffect.DirectionalLight0.SpecularColor = new Vector3(0, 1, 0); // with green highlights
                        sas.SkinnedEffect.DiffuseColor = Color.White.ToVector3();
                        sas.SkinnedEffect.AmbientLightColor = new Vector3(0.1f, 0.1f, 0.1f);
                        sas.SkinnedEffect.EmissiveColor = new Vector3(0.2f, 0, 0);
                        
                        sas.SkinnedEffect.DirectionalLight1.Enabled = true; // turn on the lighting subsystem.
                        sas.SkinnedEffect.DirectionalLight1.DiffuseColor = new Vector3(0.1f, 0.1f, 0.1f); // a red light
                        sas.SkinnedEffect.DirectionalLight1.Direction = new Vector3(0, 0, -1);  // coming along the x-axis
                        sas.SkinnedEffect.DirectionalLight1.SpecularColor = new Vector3(0.6f, 0.3f, 0.4f); // with green highlights
                        sas.SkinnedEffect.DiffuseColor = Color.White.ToVector3();
                        sas.SkinnedEffect.AmbientLightColor = new Vector3(0.1f, 0.1f, 0.1f);
                        sas.SkinnedEffect.EmissiveColor = new Vector3(0.1f, 0, 0.1f);
                        break;
                    }
                default:
                    break;
            }

            lightindexstup = (lightindexstup + 1) % 4;
           
        }

        bool rotate = true;
        ForwardSimpleAnimationShader sas;
        IObject marine;

        private void CreateSpider()
        {
            ///carrega o Modelo
            AnimatedModel am = new AnimatedModel(GraphicFactory, "..\\Content\\Model\\EnemyBeast", "..\\Content\\Textures\\EnemyBeastDiffuse");
            ///Inicializa o Controlador (Idle eh o nome da animacao inicial)
            arobo = new AnimatedController(am, "Take 001");
            //arobo.isLoop = true;               

            ///Cria o shader e o material animados 
            sas = new ForwardSimpleAnimationShader(arobo);
            ForwardAnimatedMaterial amat = new ForwardAnimatedMaterial(arobo, sas);
            marine = new IObject(amat, am, new GhostObject(new Vector3(0, -30, 0), Matrix.CreateRotationX(MathHelper.ToRadians(30)), Vector3.One * 0.4f));

            ///Adiciona no mundo
            this.World.AddObject(marine);                  
          
        }
        private void createmarine()
        {
            ///carrega o Modelo
            AnimatedModel am = new AnimatedModel(GraphicFactory, "..\\Content\\Model\\PlayerMarine", "..\\Content\\Textures\\PlayerMarineDiffuse");
            ///Inicializa o Controlador (Idle eh o nome da animacao inicial)
            arobo = new AnimatedController(am, "Idle");
            //arobo.isLoop = true;               

            ///Cria o shader e o material animados 
            sas = new ForwardSimpleAnimationShader(arobo);
            ForwardAnimatedMaterial amat = new ForwardAnimatedMaterial(arobo, sas);
            marine = new IObject(amat, am, new GhostObject(new Vector3(0, -100, 0), Matrix.Identity, Vector3.One * 5));

            ///Adiciona no mundo
            this.World.AddObject(marine);
        }

        int mindex = 0;
        public void TogleModel()
        {
            switch (mindex)
            {
                case 0:
                    {
                        this.World.RemoveObject(marine);
                        CreateSpider();
                        break;
                    }                    
                case 1:
                    {
                        this.World.RemoveObject(marine);
                        createmarine();
                        break;
                    }
                default:
                    break;
            }
            mindex = (mindex + 1) % 2;
        }

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);
            {
                createmarine();
            }

            RotatingCamera cam = new RotatingCamera(this);
            this.World.CameraManager.AddCamera(cam);

            RasterizerState = new RasterizerState();
            RasterizerState.FillMode = FillMode.WireFrame;
        }

        protected override void Update(GameTime gameTime)
        {
            if(rotate)
                marine.PhysicObject.Rotation = marine.PhysicObject.Rotation * Matrix.CreateRotationY(MathHelper.ToRadians(1)); 
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            if (isWireFrame)
            {
                render.PushRasterizerState(RasterizerState);
            }

            base.Draw(gameTime, render);

            if (isWireFrame)
            {
                render.PopRasterizerState();
            }
        }

    }
}
