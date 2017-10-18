﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur
{
    class Performance
    {
        private  static double alpha = 3;
        private  static double beta = 5;
        private static int VacuumDiamondPenalty = 5;
        private static double costElectricity = 1;      
     
        private int dust = 0;
        private int dustVacuumed = 0;
        private int diamond = 0;
        private int diamondPicked = 0;
        private int diamondVacuumed = 0;
        private int numberOfAction = 0;
        private int electricityConsummed = 0;

        private double performanceScore = 0;

        private Environment environnement;
        public Performance(Environment environnement)
        {
            this.environnement = environnement;
        }
                      
        public void addAction(String action)
        {           
           
            switch (action)
            {
                case "ADD DIAMOND":                    
                    diamond++;
                    break;

                case "ADD DUST":
                    dust++;
                    break;

                case "PICK DIAMOND":
                    diamondPicked++;
                    numberOfAction++;
                    electricityConsummed++;
                    break;

                case "VACUUM DUST":
                    dustVacuumed++;
                    numberOfAction++;
                    electricityConsummed++;
                    break;

                case "VACUUM DIAMOND":
                    diamondVacuumed++;
                    numberOfAction++;
                    electricityConsummed++;
                    break;

                case "MOVE":
                    electricityConsummed++;
                    break;



            }
            computePerformance();
        }

        public double getPerformance()
        {
            return performanceScore;
        }


        private void computePerformance()
        {

            int dustLeft = dust - dustVacuumed;
            int diamondLeft = diamond - diamondPicked - diamondVacuumed;
            int vacuumedandPicked = dustVacuumed + diamondPicked;
          
            if(electricityConsummed != 0)
                 performanceScore = ((dustVacuumed + diamondPicked) / electricityConsummed) - computeMalus();            
            
        }

        private double computeMalus()
        {
            return diamondVacuumed * VacuumDiamondPenalty;
        }

        private double computeActionCost()
        {
            return numberOfAction * costElectricity;
        }

        
    }
}