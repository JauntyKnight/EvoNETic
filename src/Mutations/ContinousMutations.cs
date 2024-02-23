using System;
using System.Collections.Generic;


public class GaussianMutation<TCollection, TFitness> : IMutation<double, TCollection, TFitness>
    where TCollection : ICollection<double>, new()
    where TFitness : IComparable<TFitness>
{
    private Random _random;
    private double _mu;
    private double _sigma;
    private double _indpb;  // the probability of each gene getting mutated

    /// <summary>
    /// Mutate the chromosome by replacing each gene with a random integer value from a uniform distribution.
    /// </summary>
    /// <param name="mu">The mean of the distribution</param>
    /// <param name="sigma">The standard deviation of the distribution</param>
    /// <param name="indpb">The probability of replacing a single gene</param>
    /// <param name="random">The random provider (optional)</param>
    public GaussianMutation(double mu, double sigma, double indpb, Random random = null)
    {
        _random = random ?? new Random();
        _mu = mu;
        _sigma = sigma;
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
    public IChromosome<double, TCollection, TFitness> Mutate(IChromosome<double, TCollection, TFitness> chromosome, double mutationProbability)
    {
        if (chromosome == null) throw new ArgumentNullException(nameof(chromosome));
        if (mutationProbability < 0 || mutationProbability > 1) throw new ArgumentOutOfRangeException(nameof(mutationProbability), "The probability has to be in the range [0, 1].");

        var result = chromosome.Clone();

        if (_random.NextDouble() > mutationProbability)
        {
            return result;
        }

        var result_genes = new List<double>(result);

        for (int i = 0; i < result_genes.Count; i++)
        {
            if (_random.NextDouble() < _indpb)
            {
                result_genes[i] = result_genes[i] + ZigguratGaussian.Sample(_random, _mu, _sigma);
            }
        }

        return new Chromosome<double, TCollection, TFitness>(result_genes);
    }
}
