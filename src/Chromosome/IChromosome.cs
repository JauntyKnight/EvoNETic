using System;
using System.Collections.Generic;

namespace EvoNETic
{
    public interface IChromosome<TGene, TCollection, TFitness> : IEnumerable<TGene>
        where TGene : IEquatable<TGene>
        where TCollection : ICollection<TGene>
        where TFitness : IComparable<TFitness>
    {
        /// <summary>
        /// A chromosome represents a candidate solution to the problem.
        /// 
        /// It is a collection of genes, which are the building blocks of the solution.
        /// For example, in the TSP problem, a chromosome is a permutation of the cities.
        /// 
        /// Length: The number of genes in the chromosome.
        /// Fitness: The quality of the solution.
        /// Clone: Returns a deep copy of the chromosome.
        /// </summary>

        int Length { get; }
        TFitness Fitness { get; set; }
        IChromosome<TGene, TCollection, TFitness> Clone();
    }
}
