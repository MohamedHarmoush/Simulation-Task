namespace Simulation_Task
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnSimulate = new System.Windows.Forms.Button();
            this.NofServers = new System.Windows.Forms.TextBox();
            this.ServerLabel = new System.Windows.Forms.Label();
            this.T4 = new System.Windows.Forms.TextBox();
            this.T3 = new System.Windows.Forms.TextBox();
            this.T2 = new System.Windows.Forms.TextBox();
            this.T1 = new System.Windows.Forms.TextBox();
            this.P1 = new System.Windows.Forms.TextBox();
            this.P2 = new System.Windows.Forms.TextBox();
            this.P3 = new System.Windows.Forms.TextBox();
            this.P4 = new System.Windows.Forms.TextBox();
            this.ChangeLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.HeaderLabel = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.NameTB = new System.Windows.Forms.TextBox();
            this.BtnTime = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnSimulate
            // 
            this.BtnSimulate.Location = new System.Drawing.Point(395, 254);
            this.BtnSimulate.Name = "BtnSimulate";
            this.BtnSimulate.Size = new System.Drawing.Size(109, 41);
            this.BtnSimulate.TabIndex = 1;
            this.BtnSimulate.Text = "Save Server No.  ";
            this.BtnSimulate.UseVisualStyleBackColor = true;
            this.BtnSimulate.Visible = false;
            this.BtnSimulate.Click += new System.EventHandler(this.button1_Click);
            // 
            // NofServers
            // 
            this.NofServers.Location = new System.Drawing.Point(12, 25);
            this.NofServers.Name = "NofServers";
            this.NofServers.Size = new System.Drawing.Size(100, 20);
            this.NofServers.TabIndex = 2;
            this.NofServers.Visible = false;
            this.NofServers.TextChanged += new System.EventHandler(this.UpdateInputGV);
            // 
            // ServerLabel
            // 
            this.ServerLabel.AutoSize = true;
            this.ServerLabel.Location = new System.Drawing.Point(12, 9);
            this.ServerLabel.Name = "ServerLabel";
            this.ServerLabel.Size = new System.Drawing.Size(40, 13);
            this.ServerLabel.TabIndex = 3;
            this.ServerLabel.Text = "Severs";
            this.ServerLabel.Visible = false;
            // 
            // T4
            // 
            this.T4.Location = new System.Drawing.Point(401, 158);
            this.T4.Name = "T4";
            this.T4.Size = new System.Drawing.Size(100, 20);
            this.T4.TabIndex = 4;
            // 
            // T3
            // 
            this.T3.Location = new System.Drawing.Point(271, 158);
            this.T3.Name = "T3";
            this.T3.Size = new System.Drawing.Size(100, 20);
            this.T3.TabIndex = 5;
            // 
            // T2
            // 
            this.T2.Location = new System.Drawing.Point(142, 158);
            this.T2.Name = "T2";
            this.T2.Size = new System.Drawing.Size(100, 20);
            this.T2.TabIndex = 6;
            // 
            // T1
            // 
            this.T1.Location = new System.Drawing.Point(9, 158);
            this.T1.Name = "T1";
            this.T1.Size = new System.Drawing.Size(100, 20);
            this.T1.TabIndex = 7;
            // 
            // P1
            // 
            this.P1.Location = new System.Drawing.Point(9, 206);
            this.P1.Name = "P1";
            this.P1.Size = new System.Drawing.Size(100, 20);
            this.P1.TabIndex = 11;
            // 
            // P2
            // 
            this.P2.Location = new System.Drawing.Point(142, 206);
            this.P2.Name = "P2";
            this.P2.Size = new System.Drawing.Size(100, 20);
            this.P2.TabIndex = 10;
            // 
            // P3
            // 
            this.P3.Location = new System.Drawing.Point(271, 206);
            this.P3.Name = "P3";
            this.P3.Size = new System.Drawing.Size(100, 20);
            this.P3.TabIndex = 9;
            // 
            // P4
            // 
            this.P4.Location = new System.Drawing.Point(401, 206);
            this.P4.Name = "P4";
            this.P4.Size = new System.Drawing.Size(100, 20);
            this.P4.TabIndex = 8;
            // 
            // ChangeLabel
            // 
            this.ChangeLabel.AutoSize = true;
            this.ChangeLabel.Location = new System.Drawing.Point(12, 139);
            this.ChangeLabel.Name = "ChangeLabel";
            this.ChangeLabel.Size = new System.Drawing.Size(83, 13);
            this.ChangeLabel.TabIndex = 12;
            this.ChangeLabel.Text = "InterArrival Time";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Probability";
            // 
            // HeaderLabel
            // 
            this.HeaderLabel.AutoSize = true;
            this.HeaderLabel.Font = new System.Drawing.Font("Comic Sans MS", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HeaderLabel.Location = new System.Drawing.Point(176, 39);
            this.HeaderLabel.MinimumSize = new System.Drawing.Size(40, 20);
            this.HeaderLabel.Name = "HeaderLabel";
            this.HeaderLabel.Size = new System.Drawing.Size(288, 24);
            this.HeaderLabel.TabIndex = 14;
            this.HeaderLabel.Text = "InterArrival Distribution Of Calls";
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(12, 76);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(69, 13);
            this.NameLabel.TabIndex = 16;
            this.NameLabel.Text = "Server Name";
            // 
            // NameTB
            // 
            this.NameTB.Location = new System.Drawing.Point(9, 95);
            this.NameTB.Name = "NameTB";
            this.NameTB.Size = new System.Drawing.Size(100, 20);
            this.NameTB.TabIndex = 15;
            // 
            // BtnTime
            // 
            this.BtnTime.Location = new System.Drawing.Point(198, 254);
            this.BtnTime.Name = "BtnTime";
            this.BtnTime.Size = new System.Drawing.Size(129, 41);
            this.BtnTime.TabIndex = 17;
            this.BtnTime.Text = "Save Time Distribution";
            this.BtnTime.UseVisualStyleBackColor = true;
            this.BtnTime.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 307);
            this.Controls.Add(this.BtnTime);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.NameTB);
            this.Controls.Add(this.HeaderLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ChangeLabel);
            this.Controls.Add(this.P1);
            this.Controls.Add(this.P2);
            this.Controls.Add(this.P3);
            this.Controls.Add(this.P4);
            this.Controls.Add(this.T1);
            this.Controls.Add(this.T2);
            this.Controls.Add(this.T3);
            this.Controls.Add(this.T4);
            this.Controls.Add(this.ServerLabel);
            this.Controls.Add(this.NofServers);
            this.Controls.Add(this.BtnSimulate);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnSimulate;
        private System.Windows.Forms.TextBox NofServers;
        private System.Windows.Forms.Label ServerLabel;
        private System.Windows.Forms.TextBox T4;
        private System.Windows.Forms.TextBox T3;
        private System.Windows.Forms.TextBox T2;
        private System.Windows.Forms.TextBox T1;
        private System.Windows.Forms.TextBox P1;
        private System.Windows.Forms.TextBox P2;
        private System.Windows.Forms.TextBox P3;
        private System.Windows.Forms.TextBox P4;
        private System.Windows.Forms.Label ChangeLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label HeaderLabel;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.TextBox NameTB;
        private System.Windows.Forms.Button BtnTime;
    }
}

