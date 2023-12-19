using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace store_management
{
    public partial class Main : Form
    {
       public Main()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Supplier_Records sr = new Supplier_Records();
            sr.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Employee_Management em = new Employee_Management();
            em.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Sales_Details sd = new Sales_Details();
            sd.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Purchase_Details pd = new Purchase_Details();
            pd.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            myphone.telephone = "";
            Customer_Records cr = new Customer_Records();
            cr.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Inventory invnt = new Inventory();
            invnt.Show();
        }
    }
}