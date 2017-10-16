using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulation_Task
{
    public partial class Results : Form
    {
        List<TimeDistribution> InterArrivalDirtribution;
        List<Server> Servers;
        Dictionary<int, bool> IdleOrBusy;
        public Results(List<TimeDistribution> InterArrivalDirtribution,List<Server> Servers)
        {
            InitializeComponent();
            this.InterArrivalDirtribution = InterArrivalDirtribution;
            this.Servers = Servers;
            IdleOrBusy = new Dictionary<int, bool>(Servers.Count);
        }

        private void Results_Load(object sender, EventArgs e)
        {
            DataTable Res = new DataTable();
            Res.Columns.Add("Customer No.", typeof(int));
            Res.Columns.Add("Random Digits For Arrival", typeof(int));
            Res.Columns.Add("Time Between Arrival", typeof(int));
            Res.Columns.Add("Clock Time For Arrival", typeof(int));
            Res.Columns.Add("Rondom Digits For Service", typeof(int));

            int NumberOfServers = Servers.Count;
            for(int i=0;i<NumberOfServers;i++)
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
        private void Results_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
