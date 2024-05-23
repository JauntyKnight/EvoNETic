using System;
using System.Collections;
using System.Collections.Generic;

namespace EvoNETic
{
    public interface ISelection<TGene, TCollection, TFitness>
        where TGene : IEquatable<TGene>
        where TCollection : ICollection<TGene>, new()
        where TFitness : IComparable<TFitness>
    {
        /// <summary>
        /// Selects a list of count chromosomes from the population, according to their fitness.
        /// All Selection classes assume that the population's fitness is already calculated.
        /// </summary>

        ICollection<IChromosome<TGene, TCollection, TFitness>> Select(IPopulation<TGene, TCollection, TFitness> population, int count);
    }
}
