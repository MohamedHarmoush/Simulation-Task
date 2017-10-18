using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultiChannelQueueModels;
namespace Simulation_Task
{
    public partial class Results : Form
    {
        List<TimeDistribution> InterArrivalDirtribution;
        List<Server> Servers;
        Dictionary<int, bool> IdleOrBusy;
        SimualtionCase simulation;
        public Results(List<TimeDistribution> InterArrivalDirtribution,List<Server> Servers)
        {
            InitializeComponent();
            this.InterArrivalDirtribution = InterArrivalDirtribution;
            this.Servers = Servers;
            simulation = new SimualtionCase();
            IdleOrBusy = new Dictionary<int, bool>(Servers.Count);
        }

        private void Results_Load(object sender, EventArgs e)
        {
            DataTable Res = new DataTable();
            int NumberOfCustomers = 100;
            Enums.ServerSelectionMethod ssm = Enums.ServerSelectionMethod.HighestPriority;
            Enums.ServerStoppingCondition ssc = Enums.ServerStoppingCondition.NumberOfCustomers;
            simulation.createResultsTable(Res, Servers, InterArrivalDirtribution,ssm,ssc,NumberOfCustomers);
            ResultGV.DataSource = Res;
            
        }
        
        private void Results_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
