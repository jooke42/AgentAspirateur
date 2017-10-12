using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using AgentAspirateur.TreeSearch;

namespace AgentAspirateur.Agent
{
    
    public class Agent
    {
        private Position position;
        private Queue<ActionType> intention;
        private List<Tile>[][] belief;
        private List<Tile>[][] desire;
        private Random rdm= new Random();
        public DustSensor dustSensors;
        public DiamondSensor diamondSensor;
        private SearchStrategy searchLogic = new IterativeDeepeningSearch();
        Boolean Alive;

       

        public Agent()
        {
            Init();
        }

        public void Init()
        {
            dustSensors = new DustSensor();
            diamondSensor = new DiamondSensor();
            Alive = true;
            this.desire = new List<Tile>[10][];
            for (int i = 0; i < desire.Length; i++)
            {
                this.desire[i] = new List<Tile>[10];
                for (int j = 0; j < desire[i].Length; j++)
                {
                    desire[i][j] = new List<Tile>() { Tile.FLOOR };
                }
            }
            intention = new Queue<ActionType>();

        }
          

        public void Start()
        {
            CycleLife();
        }

        private void CycleLife()
        {
            
            updateBelief();

            while (Alive)
            {

                if (goalCompleted())
                    intention.Clear();

                else
                {

                    //Met à jour son environnement 
                    updateBelief();

                    
                    //Choisit une action
                    searchLogic.SearchPath(g.nodes[position.x][position.y],g);

                    intention.Enqueue();
                    //Execute son action
                    Effectors.executeAction(intention.Dequeue(), position);
                }

                Thread.Sleep(2000);
            }


        }
        private Queue<ActionType> TreeSearch(Problem p,SearchStrategy strategy)
        {

        }

        private Boolean goalCompleted()
        {
            for (int i = 0; i < desire.Length; i++)
            {
                for (int j = 0; j < desire[i].Length; j++)
                {
                    if (!mapsEquals(desire[i][j], belief[i][j]))
                        return false;
                }
            }
            return true;
        }
        
        private bool mapsEquals(List<Tile> m1, List<Tile> m2)
        {
            foreach (Tile t in m1)
            {
                if (!m2.Contains(t))
                    return false;
            }
            foreach (Tile t in m2)
            {
                if (!m1.Contains(t))
                    return false;
            }
            return true;
        }
        
        private void updateBelief()
        {
            belief = MainWindow.environment.getMap();
            position = new Position(MainWindow.environment.robot);
        }

        
    }
}
