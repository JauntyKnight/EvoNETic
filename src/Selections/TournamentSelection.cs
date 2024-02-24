using System;
using System.Collections.Generic;
using System.Linq;


public class TournamentSelection<TGene, TCollection, TFitness> : SelectionBase<TGene, TCollection, TFitness>, ISelection<TGene, TCollection, TFitness>
    where TGene : IEquatable<TGene>
    where TCollection : ICollection<TGene>, new()
    where TFitness : IComparable<TFitness>
{
    private readonly Random _random;
    private readonly int _tournamentSize;

    public TournamentSelection(int tournamentSize = 3, Random random = null)
    {
        _random = random ?? new Random();
        _tournamentSize = tournamentSize;
    }

    protected override IChromosome<TGene, TCollection, TFitness> SelectOne(IPopulation<TGene, TCollection, TFitness> population)
    {
        var selected = new List<IChromosome<TGene, TCollection, TFitness>>(_tournamentSize);
        var populationList = new List<IChromosome<TGene, TCollection, TFitness>>(population.Chromosomes);

        for (int i = 0; i < _tournamentSize; i++)
        {
            var index = _random.Next(populationList.Count);
            selected.Add(populationList[index]);
        }

        return selected.MaxBy(c => c.Fitness);
    }
}
