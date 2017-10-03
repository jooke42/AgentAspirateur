using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur.Agent
{
    class Node
    {

        private Node North;
        private Node South;
        private Node East;
        private Node West;
        private bool Dust;
        private bool Diamond;


        public Node() {

            North = null;
            South = null;
            West = null;
            East = null;
            Dust = false;
            Diamond = false;

        }

        public Node(Node pNorth, Node pSouth, Node pWest, Node pEast, bool pDust, bool pDiamond)
        {

            North = pNorth;
            South = pSouth;
            West = pWest;
            East = pEast;
            Dust = pDust;
            Diamond = pDiamond;

        }

        public Node getNorth() { return North; }
        public Node getSouth() { return South; }
        public Node getEast() { return East; }
        public Node getWest() { return West; }
        public bool hasDust() { return Dust; }
        public bool hasDiamond() { return Diamond; }




        public void setNorth(Node pNorth) { North = pNorth; }
        public void setSouth(Node pSouth) { South = pSouth; }
        public void setEast(Node pEast) { East = pEast; }
        public void setWest(Node pWest) { West=pWest; }
        public void setDust(bool pDust) {Dust = pDust; }
        public void setDiamond(bool pDiamond) { Diamond=pDiamond; }

    }
}
