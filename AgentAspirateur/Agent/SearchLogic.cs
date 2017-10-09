using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur.Agent
{
    interface SearchLogic
    {
        Node SearchPath(Node Start , Node Goal, Graph g);
    }
}
