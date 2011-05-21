using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PloobsEngine.Modelo;
using PloobsEngine.SceneControl;
using PloobsEngine.Cameras;
using PloobsEngine.Light;

namespace PloobsEngine.Material
{

    public enum BilboardType
    {
        Cilindric,Spherical
    }

    /// <summary>
    /// Animated Bilboard.   
    /// Each one uses a different Instance of this shader (Clonned)
    /// bitstrip pra gerar imagens horizontais - animacao
    /// </summary>
    public class DeferredAnimatedTextureShader : IShader
    {
        /// <summary>
        /// Bilboard animada
        /// </summary>
        /// <param name="numFrames">numero de frames que tem na textura</param>
        /// <param name="msBetweenFrames">tempo entre frames em milisegundos</param>
        public DeferredAnimatedTextureShader(int numFrames, int msBetweenFrames, BilboardType bilboardType)
        {
            this.bilboardType = bilboardType;
            this.numberOfFrames = numFrames;
            this.msBetweenFrames = msBetweenFrames;            
        }

        BilboardType bilboardType;
        private Texture2D aniTex;
        private int CurrentFrame = 0;
        private int numberOfFrames = 0;
        private int msBetweenFrames = 0;
        float timer = 0;
        private int width = 0;
        private int height = 0;
        float totalwidth;
        float size ;
        float start;
        
        private Effect _shader;
        private Vector2 scale = new Vector2(100, 100);

        /// <summary>
        /// Bilboard tem tamanho 1,1 -> deve-se ser multiplicada por um fator de escala
        /// Default Vector2(100, 100);
        /// </summary>
        public Vector2 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        private Vector4 atenuation = Vector4.One;

        /// <summary>
        /// Atenuacao na cor da textura utilizada
        /// Default Vector4.One
        /// </summary>
        public Vector4 Atenuation
        {
            get { return atenuation; }
            set { atenuation = value; }
        }

        private Vector3 allowRotDir = new Vector3(0,1,0);

        /// <summary>
        /// Constraint da bilboard IF CILINDRIC
        /// SPHERICAL DOES NOT HAVE CONSTRAINT DIRECTION
        /// Default Vector3(0,1,0)
        /// </summary>
        public Vector3 AllowRotDir
        {
            get { return allowRotDir; }
            set { allowRotDir = value; }
        }

        public override MaterialType MaterialType
        {
            get { return MaterialType.DEFERRED; }
        }

        private float amplitude = 1;

        /// <summary>
        /// Amplitude do movimento (os dois pontos de cima da billboard podem se movimentar)
        /// Default 1
        /// </summary>
        public float Amplitude
        {
            get { return amplitude; }
            set { amplitude = value; }
        }

        private float movimentSpeedControl = 5;

        /// <summary>
        /// Velocidade de moviemnto
        /// Default 5
        /// </summary>
        public float MovimentSpeedControl
        {
            get { return movimentSpeedControl; }
            set { movimentSpeedControl = value; }
        }        

        public override void  Update(GameTime gt, IObject ent, IList<ILight> lights)
        {
 	        base.Update(gt, ent, lights);
            timer += gt.ElapsedGameTime.Milliseconds;

            if (timer >= msBetweenFrames)
            {
                timer = 0;

                if (CurrentFrame++ == numberOfFrames - 1)
                    CurrentFrame = 0;

                start = (CurrentFrame * width) / totalwidth;

            }            
        }

        public override void Draw(GameTime gt, IObject obj, RenderHelper render, ICamera cam, IList<ILight> lights)
        {
            _shader.Parameters["size"].SetValue(size);
            _shader.Parameters["amplitude"].SetValue(amplitude);
            _shader.Parameters["timeScale"].SetValue(movimentSpeedControl);
            _shader.Parameters["scaleX"].SetValue(scale.X);
            _shader.Parameters["scaleY"].SetValue(scale.Y);
            _shader.Parameters["xWorld"].SetValue(obj.PhysicObject.WorldMatrix);
            _shader.Parameters["xBillboardTexture"].SetValue(aniTex);
            _shader.Parameters["atenuation"].SetValue(atenuation);
            if (bilboardType == BilboardType.Cilindric)
            {
                _shader.Parameters["xAllowedRotDir"].SetValue(allowRotDir);
                _shader.Parameters["cilindric"].SetValue(true);
            }
            else
            {
                _shader.Parameters["cilindric"].SetValue(false);
                _shader.Parameters["xAllowedRotDir"].SetValue(cam.Up);
                _shader.Parameters["forward"].SetValue(Vector3.Normalize(cam.Target - cam.Position));
            }

            _shader.Parameters["xProjection"].SetValue(cam.Projection);                                   
            _shader.Parameters["start"].SetValue(start);            
            _shader.Parameters["gTime"].SetValue((float)gt.TotalGameTime.TotalMilliseconds);            
 	        _shader.Parameters["xView"].SetValue(cam.View);
            _shader.Parameters["xCamPos"].SetValue(cam.Position);          


            render.PushRasterizerState(RasterizerState.CullNone);

            BatchInformation batchInfo = obj.Modelo.GetBatchInformation(0)[0];
            {
                _shader.Parameters["alphaTest"].SetValue(alphaTestLimit);
                render.RenderBatch(batchInfo, _shader);
            }

            render.PopRasterizerState();
        }

        private float alphaTestLimit = 200.0f / 256.0f;

        public float AlphaTestLimit
        {
            get { return alphaTestLimit; }
            set { alphaTestLimit = value; }
        }

        public override void Initialize(Engine.GraphicInfo ginfo, Engine.GraphicFactory factory, IObject obj)        
        {
            this._shader = factory.GetEffect("AnimatedBillboard",false,true);
            this.aniTex = obj.Modelo.getTexture(TextureType.DIFFUSE);
            obj.Modelo.OnTextureChange += new OnTextureChange(Modelo_OnTextureChange);
            totalwidth = aniTex.Width;
            this.width = aniTex.Width / numberOfFrames;
            this.height = aniTex.Height;
            size = width / totalwidth;
        }

        void Modelo_OnTextureChange(TextureType type, IModelo model)
        {
            this.aniTex = model.getTexture(TextureType.DIFFUSE);
            totalwidth = aniTex.Width;
            this.width = aniTex.Width / numberOfFrames;
            this.height = aniTex.Height;
            size = width / totalwidth;
        }


    


    }
}
