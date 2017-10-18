using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using AgentAspirateur.TreeSearch;
using AgentAspirateur.Agent;

namespace AgentAspirateur.Agent
{
    
    public class Agent
    {
         
        private Queue<SimpleActionType> intention;
        private State belief;
        private Environment environment;
        private Random rdm= new Random();
        public DustSensor dustSensors;   
        public DiamondSensor diamondSensor;
        Boolean Alive;
        private int speed = 20;


        //learning
        public int numberOfAction;
        private int lastPerformance;
        private readonly List<int> performances = new List<int>();
       
        private bool _deltaNbAction = false;
        private const double alpha = 1.5; 




        public Agent(Environment environment)
        {
            this.environment = environment;
            Init();
        }

        public void Init()
        {
            dustSensors = new DustSensor();
            diamondSensor = new DiamondSensor();
            Alive = true;
            numberOfAction = 5;           
            intention = new Queue<SimpleActionType>();

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
               think();
               act();                            
               learn();
               Thread.Sleep(1000 / speed);
                
            }


        }

        private void think()
        {
            if (goalCompleted())
            {
                intention.Clear();
                Thread.Sleep(1000);
            }
                

            if (intention.Count == 0)
            {
                updateBelief();
                Problem p = new Problem(belief);
                //Choisit une action
                Position robotIterationPos = new Position(this.belief.robotPos);
                foreach (Action action in TreeSearch(p, new Asearch(p)).ToList())
                {
                    foreach (SimpleActionType simpleAction in action.generateSimpleAction(robotIterationPos))
                    {
                        intention.Enqueue(simpleAction);
                    }

                        
                }
            }
            
        }

        private void act()
        {
            bool needsToStop = false;
            int countAction = 0;
            while (intention.Count != 0 && !needsToStop)
            {
                countAction++;
                if (countAction == numberOfAction)
                   {
                        needsToStop = true;
                   }
                   

                SimpleActionType a = intention.Dequeue();
                Effectors.executeAction(a, belief.robotPos);

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

        public State getBelief()
        {
            return belief;
        }

        private Boolean goalCompleted()
        {

            return belief.dustOrDiamondPos.Count() == 0;
        }        
      
        private void updateBelief()
        {
           
            belief = new State(MainWindow.environment.robot, MainWindow.environment.getMap());
        }

        private void learn()
        {
            int max = 100;
            if (performances.Count == max)
            {
                performances.RemoveAt(max - 1);
            }
            int currentPerf = this.environment.getPerformance();
            performances.Insert(0, currentPerf - lastPerformance);
            lastPerformance = currentPerf;

            double sum = 0;

            for (int i= 0; i< performances.Count; i++)            
            {               
                sum += (double)performances[i] / (alpha * Math.Exp((double)i));               
            }

            if (performances.Count == 1) { numberOfAction--; }

            else if (sum < 0 && Math.Abs(sum) > 1)
            {
                if (_deltaNbAction)
                    numberOfAction++; 
                else                
                   if (numberOfAction > 1) 
                        numberOfAction--;                  
                
            }


            else if (sum > 0 && Math.Abs(sum) > 1)
            {
                if (_deltaNbAction)
                {
                    if (numberOfAction > 1) {
                        numberOfAction--;
                        _deltaNbAction = false;
                    }
                }
                else
                {
                    numberOfAction++;
                    _deltaNbAction = true;
                }
            }          
            
        }
        
    }
}
