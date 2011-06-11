
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PloobsEngine.Utils
{
    /// <summary>
    /// Moving Average class
    /// </summary>
    public class MovingAverage
    {
        /// <summary>
        /// Array of elements
        /// </summary>
        private double[] m_Data = null;

        /// <summary>
        /// Total amount
        /// </summary>
        private double m_Total = 0;

        /// <summary>
        /// Fixed size of the array
        /// </summary>
        private int m_Size = 0;

        /// <summary>
        /// How many elements we've got so far
        /// </summary>
        private int m_Count = 0;

        /// <summary>
        /// Get data
        /// </summary>
        public double[] Data
        {
            get
            {
                return m_Data;
            }
        }

        /// <summary>
        /// Get count
        /// </summary>
        public int Count
        {
            get
            {
                return m_Count;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="size">Set the fixed size of the array here</param>
        public MovingAverage(int size)
        {
            m_Data = new double[size];

            m_Count = 0;
            this.m_Size = size;
        }



        /// <summary>
        /// Add an element
        /// </summary>
        public void Add(double element)
        {
            m_Total += element;

            if (m_Count < m_Size)
            {
                m_Data[m_Count] = element;

                m_Count++;
            }
            else
            {

                m_Total -= m_Data[0];

                // Go over all elements but the last one
                for (int i = 0; i < m_Size - 1; i++)
                    m_Data[i] = m_Data[i + 1];

                // Set the last item
                m_Data[m_Size - 1] = element;
            }
        }


        /// <summary>
        /// Get value
        /// </summary>
        public double Value
        {
            get
            {
                if (m_Count < m_Size)
                    return (m_Total / m_Count);
                else
                    return (m_Total / m_Size);
            }
        }
    }

}
