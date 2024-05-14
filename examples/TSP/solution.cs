using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using EvoNETic;

class Fitness : IFitnessFunc<int, List<int>, double>
{
    public double[,] _distances;
    public List<List<double>> _coordinates;
    public int CitiesCount => _coordinates.Count;
    public double OptimalDistance { get; private set; }

    public Fitness()
    {
        _coordinates = new List<List<double>>();
        using (StreamReader sr = new StreamReader("./examples/TSP/problem.txt"))
        {
            while (sr.ReadLine() is string line)
            {
                var parsed = line.Split(' ');
                double x = double.Parse(parsed[1]);
                double y = double.Parse(parsed[2]);
                _coordinates.Add(new List<double> { x, y });
            }
            _distances = new double[_coordinates.Count, _coordinates.Count];

            for (int i = 0; i < _coordinates.Count; i++)
            {
                for (int j = 0; j < _coordinates.Count; j++)
                {
                    double x1 = _coordinates[i][0];
                    double y1 = _coordinates[i][1];
                    double x2 = _coordinates[j][0];
                    double y2 = _coordinates[j][1];
                    _distances[i, j] = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
                }
            }
        }

        using (StreamReader sr = new StreamReader("./examples/TSP/optimal.txt"))
        {
            List<int> optimalTour = new List<int>();
            while (sr.ReadLine() is string line)
            {
                optimalTour.Add(int.Parse(line));
            }

            for (int i = 1; i < optimalTour.Count; ++i)
            {
                OptimalDistance += _distances[optimalTour[i] - 1, optimalTour[i - 1] - 1];
            }
            OptimalDistance += _distances[optimalTour[0] - 1, optimalTour.Last() - 1];
        }
    }

    public double Evaluate(IChromosome<int, List<int>, double> chromosome)
    {
        double distance = 0;
        List<int> permutation = chromosome.ToList();

        for (int i = 1; i < permutation.Count; ++i)
        {
            distance += _distances[permutation[i], permutation[i - 1]];
        }

        distance += _distances[permutation[0], permutation.Last()];

        return 1 / distance;
    }
}


class Program
{
    static void Main(string[] args)
    {
        var fitness = new Fitness();
        var population_list = new List<IChromosome<int, List<int>, double>>(200);
        for (int i = 0; i < 200; ++i)
        {
            var permutation = new List<int>(fitness.CitiesCount);
            for (int j = 0; j < fitness.CitiesCount; ++j)
            {
                permutation.Add(j);
            }
            Shuffle(permutation);
            population_list.Add(new Chromosome<int, List<int>, double>(permutation));
        }

        var crossover = new OrderCrossover<int, List<int>, double>();
        var mutation = new RotateToRightMutation<int, List<int>, double>(1.0);
        var selection = new TournamentSelection<int, List<int>, double>(4);
        var algorithm = new EAMuPlusLambda<int, List<int>, double>(200, 100, crossover, mutation, selection, fitness, 5000);

        var population = new Population<int, List<int>, double>(population_list);

        algorithm.Evolve(population);

        var best = algorithm.BestChromosome;

        foreach (int city in best)
        {
            Console.Write($"{1 + city} ");
        }

        Console.WriteLine();

        Console.WriteLine($"Best fitness: {1 / best.Fitness}");
        Console.WriteLine($"Optimal fitness: {fitness.OptimalDistance}");
    }

    /// <summary>
    /// Knuth shuffle
    /// </summary>        
    static void Shuffle(List<int> array)
    {
        Random random = new Random();
        int n = array.Count();
        while (n > 1)
        {
            n--;
            int i = random.Next(n + 1);
            int temp = array[i];
            array[i] = array[n];
            array[n] = temp;
        }

    }
}


