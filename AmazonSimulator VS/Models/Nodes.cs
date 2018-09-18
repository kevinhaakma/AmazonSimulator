using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Nodes
    {
        public double x;
        public double y;
        public double z;

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
    }
}
