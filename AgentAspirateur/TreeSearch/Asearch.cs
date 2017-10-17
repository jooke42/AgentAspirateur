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
            int heuristic = computeNumberOfDustOrDiamond(manhattanDistance, goal.state.robotPos);
            return  manhattanDistance ;

        }


        private int computeManhattanDistance(Position startPoint, Position goalPoint)
        {
            int absX = Math.Abs(goalPoint.x - startPoint.x);
            int absY = Math.Abs(goalPoint.y - startPoint.y);
            int manhattanDistance = absX + absY;
            return manhattanDistance;
        }
        

        private void findBestDirection()
        {


        }

        private Node closestNode(direction d)
        {


            return null;
        }

        private int computeNumberOfDustOrDiamond(int manhattanDistance, Position p)
        {

            //North 




            int count = manhattanDistance;
            if (problem.initialState.dustOrDiamondPos.Contains(p.getPositionInDirection(direction.NORTH)) && p.getPositionInDirection(direction.NORTH).validPosition())
                count--;
            if (problem.initialState.dustOrDiamondPos.Contains(p.getPositionInDirection(direction.EAST)) && p.getPositionInDirection(direction.EAST).validPosition())
                count--;
            if (problem.initialState.dustOrDiamondPos.Contains(p.getPositionInDirection(direction.WEST)) && p.getPositionInDirection(direction.WEST).validPosition())
                count--;
            if (problem.initialState.dustOrDiamondPos.Contains(p.getPositionInDirection(direction.SOUTH)) && p.getPositionInDirection(direction.WEST).validPosition())
                count--;
            if (problem.initialState.dustOrDiamondPos.Contains(p.getPositionInDirection(direction.SE)) && p.getPositionInDirection(direction.SE).validPosition())
                count--;
            if (problem.initialState.dustOrDiamondPos.Contains(p.getPositionInDirection(direction.SW)) && p.getPositionInDirection(direction.SW).validPosition())
                count--;
            if (problem.initialState.dustOrDiamondPos.Contains(p.getPositionInDirection(direction.NE)) && p.getPositionInDirection(direction.NE).validPosition())
                count--;
            if (problem.initialState.dustOrDiamondPos.Contains(p.getPositionInDirection(direction.NW)) && p.getPositionInDirection(direction.NW).validPosition())
                count--;
            return count;
        }

       


        

        

    }

}
