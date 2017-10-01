using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace AgentAspirateur.Agent
{
    public class DiamondSensor : Sensors
    {
        override
         public Boolean check(int x, int y, List<Tile>[][] map)
        {
            return map[x][y].Contains(Tile.DIAMOND);
        }
        override
        public List<Position> getPosition(List<Tile>[][] map)
        {
            List<Position> position = new List<Position>();

            for (int i = 0; i < map.GetLength(0); i++)
            {                
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i][j].Contains(Tile.DIAMOND))
                        position.Add(new Position(i, j));

                }
            }

            return position;
        }
    }
}
