using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Controllers;

namespace Models {
    public class World : IObservable<Command>, IUpdatable
    {
        private List<Moveable> worldObjects = new List<Moveable>();
        private List<IObserver<Command>> observers = new List<IObserver<Command>>();
        private double TruckMove = 0;
        Truck truck;
        
        public World() {
            Robot r = CreateRobot(0,0,0);
            r.Move(4.6, 0, 13);

            truck = CreateTruck(0, 0, 0);
            truck.Move(10, 0, 13);

            Shelf shelf = CreateShelf(0, 0, 0);
            shelf.Move(15, 0, 13);
        }

        private Robot CreateRobot(double x, double y, double z) {
            Robot robot = new Robot(x,y,z,0,0,0);
            worldObjects.Add(robot);
            return robot;
        }

        private Truck CreateTruck(double x, double y, double z)
        {
            Truck truck = new Truck(x, y, z, 0, 0, 0);
            worldObjects.Add(truck);
            return truck;
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

        private void movetruck()
        {
            truck.Move(TruckMove, 0, 0);

            TruckMove += 0.25;
            if (truck.IsPaused() && TruckMove == 30)
            {
                TruckMove = 15.25;
                truck.Pause(false);
            }
            else if (TruckMove == 15)
            {
                truck.Pause(true);
            }
            else if (TruckMove >= 30 && !truck.IsPaused())
            {
                TruckMove = 0;
                truck.Move((TruckMove), 0, 0);
            }
        }

        public bool Update(int tick)
        {
            movetruck();
            for(int i = 0; i < worldObjects.Count; i++) {
                Moveable u = worldObjects[i];

                if(u is IUpdatable) {
                    bool needsCommand = ((IUpdatable)u).Update(tick);

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