using System;
using System.Collections.Generic;
using System.IO;


class Program {
    static void Main(string[] args) {
        // test Chromosome and GaussianMutation
        var chromosome = new Chromosome<double, List<double>>(new List<double> { 1, 2, 3, 4, 5 });
        var mutation = new GaussianMutation<List<double>>(0, 1, 0.5);

        Console.WriteLine(chromosome);

        for (int i = 0; i < 10; i++) {
            chromosome = (Chromosome<double, List<double>>)mutation.Mutate(chromosome, 1);
            Console.WriteLine(chromosome);
        }
    }
}