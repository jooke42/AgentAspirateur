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
            if (action != null)
            {
                switch (action.actionType)
                {
                    case ActionType.PICK:
                        move(action, robot);
                        pickDiamond(robot.x, robot.y);
                        break;

                    case ActionType.VACUUM:
                        move(action, robot);
                        vacuumDust(robot.x, robot.y);
                        break;
                }
            }
        }
        public static void move(Action a, Position robot)
        {

            int deltaX, deltaY;
            deltaX = a.applyTo.x - robot.x;
            deltaY = a.applyTo.y- robot.y;
            bool doX = true;
            while(deltaX !=0 || deltaY != 0)
            {
                if(deltaX != 0 && doX)
                {
                    if(deltaX > 0)
                    {
                       // Console.WriteLine("EAST");
                        MainWindow.environment.addEvent("MOVE:EAST");
                        deltaX--;
                    }
                    else
                    {
                       // Console.WriteLine("WEST");
                        MainWindow.environment.addEvent("MOVE:WEST");
                        deltaX++;
                    }
                    
                    doX = false;
                }
                else if(deltaY != 0)
                {
                    if (deltaY < 0)
                    {
                        //Console.WriteLine("NORTH");
                        MainWindow.environment.addEvent("MOVE:NORTH");
                        deltaY++;
                    }
                    else
                    {
                       // Console.WriteLine("SOUTH");
                        MainWindow.environment.addEvent("MOVE:SOUTH");
                        deltaY--;
                    }
                    doX = true;
                    
                }

                if (deltaX != 0 && !doX && deltaY == 0)
                    doX = true;

            }       
            
        }


        public static void pickDiamond(int x, int y)
        {
            MainWindow.environment.addEvent("PICK");
          //  Console.WriteLine("I pick diamond at " + x + "," + y);
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
