using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Controllers;

namespace Models {
    public class World : IObservable<Command>, IUpdatable
    {
        private static List<Node> nodes = new List<Node>();
        private List<Moveable> worldObjects = new List<Moveable>();
        public List<Robot> robots = new List<Robot>();
        public List<Shelf> shelfs = new List<Shelf>();
        private List<IObserver<Command>> observers = new List<IObserver<Command>>();
        public Ship ship;

        public static Pathfinder pathfinder = new Pathfinder();

       // private List<char> Vertex = new List<char>() { 'α', 'A', 'B', 'C','D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', };
        private List<char> Vertex = new List<char>() { 'D', 'E', 'F', 'J', 'K', 'L', 'M', 'G', 'P', 'Q', 'R', 'S', 'V', 'W', 'X', 'Y', 'α', 'A', 'B', 'C',  'H', 'I',  'N', 'O', 'T', 'U',  'Z', };

        public World() {
            //α
            pathfinder.add_vertex('α', new Dictionary<char, int>() { { 'A', 1 }, { 'B', 1 } });               //--------------A------α------B-------------//
            pathfinder.add_vertex('A', new Dictionary<char, int>() { { 'α', 1 }, { 'C', 1 } });               //----/---------------------------------\---//
            pathfinder.add_vertex('B', new Dictionary<char, int>() { { 'α', 1 }, { 'H', 1 } });               //---C------D------E------F------G-------H--//
            pathfinder.add_vertex('C', new Dictionary<char, int>() { { 'D', 1 }, { 'I', 1 } });               //---I------J------K------L------M-------N--//
            pathfinder.add_vertex('D', new Dictionary<char, int>() { { 'C', 1 }, { 'E', 1 } });               //---O------P------Q------R------S-------T--//
            pathfinder.add_vertex('E', new Dictionary<char, int>() { { 'D', 1 }, { 'F', 1 } });               //---U------V------W------X------Y-------Z--//
            pathfinder.add_vertex('F', new Dictionary<char, int>() { { 'E', 1 }, { 'G', 1 } });               
            pathfinder.add_vertex('G', new Dictionary<char, int>() { { 'F', 1 }, { 'H', 1 } });               
            pathfinder.add_vertex('H', new Dictionary<char, int>() { { 'B', 1 }, { 'G', 1 }, { 'N', 1 } });   
            pathfinder.add_vertex('I', new Dictionary<char, int>() { { 'C', 1 }, { 'J', 1 }, { 'O', 1 } });   
            pathfinder.add_vertex('J', new Dictionary<char, int>() { { 'I', 1 }, { 'K', 1 } });               
            pathfinder.add_vertex('K', new Dictionary<char, int>() { { 'J', 1 }, { 'L', 1 } });               
            pathfinder.add_vertex('L', new Dictionary<char, int>() { { 'K', 1 }, { 'M', 1 } });               
            pathfinder.add_vertex('M', new Dictionary<char, int>() { { 'L', 1 }, { 'N', 1 } });               
            pathfinder.add_vertex('N', new Dictionary<char, int>() { { 'M', 1 }, { 'H', 1 }, { 'T', 1 } });   
            pathfinder.add_vertex('O', new Dictionary<char, int>() { { 'I', 1 }, { 'P', 1 }, { 'U', 1 } });   
            pathfinder.add_vertex('P', new Dictionary<char, int>() { { 'O', 1 }, { 'A', 1 } });               
            pathfinder.add_vertex('Q', new Dictionary<char, int>() { { 'P', 1 }, { 'R', 1 } });               
            pathfinder.add_vertex('R', new Dictionary<char, int>() { { 'Q', 1 }, { 'S', 1 } });               
            pathfinder.add_vertex('S', new Dictionary<char, int>() { { 'R', 1 }, { 'T', 1 } });              
            pathfinder.add_vertex('T', new Dictionary<char, int>() { { 'N', 1 }, { 'S', 1 }, { 'Z', 1 } });   
            pathfinder.add_vertex('U', new Dictionary<char, int>() { { 'O', 1 }, { 'V', 1 } });              
            pathfinder.add_vertex('V', new Dictionary<char, int>() { { 'U', 1 }, { 'W', 1 } });              
            pathfinder.add_vertex('W', new Dictionary<char, int>() { { 'V', 1 }, { 'X', 1 } });               
            pathfinder.add_vertex('X', new Dictionary<char, int>() { { 'W', 1 }, { 'Y', 1 } });               
            pathfinder.add_vertex('Y', new Dictionary<char, int>() { { 'X', 1 }, { 'Z', 1 } });              
            pathfinder.add_vertex('Z', new Dictionary<char, int>() { { 'Y', 1 }, { 'Z', 1 } });              
                                                                                                              
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
                            nodes.Add(new Node(x, 0, z + 1.25));
                    }
                }
            }

            nodes.Add(new Node(15, 0, 0.75));
            for (double z = 0.75; z < 28; z += 5.25)
            {
                if (z != 6)
                {
                    for (double x = 3.25; x <= 27.5; x += 24)
                    {
                        nodes.Add(new Node(x, 0, z));
                    }
                }
            }

            foreach (Node node in nodes)
            {
                worldObjects.Add(node.placeholder);
                node.SetName(Vertex.First());
                Vertex.RemoveAt(0);
            }
            robots[0].MoveTo('Z');

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

        public static List<Node> GetNodes()
        {
            return nodes;
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