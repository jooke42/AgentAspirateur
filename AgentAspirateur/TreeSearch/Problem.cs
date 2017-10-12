using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur.Agent
{
    public class State
    {
        public Position robotPos;
        public List<Position> dustOrDiamondPos;

        public State(){
            this.dustOrDiamondPos = new List<Position>();
        }
        public State(State _state) {
            this.dustOrDiamondPos=new List<Position>(dustOrDiamondPos);
        }

        public State GenerateNewStateFromAction(Action action)
        {
            State newState;
            if (this.dustOrDiamondPos.Contains(action.applyTo))
            {
                newState = new State(this);
                newState.dustOrDiamondPos.Remove(action.applyTo);
                newState.robotPos = new Position(action.applyTo);
            }
            else
            {
                throw new Exception("action impossible");
            }

            return newState;
        }

        public State(Position _robotPos , List<Tile>[][] map)
        {
            this.dustOrDiamondPos = new List<Position>();
            this.robotPos = new Position(_robotPos);
            for (int i = map.Length; i > 0; i--)
            {
                for (int j = map[i].Length; j > 0; j--)
                {
                    bool dust = map[i][j].Contains(Tile.DUST);
                    bool diamond = map[i][j].Contains(Tile.DIAMOND);
                    if (dust || diamond)
                        dustOrDiamondPos.Add(new Position(i, j));
                }
            }
        }


    }
    
    public class Problem
    {
        State initialState;
        private List<Tile>[][] goal;

        public Problem(State _initialState, List<Tile>[][] _goal)
        {
            this.initialState = _initialState;
            this.goal = _goal;
        }

        public Boolean goalCompleted(State s)
        {
            return s.dustOrDiamondPos.Count() == 0;
        }
        
        
    }
}
