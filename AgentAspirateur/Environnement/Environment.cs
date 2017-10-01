using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace AgentAspirateur
{
    enum Tile { CLEAN_FLOOR,DIRTY_FLOOR, JEWELRY_FLOOR}

    public struct Size
    {
        public int width, height;
    }

    class Environment
    {
        private Tile[,] map;
        int[] robotPosition;
        Random rnd = new Random();

        public Environment()
        {
            init();
        }

        public void init()
        {
            this.map = new Tile[10,10];
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = Tile.CLEAN_FLOOR;
                }
            }

        }
        public void start()
        {
            while (true)
            {
                Thread.Sleep(2000);
                generateRandom(Tile.DIRTY_FLOOR, 0, 2);
                Thread.Sleep(2000);
                generateRandom(Tile.JEWELRY_FLOOR, 0, 2);

            }
        }
        public void generateRandom(Tile tile,int min, int max)
        {
            int nbToCreate = rnd.Next(min, max);
            while(nbToCreate > 0)
            {
                int x = rnd.Next(0, 10);
                int y = rnd.Next(0, 10);
                if(map[x,y] == Tile.CLEAN_FLOOR)
                {
                    map[x, y] = tile;
                    nbToCreate--;
                    Console.WriteLine("generating dirt at (" + x + ";" + y + ")");
                }
            }
        }

        public Tile[,] getMap()
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
                    s += "\t" + map[i, j] + "\t|";
                }
            }
            return s;
        }


    }
}
