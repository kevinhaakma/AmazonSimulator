using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Freightmaster
    {
        bool ready = true;
        public void boat()
        {
            if (ready == true)
                ready = false;
        }
    }
}
