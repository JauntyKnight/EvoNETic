using System;
using System.Collections;
using System.Collections.Generic;

public class Population<TGene, TCollection, TFitness> : IPopulation<TGene, TCollection, TFitness>
    where TGene : IEquatable<TGene>
    where TCollection : ICollection<TGene>, new()
    where TFitness : IComparable<TFitness>
{
    public int Size { get; }

    public ICollection<IChromosome<TGene, TCollection, TFitness>> Chromosomes { get; }

    public Population(ICollection<IChromosome<TGene, TCollection, TFitness>> chromosomes)
    {
        Chromosomes = new List<IChromosome<TGene, TCollection, TFitness>>(chromosomes);
        Size = Chromosomes.Count;

        foreach (var chromosome in Chromosomes)
        {
            Chromosomes.Add(chromosome);
        }
    }

    public Population(ICollection<ICollection<TGene>> genotype)
    {
        Chromosomes = new List<IChromosome<TGene, TCollection, TFitness>>();

        foreach (var chromosome in genotype)
        {
            Chromosomes.Add(new Chromosome<TGene, TCollection, TFitness>(chromosome));
        }

        Size = Chromosomes.Count;
    }

    public Population(int populationSize, Func<TGene> geneFactory, int chromosomeSize)
    {
        Chromosomes = new List<IChromosome<TGene, TCollection, TFitness>>();

        for (int i = 0; i < populationSize; i++)
        {
            Chromosomes.Add(new Chromosome<TGene, TCollection, TFitness>(geneFactory, chromosomeSize));
        }

        Size = Chromosomes.Count;
    }

    public IEnumerator<IChromosome<TGene, TCollection, TFitness>> GetEnumerator()
    {
        return Chromosomes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
