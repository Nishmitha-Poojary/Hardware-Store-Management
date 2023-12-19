using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace store_management
{
    public partial class Employee_Management : Form
    {
        public Employee_Management()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            Employee_Records er = new Employee_Records();
            er.Show();
            this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Attendance1 at = new Attendance1();
            at.Show();
            this.Close();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            myempattnd.empid = " ";
            myempattnd.yr = 0;
            myempattnd.month = "";
            myempattnd.wrdy = 0;
            myempattnd.ltaken = 0;
            Employee_Salary es = new Employee_Salary();
            es.Show();
            this.Close();
        }
    }
}