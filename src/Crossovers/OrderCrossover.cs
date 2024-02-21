using System;
using System.Collections.Generic;


public class OrderCrossover<TGene, TCollection> : ICrossover<TGene, TCollection>
    where TGene : IEquatable<TGene>
    where TCollection : ICollection<TGene>, new()
{
    private Random _random;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderCrossover{TGene, TCollection}"/> class.
    /// </summary>
    /// <param name="random"></param>
    public OrderCrossover(Random random=null)
    {
        _random = random ?? new Random();
    }

    /// <summary>
    /// The order crossover (OX1) is an operator used on two parents to create two children.
    /// The idea is to keep a subset of the first parent and fill the remainder of the child with the genes from the second parent, 
    /// in the order in which they appear, without duplicating genes in the subset.
    /// </summary>
    /// <param name="chromosome1"></param>
    /// <param name="chromosome2"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public IChromosome<TGene, TCollection> Cross(IChromosome<TGene, TCollection> chromosome1, IChromosome<TGene, TCollection> chromosome2)
    {
        if (chromosome1.Length != chromosome2.Length)
        {
            throw new ArgumentException("The chromosomes must have the same length.");
        }

        var length = chromosome1.Length;
        var index1 = _random.Next(0, length - 1);
        var index2 = _random.Next(index1, length + 1);

        var offspring = new TCollection();
        var enumerator1 = chromosome1.GetEnumerator();
        var enumerator2 = chromosome2.GetEnumerator();
        var used_vals = new HashSet<TGene>();

        for (int i = 0; i < index2; i++)
        {
            if (i >= index1)
            {
                used_vals.Add(enumerator1.Current);
            }
        }

        // reset enumerator1
        enumerator1 = chromosome1.GetEnumerator();

        for (int i = 0; i < length; ++i) {
            if (i >= index1 && i < index2) {
                offspring.Add(enumerator1.Current);
            } else {
                while (used_vals.Contains(enumerator2.Current)) {
                    enumerator2.MoveNext();
                }
                
                offspring.Add(enumerator2.Current);
            }

            enumerator1.MoveNext();
        }
        

        return new Chromosome<TGene, TCollection>(offspring);
    }
}
