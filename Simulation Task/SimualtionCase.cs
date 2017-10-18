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
        public int MaxQueueLength { get; set; }
        public Queue<Customer> CustomerQueue { get; set; }
        private Queue<int> CustomersIds { get; set; }
        private bool[] CustomerIsServed { get; set; }
        private Random rand;
        private int[] CustomerArrivalTimes { get; set; }
        
        /// <summary>
        /// list for customer waiting time because if we simulate for specific time we don't known how many customers will enter simulation.
        /// </summary>
        public List<int> CustomersWaitingTime { get; set; }
        public void createResultsTable(DataTable Res, List<Server> Servers, List<TimeDistribution> InterArrivalDirtribution, Enums.ServerSelectionMethod ssm, Enums.ServerStoppingCondition ssc, int NumberOfCustomers)
        {
            createTableColumns(Res, Servers);
            MaxQueueLength = 0;
            rand = new Random();
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
            
                Res.Columns.Add("Server Name", typeof(string));
                Res.Columns.Add("Server Time Service Begins", typeof(int));
                Res.Columns.Add("Server Service Time", typeof(int));
                Res.Columns.Add("Server Time Service End", typeof(int));

            
            Res.Columns.Add("Time In Queue", typeof(int));
        }
        private int RandomNumberGenerator(int min,int max)
        {
            
            return rand.Next(min,max);
        }
        private void makeSimulationCalc(DataTable table, List<Server> Servers, List<TimeDistribution> InterArrivalDistribution, Enums.ServerSelectionMethod ssm, Enums.ServerStoppingCondition ssc, int NumberOfCustomers)
        {
            int[] serverEndTime = new int[Servers.Count];
            CustomerIsServed = new bool[NumberOfCustomers];
            Customer customer = new Customer();
            for (int i = 0; i < Servers.Count; i++)
                serverEndTime[i] = 0;
            ////initialize CustomersWaiting Time.
            CustomersWaitingTime  = new List<int>(NumberOfCustomers);
            CustomerQueue = new  Queue<Customer>();
            CustomersIds = new Queue<int>();
            for(int i =0 ; i< NumberOfCustomers;i++)
                CustomersWaitingTime.Add(0);
            CustomerArrivalTimes = new int[NumberOfCustomers ];


            for (int i = 0; (i < NumberOfCustomers) || (CustomerQueue.Count > 0);i++)
            {
                //free 
                //CustomerQueue.Enqueue(i);
                //int serverId = -1;
                ///////get idle server and assign it to a customer
                ////if no servers avaliable serverId =-1
                int serverId = -1;
                
                if(i == 0)
                {
                    customer.CustomerId = 0;
                    customer.ArrivalTime = 0;
                    customer.RandomInterarrivalTime = 0;
                    customer.InterarrivalTime = 0;

                }else
                {
                    if(i < NumberOfCustomers)
                        customer = customerData(InterArrivalDistribution, table, i);
                }
                if(CustomerQueue.Count != 0 )
                {
                    do
                    {

                        Customer customerInQueue = CustomerQueue.Peek();
                        serverId = Selection(Servers, serverEndTime, customerInQueue.ArrivalTime, customerInQueue.CustomerId);
                        if (serverId != -1)
                        {
                            customerInQueue = CustomerQueue.Dequeue();
                            CustomersIds.Dequeue();
                            //serverId = Selection(Servers, serverEndTime, customerInQueue.ArrivalTime, customerInQueue.CustomerId);
                            AssignedServer = Servers[serverId];
                            serveCustomer(customerInQueue, table, InterArrivalDistribution, ref serverEndTime);
                           
                        }
                    } while (CustomerQueue.Count != 0 && serverId != -1);
                    if(CustomerQueue.Count == 0 && serverId != -1)
                    {
                        serverId = Selection(Servers, serverEndTime, customer.ArrivalTime,customer.CustomerId);
                        if(serverId !=-1)
                        {
                            AssignedServer = Servers[serverId];
                            serveCustomer(customer, table, InterArrivalDistribution, ref serverEndTime);
                        }
                        else
                        {
                            if (!CustomersIds.Contains(customer.CustomerId))
                            {
                                CustomerQueue.Enqueue(customer);
                                CustomersIds.Enqueue(customer.CustomerId);
                                updateWaitingTime();
                            }
                        }
                       
                    }
                    else if (CustomerQueue.Count != 0 && serverId == -1)
                    {
                        if (!CustomersIds.Contains(customer.CustomerId))
                        {
                            CustomerQueue.Enqueue(customer);
                            CustomersIds.Enqueue(customer.CustomerId);
                            updateWaitingTime();
                        }
                    }
                }else
                {
                    serverId = Selection(Servers, serverEndTime, customer.ArrivalTime,customer.CustomerId);
                    if (serverId != -1)
                    {
                        AssignedServer = Servers[serverId];
                        serveCustomer(customer, table, InterArrivalDistribution, ref serverEndTime);
                    }else
                    {
                        if (!CustomersIds.Contains(customer.CustomerId))
                        {
                            CustomerQueue.Enqueue(customer);
                            CustomersIds.Enqueue(customer.CustomerId);
                            updateWaitingTime();
                        }
                    }
                }
                if (MaxQueueLength < CustomerQueue.Count)
                    MaxQueueLength = CustomerQueue.Count;
                    

            }
        }
        private Customer customerData(List<TimeDistribution> InterArrivalDistribution, DataTable table, int customerId)
        {
            Customer customer = new Customer();
            RandomInterarrivalTime = RandomNumberGenerator(1, 100);
            InterarrivalTime = getTimeFromTimeDistribution(InterArrivalDistribution, RandomInterarrivalTime);
            ArrivalTime = CustomerArrivalTimes[customerId - 1] + InterarrivalTime;
           // ArrivalTime = int.Parse(table.Rows[customerId - 1][3].ToString()) + InterarrivalTime;
            CustomerArrivalTimes[customerId] = ArrivalTime;
            customer.CustomerId = customerId;
            customer.ArrivalTime = ArrivalTime;
            customer.InterarrivalTime = InterarrivalTime;
            customer.RandomInterarrivalTime = RandomInterarrivalTime;
            return customer;
        }
        private void updateWaitingTime()
        {
            for (int j = 0; j < CustomersWaitingTime.Count; j++)
            {
                
                if (CustomersIds.Contains(j))
                    CustomersWaitingTime[j]++;
            }
        }
        private void serveCustomer(Customer customer, DataTable table, List<TimeDistribution> intervalTimeDistribution,ref int []ServerEndTime)
        {
            DataRow dr = table.NewRow();
            CustomerNumber = customer.CustomerId +1;
            RandomServiceTime = RandomNumberGenerator(1, 100);
            ServiceTime = getTimeFromTimeDistribution(AssignedServer.ServiceTimeDistribution,RandomServiceTime);
            TimeServiceBegins = CustomerArrivalTimes[customer.CustomerId] + CustomersWaitingTime[customer.CustomerId];
            TimeServiceEnds = TimeServiceBegins + ServiceTime;
            WaitingTime = CustomersWaitingTime[customer.CustomerId];
            ServerEndTime[AssignedServer.ServerId] = TimeServiceEnds;
            dr[0] = CustomerNumber;
            dr[1] = customer.RandomInterarrivalTime;
            dr[2] = customer.InterarrivalTime;
            dr[3] = customer.ArrivalTime;
            dr[4] = RandomServiceTime;
            dr[5] = AssignedServer.Name;
            dr[6] = TimeServiceBegins;
            dr[7] = ServiceTime;
            dr[8] = TimeServiceEnds;
            dr[9] = CustomersWaitingTime[customer.CustomerId];
            CustomerIsServed[customer.CustomerId] = true;
            table.Rows.Add(dr);

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
        //// arrival time
        // idle represents endtime of each server and if server is idle the content equals 0
        private int Selection(List<Server> servers, int[] endTime, int arrivalTime, int customerId)
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
                if (arrivalTime + CustomersWaitingTime[customerId] >= endTime[i])
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
    }
}
