using System.Windows.Forms;
using System;

namespace budgetCalculator
{
    partial class ReportDetailForm
    {
        private System.ComponentModel.IContainer components = null;

        private DataGridView dataGridView1;
        private Label lblReportDate;
        private Label lblTotalEnergy;
        private Label lblTotalCost;
        private Label lblRemainingBudget;
        private Button btnNext;
        private Button btnPrevious;

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
            this.dataGridView1 = new DataGridView();
            this.lblReportDate = new Label();
            this.lblTotalEnergy = new Label();
            this.lblTotalCost = new Label();
            this.lblRemainingBudget = new Label();
            this.btnNext = new Button();
            this.btnPrevious = new Button();

            
            this.dataGridView1.Location = new System.Drawing.Point(20, 50);
            this.dataGridView1.Size = new System.Drawing.Size(760, 300);
            this.dataGridView1.ReadOnly = true;

            
            this.lblReportDate.Location = new System.Drawing.Point(20, 10);
            this.lblReportDate.Size = new System.Drawing.Size(300, 20);

            
            this.lblTotalEnergy.Location = new System.Drawing.Point(20, 360);
            this.lblTotalEnergy.Size = new System.Drawing.Size(200, 20);

            
            this.lblTotalCost.Location = new System.Drawing.Point(240, 360);
            this.lblTotalCost.Size = new System.Drawing.Size(200, 20);

            
            this.lblRemainingBudget.Location = new System.Drawing.Point(460, 360);
            this.lblRemainingBudget.Size = new System.Drawing.Size(200, 20);

            
            this.btnNext.Text = "Next";
            this.btnNext.Location = new System.Drawing.Point(600, 400);
            this.btnNext.Size = new System.Drawing.Size(75, 30);
            this.btnNext.Click += new EventHandler(this.btnNext_Click);

            
            this.btnPrevious.Text = "Previous";
            this.btnPrevious.Location = new System.Drawing.Point(500, 400);
            this.btnPrevious.Size = new System.Drawing.Size(75, 30);
            this.btnPrevious.Click += new EventHandler(this.btnPrevious_Click);

            
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblReportDate);
            this.Controls.Add(this.lblTotalEnergy);
            this.Controls.Add(this.lblTotalCost);
            this.Controls.Add(this.lblRemainingBudget);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Text = "Report Details";
            this.Load += new EventHandler(this.ReportDetailForm_Load);
        }
    }
}
