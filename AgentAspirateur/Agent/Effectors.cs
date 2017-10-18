using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace AgentAspirateur.Agent
{
    static class Effectors
    {
        public static int SpeedFactor = 2;
        public static void executeAction(SimpleActionType action, Position robot)
        {
            Thread.Sleep(1000 / SpeedFactor);
            if (action != null)
            {
                switch (action)
                {
                    case SimpleActionType.PICK:
                        pickDiamond(robot.x, robot.y);
                        break;

                    case SimpleActionType.VACUUM:
                        vacuumDust(robot.x, robot.y);
                        break;
                    case SimpleActionType.TOP:
                        MainWindow.environment.addEvent("MOVE:NORTH");
                        break;
                    case SimpleActionType.BOTTOM:
                        MainWindow.environment.addEvent("MOVE:SOUTH");
                        break;
                    case SimpleActionType.LEFT:
                        MainWindow.environment.addEvent("MOVE:WEST");
                        break;
                    case SimpleActionType.RIGHT:
                        MainWindow.environment.addEvent("MOVE:EAST");
                        break;
                }
            }
        }
        public static void pickDiamond(int x, int y)
        {
            MainWindow.environment.addEvent("PICK");
            Console.WriteLine("I pick diamond at " + x + "," + y);
        }

        public static void vacuumDust(int x, int y)
        {
            MainWindow.environment.addEvent("VACUUM");
            Console.WriteLine("I vacuum dust at " + x + "," + y);
        }

        public static void PickAndVacuum(int x, int y)
        {
            pickDiamond(x, y);
            vacuumDust(x, y);
        }

    }
}