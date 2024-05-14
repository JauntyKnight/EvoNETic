using System;
using System.Collections.Generic;

namespace EvoNETic
{
    public class OrderCrossover<TGene, TCollection, TFitness> : ICrossover<TGene, TCollection, TFitness>
        where TGene : IEquatable<TGene>
        where TCollection : ICollection<TGene>, new()
        where TFitness : IComparable<TFitness>
    {
        private Random _random;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderCrossover{TGene, TCollection}"/> class.
        /// </summary>
        /// <param name="random"></param>
        public OrderCrossover(Random random = null)
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
        public IChromosome<TGene, TCollection, TFitness> Cross(IChromosome<TGene, TCollection, TFitness> chromosome1, IChromosome<TGene, TCollection, TFitness> chromosome2)
        {
            if (chromosome1.Length != chromosome2.Length)
            {
                throw new ArgumentException("The chromosomes must have the same length.");
            }

            var length = chromosome1.Length;
            var segment_start = _random.Next(length);
            var segment_finsih = segment_start + _random.Next(length);

            // Console.WriteLine(segment_start);
            // Console.WriteLine(segment_finsih);

            var offspring = new TCollection();
            // Console.WriteLine($"offspring: {offspring.Count}");
            var enumerator1 = chromosome1.GetEnumerator();
            var enumerator2 = chromosome2.GetEnumerator();
            var used_vals = new HashSet<TGene>();
            var all_vals = new HashSet<TGene>();

            // Console.WriteLine($"enumerator1: {enumerator1.Current}");
            // Console.WriteLine($"enumerator2: {enumerator2.Current}");
            enumerator1.MoveNext();
            enumerator2.MoveNext();

            // Console.WriteLine("Starting crossover");

            for (int i = 0; i < Math.Min(length, segment_finsih); ++i)
            {
                if (i >= segment_start)
                {
                    used_vals.Add(enumerator1.Current);
                }

                enumerator1.MoveNext();
            }

            enumerator1.Reset();
            enumerator1.MoveNext();

            for (int i = 0; i < length; ++i)
            {
                if (i >= segment_start && i < segment_finsih)
                {
                    offspring.Add(enumerator1.Current);
                    all_vals.Add(enumerator1.Current);
                }
                else
                {
                    while (used_vals.Contains(enumerator2.Current))
                    {
                        enumerator2.MoveNext();
                    }

                    offspring.Add(enumerator2.Current);
                    all_vals.Add(enumerator2.Current);
                    enumerator2.MoveNext();
                }

                enumerator1.MoveNext();
            }

            // Console.WriteLine("Finished crossover");

            if (all_vals.Count != length)
            {
                Console.WriteLine($"segment_start: {segment_start}, segment_end: {segment_finsih}");
                Console.WriteLine($"all values: {all_vals.Count}, Length: {length}");
                Console.Write("Enumerator1: [");
                enumerator1.Reset();
                enumerator2.Reset();
                while (enumerator1.MoveNext())
                {
                    Console.Write($"{enumerator1.Current}, ");
                }
                enumerator1.Reset();
                Console.WriteLine("]");

                Console.Write("Enumerator2: [");
                while (enumerator2.MoveNext())
                {
                    Console.Write($"{enumerator2.Current}, ");
                }
                enumerator2.Reset();
                Console.WriteLine("]");

                Console.WriteLine("Used values: ");
                foreach (var val in used_vals)
                {
                    Console.Write($"{val}, ");
                }
                Console.WriteLine();
                Console.Write("Offspring:   [");
                foreach (var val in offspring)
                {
                    Console.Write($"{val}, ");
                }
                Console.WriteLine("]");
                throw new ArgumentException("The generated offspring has different size than the parents!.");
            }

            return new Chromosome<TGene, TCollection, TFitness>(offspring);
        }
    }
}
