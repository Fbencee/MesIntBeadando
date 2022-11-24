using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesIntBeadando
{
    class Individual
    {
        public List<City> Sequence { get; set; }
        public long Fitness { get; set; }

        public Individual(List<City> sequence)
        {
            this.Sequence = sequence;
            CalculateFitness();
        }

        public Individual(Individual individual)
        {
            this.Sequence = individual.Sequence;
            this.Fitness = individual.Fitness;
        }

        /// <summary>
        /// Calculates the fitness of the individual
        /// </summary>
        public void CalculateFitness()
        {
            this.Fitness = City.Depot.CalculateDistance(Sequence[0]) + City.Depot.CalculateDistance(Sequence[^1]);

            for (int i = 0; i < Sequence.Count - 1; i++)
            {
                this.Fitness += Sequence[i].CalculateDistance(Sequence[i + 1]);
            }
        }

        /// <summary>
        /// This method randomly exchange two member of the sequence
        /// </summary>
        /// <param name="random"></param>
        /// <param name="mutationRate">The chance of the mutatation</param>
        public void Mutation(Random random, double mutationRate)
        {
            for (int i = 0; i < Sequence.Count; i++)
            {
                if (random.NextDouble() > mutationRate)
                {
                    continue;
                }

                int randomNum1 = random.Next(Sequence.Count);
                int randomNum2 = random.Next(Sequence.Count);

                if (randomNum1 == randomNum2)
                {
                    continue;
                }

                City temp = Sequence[randomNum1];
                Sequence[randomNum1] = Sequence[randomNum2];
                Sequence[randomNum2] = temp;
            }
        }

        /// <summary>
        /// This method recombinates two individuals to a new one
        /// </summary>
        /// <param name="individual1">first parent</param>
        /// <param name="individual2">second parent</param>
        /// <param name="random"></param>
        /// <returns>A new individual</returns>
        public static Individual CrossOver(Individual individual1, Individual individual2, Random random)
        {
            int splitPos = random.Next(0, individual1.Sequence.Count);
            List<City> newSequence = SplitTwoParts(individual1.Sequence, splitPos);

            for (int i = 0; i < individual2.Sequence.Count; i++)
            {
                if (newSequence.Contains(individual2.Sequence[i]) && individual2.Sequence[i] != City.Depot)
                {
                    continue;
                }
                newSequence.Add(individual2.Sequence[i]);
            }

            int diff = newSequence.Count - individual1.Sequence.Count;

            if (diff != 0)
            {
                List<int> depotPositions = FindDepots(newSequence);
                depotPositions = depotPositions.OrderBy(p => random.Next(0, depotPositions.Count)).Take(diff).OrderByDescending(p => p).ToList();

                foreach (int pos in depotPositions)
                {
                    newSequence.RemoveAt(pos);
                }
            }

            return new(newSequence);
        }

        /// <summary>
        /// This method finds the depot positions in the given list
        /// </summary>
        /// <param name="sequence">The list you want to search in</param>
        /// <returns>A list of depot postition indexes</returns>
        public static List<int> FindDepots(List<City> sequence)
        {
            List<int> depotPos = new();

            for (int i = 0; i < sequence.Count; i++)
            {
                if (sequence[i] == City.Depot)
                {
                    depotPos.Add(i);
                }
            }

            return depotPos;
        }

        /// <summary>
        /// This method splits a list to two parts at a specified position
        /// </summary>
        /// <param name="sequence">The list we want to split</param>
        /// <param name="splitLength">The position where we want to split</param>
        /// <returns>The first part of the specified list</returns>
        public static List<City> SplitTwoParts(List<City> sequence, int splitPos)
        {
            List<City> p1 = new();

            for (int i = 0; i < sequence.Count; i++)
            {
                if (i < splitPos)
                {
                    p1.Add(sequence[i]);
                }
            }

            return p1;
        }

        /// <summary>
        /// This method checks wether the list contains all the cities or not
        /// </summary>
        /// <param name="sequence">The list we want to check</param>
        /// <returns>bool</returns>
        public static bool IsContainsAllTheCities(List<City> sequence)
        {
            foreach (City city in City.All)
            {
                if (!sequence.Contains(city))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
