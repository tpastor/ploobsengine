//  All code copyright (c) 2003 Barry Lapthorn
//  Website:  http://www.lapthorn.net
//
//  Disclaimer:  
//  All code is provided on an "AS IS" basis, without warranty. The author 
//  makes no representation, or warranty, either express or implied, with 
//  respect to the code, its quality, accuracy, or fitness for a specific 
//  purpose. Therefore, the author shall not have any liability to you or any 
//  other person or entity with respect to any liability, loss, or damage 
//  caused or alleged to have been caused directly or indirectly by the code
//  provided.  This includes, but is not limited to, interruption of service, 
//  loss of data, loss of profits, or consequential damages from the use of 
//  this code.
//
//
//  $Author: barry $
//  $Revision: 1.1 $
//
//  $Id: GenomeComparer.cs,v 1.1 2003/08/19 20:59:05 barry Exp $

using System;
using System.Collections;
using System.Collections.Generic;


namespace PloobsEngine.IA.Genetic
{
	/// <summary>
	/// Compares genomes by fitness
	/// </summary>
	public sealed class GenomeComparer : IComparer<Genome>
	{
		public GenomeComparer()
		{
		}
        public int Compare(Genome x, Genome y)
		{			
			if (( x).Fitness > (y).Fitness)
				return 1;
			else if (( x).Fitness == (y).Fitness)
				return 0;
			else
				return -1;

		}
	}
}
