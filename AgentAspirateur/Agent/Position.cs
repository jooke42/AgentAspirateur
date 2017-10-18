using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur
{
    public enum direction { NORTH, EAST,SOUTH,WEST, NE, NW, SE, SW}

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
                    
            }
            return d;
        }
    }
    

    public class Position : IEquatable<Position>

    {
        public int x, y;
        public Position(){}
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
        public double dist(Position p2)
        {
            double x = 0;
            double y = 0;

            x = p2.x - this.x;
            y = p2.y - this.y;

            return Math.Sqrt(x*x+y*y);
        }
        public Room getRoomInDirection(direction d)
        {
            Room p = null;
            switch (d)
            {
                case direction.NORTH:
                    p= new Room(this.x,this.y-1);
                    break;
                case direction.EAST:
                    p = new Room(this.x + 1, this.y);
                    break;
                case direction.SOUTH:
                    p = new Room(this.x, this.y + 1);
                    break;
                case direction.WEST:
                    p = new Room(this.x - 1, this.y);
                    break;
                case direction.NE:
                    p = new Room(this.x + 1, this.y-1);
                    break;
                case direction.NW:
                    p = new Room(this.x - 1, this.y - 1);
                    break;
                case direction.SE:
                    p = new Room(this.x + 1, this.y + 1);
                    break;
                case direction.SW:
                    p = new Room(this.x - 1, this.y + 1);
                    break;
            }
            return p;

        }

        public bool isNorth(Position other)
        {
            return other.x == this.x && other.y == this.y - 1;
        }

        public bool isSouth(Position other)
        {

            return other.x == this.x && other.y == this.y + 1;
        }

        public bool isEast(Position other)
        {

            return other.x == this.x +1 && other.y == this.y ;
        }

        public bool isWest(Position other)
        {
            return other.x == this.x - 1 && other.y == this.y;
        }

        public bool validPosition(int xlim,int ylim)
        {
            return xlim > this.x && this.x >= 0 && this.y >= 0 && ylim > this.y;
        }

        public bool validPosition()
        {
            return validPosition(10, 10);
        }

        public bool Equals(Position other)
        {
            return this.x == other.x && this.y == other.y;
        }

        public override string ToString()
        {
            return "(" + x+";"+y+")";
        }
    }
}
