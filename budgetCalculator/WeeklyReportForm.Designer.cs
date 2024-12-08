namespace budgetCalculator
{
    partial class WeeklyReportForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.labelReportRegion = new System.Windows.Forms.Label();
            this.labelReportDate = new System.Windows.Forms.Label();
            this.labelTotalEnergy = new System.Windows.Forms.Label();
            this.labelTotalCost = new System.Windows.Forms.Label();
            this.labelRemainingBudget = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(20, 150);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(750, 200);
            this.dataGridView1.TabIndex = 0;
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(695, 380);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(75, 30);
            this.buttonNext.TabIndex = 1;
            this.buttonNext.Text = "Next";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.Location = new System.Drawing.Point(600, 380);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(75, 30);
            this.buttonPrevious.TabIndex = 2;
            this.buttonPrevious.Text = "Previous";
            this.buttonPrevious.UseVisualStyleBackColor = true;
            this.buttonPrevious.Click += new System.EventHandler(this.buttonPrevious_Click);
            // 
            // labelReportRegion
            // 
            this.labelReportRegion.AutoSize = true;
            this.labelReportRegion.Location = new System.Drawing.Point(20, 20);
            this.labelReportRegion.Name = "labelReportRegion";
            this.labelReportRegion.Size = new System.Drawing.Size(58, 16);
            this.labelReportRegion.TabIndex = 3;
            this.labelReportRegion.Text = "Region: -";
            // 
            // labelReportDate
            // 
            this.labelReportDate.AutoSize = true;
            this.labelReportDate.Location = new System.Drawing.Point(20, 50);
            this.labelReportDate.Name = "labelReportDate";
            this.labelReportDate.Size = new System.Drawing.Size(42, 16);
            this.labelReportDate.TabIndex = 4;
            this.labelReportDate.Text = "Date: -";
            // 
            // labelTotalEnergy
            // 
            this.labelTotalEnergy.AutoSize = true;
            this.labelTotalEnergy.Location = new System.Drawing.Point(20, 80);
            this.labelTotalEnergy.Name = "labelTotalEnergy";
            this.labelTotalEnergy.Size = new System.Drawing.Size(89, 16);
            this.labelTotalEnergy.TabIndex = 5;
            this.labelTotalEnergy.Text = "Total Energy: -";
            // 
            // labelTotalCost
            // 
            this.labelTotalCost.AutoSize = true;
            this.labelTotalCost.Location = new System.Drawing.Point(20, 110);
            this.labelTotalCost.Name = "labelTotalCost";
            this.labelTotalCost.Size = new System.Drawing.Size(72, 16);
            this.labelTotalCost.TabIndex = 6;
            this.labelTotalCost.Text = "Total Cost: -";
            // 
            // labelRemainingBudget
            // 
            this.labelRemainingBudget.AutoSize = true;
            this.labelRemainingBudget.Location = new System.Drawing.Point(20, 140);
            this.labelRemainingBudget.Name = "labelRemainingBudget";
            this.labelRemainingBudget.Size = new System.Drawing.Size(120, 16);
            this.labelRemainingBudget.TabIndex = 7;
            this.labelRemainingBudget.Text = "Remaining Budget: -";
            // 
            // WeeklyReportForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelRemainingBudget);
            this.Controls.Add(this.labelTotalCost);
            this.Controls.Add(this.labelTotalEnergy);
            this.Controls.Add(this.labelReportDate);
            this.Controls.Add(this.labelReportRegion);
            this.Controls.Add(this.buttonPrevious);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.dataGridView1);
            this.Name = "WeeklyReportForm";
            this.Text = "Weekly Report";
            this.Load += new System.EventHandler(this.WeeklyReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonPrevious;
        private System.Windows.Forms.Label labelReportRegion;
        private System.Windows.Forms.Label labelReportDate;
        private System.Windows.Forms.Label labelTotalEnergy;
        private System.Windows.Forms.Label labelTotalCost;
        private System.Windows.Forms.Label labelRemainingBudget;
    }
}
