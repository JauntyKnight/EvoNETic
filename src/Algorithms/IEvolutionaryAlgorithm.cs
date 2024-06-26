using System;
using System.Collections.Generic;

namespace EvoNETic
{
    /// <summary>
    /// A generic interface for evolutionary algorithms.
    /// </summary>

    public interface IEvolutionaryAlgorithm<TGene, TCollection, TFitness>
        where TGene : IEquatable<TGene>
        where TCollection : ICollection<TGene>, new()
        where TFitness : IComparable<TFitness>
    {
        int GenerationsNumber { get; }

        IChromosome<TGene, TCollection, TFitness> BestChromosome { get; }

        void Evolve(IPopulation<TGene, TCollection, TFitness> population);

        TimeSpan TimeElapsed { get; }
    }
}
