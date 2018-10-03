using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public abstract class Nodes
    {
        public double x;
        public double y;
        public double z;

        public char c;
    }

    public class Node : Nodes
    {
        private List<Shelf> shelves = new List<Shelf>();

        public Node(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void SetName(char c)
        {
            this.c = c;
        }

        public List<Shelf> GetAllShelves()
        {
            return shelves;
        }

        public void AddShelfRange(List<Shelf> shelves)
        {
            this.shelves.AddRange(shelves);
        }
    }
}
