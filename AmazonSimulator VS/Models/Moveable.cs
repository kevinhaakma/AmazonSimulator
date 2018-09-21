using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;

namespace Models {
    public abstract class Moveable : IUpdatable {
        public double _x = 0;
        public double _y = 0;
        public double _z = 0;
        public double _rX = 0;
        public double _rY = 0;
        public double _rZ = 0;

        public string status { get; set; }
        public string type { get; set; }
        public Guid guid { get; set; }
        public double x { get { return _x; } }
        public double y { get { return _y; } }
        public double z { get { return _z; } }
        public double rotationX { get { return _rX; } }
        public double rotationY { get { return _rY; } }
        public double rotationZ { get { return _rZ; } }

        private bool _Pause = false, HasReachedPlaneNode = false, HasReachedShelfNode = false;
        public bool needsUpdate = true;

        public virtual void Move(double x, double y, double z) {
            if (!_Pause)
            {
                this._x = x;
                this._y = y;
                this._z = z;

                needsUpdate = true;
            }
        }

        public void MoveTo(Nodes node)
        {
            if (x < node.x)
            {
                //move right
                _x += 0.125;
            }

            else if (x > node.x)
            {
                //move left
                _x -= 0.125;
            }

            else if (z < node.z)
            {
                //move up
                _z += 0.125;
            }

            else if (z > node.z)
            {
                //move down
                _z -= 0.125;
            }

            else if (x == node.x && z == node.z && node.GetType() == typeof(PlaneNode))
            {
                HasReachedPlaneNode = true;
            }

            else if (x == node.x && z == node.z && node.GetType() == typeof(ShelfNode))
            {
                HasReachedShelfNode = true;
            }
            needsUpdate = true;
        }

        public void ReachedPlane(bool _bool)
        {
            HasReachedPlaneNode = _bool;
        }

        public void ReachedShelfNode(bool _bool)
        {
            HasReachedShelfNode = _bool;
        }

        public bool HasReachedPlane()
        {
            return HasReachedPlaneNode;
        }

        public bool HasReachedShelf()
        {
            return HasReachedShelfNode;
        }

        public bool IsPaused()
        {
            return _Pause;
        }

        public void Pause(bool _Pause)
        {
            this._Pause = _Pause;
        }

        public virtual void Rotate(double rotationX, double rotationY, double rotationZ) {
            this._rX = rotationX;
            this._rY = rotationY;
            this._rZ = rotationZ;

            needsUpdate = true;
        }

        public virtual bool Update(int tick)
        {
            if(needsUpdate) {
                needsUpdate = false;
                return true;
            }
            return false;
        }
    }

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
    }

    public class Ship : Moveable, IUpdatable
    {
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
    }

    public class Placeholder : Moveable, IUpdatable
    {
        public Placeholder(double x, double y, double z, double rotationX, double rotationY, double rotationZ)
        {
            this.type = "placeholder";
            this.guid = Guid.NewGuid();

            this._x = x;
            this._y = y;
            this._z = z;

            this._rX = rotationX;
            this._rY = rotationY;
            this._rZ = rotationZ;
        }
    }

    public class Shelf : Moveable, IUpdatable
    {
        private List<string> items = new List<string>();

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