using System;
using System.Collections.Generic;


public interface IMutation<TGene, TCollection, TFitness>
    where TGene : IEquatable<TGene>
    where TCollection : ICollection<TGene>
    where TFitness : IComparable<TFitness>
{
    IChromosome<TGene, TCollection, TFitness> Mutate(IChromosome<TGene, TCollection, TFitness> chromosome, double mutationProbability);
}
