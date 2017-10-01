using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur.Agent
{
   public abstract class Sensors
    {

        public abstract Boolean check(int x, int y, Tile[,] map);
        public abstract List<Position> getPosition(Tile[,] map);

    }
}
