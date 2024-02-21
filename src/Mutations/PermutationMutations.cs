using System;
using System.Collections.Generic;


public class RotateToRightMutation<TGene, TCollection> : IMutation<TGene, TCollection>
    where TGene : IEquatable<TGene>
    where TCollection : IList<TGene>, new()
{
    private Random _random;

    /// <summary>
    /// Mutate the chromosome by rotating a subset of genes to the right.
    /// </summary>
    /// <param name="random">The random numbers generator (optional)</param>
    public RotateToRightMutation(Random random=null)
    {
        _random = random ?? new Random();
    }

    /// <summary>
    /// Mutate the chromosome by rotating a subset of genes to the right.
    /// 
    /// Procedure:
    /// 1. Let a permutation P_0 be given.
    /// 2. Select a partial list of genes, i.e. a start index i, and a length j.
    /// 3. Select a number k, which is the number of positions to rotate the subset of genes.
    /// 4. Rotate the subset of genes to the right by k positions.
    /// 5. Return the mutated permutation.
    /// 
    /// Example:
    /// For a given permutation P_0 = (A, B, C, D, E, F, G), i = 5, j = 4, k = 1:
    /// The subset of genes is (F, G, A, B).
    /// The subset of genes rotated to the right by 1 position is (B, F, G, A).
    /// The mutated permutation is (B, F, C, D, E, G, A).
    /// </summary>
    /// <param name="chromosome"></param>
    /// <param name="mutationProbability"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public IChromosome<TGene, TCollection> Mutate(IChromosome<TGene, TCollection> chromosome, double mutationProbability)
    {
        if (chromosome == null) throw new ArgumentNullException(nameof(chromosome));
        if (mutationProbability < 0 || mutationProbability > 1) throw new ArgumentOutOfRangeException(nameof(mutationProbability), "The probability has to be in the range [0, 1].");

        if (_random.NextDouble() > mutationProbability)
        {
            return chromosome;
        }

        var result = chromosome.Clone();

        var genes = new List<TGene>(result);

        int i = _random.Next(genes.Count - 2);  // the start index of the subset of genes to rotate
        int j = _random.Next(1, genes.Count);  // the length of the subset of genes to rotate
        int k = _random.Next(1, j - 1);  // by how many positions to rotate the subset of genes

        var subset = new TGene[j];


        // create the subset of genes
        for (int l = 0; l < j; l++)
        {
            subset[l] = genes[(i + l) % genes.Count];
        }

        // rotate the subset of genes to the right by k positions
        for (int l = 0; l < j; l++)
        {
            genes[(i + l + k) % genes.Count] = subset[l];
        }

        result = new Chromosome<TGene, TCollection>(genes);

        return result;
    }
}


public class InversionMutation<TGene, TCollection> : IMutation<TGene, TCollection>
    where TGene : IEquatable<TGene>
    where TCollection : IList<TGene>, new()
{
    private Random _random;

    /// <summary>
    /// Mutate the chromosome by inverting a subset of genes.
    /// </summary>
    /// <param name="random">The random numbers generator (optional)</param>
    public InversionMutation(Random random=null)
    {
        _random = random ?? new Random();
    }

    /// <summary>
    /// Mutate the chromosome by inverting a subset of genes.
    /// 
    /// Procedure:
    /// 1. Let a permutation P_0 be given.
    /// 2. Select a partial list of genes, i.e. a start index i, and a length j.
    /// 3. Invert the subset of genes.
    /// 4. Return the mutated permutation.
    /// 
    /// Example:
    /// For a given permutation P_0 = (A, B, C, D, E, F, G), i = 2, j = 4:
    /// The subset of genes is (C, D, E, F).
    /// The subset of genes inverted is (F, E, D, C).
    /// The mutated permutation is (A, B, F, E, D, C, G).
    /// </summary>
    /// <param name="chromosome"></param>
    /// <param name="mutationProbability"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public IChromosome<TGene, TCollection> Mutate(IChromosome<TGene, TCollection> chromosome, double mutationProbability)
    {
        if (chromosome == null) throw new ArgumentNullException(nameof(chromosome));
        if (mutationProbability < 0 || mutationProbability > 1) throw new ArgumentOutOfRangeException(nameof(mutationProbability), "The probability has to be in the range [0, 1].");

        if (_random.NextDouble() > mutationProbability)
        {
            return chromosome;
        }

        var result = chromosome.Clone();

        var genes = new List<TGene>(result);

        int i = _random.Next(genes.Count - 2);  // the start index of the subset of genes to invert
        int j = _random.Next(1, genes.Count - i);  // the length of the subset of genes to invert

        var subset = new TGene[j];

        // create the subset of genes
        for (int l = 0; l < j; l++)
        {
            subset[l] = genes[(i + l) % genes.Count];
        }

        // invert the subset of genes
        for (int l = 0; l < j; l++)
        {
            genes[(i + l) % genes.Count] = subset[j - l - 1];
        }

        result = new Chromosome<TGene, TCollection>(genes);
        return result;
    }
}


public class SwapMutation<TGene, TCollection> : IMutation<TGene, TCollection>
    where TGene : IEquatable<TGene>
    where TCollection : IList<TGene>, new()
{
    private Random _random;

    /// <summary>
    /// Mutate the chromosome by swapping two genes.
    /// </summary>
    /// <param name="random">The random numbers generator (optional)</param>
    public SwapMutation(Random random=null)
    {
        _random = random ?? new Random();
    }

    /// <summary>
    /// Mutate the chromosome by swapping two genes.
    /// 
    /// Procedure:
    /// 1. Let a permutation P_0 be given.
    /// 2. Select two indices i and j, where i != j.
    /// 3. Swap the genes at positions i and j.
    /// 4. Return the mutated permutation.
    /// 
    /// Example:
    /// For a given permutation P_0 = (A, B, C, D, E, F, G), i = 2, j = 5:
    /// The genes at positions 2 and 5 are C and F, respectively.
    /// The mutated permutation is (A, B, F, D, E, C, G).
    /// </summary>
    /// <param name="chromosome"></param>
    /// <param name="mutationProbability"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public IChromosome<TGene, TCollection> Mutate(IChromosome<TGene, TCollection> chromosome, double mutationProbability)
    {
        if (chromosome == null) throw new ArgumentNullException(nameof(chromosome));
        if (mutationProbability < 0 || mutationProbability > 1) throw new ArgumentOutOfRangeException(nameof(mutationProbability), "The probability has to be in the range [0, 1].");

        if (_random.NextDouble() > mutationProbability)
        {
            return chromosome;
        }

        var result = chromosome.Clone();

        var genes = new List<TGene>(result);

        int i = _random.Next(genes.Count - 1);  // the first index of the subset of genes to swap
        int j = _random.Next(i + 1, genes.Count);  // the second index of the subset of genes to swap

        (genes[i], genes[j]) = (genes[j], genes[i]);

        result = new Chromosome<TGene, TCollection>(genes);
        return result;
    }
}