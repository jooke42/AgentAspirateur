using AgentAspirateur.Agent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur.TreeSearch
{
    class UniformCostSearch : SearchStrategy
    {
        public Node SearchPath(Problem p)
        {
            SortedSet<Node> fringe = new SortedSet<Node>();
            Node startingNode = new Node(null, null, p.initialState, 0);
            fringe.Add(startingNode);
            Stack<Agent.Action> actions = new Stack<Agent.Action>();

            while( fringe.Count() != 0)
            {
                Node node = fringe.Max();
                fringe.Remove(node);
                if (p.goalCompleted(node.state))
                    return node;

                foreach(Node n in node.expand(p))
                    fringe.Add(n);
            }

            return null;          
            
        }

       


    }
}
