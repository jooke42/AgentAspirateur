using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur.Agent
{
    public enum ActionType { PICK_VACUUM, VACUUM, PICK, }
    public enum SimpleActionType { VACUUM, PICK, TOP, BOTTOM, LEFT, RIGHT }

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
        public List<SimpleActionType> generateSimpleAction(Position robot)
        {
            List<SimpleActionType> simpleActionList = new List<SimpleActionType>();
                switch (this.actionType)
                {
                    case ActionType.PICK:
                        simpleActionList.AddRange(move(robot));
                        simpleActionList.Add(SimpleActionType.PICK);
                        break;

                    case ActionType.VACUUM:
                        simpleActionList.AddRange(move(robot));
                        simpleActionList.Add(SimpleActionType.VACUUM);
                    break;
                    case ActionType.PICK_VACUUM:
                        simpleActionList.AddRange(move(robot));
                        simpleActionList.Add(SimpleActionType.PICK);
                        simpleActionList.Add(SimpleActionType.VACUUM);
                    break;
                }
            return simpleActionList;
        }

        public List<SimpleActionType> move(Position robot)
        {
            List<SimpleActionType> simpleActionList = new List<SimpleActionType>();
            int deltaX, deltaY;
            deltaX = this.applyTo.x - robot.x;
            deltaY = this.applyTo.y - robot.y;
            bool doX = true;
            while (deltaX != 0 || deltaY != 0)
            {
                if (deltaX != 0 && doX)
                {
                    if (deltaX > 0)
                    {
                        simpleActionList.Add(SimpleActionType.RIGHT);
                        deltaX--;
                    }
                    else
                    {
                        simpleActionList.Add(SimpleActionType.LEFT);
                        deltaX++;
                    }

                    doX = false;
                }
                else if (deltaY != 0)
                {
                    if (deltaY < 0)
                    {
                        //Console.WriteLine("NORTH");
                        simpleActionList.Add(SimpleActionType.TOP);
                        deltaY++;
                    }
                    else
                    {
                        // Console.WriteLine("SOUTH");
                        simpleActionList.Add(SimpleActionType.BOTTOM);
                        deltaY--;
                    }
                    doX = true;

                }

                if (deltaX != 0 && !doX && deltaY == 0)
                    doX = true;
                robot.x = this.applyTo.x;
                robot.y = this.applyTo.y;
            }
            return simpleActionList;
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
