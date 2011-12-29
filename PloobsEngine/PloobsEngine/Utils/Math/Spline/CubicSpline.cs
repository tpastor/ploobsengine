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

//-----------------------------------------------------------------------------
// CubicSpline.cs
//-----------------------------------------------------------------------------
 
#if !WINDOWS_PHONE
using System;
using System.Collections.Generic;
using System.Linq;
 
namespace PloobsEngine.Utils.Spline
{
    /// <summary>
    /// N-dimensional cubic spline interpolator.
    /// </summary>
    public class CubicSpline
    {
        /// <summary>
        /// Sorted list of data points (x, y[]).
        /// </summary>
        private SortedList<double, double[]> data;
 
        /// <summary>
        /// Number of dimensions for y values.
        /// </summary>
        private int dimensions;
 
        /// <summary>
        /// Current x values of data as a sorted array.
        /// </summary>
        private double[] xData;
 
        /// <summary>
        /// Current y values of data as sorted array. First index is the dimension.
        /// </summary>
        private double[][] yData;
 
        /// <summary>
        /// Current intervals between x values.
        /// </summary>
        private double[] xIntervals;
 
        /// <summary>
        /// B Coefficients. First index is the dimension.
        /// </summary>
        private double[][] bCoeff;
 
        /// <summary>
        /// C Coefficients. First index is the dimension.
        /// </summary>
        private double[][] cCoeff;
 
        /// <summary>
        /// D Coefficients. First index is the dimension.
        /// </summary>
        private double[][] dCoeff;
 
        /// <summary>
        /// Flag indicating that spline coefficients need to be updated.
        /// </summary>
        private bool needUpdate;
 
        /// <summary>
        /// Creates an instance of the cubic spline class with one dimension.
        /// </summary>
        public CubicSpline()
            : this(1)
        {
        }
 
        /// <summary>
        /// Creates an instance of the cubic spline class with 
        /// the specified number of dimensions.
        /// </summary>
        public CubicSpline(int dimensions)
        {
            if (dimensions < 1)
            {
                throw new ArgumentOutOfRangeException(
                    "dimensions", 
                    "The number of dimensions must be 1 or more");
            }
 
            this.dimensions = dimensions;
            this.data = new SortedList<double, double[]>();
            this.bCoeff = new double[dimensions][];
            this.cCoeff = new double[dimensions][];
            this.dCoeff = new double[dimensions][];
            this.needUpdate = false;
        }
 
        /// <summary>
        /// Gets the number of dimensions of the spline.
        /// </summary>
        public double Dimensions
        {
            get
            {
                return this.dimensions;
            }
        }
 
        /// <summary>
        /// Gets the number of points in the spline.
        /// </summary>
        public double Count
        {
            get
            {
                return this.data.Count;
            }
        }
 
        /// <summary>
        /// Gets a flag indicating wether sufficient points have been added
        /// so that interpolation can occur.
        /// </summary>
        public bool CanInterpolate
        {
            get
            {
                return (this.data.Count > 3);
            }
        }
 
        /// <summary>
        /// Clears the data array of the spline.
        /// </summary>
        public void Clear()
        {
            this.data.Clear();
            this.xData = null;
            this.xIntervals = null;
            this.yData = null;
            for (int i = 0; i < this.dimensions; i++)
            {
                this.bCoeff[i] = null;
                this.cCoeff[i] = null;
                this.dCoeff[i] = null;
            }
 
            this.needUpdate = false;
        }
 
        /// <summary>
        /// Adds a data point to the spline. 
        /// If the data point is added, the coefficients are recalculated the next time
        /// Interpolate() is called.
        /// </summary>
        /// <param name="x">X coordinate of the spline. Must be unique (i.e. not added before).</param>
        /// <param name="y">
        /// Y coordinates of the spline. 
        /// The number of values in the array must match the number of dimensions if the spline.
        /// </param>
        /// <returns>True if the point was added, false otherwise.</returns>
        public bool AddDataPoint(double x, double[] y)
        {
            if (y.Length != this.dimensions)
            {
                throw new ArgumentOutOfRangeException(
                    "y",
                    "The array must contain " + this.dimensions + " values.");
            }
 
            if (this.data.ContainsKey(x))
            {
                return false;
            }
            else
            {
                this.data.Add(x, y);
                this.needUpdate = true;
                return true;
            }
        }
 
        /// <summary>
        /// Interpolate spline at position X.
        /// May recalculate coefficients if they need to be updated (i.e. after AddDataPoint).
        /// </summary>
        /// <param name="x">Position X.</param>
        /// <returns>Interpolated Y.</returns>
        public double[] Interpolate(double x)
        {
            if (!this.CanInterpolate)
            {
                throw new InvalidOperationException(
                    "Insufficient number of points. Add at least 4 points before calling Interpolate.");
            }
 
            if (this.needUpdate)
            {
                this.CalculateCoefficients();
            }
 
            int index = BinaryIndexSearch(this.xData, x);
            double dx = x - this.xData[index];
            double[] y = new double[this.dimensions];
            for (int i = 0; i < this.dimensions; i++)
            {
                y[i] = ((this.dCoeff[i][index] * dx + this.cCoeff[i][index]) * dx + this.bCoeff[i][index]) * dx + this.yData[i][index];
            }
 
            return y;
        }
 
        #region private_methods
 
        /// <summary>
        /// Binary search for the lowest index where values[i] lte x.
        /// If x lt MIN(values), zero is returned and if x gt MAX(values), N-1 is returned.
        /// </summary>
        /// <param name="values">Sorted array of N values.</param>
        /// <param name="value">Value to find in array</param>
        /// <returns>The found lowest index in the range from 0 to N-1.</returns>
        private static int BinaryIndexSearch(double[] values, double value)
        {
            // special cases
            if (value <= values[0])
            {
                return 0;
            }
 
            int max = values.Length - 1;
 
            if (value >= values[max])
            {
                return max - 1;
            }
 
            // binary search
            int low = 0;
            int high = max;
            while (low < high)
            {
                int mid = low + (high - low) / 2;
                if (values[mid] < value)
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid;
                }
            }
 
            // found
            return low - 1;
        }
 
        /// <summary>
        /// Calculate intervals between values.
        /// </summary>
        /// <param name="values">Array of N values, where N>1.</param>
        /// <returns>Array of N-1 intervals. </returns>
        private static double[] CalculateIntervals(double[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException();
            }
 
            if (values.Length < 2)
            {
                throw new ArgumentOutOfRangeException();
            }
 
            int inputLength = values.Length;
            int outputLength = inputLength - 1;
            double[] intervals = new double[outputLength];
            for (int i = 0, ip = 1; i < outputLength; i++, ip++)
            {
                intervals[i] = values[ip] - values[i];
            }
 
            return intervals;
        }
 
        /// <summary>
        /// Calculates C coefficients from A, X and intervals.
        /// </summary>
        /// <param name="a">A coefficients</param>
        /// <param name="xdata">Array of X data</param>
        /// <param name="intervals">Intervals</param>
        /// <returns>C coefficients</returns>
        private static double[] CalculateC(double[] a, double[] xdata, double[] intervals)
        {
            int dataLength = xdata.Length;
            double[] c = new double[dataLength];
            double[] alpha = new double[dataLength];
            double[] l = new double[dataLength];
            double[] mu = new double[dataLength];
            double[] z = new double[dataLength];
 
            // Solve tridiagonal matrix
            for (int i = 1, im = 0, ip = 2; i < dataLength - 2; i++, im++, ip++)
            {
                alpha[i] = 3 * ((a[ip] * intervals[im]) - (a[i] * (xdata[ip] - xdata[im]))
                    + (a[im] * intervals[i])) / (intervals[im] * intervals[i]);
            }
 
            l[0] = 0.0;
            mu[0] = 0.0;
            z[0] = 0.0;
            for (int i = 1, im = 0, ip = 2; i < dataLength - 2; i++, im++, ip++)
            {
                l[i] = 2 * (xdata[ip] - xdata[im]) - intervals[im] * mu[im];
                mu[i] = intervals[i] / l[i];
                z[i] = (alpha[i] - intervals[im] * z[im]) / l[i];
            }
 
            // Calculate c's
            c[dataLength - 1] = 0;
            for (int i = dataLength - 2, ip = dataLength - 1; i >= 0; i--, ip--)
            {
                c[i] = z[i] - mu[i] * c[ip];
            }
 
            return c;
        }
 
        /// <summary>
        /// Calculates B coefficients from A, C and intervals.
        /// </summary>
        /// <param name="a">A coefficients</param>
        /// <param name="c">C coefficients</param>
        /// <param name="interval">Intervals</param>
        /// <returns>B coefficients</returns>
        private static double[] CalculateB(double[] a, double[] c, double[] interval)
        {
            int dataLength = a.Length;
            double[] b = new double[dataLength];
 
            for (int i = dataLength - 2, ip = dataLength - 1; i >= 0; i--, ip--)
            {
                b[i] = (a[ip] - a[i]) / interval[i] - interval[i] * (c[ip] + 2 * c[i]) / 3;
            }
 
            return b;
        }
 
        /// <summary>
        /// Calculates D coefficients from C and interval.
        /// </summary>
        /// <param name="c">C coefficients.</param>
        /// <param name="interval">Intervals.</param>
        /// <returns>D coefficients.</returns>
        private static double[] CalculateD(double[] c, double[] interval)
        {
            int dataLength = c.Length;
            double[] d = new double[dataLength];
 
            for (int i = dataLength - 2; i >= 0; i--)
            {
                d[i] = (c[i + 1] - c[i]) / (3 * interval[i]);
            }
 
            return d;
        }
 
        /// <summary>
        /// Calculate coefficients for the spline and the current data array.
        /// </summary>
        private void CalculateCoefficients()
        {
            // create all data arrays
            this.xData = this.data.Keys.ToArray();
            this.xIntervals = CalculateIntervals(this.xData);
            double[][] yArray = this.data.Values.ToArray();
            this.yData = new double[this.dimensions][];
            for (int i = 0; i < this.dimensions; i++)
            {
                // y data array needs to be rotated in its order from the list array
                this.yData[i] = new double[this.data.Count];
                for (int j = 0; j < this.data.Count; j++)
                {
                    this.yData[i][j] = yArray[j][i];
                }
 
                // calculate coefficients
                this.cCoeff[i] = CalculateC(this.yData[i], this.xData, this.xIntervals);
                this.bCoeff[i] = CalculateB(this.yData[i], this.cCoeff[i], this.xIntervals);
                this.dCoeff[i] = CalculateD(this.cCoeff[i], this.xIntervals);
            }
 
            this.needUpdate = false;
        }
 
        #endregion
    }
}
#endif