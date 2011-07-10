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
    

    /*
     *  FloatInterpolator interpolator = new FloatInterpolator(); 
        interpolator.Start(5f, 15f, 2f);  // interpolate from 5f to 15f over 2 seconds         
        float currentValue;         
        if (interpolator.IsActive)
        {
           currentValue = interpolator.Update(gameTime);
        }
        else
        {
           // interpolation has finished
        } 
     */


    /// <summary>
    /// General Interpolator Base
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Interpolator<T>
    {
        #region Fields

        public bool IsActive;
        protected double currentDuration;
        protected double totalDuration;
        protected bool smoothStep;
        protected T value1;
        protected T value2;
        protected T currentValue;

        public T Value2
        {
            get { return value2; }
        }

        public T CurrentValue
        {
            get { return currentValue; }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Start the Interpolator
        /// </summary>
        /// <param name="value1">Value to Start interpolation from</param>
        /// <param name="value2">Value to interpolate to</param>
        /// <param name="totalDuration">Time to interpolate from value1 to value2 in seconds</param>
        public void Start(T value1, T value2, double totalDuration)
        {
            Start(value1, value2, totalDuration, false);
        }

        /// <summary>
        /// Start the interpolator
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="totalDuration"></param>
        /// <param name="smoothStep">Use smoothstep rather than lerp</param>
        public void Start(T value1, T value2, double totalDuration, bool smoothStep)
        {
            this.value1 = value1;
            this.value2 = value2;
            this.totalDuration = totalDuration;
            this.smoothStep = smoothStep;

            IsActive = true;
            currentDuration = 0.0;
            currentValue = value1;
        }

        #endregion

        #region Updating

        /// <summary>
        /// Update the interpolator
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public virtual T Update(GameTime gameTime)
        {
            currentDuration += gameTime.ElapsedGameTime.TotalSeconds;

            if (currentDuration > totalDuration)
            {
                currentDuration = totalDuration;
                IsActive = false;
            }

            return currentValue = Interpolate();
        }

        /// <summary>
        /// Peek the next interpolation value without affecting the next update
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public T PeekValue(GameTime gameTime)
        {
            double saveCurrent = currentDuration;
            bool saveActive = IsActive;

            T nextValue = Update(gameTime);

            currentDuration = saveCurrent;
            IsActive = saveActive;

            return nextValue;
        }

        /// <summary>
        /// Reset the interpolator
        /// </summary>
        public virtual void Reset()
        {
            IsActive = true;
            currentDuration = 0.0;            
        }

        /// <summary>
        /// Override to implement interpolation for a concrete data type
        /// </summary>
        /// <returns></returns>
        protected abstract T Interpolate();

        #endregion
    }
}
