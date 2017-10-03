using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur
{
    public enum direction { NORTH, EAST,SOUTH,WEST}

    public static class DirectionMethod
    {
        public static direction directionFromString(String dirS)
        {
            direction d;
            switch (dirS)
            {
                case "NORTH":
                    d = direction.NORTH;
                    break;
                case "SOUTH":
                    d = direction.SOUTH;
                    break;
                case "EAST":
                    d = direction.EAST;
                    break;
                case "WEST":
                    d = direction.WEST;
                    break;
                default:
                    throw new Exception("string is not NORTH, SOUTH, EAST, WEST");
                    break;
            }
            return d;
        }
    }
    public class Position

    {
        public int x, y;

        public Position(Position p)
        {
            this.x = p.x;
            this.y = p.y;
        }

        public Position(int _x, int _y)
        {
            this.x = _x;
            this.y = _y;
        }

        public Position getPositionInDirection(direction d)
        {
            Position p = null;
            switch (d)
            {
                case direction.NORTH:
                    p= new Position(this.x,this.y-1);
                    break;
                case direction.EAST:
                    p = new Position(this.x + 1, this.y);
                    break;
                case direction.SOUTH:
                    p = new Position(this.x, this.y + 1);
                    break;
                case direction.WEST:
                    p = new Position(this.x - 1, this.y);
                    break;
            }
            return p;

        }
        public bool validPosition(int xlim,int ylim)
        {
            return xlim > this.x && this.x >= 0 && this.y >= 0 && ylim > this.y;
        }
    }
}
