using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine;
using PloobsEngine.SceneControl;
using PloobsEngine.Utils;
using Microsoft.Xna.Framework.Graphics;
using PloobsEngine.Light;
using Microsoft.Xna.Framework;

namespace IntroductionDemo4._0
{
    /// <summary>
    /// Interpolador de Luzes
    /// Herda de IScreenUpdateable, isto significa q esta classe tera seu metodo Update chamado a todo frame (enquanto a screen associada estiver ativa e o objeto ativado (chamar metodo Start() herdado))
    /// </summary>
    public class UnitLightInterpolator : IScreenUpdateable
    {
        /// <summary>
        /// Classe interna usada para armazenar o estado de cada luz
        /// </summary>
        class lightInterpolation
        {
            public DeferredLight dl;
            public Vec3Interpolator vi;
            public Vector3 v1;
            public Vector3 v2;
            public double duration;

            public void SwapColors()
            {
                Vector3 v = v1;
                v1 = v2;
                v2 = v;
            }
                 
        }

        /// <summary>
        /// Lista de luzes
        /// </summary>
        List<lightInterpolation> lights = new List<lightInterpolation>();
        
        
        /// <summary>
        /// Se a interpolacao deve ser ciclica (0 -> 1 -> 0 -> 1 .... )
        /// </summary>
        public bool OnLoopReverse
        {
            get { return onLoopReverse; }
            set { onLoopReverse = value; }
        }
        bool onLoopReverse = false;

        /// <summary>
        /// Adiciona uma luz ao interpolador
        /// As cores das luzes adicionadas serao gerenciadas por este component
        /// </summary>
        /// <param name="dl">luz</param>
        /// <param name="c1">Cor original</param>
        /// <param name="c2">Cor destino</param>
        /// <param name="duration">Duracao da interpolacao em SEGUNDOS</param>
        public void AddLight(DeferredLight dl, Color c1, Color c2, double duration)
        {
            lightInterpolation l = new lightInterpolation();
            l.dl = dl;
            l.duration = duration;
            l.v1 = c1.ToVector3();
            l.v2 = c2.ToVector3();
            lights.Add(l);
            l.vi = new Vec3Interpolator();
            l.vi.Start(l.v1, l.v2, duration, true);
        }

        protected override void CleanUp()
        {
            this.Stop();
            base.CleanUp();
        }


        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="screen">Screen Associada</param>
        /// <param name="OnLoopReverse">Se a interpolacao deve ser ciclica (0 -> 1 -> 0 -> 1 .... )</param>
        public UnitLightInterpolator(IScreen screen,bool OnLoopReverse)
            : base(screen)
        {
            this.onLoopReverse = OnLoopReverse;
        }

        protected override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {            
            foreach (var item in lights)
            {
                item.vi.Update(gameTime);
                item.dl.Color= new Color(item.vi.CurrentValue);

                if (item.vi.IsActive == false && onLoopReverse == true)
                {
                    item.SwapColors();
                    item.vi.Start(item.v1, item.v2, item.duration, true);
                }
            }
            
        }
    }
}
