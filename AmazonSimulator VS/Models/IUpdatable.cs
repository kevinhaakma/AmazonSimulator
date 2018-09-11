using System;
using System.Collections.Generic;
using System.Linq;

namespace Models {
    interface IUpdatable
    {
        bool Update(int tick);
    }
}
