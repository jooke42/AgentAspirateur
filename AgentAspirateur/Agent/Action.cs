using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur.Agent
{
    public enum ActionType { MOVE, VACUUM, PICK }

    public class Action : IEquatable<Action>
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
    }
}
