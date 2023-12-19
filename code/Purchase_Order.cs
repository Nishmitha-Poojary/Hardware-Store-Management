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
    public partial class Purchase_Order : Form
    {
        connect c;
        DataSet ds;
        SqlDataAdapter adp = new SqlDataAdapter();
        DataTable tables = new DataTable();

        public Purchase_Order()
        {
            InitializeComponent();
        }
        public void Generat_Purchase_Order_Id()
        {
            c = new connect();
            string pur_id;
            string queryPOrdr = "select Pur_ordr_no from Purchase_ordr order by Pur_ordr_no Desc";
            SqlCommand cmdP = new SqlCommand(queryPOrdr, c.cnn);
            SqlDataReader drP = cmdP.ExecuteReader();
            if (drP.Read())
            {
                int i = int.Parse(drP[0].ToString()) + 1;
                pur_id = i.ToString("0000");
            }
            else if (Convert.IsDBNull(drP))
            {
                pur_id = ("0001");
            }
            else
            {
                pur_id = ("0001");
            }
            lblpurno.Text = pur_id.ToString();
        }
        public void clear()
        {
            lblpurno.Text = "";
            lblsupno.Text = "";
            lblsupname.Text = "";
            lblcompany.Text = "";
            lblitcode.Text = "";
            lblitname.Text = "";
            lblsizetype.Text = "";
            lblbrand.Text = "";
            txtqty.Text = "";
        }

        int flag = 0,flag1=0;
        private void Purchase_Order_Load(object sender, EventArgs e)
        {
            Generat_Purchase_Order_Id();
            flag = 0;
            flag1 = 0;
            // TODO: This line of code loads data into the 'managementDataSet_PItem.Inventory' table. You can move, or remove it, as needed.
            this.inventoryTableAdapter.Fill(this.managementDataSet_PItem.Inventory);
            // TODO: This line of code loads data into the 'managementDataSet_PSup.Supplier_details' table. You can move, or remove it, as needed.
            this.supplier_detailsTableAdapter.Fill(this.managementDataSet_PSup.Supplier_details);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.CurrentRow.Selected = true;
                lblsupno.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                lblsupname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                lblcompany.Text =dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                flag1 = 1;
            }
        }
        long quanty = 0;

        private void btninsertp_Click_1(object sender, EventArgs e)
        {
            if (flag1 == 0)
            {
                MessageBox.Show("Select the Supplier", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (flag == 0)
            {
                MessageBox.Show("Select the Product", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (lblpurno.Text == ""||txtqty.Text =="")
            {
                MessageBox.Show("Enter the Fields", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           else
            {
                c = new connect();
                c.cmd.CommandText = "Select * from Purchase_ordr where Pur_ordr_no='" + lblpurno.Text + "'";
                ds = new DataSet();
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "purint");
                c.cmd.ExecuteNonQuery();
                if (ds.Tables["purint"].Rows.Count > 0)
                {
                    MessageBox.Show("Purchase Order Number already exists","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        quanty = Convert.ToInt64(txtqty.Text);
                        c.cmd.CommandText = "Insert into Purchase_ordr values(@Pur_ordr_no,@Pur_date,@Sup_no,@Sup_name,@Company,@Item_code,@Item_name,@Size_Type,@Brand,@Quantity)";
                        c.cmd.Parameters.Clear();
                        c.cmd.Parameters.Add("@Pur_ordr_no", SqlDbType.NVarChar).Value = lblpurno.Text;
                        c.cmd.Parameters.Add("@Pur_date", SqlDbType.DateTime).Value = pur_date.Text;
                        c.cmd.Parameters.Add("@Sup_no", SqlDbType.NVarChar).Value = lblsupno.Text;
                        c.cmd.Parameters.Add("@Sup_name", SqlDbType.NVarChar).Value = lblsupname.Text;
                        c.cmd.Parameters.Add("@Company", SqlDbType.NVarChar).Value = lblcompany.Text;
                        c.cmd.Parameters.Add("@Item_code", SqlDbType.NVarChar).Value = lblitcode.Text;
                        c.cmd.Parameters.Add("@Item_name", SqlDbType.NVarChar).Value = lblitname.Text;
                        c.cmd.Parameters.Add("@Size_Type", SqlDbType.NVarChar).Value = lblsizetype.Text;
                        c.cmd.Parameters.Add("@Brand", SqlDbType.NVarChar).Value = lblbrand.Text;
                        c.cmd.Parameters.Add("@Quantity", SqlDbType.BigInt).Value = quanty;
                        c.cmd.ExecuteNonQuery();
                        MessageBox.Show("Purchase Order Inserted Successfull..!!!","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        clear();
                        Generat_Purchase_Order_Id();
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

        private void dataGridView2_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView2.CurrentRow.Selected = true;
                lblitcode.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                lblitname.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                lblsizetype.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
                lblbrand.Text = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();
                flag = 1;
            }
        }

        private void txtpurno_KeyPress(object sender, KeyPressEventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnviewp_Click(object sender, EventArgs e)
        {
            Purchase_ordr_record por = new Purchase_ordr_record();
            por.Show();
        }

        private void txtqty_TextChanged(object sender, EventArgs e)
        {
            if (txtqty.Text == "0")
            {
                DialogResult res1 = MessageBox.Show("Quantity cannot be Zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (res1 == DialogResult.OK)
                {
                    txtqty.Clear();
                }
            }
        }
    }
}