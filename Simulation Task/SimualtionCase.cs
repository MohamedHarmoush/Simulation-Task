using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiChannelQueueModels;
namespace Simulation_Task
{
    public class SimualtionCase
    {
        public int CustomerNumber { get; set; }

        public double RandomInterarrivalTime { get; set; }

        public int InterarrivalTime { get; set; }

        public int ArrivalTime { get; set; }

        public double RandomServiceTime { get; set; }

        public int ServiceTime { get; set; }

        public int TimeServiceBegins { get; set; }

        public int TimeServiceEnds { get; set; }

        public int WaitingTime { get; set; }

        public int DepartureTime { get; set; }

        public Server AssignedServer { get; set; }
        public Queue<int> CustomerQueue { get; set;}
        public void createResultsTable(DataTable Res, List<Server> Servers, List<TimeDistribution> InterArrivalDirtribution,Enums.ServerSelectionMethod ssm,Enums.ServerStoppingCondition ssc,int NumberOfCustomers)
        {
            //comment
            createTableColumns(Res, Servers);
            makeSimulationCalc(Res, Servers,InterArrivalDirtribution,ssm,ssc,NumberOfCustomers);
            

        }

        public void createTableColumns(DataTable Res, List<Server> Servers)
        {
            Res.Columns.Add("Customer No.", typeof(int));
            Res.Columns.Add("Random Digits For Arrival", typeof(int));
            Res.Columns.Add("Time Between Arrival", typeof(int));
            Res.Columns.Add("Clock Time For Arrival", typeof(int));
            Res.Columns.Add("Rondom Digits For Service", typeof(int));

            int NumberOfServers = Servers.Count;
            for (int i = 0; i < NumberOfServers; i++)
            {
                Res.Columns.Add("Server Name", typeof(string));
                Res.Columns.Add("Time Service Begins", typeof(int));
                Res.Columns.Add("Service Time", typeof(int));
                Res.Columns.Add("Time Service End", typeof(int));

            }
            Res.Columns.Add("Time In Queue", typeof(int));
        }
        private int RandomNumberGenerator()
        {
            Random rand = new Random();
            return rand.Next(0, 99);
        }
        private void makeSimulationCalc(DataTable table, List<Server> Servers, List<TimeDistribution> InterArrivalDirtribution,Enums.ServerSelectionMethod ssm,Enums.ServerStoppingCondition ssc,int NumberOfCustomers)
        {
            for(int i = 0; (i < NumberOfCustomers)||(CustomerQueue.Count > 0) ;i++)
            {
                CustomerQueue.Enqueue(i);
                int serverId = -1;
                /////get idle server and assign it to a customer
                //if no servers avaliable serverId =-1
                
                if(serverId !=-1)
                {

                }
                
            }
        }
        private int Selection(List<Server> servers, bool[] idle)
        {
            double[] TotServiceTime = new double[idle.Length];
            for (int i = 0; i < idle.Length; i++)
            {
                TotServiceTime[0] = servers[i].ServiceTimeDistribution[0].Time *
                                    servers[i].ServiceTimeDistribution[0].Probability
                                    +
                                    servers[i].ServiceTimeDistribution[1].Time *
                                    servers[i].ServiceTimeDistribution[1].Probability
                                    +
                                    servers[i].ServiceTimeDistribution[2].Time *
                                    servers[i].ServiceTimeDistribution[2].Probability
                                    +
                                    servers[i].ServiceTimeDistribution[3].Time *
                                    servers[i].ServiceTimeDistribution[3].Probability
                                    ;

            }
            double minTot = double.MaxValue;
            int idx = -1;
            for (int i = 0; i < idle.Length; i++)
            {
                if (!idle[i])
                {
                    if (TotServiceTime[i] > minTot)
                    {
                        idx = i;
                        minTot = TotServiceTime[i];
                    }
                }
            }
            return idx;
        }
    }
}
