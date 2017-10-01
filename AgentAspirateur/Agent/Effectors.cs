using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur.Agent
{
    static class Effectors
    {
        
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
