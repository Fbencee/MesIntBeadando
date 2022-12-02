using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MesIntBeadando
{
    class Program
    {
        static void Main(string[] args)
        {
            int G = 100;//number of gens 
            int N = 100;//size of gen
            int M = 20;//number of cities (10 20  50 100 200 500)
            int K = 4;//number of courier (1 2 4 5 10 20)
            double mutationRate = 0.1;//

            Random random = new();
            Problem vrp = new(random,1,101);
            vrp.Init(random, M, 1, 101);
            Gen gen = new(mutationRate, N, K, random,vrp);
            Gen newGen = gen;
            Console.WriteLine($"Start Gen Best Fitness:{gen.BestIndividual.Fitness}");
            Individual bestOne = new(gen.BestIndividual);

            for (int i = 1; i < G; i++)
            {
                List<Individual> newIndividuals = GenerateNewIndividuals(newGen);
                newGen = new(mutationRate, N, K, random, newIndividuals);
                Console.WriteLine($"Gen{i} Current Gen Best Fitness:{newGen.BestIndividual.Fitness}");

                if (bestOne.Fitness > newGen.BestIndividual.Fitness)
                {
                    bestOne = newGen.BestIndividual;
                }
            }

            Console.WriteLine($"The best solution's fitness:{bestOne.Fitness}");
        }

        /// <summary>
        /// This method generates a new individual list for the new generation
        /// </summary>
        /// <returns>A new sequence</returns>
        public static List<Individual> GenerateNewIndividuals(Gen gen)
        {
            List<Individual> newIndividuals = new();
            gen.MutateAll();
            List<Individual> matingPool = CreateMatingPool(gen);

            for (int i = 0; i < gen.N; i++)
            {
                int rand1 = gen.Rnd.Next(0, matingPool.Count);
                int rand2 = gen.Rnd.Next(0, matingPool.Count);
                Individual newInd = Individual.CrossOver(matingPool[rand1], matingPool[rand2], gen.Rnd);
                newIndividuals.Add(newInd);
            }

            return newIndividuals;
        }

        /// <summary>
        /// This method creates a mating pool
        /// </summary>
        /// <returns>Mating pool</returns>
        public static List<Individual> CreateMatingPool(Gen gen)
        {
            List<Individual> matingPool = new();
            long sum = gen.Individuals.Select(f => f.Fitness).Sum();//the sum of fitnesses
            foreach (Individual individual in gen.Individuals)
            {
                double ratio = (double)individual.Fitness / sum;
                int amount = (int)(1000 * (1 - ratio));

                for (int i = 0; i < amount; i++)
                {
                    matingPool.Add(individual);
                }
            }

            return matingPool;
        }
    }
}
