using System;
using System.Collections.Generic;

namespace EvoNETic
{
    public interface IPopulation<TGene, TCollection, TFitness> : IEnumerable<IChromosome<TGene, TCollection, TFitness>>
        where TGene : IEquatable<TGene>
        where TCollection : ICollection<TGene>
        where TFitness : IComparable<TFitness>
    {
        /// <summary>
        /// A population is a collection of chromosomes.
        /// </summary>

        int Size { get; }

        ICollection<IChromosome<TGene, TCollection, TFitness>> Chromosomes { get; }
    }
}
