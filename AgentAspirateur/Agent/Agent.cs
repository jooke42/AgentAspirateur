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
        private Queue<Action> intention;
        
        private State belief;
        private Environment environment;
        private Random rdm= new Random();
        public DustSensor dustSensors;
        private PerformanceSensor performanceSensor;
        public DiamondSensor diamondSensor;
        Boolean Alive;
        private int speed = 20;


        //learning
        private int numberOfAction;
        private int lastPerformance;
        private readonly List<int> _deltaPerformances = new List<int>();
        private const int TailleListePerf = 100;
        private bool _deltaNbAction = false;
        private const double Alpha = 1.5; //facteur de non prise en compte des anciens deltaPerf




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
            intention = new Queue<Action>();
            numberOfAction = 10;
            
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
               // displayBelief();

                if (goalCompleted())
                    intention.Clear();
                  
                else
                {
                    think();
                    act();                            
                    learn();
                    Thread.Sleep(1000 / speed);                    
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
            bool needsToStop = false;
            int countAction = 0;
            while (intention.Count != 0 && !needsToStop)
            {
                countAction++;
                if (countAction == numberOfAction)
                    needsToStop = true;

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

        private void learn()
        {
           
            int perf = this.environment.getPerformance();
             _deltaPerformances.Insert(0, perf - lastPerformance);
            lastPerformance = perf;
            double sum = 0;
            foreach (int i in _deltaPerformances)
            {               
                sum += (double)i / (Alpha * Math.Exp((double)i));               
            }

            if (_deltaPerformances.Count == 1) { numberOfAction--; }

            else if (sum < 0 )
            {
                if (_deltaNbAction) { numberOfAction++; }
                else
                {
                    if (numberOfAction > 1) { numberOfAction--; }
                }
            }


            else if (sum > 0 )
            {
                if (_deltaNbAction)
                {
                    if (numberOfAction > 1) { numberOfAction--; _deltaNbAction = false; }
                }
                else { numberOfAction++; _deltaNbAction = true; }
            }
        }
        
    }
}
