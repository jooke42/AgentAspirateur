﻿using System;
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
                case Action.NORTH:
                case Action.SOUTH:
                case Action.EAST:
                case Action.WEST:
                    move(action,robot);
                    break;
            }
        }
        public static void move(Action movement,Position robot)
        {

            switch (movement)
            {
                case Action.NORTH:
                    Console.WriteLine("NORTH");
                    MainWindow.environment.addEvent("MOVE:NORTH");
                    break;
                case Action.SOUTH:
                    Console.WriteLine("SOUTH");
                    MainWindow.environment.addEvent("MOVE:SOUTH");
                    break;
                case Action.EAST:
                    Console.WriteLine("EAST");
                    MainWindow.environment.addEvent("MOVE:EAST");
                    break;
                case Action.WEST:
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
