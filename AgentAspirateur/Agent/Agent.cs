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
        /* etat mental : BDI
         * Le desire étant une pièce sans poussière et diamand
         */
        private Queue<SimpleActionType> intention;
        private State belief;

        /*
         * Environnement de l'agent
         * avec ses capteurs et effecteurs
         
         */
        private Environment environment;       
        public Sensors dustSensors;   
        public Sensors diamondSensor;


        Boolean Alive;
        public bool uniformAlgo;
        private Random rdm = new Random();

        /*
         * Variable d'apprentissag         
         * 
         * */
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
            numberOfAction = 10;           
            intention = new Queue<SimpleActionType>();
            uniformAlgo = true;
        }
          

        public void Start()
        {
            CycleLife();
        }

        /*
         * Thread de l'agent
         * */

        private void CycleLife()
        {
            
            updateBelief();           

            while (Alive)
            {
               // Réfléchit et planifie ses actions
               think();

               //Execute ses actions
               act();         
                
               //Apprend pour améliorer sa performance
               learn();
              
            }


        }

        private void think()
        {
            /*            
             *   Si le manoir est propre, pas besoin de faire d'exploration          
             * */
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

                /*
                 * Changement de stratégie quand on clique sur le boutton switch strategy
                 * 
                 */
                SearchStrategy strategy;
                if (uniformAlgo)
                    strategy = new UniformCostSearch();
                else
                    strategy = new Asearch(p);

                /*
                 * Exploration et planification de ses intentions
                 * 
                 */
                foreach (Action action in TreeSearch(p, strategy).ToList())
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

            //Permet d'effectuer autant de mouvement que prévu par l'apprentissage
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

     
        private Stack<Action> TreeSearch(Problem p,SearchStrategy strategy)
        {
            /*
             * Arbre de recherche permet de savoir quoi faire
             * 
             */
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
            HashSet<Room> list = new HashSet<Room>();
            //Met à jour sa croyance sur sa position et sur l'état de l'environnement
            List<Room> dust = dustSensors.getPosition(MainWindow.environment.getMap());
            List<Room> diamond = diamondSensor.getPosition(MainWindow.environment.getMap());
            foreach(Room r in dust)
            {
                list.Add(r);
            }
            foreach (Room r in diamond)
            {
                list.Add(r);
            }

            belief = new State(MainWindow.environment.robot, list.ToList());
        }

        //Apprentissage
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

            else if (sum < 0 )
            {
                if (_deltaNbAction)
                    numberOfAction++; 
                else                
                   if (numberOfAction > 1) 
                        numberOfAction--;                  
            }


            else if (sum > 0 )
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

        //Nettoie la liste d'intention de l'agent
        public void clearIntention()
        {
            this.intention.Clear();
        }
        
    }
}
