using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace AgentAspirateur
{
    enum Action { HAUT,BAS,GAUCHE,DROITE,ASPIRER,RAMASSER}

    class Agent
    {
        int[,] beliefs;
        List<Action> intents = new List<Action>();
        int[,] desires;
        private Random rdm= new Random();

        public void addRandomMovingAction()
        {
            Action[] movements = { Action.HAUT, Action.BAS, Action.GAUCHE, Action.DROITE };
            intents.Add(movements[rdm.Next(4)]);
        }


       
    }
}
