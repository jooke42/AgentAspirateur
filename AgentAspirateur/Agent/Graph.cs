using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur.Agent
{
    class Graph
    {
        public Node[][] nodes;
        public Graph(List<Tile>[][] map)
        {
            nodes = new Node[map.Length][];
            for (int i = nodes.Length; i > 0; i--)
            {
                nodes[i] = new Node[map[i].Length];
                for (int j = nodes[i].Length; j > 0; j--)
                {
                    bool dust = map[i][j].Contains(Tile.DUST);
                    bool diamond = map[i][j].Contains(Tile.DIAMOND);
                    nodes[i][j]= new Node(i,j,dust,diamond);

                    if( i + 1 < nodes.Length)
                        nodes[i][j].addNeighboor(nodes[i + 1][j]);
                    if (j + 1 < nodes[i].Length)
                        nodes[i][j].addNeighboor(nodes[i][j + 1]);
                }
            }
        }
    }
}
