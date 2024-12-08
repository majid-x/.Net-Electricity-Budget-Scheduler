namespace budgetCalculator
{
    partial class addAppliance
    {
        private System.ComponentModel.IContainer components = null;

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
            this.txtApplianceName = new System.Windows.Forms.TextBox();
            this.numericUpDownWattage = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblApplianceName = new System.Windows.Forms.Label();
            this.lblWattage = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWattage)).BeginInit();
            this.SuspendLayout();
            // 
            // txtApplianceName
            // 
            this.txtApplianceName.Location = new System.Drawing.Point(150, 30);
            this.txtApplianceName.Name = "txtApplianceName";
            this.txtApplianceName.Size = new System.Drawing.Size(200, 22);
            this.txtApplianceName.TabIndex = 0;
            // 
            // numericUpDownWattage
            // 
            this.numericUpDownWattage.Location = new System.Drawing.Point(150, 70);
            this.numericUpDownWattage.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownWattage.Name = "numericUpDownWattage";
            this.numericUpDownWattage.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownWattage.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(150, 110);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save Appliance";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblApplianceName
            // 
            this.lblApplianceName.AutoSize = true;
            this.lblApplianceName.Location = new System.Drawing.Point(30, 30);
            this.lblApplianceName.Name = "lblApplianceName";
            this.lblApplianceName.Size = new System.Drawing.Size(111, 16);
            this.lblApplianceName.TabIndex = 3;
            this.lblApplianceName.Text = "Appliance Name:";
            // 
            // lblWattage
            // 
            this.lblWattage.AutoSize = true;
            this.lblWattage.Location = new System.Drawing.Point(30, 70);
            this.lblWattage.Name = "lblWattage";
            this.lblWattage.Size = new System.Drawing.Size(61, 16);
            this.lblWattage.TabIndex = 4;
            this.lblWattage.Text = "Wattage:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(297, 110);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Back";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(388, 98);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // addAppliance
            // 
            this.ClientSize = new System.Drawing.Size(384, 161);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblWattage);
            this.Controls.Add(this.lblApplianceName);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.numericUpDownWattage);
            this.Controls.Add(this.txtApplianceName);
            this.Name = "addAppliance";
            this.Text = "Add Appliance";
            this.Load += new System.EventHandler(this.ApplianceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWattage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TextBox txtApplianceName;
        private System.Windows.Forms.NumericUpDown numericUpDownWattage;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblApplianceName;
        private System.Windows.Forms.Label lblWattage;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}