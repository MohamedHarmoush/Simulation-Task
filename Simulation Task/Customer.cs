using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation_Task
{
    class Customer
    {
        public int CustomerId { get; set; }
        public int ArrivalTime { get; set; }
        public Customer()
        {

        }
        public Customer(int id, int time)
        {
            this.CustomerId = id;
            this.ArrivalTime = time;
        }

    }
}
