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
        public double MutationRate { get; set; }
        public int N { get; set; }
        public int K { get; set; }
        public Random Rnd { get; set; }
        public Individual BestIndividual { get; set; }

        private Gen(double mutationRate, int n, int k, Random rnd)
        {
            this.MutationRate = mutationRate;
            this.N = n;
            this.K = k;
            this.Rnd = rnd;
        }

        public Gen(double mutationRate, int n, int k, Random rnd, Problem vrp)
            : this(mutationRate, n, k, rnd)
        {
            InitIndividuals(vrp);
            CalculateBestIndividual();
        }

        public Gen(double mutationRate, int n, int k, Random rnd, List<Individual> individuals)
            : this(mutationRate, n, k, rnd)
        {
            this.Individuals = individuals;
            CalculateBestIndividual();
        }

        /// <summary>
        /// This method initializes all the individuals in the generation
        /// </summary>
        public void InitIndividuals(Problem vrp)
        {
            Individuals = new();

            for (int i = 0; i < N; i++)
            {
                List<City> s = vrp.All.ToList();

                for (int j = 0; j < K - 1; j++)
                {
                    s.Add(vrp.Depot);
                }

                s = s.OrderBy(c => Rnd.Next()).ToList();
                Individual current = new(s, vrp);
                Individuals.Add(current);


            }
        }

        /// <summary>
        /// This method calculates the best individual in the generation
        /// </summary>
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

        /// <summary>
        /// This method mutate all the individuals
        /// </summary>
        public void MutateAll()
        {
            foreach (Individual individual in this.Individuals)
            {
                individual.Mutation(Rnd, MutationRate);
            }
        }


    }
}
