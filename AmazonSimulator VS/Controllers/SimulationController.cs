using System;
using System.Collections.Generic;
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
        Freightmaster master = new Freightmaster();
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
            while (running)
            {
                master.Check(w);
                Thread.Sleep(tickTime * 5);
            }           
        }
    }
}