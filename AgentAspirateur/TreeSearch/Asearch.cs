using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentAspirateur.Agent;

namespace AgentAspirateur.TreeSearch
{
    struct goal
    {
        public Position position { get; set; }
        public int ManhattanDistance { get; set; }

    }

    struct adjacentSquare
    {
        public Position position { get; set; }
        public int ManhattanDistance { get; set; }
        public int G { get; set; }

        public int F { get; set; }

        public bool hasDiamond { get; set; }
        public bool hasDust { get; set; }
        
    }

    class Asearch : TreeSearch
    {

        private goal currentGoal;
        public void AddProblem()
        {
            throw new NotImplementedException();
        }

        public List<Agent.Action> findPath(Position start, List<Position> dustOrDiamond)
        {

            currentGoal = getGoal(start, dustOrDiamond);

            return null;

        }
              
       


        private List<Position> adjacentSquares(Position currentPosition)
        {
            List<Position> adjacentSquares = new List<Position>();
            Position east = currentPosition.getPositionInDirection(direction.EAST);
            Position north = currentPosition.getPositionInDirection(direction.NORTH);
            Position south = currentPosition.getPositionInDirection(direction.SOUTH);
            Position west = currentPosition.getPositionInDirection(direction.WEST);

            if (east.validPosition())
                adjacentSquares.Add(east);
            if (north.validPosition())
                adjacentSquares.Add(north);
            if (south.validPosition())
                adjacentSquares.Add(south);
            if (west.validPosition())
                adjacentSquares.Add(west);

            return adjacentSquares;



        }
                    
                        
        private goal getGoal(Position currentPosition, List<Position> dustOrDiamond)
        {
            return getClosestGoal(currentPosition, dustOrDiamond);
        }

        private List<goal> getGoals(Position currentPosition,List<Position> dustOrDiamond)
        {
            List<goal> goals = new List<goal>();
            foreach(Position p in dustOrDiamond)
            {
                goals.Add(new goal { position = p, ManhattanDistance = computeHeuristic(currentPosition, p) });
            }
            return goals;
        }
        
        private goal getClosestGoal(Position currentPosition, List<Position> dustOrDiamond)
        {
            goal goal = new goal { position = new Position(0, 0), ManhattanDistance = int.MaxValue };
            foreach(goal g in getGoals(currentPosition, dustOrDiamond))
                 if (g.ManhattanDistance < goal.ManhattanDistance)
                 {
                        goal = g;
                 }
            return goal;
        }   
        
        private int computeHeuristic(Position startPoint, Position goalPoint)
        {
            return  computeManhattanDistance(startPoint, goalPoint);
        }

        private int computeManhattanDistance(Position startPoint, Position goalPoint)
        {
            int absX = Math.Abs(goalPoint.x - startPoint.x);
            int absY = Math.Abs(goalPoint.y - startPoint.y);          
            int manhattanDistance = absX + absY ;
            return manhattanDistance;

        }

        private int computeG(adjacentSquare adjacentSquare)
        {
            int g = 3;

            if (adjacentSquare.hasDiamond)
                g--;
            if (adjacentSquare.hasDust)
                g--;
            
            return g;
        }

    }


   

}
