using System;
using System.Collections.Generic;


public class UniformIntMutation<TCollection, TFitness> : IMutation<int, TCollection, TFitness>
    where TCollection : ICollection<int>, new()
    where TFitness : IComparable<TFitness>
{
    private readonly Random _random;
    private readonly int _minValue;
    private readonly int _maxValue;
    private readonly double _indpb;  // the probability of each gene getting mutated
    private readonly double _mutationProbability;  // the probability of the chromosome getting mutated

    /// <summary>
    /// Mutate the chromosome by replacing each gene with a random integer value from a uniform distribution.
    /// </summary>
    /// <param name="minValue">The min value of the distribution</param>
    /// <param name="maxValue">The max value of the distribution</param>
    /// <param name="indpb">The probability of replacing a single gene</param>
    /// <param name="random">The random provider (optional)</param>
    public UniformIntMutation(double mutation, int minValue, int maxValue, double indpb, Random random = null)
    {
        _random = random ?? new Random();
        _minValue = minValue;
        _maxValue = maxValue;
        _indpb = indpb;
        _mutationProbability = mutation;
    }

    /// <summary>
    /// Mutate the chromosome by replacing each gene with a random integer value from a uniform distribution.
    /// </summary>
    /// <param name="chromosome"></param>
    /// <param name="mutationProbability"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public IChromosome<int, TCollection, TFitness> Mutate(IChromosome<int, TCollection, TFitness> chromosome)
    {
        if (chromosome == null) throw new ArgumentNullException(nameof(chromosome));


        if (_random.NextDouble() > _mutationProbability)
        {
            return chromosome;
        }

        var result = chromosome.Clone();

        var result_genes = new List<int>(result);

        for (int i = 0; i < result_genes.Count; i++)
        {
            if (_random.NextDouble() < _indpb)
            {
                result_genes[i] = _random.Next(_minValue, _maxValue);
            }
        }

        return new Chromosome<int, TCollection, TFitness>(result_genes);
    }
}
