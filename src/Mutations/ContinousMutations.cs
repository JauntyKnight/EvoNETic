using System;
using System.Collections.Generic;

namespace EvoNETic
{
    public class GaussianMutation<TCollection, TFitness> : IMutation<double, TCollection, TFitness>
        where TCollection : ICollection<double>, new()
        where TFitness : IComparable<TFitness>
    {
        private readonly Random _random;
        private readonly double _mu;
        private readonly double _sigma;
        private readonly double _indpb;  // the probability of each gene getting mutated
        private readonly double _mutationProbability;  // the probability of the chromosome getting mutated

        /// <summary>
        /// Mutate the chromosome by replacing each gene with a random integer value from a uniform distribution.
        /// </summary>
        /// <param name="mutationProbability">The probability of the chromosome getting mutated</param>
        /// <param name="mu">The mean of the distribution</param>
        /// <param name="sigma">The standard deviation of the distribution</param>
        /// <param name="indpb">The probability of replacing a single gene</param>
        /// <param name="random">The random provider (optional)</param>
        public GaussianMutation(double mutationProbability, double mu, double sigma, double indpb, Random random = null)
        {
            _random = random ?? new Random();
            _mu = mu;
            _sigma = sigma;
            _indpb = indpb;
            _mutationProbability = mutationProbability;
        }

        /// <summary>
        /// Mutate the chromosome by replacing each gene with a random integer value from a uniform distribution.
        /// </summary>

        public IChromosome<double, TCollection, TFitness> Mutate(IChromosome<double, TCollection, TFitness> chromosome)
        {
            if (chromosome == null) throw new ArgumentNullException(nameof(chromosome));


            if (_random.NextDouble() > _mutationProbability)
            {
                return chromosome;
            }

            var result = chromosome.Clone();

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
}
