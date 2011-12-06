using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PloobsEngine.Light;
using PloobsEngine;
using PloobsEngine.SceneControl;

namespace IntroductionDemo4._0
{
    /// <summary>
    /// Extend IUpdateable, so the engine will call the method updated very update frame
    /// The method start and end controls when the update is called
    /// </summary>
    public class PointLightCircularUpdater : PloobsEngine.IScreenUpdateable
    {
        class lightProp
        {
            public PointLightPE pl;
            public Vector3 center;
            public float speed;
            public float radius;
            public float speedAcumullated;
        }

        List<lightProp> lights = new List<lightProp>();

        public PointLightCircularUpdater(IScreen screen) : base(screen) { }

        public void AddLight(PointLightPE pl,float speed, float radius , float initialAngle) 
        {
            lightProp lp = new lightProp();            
            lp.speedAcumullated = initialAngle;
            lp.pl = pl;
            lp.center = pl.LightPosition;
            lp.speed = speed;
            lp.radius = radius;
            lights.Add(lp);
        }

        protected override void CleanUp()
        {
            this.Stop(); ///When destroyed, stop calling update method
            base.CleanUp();
        }
        
        /// <summary>
        /// Just move the lights in a random circle
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            foreach (var item in lights)
            {
                item.speedAcumullated += item.speed;
                item.pl.LightPosition = item.center + (new Vector3(item.radius * (float)Math.Cos(item.speedAcumullated), item.center.Y, item.radius * (float)Math.Sin(item.speedAcumullated)));
            }                        
        }
    }
}
