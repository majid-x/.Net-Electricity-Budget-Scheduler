namespace BudgetCalculator
{
    partial class DeleteApp
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvAppliances;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblSelectedAppliance;
        private System.Windows.Forms.Label lblWattage;
        private System.Windows.Forms.Button btnClose;

        // Disposing of resources
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // Initialize components
        private void InitializeComponent()
        {
            this.dgvAppliances = new System.Windows.Forms.DataGridView();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblSelectedAppliance = new System.Windows.Forms.Label();
            this.lblWattage = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppliances)).BeginInit();
            this.SuspendLayout();

            // DataGridView to show appliances
            this.dgvAppliances.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAppliances.Location = new System.Drawing.Point(12, 12);
            this.dgvAppliances.Name = "dgvAppliances";
            this.dgvAppliances.RowHeadersWidth = 51;
            this.dgvAppliances.Size = new System.Drawing.Size(560, 150);
            this.dgvAppliances.TabIndex = 0;
            this.dgvAppliances.SelectionChanged += new System.EventHandler(this.dgvAppliances_SelectionChanged);

            // Delete button
            this.btnDelete.Location = new System.Drawing.Point(180, 200);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 23);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete Appliance";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // Label to show selected appliance
            this.lblSelectedAppliance.Location = new System.Drawing.Point(12, 180);
            this.lblSelectedAppliance.Name = "lblSelectedAppliance";
            this.lblSelectedAppliance.Size = new System.Drawing.Size(560, 20);
            this.lblSelectedAppliance.TabIndex = 2;

            // Label to show wattage of selected appliance
            this.lblWattage.Location = new System.Drawing.Point(12, 220);
            this.lblWattage.Name = "lblWattage";
            this.lblWattage.Size = new System.Drawing.Size(560, 20);
            this.lblWattage.TabIndex = 3;

            // Close button
            this.btnClose.Location = new System.Drawing.Point(320, 200);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // ApplianceForm setup
            this.ClientSize = new System.Drawing.Size(584, 261);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblWattage);
            this.Controls.Add(this.lblSelectedAppliance);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.dgvAppliances);
            this.Name = "ApplianceForm";
            this.Text = "Appliance List";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppliances)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
