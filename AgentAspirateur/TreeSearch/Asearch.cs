using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentAspirateur.Agent;
using System.Collections;


namespace AgentAspirateur.TreeSearch
{
   
    class Asearch : SearchStrategy
    {
        
       
        //Trouve le meilleur noeud
        public Node SearchPath(Problem p)
        {
            List<Node> fringe = new List<Node>();
            Node startingNode = new Node(null, null, p.initialState, 0);
            fringe.Add(startingNode);          
            Stack<Agent.Action> actions = new Stack<Agent.Action>();

            while (fringe.Count() != 0)
            {
                Node node = fringe.First();
                fringe.Remove(node);
                if (p.goalCompleted(node.state))
                    return node;
                               

                foreach (Node n in node.expand(p))
                {
                    n.setHeuristic(findBestNode(n));
                    fringe.Add(n);                    
                    fringe.Sort(Compare);
                }
            }

            return null;
            
        }
             

        //Renvoi la meilleure heuristic
        private int findBestNode(Node start)
        {
            int minHeuristic = 500;
           
            foreach (Room room in start.state.dustOrDiamondPos)
            {
                int tmp = computeHeuristic(start, room.getCoordinate());
                if (tmp < minHeuristic)
                {
                    minHeuristic = tmp;                 

                }
            }

            return minHeuristic;
        }

        //Calcul l'heuristic
        private int computeHeuristic(Node start, Position goal)
        {
            //Compute manhattan Distance between node stard and goal
            int manhattanDistance = computeManhattanDistance(start.state.robotPos, goal);           
            return manhattanDistance + start.state.dustOrDiamondPos.Count;        

        }

        //Distance de manhattan
        private int computeManhattanDistance(Position startPoint, Position goalPoint)
        {
            int absX = Math.Abs(goalPoint.x - startPoint.x);
            int absY = Math.Abs(goalPoint.y - startPoint.y);
            int manhattanDistance = absX + absY;
            return manhattanDistance;
        }
        
        
        // Permet de comparer deux calcul de fonction d'évaluation
        private int Compare(Node n1, Node n2)
        {
            int eval1 = n1.getHeuristic() + n1.pathCost;
            int eval2 = n2.getHeuristic() + n2.pathCost;
            int eval = eval1.CompareTo(eval2);
            return eval;
        }

      





    }

}
