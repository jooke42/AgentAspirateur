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
                    //if (n.parentNode != null)
                    //{
                    //    if (n.parentNode.action != null)
                    //    {
                    //        if (n.parentNode.action.actionType == ActionType.VACUUM)
                    //        {

                    //            minHeuristic = 0;
                    //        }
                    //    }
                    //}

                }
            }
                       

            return bestNode;
        }


        private int computeHeuristic(Node start, Node goal)
        {
            //Compute manhattan Distance between node stard and goal
            int manhattanDistance = computeManhattanDistance(start.state.robotPos, goal.state.robotPos);
            int heuristic = computeNumberOfDustOrDiamond(manhattanDistance, goal.state.robotPos);
            Console.WriteLine("Comptage" + goal.state.dustOrDiamondPos.Count);

            return manhattanDistance + goal.state.dustOrDiamondPos.Count; 


            //Compue number of diamond and dust left MERCI MARTEAUX 

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
            if (problem.initialState.dustOrDiamondPos.Contains(p.getRoomInDirection(direction.NORTH)) && p.getRoomInDirection(direction.NORTH).getCoordinate().validPosition())
                count--;
            if (problem.initialState.dustOrDiamondPos.Contains(p.getRoomInDirection(direction.EAST)) && p.getRoomInDirection(direction.EAST).getCoordinate().validPosition())
                count--;
            if (problem.initialState.dustOrDiamondPos.Contains(p.getRoomInDirection(direction.WEST)) && p.getRoomInDirection(direction.WEST).getCoordinate().validPosition())
                count--;
            if (problem.initialState.dustOrDiamondPos.Contains(p.getRoomInDirection(direction.SOUTH)) && p.getRoomInDirection(direction.WEST).getCoordinate().validPosition())
                count--;
            if (problem.initialState.dustOrDiamondPos.Contains(p.getRoomInDirection(direction.SE)) && p.getRoomInDirection(direction.SE).getCoordinate().validPosition())
                count--;
            if (problem.initialState.dustOrDiamondPos.Contains(p.getRoomInDirection(direction.SW)) && p.getRoomInDirection(direction.SW).getCoordinate().validPosition())
                count--;
            if (problem.initialState.dustOrDiamondPos.Contains(p.getRoomInDirection(direction.NE)) && p.getRoomInDirection(direction.NE).getCoordinate().validPosition())
                count--;
            if (problem.initialState.dustOrDiamondPos.Contains(p.getRoomInDirection(direction.NW)) && p.getRoomInDirection(direction.NW).getCoordinate().validPosition())
                count--;
            return count;
            
        }

       


        

        

    }

}
