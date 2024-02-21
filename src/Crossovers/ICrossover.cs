using System;
using System.Collections.Generic;

public interface ICrossover<TGene, TCollection>
    where TGene : IEquatable<TGene>
    where TCollection : ICollection<TGene>
{
    
    IChromosome<TGene, TCollection> Cross(IChromosome<TGene, TCollection> chromosome1, IChromosome<TGene, TCollection> chromosome2);
}
