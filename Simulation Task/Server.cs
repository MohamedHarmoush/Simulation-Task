using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation_Task
{
    public class Server
    {
        public Server()
        {
            ServiceTimeDistribution = new List<TimeDistribution>();
        }
        public Server(string name , int id , List<TimeDistribution> times)
        {
            ServiceTimeDistribution = times;
            Name = name;
            ServerId = id;
        }
        public int ServerId { get; set; }

        public string Name { get; set; }

        public double ServiceEfficiency { get; set; }

        public List<TimeDistribution> ServiceTimeDistribution { get; set; }
    }
}
