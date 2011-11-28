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
using System.Diagnostics;
using PloobsEngine.Utils;

namespace Bnoerj.AI.Steering
{
    public class Clock
    {
        Stopwatch stopwatch;

        // constructor
        public Clock()
        {
            // default is "real time, variable frame rate" and not paused
            PausedState = false;
            
            // real "wall clock" time since launch
            totalRealTime = 0;

            // time simulation has run
            totalSimulationTime = 0;

            // time spent paused
            totalPausedTime = 0;

            // sum of (non-realtime driven) advances to simulation time
            totalAdvanceTime = 0;

            // interval since last simulation time 
            elapsedSimulationTime = 0;

            // interval since last clock update time 
            elapsedRealTime = 0;

            // interval since last clock update,
            // exclusive of time spent waiting for frame boundary when targetFPS>0
            elapsedNonWaitRealTime = 0;

            // "manually" advance clock by this amount on next update
            newAdvanceTime = 0;

            // "Calendar time" when this clock was first updated
            stopwatch = new Stopwatch();

        }

        // update this clock, called exactly once per simulation step ("frame")
        public void Update()
        {            
            // save previous real time to measure elapsed time
            float previousRealTime = totalRealTime;

            // real "wall clock" time since this application was launched
            totalRealTime = RealTimeSinceFirstClockUpdate();

            // time since last clock update
            elapsedRealTime = totalRealTime - previousRealTime;

            // accumulate paused time
            if (paused) totalPausedTime += elapsedRealTime;

            // save previous simulation time to measure elapsed time
            float previousSimulationTime = totalSimulationTime;

            // new simulation time is total run time minus time spent paused
            totalSimulationTime = (totalRealTime + totalAdvanceTime - totalPausedTime);
            


            // update total "manual advance" time
            totalAdvanceTime += newAdvanceTime;

            // how much time has elapsed since the last simulation step?
            if (paused)
            {
                elapsedSimulationTime = newAdvanceTime;
            }
            else
            {
                elapsedSimulationTime = (totalSimulationTime - previousSimulationTime);
            }

            // reset advance amount
            newAdvanceTime = 0;
        }

        // returns the number of seconds of real time (represented as a float)
        // since the clock was first updated.
        public float RealTimeSinceFirstClockUpdate()
        {
            if (stopwatch.IsRunning == false)
            {
                stopwatch.Start();
            }
            return (float)stopwatch.Elapsed.TotalSeconds;
        }

        // is simulation running or paused?
        bool paused;
        
        public bool TogglePausedState()
        {
            return (paused = !paused);
        }

        public bool PausedState
        {
            get { return paused; }
            set { paused = value; }
        }

        
        // real "wall clock" time since launch
        float totalRealTime;

        // total time simulation has run
        float totalSimulationTime;

        // total time spent paused
        float totalPausedTime;

        // sum of (non-realtime driven) advances to simulation time
        float totalAdvanceTime;

        // interval since last simulation time
        // (xxx does this need to be stored in the instance? xxx)
        float elapsedSimulationTime;

        // interval since last clock update time 
        // (xxx does this need to be stored in the instance? xxx)
        float elapsedRealTime;

        // interval since last clock update,
        // exclusive of time spent waiting for frame boundary when targetFPS>0
        float elapsedNonWaitRealTime;

        public float TotalRealTime
        {
            get { return totalRealTime; }
        }
        public float TotalSimulationTime
        {
            get { return totalSimulationTime; }
        }
        public float TotalPausedTime
        {
            get { return totalPausedTime; }
        }
        public float TotalAdvanceTime
        {
            get { return totalAdvanceTime; }
        }
        public float ElapsedSimulationTime
        {
            get { return elapsedSimulationTime; }
        }
        public float ElapsedRealTime
        {
            get { return elapsedRealTime; }
        }
        public float ElapsedNonWaitRealTime
        {
            get { return elapsedNonWaitRealTime; }
        }

        // "manually" advance clock by this amount on next update
        float newAdvanceTime;
    }
}
