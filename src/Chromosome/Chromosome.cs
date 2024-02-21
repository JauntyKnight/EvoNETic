using System;
using System.Collections;
using System.Collections.Generic;

public class Chromosome<TGene, TCollection> : IChromosome<TGene, TCollection>
    where TGene : IEquatable<TGene>
    where TCollection : ICollection<TGene>, new()
{
    public int Length { get; }
    public double Fitness { get; set; }

    private TCollection _genes;

    public Chromosome(ICollection<TGene> genes)
    {
        _genes = new TCollection();

        foreach (var gene in genes)
        {
            _genes.Add(gene);
        }

        Length = _genes.Count;
        Fitness = 0;
    }

    public Chromosome(Func<TGene> geneFactory, int length)
    {
        _genes = new TCollection();
        Fitness = 0;

        for (int i = 0; i < length; i++)
        {
            _genes.Add(geneFactory());
        }

        Length = _genes.Count;
    }

    public Chromosome(IChromosome<TGene, TCollection> chromosome)
    {
        _genes = new TCollection();

        foreach (var gene in chromosome)
        {
            _genes.Add(gene);
        }

        Length = _genes.Count;
        Fitness = chromosome.Fitness;
    }

    public static implicit operator TCollection(Chromosome<TGene, TCollection> chromosome)
    {
        return chromosome._genes;
    }

    public static implicit operator Chromosome<TGene, TCollection>(TCollection genes)
    {
        return new Chromosome<TGene, TCollection>(genes);
    }

    public IChromosome<TGene, TCollection> Clone()
    {
        return new Chromosome<TGene, TCollection>(_genes);
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
