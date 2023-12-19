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
    public partial class login : Form
    {
        connect c;
        SqlDataAdapter adp = new SqlDataAdapter();
        DataSet ds;
        public login()
        {
            InitializeComponent();
        }

        private void lblfp_Click(object sender, EventArgs e)
        {
            mylogin.admin_name = txtuser.Text;
            if (txtuser.Text == "")
            {
                MessageBox.Show("Please enter the Username", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                c = new connect();
                c.cmd.CommandText = "Select * from Ad_login where user_name='" + txtuser.Text + "'";
                ds = new DataSet();
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "log");
                if (ds.Tables["log"].Rows.Count > 0)
                {
                    forget fog = new forget();
                    fog.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid Username", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

       private void button1_Click(object sender, EventArgs e)
        {
            txtuser.Text = "";
            txtpass.Text = "";
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            if (txtuser.Text == "" || txtpass.Text == "")
            {
                MessageBox.Show("Please enter User Name and Password", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    c = new connect();
                    c.cmd.CommandText = "Select * from Ad_login where user_name='" + txtuser.Text.Trim() + "'and password='" + txtpass.Text.Trim() + "'";
                    ds = new DataSet();
                    adp.SelectCommand = c.cmd;
                    adp.Fill(ds, "log");
                    if (ds.Tables["log"].Rows.Count > 0)
                    {
                        Main mn = new Main();
                        this.Hide();
                        mn.Show();
                    }
                    else
                    {
                        MessageBox.Show("Check your username or password!!!","Error",MessageBoxButtons .OK,MessageBoxIcon .Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
         }

        private void button2_Click(object sender, EventArgs e)
        {
            txtuser.Text = "";
            txtpass.Text = "";
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtpass_TextChanged(object sender, EventArgs e)
        {
            
            if (txtpass.TextLength >= 6 && txtpass.TextLength<=11)
            {
                txtpass.ForeColor = Color.Black;
            }
            else if (txtpass.TextLength >=11)
            {
                MessageBox.Show("character must be 6-12 characters only...","Warning",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            else
            {
                txtpass.ForeColor = Color.Red;
            }
        }

        private void txtuser_TextChanged(object sender, EventArgs e)
        {
            
                    c = new connect();
                    c.cmd.CommandText = "Select * from Ad_login where user_name='" + txtuser.Text.Trim() + "'";
                    ds = new DataSet();
                    adp.SelectCommand = c.cmd;
                    adp.Fill(ds, "log");
                    if (ds.Tables["log"].Rows.Count > 0)
                    {
                        txtuser.ForeColor = Color.Black;
                    }
                    else
                    {
                        txtuser.ForeColor = Color.Red;
                    }
        }

        private void login_Load(object sender, EventArgs e)
        {

        }
  }
}
public static class mylogin
{
    public static string admin_name = "";
}