using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Tasks
    {

    }

    public class MoveTask : Tasks
    {
        private char node;

        public MoveTask(char node)
        {
            this.node = node;
        }

        public char getinfo()
        {
            return node;
        }
    }

    public class DropShelf : Tasks
    {
        private List<double> coords;

        public DropShelf(double x, double z)
        {
            coords = new List<double>() { x, z };
        }

        public List<double> getinfo()
        {
            return coords;
        }
    }

    public class PickupShelf : Tasks
    {
        private Shelf shelf;

        public PickupShelf(Shelf shelf)
        {
            this.shelf = shelf;
        }

        public Shelf getinfo()
        {
            return shelf;
        }
    }
}
