using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Robot : Moveable, IUpdatable
    {
        public Robot(double x, double y, double z, double rotationX, double rotationY, double rotationZ)
        {
            this.type = "robot";
            this.status = "idle";

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
            if (!this.HasReachedPlane())
            {
                this.status = "Omw to PlaneNode";
            }

            if (!this.HasReachedShelf())
            {
                this.status = "Omw to Shelf";
            }
            else
            {
                this.status = "idle";
            }

            return true;
        }
    }
}
