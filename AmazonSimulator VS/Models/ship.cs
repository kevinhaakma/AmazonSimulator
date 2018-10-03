using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Ship : Moveable, IUpdatable
    {

        public Ship(double x, double y, double z, double rotationX, double rotationY, double rotationZ)
        {
            type = "ship";
            guid = Guid.NewGuid();

            _x = x;
            _y = y;
            _z = z;

            _rX = rotationX;
            _rY = rotationY;
            _rZ = rotationZ;
        }

        public override bool Update(int tick, int tickCount)
        {
            if (!IsPaused())
            {
                _x += 0.125;
                Move((_x), 0, 0);
            }
                     
            if (!IsPaused() && _x == 15)
            {
                Pause(true);
            }
            else if (_x == 30)
            {
                _x = 0;
            }

            _rZ = Math.Cos(Convert.ToDouble(tickCount) / 5) / 64;
            _rX = Math.Sin(Convert.ToDouble(tickCount) / 5) / 64;

            return true;
        }
    }
}
