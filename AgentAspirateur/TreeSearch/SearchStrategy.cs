﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur.Agent
{
    interface SearchStrategy
    {
        // SearchPath to resolve problem
        List<Action> SearchPath(Problem p);
        
    }
}
