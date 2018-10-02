using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Robot : Moveable, IUpdatable
    {
        private List<Node> PathToDestNodes = new List<Node>();
        private List<Tasks> tasks = new List<Tasks>();
        private Shelf shelf;
        private bool moving = false;

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
            if (tasks.Count == 0 && !moving)
            {
                this.status = "idle";
            }
            else if (tasks.Count > 0 && !moving){
                if (tasks[0] is MoveTask)
                {
                    MoveTask Task = tasks[0] as MoveTask;
                    this.MoveTo(Task.getinfo());
                    if(Task.getinfo() == 'α')
                        this.status = "Omw to dock";
                    else
                        this.status = "Omw to " + Task.getinfo();
                    moving = true;
                }
                else if (tasks[0] is DropShelf)
                {
                    DropShelf Task = tasks[0] as DropShelf;
                    shelf.Move(Task.getinfo()[0], 0, Task.getinfo()[1]);
                    shelf = null;
                    tasks.RemoveAt(0);
                }
                else if (tasks[0] is PickupShelf)
                {
                    PickupShelf Task = tasks[0] as PickupShelf;
                    this.shelf = Task.getinfo();
                    this.shelf.Move(_x,0.3,_z);
                    tasks.RemoveAt(0);
                }
            }

            if (PathToDestNodes.Count > 0 && moving)
            {
                if (_x < PathToDestNodes[0].x)
                {
                    //move right
                    _x += 0.125;
                }

                else if (_x > PathToDestNodes[0].x)
                {
                    //move left
                    _x -= 0.125;
                }

                else if (_z < PathToDestNodes[0].z)
                {
                    //move up
                    _z += 0.125;
                }

                else if (_z > PathToDestNodes[0].z)
                {
                    //move down
                    _z -= 0.125;
                }

                if (shelf != null)
                {
                    shelf.Move(_x, 0.3, _z);
                }

                if (x == PathToDestNodes[0].x && z == PathToDestNodes[0].z)
                {
                    PathToDestNodes.RemoveAt(0);
                    if(PathToDestNodes.Count == 0)
                    {
                        tasks.RemoveAt(0);
                        moving = false;
                    }                        
                }
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

            CurrentPos = ClosestNode.c;

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

        public void addtask(Tasks task)
        {
            tasks.Add(task);
        }
    }
}
