using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesIntBeadando
{
    class Problem
    {

        public List<City> All { get; set; }
        public City Depot { get; set; }

        public Problem(Random random, int lowerBound, int upperBound)
        {
            All = new();
            Depot = new City("Depot", random.Next(lowerBound, upperBound), random.Next(lowerBound, upperBound));
        }


        /// <summary>
        /// This method initializes all the cities
        /// </summary>
        /// <param name="random"></param>
        /// <param name="M">The number of the cities</param>
        /// <param name="lowerBound">The lowerbound of the possible point coordinates</param>
        /// <param name="upperBound">The upperbound of the possible point coordinates</param>
        public void Init(Random random, int M, int lowerBound, int upperBound)
        {
            for (int i = 0; i < M; i++)
            {
                int x = random.Next(lowerBound, upperBound);
                int y = random.Next(lowerBound, upperBound);

                All.Add(new City($"City_{i + 1}", x, y));
            }
        }

    }
}
