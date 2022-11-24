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
            int M = 500;//number of cities (10 20  50 100 200 500)
            int K = 10;//number of  (1 2 4 5 10 20)
            double mutationRate = 0.1;//

            Random random = new();
            City.Init(random, M, 1, 101);
            Gen gen = new(mutationRate, N, K, random);
            Console.WriteLine($"Start Gen Best Fitness:{gen.BestIndividual.Fitness}");
            Gen prevGen = new(gen);
            Gen newGen=new(gen);
            Individual bestOne = new(gen.BestIndividual);

            for (int i = 1; i < G; i++)
            {
                if (i != 1)
                {
                    prevGen = newGen;
                }

                newGen = new(prevGen);
                Console.WriteLine($"Gen{i} Current Gen Best Fitness:{newGen.BestIndividual.Fitness}");

                if (bestOne.Fitness > newGen.BestIndividual.Fitness)
                {
                    bestOne = newGen.BestIndividual;
                }
            }

            Console.WriteLine($"The best solution's fitness:{bestOne.Fitness}");
            ;
        }
    }
}
