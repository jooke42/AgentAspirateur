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
        private State belief;
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
                updateBelief();
                Thread.Sleep(500);
                if (goalCompleted())
                    intention.Clear();

                else
                {

                    //Update Environment
                                    
                    if(intention.Count() == 0) { 
                        
                        Problem p = new Problem(belief);
                        //Choisit une action
                        foreach(Action a in TreeSearch(p, new Asearch(p)).ToList())
                        {
                            intention.Enqueue(a);
                        }
                      }

                    if(intention.Count != 0)
                    {
                        Action a = intention.Dequeue();
                        Effectors.executeAction(a, belief.robotPos);                        
                        belief.robotPos = a.applyTo;                      

                    }
                        
                    Thread.Sleep(1000);
                    //Execute son action

                }

                
            }


        }


        private void displayBelief()
        {
            Console.WriteLine("Position du robot: " + this.belief.robotPos.x + " " + this.belief.robotPos.y);

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

        
    }
}
