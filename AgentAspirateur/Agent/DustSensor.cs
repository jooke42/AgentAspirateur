using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur.Agent
{
    public class DustSensor : Sensors 
    {

        override
        public Boolean check(int x, int y,Room[][] map)
        {
            return map[x][y].getHasDust();
            
        }

        override
      public List<Position> getPosition(Room[][] map)
        {
            List<Position> position = new List<Position>();

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i][j].getHasDust())
                        position.Add(new Position(i, j));

                }
            }

            return position;
        }

    }
}
