using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Ship : Moveable, IUpdatable
    {
        int lasttick;
        public Ship(double x, double y, double z, double rotationX, double rotationY, double rotationZ)
        {
            this.type = "ship";
            this.guid = Guid.NewGuid();

            this._x = x;
            this._y = y;
            this._z = z;

            this._rX = rotationX;
            this._rY = rotationY;
            this._rZ = rotationZ;
        }

        public override bool Update(int tick, int tickCount)
        {
            if (!this.IsPaused())
            {
                _x += 0.125;
                this.Move((_x), 0, 0);
            }

            _rZ = Math.Cos(Convert.ToDouble(tickCount) / 5) / 64;
            _rX = Math.Sin(Convert.ToDouble(tickCount) / 5) / 64;

            if (this.IsPaused() && (tickCount - lasttick == 100))
            {
                this.Pause(false);
            }
            else if (!this.IsPaused() && _x == 15)
            {
                this.Pause(true);
                lasttick = tickCount;
            }
            else if (_x == 30)
            {
                _x = 0;
            }

            return true;
        }

    }
}
