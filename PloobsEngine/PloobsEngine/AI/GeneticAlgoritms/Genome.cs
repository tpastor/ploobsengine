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
using System.Collections;

namespace PloobsEngine.IA.Genetic
{
	/// <summary>
	/// Summary description for Genome.
	/// </summary>
	public class Genome
	{
		public Genome()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public Genome(int length)
		{
			m_length = length;
			m_genes = new double[ length ];
			CreateGenes();
		}
		public Genome(int length, bool createGenes)
		{
			m_length = length;
			m_genes = new double[ length ];
			if (createGenes)
				CreateGenes();
		}

		public Genome(ref double[] genes)
		{
			m_length = genes.GetLength(0);
			m_genes = new double[ m_length ];
			for (int i = 0 ; i < m_length ; i++)
				m_genes[i] = genes[i];
		}
		 

		private void CreateGenes()
		{
			// DateTime d = DateTime.UtcNow;
			for (int i = 0 ; i < m_length ; i++)
				m_genes[i] = m_random.NextDouble();
		}

		public void Crossover(ref Genome genome2, out Genome child1, out Genome child2)
		{
			int pos = (int)(m_random.NextDouble() * (double)m_length);
			child1 = new Genome(m_length, false);
			child2 = new Genome(m_length, false);
			for(int i = 0 ; i < m_length ; i++)
			{
				if (i < pos)
				{
					child1.m_genes[i] = m_genes[i];
					child2.m_genes[i] = genome2.m_genes[i];
				}
				else
				{
					child1.m_genes[i] = genome2.m_genes[i];
					child2.m_genes[i] = m_genes[i];
				}
			}
		}


		public void Mutate()
		{
			for (int pos = 0 ; pos < m_length; pos++)
			{
				if (m_random.NextDouble() < m_mutationRate)
					m_genes[pos] = (m_genes[pos] + m_random.NextDouble())/2.0;
			}
		}

		public double[] Genes()
		{
			return m_genes;
		}

#if !WINRT
		public void Output()
		{
			for (int i = 0 ; i < m_length ; i++)
			{
				System.Console.WriteLine("{0:F4}", m_genes[i]);
			}
			System.Console.Write("\n");
		}
#endif

		public void GetValues(ref double[] values)
		{
			for (int i = 0 ; i < m_length ; i++)
				values[i] = m_genes[i];
		}


		public double[] m_genes;
		private int m_length;
		private double m_fitness;
		static Random m_random = new Random();

		private static double m_mutationRate;

		public double Fitness
		{
			get
			{
				return m_fitness;
			}
			set
			{
				m_fitness = value;
			}
		}




		public static double MutationRate
		{
			get
			{
				return m_mutationRate;
			}
			set
			{
				m_mutationRate = value;
			}
		}

		public int Length
		{
			get
			{
				return m_length;
			}
		}
	}
}
