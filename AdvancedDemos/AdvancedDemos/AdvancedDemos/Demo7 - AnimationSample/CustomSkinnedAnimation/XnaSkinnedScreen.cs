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
using PloobsProjectTemplate;
using SkinnedModel;

namespace AdvancedDemo4._0
{
    [PloobsEngine.TestSuite.TesteVisualScreen]
    public class XnaSkinnedScreen : IScene
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
            
            {
                GhostObject GhostObject = new PloobsEngine.Physics.Bepu.GhostObject();
                SkinnedIObject SkinnedIObject = new SkinnedIObject(factory, "Model/Dude/dude", GhostObject);
                this.World.AddObject(SkinnedIObject);
                
                AnimationClip clip = SkinnedIObject.SkinnedModel.SkinningData.AnimationClips["Take 001"];
                SkinnedIObject.SkinnedModel.AnimationPlayer.StartClip(clip);

                SkinnedIObject.ForwardSkinnedShader.SkinnedEffect.EnableDefaultLighting();
                SkinnedIObject.ForwardSkinnedShader.SkinnedEffect.SpecularColor = new Vector3(0.25f);
                SkinnedIObject.ForwardSkinnedShader.SkinnedEffect.SpecularPower = 16;
            }




            ///add a camera
            this.World.CameraManager.AddCamera(new CameraFirstPerson(MathHelper.ToRadians(50), MathHelper.ToRadians(-10), new Vector3(50), GraphicInfo));
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
            render.RenderTextComplete("Demo: Using Custom Animation API with Ploobs", new Vector2(10, 15), Color.Red, Matrix.Identity);
            render.RenderTextComplete("In this demo, we used the XNA SkinningSample from Microsoft", new Vector2(10, 35), Color.Red, Matrix.Identity);
        }

    }
}
