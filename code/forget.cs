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
    public partial class forget : Form
    {
        connect c;
        SqlDataAdapter adp = new SqlDataAdapter();
        DataSet ds;
        public forget()
        {
            InitializeComponent();
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            string qtn = comboBox1.SelectedItem.ToString();
            if (comboBox1.SelectedItem.ToString()=="<<SELECT>>" || txtans.Text == "")
            {
                MessageBox.Show("Enter the required fields","Required",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                c = new connect();
                c.cmd.CommandText = "Select * from Ad_login where User_name='"+mylogin.admin_name+"'and sec_qtn='" + qtn + "'and sec_ans='" + txtans.Text.Trim() +  "'";
                ds = new DataSet();
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "fog");
                if (ds.Tables["fog"].Rows.Count > 0)
                {
                    reset rs = new reset();
                    this.Hide();
                    rs.Show();
                }
                else
                {
                    MessageBox.Show("check your security quetion or answer","ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    comboBox1.SelectedIndex = 0;
                    txtans.Text = "";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            txtans.Text = "";
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void forget_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }
    }
}