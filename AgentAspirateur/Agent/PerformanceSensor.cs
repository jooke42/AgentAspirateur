using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentAspirateur.Agent
{
    public class PerformanceSensor
    {
        Environment environnement;
        public PerformanceSensor(Environment environnement)
        {
            this.environnement = environnement;

        }

        public double getPerformance()
        {
            return environnement.getPerformance();
        }
    }
}
