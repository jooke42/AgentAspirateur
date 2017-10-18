using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using AgentAspirateur.TreeSearch;
using AgentAspirateur.Environnement;
using AgentAspirateur.Agent;

namespace AgentAspirateur.Agent
{
    
    public class Agent
    {
        private Queue<Action> intention;
        private State belief;
        private Environnement.Environment environment;
        private Random rdm= new Random();
        public DustSensor dustSensors;
        private PerformanceSensor performanceSensor;
        public DiamondSensor diamondSensor;
        Boolean Alive;
        private int speed = 20;



        public Agent(Environnement.Environment environment)
        {
            this.environment = environment;
            Init();
        }

        public void Init()
        {
            dustSensors = new DustSensor();
            diamondSensor = new DiamondSensor();
            Alive = true;
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
           

            while (Alive)
            {

                updateBelief();
                displayBelief();

                if (goalCompleted())
                    intention.Clear();
                  
                else
                {

                    think();
                    act();                            
                    learning();
                    Thread.Sleep(1000 / speed);
                    // Thread.Sleep(1000);
                    //Execute son action

                }

                
            }


        }

        private void think()
        {
            if (intention.Count == 0)
            {
                Problem p = new Problem(belief);
                //Choisit une action
                foreach (Action action in TreeSearch(p, new UniformCostSearch()).ToList())
                {
                    
                    intention.Enqueue(action);
                }
            }
            
        }

        private void act()
        {
            if (intention.Count != 0)
            {
                Action a = intention.Dequeue();
                Effectors.executeAction(a, belief.robotPos);                
                belief.robotPos = a.applyTo;
            }
        }


        private void displayBelief()
        {
            Console.WriteLine("Belief du robot: " + this.belief.robotPos.x + " " + this.belief.robotPos.y);
           // belief.dustOrDiamondPos.ToString();

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

            return belief.dustOrDiamondPos.Count() == 0;
        }        
      
        private void updateBelief()
        {
           
            belief = new State(MainWindow.environment.robot, MainWindow.environment.getMap());
        }

        private void learning()
        {
           
        }
        
    }
}
