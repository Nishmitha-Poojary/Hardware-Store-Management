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
    public partial class Inventory : Form
    {
        connect c;
        DataSet ds;
        SqlDataAdapter adp = new SqlDataAdapter();
        public Inventory()
        {
            InitializeComponent();
        }
        public void populate()
        {
            try
            {
                c = new connect();
                string myquery = "Select * from Inventory";
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
        public void Generat_Item_Code()
        {
            c = new connect();
            string item_id;
            string queryItem = "select Item_code from Inventory order by Item_code Desc";
            SqlCommand cmdI = new SqlCommand(queryItem, c.cnn);
            SqlDataReader drI = cmdI.ExecuteReader();
            if (drI.Read())
            {
                int i = int.Parse(drI[0].ToString()) + 1;
                item_id = i.ToString("0000");
            }
            else if (Convert.IsDBNull(drI))
            {
                item_id = ("0001");
            }
            else
            {
                item_id = ("0001");
            }
            txticode.Text = item_id.ToString();
        }
        public void clear()
        {
            txticode.Text = "";
            txtiname.Text = "";
            txtsize.Text = "";
            txtbrand.Text = "";
            txtcostp.Text = "";
            txtsellingp.Text = "";
            txtqty.Text = "";
            txtstatus.Text = "";
        }


        private void btniadd_Click(object sender, EventArgs e)
        {
            if (txticode.Text == "" || txtiname.Text == "" || comboBox1.SelectedValue.ToString() == "--SELECT--" || txtsize.Text == "" || txtbrand.Text == "" || txtcostp.Text == "" || txtsellingp.Text == "" || txtqty.Text == "" || txtqty.Text == "" || txtstatus.Text == "")
            {
                MessageBox.Show("Please enter required fields and check the quantity", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                c = new connect();
                c.cmd.CommandText = "Select * from Inventory where Item_code='" + txticode.Text + "'";
                ds = new DataSet();
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "invnt");
                c.cmd.ExecuteNonQuery();
                if (ds.Tables["invnt"].Rows.Count > 0)
                {
                    MessageBox.Show("Item Code already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        c.cmd.CommandText = "Insert into Inventory values(@Item_code,@Item_name,@Category,@Size_Type,@Brand,@Cost_Price,@Selling_Price,@Quantity,@Status)";
                        c.cmd.Parameters.Clear();
                        c.cmd.Parameters.Add("@Item_code", SqlDbType.NVarChar).Value = txticode.Text;
                        c.cmd.Parameters.Add("@Item_name", SqlDbType.NVarChar).Value = txtiname.Text;
                        c.cmd.Parameters.Add("@Category", SqlDbType.NVarChar).Value = comboBox1.SelectedValue.ToString();
                        c.cmd.Parameters.Add("@Size_Type", SqlDbType.NVarChar).Value = txtsize.Text;
                        c.cmd.Parameters.Add("@Brand", SqlDbType.NVarChar).Value = txtbrand.Text;
                        c.cmd.Parameters.Add("@Cost_Price", SqlDbType.BigInt).Value = Convert.ToInt64(txtcostp.Text);
                        c.cmd.Parameters.Add("@Selling_Price", SqlDbType.BigInt).Value = Convert.ToInt64(txtsellingp.Text);
                        c.cmd.Parameters.Add("@Quantity", SqlDbType.BigInt).Value = Convert.ToInt64(txtqty.Text);
                        c.cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = txtstatus.Text; ;
                        c.cmd.ExecuteNonQuery();
                        MessageBox.Show("!!!...Item Inserted...!!!", "Inserted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        populate();
                        clear();
                        Generat_Item_Code();
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

        private void Inventory_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'managementDataSet7.Inventory' table. You can move, or remove it, as needed.
            this.inventoryTableAdapter.Fill(this.managementDataSet7.Inventory);
            // TODO: This line of code loads data into the 'managementDataSet6.Cat_tables' table. You can move, or remove it, as needed.
            this.cat_tablesTableAdapter.Fill(this.managementDataSet6.Cat_tables);
            Generat_Item_Code();

        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            Inventory invnt = new Inventory();
            invnt.Show();
            this.Close();
            clear();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (txticode.Text == "")
            {
                MessageBox.Show("Please enter Item Code", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    c = new connect();
                    c.cmd.CommandText = "Select * from Inventory where Item_code='" + txticode.Text + "'";
                    ds = new DataSet();
                    adp.SelectCommand = c.cmd;
                    adp.Fill(ds, "del");
                    if (ds.Tables["del"].Rows.Count > 0)
                    {

                        c.cmd.CommandText = "Delete  from Inventory where Item_code='" + txticode.Text + "'";
                        c.cmd.ExecuteNonQuery();
                        MessageBox.Show("!!!...Item Deleted...!!!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        populate();
                        //clear();
                    }
                    else
                    {
                        MessageBox.Show("Item code not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btncatadd_Click(object sender, EventArgs e)
        {
            this.Hide();
            Category cat = new Category();
            cat.Show();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.CurrentRow.Selected = true;
                txticode.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                txtiname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                //comboBox1.SelectedValue = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                txtsize.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                txtbrand.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                txtcostp.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                txtsellingp.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                txtqty.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                txtstatus.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
            }
        }

        private void txtcostp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Character, Spaces and Special Characters are not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtsellingp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Character, Spaces and Special Characters are not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Character, Spaces and Special Characters are not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtqty_TextChanged(object sender, EventArgs e)
        {
            if (txtqty.Text == "0")
            {
                txtstatus.Text = "Stock-Out";
            }
            else
            {
                txtstatus.Text = "Stock-In";
            }
            /*if (txtqty.Text == "0")
            {
                DialogResult res1 = MessageBox.Show("Quantity cannot be Zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (res1 == DialogResult.OK)
                {
                    txtqty.Clear();
                }
            }*/
        }

        private void txtsellingp_TextChanged(object sender, EventArgs e)
        {
            if (txtsellingp.Text == "0")
            {
                DialogResult res1 = MessageBox.Show("Selling Price cannot be Zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (res1 == DialogResult.OK)
                {
                    txtsellingp.Clear();
                }
            }
        }

        private void txtcostp_TextChanged(object sender, EventArgs e)
        {
            if (txtcostp.Text == "0")
            {
                DialogResult res1 = MessageBox.Show("Cost Price cannot be Zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (res1 == DialogResult.OK)
                {
                    txtcostp.Clear();
                }
            }
        }
    }

}  

            
        