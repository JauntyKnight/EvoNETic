using System;
using System.Collections.Generic;

public class TwoPointCrossover<TGene, TCollection, TFitness> : ICrossover<TGene, TCollection, TFitness>
    where TGene : IEquatable<TGene>
    where TCollection : ICollection<TGene>, new()
    where TFitness : IComparable<TFitness>
{
    private Random _random;

    public TwoPointCrossover(Random random = null)
    {
        _random = random ?? new Random();
    }

    public IChromosome<TGene, TCollection, TFitness> Cross(IChromosome<TGene, TCollection, TFitness> chromosome1, IChromosome<TGene, TCollection, TFitness> chromosome2)
    {
        if (chromosome1.Length != chromosome2.Length)
        {
            throw new ArgumentException("The chromosomes must have the same length.");
        }

        var length = chromosome1.Length;
        var index1 = _random.Next(1, length - 1);
        var index2 = _random.Next(1, length - 1);

        while (index1 == index2)
        {
            index2 = _random.Next(1, length - 1);
        }

        if (index1 > index2)
        {
            (index1, index2) = (index2, index1);
        }

        var offspring = new TCollection();

        var enumerator1 = chromosome1.GetEnumerator();
        var enumerator2 = chromosome2.GetEnumerator();

        for (int i = 0; i < length; i++)
        {
            if (i < index1 || i > index2)
            {
                offspring.Add(enumerator1.Current);
            }
            else
            {
                offspring.Add(enumerator2.Current);
            }

            enumerator1.MoveNext();
            enumerator2.MoveNext();
        }

        return new Chromosome<TGene, TCollection, TFitness>(offspring);
    }
}

