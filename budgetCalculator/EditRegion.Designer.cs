namespace budgetCalculator
{
    partial class EditRegion
    {
        private System.Windows.Forms.DataGridView dgvRegions;
        private System.Windows.Forms.DateTimePicker dtpPeakStart;
        private System.Windows.Forms.DateTimePicker dtpPeakEnd;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblSelectedRegion;
        private System.Windows.Forms.Label lblPeakHours;

        private void InitializeComponent()
        {
            this.dgvRegions = new System.Windows.Forms.DataGridView();
            this.dtpPeakStart = new System.Windows.Forms.DateTimePicker();
            this.dtpPeakEnd = new System.Windows.Forms.DateTimePicker();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblSelectedRegion = new System.Windows.Forms.Label();
            this.lblPeakHours = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvRegions)).BeginInit();
            this.SuspendLayout();

            // 
            // dgvRegions
            // 
            this.dgvRegions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRegions.Location = new System.Drawing.Point(12, 12);
            this.dgvRegions.Name = "dgvRegions";
            this.dgvRegions.RowHeadersWidth = 51;
            this.dgvRegions.Size = new System.Drawing.Size(560, 150);
            this.dgvRegions.TabIndex = 0;
            this.dgvRegions.SelectionChanged += new System.EventHandler(this.dgvRegions_SelectionChanged);

            // 
            // dtpPeakStart
            // 
            this.dtpPeakStart.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpPeakStart.Location = new System.Drawing.Point(180, 200);
            this.dtpPeakStart.Name = "dtpPeakStart";
            this.dtpPeakStart.ShowUpDown = true;
            this.dtpPeakStart.Size = new System.Drawing.Size(120, 22);
            this.dtpPeakStart.TabIndex = 1;

            // 
            // dtpPeakEnd
            // 
            this.dtpPeakEnd.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpPeakEnd.Location = new System.Drawing.Point(340, 200);
            this.dtpPeakEnd.Name = "dtpPeakEnd";
            this.dtpPeakEnd.ShowUpDown = true;
            this.dtpPeakEnd.Size = new System.Drawing.Size(120, 22);
            this.dtpPeakEnd.TabIndex = 2;

            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(180, 280);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 30);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save Changes";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(340, 280);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 30);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete Region";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // 
            // lblSelectedRegion
            // 
            this.lblSelectedRegion.AutoSize = true;
            this.lblSelectedRegion.Location = new System.Drawing.Point(12, 175);
            this.lblSelectedRegion.Name = "lblSelectedRegion";
            this.lblSelectedRegion.Size = new System.Drawing.Size(111, 16);
            this.lblSelectedRegion.TabIndex = 5;
            this.lblSelectedRegion.Text = "Selected Region:";

            // 
            // lblPeakHours
            // 
            this.lblPeakHours.AutoSize = true;
            this.lblPeakHours.Location = new System.Drawing.Point(12, 205);
            this.lblPeakHours.Name = "lblPeakHours";
            this.lblPeakHours.Size = new System.Drawing.Size(81, 16);
            this.lblPeakHours.TabIndex = 6;
            this.lblPeakHours.Text = "Peak Hours:";

            // 
            // EditRegion
            // 
            this.ClientSize = new System.Drawing.Size(584, 321);
            this.Controls.Add(this.dgvRegions);
            this.Controls.Add(this.dtpPeakStart);
            this.Controls.Add(this.dtpPeakEnd);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lblSelectedRegion);
            this.Controls.Add(this.lblPeakHours);
            this.Name = "EditRegion";
            this.Text = "Edit Region";
            this.Load += new System.EventHandler(this.EditRegion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
