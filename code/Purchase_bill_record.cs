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
    public partial class Purchase_bill_record : Form
    {
        connect c;
        DataSet ds = new DataSet();
        SqlDataAdapter adp = new SqlDataAdapter();
        public Purchase_bill_record()
        {
            InitializeComponent();
        }
        public void populatebill()
        {
            try
            {
                c = new connect();
                string myquery = "Select * from Purchase_bill";
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

        private void Purchase_bill_record_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'managementDataSet_Pur_bill.Purchase_bill' table. You can move, or remove it, as needed.
            this.purchase_billTableAdapter.Fill(this.managementDataSet_Pur_bill.Purchase_bill);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string pay = "";
            if (e.RowIndex >=0)
            {
                dataGridView1.CurrentRow.Selected = true;
                if (dataGridView1.SelectedRows[0].Cells[12].Value.ToString() == "Un-Paid")
                {
                    DialogResult result = MessageBox.Show("Do you want to edit status to 'pay'....?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        c = new connect();
                        c.cmd.CommandText = "Select * from Purchase_bill where Pur_bill_no='" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "'";
                        ds = new DataSet();
                        adp.SelectCommand = c.cmd;
                        adp.Fill(ds, "Editpay");
                        c.cmd.ExecuteNonQuery();
                        if (ds.Tables["Editpay"].Rows.Count > 0)
                        {
                            c = new connect();
                            c.cmd.CommandText = "Update Purchase_bill set Status=@Status where Pur_bill_no='" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "'";
                            c.cmd.Parameters.Clear();
                            pay = "Paid";
                            c.cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = pay;
                            MessageBox.Show("Status Updated...!!!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            c.cmd.ExecuteNonQuery();
                            populatebill();
                        }
                    }
                    else if (result == DialogResult.No)
                    {
                        this.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Amount already Paid Can't be repaid...!!!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btneditsat_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please Select the Required Field to Edit the Status", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}