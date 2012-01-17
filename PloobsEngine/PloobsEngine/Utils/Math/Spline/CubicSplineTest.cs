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
#if WINDOWS
using System;
 
namespace PloobsEngine.Utils.Spline
{
    /// <summary>
    /// Unit tests for CubicSpline class.
    /// </summary>
    
    internal class UnitTestSpline
    {
        public UnitTestSpline()
        {
        }
 
        /// <summary>
        /// Test exception when trying to interpolate with no points added.
        /// </summary>
        public void SplineExceptionOnNoPointsAdded()
        {
            CubicSpline spline = new CubicSpline();            
            double[] y = spline.Interpolate(1.5);
        }
 
        /// <summary>
        /// Test exception when trying to interpolate with only one point added.
        /// </summary>
        public void SplineExceptionOnOnlyOnePointAdded()
        {
            CubicSpline spline = new CubicSpline();
            spline.AddDataPoint(1.0, new double[] { 1.0 });
            //Assert.AreEqual(1, spline.Count);
            //Assert.IsFalse(spline.CanInterpolate);
            double[] y = spline.Interpolate(1.5);
        }
 
        /// <summary>
        /// Test exception when trying to create spline with wrong dimension.
        /// </summary>
       
        public void SplineExceptionInvalidDimension()
        {
            CubicSpline spline = new CubicSpline(0);
        }
 
        /// <summary>
        /// Test exception when trying to add points with the wrong dimension.
        /// </summary>      
        public void SplineExceptionAddingWithDimensionMismatch()
        {
            CubicSpline spline = new CubicSpline(2);
            //Assert.AreEqual(2, spline.Dimensions);
            spline.AddDataPoint(1.0, new double[] { 1.0 });
        }
 
        /// <summary>
        /// Test exception when trying to interpolate with only two points added.
        /// </summary>
        public void SplineExceptionOnOnlyTwoPointsAdded()
        {
            double[,] data = {{1.0, 1.0},
                              {2.0, 2.0}};
 
            CubicSpline spline = new CubicSpline();
            for (int i = 0; i < 2; i++)
            {
                spline.AddDataPoint(data[i, 0], new double[] { data[i, 1] });
            }
 
            //Assert.AreEqual(2, spline.Count);
            //Assert.IsFalse(spline.CanInterpolate);
            double[] y = spline.Interpolate(1.5);
        }
 
        /// <summary>
        /// Test exception when trying to interpolate with only three points added.
        /// </summary>       
        public void SplineExceptionOnOnlyThreePointsAdded()
        {
            double[,] data = {{1.0, 1.0},
                              {2.0, 2.0},
                              {3.0, 1.0}};
 
            CubicSpline spline = new CubicSpline();
            for (int i = 0; i < 3; i++)
            {
                spline.AddDataPoint(data[i, 0], new double[] { data[i, 1] });
            }
 
            //Assert.AreEqual(3, spline.Count);
            //Assert.IsFalse(spline.CanInterpolate);
            double[] y = spline.Interpolate(1.5);
        }
 
        /// <summary>
        /// Interpolate between 4 points forming a straight line.
        /// </summary>       
        public void SplineInterpolateFourPointLine()
        {
            double[,] data = {{0.0, 0.0},
                              {1.0, 1.0},
                              {2.0, 2.0},
                              {3.0, 3.0}};
 
            CubicSpline spline = new CubicSpline();
            for (int i = 0; i < 4; i++)
            {
                spline.AddDataPoint(data[i, 0], new double[] { data[i, 1] });
            }
 
            //Assert.AreEqual(4, spline.Count);
            //Assert.IsTrue(spline.CanInterpolate);
 
            for (int i = 0; i <= 30; i++)
            {
                double x = 0.1 * (double)i;
                double[] y = spline.Interpolate(x);
                //Assert.AreEqual(x, y[0]);
            }
        }
 
        /// <summary>
        /// Validate that adding duplicate x coordinates will reject the data.
        /// </summary>      
        public void SplineRejectDuplicatePointsWhenAdding()
        {
            double[,] data = {{0.0, 0.0},
                           {1.0, 1.0},
                           {2.0, 2.0},
                           {3.0, 3.0},
                          };
 
            CubicSpline spline = new CubicSpline();
            for (int i = 0; i < 4; i++)
            {
                bool addResult = spline.AddDataPoint(data[i, 0], new double[] { data[i, 1] });
                //Assert.IsTrue(addResult);
            }
 
            // try to add again
            for (int i = 0; i < 4; i++)
            {
                bool addResult = spline.AddDataPoint(data[i, 0], new double[] { data[i, 1] });
                //Assert.IsFalse(addResult);
            }
 
            //Assert.AreEqual(4, spline.Count);
            //Assert.IsTrue(spline.CanInterpolate);
        }
 
        /// <summary>
        /// Validate that the data array can be cleared.
        /// </summary>        
        public void SplineCanClearDataArrayThenAddAndInterpolate()
        {
            double[,] data = {{0.0, 0.0},
                           {1.0, 1.0},
                           {2.0, 2.0},
                           {3.0, 3.0},
                          };
 
            CubicSpline spline = new CubicSpline();
 
            // several tries
            for (int t = 0; t < 3; t++)
            {
                for (int i = 0; i < 4; i++)
                {
                    bool addResult = spline.AddDataPoint(data[i, 0], new double[] { data[i, 1] });
                    //Assert.IsTrue(addResult);
                }
 
                //Assert.AreEqual(4, spline.Count);
                //Assert.IsTrue(spline.CanInterpolate);
 
                // test interpolator
                Random random = new Random();
                for (int j = 0; j < 10; j++)
                {
                    double x = random.NextDouble();
                    double[] y = spline.Interpolate(x);
                    //Assert.AreEqual(x, y[0]);
                }
 
                // test clear
                spline.Clear();
                //Assert.AreEqual(0, spline.Count);
                //Assert.IsFalse(spline.CanInterpolate);
            }
        }
 
        /// <summary>
        /// Interpolate between 4 points forming Z shape.
        /// </summary>        
        public void SplineInterpolateFourPointShape()
        {
            double[,] data = {{1.0, 1.0},
                              {2.0, 2.0},
                              {3.0, 1.0},
                              {4.0, 3.0}};
 
            CubicSpline spline = new CubicSpline();
            for (int i = 0; i < 4; i++)
            {
                spline.AddDataPoint(data[i, 0], new double[] { data[i, 1] });
            }
 
            //Assert.AreEqual(4, spline.Count);
            //Assert.IsTrue(spline.CanInterpolate);
 
            double[] testinput = { 1.5, 2.5, 3.5, 3.9 };
            double[] testoutput = { 1.6, 1.6, 2.0, 2.85 };
 
            for (int i=0; i<testinput.Length; i++)
            {
                double[] y = spline.Interpolate(testinput[i]);
                //Assert.AreEqual(testoutput[i], y[0], 0.1);
            }
        }
 
        /// <summary>
        /// Interpolate between 4 points forming Z shape in two dimensions.
        /// </summary>        
        public void SplineInterpolateFourPointShape2D()
        {
            double[,] data = {{1.0, 1.0, 2.0},
                              {2.0, 2.0, 4.0},
                              {3.0, 1.0, 2.0},
                              {4.0, 3.0, 6.0}};
 
            CubicSpline spline = new CubicSpline(2);
            //Assert.AreEqual(2, spline.Dimensions);
            for (int i = 0; i < 4; i++)
            {
                spline.AddDataPoint(data[i, 0], new double[] { data[i, 1], data[i, 2] });
            }
 
            //Assert.AreEqual(4, spline.Count);
            //Assert.IsTrue(spline.CanInterpolate);
 
            double[] testinput = { 1.5, 2.5, 3.5, 3.9 };
            double[] testoutput = { 1.6, 1.6, 2.0, 2.85 };
 
            for (int i = 0; i < testinput.Length; i++)
            {
                double[] y = spline.Interpolate(testinput[i]);
                //Assert.AreEqual(testoutput[i], y[0], 0.1);
                //Assert.AreEqual(2 * testoutput[i], y[1], 0.2);
            }
        }
 
        /// <summary>
        /// Interpolate left and right of the 4 points forming a Z shape.
        /// </summary>        
        public void SplineInterpolatePastEdgeOfFourPointShape()
        {
            double[,] data = {{1.0, 1.0},
                              {2.0, 2.0},
                              {3.0, 1.0},
                              {4.0, 2.0}};
 
            CubicSpline spline = new CubicSpline();
            for (int i = 0; i < 4; i++)
            {
                spline.AddDataPoint(data[i, 0], new double[] { data[i, 1] });
            }
 
            //Assert.AreEqual(4, spline.Count);
            //Assert.IsTrue(spline.CanInterpolate);
 
            double[] testinput = { 0.8, 0.9, 4.1, 4.2 };
            double[] testoutput = { 0.8, 0.9, 2.1, 2.2 };
 
            for (int i = 0; i < testinput.Length; i++)
            {
                double[] y = spline.Interpolate(testinput[i]);
                //Assert.AreEqual(testoutput[i], y[0], 0.1);
            }
        }
 
        /// <summary>
        /// Interpolate points along a sine wave approximated with 12 points.
        /// </summary>        
        public void SplineInterpolateSineWave()
        {
 
            int N = 12;
            double[] x = new double[N];
            double[] y = new double[N];
            double xx = Math.PI;
            double step = 4 * Math.PI / (N - 1);
 
            for (int i = 0; i < N; ++i, xx += step)
            {
                double yy = Math.Sin(2 * xx) / xx;
                x[i] = xx;
                y[i] = yy;
            }
 
            CubicSpline spline = new CubicSpline();
            for (int i = 0; i < N; i++)
            {
                spline.AddDataPoint(x[i], new double[] { y[i] });
            }
 
            //Assert.AreEqual(N, spline.Count);
            //Assert.IsTrue(spline.CanInterpolate);
 
            N = 30;
            xx = Math.PI;
            step = 3 * Math.PI / (N - 1);
            for (int i = 0; i < N; ++i, xx += step)
            {
                double yExpected = Math.Sin(2 * xx) / xx;
                double[] yCalculated = spline.Interpolate(xx);
                //Assert.AreEqual(yExpected, yCalculated[0], 0.1);
            }
        }
    }
}

#endif