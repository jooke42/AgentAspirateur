using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentAspirateur.Environnement;

namespace AgentAspirateur.Agent
{
    public class State
    {
        public Position robotPos;
        public List<Room> dustOrDiamondPos;

        public State(){
            this.dustOrDiamondPos = new List<Room>();
        }
        public State(State _state) {
            this.dustOrDiamondPos=new List<Room>(_state.dustOrDiamondPos);
        }

        public State GenerateNewStateFromAction(Action action)
        {
            State newState;
            if (this.dustOrDiamondPos.Contains(new Room(action.applyTo)))
            {
                newState = new State(this);
                newState.dustOrDiamondPos.Remove(new Room(action.applyTo));
                newState.robotPos = new Position(action.applyTo);
            }
            else
            {
                throw new Exception("action impossible");
            }

            return newState;
        }

        public State(Position _robotPos , Room[][] map)
        {
            this.dustOrDiamondPos = new List<Room>();
            this.robotPos = new Position(_robotPos);
            for (int i = map.Length -1; i >= 0; i--)
            {
                for (int j = map[i].Length-1; j >= 0; j--)
                {
                    if (!map[i][j].isClean())
                        dustOrDiamondPos.Add(map[i][j]);
                }
            }
        }


    }
    
    public class Problem
    {
        public State initialState;

        public Problem(State _initialState)
        {
            this.initialState = _initialState;
        }

        public Boolean goalCompleted(State s)
        {
            return s.dustOrDiamondPos.Count() == 0;
        }
        public override string ToString()
        {
            return initialState.ToString();
        }


    }
}
