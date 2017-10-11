using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur.Agent
{
    class Node
    {

        private HashSet<Node> neighboors = new HashSet<Node>();
        private bool dust;
        private bool diamond;
        public Position pos;

        public Node() {
            dust = false;
            diamond = false;
        }

        public Node(int _x, int _y, bool _dust, bool _diamond)
        {
            pos = new Position(_x,_y);
            dust = _dust;
            diamond = _diamond;
        }

        public Node(Position _p, bool _dust, bool _diamond)
        {
            pos = new Position(_p);
            dust = _dust;
            diamond = _diamond;
        }

        public bool hasDust() { return dust; }
        public bool hasDiamond() { return diamond; }

        public void addNeighboor(Node n)
        {
            this.neighboors.Add(n);
            n.neighboors.Add(this);
        }
        public void removeNeighboor(Node n)
        {
            this.neighboors.Remove(n);
            n.neighboors.Remove(this);
        }
        public HashSet<Node> getNeighboors()
        {
            return neighboors;
        }
        public void setDust(bool pDust) {dust = pDust; }
        public void setDiamond(bool pDiamond) { diamond = pDiamond; }

    }
}
