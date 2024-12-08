using System.Windows.Forms;

namespace budgetCalculator
{
    partial class MonthlyReportForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label labelReportDate;
        private Label labelTotalEnergy;
        private Label labelTotalCost;
        private Label labelRemainingBudget;
        private Button buttonNext;
        private Button buttonPrevious;
        private DataGridView dataGridView1;

        private void InitializeComponent()
        {
            this.labelReportDate = new System.Windows.Forms.Label();
            this.labelTotalEnergy = new System.Windows.Forms.Label();
            this.labelTotalCost = new System.Windows.Forms.Label();
            this.labelRemainingBudget = new System.Windows.Forms.Label();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelReportDate
            // 
            this.labelReportDate.AutoSize = true;
            this.labelReportDate.Location = new System.Drawing.Point(13, 13);
            this.labelReportDate.Name = "labelReportDate";
            this.labelReportDate.Size = new System.Drawing.Size(80, 13);
            this.labelReportDate.TabIndex = 0;
            this.labelReportDate.Text = "Report Date: --";
            // 
            // labelTotalEnergy
            // 
            this.labelTotalEnergy.AutoSize = true;
            this.labelTotalEnergy.Location = new System.Drawing.Point(13, 40);
            this.labelTotalEnergy.Name = "labelTotalEnergy";
            this.labelTotalEnergy.Size = new System.Drawing.Size(112, 13);
            this.labelTotalEnergy.TabIndex = 1;
            this.labelTotalEnergy.Text = "Total Energy: -- kWh";
            // 
            // labelTotalCost
            // 
            this.labelTotalCost.AutoSize = true;
            this.labelTotalCost.Location = new System.Drawing.Point(13, 67);
            this.labelTotalCost.Name = "labelTotalCost";
            this.labelTotalCost.Size = new System.Drawing.Size(95, 13);
            this.labelTotalCost.TabIndex = 2;
            this.labelTotalCost.Text = "Total Cost: -- RS";
            // 
            // labelRemainingBudget
            // 
            this.labelRemainingBudget.AutoSize = true;
            this.labelRemainingBudget.Location = new System.Drawing.Point(13, 94);
            this.labelRemainingBudget.Name = "labelRemainingBudget";
            this.labelRemainingBudget.Size = new System.Drawing.Size(127, 13);
            this.labelRemainingBudget.TabIndex = 3;
            this.labelRemainingBudget.Text = "Remaining Budget: -- RS";
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(220, 130);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(75, 23);
            this.buttonNext.TabIndex = 4;
            this.buttonNext.Text = "Next";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.Location = new System.Drawing.Point(139, 130);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(75, 23);
            this.buttonPrevious.TabIndex = 5;
            this.buttonPrevious.Text = "Previous";
            this.buttonPrevious.UseVisualStyleBackColor = true;
            this.buttonPrevious.Click += new System.EventHandler(this.buttonPrevious_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(16, 160);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(400, 150);
            this.dataGridView1.TabIndex = 6;
            // 
            // MonthlyReportForm
            // 
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.buttonPrevious);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.labelRemainingBudget);
            this.Controls.Add(this.labelTotalCost);
            this.Controls.Add(this.labelTotalEnergy);
            this.Controls.Add(this.labelReportDate);
            this.Name = "MonthlyReportForm";
            this.Text = "Monthly Report";
            this.Load += new System.EventHandler(this.MonthlyReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
