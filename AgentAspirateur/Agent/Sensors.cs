using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentAspirateur.Environnement;

namespace AgentAspirateur.Agent
{
   public abstract class Sensors
    {

        public abstract Boolean check(int x, int y, Room[][] map);
        public abstract List<Position> getPosition(Room[][] map);

    }
}
