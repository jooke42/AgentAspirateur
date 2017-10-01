using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace AgentAspirateur
{
    enum Tile { FLOOR, DUST, DIAMOND, ROBOT }

    public struct Size
    {
        public int width, height;
    }

    class Environment
    {
        private List<Tile>[][] map;
        Random rnd = new Random();

        public Environment()
        {
            init();
        }

        public void init()
        {
            this.map = new List<Tile>[10][];
            for (int i = 0; i < map.Length; i++)
            {
                this.map[i] = new List<Tile>[10];
                for (int j = 0; j < map[i].Length; j++)
                {
                    map[i][j] = new List<Tile>() { Tile.FLOOR };
                }
            }

        }
        public void start()
        {
            while (true)
            {
                Thread.Sleep(2000);
                generateRandom(Tile.DUST, 0, 2);
                
            }
        }
        public void generateRandom(Tile tile,int min, int max)
        {
            int nbToCreate = rnd.Next(min, max);
            while(nbToCreate > 0)
            {
                int x = rnd.Next(0, 10);
                int y = rnd.Next(0, 10);
                if (!map[x][y].Contains(tile)) 
                {
                    map[x][y].Add(tile);
                    nbToCreate--;
                    Console.WriteLine("generating dirt at (" + x + ";" + y + ")");
                }
            }
        }

        public List<Tile>[][] getMap()
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


    }
}
