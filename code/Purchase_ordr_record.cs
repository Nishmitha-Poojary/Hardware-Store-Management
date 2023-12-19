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
    public partial class Purchase_ordr_record : Form
    {
        connect c;
        DataSet ds;
        SqlDataAdapter adp = new SqlDataAdapter();
        public Purchase_ordr_record()
        {
            InitializeComponent();
        }
        public void populatePur()
        {
            try
            {
                c = new connect();
                string myquery = "Select * from Purchase_ordr";
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
        int flag = 0;
        long pur_no =0;

        private void Purchase_ordr_record_Load(object sender, EventArgs e)
        {
            flag = 0;
            // TODO: This line of code loads data into the 'managementDataSetPur_ordr.Purchase_ordr' table. You can move, or remove it, as needed.
            this.purchase_ordrTableAdapter.Fill(this.managementDataSetPur_ordr.Purchase_ordr);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.CurrentRow.Selected = true;
                myglobalpur .pur_id=dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                myglobalpur.sup_id = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                myglobalpur.sup_name = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                myglobalpur.compny = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                myglobalpur.it_code = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                myglobalpur.itm_name = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                myglobalpur.sz_typ = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                myglobalpur.brnd = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
                myglobalpur.pur_qty  = Convert.ToInt64(dataGridView1.SelectedRows[0].Cells[9].Value.ToString());
                flag = 1;
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (flag == 0)
            {
                MessageBox.Show("Select the Purchase order to be deleted", "SELECT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    pur_no = Convert.ToInt64(myglobalpur.pur_id);
                    c = new connect();
                    c.cmd.CommandText = "Select * from Purchase_ordr where Pur_ordr_no='" +myglobalpur.pur_id+ "'";
                    ds = new DataSet();
                    adp.SelectCommand = c.cmd;
                    adp.Fill(ds, "delp");
                    if (ds.Tables["delp"].Rows.Count > 0)
                    {

                        c.cmd.CommandText = "Delete  from Purchase_ordr where Pur_ordr_no='" +myglobalpur.pur_id+ "'";
                        c.cmd.ExecuteNonQuery();
                        MessageBox.Show("Order Deleted..!!!", "DELETED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        populatePur();
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

        private void btngenaratebill_Click(object sender, EventArgs e)
        {
            if (flag == 0)
            {
                MessageBox.Show("Select the Purchase order to generate bill", "SELECT", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Purchase_bill pb1 = new Purchase_bill();
                pb1.Show();
            }
        }
    }
}
public static class myglobalpur
{
    public static long pur_qty = 0;
    public static string pur_id = "",sup_id="",sup_name="",compny="",it_code="",itm_name="",sz_typ="",brnd="";
}
