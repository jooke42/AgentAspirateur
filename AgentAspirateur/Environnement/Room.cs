using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur.Environnement
{
    public class Room : IEquatable<Room>, IEquatable<Position>
    {
        Position coordinate;
        bool hasDust;
        bool hasDiamond;
        
        public Room(Position _coordinate,bool _hasDust,bool _hasDiamond)
        {
            this.coordinate = new Position(_coordinate);
            this.hasDiamond = _hasDiamond;
            this.hasDust = _hasDust;
        }

        public Room(Position _coordinate)
        {
            this.coordinate = new Position(_coordinate);
            this.hasDiamond = false;
            this.hasDust = false;
        }
        public Room(int _coordinateX, int _coordinateY)
        {
            this.coordinate = new Position(_coordinateX,_coordinateY);
            this.hasDiamond = false;
            this.hasDust = false;
        }

        #region Getter_Setter

        public Position getCoordinate()
        {
            return this.coordinate;
        }

        public void setCoordinate(Position _coord)
        {
            this.coordinate =_coord!=null?new Position(_coord):null;
        }

        public bool getHasDust()
        {
            return this.hasDust;
        }

        public void setHasDust(bool _hasDust)
        {
            this.hasDust = _hasDust;
        }

        public bool getHasDiamond()
        {
            return this.hasDiamond;
        }

        public void setHasDiamond(bool _hasDiamond)
        {
            this.hasDiamond = _hasDiamond;
        }


        #endregion 

        public bool isClean()
        {
            return hasDust == false && hasDiamond == false;
        }

        public bool Equals(Room other)
        {
            return this.coordinate.Equals(other.coordinate);
        }

        public bool Equals(Position other)
        {
            return this.coordinate.Equals(other);
        }
    }
}
