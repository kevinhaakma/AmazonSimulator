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
        public Node(double x, double y, double z)
        {
            placeholder = new Placeholder(x, y, z, 0, 0, 0);

            this.x = x;
            this.y = y;
            this.z = z;
        }

        private Shelf shelf;

        public Shelf GetShelf()
        {
            Shelf tmp = shelf;
            shelf = null;
            return tmp;
        }

        public void SetName(char c)
        {
            this.c = c;
        }

        public void SetShelf(Shelf shelf)
        {
            this.shelf = shelf;
            this.shelf.Move(x, y, z);
        }

        public bool HasShelf()
        {
            return (shelf != null);
        }
    }
}
