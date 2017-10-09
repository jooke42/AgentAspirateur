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
        int x, y;

        public Node() {
            dust = false;
            diamond = false;
        }

        public Node(int _x, int _y, bool _dust, bool _diamond)
        {
            this.x = _x;
            this.y = _y;
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

        public void setDust(bool pDust) {dust = pDust; }
        public void setDiamond(bool pDiamond) { diamond = pDiamond; }

    }
}
