using System;
using System.Collections.Generic;
using System.Linq;

namespace EvoNETic
{
    public class RouletteSelection<TGene, TCollection> : SelectionBase<TGene, TCollection, double>, ISelection<TGene, TCollection, double>
        where TGene : IEquatable<TGene>
        where TCollection : ICollection<TGene>, new()
    {
        private readonly Random _random;

        public RouletteSelection(Random random = null)
        {
            _random = random ?? new Random();
        }

        protected override IChromosome<TGene, TCollection, double> SelectOne(IPopulation<TGene, TCollection, double> population)
        {
            var populationList = new List<IChromosome<TGene, TCollection, double>>(population.Chromosomes);
            populationList.Sort((c1, c2) => c1.Fitness.CompareTo(c2.Fitness));

            var totalFitness = populationList.Sum(c => c.Fitness);
            var randomFitness = _random.NextDouble() * totalFitness;

            var currentFitness = 0.0;
            foreach (var chromosome in populationList)
            {
                currentFitness += chromosome.Fitness;
                if (currentFitness >= randomFitness)
                {
                    return chromosome;
                }
            }

            return populationList.Last();
        }
    }
}
