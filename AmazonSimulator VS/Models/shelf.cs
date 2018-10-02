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

            this._x = x;
            this._y = y;
            this._z = z;

            this._rX = rotationX;
            this._rY = rotationY;
            this._rZ = rotationZ;

            defx = x;
            defz = z;
        }

        public void SetNode(char c)
        {
            this.c = c;
        }


        public char GetNode()
        {
            return this.c;
        }

        public bool ContainsItems()
        {
            return (items.Count > 0);
        }

        public List<string> GetItems()
        {
            return items;
        }

        public List<string> MoveItems()
        {
            List<string> tmp = items;
            items = new List<string>();
            return items;
        }
    }
}
