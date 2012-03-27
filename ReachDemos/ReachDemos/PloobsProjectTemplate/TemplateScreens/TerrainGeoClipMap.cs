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

namespace ProjectTemplate
{
    /// <summary>
    /// Basic Forward Screen
    /// </summary>
    public class TerrainGeoClipMap : IScene
    {
        /// <summary>
        /// Sets the world and render technich.
        /// </summary>
        /// <param name="renderTech">The render tech.</param>
        /// <param name="world">The world.</param>
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            ///create the IWorld
            world = new IWorld(new BepuPhysicWorld(-0.97f, true, 1), new SimpleCuller());

            ///Create the deferred technich
            ForwardRenderTecnichDescription desc = new ForwardRenderTecnichDescription();
            desc.BackGroundColor = Color.AliceBlue;
            renderTech = new ForwardRenderTecnich(desc);
        }

        /// <summary>
        /// Load content for the screen.
        /// </summary>
        /// <param name="GraphicInfo"></param>
        /// <param name="factory"></param>
        /// <param name="contentManager"></param>
        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory, IContentManager contentManager)
        {
            base.LoadContent(GraphicInfo, factory, contentManager);

            
            ///Forward Shader (look at this shader construction for more info)
            TerrainMaterial material = new TerrainMaterial(factory, "Terrain//HeightMap");
            ///The object itself
            TerrainObject to = new TerrainObject(factory, Vector3.Zero, Matrix.Identity, material.Model.GetHeights(), MaterialDescription.DefaultBepuMaterial());
            IObject obj = new IObject(material, null, to);
            ///Add to the world
            this.World.AddObject(obj); 

            ///add a camera
            this.World.CameraManager.AddCamera(new CameraFirstPerson(GraphicInfo));

            Texture2D t = factory.CreateTexture2DColor(1,1,Color.Red);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                     SimpleModel simpleModel = new SimpleModel(factory, "Model/block");
                     simpleModel.SetTexture(t);
                    ///Physic info (position, rotation and scale are set here)
                     BoxObject tmesh = new BoxObject(new Vector3(j * 10 + 200,100,i * 10 + 200), 1, 1, 1, 10, Vector3.One * 2, Matrix.Identity, MaterialDescription.DefaultBepuMaterial());
                    ///Forward Shader (look at this shader construction for more info)
                    ForwardXNABasicShader shader = new ForwardXNABasicShader();
                    ///Forward material
                    ForwardMaterial fmaterial = new ForwardMaterial(shader);
                    ///The object itself
                    obj = new IObject(fmaterial, simpleModel, tmesh);
                    ///Add to the world
                    this.World.AddObject(obj); 

                }
            }

        }

        /// <summary>
        /// This is called when the screen should draw itself.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="render"></param>
        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
            base.Draw(gameTime, render);

            ///Draw some text on the screen
            render.RenderTextComplete("Demo: GeoClipMap Terrain", new Vector2(10, 15), Color.Red, Matrix.Identity);
        }

    }
}
