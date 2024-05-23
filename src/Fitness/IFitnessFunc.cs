using System;
using System.Collections.Generic;

namespace EvoNETic
{
    public interface IFitnessFunc<TGene, TCollection, TFitness>
        where TGene : IEquatable<TGene>
        where TCollection : ICollection<TGene>
        where TFitness : IComparable<TFitness>
    {
        /// <summary>
        /// Evaluates the quality of a chromosome (candidate solution to the problem).
        /// </summary>

        TFitness Evaluate(IChromosome<TGene, TCollection, TFitness> chromosome);
    }
}
