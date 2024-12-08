namespace budgetCalculator
{
    partial class createNewRegion
    {
        private System.Windows.Forms.TextBox txtRegionName;
        private System.Windows.Forms.DateTimePicker dtpPeakStart;
        private System.Windows.Forms.DateTimePicker dtpPeakEnd;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblPeakHours;
        private System.Windows.Forms.Label lblOffPeakHours;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;

        private void InitializeComponent()
        {
            this.txtRegionName = new System.Windows.Forms.TextBox();
            this.dtpPeakStart = new System.Windows.Forms.DateTimePicker();
            this.dtpPeakEnd = new System.Windows.Forms.DateTimePicker();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblPeakHours = new System.Windows.Forms.Label();
            this.lblOffPeakHours = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // txtRegionName
            this.txtRegionName.Location = new System.Drawing.Point(203, 22);
            this.txtRegionName.Name = "txtRegionName";
            this.txtRegionName.Size = new System.Drawing.Size(200, 22);
            this.txtRegionName.TabIndex = 0;

            // dtpPeakStart
            this.dtpPeakStart.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpPeakStart.Location = new System.Drawing.Point(203, 72);
            this.dtpPeakStart.Name = "dtpPeakStart";
            this.dtpPeakStart.ShowUpDown = true;
            this.dtpPeakStart.Size = new System.Drawing.Size(200, 22);
            this.dtpPeakStart.TabIndex = 1;
            this.dtpPeakStart.ValueChanged += new System.EventHandler(this.dtp_ValueChanged);

            // dtpPeakEnd
            this.dtpPeakEnd.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpPeakEnd.Location = new System.Drawing.Point(203, 112);
            this.dtpPeakEnd.Name = "dtpPeakEnd";
            this.dtpPeakEnd.ShowUpDown = true;
            this.dtpPeakEnd.Size = new System.Drawing.Size(200, 22);
            this.dtpPeakEnd.TabIndex = 2;
            this.dtpPeakEnd.ValueChanged += new System.EventHandler(this.dtp_ValueChanged);

            // btnSave
            this.btnSave.Location = new System.Drawing.Point(255, 176);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save Region";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // lblPeakHours
            this.lblPeakHours.Location = new System.Drawing.Point(203, 144);
            this.lblPeakHours.Name = "lblPeakHours";
            this.lblPeakHours.Size = new System.Drawing.Size(200, 16);
            this.lblPeakHours.TabIndex = 4;

            // lblOffPeakHours
            this.lblOffPeakHours.Location = new System.Drawing.Point(203, 160);
            this.lblOffPeakHours.Name = "lblOffPeakHours";
            this.lblOffPeakHours.Size = new System.Drawing.Size(200, 16);
            this.lblOffPeakHours.TabIndex = 5;

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Region Name";

            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Peak Hour Start";

            // label3
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Peak Hour End";

            // button1
            this.button1.Location = new System.Drawing.Point(498, 176);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Back";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);

            // createNewRegion
            this.ClientSize = new System.Drawing.Size(650, 220);
            this.Controls.Add(this.txtRegionName);
            this.Controls.Add(this.dtpPeakStart);
            this.Controls.Add(this.dtpPeakEnd);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblPeakHours);
            this.Controls.Add(this.lblOffPeakHours);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Name = "createNewRegion";
            this.Text = "Create New Region";
            this.Load += new System.EventHandler(this.createNewRegion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
