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
    /// Helper to wait for a period
    /// When off always return false in hasPassed Method
    /// </summary>
    public class TimePassed
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimePassed"/> class.
        /// Starts on by Default
        /// </summary>
        /// <param name="intervalInSeconds"></param>
        public TimePassed(float intervalInSeconds)
        {
            this.interval = intervalInSeconds;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimePassed"/> class.
        /// </summary>
        /// <param name="intervalInSeconds">The interval in seconds.</param>
        /// <param name="startOn">if set to <c>true</c> [start on].</param>
        public TimePassed(float intervalInSeconds, bool startOn)
        {
            this.interval = intervalInSeconds;
            on = startOn;
        }

        bool on = true;

        /// <summary>
        /// On or Off
        /// </summary>
        public bool State
        {
            get { return on; }
            set { on = value; }
        }
        double last = 0;
        float interval;

        /// <summary>
        /// Gets or sets the interval in seconds
        /// </summary>
        /// <value>
        /// The interval.
        /// </value>
        public float Interval
        {
            get { return interval; }
            set { interval = value; }
        }

        /// <summary>
        /// Inits or restart.
        /// </summary>
        public void InitOrRestart(GameTime gt)
        {
            on = true;
            last = gt.TotalGameTime.TotalSeconds;
        }

        /// <summary>
        /// Determines whether the interval has passed.        
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance has passed; otherwise, <c>false</c>.
        /// </returns>
        public bool hasPassed(GameTime gt)
        {
            if (on)
            {
                if ((gt.TotalGameTime.TotalSeconds - last) > interval)
                {
                    last = gt.TotalGameTime.TotalSeconds;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            else
            {
                return false;
            }

        }
    }
}
