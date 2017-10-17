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
        private Queue<Action> intention;
        private List<Tile>[][] belief;
        private List<Tile>[][] desire;
        private Random rdm= new Random();
        public DustSensor dustSensors;
        public DiamondSensor diamondSensor;
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
            intention = new Queue<Action>();
            //test
        }
          

        public void Start()
        {
            CycleLife();
        }

        private void CycleLife()
        {
            
            updateBelief();
            displayBelief();

            while (Alive)
            {

                if (goalCompleted())
                    intention.Clear();

                else
                {

                    //Met à jour son environnement 
                                    
                    if(intention.Count() == 0)
                     {
                        updateBelief();
                        Problem p = new Problem(new State(position, belief), desire);
                        //Choisit une action
                        foreach(Action a in TreeSearch(p, new UniformCostSearch()).ToList())
                        {
                            intention.Enqueue(a);
                        }
                      }

                    if(intention.Count != 0)
                    {
                        Action a = intention.Dequeue();
                        Effectors.executeAction(a, position);                       
                        position = a.applyTo;
                       

                    }
                        
                    Thread.Sleep(2000);
                    //Execute son action

                }

                
            }


        }


        private void displayBelief()
        {
            Console.WriteLine("Position du robot: " + this.position.x + " " + this.position.y);

        }
        private Stack<Action> TreeSearch(Problem p,SearchStrategy strategy)
        {
            Stack<Action> actionList = new Stack<Action>();
            Node endNode = strategy.SearchPath(p);

            while (endNode != null)
            {

                if (endNode.action != null)
                    actionList.Push(endNode.action);
                endNode = endNode.parentNode;
            }
           actionList.Reverse();
            return actionList;

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
