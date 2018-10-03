using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Shelf : Moveable, IUpdatable
    {
        private List<string> items = new List<string>();
        private char c;
        public double defx, defz;

        public Shelf(double x, double y, double z, double rotationX, double rotationY, double rotationZ)
        {
            type = "shelf";
            guid = Guid.NewGuid();

            _x = x;
            _y = y;
            _z = z;

            _rX = rotationX;
            _rY = rotationY;
            _rZ = rotationZ;

            defx = x;
            defz = z;
        }

        public void SetNode(char c)
        {
            this.c = c;
        }

        public char GetNode()
        {
            return c;
        }       
    }
}
