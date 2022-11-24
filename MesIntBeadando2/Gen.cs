using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesIntBeadando
{
    class Gen
    {
        public List<Individual> Individuals { get; set; }
        public Gen PreviousGen { get; set; }
        public double MutationRate { get; set; }
        public int N { get; set; }
        public int K { get; set; }
        public Random Rnd { get; set; }
        public Individual BestIndividual { get; set; }

        public Gen(double mutationRate, int n, int k, Random rnd)
        {
            this.N = n;
            this.K = k;
            this.Rnd = rnd;
            this.MutationRate = mutationRate;
            InitIndividuals();
            CalculateBestIndividual();
        }

        public Gen(Gen gen)
        {

            this.N = gen.N;
            this.K = gen.K;
            this.Rnd = gen.Rnd;
            this.MutationRate = gen.MutationRate;
            this.PreviousGen = gen;
            this.Individuals = GenerateNewIndividuals();
            CalculateBestIndividual();
        }

        public void InitIndividuals()
        {
            Individuals = new();

            for (int i = 0; i < N; i++)
            {
                List<City> s = City.All.ToList();

                for (int j = 0; j < K - 1; j++)
                {
                    s.Add(City.Depot);
                }

                s = s.OrderBy(c => Rnd.Next()).ToList();
                Individual current = new(s);
                Individuals.Add(current);


            }
        }

        public void CalculateBestIndividual()
        {
            foreach (Individual individual in this.Individuals)
            {
                if (BestIndividual is null || BestIndividual.Fitness > individual.Fitness)
                {
                    BestIndividual = individual;
                }
            }
        }

        public void MutateAll()
        {
            foreach (Individual individual in this.PreviousGen.Individuals)
            {
                individual.Mutation(Rnd, MutationRate);
            }
        }

        public List<Individual> GenerateNewIndividuals()
        {
            List<Individual> newIndividuals = new();
            MutateAll();
            List<Individual> matingPool = CreateMatingPool();

            for (int i = 0; i < N; i++)
            {
                
                int rand1 = Rnd.Next(0, matingPool.Count);
                int rand2 = Rnd.Next(0, matingPool.Count);
                Individual newInd = Individual.CrossOver(matingPool[rand1], matingPool[rand2], Rnd);
                newIndividuals.Add(newInd);
            }

            return newIndividuals;
        }

        public List<Individual> CreateMatingPool()
        {
            List<Individual> matingPool = new();
            long sum = this.PreviousGen.Individuals.Select(f => f.Fitness).Sum();
            foreach (Individual individual in this.PreviousGen.Individuals)
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
