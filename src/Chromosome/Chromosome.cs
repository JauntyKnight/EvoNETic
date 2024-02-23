using System;
using System.Collections;
using System.Collections.Generic;

public class Chromosome<TGene, TCollection, TFitness> : IChromosome<TGene, TCollection, TFitness>
    where TGene : IEquatable<TGene>
    where TCollection : ICollection<TGene>, new()
    where TFitness : IComparable<TFitness>
{
    public int Length { get; }
    public TFitness Fitness { get; set; }

    private TCollection _genes;

    public Chromosome(ICollection<TGene> genes)
    {
        _genes = new TCollection();

        foreach (var gene in genes)
        {
            _genes.Add(gene);
        }

        Length = _genes.Count;
        Fitness = default;
    }

    public Chromosome(Func<TGene> geneFactory, int length)
    {
        _genes = new TCollection();
        Fitness = default;

        for (int i = 0; i < length; i++)
        {
            _genes.Add(geneFactory());
        }

        Length = _genes.Count;
    }

    public Chromosome(IChromosome<TGene, TCollection, TFitness> chromosome)
    {
        _genes = new TCollection();

        foreach (var gene in chromosome)
        {
            _genes.Add(gene);
        }

        Length = _genes.Count;
        Fitness = chromosome.Fitness;
    }

    public static implicit operator TCollection(Chromosome<TGene, TCollection, TFitness> chromosome)
    {
        TCollection genes = new TCollection();

        foreach (var gene in chromosome)
        {
            genes.Add(gene);
        }

        return genes;
    }

    public static implicit operator Chromosome<TGene, TCollection, TFitness>(TCollection genes)
    {
        return new Chromosome<TGene, TCollection, TFitness>(genes);
    }

    public IChromosome<TGene, TCollection, TFitness> Clone()
    {
        return new Chromosome<TGene, TCollection, TFitness>(_genes);
    }

    public IEnumerator<TGene> GetEnumerator()
    {
        return _genes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", _genes)}]";
    }
}
