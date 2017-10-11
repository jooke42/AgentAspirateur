using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur.Agent
{
    class Graph
    {
        public List<Node> nodes;
        public Graph(List<Tile>[][] map)
        {
            nodes = new List<Node>();
            for (int i = map.Length; i > 0; i--)
            {
                for (int j = map[i].Length; j > 0; j--)
                {
                    bool dust = map[i][j].Contains(Tile.DUST);
                    bool diamond = map[i][j].Contains(Tile.DIAMOND);
                    Node newNode = new Node(i, j, dust, diamond);
                    foreach (Node n in nodes)
                        n.addNeighboor(n);
                }
            }
        }
    }
}
