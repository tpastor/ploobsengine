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

namespace AdvancedDemo4._0
{
    /// <summary>
    /// Terrain Sample
    /// </summary>
    public class TerrainScreen : IScene
    {

        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            BepuPhysicWorld BepuPhysicWorld = new BepuPhysicWorld(-0.97f);
            SimpleCuller SimpleCuller = new SimpleCuller();
            world = new IWorld(BepuPhysicWorld, SimpleCuller);

            ForwardRenderTecnichDescription desc = ForwardRenderTecnichDescription.Default();
            desc.BackGroundColor = Color.CornflowerBlue;
            renderTech = new ForwardRenderTecnich(desc);
        }

        
        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory, IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            ///Create the Terrain Object
            ///Controls how the heigh map is loaded
            TerrainObject to = new TerrainObject(factory, "..\\Content\\Textures\\Untitled", Vector3.Zero, Matrix.Identity, MaterialDescription.DefaultBepuMaterial(), 1, 1,10);
            ///Create the Model using the Terrain Object. Here we pass the textures used, in our case we are using MultiTextured Terrain so we pass lots of textures
            TerrainModel stm = new TerrainModel(factory, to, "TerrainName", "..\\Content\\Textures\\Terraingrass");
            stm.SetTexture(factory.CreateTexture2DColor(1,1,Color.Green), TextureType.DIFFUSE);
            ///Create the shader
            ///In this sample we passed lots of textures, each one describe a level in the terrain, the ground is the sand and grass. the hills are rocks and the "mountains" are snow
            ///They are interpolated in the shader, you can control how using the shader parameters exposed in the DeferredTerrainShader
            ForwardXNABasicShader shader = new ForwardXNABasicShader();
            ///Deferred material
            ForwardMaterial fmaterial = new ForwardMaterial(shader);
            ///The object itself
            IObject obj = new IObject(fmaterial, stm, to);
            ///Add to the world
            this.World.AddObject(obj);

            shader.BasicEffect.EnableDefaultLighting();

            CameraFirstPerson cam = new CameraFirstPerson(GraphicInfo.Viewport);
            this.World.CameraManager.AddCamera(cam);            
        }
        
        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);
            render.RenderTextComplete("Demo 8-22:Terrain Sample", new Vector2(10, 15), Color.White, Matrix.Identity);
        }


    }
}
