using System;
using System.Collections.Generic;

namespace EvoNETic
{
    public abstract class SelectionBase<TGene, TCollection, TFitness> : ISelection<TGene, TCollection, TFitness>
        where TGene : IEquatable<TGene>
        where TCollection : ICollection<TGene>, new()
        where TFitness : IComparable<TFitness>
    {
        protected abstract IChromosome<TGene, TCollection, TFitness> SelectOne(IPopulation<TGene, TCollection, TFitness> population);

        public ICollection<IChromosome<TGene, TCollection, TFitness>> Select(IPopulation<TGene, TCollection, TFitness> population, int number)
        {
            var selected = new List<IChromosome<TGene, TCollection, TFitness>>(number);
            for (int i = 0; i < number; i++)
            {
                selected.Add(SelectOne(population));
            }
            return selected;
        }
    }
}
