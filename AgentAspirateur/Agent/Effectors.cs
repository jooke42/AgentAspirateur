using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur.Agent
{
    static class Effectors
    {
        
        public static void executeAction(ActionType action, Position robot)
        {
            switch (action)
            {
                case ActionType.PICK:
                    pickDiamond(robot.x, robot.y);
                    break;

                case ActionType.VACUUM:
                    vacuumDust(robot.x, robot.y);
                    break;
                case ActionType.MOVE:
                case ActionType.SOUTH:
                case ActionType.EAST:
                case ActionType.WEST:
                    move(action,robot);
                    break;
            }
        }
        public static void move(ActionType movement,Position robot)
        {

            switch (movement)
            {
                case ActionType.NORTH:
                    Console.WriteLine("NORTH");
                    MainWindow.environment.addEvent("MOVE:NORTH");
                    break;
                case ActionType.SOUTH:
                    Console.WriteLine("SOUTH");
                    MainWindow.environment.addEvent("MOVE:SOUTH");
                    break;
                case ActionType.EAST:
                    Console.WriteLine("EAST");
                    MainWindow.environment.addEvent("MOVE:EAST");
                    break;
                case ActionType.WEST:
                    Console.WriteLine("WEST");
                    MainWindow.environment.addEvent("MOVE:WEST");
                    break;
            }
            
        }


        public static void pickDiamond(int x, int y)
        {
            Console.WriteLine("I pick diamond at " + x + "," + y);
        } 

        public static void vacuumDust(int x, int y)
        {
           
            Console.WriteLine("I vacuum dust at " + x + "," + y);
        }

        public static void PickAndVacuum(int x, int y)
        {
            pickDiamond(x, y);
            vacuumDust(x, y);
        }

    }
}
