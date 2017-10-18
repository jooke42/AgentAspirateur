using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur.Agent
{
    public enum ActionType { PICK_VACUUM, VACUUM, PICK }

    public class Action : IEqualityComparer<Action>
    {
        public Position applyTo;
        public ActionType actionType;

        public Action(Action t)
        {
            this.applyTo = new Position(t.applyTo);
            this.actionType = t.actionType;
        }

        public Action(int _x, int _y, ActionType _actionType)
        {
            this.applyTo = new Position(_x, _y);
            this.actionType = _actionType;
        }

        public Action(Position pos, ActionType _actionType)
        {
            this.applyTo = new Position(pos);
            this.actionType = _actionType;
        }

        public bool Equals(Action other)
        {
            return this.applyTo.Equals(other.applyTo) && this.actionType == other.actionType;
        }

        public int CompareTo(Action other)
        {
            if (this.Equals(other))
                return 0;
            else
                return -1;
            
        }

        public bool Equals(Action x, Action y)
        {
            return x.applyTo.Equals(y.applyTo) && x.actionType == y.actionType;
        }

        public int GetHashCode(Action obj)
        {
            throw new NotImplementedException();
        }
    }
}
