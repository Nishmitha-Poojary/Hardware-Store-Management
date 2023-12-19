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
    public partial class Sales_Bill : Form
    {
        connect c;
        SqlDataAdapter adp = new SqlDataAdapter();
        DataSet ds;
        DataTable dt = new DataTable();
        public Sales_Bill()
        {
            InitializeComponent();
        }
        public void populatebill()
        {
            try
            {
                c = new connect();
                string myquery = "Select * from Sales_bill";
                SqlDataAdapter da = new SqlDataAdapter(myquery, c.cnn);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void clear()
        {
            txtbillno.Text = "";
            lblsalesid.Text = "";
            lbltotal.Text = "";
            bill_date.Text = "";
            paid.Checked = false;
            unpaid.Checked = false;
        }
        public void Generat_Sales_bill_Id()
        {
            c = new connect();
            string b_id;
            string querysb = "select bill_id from Sales_bill order by Bill_id Desc";
            SqlCommand cmdsb = new SqlCommand(querysb, c.cnn);
            SqlDataReader drsb = cmdsb.ExecuteReader();
            if (drsb.Read())
            {
                int i = int.Parse(drsb[0].ToString()) + 1;
                b_id = i.ToString("0000");
            }
            else if (Convert.IsDBNull(drsb))
            {
                b_id = ("0001");
            }
            else
            {
                b_id = ("0001");
            }
            txtbillno.Text = b_id.ToString();
        }

        int flag = 0;
        private void Sales_Bill_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'managementDataSet10.Sales_bill' table. You can move, or remove it, as needed.
            this.sales_billTableAdapter.Fill(this.managementDataSet10.Sales_bill);
            lblsalesid.Text = myglobal.order_id.ToString();
            lbltotal.Text  = myglobal.total.ToString();
            flag =0;
            Generat_Sales_bill_Id();
            bill_date.MaxDate = DateTime.Today;
        }

        string p = "";
        private void btnadd_Click(object sender, EventArgs e)
        {
            if(paid.Checked==true )
            {
                p="Paid" ;
            }
            else 
            {
                p="Un-Paid";
            }
            if (txtbillno.Text == "")
            {
                MessageBox.Show("Enter the Quantity of the Product", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                  else if(paid .Checked ==false && unpaid.Checked ==false)
            {
                MessageBox.Show("Please select the status", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
            else
            {
                c = new connect();
                c.cmd.CommandText = "Select * from Sales_bill where Bill_id='" +txtbillno.Text+"'or Sales_id='" +lblsalesid.Text+ "'";
                ds = new DataSet();
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "bill");
                c.cmd.ExecuteNonQuery();
                if (ds.Tables["bill"].Rows.Count > 0)
                {
                    MessageBox.Show("Sales Id already exists","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        c.cmd.CommandText = "Insert into Sales_bill values(@Bill_id,@Sales_id,@Bill_date,@Total_Amount,@Status)";
                        c.cmd.Parameters.Clear();
                        c.cmd.Parameters.Add("@Bill_id", SqlDbType.NVarChar).Value =txtbillno.Text;
                        c.cmd.Parameters.Add("@Sales_id", SqlDbType.NVarChar).Value = lblsalesid.Text;
                        c.cmd.Parameters.Add("@Bill_date", SqlDbType.DateTime).Value =bill_date.Text ;
                        c.cmd.Parameters.Add("@Total_Amount", SqlDbType.BigInt).Value =Convert.ToInt64(lbltotal.Text);
                        c.cmd.Parameters.Add("@status", SqlDbType.NVarChar).Value = p.ToString();
                        c.cmd.ExecuteNonQuery();
                        MessageBox.Show("!!!...Sales Bill Inserted...!!!","Inserted",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        populatebill();
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

       /* private void buttonedit_Click(object sender, EventArgs e)
        {
            if(paid.Checked==true )
            {
                p="Paid" ;
            }
            else 
            {
                p="Un-Paid";
            }
            if(flag ==0)
            {
                MessageBox .Show("Select the bill record to Update","Select",MessageBoxButtons.OK,MessageBoxIcon.Information );
            }
            else 
            {
            try
            {
                c = new connect();
                c.cmd.CommandText = "Select * from Sales_bill where Bill_id='" + txtbillno.Text + "'";
                ds = new DataSet();
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "upbill");
                c.cmd.ExecuteNonQuery();
                if (ds.Tables["upbill"].Rows.Count > 0)
                {
                    c.cmd.CommandText = "Update Sales_bill set Status=@Status where Bill_id='" + txtbillno.Text + "'";
                    c.cmd.Parameters.Clear();
                    c.cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = p.ToString();
                    c.cmd.ExecuteNonQuery();
                    MessageBox.Show("Updated..!!!","Updated",MessageBoxButtons .OK,MessageBoxIcon.Information);
                    populatebill();
                    clear();
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
        }*/

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             if (e.RowIndex >= 0)
            {
                dataGridView1.CurrentRow.Selected = true;
                txtbillno.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                lblsalesid .Text= dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                bill_date.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                lbltotal.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                paid.Checked =false ;
                 unpaid .Checked=false ;
                 flag =1;
             }
        }
    }
}
