# EvoNETic: a .NET library for evolutionary algorithms

EvoNETic is a .NET library for evolutionary algorithms. It is designed to be easy to use and flexible, allowing you to quickly implement and test your own evolutionary algorithms.


## Features

- **Flexible** - EvoNETic is designed to be flexible, a swiss army knife for evolutionary algorithms. It doesn't requre specific types of individuals, population, fitness functions, etc. It only requires to implement a few interfaces and you are ready to go.

- **Great toolbox** - the library provides a few ready-to-use components, such as selection methods, crossover operators, mutation operators, etc.


## Developer insights

EvoNETic is composed of a few building bloks, each of them being an interface that you need to implement in order to use the library. Some out-of-the-box implementations are provided, but you can easily create your own. The main building blocks are:

- **`IChromosome`** - represents an individual in the population. It is templated with 3 types:
    - `TGene` - the type of the gene
    - `TCollection` - the type of the collection of genes
    - `TFitness` - the type of the fitness value

- **`IPopulation`** - represents a collection of chromosomes.

- **`IFitnessFunc`** - a class that evaluates the fitness of a chromosome.

- **`ISelection`** - selects chromosomes from the population to perform crossover. Implementations include:
    - Roulette selection
    - Tournament selection

- **`ICrossover`** - performs crossover between two chromosomes, in order to create a new one.
Implementations include:
    - One-point crossover
    - Two-point crossover
    - Uniform crossover (for genes that are floats)
    - Order crossover (for chromosomes that represent permutations)

- **`IMutation`** - mutates a chromosome randomly. Implementations include:
    - Integer mutation
    - Continuous mutation
    - A bunch of permutation mutations (swap, rotate to right, inversion)

- **`IEvolutionaryAlgorithm`** - the main class that orchestrates the evolution process. It requires all the above interfaces to be implemented in order to work.
Implementations include:
    - $\mu + \lambda$ evolutionary algorithm
    - $\mu , \lambda$ evolutionary algorithm


## Example

An example solving the Travelling Salesman Problem (TSP) is provided inside `examples/TSP`.
