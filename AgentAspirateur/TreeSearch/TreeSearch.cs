using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur.TreeSearch
{
    public interface TreeSearch
    {

        //Problem
        void AddProblem();

        List<Agent.Action> findPath(Position start, List<Position> dustOrDiamond);
       


    }
}
