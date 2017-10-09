using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace AgentAspirateur.Agent
{

    

    class IterativeDeepeningSearch : SearchLogic
    {

        public const bool NOT_EMPTY = true;


        Node SearchPath(Node nod) {

            int detph = 0;
            Node result = null;

            for (detph = 0; detph < int.MaxValue; detph++)
            {
                result = DepthLimitedSearch(nod,NOT_EMPTY, detph);
                if (result != null) return result;

            }

            return null;


        }




        Node DepthLimitedSearch(Node nod, bool goal, int limit)
        {

            Node result = null;
            if (nod.hasDust() == goal || nod.hasDiamond() == goal) { return result; }
            else if (limit == 0) { return result; }
            else {
                result = DepthLimitedSearch(nod.getNorth(), goal, --limit);

                result = DepthLimitedSearch(nod.getSouth(), goal, --limit);
                result = DepthLimitedSearch(nod.getWest(), goal, --limit);
                result = DepthLimitedSearch(nod.getEast(), goal, --limit);




            }

            return null;

        }

        public Node SearchPath(Node Start, Node Goal, Graph g)
        {
            throw new NotImplementedException();
        }
    }
}
