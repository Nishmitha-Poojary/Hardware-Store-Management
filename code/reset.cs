using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data .SqlClient ;

namespace store_management
{
    public partial class reset : Form
    {
        connect c;
        DataSet ds;
        SqlDataAdapter adp=new SqlDataAdapter ();
        public reset()
        {
            InitializeComponent();
        }

        private void btnreset1_Click(object sender, EventArgs e)
        {
            if (txtuser1.Text == "" || txtnewp.Text == "" || txtcomp.Text == "")
            {
                MessageBox.Show("Please enter required fields", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                c = new connect();
                c.cmd.CommandText = "Select * from Ad_login where user_name='" + txtuser1.Text + "'";
                ds = new DataSet();
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "pass1");
                c.cmd.ExecuteNonQuery();
                if (ds.Tables["pass1"].Rows.Count > 0)
                {
                    if (txtnewp.Text == txtcomp.Text)
                    {

                        c.cmd.CommandText = "Update Ad_login set password=@password where user_name='" + txtuser1.Text + "'";
                        c.cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = txtcomp.Text;
                        c.cmd.ExecuteNonQuery();
                        MessageBox.Show("Password Reseted...!!!","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        login log = new login();
                        log.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Field New Password and Comfirm Password Must Match..!!","Error",MessageBoxButtons .OK,MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("User name is incorrect...Type again","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    txtuser1.Text = "";
                }
            } 
          }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtnewp_TextChanged(object sender, EventArgs e)
        {
            if (txtnewp.TextLength >= 6 && txtnewp.TextLength <= 11)
            {
                txtnewp.ForeColor = Color.Black;
            }
            else if (txtnewp.TextLength >= 11)
            {
                MessageBox.Show("character must be 6-12 characters only...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                txtnewp.ForeColor = Color.Red;
            }
        }

        private void reset_Load(object sender, EventArgs e)
        {
            txtuser1.Text = mylogin.admin_name.ToString();
        }

    }

}