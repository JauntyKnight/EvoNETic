using System;
using System.Collections.Generic;

namespace EvoNETic
{
    public interface IMutation<TGene, TCollection, TFitness>
        where TGene : IEquatable<TGene>
        where TCollection : ICollection<TGene>
        where TFitness : IComparable<TFitness>
    {
        /// <summary>
        /// A mutation is an operator used to perturb a chromosome to produce a new one.
        /// </summary>

        IChromosome<TGene, TCollection, TFitness> Mutate(IChromosome<TGene, TCollection, TFitness> chromosome);
    }
}
