﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BudgetCalculator;

namespace budgetCalculator
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            createNewRegion cr = new createNewRegion();
            cr.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditRegion cr = new EditRegion();
            cr.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            addAppliance ap = new addAppliance();
            ap.Show();
            this.Hide();
        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DeleteApp ex = new DeleteApp();
            ex.Show();
            this.Hide();
        }
    }
}
