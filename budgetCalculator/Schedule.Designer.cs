namespace budgetCalculator
{
    partial class Schedule
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvSchedule;
        private System.Windows.Forms.Label lblTotalEnergy;
        private System.Windows.Forms.Label lblTotalCost;
        private System.Windows.Forms.Label lblRemainingBudget;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRegenerate;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label lblReportDate;
        private System.Windows.Forms.Button btnDownloadPdf;
        private System.Windows.Forms.Label label1;


        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvSchedule = new System.Windows.Forms.DataGridView();
            this.lblTotalEnergy = new System.Windows.Forms.Label();
            this.lblTotalCost = new System.Windows.Forms.Label();
            this.lblRemainingBudget = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRegenerate = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.lblReportDate = new System.Windows.Forms.Label();
            this.btnDownloadPdf = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();


            this.dgvSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSchedule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "Appliance", Name = "Appliance" },
                new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "Start Time", Name = "StartTime" },
                new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "End Time", Name = "EndTime" },
                new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "Wattage (W)", Name = "Wattage" },
                new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "Energy (kWh)", Name = "Energy" },
                new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "Cost (RS)", Name = "Cost" },
                new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText = "Time Type", Name = "TimeType" }
            });
            this.dgvSchedule.Location = new System.Drawing.Point(12, 40);
            this.dgvSchedule.Name = "dgvSchedule";
            this.dgvSchedule.RowTemplate.Height = 25;
            this.dgvSchedule.Size = new System.Drawing.Size(760, 300);
            this.dgvSchedule.TabIndex = 0;

            
            this.lblTotalEnergy.AutoSize = true;
            this.lblTotalEnergy.Location = new System.Drawing.Point(12, 350);
            this.lblTotalEnergy.Name = "lblTotalEnergy";
            this.lblTotalEnergy.Size = new System.Drawing.Size(100, 15);
            this.lblTotalEnergy.TabIndex = 1;

            
            this.lblTotalCost.AutoSize = true;
            this.lblTotalCost.Location = new System.Drawing.Point(12, 375);
            this.lblTotalCost.Name = "lblTotalCost";
            this.lblTotalCost.Size = new System.Drawing.Size(68, 15);
            this.lblTotalCost.TabIndex = 2;

            
            this.lblRemainingBudget.AutoSize = true;
            this.lblRemainingBudget.Location = new System.Drawing.Point(12, 400);
            this.lblRemainingBudget.Name = "lblRemainingBudget";
            this.lblRemainingBudget.Size = new System.Drawing.Size(109, 15);
            this.lblRemainingBudget.TabIndex = 3;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Coral;
            this.label1.Location = new System.Drawing.Point(594, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Currently in Peak Hours";

            this.lblReportDate.AutoSize = true;
            this.lblReportDate.Location = new System.Drawing.Point(12, 15);
            this.lblReportDate.Name = "lblReportDate";
            this.lblReportDate.Size = new System.Drawing.Size(85, 15);
            this.lblReportDate.TabIndex = 4;
            this.lblReportDate.Text = "Report Date: ";

            
            this.btnClose.Location = new System.Drawing.Point(650, 380);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 30);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

           
            this.btnRegenerate.Location = new System.Drawing.Point(520, 380);
            this.btnRegenerate.Name = "btnRegenerate";
            this.btnRegenerate.Size = new System.Drawing.Size(120, 30);
            this.btnRegenerate.TabIndex = 6;
            this.btnRegenerate.Text = "Regenerate";
            this.btnRegenerate.UseVisualStyleBackColor = true;
            this.btnRegenerate.Click += new System.EventHandler(this.btnRegenerate_Click);

            
            this.btnSubmit.Location = new System.Drawing.Point(390, 380);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(120, 30);
            this.btnSubmit.TabIndex = 7;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);

            
            this.btnDownloadPdf.Location = new System.Drawing.Point(260, 380);
            this.btnDownloadPdf.Name = "btnDownloadPdf";
            this.btnDownloadPdf.Size = new System.Drawing.Size(120, 30);
            this.btnDownloadPdf.TabIndex = 8;
            this.btnDownloadPdf.Text = "Download as PDF";
            this.btnDownloadPdf.UseVisualStyleBackColor = true;
            this.btnDownloadPdf.Click += new System.EventHandler(this.btnDownloadPdf_Click);

            
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 421);
            this.Controls.Add(this.lblReportDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvSchedule);
            this.Controls.Add(this.lblTotalEnergy);
            this.Controls.Add(this.lblTotalCost);
            this.Controls.Add(this.lblRemainingBudget);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRegenerate);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnDownloadPdf);
            this.Name = "Schedule";
            this.Text = "Budget Schedule";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
