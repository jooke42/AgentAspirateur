using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentAspirateur.Environnement;
namespace AgentAspirateur.Agent
{
    public class Node : IComparable<Node>
    {
        public Node parentNode;
        private HashSet<Node> childNodes = new HashSet<Node>();
        public int depth;
        public int pathCost;
        public Action action;
        public State state;
        private int F;
        private int G;
        private int H;
     


        public Node(
            Node _parentNode,
            Action _action,
            State _state,
            int _depth)
        {
            this.parentNode = _parentNode;
            this.action = _action;
            this.state = _state;
            this.depth = _depth;
            G = getG();
            H = getH();
            F = getF();
        }

        public void setPathCost(int _pathCost)
        {
            this.pathCost = _pathCost;
        }

        // Expand given node
        public List<Node> expand(Problem p)
        {
          
                List<Node> nodes = new List<Node>();
                foreach (KeyValuePair<Action, State> entry in successorFN(p))
                {

                    Node s = new Node(
                        this,
                         entry.Key,
                        entry.Value,
                        this.depth + 1
                        );
                    this.childNodes.Add(s);

                    s.setPathCost((int)(this.pathCost + actionCost(this, s)));   
                    nodes.Add(s);
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
            foreach(Room room in this.state.dustOrDiamondPos)
            {
                if (room.getHasDust() && room.getHasDiamond())
                {
                    Action newAct = new Action(room.getCoordinate(), ActionType.PICK_VACUUM);
                    result.Add(newAct, this.state.GenerateNewStateFromAction(newAct));
                }
                else if (room.getHasDiamond())
                {
                    Action newAct = new Action(room.getCoordinate(), ActionType.PICK);
                    result.Add(newAct, this.state.GenerateNewStateFromAction(newAct));
                }else if (room.getHasDust())
                {
                    Action newAct = new Action(room.getCoordinate(), ActionType.VACUUM);
                    result.Add(newAct, this.state.GenerateNewStateFromAction(newAct));
                }
                
            }

            return result;
           
        }



        public HashSet<Node> getChildNotes()
        {
            return childNodes;
        }

        public int CompareTo(Node other)
        {
            if (this.pathCost > other.pathCost) return -1;
            if (this.pathCost == other.pathCost) return 0;
            return 1;
        }

        public override string ToString()
        {
            return state.robotPos.ToString();
        }

        public int getG()
        {

            if (parentNode == null)
                return 0;
            return parentNode.G + 1;
        }
        public int getH()
        {
            return 0;
        }

        public int getF()
        {
            return G + H;
        }
              
      
       



    }
}
