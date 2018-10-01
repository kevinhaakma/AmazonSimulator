using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Controllers;

namespace Models {
    public class World : IObservable<Command>, IUpdatable
    {
        private List<ShelfNode> shelfNodes = new List<ShelfNode>();
        private List<PlaneNode> planeNodes = new List<PlaneNode>();
        private List<Moveable> worldObjects = new List<Moveable>();
        public List<Robot> robots = new List<Robot>();
        public List<Shelf> shelfs = new List<Shelf>();
        private List<IObserver<Command>> observers = new List<IObserver<Command>>();
        public Ship ship;
        
        public World() {
            ship = CreateShip(10, 0, 13);

            for (int x = 5; x < 26; x += 5)
            {
                robots.Add(CreateRobot(x, 0, 5));
            }

            int i = 0;
            for (double z = 10; z < 28; z += 2.625)
            {
                i++;
                {
                    for (double x = 6; x <= 25; x += (25 / 4))
                    {
                        shelfs.Add(CreateShelf(x, 0, z));
                        if (i % 2 != 0)
                            shelfNodes.Add(new ShelfNode(x, 0, z + 1.25));
                    }
                }
            }

            planeNodes.Add(new PlaneNode(15, 0, 0.75));
            for (double z = 0.75; z < 28; z += 5.25)
            {
                if (z != 6)
                {
                    for (double x = 3.25; x <= 27.5; x += 24)
                    {
                        planeNodes.Add(new PlaneNode(x, 0, z));
                    }
                }
            }

            foreach (ShelfNode node in shelfNodes)
                worldObjects.Add(node.placeholder);

            foreach (PlaneNode node in planeNodes)
                worldObjects.Add(node.placeholder);
        }

        private Robot CreateRobot(double x, double y, double z) {
            Robot robot = new Robot(x,y,z,0,0,0);
            worldObjects.Add(robot);
            return robot;
        }

        private Ship CreateShip(double x, double y, double z)
        {
            Ship ship = new Ship(x, y, z, 0, 0, 0);
            worldObjects.Add(ship);
            return ship;
        }

        private Shelf CreateShelf(double x, double y, double z)
        {
            Shelf shelf = new Shelf(x, y, z, 0, 0, 0);
            worldObjects.Add(shelf);
            return shelf;
        }

        public IDisposable Subscribe(IObserver<Command> observer)
        {
            if (!observers.Contains(observer)) {
                observers.Add(observer);

                SendCreationCommandsToObserver(observer);
            }
            return new Unsubscriber<Command>(observers, observer);
        }

        private void SendCommandToObservers(Command c) {
            for(int i = 0; i < this.observers.Count; i++) {
                this.observers[i].OnNext(c);
            }
        }

        private void SendCreationCommandsToObserver(IObserver<Command> obs) {
            foreach(Moveable m3d in worldObjects) {
                obs.OnNext(new UpdateModel3DCommand(m3d));
            }
        }

        private void moveRobot()
        {
            for (int i = 0; i < 5; i++)
            {
                if (!robots[i].HasReachedPlane())
                {
                    robots[i].MoveTo(planeNodes[i + 5]);
                }

                else if (robots[i].HasReachedPlane() && !robots[i].HasReachedShelf())
                {
                    robots[i].MoveTo(shelfNodes[i + 5]);
                }
            }
        }

        public bool Update(int tick, int tickCount)
        {
            moveRobot();
            for(int i = 0; i < worldObjects.Count; i++) {
                Moveable u = worldObjects[i];

                if(u is IUpdatable) {
                    bool needsCommand = ((IUpdatable)u).Update(tick, tickCount);

                    if(needsCommand) {
                        SendCommandToObservers(new UpdateModel3DCommand(u));
                    }
                }
            }

            return true;
        }
    }

    internal class Unsubscriber<Command> : IDisposable
    {
        private List<IObserver<Command>> _observers;
        private IObserver<Command> _observer;

        internal Unsubscriber(List<IObserver<Command>> observers, IObserver<Command> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose() 
        {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}