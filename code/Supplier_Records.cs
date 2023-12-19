using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace store_management
{
    public partial class Supplier_Records : Form
    {
        connect c;
        DataSet ds;
        SqlDataAdapter adp = new SqlDataAdapter();
        public Supplier_Records()
        {
            InitializeComponent();
        }
        public void populate()
        {
            try
            {
                c = new connect();
                string myquery = "Select * from Supplier_details";
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
                txtsno.Text = "";
                txtsname.Text = "";
                txtsaddress.Text = "";
                txtsphone.Text = "";
                txtsemail.Text = "";
                txtcompany.Text = "";
            }
        public void Generat_Sup_Id()
        {
            c = new connect();
            string sup_id;
            string querySup = "select Sup_no from Supplier_details order by Sup_no Desc";
            SqlCommand cmdS = new SqlCommand(querySup, c.cnn);
            SqlDataReader drS = cmdS.ExecuteReader();
            if (drS.Read())
            {
                int i = int.Parse(drS[0].ToString()) + 1;
                sup_id = i.ToString("0000");
            }
            else if (Convert.IsDBNull(drS))
            {
                sup_id = ("0001");
            }
            else
            {
                sup_id = ("0001");
            }
            txtsno.Text= sup_id.ToString();
        }
        

        private void Supplier_Records_Load(object sender, EventArgs e)
        {
            Generat_Sup_Id();
            // TODO: This line of code loads data into the 'managementDataSet3.Supplier_details' table. You can move, or remove it, as needed.
            this.supplier_detailsTableAdapter.Fill(this.managementDataSet3.Supplier_details);
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (txtsno.Text == "" || txtsname.Text == "" || txtsphone.Text == "" || txtsaddress.Text  == "" || txtsemail.Text == "")
            {
                MessageBox.Show("Please enter required fields", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtsphone.TextLength < 10)
            {
                MessageBox.Show("Contact Number cannot be Less than 10", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                c = new connect();
                c.cmd.CommandText = "Select * from Supplier_details where Phone_no='" + Convert.ToInt64(txtsphone.Text) + "'or Email= '" + txtsemail.Text + "'";
                ds = new DataSet();
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "same1");
                c.cmd.ExecuteNonQuery();
                if (ds.Tables["same1"].Rows.Count > 0)
                {
                    MessageBox.Show("Phone number or E-mail already exists","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        c.cmd.CommandText = "Insert into Supplier_details values(@Sup_no,@Sup_name,@Phone_no,@Address,@Email,@Company)";
                        c.cmd.Parameters.Clear();
                        c.cmd.Parameters.Add("@Sup_no", SqlDbType.NVarChar).Value = txtsno.Text;
                        c.cmd.Parameters.Add("@Sup_name", SqlDbType.NVarChar).Value = txtsname.Text;
                        c.cmd.Parameters.Add("@Phone_no", SqlDbType.BigInt).Value = Convert.ToInt64(txtsphone.Text);
                        c.cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = txtsaddress.Text;
                        c.cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = txtsemail.Text;
                        c.cmd.Parameters.Add("@Company", SqlDbType.NVarChar).Value = txtcompany.Text; 
                        c.cmd.ExecuteNonQuery();
                        MessageBox.Show("Record Inserted..!!!","Inserted",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        populate();
                        clear();
                        Generat_Sup_Id();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Supplier Id Already present","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clear();
            Generat_Sup_Id();
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            if (txtsno.Text == "" || txtsname.Text == "" || txtsphone.Text == "" || txtsaddress.Text == "" || txtsemail.Text == "")
            {
                MessageBox.Show("Please enter required fields", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtsphone.TextLength < 10)
            {
                MessageBox.Show("Contact Number cannot be Less than 10", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    c = new connect();
                    c.cmd.CommandText = "Select * from Supplier_details where sup_no='" + txtsno.Text + "'";
                    ds = new DataSet();
                    adp.SelectCommand = c.cmd;
                    adp.Fill(ds, "sup");
                    c.cmd.ExecuteNonQuery();
                    if (ds.Tables["sup"].Rows.Count > 0)
                    {
                        c.cmd.CommandText = "Update Supplier_details set Sup_name=@Sup_name,Phone_no=@Phone_no,Address=@Address,Email=@Email,Company=@Company where Sup_no='" + txtsno.Text + "'";
                        c.cmd.Parameters.Clear();
                        c.cmd.Parameters.Add("@Sup_name", SqlDbType.NVarChar).Value = txtsname.Text;
                        c.cmd.Parameters.Add("@Phone_no", SqlDbType.BigInt).Value = Convert.ToInt64(txtsphone.Text);
                        c.cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = txtsaddress.Text;
                        c.cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = txtsemail.Text;
                        c.cmd.Parameters.Add("@Company", SqlDbType.NVarChar).Value = txtcompany.Text; 
                        c.cmd.ExecuteNonQuery();
                        MessageBox.Show("Updated..!!!","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        populate();
                        clear();
                        Generat_Sup_Id();
                    }
                    else
                    {
                        MessageBox.Show("Supplier Number does not exists!!!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Phone Number or E-mail already exists");
                }
                finally
                {
                    c.cnn.Close();
                }
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (txtsno.Text =="")
            {
                MessageBox.Show("Please enter Supplier Id", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    c = new connect();
                    c.cmd.CommandText = "Select * from Supplier_details where Sup_no='" + txtsno.Text+"'";
                    ds = new DataSet();
                    adp.SelectCommand = c.cmd;
                    adp.Fill(ds, "del");
                    if (ds.Tables["del"].Rows.Count > 0)
                    {

                        c.cmd.CommandText = "Delete  from Supplier_details where Sup_no='" + txtsno.Text+"'";
                        c.cmd.ExecuteNonQuery();
                        MessageBox.Show("Record Deleted..!!!","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        txtsno.Text = "";
                        populate();
                        clear();
                        Generat_Sup_Id();
                    }
                    else
                    {
                        MessageBox.Show("Record Does not exists","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
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

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.CurrentRow.Selected = true;
                txtsno.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                txtsname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                txtsphone.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                txtsaddress.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                txtsemail.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                txtcompany.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            }
        }

        private void txtsphone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Character, Spaces and Special Characters are not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtsname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Numbers and Special Characters are not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtsphone_TextChanged(object sender, EventArgs e)
        {
            if (txtsphone.TextLength == 10)
            {
                txtsphone.ForeColor = Color.Black;
            }
            else
            {
                txtsphone.ForeColor = Color.Red;
            }
            if (txtsphone.Text == "0")
            {
                DialogResult res1 = MessageBox.Show("Phone Number cannot be Zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (res1 == DialogResult.OK)
                {
                    txtsphone.Clear();
                }
            }
        }

        private void txtsemail_Leave(object sender, EventArgs e)
        {
            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (Regex.IsMatch(txtsemail.Text, pattern))
            {
                //errorProvider1.Clear();
            }
            else
            {
                //errorProvider1.SetError(this.txtsemail, "Please Provide Valid Email Address");
                //return;
                MessageBox.Show("Please Provide Valid Email Address", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtsemail.Text = "";
            }
        }
    }
}