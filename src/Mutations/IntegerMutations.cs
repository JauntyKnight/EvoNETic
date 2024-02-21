using System;
using System.Collections.Generic;


public class UniformIntMutation<TCollection> : IMutation<int, TCollection> 
    where TCollection : ICollection<int>, new()
{
    private Random _random;
    private int _minValue;
    private int _maxValue;
    private double _indpb;  // the probability of each gene getting mutated

    /// <summary>
    /// Mutate the chromosome by replacing each gene with a random integer value from a uniform distribution.
    /// </summary>
    /// <param name="minValue">The min value of the distribution</param>
    /// <param name="maxValue">The max value of the distribution</param>
    /// <param name="indpb">The probability of replacing a single gene</param>
    /// <param name="random">The random provider (optional)</param>
    public UniformIntMutation(int minValue, int maxValue, double indpb, Random random=null)
    {
        _random = random ?? new Random();
        _minValue = minValue;
        _maxValue = maxValue;
        _indpb = indpb;
    }
    
    /// <summary>
    /// Mutate the chromosome by replacing each gene with a random integer value from a uniform distribution.
    /// </summary>
    /// <param name="chromosome"></param>
    /// <param name="mutationProbability"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public IChromosome<int, TCollection> Mutate(IChromosome<int, TCollection> chromosome, double mutationProbability)
    {
        if (chromosome == null) throw new ArgumentNullException(nameof(chromosome));
        if (mutationProbability < 0 || mutationProbability > 1) throw new ArgumentOutOfRangeException(nameof(mutationProbability), "The probability has to be in the range [0, 1].");

        var result = chromosome.Clone();

        if (_random.NextDouble() > mutationProbability)
        {
            return result;
        }

        var result_genes = new List<int>(result);

        for (int i = 0; i < result_genes.Count; i++)
        {
            if (_random.NextDouble() < _indpb)
            {
                result_genes[i] = _random.Next(_minValue, _maxValue);
            }
        }

        return new Chromosome<int, TCollection>(result_genes);
    }
}
