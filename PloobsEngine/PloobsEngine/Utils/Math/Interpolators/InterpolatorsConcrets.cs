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
    public class FloatInterpolator : Interpolator<float>
    {
        protected override float Interpolate()
        {
            return smoothStep
                ? MathHelper.SmoothStep(value1, value2, (float)(currentDuration / totalDuration))
                : MathHelper.Lerp(value1, value2, (float)(currentDuration / totalDuration));
        }
    }

    public class FloatInterpolatorConstantStep : Interpolator<float>
    {
        private float step;
        private float acum = 0;
        public FloatInterpolatorConstantStep(float step)
        {
            this.step = step;
        }

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

        public override void Reset()
        {
            acum = 0;
            IsActive = true;
        }

        protected override float Interpolate()
        {
            acum += step;
            return smoothStep
                ? MathHelper.SmoothStep(value1, value2, acum)
                : MathHelper.Lerp(value1, value2, acum);
        }
    }

    public class Vec3InterpolatorConstantStep : Interpolator<Vector3>
    {
        private float step;
        private float acum = 0;
        public Vec3InterpolatorConstantStep(float step)
        {
            this.step = step;
        }
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

        public override void Reset()
        {
            acum = 0;
            IsActive = true;
        }


        protected override Vector3 Interpolate()
        {
            acum += step;
            return smoothStep
                ? Vector3.SmoothStep(value1, value2, acum)
                : Vector3.Lerp(value1, value2, acum);
        }
    }

    public class Vec3Interpolator : Interpolator<Vector3>
    {
        protected override Vector3 Interpolate()
        {
            return smoothStep
                ? Vector3.SmoothStep(value1, value2, (float)(currentDuration / totalDuration))
                : Vector3.Lerp(value1, value2, (float)(currentDuration / totalDuration));
        }
    }


    public class Vec4Interpolator : Interpolator<Vector4>
    {
        protected override Vector4 Interpolate()
        {
            return smoothStep
                ? Vector4.SmoothStep(value1, value2, (float)(currentDuration / totalDuration))
                : Vector4.Lerp(value1, value2, (float)(currentDuration / totalDuration));
        }
    }

}
