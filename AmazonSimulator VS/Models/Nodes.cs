using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public abstract class Nodes
    {
        public Placeholder placeholder;

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
            placeholder = new Placeholder(x, y, z, 0, 0, 0);

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

        public Shelf GetShelf()
        {
            if (shelves[0] != null)
                return shelves[0];

            else
                return shelves[1];    
        }

        public void SetShelf(Shelf shelf)
        {
            shelves.Add(shelf);
        }

        public void AddShelfRange(List<Shelf> shelves)
        {
            this.shelves.AddRange(shelves);
        }

        public bool HasShelf()
        {
            return (shelves.Count > 0);
        }
    }
}
