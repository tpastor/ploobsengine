using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PloobsEngine;
using PloobsEngine.Light;
using Microsoft.Xna.Framework;
using PloobsEngine.SceneControl;

namespace IntroductionDemo4._0
{
    /// <summary>
    /// Extend IUpdateable, so the engine will call the method updated very update frame
    /// The method start and end controls when the update is called
    /// </summary>
    public class SpotLightCircularUpdater : IScreenUpdateable
    {
        SpotLightPE sl;
        Vector3 center;
        float speed;
        float radius;
        float speedAcumullated ;
        bool clockwise = true;

        public SpotLightCircularUpdater(IScreen screen, SpotLightPE pl, float speed, float radius, float initialAngle, bool clockwise)
            : base(screen)
        {
            speedAcumullated = initialAngle;
            this.sl = pl;
            center = pl.Direction;
            this.speed = speed * 0.05f;
            this.radius = radius;
            this.clockwise = clockwise;
            this.Start(); ///Start to call the update
        }


        protected override void CleanUp()
        {
            this.Stop(); ///When destroyed, stop calling update method
            base.CleanUp();
        }
        
        /// <summary>
        /// Just move the lights in a circle
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if(clockwise)
            speedAcumullated += speed;
            else
            speedAcumullated -= speed;

            sl.Direction = center + (new Vector3(radius * (float)Math.Cos(speedAcumullated), center.Y, radius * (float)Math.Sin(speedAcumullated)));             
        }
    }
}
