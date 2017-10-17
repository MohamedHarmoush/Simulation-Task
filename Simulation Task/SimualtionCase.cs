﻿using System;
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
        public int MaxQueueLength { get; set; }
        public Queue<int> CustomerQueue { get; set; }
        /// <summary>
        /// list for customer waiting time because if we simulate for specific time we don't known how many customers will enter simulation.
        /// </summary>
        public List<int> CustomersWaitingTime { get; set; }
        public void createResultsTable(DataTable Res, List<Server> Servers, List<TimeDistribution> InterArrivalDirtribution, Enums.ServerSelectionMethod ssm, Enums.ServerStoppingCondition ssc, int NumberOfCustomers)
        {
            //comment
            //comment tani
            createTableColumns(Res, Servers);
            MaxQueueLength = -1;
            makeSimulationCalc(Res, Servers, InterArrivalDirtribution, ssm, ssc, NumberOfCustomers);


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
        private int RandomNumberGenerator(int min,int max)
        {
            Random rand = new Random();
            return rand.Next(min,max);
        }
        private void makeSimulationCalc(DataTable table, List<Server> Servers, List<TimeDistribution> InterArrivalDirtribution, Enums.ServerSelectionMethod ssm, Enums.ServerStoppingCondition ssc, int NumberOfCustomers)
        {
            int[] serverEndTime = new int[Servers.Count];
            for (int i = 0; i < Servers.Count; i++)
                serverEndTime[i] = 0;
            ////initialize CustomersWaiting Time.
            CustomersWaitingTime  = new List<int>(NumberOfCustomers);
            for(int i =0 ; i< NumberOfCustomers;i++)
                CustomersWaitingTime.Add(0);
            for (int i = 0; (i < NumberOfCustomers) || (CustomerQueue.Count > 0); i++)
            {
                //free 
                //CustomerQueue.Enqueue(i);
                //int serverId = -1;
                ///////get idle server and assign it to a customer
                ////if no servers avaliable serverId =-1
                int serverId;
                int customerId ;
                if(i == 0)
                    ArrivalTime = 0;
                serverId = Selection(Servers, serverEndTime);
                if (serverId != -1)
                {
                    while (CustomerQueue.Count != 0 && serverId != -1)
                    {
                        customerId = CustomerQueue.Dequeue();
                        AssignedServer = Servers[serverId];
                        serveCustomer(customerId, table, InterArrivalDirtribution,ref serverEndTime);
                        serverId = Selection(Servers, serverEndTime);
                    }
                    /// if queue empty and there's avaliable servers
                    if(serverId != -1)
                    {
                        customerId = i;
                        AssignedServer = Servers[serverId];
                        serveCustomer(customerId, table, InterArrivalDirtribution, ref serverEndTime);
                    }
                    //// if queue not empty and there isn't avaliable servers
                    else if (serverId == -1 && CustomerQueue.Count != 0)
                    {
                        CustomerQueue.Enqueue(i);
                        updateWaitingTime();
                            
                        
                    }
                }else
                {
                    CustomerQueue.Enqueue(i);
                    updateWaitingTime();
                }

                if (MaxQueueLength < CustomerQueue.Count)
                    MaxQueueLength = CustomerQueue.Count;


            }
        }
        private void updateWaitingTime()
        {
            for (int j = 0; j < CustomersWaitingTime.Count; j++)
            {
                if (CustomerQueue.Contains(j))
                    CustomersWaitingTime[j]++;
            }
        }
        private void serveCustomer(int customerId, DataTable table, List<TimeDistribution> intervalTimeDistribution,ref int []ServerEndTime)
        {
            DataRow dr = table.NewRow();
            CustomerNumber = customerId + 1;
            if(customerId == 0)
            {
                RandomInterarrivalTime = 0;
                InterarrivalTime = 0;
                ArrivalTime = 0;
            }
            else
            {
                int previousCusomer = customerId -1;
                RandomInterarrivalTime = RandomNumberGenerator(1, 100);
                InterarrivalTime = getTimeFromTimeDistribution(intervalTimeDistribution,RandomInterarrivalTime);
                ArrivalTime = int.Parse(table.Rows[previousCusomer][3].ToString()) + InterarrivalTime;
            }
            RandomServiceTime = RandomNumberGenerator(1, 100);
            ServiceTime = getTimeFromTimeDistribution(AssignedServer.ServiceTimeDistribution,RandomServiceTime);
            TimeServiceBegins = ArrivalTime + CustomersWaitingTime[customerId];
            TimeServiceEnds = TimeServiceBegins + ServiceTime;
            WaitingTime = CustomersWaitingTime[customerId];
            ServerEndTime[AssignedServer.ServerId] = TimeServiceEnds;

          //  return TimeServiceEnds;
        }
        private int getTimeFromTimeDistribution(List<TimeDistribution> timeDistribution, double RandomTime)
        {
            int time = 0;
            for (int i = 0; i < timeDistribution.Count;i++)
            {
                double min = timeDistribution[i].MinRange;
                double max = timeDistribution[i].MaxRange;
                if (min <= RandomTime && max >= RandomTime)
                    time = timeDistribution[i].Time;
            }
            return time;
        }
        /// <summary>
        /// Error ,check again
        /// </summary>
        /// <param name="EndTime"></param>
        /// <param name="CustomerArrivalTime"></param>
        private void Free_Servers(ref int[] EndTime, int CustomerArrivalTime)
        {
            for (int i = 0; i < EndTime.Length; i++)
            {
                if (EndTime[i] <= CustomerArrivalTime) 
                    EndTime[i] = CustomerArrivalTime + ServiceTime;
            }
        }
        // idle represents endtime of each server and if server is idle the content equals 0
        private int Selection(List<Server> servers, int[] endTime)
        {
            double[] TotServiceTime = new double[endTime.Length];
            for (int i = 0; i < endTime.Length; i++)
            {
                TotServiceTime[i] = servers[i].ServiceTimeDistribution[0].Time *
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
            for (int i = 0; i < endTime.Length; i++)
            {
                if (endTime[i] <= ArrivalTime)
                {
                    if (TotServiceTime[i] < minTot)
                    {
                        idx = i;
                        minTot = TotServiceTime[i];
                    }
                }
            }
            return idx;
        }
     /*   private int Selection(List<Server> servers, int[] idle)
        {
            double[] TotServiceTime = new double[idle.Length];
            for (int i = 0; i < idle.Length; i++)
            {
                TotServiceTime[i] = servers[i].ServiceTimeDistribution[0].Time *
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
                if (idle[i] == 0)
                {
                    if (TotServiceTime[i] < minTot)
                    {
                        idx = i;
                        minTot = TotServiceTime[i];
                    }
                }
            }
            return idx;
        }*/
    }
}
