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
    public partial class Form1 : Form
    {
        List<TimeDistribution> InterArrivalDistribution;
        int NoFServers;
        List<Server> Servers;

        public Form1()
        {
            InitializeComponent();
            InterArrivalDistribution = new List<TimeDistribution>(4);
            Servers = new List<Server>(0);
        }

        private void UpdateInputGV(object sender, EventArgs e)
        {

        }
        int Count = 0;
        private void button1_Click(object sender, EventArgs e)
        {

            if (Count == 0) NoFServers = int.Parse(NofServers.Text);
            HeaderLabel.Text = "Enter Server " + (Count + 1).ToString() + " Time Distribution.";
            string ServerName = NameTB.Text.ToString();
            List<TimeDistribution> ServerServiceTime = new List<TimeDistribution>(4);
            for (int j = 0; j < 4; j++)
            {
                ServerServiceTime.Add(new TimeDistribution());
            }

            ServerServiceTime[0].Time = int.Parse(T1.Text.ToString());
            ServerServiceTime[1].Time = int.Parse(T2.Text.ToString());
            ServerServiceTime[2].Time = int.Parse(T3.Text.ToString());
            ServerServiceTime[3].Time = int.Parse(T4.Text.ToString());

            ServerServiceTime[0].Probability = double.Parse(P1.Text.ToString());
            ServerServiceTime[1].Probability = double.Parse(P2.Text.ToString());
            ServerServiceTime[2].Probability = double.Parse(P3.Text.ToString());
            ServerServiceTime[3].Probability = double.Parse(P4.Text.ToString());

            ServerServiceTime[0].CummProbability = ServerServiceTime[0].Probability;
            ServerServiceTime[1].CummProbability = ServerServiceTime[1].Probability + ServerServiceTime[0].CummProbability;
            ServerServiceTime[2].CummProbability = ServerServiceTime[2].Probability + ServerServiceTime[1].CummProbability;
            ServerServiceTime[3].CummProbability = ServerServiceTime[3].Probability + ServerServiceTime[2].CummProbability;

            ServerServiceTime[0].MinRange = 1;
            ServerServiceTime[0].MaxRange = ServerServiceTime[0].CummProbability * 100;

            ServerServiceTime[1].MinRange = ServerServiceTime[0].MaxRange + 1;
            ServerServiceTime[1].MaxRange = ServerServiceTime[1].CummProbability * 100;


            ServerServiceTime[2].MinRange = ServerServiceTime[1].MaxRange + 1;
            ServerServiceTime[2].MaxRange = ServerServiceTime[2].CummProbability * 100;

            ServerServiceTime[3].MinRange = ServerServiceTime[2].MaxRange + 1;
            ServerServiceTime[3].MaxRange = ServerServiceTime[3].CummProbability * 100 - 1;

            Servers.Add(new Server(ServerName, Count, ServerServiceTime));
            T1.Text = "";
            T2.Text = "";
            T3.Text = "";
            T4.Text = "";
            P1.Text = "";
            P2.Text = "";
            P3.Text = "";
            P4.Text = "";
            BtnSimulate.Text = "Save Server No. " + (Count + 1).ToString();



            Count++;

            if (Count == NoFServers)
            {
                this.Visible = false;
                Results Res = new Results(InterArrivalDistribution, Servers);
                Res.Show();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NameLabel.Visible = false;
            NameTB.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                InterArrivalDistribution.Add(new TimeDistribution());
            }
            InterArrivalDistribution[0].Time = int.Parse(T1.Text.ToString());
            InterArrivalDistribution[1].Time = int.Parse(T2.Text.ToString());
            InterArrivalDistribution[2].Time = int.Parse(T3.Text.ToString());
            InterArrivalDistribution[3].Time = int.Parse(T4.Text.ToString());

            InterArrivalDistribution[0].Probability = double.Parse(P1.Text.ToString());
            InterArrivalDistribution[1].Probability = double.Parse(P2.Text.ToString());
            InterArrivalDistribution[2].Probability = double.Parse(P3.Text.ToString());
            InterArrivalDistribution[3].Probability = double.Parse(P4.Text.ToString());

            InterArrivalDistribution[0].CummProbability = InterArrivalDistribution[0].Probability;
            InterArrivalDistribution[1].CummProbability = InterArrivalDistribution[1].Probability + InterArrivalDistribution[0].CummProbability;
            InterArrivalDistribution[2].CummProbability = InterArrivalDistribution[2].Probability + InterArrivalDistribution[1].CummProbability;
            InterArrivalDistribution[3].CummProbability = InterArrivalDistribution[3].Probability + InterArrivalDistribution[2].CummProbability;

            InterArrivalDistribution[0].MinRange = 1;
            InterArrivalDistribution[0].MaxRange = InterArrivalDistribution[0].CummProbability * 100;

            InterArrivalDistribution[1].MinRange = InterArrivalDistribution[0].MaxRange + 1;
            InterArrivalDistribution[1].MaxRange = InterArrivalDistribution[1].CummProbability * 100;

            InterArrivalDistribution[2].MinRange = InterArrivalDistribution[1].MaxRange + 1;
            InterArrivalDistribution[2].MaxRange = InterArrivalDistribution[2].CummProbability * 100;

            InterArrivalDistribution[3].MinRange = InterArrivalDistribution[2].MaxRange + 1;
            InterArrivalDistribution[3].MaxRange = InterArrivalDistribution[3].CummProbability * 100 - 1;
            T1.Text = "";
            T2.Text = "";
            T3.Text = "";
            T4.Text = "";
            P1.Text = "";
            P2.Text = "";
            P3.Text = "";
            P4.Text = "";
            BtnSimulate.Text = "Save Server No. 1";
            HeaderLabel.Text = "Enter Server " + 1 + " Time Distribution.";
            ChangeLabel.Text = "Service Time";
            NameLabel.Visible = true;
            NameTB.Visible = true;
            NofServers.Visible = true;
            ServerLabel.Visible = true;
            BtnSimulate.Visible = true;
            BtnTime.Visible = false;
        }
    }
}
