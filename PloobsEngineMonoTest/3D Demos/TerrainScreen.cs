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
using PloobsEngine.Physic.PhysicObjects.BepuObject;
using PloobsEngine.Input;
using Microsoft.Xna.Framework.Graphics;
using PloobsEnginePhone7Template;
using PloobsEngine.Utils;
using EngineTestes;

namespace AdvancedDemo4._0
{
    /// <summary>
    /// Terrain Sample
    /// </summary>
    public class TerrainScreen : IScene
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
        }

        public void releaseobjs()
        {
            foreach (var item in objs)
            {
                item.PhysicObject.isMotionLess = !item.PhysicObject.isMotionLess;
            }
        }

        public void recreateTerrain(int h)
        {
            ///remove previous one
            this.World.RemoveObject(obj);

            ///create the new one
            createterrain(h);
        }

        void createterrain(int height = 5)
        {
            ///Create the Terrain Object
            ///Controls how the heigh map is loaded
            TerrainObject to = new TerrainObject(GraphicFactory, "..\\Content\\Textures\\hmap", new Vector3(0, -30, 0), Matrix.Identity, MaterialDescription.DefaultBepuMaterial(), 3, 3, height);

            ///Create the Model using the Terrain Object. Here we pass the textures used
            TerrainModel stm = new TerrainModel(GraphicFactory, to, "TerrainName", "..\\Content\\Textures\\Terraingrass");
            stm.SetTexture("..\\Content\\Textures\\Terraingrass", TextureType.DIFFUSE);
            ForwardXNABasicShader shader = new ForwardXNABasicShader();
            ForwardMaterial fmaterial = new ForwardMaterial(shader);
            ///The object itself
             obj = new IObject(fmaterial, stm, to);
            ///Add to the world
            this.World.AddObject(obj);

            ///light =p
            shader.BasicEffect.EnableDefaultLighting();
            shader.BasicEffect.SpecularPower = 250;
            shader.BasicEffect.PreferPerPixelLighting = true;


            //Fog =P
            shader.BasicEffect.FogEnabled = true;
            shader.BasicEffect.FogColor = new Vector3(0.1f, 0.1f, 0.1f); // Dark grey
            shader.BasicEffect.FogStart = 30;
            shader.BasicEffect.FogEnd = 300;
        }

        IObject obj;
        List<IObject> objs = new List<IObject>();
        public bool wireframe = false;
        RasterizerState RasterizerState;
        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory, IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            RasterizerState = new RasterizerState();
            RasterizerState.FillMode = FillMode.WireFrame;

            createterrain();
          

            int numColumns = 5 ;
            int numRows = 5 ;
            

            float separation = 20;
            for (int i = 0; i < numRows; i++)
                for (int j = 0; j < numColumns; j++)
                {
                        SimpleModel sm = new SimpleModel(GraphicFactory, "..\\Content\\Model\\cubo");
                        sm.SetTexture(GraphicFactory.CreateTexture2DColor(1, 1, StaticRandom.RandomColor()), TextureType.DIFFUSE);
                        MaterialDescription md = MaterialDescription.DefaultBepuMaterial();
                        BoxObject pi = new BoxObject(new Vector3(separation * i - 50, 50, separation * j - 50), 1, 1, 1, 1, new Vector3(5), Matrix.Identity, md);
                        ForwardXNABasicShader shader2 = new ForwardXNABasicShader();
                        IMaterial mat = new ForwardMaterial(shader2);
                        IObject obj5 = new IObject(mat, sm, pi);
                        this.World.AddObject(obj5);
                        shader2.BasicEffect.EnableDefaultLighting();
                        objs.Add(obj5);
                        pi.isMotionLess = true;

                    }

            CameraFirstPerson cam = new CameraFirstPerson(GraphicInfo);
            this.World.CameraManager.AddCamera(cam);
        }

        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            if (wireframe)
            {
                render.PushRasterizerState(RasterizerState);
            }
            base.Draw(gameTime, render);
            if (wireframe)
            {
                render.PopRasterizerState();
            }
            
            render.RenderTextComplete("Terrain Sample", new Vector2(10, 15), Color.White, Matrix.Identity);
        }


    }
}
