using System;
using System.Collections.Generic;


public interface IMutation<TGene, TCollection>
    where TGene : IEquatable<TGene>
    where TCollection : ICollection<TGene>
{
    IChromosome<TGene, TCollection> Mutate(IChromosome<TGene, TCollection> chromosome, double mutationProbability);
}