using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur.Agent
{
    public struct State
    {
        public Position robotPos;
        public List<Tile>[][] map;

    }

    public enum Action { NORTH, SOUTH, EAST, WEST, VACUUM, PICK }

    class Problem
    {
        State initialState;
        private List<Tile>[][] goal;

        public Problem(State _initialState, List<Tile>[][] _goal)
        {
            this.initialState = _initialState;
            this.goal = _goal;
        }

        public Boolean goalCompleted()
        {
            for (int i = 0; i < goal.Length; i++)
            {
                for (int j = 0; j < goal[i].Length; j++)
                {
                    if (!mapsEquals(goal[i][j], initialState.map[i][j]))
                        return false;
                }
            }
            return true;
        }

        public double ActionCost(Node start, Node end)
        {
            return start.pos.dist(end.pos);
        }

        private bool mapsEquals(List<Tile> m1, List<Tile> m2)
        {
            
            foreach (Tile t in m1)
            {
                if (!m2.Contains(t))
                    return false;
            }
            foreach (Tile t in m2)
            {
                if (!m1.Contains(t))
                    return false;
            }
            return true;
        }
    }
}
