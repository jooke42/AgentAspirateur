﻿using System;
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
        public int bestFreqExploration;
        public int randomFreqExploration;
        private int lastPerformance;
        private int lastRandomPerformance;    
        private static int measure = 3;
        private int leftToDo = 0;
        private int bestPerformance;
   



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
            bestFreqExploration = 10;           
            intention = new Queue<SimpleActionType>();
            uniformAlgo = true;
            randomFreqExploration = rdm.Next(1, 10);            
            bestPerformance = this.environment.getPerformance();
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
                    strategy = new Asearch();

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
                if (countAction == bestFreqExploration)
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
            List<Room> dust = dustSensors.getPosition(environment.getMap());
            List<Room> diamond = diamondSensor.getPosition(environment.getMap());
            foreach(Room r in dust)
            {
                list.Add(r);
            }
            foreach (Room r in diamond)
            {
                list.Add(r);
            }

            belief = new State(environment.robot, list.ToList());
        }

        //Apprentissage      
        private void learn()
        {
           
            bool changeFreq = false;            
                                
           if (leftToDo < measure)
                  addMeasure();
                    
           lastPerformance = environment.getPerformance();
           leftToDo++;



            if (leftToDo == measure)
            {
                //Random freq 
                if (bestPerformance < lastRandomPerformance)
                    updateFrequencyAndPerf(randomFreqExploration, lastRandomPerformance);

                else
                    changeFreq = true;


                if (changeFreq)
                    randomFreqExploration = rdm.Next(1, 10);

                leftToDo = 0;

            }

            
        }


        private void addMeasure()
        {          
            lastRandomPerformance += this.environment.getPerformance() - lastPerformance;
        }

        private void updateFrequencyAndPerf(int freq, int perf)
        {
            bestFreqExploration = freq;
            bestPerformance = perf;
        }
        

        //Nettoie la liste d'intention de l'agent
        public void clearIntention()
        {
            this.intention.Clear();
        }
        
    }
}
