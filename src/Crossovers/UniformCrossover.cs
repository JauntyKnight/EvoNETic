using System;
using System.Collections.Generic;

public class UniformCrossover<TGene, TCollection> : ICrossover<TGene, TCollection>
    where TGene : IEquatable<TGene>
    where TCollection : ICollection<TGene>, new()
{
    private readonly Random _random;

    /// <summary>
    /// Initializes a new instance of the <see cref="UniformCrossover{TGene, TCollection}"/> class.
    /// </summary>
    /// <param name="random"></param>
    public UniformCrossover(Random random=null)
    {
        _random = random ?? new Random();
    }

    public IChromosome<TGene, TCollection> Cross(IChromosome<TGene, TCollection> chromosome1, IChromosome<TGene, TCollection> chromosome2)
    {
        if (chromosome1.Length != chromosome2.Length)
        {
            throw new ArgumentException("The chromosomes must have the same length.");
        }

        var offspring = new TCollection();

        var enumerator1 = chromosome1.GetEnumerator();
        var enumerator2 = chromosome2.GetEnumerator();

        for (int i = 0; i < chromosome1.Length; i++)
        {
            if (_random.NextDouble() < 0.5)
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

        return new Chromosome<TGene, TCollection>(offspring);
    }
}
