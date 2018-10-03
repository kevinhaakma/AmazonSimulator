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
        private char CurrentPos = 'α';

        public Robot(double x, double y, double z, double rotationX, double rotationY, double rotationZ)
        {
            type = "robot";
            status = "idle";

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
            if (tasks.Count == 0 && !moving)
            {
                status = "idle";
            }
            else if (tasks.Count > 0 && !moving){
                if (tasks[0] is MoveTask)
                {
                    MoveTask Task = tasks[0] as MoveTask;
                    MoveTo(Task.getinfo());
                    CurrentPos = Task.getinfo();
                    if(Task.getinfo() == 'α')
                        status = "Omw to dock";
                    else
                        status = "Omw to " + Task.getinfo();
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
                    shelf = Task.getinfo();
                    shelf.Move(_x,0.3,_z);
                    tasks.RemoveAt(0);
                }
            }

            if (PathToDestNodes.Count > 0 && moving)
            {
                if (_x < PathToDestNodes[0].x)
                {
                    //move right
                    _rY = Math.PI/2;
                    _x += 0.125;
                }

                else if (_x > PathToDestNodes[0].x)
                {
                    //move left
                    _rY = -Math.PI/2;
                    _x -= 0.125;
                }

                else if (_z < PathToDestNodes[0].z)
                {
                    //move up
                    _rY = 0;
                    _z += 0.125;
                }

                else if (_z > PathToDestNodes[0].z)
                {
                    //move down
                    _rY = Math.PI;
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
            List<char> PathToDestChar = new List<char>();

            World.pathfinder.shortest_path(CurrentPos, Char).ForEach(x => PathToDestChar.Add(x));
            PathToDestChar.Reverse();

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

            needsUpdate = true;
        }

        public void addtask(Tasks task)
        {
            tasks.Add(task);
        }
    }
}
