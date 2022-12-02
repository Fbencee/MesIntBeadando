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
    }
}
