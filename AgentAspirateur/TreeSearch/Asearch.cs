using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentAspirateur.Agent;

namespace AgentAspirateur.TreeSearch
{
   
    class Asearch : SearchStrategy
    {
        Problem problem;

        public Asearch(Problem _problem)
        {
            this.problem = _problem;          
        }

        public Node SearchPath(Problem p)
        {
            Node startingNode = new Node(null, null, p.initialState, 0);
            //compute path code befoe sort

            return findBestNode(startingNode,startingNode.expand(p));          
            
        }

        private Node findBestNode(Node start, List<Node> node)
        {
            int minHeuristic = Int32.MaxValue;
            Node bestNode = null;
            foreach(Node n in node)
            {
                int tmp = computeHeuristic(start, n);
                if (tmp < minHeuristic)
                {
                    minHeuristic = tmp;
                    bestNode = n;
                    
                }
            }

            return bestNode;
        }


        private int computeHeuristic(Node start, Node goal)
        {
            //Compute manhattan Distance between node stard and goal
            int manhattanDistance = computeManhattanDistance(start.state.robotPos, goal.state.robotPos);

            int heuristic = getHeuristic(start, goal);

            return heuristic + manhattanDistance;

        }


        private int computeManhattanDistance(Position startPoint, Position goalPoint)
        {
            int absX = Math.Abs(goalPoint.x - startPoint.x);
            int absY = Math.Abs(goalPoint.y - startPoint.y);
            int manhattanDistance = absX + absY;
            return manhattanDistance;
        }

        private int getHeuristic(Node start, Node goal)
        {
            int heuristic = Int32.MaxValue;


            //Find each diamond and dust 

            return 0;
        }

        private int computeNumberOfDustOrDiamond(Position p)
        {
            
            return 0;
        }

        private int getDustNumberInDirection(Position start,direction direction, Problem p)
        {          
            List<Position> dustOrDiamond =  p.initialState.dustOrDiamondPos;

            return 0; 

        }


        

        

    }

}
