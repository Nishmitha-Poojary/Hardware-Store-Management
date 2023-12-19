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
    public partial class Customer_Records : Form
    {
        connect c;
        DataSet ds;
        SqlDataAdapter adp = new SqlDataAdapter();
        public Customer_Records()
        {
            InitializeComponent();
        }
        public void populate()
        {
            try
            {
                c = new connect();
                string myquery = "Select * from Cust_details";
                SqlDataAdapter da = new SqlDataAdapter(myquery, c.cnn);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];

            }
            catch (Exception)
            {
                throw;
            }
        }
        public void clear()
        {
            txtcid .Text ="";
            txtcname .Text ="";
            txtcadd .Text ="";
            txtcphone .Text ="";
         }
        public void Generat_cust_Id()
        {
            c = new connect();
            string cust_id;
            string queryCust = "select Cust_id from Cust_details order by Cust_id Desc";
            SqlCommand cmdC = new SqlCommand(queryCust, c.cnn);
            SqlDataReader drC = cmdC.ExecuteReader();
            if (drC.Read())
            {
                int i = int.Parse(drC[0].ToString()) + 1;
                cust_id = i.ToString("0000");
            }
            else if (Convert.IsDBNull(drC))
            {
                cust_id = ("0001");
            }
            else
            {
                cust_id = ("0001");
            }
            txtcid.Text = cust_id.ToString();
        }
   
         private void Customer_Records_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'managementDataSet1.Cust_details' table. You can move, or remove it, as needed.
            this.cust_detailsTableAdapter1.Fill(this.managementDataSet1.Cust_details);
            Generat_cust_Id();
            txtcphone.Text = myphone.telephone.ToString ();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (txtcid.Text == "" || txtcname.Text == "" || txtcphone.Text == "" || txtcadd.Text == "")
            {
                MessageBox.Show("Please enter required fields", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtcphone.TextLength < 10)
            {
                MessageBox.Show("Contact Number cannot be Less than 10", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                c = new connect();
                c.cmd.CommandText = "Select * from Cust_details where Phone_no='" + Convert.ToInt64(txtcphone.Text) + "'";
                ds = new DataSet();
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "same");
                c.cmd.ExecuteNonQuery();
                if (ds.Tables["same"].Rows.Count > 0)
                {
                    MessageBox.Show("Phone number exists");
                }
                else
                {
                    try
                    {
                        c.cmd.CommandText = "Insert into Cust_details values(@Cust_id,@Cust_name,@Phone_no,@Address)";
                        c.cmd.Parameters.Clear();
                        c.cmd.Parameters.Add("@Cust_id", SqlDbType.NVarChar).Value = txtcid.Text;
                        c.cmd.Parameters.Add("@Cust_name", SqlDbType.NVarChar).Value = txtcname.Text;
                        c.cmd.Parameters.Add("@Phone_no", SqlDbType.BigInt).Value = Convert.ToInt64(txtcphone.Text);
                        c.cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = txtcadd.Text;
                        c.cmd.ExecuteNonQuery();
                        MessageBox.Show("Record Inserted..!!!","Inserted",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        populate();
                        clear();
                        Generat_cust_Id();
                    }
                    catch (Exception)
                    {
                        MessageBox .Show ("Customer Id already exists","Error",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                    finally
                    {
                        c.cnn.Close();
                     }
                }
            }
        }
        
        private void btnback_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            if (txtcid.Text == "" || txtcname.Text == "" || txtcphone.Text == "" || txtcadd.Text == "")
            {
                MessageBox.Show("Please enter required fields", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtcphone.TextLength < 10)
            {
                MessageBox.Show("Contact Number cannot be Less than 10", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
               try
               {
                   c = new connect();
                   c.cmd.CommandText = "Select * from Cust_details where Cust_id='" + txtcid.Text + "'";
                   ds = new DataSet();
                   adp.SelectCommand = c.cmd;
                   adp.Fill(ds, "cust");
                   c.cmd.ExecuteNonQuery();
                   if (ds.Tables["cust"].Rows.Count > 0)
                   {
                       c.cmd.CommandText = "Update Cust_details set Cust_name=@Cust_name,Phone_no=@Phone_no,Address=@Address where Cust_id='" + txtcid.Text + "'";
                       c.cmd.Parameters.Clear();
                       c.cmd.Parameters.Add("@Cust_name", SqlDbType.NVarChar).Value = txtcname.Text;
                       c.cmd.Parameters.Add("@Phone_no", SqlDbType.BigInt).Value = Convert.ToInt64(txtcphone.Text);
                       c.cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = txtcadd.Text;
                       c.cmd.ExecuteNonQuery();
                       MessageBox.Show("Updated..!!!","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                       populate();
                       clear();
                       Generat_cust_Id();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Customer Id!!!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
               }
               catch (Exception)
               {
                    throw;
                }
                finally
                {
                    c.cnn.Close();
                }
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (txtcid.Text == "")
            {
                MessageBox.Show("Please enter Customer Id", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    c = new connect();
                    c.cmd.CommandText = "Select * from Cust_details where Cust_id='" + txtcid.Text+"'";
                    ds = new DataSet();
                    adp.SelectCommand = c.cmd;
                    adp.Fill(ds, "del");
                    if (ds.Tables["del"].Rows.Count > 0)
                    {

                        c.cmd.CommandText = "Delete  from Cust_details where Cust_id='" + txtcid.Text+"'";
                        c.cmd.ExecuteNonQuery();
                        MessageBox.Show("Record Deleted..!!!","Deleted",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        populate();
                        clear();
                        Generat_cust_Id();
                    }
                    else
                    {
                        MessageBox.Show("Record not found","Error",MessageBoxButtons .OK,MessageBoxIcon.Information);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    c.cnn.Close();
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.CurrentRow.Selected = true;
                txtcid.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                txtcname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                txtcphone.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                txtcadd.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            }
        }

        private void btnclear_Click_1(object sender, EventArgs e)
        {
            clear();
            Generat_cust_Id();
        }

        private void txtcname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Numbers and Special Characters are not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtcphone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Character,Space and Special Characters are not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }  
        }

        private void txtcphone_TextChanged(object sender, EventArgs e)
        {
            if (txtcphone.TextLength == 10)
            {
                txtcphone.ForeColor = Color.Black;
            }
            else
            {
                txtcphone.ForeColor = Color.Red;
            }
            if (txtcphone.Text == "0")
            {
                DialogResult res1 = MessageBox.Show("Phone Number cannot be Zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (res1 == DialogResult.OK)
                {
                    txtcphone.Clear();
                }
            }
        }

    }
}
    
