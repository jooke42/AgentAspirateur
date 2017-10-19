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
         public Boolean check(int x, int y, Room[][] map)
        {
            return map[x][y].hasDiamond;
        }
        override
        public List<Room> getPosition(Room[][] map)
        {
            List<Room> position = new List<Room>();

            for (int i = 0; i < map.Length ; i++)
            {                
                for (int j = 0; j < map[i].Length ; j++)
                {
                    if (map[i][j].hasDiamond)
                        position.Add(new Room(map[i][j]));

                }
            }

            return position;
        }
    }
}
