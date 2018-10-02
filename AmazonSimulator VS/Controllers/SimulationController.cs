using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Models;
using Views;

namespace Controllers {
    struct ObservingClient {
        public ClientView cv;
        public IDisposable unsubscribe;
    }
    public class SimulationController {
        private World w;
        private List<ObservingClient> views = new List<ObservingClient>();
        private bool running = false;
        private int tickTime = 25;
        private int tickCount = 0;

        public SimulationController(World w) {
            this.w = w;
        }

        public void AddView(ClientView v) {
            ObservingClient oc = new ObservingClient();

            oc.unsubscribe = this.w.Subscribe(v);
            oc.cv = v;

            views.Add(oc);
        }

        public void RemoveView(ClientView v) {
            for(int i = 0; i < views.Count; i++) {
                ObservingClient currentOC = views[i];

                if(currentOC.cv == v) {
                    views.Remove(currentOC);
                    currentOC.unsubscribe.Dispose();
                }
            }
        }

        public void Simulate() {
            running = true;

            new Thread(() =>
            {
                freight();
            }).Start();

            while (running) {
                tickCount++;
                w.Update(tickTime, tickCount);
                Thread.Sleep(tickTime);               
            }
        }

        public void EndSimulation() {
            running = false;
        }

        public void freight()
        {
            bool docked = false;
            Random rnd = new Random();
            int[] grabshelfs;
            int shelfsindock = 0;

            while (running)
            {
                if (w.GetShip().IsPaused() && !docked) //als een schip is aangekomen bij de dock(gepauseerd) en nog dit nog niet gezien is(!docked) draait 1x per ship pauze
                {
                    int robotshelfs = w.GetRobots().Count() * 2;
                    docked = true;
                    grabshelfs = new int[robotshelfs];
                    if (shelfsindock < (robotshelfs * 2)) // als er minder dan robots * 2 * 3 in het dock staan dan worden er nog boten geladen
                    {
                        for (int i = 0; i < robotshelfs; i++)
                        {
                            int temp = rnd.Next(0, 32);
                            if (w.GetShelfs()[temp].x != 15.25 && !grabshelfs.Contains(temp))
                            {
                                grabshelfs[i] = temp;
                            }
                            else i--;
                        }
                        shelfsindock += robotshelfs;

                        int e = 0;
                        for (int a = 0; a < 2; a++)
                        {
                            foreach (Robot robot in w.GetRobots())
                            {
                                robot.addtask(new MoveTask(w.GetShelfs()[grabshelfs[e]].GetNode()));
                                robot.addtask(new PickupShelf(w.GetShelfs()[grabshelfs[e]]));
                                robot.addtask(new MoveTask('α'));
                                robot.addtask(new DropShelf(15.25, 0.75));
                                e++;
                            }
                        }
                    }
                    else // als er robots * 2 * 3 in het dock staan dan worden er boten gelost
                    {
                        for (int i = 0; i < robotshelfs; i++)
                        {
                            int temp = rnd.Next(1, 28);
                            if (w.GetShelfs()[temp].x == 15.25 && !grabshelfs.Contains(temp))
                            {
                                grabshelfs[i] = temp;
                            }
                            else i--;
                        }

                        int e = 0;
                        for (int a = 0; a < 2; a++)
                        {
                            foreach (Robot robot in w.GetRobots())
                            {
                                robot.addtask(new PickupShelf(w.GetShelfs()[grabshelfs[e]]));
                                robot.addtask(new MoveTask(w.GetShelfs()[grabshelfs[e]].GetNode()));
                                robot.addtask(new DropShelf(w.GetShelfs()[grabshelfs[e]].defx, w.GetShelfs()[grabshelfs[e]].defz));
                                robot.addtask(new MoveTask('α'));                               
                                e++;
                            }
                        }
                        shelfsindock -= robotshelfs;
                    }
                }
                else if(w.GetShip().IsPaused() && docked)
                {
                    int i = 0;
                    foreach(Shelf dock in w.GetShelfs())
                    {
                        if (dock.x == 15.25)
                            i++;
                    }
                    if (i == shelfsindock)
                    {
                        w.GetShip().Pause(false);
                        docked = false;
                    }                       
                }
                Thread.Sleep(tickTime * 5);
            }           
        }
    }
}