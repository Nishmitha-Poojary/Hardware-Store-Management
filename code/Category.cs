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
    public partial class Category : Form
    {
        connect c;
        DataSet ds;
        SqlDataAdapter adp = new SqlDataAdapter();
        public Category()
        {
            InitializeComponent();
        }
        public void populate()
        {
            try
            {
                c = new connect();
                string myquery = "Select * from Cat_tables";
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
            txtcatid.Text = "";
            txtcatname.Text = "";
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if ( txtcatid.Text== "" || txtcatname.Text == "")
            {
                MessageBox.Show("Please enter required fields", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            { c = new connect();
                c.cmd.CommandText = "Select * from Cat_tables where cat_id='" +txtcatid.Text+"'";
                ds = new DataSet();
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "cat");
                c.cmd.ExecuteNonQuery();
                if (ds.Tables["cat"].Rows.Count > 0)
                {
                    MessageBox.Show("Category Id already exists");
                }
                else
                {
                    try
                    {
                        c.cmd.CommandText = "Insert into Cat_tables values(@cat_id,@cat_name)";
                        c.cmd.Parameters.Clear();
                        c.cmd.Parameters.Add("@cat_id", SqlDbType.NVarChar).Value = txtcatid.Text;
                        c.cmd.Parameters.Add("@cat_name", SqlDbType.NVarChar).Value = txtcatname.Text;
                        c.cmd.ExecuteNonQuery();
                        MessageBox.Show("!!!...Category Inserted...!!!");
                        populate();
                        clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        c.cnn.Close();
                    }
                }
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {

            if (txtcatid.Text == "")
            {
                MessageBox.Show("Please enter Category Id", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    c = new connect();
                    c.cmd.CommandText = "Select * from Cat_tables where cat_id='" + txtcatid.Text + "'";
                    ds = new DataSet();
                    adp.SelectCommand = c.cmd;
                    adp.Fill(ds, "del");
                    if (ds.Tables["del"].Rows.Count > 0)
                    {

                        c.cmd.CommandText = "Delete  from Cat_tables where cat_id='" + txtcatid.Text + "'";
                        c.cmd.ExecuteNonQuery();
                        MessageBox.Show("!!!...Category Deleted...!!!");
                        populate();
                        clear();
                    }
                    else
                    {
                        MessageBox.Show("Category Id not found");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    c.cnn.Close();
                }
            }
        }

        private void Category_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'managementDataSet5.Cat_tables' table. You can move, or remove it, as needed.
            this.cat_tablesTableAdapter.Fill(this.managementDataSet5.Cat_tables);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.CurrentRow.Selected = true;
                txtcatid.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                txtcatname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
            Inventory int1 = new Inventory();
            int1.Show();
        }
    }
}