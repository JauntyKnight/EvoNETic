using System;
using System.Collections.Generic;

namespace EvoNETic
{
    public interface ICrossover<TGene, TCollection, TFitness>
        where TGene : IEquatable<TGene>
        where TCollection : ICollection<TGene>
        where TFitness : IComparable<TFitness>
    {
        /// <summary>
        /// A crossover is an operator used to combine two chromosomes to produce a new one.
        /// </summary>

        IChromosome<TGene, TCollection, TFitness> Cross(IChromosome<TGene, TCollection, TFitness> chromosome1, IChromosome<TGene, TCollection, TFitness> chromosome2);
    }
}
