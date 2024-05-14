using System;
using System.Collections.Generic;

namespace EvoNETic
{
    public interface IFitnessFunc<TGene, TCollection, TFitness>
        where TGene : IEquatable<TGene>
        where TCollection : ICollection<TGene>
        where TFitness : IComparable<TFitness>
    {
        TFitness Evaluate(IChromosome<TGene, TCollection, TFitness> chromosome);
    }
}
