using System;
using System.Collections.Generic;

namespace EvoNETic
{
    public class OnePointCrossover<TGene, TCollection, TFitness> : ICrossover<TGene, TCollection, TFitness>
        where TGene : IEquatable<TGene>
        where TCollection : ICollection<TGene>, new()
        where TFitness : IComparable<TFitness>
    {
        private Random _random;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnePointCrossover{TGene, TCollection}"/> class.
        /// </summary>
        /// <param name="random">The random generator (optional)</param>
        public OnePointCrossover(Random random = null)
        {
            _random = random ?? new Random();
        }

        /// <summary>
        /// The one point crossover (also called single point crossover) is a genetic operator used on chromosomes.
        /// It separates the genes of two chromosomes at one point to combine them to form new offspring.
        /// </summary>
        /// <param name="chromosome1"></param>
        /// <param name="chromosome2"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public IChromosome<TGene, TCollection, TFitness> Cross(IChromosome<TGene, TCollection, TFitness> chromosome1, IChromosome<TGene, TCollection, TFitness> chromosome2)
        {
            if (chromosome1.Length != chromosome2.Length)
            {
                throw new ArgumentException("Chromosomes must have the same length.");
            }

            int split_point = _random.Next(1, chromosome1.Length - 1);  // how many genes to take from chromosome1

            var genes = new TCollection();

            var enumerator1 = chromosome1.GetEnumerator();
            var enumerator2 = chromosome2.GetEnumerator();

            for (int i = 0; i < chromosome1.Length; i++)
            {
                if (i < split_point)
                {
                    genes.Add(enumerator1.Current);
                }
                else
                {
                    genes.Add(enumerator2.Current);
                }
                enumerator1.MoveNext();
                enumerator2.MoveNext();
            }

            return new Chromosome<TGene, TCollection, TFitness>(genes);
        }
    }
}
