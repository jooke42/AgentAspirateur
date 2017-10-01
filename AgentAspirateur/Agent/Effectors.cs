using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur.Agent
{
    static class Effectors
    {
        
        public static void executeAction(Action action, Position robot)
        {
            switch (action)
            {
                case Action.PICK:
                    pickDiamond(robot.x, robot.y);
                    break;

                case Action.VACUUM:
                    vacuumDust(robot.x, robot.y);
                    break;
                case Action.UP:
                case Action.DOWN:
                case Action.RIGHT:
                case Action.LEFT:
                    move(action,robot);
                    break;
            }
        }
        public static void move(Action movement,Position robot)
        {

            switch (movement)
            {
                case Action.UP:
                    Console.WriteLine("UP");
                    break;
                case Action.DOWN:
                    Console.WriteLine("DOWN");
                    break;
                case Action.RIGHT:
                    Console.WriteLine("RIGHT");
                    break;
                case Action.LEFT:
                    Console.WriteLine("LEFT");
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
