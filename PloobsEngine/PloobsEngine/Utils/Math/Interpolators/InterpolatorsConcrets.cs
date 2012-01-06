#region License
/*
    PloobsEngine Game Engine Version 0.3 Beta
    Copyright (C) 2011  Ploobs

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PloobsEngine.Utils
{
    /// <summary>
    /// Float Interpolator
    /// </summary>
    public class FloatInterpolator : Interpolator<float>
    {
        /// <summary>
        /// Interpolates this instance.
        /// </summary>
        /// <returns></returns>
        protected override float Interpolate()
        {
            return smoothStep
                ? MathHelper.SmoothStep(value1, value2, (float)(currentDuration / totalDuration))
                : MathHelper.Lerp(value1, value2, (float)(currentDuration / totalDuration));
        }
    }

    /// <summary>
    /// Float Interpolator at constant step
    /// </summary>
    public class FloatInterpolatorConstantStep : Interpolator<float>
    {
        private float step;
        private float acum = 0;
        /// <summary>
        /// Initializes a new instance of the <see cref="FloatInterpolatorConstantStep"/> class.
        /// </summary>
        /// <param name="step">The step.</param>
        public FloatInterpolatorConstantStep(float step)
        {
            this.step = step;
        }

        /// <summary>
        /// Updates
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <returns></returns>
        public override float Update(GameTime gameTime)
        {
            if (IsActive == true)
            {
                if (acum >= 1)
                    IsActive = false;
               currentValue = Interpolate();
            }

            return CurrentValue;            
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public override void Reset()
        {
            acum = 0;
            IsActive = true;
        }

        /// <summary>
        /// Interpolates this instance.
        /// </summary>
        /// <returns></returns>
        protected override float Interpolate()
        {
            acum += step;
            return smoothStep
                ? MathHelper.SmoothStep(value1, value2, acum)
                : MathHelper.Lerp(value1, value2, acum);
        }
    }

    /// <summary>
    /// Interpolator for Vector3 ate Constant step
    /// </summary>
    public class Vec3InterpolatorConstantStep : Interpolator<Vector3>
    {
        private float step;
        private float acum = 0;
        /// <summary>
        /// Initializes a new instance of the <see cref="Vec3InterpolatorConstantStep"/> class.
        /// </summary>
        /// <param name="step">The step.</param>
        public Vec3InterpolatorConstantStep(float step)
        {
            this.step = step;
        }
        /// <summary>
        /// Updates this instance
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <returns></returns>
        public override Vector3 Update(GameTime gameTime)
        {
            if (IsActive == true)
            {
                if (acum >= 1)
                    IsActive = false;
                currentValue = Interpolate();
            }

            return currentValue;
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public override void Reset()
        {
            acum = 0;
            IsActive = true;
        }


        /// <summary>
        /// Interpolates this instance.
        /// </summary>
        /// <returns></returns>
        protected override Vector3 Interpolate()
        {
            acum += step;
            return smoothStep
                ? Vector3.SmoothStep(value1, value2, acum)
                : Vector3.Lerp(value1, value2, acum);
        }
    }

    /// <summary>
    /// Vector3 Interpolator
    /// </summary>
    public class Vec3Interpolator : Interpolator<Vector3>
    {
        /// <summary>
        /// Interpolates this instance.
        /// </summary>
        /// <returns></returns>
        protected override Vector3 Interpolate()
        {
            return smoothStep
                ? Vector3.SmoothStep(value1, value2, (float)(currentDuration / totalDuration))
                : Vector3.Lerp(value1, value2, (float)(currentDuration / totalDuration));
        }
    }


    /// <summary>
    /// Vector4 Interpolator
    /// </summary>
    public class Vec4Interpolator : Interpolator<Vector4>
    {
        /// <summary>
        /// Interpolates this instance.
        /// </summary>
        /// <returns></returns>
        protected override Vector4 Interpolate()
        {
            return smoothStep
                ? Vector4.SmoothStep(value1, value2, (float)(currentDuration / totalDuration))
                : Vector4.Lerp(value1, value2, (float)(currentDuration / totalDuration));
        }
    }

}
