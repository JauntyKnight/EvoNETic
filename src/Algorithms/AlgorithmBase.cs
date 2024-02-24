using System;
using System.Collections.Generic;
using System.Linq;


public abstract class AlgorithmBase<TGene, TCollection, TFitness> : IEvolutionaryAlgorithm<TGene, TCollection, TFitness>
    where TGene : IEquatable<TGene>
    where TCollection : ICollection<TGene>, new()
    where TFitness : IComparable<TFitness>
{
    public int GenerationsNumber { get; }


    public IChromosome<TGene, TCollection, TFitness> BestChromosome { get; protected set; }

    public ICrossover<TGene, TCollection, TFitness> Crossover { get; }
    public IMutation<TGene, TCollection, TFitness> Mutation { get; }
    public ISelection<TGene, TCollection, TFitness> Selection { get; }
    public IFitnessFunc<TGene, TCollection, TFitness> FitnessFunc { get; }

    public abstract void Evolve(IPopulation<TGene, TCollection, TFitness> population);

    public TimeSpan TimeElapsed { get; protected set; }

#nullable enable
    protected readonly TFitness? _targetFitness;
    protected readonly TimeSpan? _maxTime;

    public AlgorithmBase(
        ICrossover<TGene, TCollection, TFitness> crossover,
        IMutation<TGene, TCollection, TFitness> mutation,
        ISelection<TGene, TCollection, TFitness> selection,
        IFitnessFunc<TGene, TCollection, TFitness> fitnessFunc,
        int generationsNumber,
        TFitness? targetFitness = default,
        TimeSpan? maxTime = null
    )
    {
        Crossover = crossover;
        Mutation = mutation;
        Selection = selection;
        FitnessFunc = fitnessFunc;
        _targetFitness = targetFitness;
        _maxTime = maxTime;
        GenerationsNumber = generationsNumber;
    }

    protected void PrintHeader()
    {
        Console.WriteLine(String.Format("{0, -5} | {1, -5} | {2, -10} | {3, -10} | {4, -10}", "Gen", "Time", "Generation Max", "Generation Min", "All Time Best"));
    }

    protected void PrintProgress(int generation, IList<IChromosome<TGene, TCollection, TFitness>> population)
    {
        Console.WriteLine(String.Format("{0, -5} | {1, -5} | {2, -10} | {3, -10} | {4, -10}", generation, TimeElapsed, population.First().Fitness, population.Last().Fitness, BestChromosome.Fitness));
    }
}

