using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Collections;


namespace AgentAspirateur
{
    public struct Size
    {
        public int width, height;
        public Size(int w,int h)
        {
            this.width = w;
            this.height = h;
        }
    }

    public class Environment
    {

       
        private Queue<string> events = new Queue<string>();
        public Position robot;
        private Room[][] map;
        Random rndDust = new Random();
        Random rndDiamond = new Random(9);
        Size sizeMap;
        private readonly System.Timers.Timer _timerDust;
        private readonly System.Timers.Timer _timerDiamond;
        private Performance performance;

        //Mesure de performance 

        private int malusDiamondVacuumed = 5;
        private const int newDust = 1;
        private const int newDiamond = 1;
        private const int electricity = 1;
        private int performanceScore = 0;
     


        public Environment()
        {
            init();
            _timerDust = new System.Timers.Timer(10000); //Updates every 2 sec
            _timerDust.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEventDust);
            _timerDiamond = new System.Timers.Timer(10000); //Updates every 2 sec
            _timerDust.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEventDiamond);
            performance = new Performance(this);
        }


        public void init()
        {
            this.sizeMap = new Size(10, 10);
            this.robot = new Position(5, 5);
            this.map = new Room[sizeMap.width][];
            for (int i = 0; i < map.Length; i++)
            {
                this.map[i] = new Room[sizeMap.height];
                for (int j = 0; j < map[i].Length; j++)
                {
                    map[i][j] = new Room(new Position(i, j));
                }
            }

        }

        private void OnTimedEventDust(object source, ElapsedEventArgs e)
        {

            generateRandomDust();

        }

        private void OnTimedEventDiamond(object source, ElapsedEventArgs e)
        {

            generateRandomDiamond();

        }
        public void start()
        {

        
            _timerDust.Start();
            _timerDiamond.Start();

            while (true)
            {
                Thread.Sleep(500);
                displayBelief();
                while (events.Count != 0)
                {
                   
                    string evt = (string) events.Dequeue();
                  /**
                         * PICK
                         * VACUUM
                         * MOVE:LEFT
                         */
                        string[] parsedEvent = evt.Split(':');
                        string action = parsedEvent[0];
                        string move;
                        Position newPos;
                        switch (action)
                        {
                            case "PICK":
                            if (map[robot.x][robot.y].getHasDiamond())
                            {
                                map[robot.x][robot.y].setHasDiamond(false);
                                performanceScore -= newDiamond;

                            }
                                
                               
                              
                            break;
                            case "VACUUM":
                            if (map[robot.x][robot.y].getHasDust())
                            {
                                map[robot.x][robot.y].setHasDust(false);                             
                                performanceScore -= newDust;
                                performanceScore += electricity;
                            }

                            if (map[robot.x][robot.y].getHasDiamond())
                            {
                                map[robot.x][robot.y].setHasDiamond(false);
                                performanceScore += malusDiamondVacuumed;
                                performanceScore += electricity;


                            }
                            
                            break;
                            case "MOVE":
                                move = parsedEvent[1];
                                newPos = this.robot.getRoomInDirection(DirectionMethod.directionFromString(move)).getCoordinate();
                                 if (newPos.validPosition(this.sizeMap.width, this.sizeMap.height))
                                 {
                                    robot = newPos;
                                    
                                 }
                           
                            break;
                        }
                    Thread.Sleep(500);

                    

                }
                
            }
        }

        private void displayBelief()
        {
            // Console.WriteLine("Position du robot env: " + this.robot.x + " " + this.robot.y);
          //  Console.WriteLine("Mesure de performance " + this.performance.getPerformance());

        }
        public void generateRandomDust()
        {
            int nbToCreate = rndDust.Next(1, 3);
            while (nbToCreate > 0)
            {
                int x = rndDust.Next(0, 10);
                int y = rndDust.Next(0, 10);
                if (!map[x][y].getHasDust())
                {
                    map[x][y].setHasDust(true);
                    nbToCreate--;
                    Console.WriteLine("generating dirt at (" + x + ";" + y + ")");
                    performanceScore += newDust;                   
                   
                }
            }
        }
        public void generateRandomDiamond()
        {
            int nbToCreate = rndDiamond.Next(1, 2);
            while(nbToCreate > 0)
            {
                int x = rndDiamond.Next(0, 10);
                int y = rndDiamond.Next(0, 10);
                if (!map[x][y].getHasDiamond()) 
                {
                    map[x][y].setHasDiamond(true);
                    nbToCreate--;
                    Console.WriteLine("generating Diamond at (" + x + ";" + y + ")");
                    performanceScore += newDiamond;
                  
                   
                }
            }
        }

        public Room[][] getMap()
        {
            return this.map;
        }
    


        public string toString()
        {
            string s = "";
            for (int i = 0; i < map.GetLength(0); i++)
            {
                s += "|";
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    s += "\t" + map[i][j].ToString() + "\t|";
                }
            }
            return s;
        }

       public int getPerformance()
        {
            return performanceScore;
        }

        public void addEvent(String _event)
        {
            if (!this.events.Contains(_event))
                this.events.Enqueue(_event);
        }


    }
}
