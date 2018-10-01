using System;
using System.Collections.Generic;
using System.Text;


namespace Controllers
{
    public class Freightmaster
    {
        bool docked = false;

        public void boatdocked(bool docked)
        {
            this.docked = docked;            
        }
    }
}
