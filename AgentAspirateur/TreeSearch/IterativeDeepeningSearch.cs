using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace AgentAspirateur.Agent
{

    

    class IterativeDeepeningSearch : SearchStrategy
    {
        Node DepthLimitedSearch(Problem p,Node currentNode, int depth)
        {
            if (p.goalCompleted() && depth == 0) {
                return currentNode;
            }
            else {
                foreach (Node childNode in currentNode.getNeighboors())
                {
                    return DepthLimitedSearch(childNode, --depth);
                }
            }

            return null;

        }

        public List<Action> SearchPath(Problem p, Graph g)
        {
            int detph = 0;
            Queue<Node> result = new Queue<Node>();

            for (detph = 0; detph < int.MaxValue; detph++)
            {
                result = DepthLimitedSearch(p., detph);
                if (result != null) return result;

            }

            return null;
        }
    }
}
