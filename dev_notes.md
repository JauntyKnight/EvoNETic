# Developer Documentation

## `IChromosome` Interface

A chromosome is a candidate solution the problem that we are trying to solve. It is a sequence of genes that represent a potential solution. `IChromosome` is a generic interface `IChromosome<TGene, TCollection, TFitness>`, where:
- `TGene` is the type of the genes in the chromosome. Must implement `IEquatable<TGene>`.
- `TCollection` is the type of the collection that holds the genes. Must implement `ICollection<TGene>`.
- `TFitness` is the type of the fitness value of the chromosome. Must implement `IComparable<TFitness>`.

Additionally, the interface must also implement `IEnumerable<TGene>`, and a `.Clone()` method that returns a deep copy of the chromosome.

The library offers an out-of-the-box implementation of the interface, `Chromosome<TGene, TCollection, TFitness>`, which also offers some constructors for ease of life.

## `IFitnessFunction` Interface

This interface evaluates the quality of a chromosome (a candidate solution), i.e. how well it solves the problem. It is a generic interface with respect to the same types as the chromosome: `TGene`, `TCollection`, and `TFitness`. It has a single method `Evaluate(IChromosome<TGene, TCollection, TFitness> chromosome)` that returns the fitness value of the chromosome.

## `IPopulation` Interface

A population is a collection that encapsulates a set of chromosomes. It must also implement the `IEnumerable<IChromosome<TGene, TCollection, TFitness>>` interface.

An out-of-the-box implementation of the interface is `Population<TGene, TCollection, TFitness>`, which provides a few constructors to facilitate the creation of populations.

## `ISelection` Interface

Selection is the process of selecting chromosomes from the population to be used in the next generation. The `ISelection` interface is a generic interface with respect to the same types as the chromosome: `TGene`, `TCollection`, and `TFitness`. It has a single method `ICollection<IChromosome<TGene, TCollection, TFitness>> Select(IPopulation<TGene, TCollection, TFitness> population, int count)` that returns a collection of `count`(usually 2) chromosomes selected from the population.

### `SelectionBase` Abstract Class

The class implements `.Select(IPopulation<TGene, TCollection, TFitness> population, int count)` method, by running a loop `count` times and calling the abstract method `.SelectOne(IPopulation<TGene, TCollection, TFitness> population)` in each iteration. The method returns a collection of the selected chromosomes.

### `RouletteWheelSelection` Class

In a roulette wheel selection, each chromosome is assigned a probability of being selected, proportional to its fitness value.

This class extends `SelectionBase` and implements the `.SelectOne(IPopulation<TGene, TCollection, double> population)` method. It assumes `TFitness` is of type `double`. It selects a chromosome from the population using the roulette wheel selection method. Its constructor takes an optional parameter of `System.Random` object, to provide the user more control over the selection process, if desired.

The `.SelectOne(IPopulation<TGene, TCollection, TFitness> population)` method performs a shallow copy of the population to a list, then sorts the population in increasing order of the fitness values. It then calculates the cumulative sum of the fitness values, and selects a random number between 0 and the sum. It then iterates over the population, and returns the first chromosome whose cumulative sum (the sum of all its predecessors) is greater than the random number.

Complexity: `O(n log n)`, where `n` is the population size.

### `TournamentSelection` Class

In tournament selection, a random subset of the population is selected, and the best chromosome from the subset is chosen.

This class extends `SelectionBase` and implements the `.SelectOne(IPopulation<TGene, TCollection, TFitness> population)` method. It selects a chromosome from the population using the tournament selection method. Its constructor takes an optional parameter of `System.Random` object, to provide the user more control over the selection process, if desired.

The `.SelectOne(IPopulation<TGene, TCollection, TFitness> population)` method performs a shallow copy of the population to a list, then selects a random subset of the population. It then returns the chromosome with the best fitness value from the subset.

Complexity: `O(n)`, where `n` is the population size. The bottleneck is the copying of the population to a list.


## `ICrossover` Interface

Crossover is the process of combining two parent chromosomes to produce one or more offspring chromosomes. The `ICrossover` interface is a generic interface with respect to the same types as the chromosome: `TGene`, `TCollection`, and `TFitness`. It has a single method `IChromosome<TGene, TCollection, TFitness> Cross(IChromosome<TGene, TCollection, TFitness> parent1, IChromosome<TGene, TCollection, TFitness> parent2)` that returns a new chromosome that is a result of the crossover of the two parent chromosomes.

### `OnePointCrossover` Class

This crossover method selects a random point in the parent chromosomes, so that the offspring chromosomes are a combination of the genes before the point from one parent, and the genes after the point from the other parent.

The constructor of this class takes an optional parameter of `System.Random` object, to provide the user more control over the crossover process, if desired.

The `.Cross(IChromosome<TGene, TCollection, TFitness> parent1, IChromosome<TGene, TCollection, TFitness> parent2)` method selects a random point in the parent chromosomes. It then iterates over the genes of the both parents simultaneously, and copies the genes before the point from the first parent, and the genes after the point from the second parent, to create the offspring chromosome.

Complexity: `O(n)`, where `n` is the number of genes in the chromosome.

### `TwoPointCrossover` Class

A generalization of the one-point crossover, this method selects two random points in the parent chromosomes, so that the offspring chromosomes are a combination of the genes between the two points from one parent, and the genes outside the two points from the other parent.

Complexity: `O(n)`, where `n` is the number of genes in the chromosome.

### `UniformCrossover` Class

Yet another generalization of the one-point crossover. It iterates over the genes of the both parents simultaneously, and for each gene, it randomly selects one of the parents to copy the gene from.

Complexity: `O(n)`, where `n` is the number of genes in the chromosome.

### `OrderCrossover` Class

This method is used for chromosomes that represent permutations. It selects a random subset of the genes from one parent, and fills the remaining genes in the offspring with the genes from the other parent, in the order they appear in the second parent. Everything is done in 2 iterations over the parent chromosomes.

Complexity: `O(n)`, where `n` is the number of genes in the chromosome.


## `IMutation` Interface

Mutation is the process of changing a gene in a chromosome. The `IMutation` interface is a generic interface with respect to the same types as the chromosome: `TGene`, `TCollection`, and `TFitness`. It has a single method `IChromosome<TGene, TCollection, TFitness> Mutate(IChromosome<TGene, TCollection, TFitness> chromosome)` that returns a new chromosome that is a result of the mutation of the input chromosome.

### `GaussianMutation` Class

This operator adds some random noise to the genes of the chromosome from a Gaussian distribution, with some probability. It assumes `TGene` is of type `double`.

The constructor accepts parameters to control the the behavior of the mutation:
- `mutationProbability`: The probability of a chromosome undergoing mutation in the first place.
- `mu`: The mean of the Gaussian distribution.
- `sigma`: The standard deviation of the Gaussian distribution.
- `indpb`: The probability of a gene undergoing mutation.
- `random`: An optional `System.Random` object to provide the user more control over the mutation process. It will not affect the generated Gaussian noise, only the selection of the genes to mutate.

Complexity: `O(n)`, where `n` is the number of genes in the chromosome.

### `UniformMutation` Class

This operator replaces some of the genes of the chromosome with a random value, with some probability. It assumes `TGene` is of type `int`.

The constructor accepts parameters to control the the behavior of the mutation:
- `mutationProbability`: The probability of a chromosome undergoing mutation in the first place.
- `minValue`: The minimum value that a mutated gene can take.
- `maxValue`: The maximum value that a mutated gene can take.
- `indpb`: The probability of a gene undergoing mutation.
- `random`: An optional `System.Random` object to provide the user more control over the mutation process.

Internally, it iterates over the genes of the chromosome, and with probability `indpb`, it replaces the gene with a random value between `minValue` and `maxValue`.

Complexity: `O(n)`, where `n` is the number of genes in the chromosome.

### `RotateToRightMutation` Class, `InversionMutation` Class, `SwapMutation` Class

Specialized mutation operators for permutation chromosomes. They rotate the genes to the right, invert a subset of the genes, and swap two genes, respectively. See `Mutations/PermutationMutations.cs` for more details.

## `IEvolutionaryAlgorithm` Interface

A very generic interface that represents an evolutionary algorithm.
It dictates that the algorithm must call the `void Evolve(IPopulation<TGene, TCollection, TFitness> population)` method to evolve the population `GenerationsNumber` times, and updates the field `BestChromosome` with the best chromosome found during the evolution.
The goal of the algorithm is to orchestrate the selection, crossover, and mutation processes to evolve the population towards the optimal solution.

## EAMuPlusLambda Class, EAMuCommaLambda Class

These classes implement the `IEvolutionaryAlgorithm` interface, and represent the `(mu + lambda)` and `(mu, lambda)` evolutionary algorithms, respectively, i.e. generate `lambda` offsprings, and select `mu` best out of both parents and offsprings in the first case, or only offsprings in the second. They evolve the population `GenerationsNumber` times, and update the field `BestChromosome` with the best chromosome found during the evolution.

They have additional optional stopping criteria, such as a threshold fitness value, or a max time limit on the evolution process.

The construcors of these classes take an instance of `ISelection`, `ICrossover`, and `IMutation` classes, i.e. the operators that will be used in the evolution process.




