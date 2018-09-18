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
    }

    public class PlaneNode : Nodes
    {
        public PlaneNode(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    public class ShelfNode : Nodes
    {
        public ShelfNode(double x, double y, double z)
        {
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
