using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesIntBeadando
{
    class City
    {
        public static List<City> All { get; set; }
        public static City Depot { get; set; }

        public string Name { get; set; }
        public Point Position { get; set; }

        public City(string name, int x, int y)
        {
            Name = name;
            Position = new Point(x, y);
        }

        /// <summary>
        /// Calculate the distance between the given City and the on where this method called
        /// </summary>
        /// <param name="city"></param>
        /// <returns>Distance of two cities</returns>
        public int CalculateDistance(City city)
        {
            return Math.Abs(Position.X - city.Position.X) + Math.Abs(Position.Y - city.Position.Y);
        }

        /// <summary>
        /// This method intializes all the cities
        /// </summary>
        /// <param name="random"></param>
        /// <param name="M">The number of the cities</param>
        /// <param name="lowerBound">The lowerbound of the possible point coordinates</param>
        /// <param name="upperBound">The upperbound of the possible point coordinates</param>
        public static void Init(Random random, int M, int lowerBound, int upperBound)
        {
            All = new();
            Depot = new City("Depot", random.Next(lowerBound, upperBound), random.Next(lowerBound, upperBound));

            for (int i = 0; i < M; i++)
            {
                int x = random.Next(lowerBound, upperBound);
                int y = random.Next(lowerBound, upperBound);

                All.Add(new City($"City_{i+1}", x, y));
            }
        }

    }
}
