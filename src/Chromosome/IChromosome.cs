using System;
using System.Collections.Generic;

public interface IChromosome<TGene, TCollection, TFitness> : IEnumerable<TGene>
    where TGene : IEquatable<TGene>
    where TCollection : ICollection<TGene>
    where TFitness : IComparable<TFitness>
{
    int Length { get; }
    TFitness Fitness { get; set; }
    IChromosome<TGene, TCollection, TFitness> Clone();
}
