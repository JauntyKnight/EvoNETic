using System;
using System.Collections.Generic;
using System.Linq;


#nullable enable

public class EAMuPlusLambda<TGene, TCollection, TFitness> : AlgorithmBase<TGene, TCollection, TFitness>
    where TGene : IEquatable<TGene>
    where TCollection : ICollection<TGene>, new()
    where TFitness : IComparable<TFitness>
{
    private readonly int _mu;
    private readonly int _lambda;

    public EAMuPlusLambda(
        int mu,
        int lambda,
        ICrossover<TGene, TCollection, TFitness> crossover,
        IMutation<TGene, TCollection, TFitness> mutation,
        ISelection<TGene, TCollection, TFitness> selection,
        IFitnessFunc<TGene, TCollection, TFitness> fitnessFunc,
        int generationsNumber,
        TFitness? targetFitness = default,
        TimeSpan? maxTime = null
    ) : base(
        crossover,
        mutation,
        selection,
        fitnessFunc,
        generationsNumber,
        targetFitness,
        maxTime
    )
    {
        _mu = mu;
        _lambda = lambda;
    }


    public override void Evolve(IPopulation<TGene, TCollection, TFitness> population)
    {
        var StartTime = DateTime.Now;
        PrintHeader();

        for (int generation = 0; generation < GenerationsNumber; generation++)
        {
            // check the stoping criteria
            // if (_targetFitness != default(TFitness) && BestChromosome is not null && BestChromosome.Fitness.CompareTo(_targetFitness) >= 0)
            // {
            //     Console.WriteLine("Target fitness reached, stopping the algorithm");
            //     return;
            // }
            // if (_maxTime is not null && TimeElapsed.CompareTo(_maxTime) >= 0)
            // {
            //     Console.WriteLine("Max time reached, stopping the algorithm");
            //     return;
            // }

            // evaluate the fitness of each chromosome
            foreach (var chromosome in population)
            {
                chromosome.Fitness = FitnessFunc.Evaluate(chromosome);
            }

            // generate the offspring
            var offspring = new List<IChromosome<TGene, TCollection, TFitness>>(_lambda);

            for (int i = 0; i < _lambda; i++)
            {
                var parentsPair = Selection.Select(population, 2);
                var offspringChromosome = Crossover.Cross(parentsPair[0], parentsPair[1]);
                offspringChromosome = Mutation.Mutate(offspringChromosome);

                // evaluate the fitness of the offspring and add it to the offspring list
                offspringChromosome.Fitness = FitnessFunc.Evaluate(offspringChromosome);
                offspring.Add(offspringChromosome);
            }

            // select the best mu chromosomes from the population and the offspring
            var allChromosomes = new List<IChromosome<TGene, TCollection, TFitness>>(population);
            allChromosomes.AddRange(offspring);
            allChromosomes.Sort((x, y) => y.Fitness.CompareTo(x.Fitness));
            var newPopulation = allChromosomes.GetRange(0, _mu);

            population = new Population<TGene, TCollection, TFitness>(newPopulation);

            // update the best chromosome
            if (BestChromosome is null || newPopulation[0].Fitness.CompareTo(BestChromosome.Fitness) > 0)
            {
                BestChromosome = newPopulation[0];
            }

            TimeElapsed = DateTime.Now - StartTime;

            PrintProgress(generation, newPopulation);
        }
    }
}
