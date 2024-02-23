using System;
using System.Collections;
using System.Collections.Generic;


public interface ISelection<TGene, TCollection, TFitness>
    where TGene : IEquatable<TGene>
    where TCollection : ICollection<TGene>, new()
    where TFitness : IComparable<TFitness>
{
    /// <summary>
    /// Selects a list of chromosomes from the population, according to their fitness.
    /// </summary>
    /// <param name="population"></param>
    /// <returns></returns>
    IList<IChromosome<TGene, TCollection, TFitness>> Select(IList<IChromosome<TGene, TCollection, TFitness>> population);
}
