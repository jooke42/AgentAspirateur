using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur.Agent
{
    public class Node
    {
        private Node parentNode;
        private HashSet<Node> childNodes = new HashSet<Node>();
        int depth;
        int pathCost;
        Action action;
        public State state;

        public Node(
            Node _parentNode,
            Action _action,
            State _state,
            int _depth)
        {
            this.parentNode = _parentNode;
            this.action = new Action(_action);
            this.state = _state;
            this.depth = _depth;
        }

        public void setPathCost(int _pathCost)
        {
            this.pathCost = _pathCost;
        }

        // Expand given node
        public HashSet<Node> expand(Problem p)
        {
            HashSet<Node> nodes = new HashSet<Node>();
            foreach (KeyValuePair<Action, State> entry in successorFN(p))
            {
                Node s = new Node(
                    this,
                     entry.Key,
                    entry.Value,
                    this.depth + 1
                    );

                s.setPathCost((int)(this.pathCost + actionCost(this, s)));
            }
            return nodes;
        }

        public double actionCost(Node start, Node end)
        {
            return start.state.robotPos.dist(end.state.robotPos);
        }

        // generate 
        Dictionary<Action, State> successorFN(Problem p)
        {
            Dictionary<Action, State> result = new Dictionary<Action, State>();
            foreach(Position pos in this.state.dustOrDiamondPos)
            {

                Action newAct = new Action(pos, ActionType.MOVE);
                result.Add(newAct, this.state.GenerateNewStateFromAction(newAct));
            }

            return result;
        }
    }
}
