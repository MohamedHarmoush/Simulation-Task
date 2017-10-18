using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation_Task
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public int ArrivalTime { get; set; }
        public double RandomInterarrivalTime { get; set; }
        public int InterarrivalTime { get; set; }
        public Customer()
        {

        }
        public Customer(int id, int arrivalTime, double randomIntervalTime, int interarrivalTime)
        {
            this.CustomerId = id;
            this.ArrivalTime = arrivalTime;
            this.RandomInterarrivalTime = randomIntervalTime;
            this.InterarrivalTime = interarrivalTime;
        }

    }
}
