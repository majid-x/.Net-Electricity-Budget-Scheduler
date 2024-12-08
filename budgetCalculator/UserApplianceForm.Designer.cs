using System.Windows.Forms;
using System;

namespace budgetCalculator
{
    partial class UserApplianceForm
    {
        private ComboBox cboRegion; // ComboBox for Region
        private TextBox txtUserId; // User ID TextBox
        private TextBox txtBudget; // Budget TextBox
        private DataGridView dgvAppliances; // DataGridView for appliances
        private Button btnSubmit; // Submit Button

        private void InitializeComponent()
        {
            this.cboRegion = new System.Windows.Forms.ComboBox();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.txtBudget = new System.Windows.Forms.TextBox();
            this.dgvAppliances = new System.Windows.Forms.DataGridView();
            this.Appliance = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.UnitNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UsageHours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppliances)).BeginInit();
            this.SuspendLayout();
            // 
            // cboRegion
            // 
            this.cboRegion.FormattingEnabled = true;
            this.cboRegion.Location = new System.Drawing.Point(203, 22);
            this.cboRegion.Name = "cboRegion";
            this.cboRegion.Size = new System.Drawing.Size(200, 24);
            this.cboRegion.TabIndex = 0;
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(203, 59);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(200, 22);
            this.txtUserId.TabIndex = 1;
            // 
            // txtBudget
            // 
            this.txtBudget.Location = new System.Drawing.Point(203, 97);
            this.txtBudget.Name = "txtBudget";
            this.txtBudget.Size = new System.Drawing.Size(200, 22);
            this.txtBudget.TabIndex = 2;
            // 
            // dgvAppliances
            // 
            this.dgvAppliances.ColumnHeadersHeight = 29;
            this.dgvAppliances.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Appliance,
            this.UnitNumber,
            this.UsageHours});
            this.dgvAppliances.Location = new System.Drawing.Point(70, 142);
            this.dgvAppliances.Name = "dgvAppliances";
            this.dgvAppliances.RowHeadersWidth = 51;
            this.dgvAppliances.Size = new System.Drawing.Size(500, 200);
            this.dgvAppliances.TabIndex = 3;
            // 
            // Appliance
            // 
            this.Appliance.HeaderText = "Appliance";
            this.Appliance.MinimumWidth = 6;
            this.Appliance.Name = "Appliance";
            this.Appliance.Width = 125;
            // 
            // UnitNumber
            // 
            this.UnitNumber.HeaderText = "Unit Number";
            this.UnitNumber.MinimumWidth = 6;
            this.UnitNumber.Name = "UnitNumber";
            this.UnitNumber.Width = 125;
            // 
            // UsageHours
            // 
            this.UsageHours.HeaderText = "Usage Hours";
            this.UsageHours.MinimumWidth = 6;
            this.UsageHours.Name = "UsageHours";
            this.UsageHours.Width = 125;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(242, 362);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Daily";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(99, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Region";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(99, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Budget";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(99, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Meter No";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(632, 362);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Back";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(349, 362);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Weekly";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(468, 362);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "Monthly";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(632, 79);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(115, 28);
            this.button5.TabIndex = 12;
            this.button5.Text = "Add Appliance";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(650, 125);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 13;
            this.button4.Text = "Reload";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // UserApplianceForm
            // 
            this.ClientSize = new System.Drawing.Size(777, 458);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboRegion);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.txtBudget);
            this.Controls.Add(this.dgvAppliances);
            this.Controls.Add(this.btnSubmit);
            this.Name = "UserApplianceForm";
            this.Text = "User Appliance Selection";
            this.Load += new System.EventHandler(this.UserApplianceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppliances)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private DataGridViewComboBoxColumn Appliance;
        private DataGridViewTextBoxColumn UnitNumber;
        private DataGridViewTextBoxColumn UsageHours;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button5;
        private Button button4;
    }
}
