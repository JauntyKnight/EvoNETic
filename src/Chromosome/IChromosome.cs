using System;
using System.Collections.Generic;

public interface IChromosome<TGene, TCollection> : IEnumerable<TGene>
    where TGene : IEquatable<TGene>
    where TCollection : ICollection<TGene>
{
    int Length { get; }
    double Fitness { get; set; }
    IChromosome<TGene, TCollection> Clone();
}
