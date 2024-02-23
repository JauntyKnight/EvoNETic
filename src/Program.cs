using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // test permutations
        var chromosome = new Chromosome<int, List<int>, double>(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        var chromosome2 = new Chromosome<int, List<int>, double>(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        var mutation = new InversionMutation<int, List<int>, double>();

        for (int i = 0; i < 10; i++)
        {
            chromosome2 = (Chromosome<int, List<int>, double>)mutation.Mutate(chromosome, 1.0);

            try
            {
                IsPermutation(chromosome2, 10);
            }
            catch (InvalidDataException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(chromosome);
                Console.WriteLine(chromosome2);
            }

            Console.WriteLine(chromosome);
            Console.WriteLine(chromosome2);
            chromosome = chromosome2;

        }

    }

    static void IsPermutation(IEnumerable<int> sequence, int n)
    {
        var set = new HashSet<int>();

        foreach (var i in sequence)
        {
            if (i < 0 || i >= n)
            {
                throw new InvalidDataException("The sequence is not a permutation.");
            }

            if (set.Contains(i))
            {
                throw new InvalidDataException("The sequence is not a permutation.");
            }

            set.Add(i);
        }
    }
}
