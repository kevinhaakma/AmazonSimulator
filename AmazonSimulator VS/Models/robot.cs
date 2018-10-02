using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Robot : Moveable, IUpdatable
    {
        List<Node> PathToDestNodes = new List<Node>();

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
            if (PathToDestNodes.Count > 0)
            {
                if (x < PathToDestNodes[0].x)
                {
                    //move right
                    _x += 0.125;
                }

                else if (x > PathToDestNodes[0].x)
                {
                    //move left
                    _x -= 0.125;
                }

                else if (z < PathToDestNodes[0].z)
                {
                    //move up
                    _z += 0.125;
                }

                else if (z > PathToDestNodes[0].z)
                {
                    //move down
                    _z -= 0.125;
                }

                if(x == PathToDestNodes[0].x && z == PathToDestNodes[0].z)
                {
                    PathToDestNodes.RemoveAt(0);
                }
            }


            //Robot status updates
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

        public void MoveTo(char Char)
        {
            Node ClosestNode = null;
            char CurrentPos = 'A';
            foreach (Node node in World.GetNodes())
            {
                if(ClosestNode != null)
                {
                    if ((_x - node.x) < (ClosestNode.x - node.x) || (_z - node.z) < (ClosestNode.z - node.z))
                    {
                        ClosestNode = node;
                    }
                }

                else
                {
                    ClosestNode = node;
                }
            }

            ClosestNode.c = CurrentPos;

            List<char> PathToDestChar = new List<char>();

            World.pathfinder.shortest_path(CurrentPos, Char).ForEach(x => PathToDestChar.Add(x));

            foreach (char c in PathToDestChar)
            {
                foreach (Node node in World.GetNodes())
                {
                    if(node.c == c)
                    {
                        PathToDestNodes.Add(node);
                    }
                }
            }
            PathToDestNodes.Reverse();
            needsUpdate = true;
        }
    }
}
