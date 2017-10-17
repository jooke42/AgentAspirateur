using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;

namespace AgentAspirateur
{
    public enum Tile { FLOOR, DUST, DIAMOND }

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
        private List<Tile>[][] map;
        Random rndDust = new Random();
        Random rndDiamond = new Random(9);
        Size sizeMap;
        private readonly System.Timers.Timer _timerDust;
        private readonly System.Timers.Timer _timerDiamond;
        private Performance performance;
       


        public Environment()
        {
            init();
            _timerDust = new System.Timers.Timer(5000); //Updates every 2 sec
            _timerDust.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEventDust);

            _timerDiamond = new System.Timers.Timer(7000); //Updates every 2 sec
            _timerDust.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEventDiamond);

           

            performance = new Performance(this);
        }


        public void init()
        {
            this.sizeMap = new Size(10, 10);
            this.robot = new Position(5, 7);
            this.map = new List<Tile>[sizeMap.width][];
            for (int i = 0; i < map.Length; i++)
            {
                this.map[i] = new List<Tile>[sizeMap.height];
                for (int j = 0; j < map[i].Length; j++)
                {
                    map[i][j] = new List<Tile>() { Tile.FLOOR };
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
            map[2][2].Add(Tile.DUST);
            map[2][1].Add(Tile.DUST);
            map[2][3].Add(Tile.DUST);


            map[8][1].Add(Tile.DUST);

            map[5][2].Add(Tile.DUST);
               _timerDust.Start();
            //_timerDiamond.Start();

            while (true)
            {
                Thread.Sleep(500);

                

                displayBelief();
                while (events.Count != 0)
                {
                   
                    string evt = events.Dequeue();
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
                                map[robot.x][robot.y].Remove(Tile.DIAMOND);
                                performance.addAction("PICK");
                              
                            break;
                            case "VACUUM":
                            if (map[robot.x][robot.y].Contains(Tile.DUST))
                            {
                                map[robot.x][robot.y].Remove(Tile.DUST);
                                performance.addAction("VACUUM DUST");
                               
                            }

                            if (map[robot.x][robot.y].Contains(Tile.DIAMOND))
                            {
                                map[robot.x][robot.y].Remove(Tile.DIAMOND);
                                performance.addAction("VACUUM DIAMOND");
                              
                            }
                            

                            break;
                            case "MOVE":
                                move = parsedEvent[1];
                                newPos = this.robot.getPositionInDirection(DirectionMethod.directionFromString(move));
                                if (newPos.validPosition(this.sizeMap.width, this.sizeMap.height))
                                    robot = newPos;
                                break;
                        }
                    //Thread.Sleep(500);
                    
                }
                
            }
        }

        private void displayBelief()
        {
            // Console.WriteLine("Position du robot env: " + this.robot.x + " " + this.robot.y);
            Console.WriteLine("Mesure de performance" + this.performance.getPerformance());

        }
        public void generateRandomDust()
        {
            int nbToCreate = rndDust.Next(0, 3);
            while (nbToCreate > 0)
            {
                int x = rndDust.Next(0, 10);
                int y = rndDust.Next(0, 10);
                if (!map[x][y].Contains(Tile.DUST))
                {
                    map[x][y].Add(Tile.DUST);
                    nbToCreate--;
                    Console.WriteLine("generating dirt at (" + x + ";" + y + ")");
                    performance.addAction("ADD DUST");
                   
                }
            }
        }
        public void generateRandomDiamond()
        {
            int nbToCreate = rndDiamond.Next(0, 2);
            while(nbToCreate > 0)
            {
                int x = rndDiamond.Next(0, 10);
                int y = rndDiamond.Next(0, 10);
                if (!map[x][y].Contains(Tile.DIAMOND)) 
                {
                    map[x][y].Add(Tile.DIAMOND);
                    nbToCreate--;
                    Console.WriteLine("generating Diamond at (" + x + ";" + y + ")");
                    performance.addAction("ADD DIAMOND");
                   
                }
            }
        }

        public List<Tile>[][] getMap()
        {
            return this.map;
        }

        public void addEvent(String _event)
        {
            if(!this.events.Contains(_event))
                this.events.Enqueue(_event);
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

       


    }
}
