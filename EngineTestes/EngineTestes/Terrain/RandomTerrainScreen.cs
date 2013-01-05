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

namespace EngineTestes
{
    public class RandomTerrainScreen : IScene
    {
        private Microsoft.Xna.Framework.Graphics.Texture2D perlim;
        private TerrainObject to;
        private SegmentInterceptInfo ri;
        protected override void SetWorldAndRenderTechnich(out IRenderTechnic renderTech, out IWorld world)
        {
            world = new IWorld(new BepuPhysicWorld(), new SimpleCuller());

            DeferredRenderTechnicInitDescription desc = DeferredRenderTechnicInitDescription.Default();
            desc.DefferedDebug = false;
            desc.UseFloatingBufferForLightMap = true;            
            renderTech = new DeferredRenderTechnic(desc);
        }
 
        protected override void InitScreen(GraphicInfo GraphicInfo, EngineStuff engine)
        {
            base.InitScreen(GraphicInfo, engine);

            SkyBox skybox = new SkyBox();
            engine.AddComponent(skybox);

            InputAdvanced ia = new InputAdvanced();
            engine.AddComponent(ia);
        }

        protected override void LoadContent(GraphicInfo GraphicInfo, GraphicFactory factory ,IContentManager contentManager)
        {

            base.LoadContent(GraphicInfo,factory, contentManager);


            Picking picking = new Picking(this);
            this.AddScreenUpdateable(picking);

            perlim = factory.CreateTexture2DPerlinNoise(512, 512, 0.03f, 3f, 0.5f, 1);
            to = new TerrainObject(factory,perlim, Vector3.Zero, Matrix.Identity, MaterialDescription.DefaultBepuMaterial(), 2, 2);
            //TerrainObject to = new TerrainObject(factory,"..\\Content\\Textures\\Untitled",Vector3.Zero,Matrix.Identity,MaterialDescription.DefaultBepuMaterial(),2,1);
            TerrainModel stm = new TerrainModel(factory, to,"TerrainName","..\\Content\\Textures\\Terraingrass", "..\\Content\\Textures\\rock", "..\\Content\\Textures\\sand", "..\\Content\\Textures\\snow");
            DeferredTerrainShader shader = new DeferredTerrainShader(TerrainType.MULTITEXTURE);
            DeferredMaterial mat = new DeferredMaterial(shader);
            IObject obj3 = new IObject(mat, stm, to);
            this.World.AddObject(obj3);

            
        
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

            this.World.CameraManager.AddCamera(new CameraFirstPerson(true,GraphicInfo));

            SkyBoxSetTextureCube stc = new SkyBoxSetTextureCube("Textures//cubemap");
            CommandProcessor.getCommandProcessor().SendCommandAssyncronous(stc);

            ///O PICKING FUNCIONA APENAS COM OBJETOS QUE TENHAM CORPO FISICO REAL !!!
            ///OS GHOST E OS DUMMY NUNCA SERAO SELECIONADOS
            ///Para ser informado a todo frame sobre as colisoes do raio, basta utilizar o outro construtor
            picking.OnPickedLeftButton += new OnPicked(onPick); 

        }
        /// <summary>
        /// Called when picking happens
        /// </summary>
        /// <param name="SegmentInterceptInfo">The segment intercept info.</param>
        void onPick(SegmentInterceptInfo SegmentInterceptInfo)
        {

            IObject obj = SegmentInterceptInfo.PhysicObject.ObjectOwner;
            if (obj != null)
            {
               
                this.ri = SegmentInterceptInfo;                
            }
        }
        protected override void Draw(GameTime gameTime, RenderHelper render)
        {
           
            
            base.Draw(gameTime, render);

            if (ri != null)
            {
                render.RenderTextComplete("Position " + ri.ImpactPosition, new Vector2(20, 40), Color.White, Matrix.Identity);
                render.RenderTextComplete("Interpolated Lite " +new Vector3(ri.ImpactPosition.X,to.getLiteHeight(ri.ImpactPosition.X,ri.ImpactPosition.Z),ri.ImpactPosition.Z) , new Vector2(20, 60), Color.White, Matrix.Identity);
                render.RenderTextComplete("Interpolated " + new Vector3(ri.ImpactPosition.X, to.getHeightFast(ri.ImpactPosition.X, ri.ImpactPosition.Z), ri.ImpactPosition.Z), new Vector2(20, 80), Color.White, Matrix.Identity);
            }
        }

    }
}
