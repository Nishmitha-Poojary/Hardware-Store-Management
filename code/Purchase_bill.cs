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
    public partial class Purchase_bill : Form
    {
        connect c;
        DataSet ds;
        SqlDataAdapter adp = new SqlDataAdapter();
        DataTable tables = new DataTable();

        public Purchase_bill()
        {
            InitializeComponent();
        }
        public void Generat_Pur_Bill_Id()
        {
            c = new connect();
            string bill_id;
            string querypbill = "select Pur_bill_no from Purchase_bill order by Pur_bill_no Desc";
            SqlCommand cmdpb = new SqlCommand(querypbill, c.cnn);
            SqlDataReader drpb = cmdpb.ExecuteReader();
            if (drpb.Read())
            {
                long i = Convert.ToInt64(drpb[0].ToString()) + 1;
                bill_id = i.ToString("0000");
            }
            else if (Convert.IsDBNull(drpb))
            {
                bill_id = ("0001");
            }
            else
            {
                bill_id = ("0001");
            }
            lblpbillno.Text = bill_id.ToString();
        }



        private void Purchase_bill_Load(object sender, EventArgs e)
        {
            Generat_Pur_Bill_Id();
            lblpurno1.Text = myglobalpur.pur_id.ToString();
            lblsupid1.Text = myglobalpur.sup_id.ToString();
            lblsupname1.Text = myglobalpur.sup_name.ToString();
            lblcompany1.Text = myglobalpur.compny.ToString();
            lblitname1.Text = myglobalpur.itm_name.ToString();
            lblsizetype1.Text = myglobalpur.sz_typ.ToString();
            lblbrand1.Text = myglobalpur.brnd.ToString();
            lblqty1.Text = myglobalpur.pur_qty.ToString();
            rbpaid.Checked = false;
            rbunpaid.Checked = false;
            P_bill_dateTime.MaxDate = DateTime.Today;
            
        }
        public void clear()
        {
            P_bill_dateTime.Text = "";
            lblpurno1.Text = "";
            lblsupid1.Text = "";
            lblsupname1.Text = "";
            lblcompany1.Text = "";
            lblitname1.Text = "";
            lblsizetype1.Text = "";
            lblbrand1.Text = "";
            lblqty1.Text = "";
            txtcostprice.Text = "";
            txttotamt.Text = "";
            rbpaid.Checked = false;
            rbunpaid.Checked = false;

        }
        string sat = "", sat1 = "";
        long stock1 = 0;
        private void btnadd_Click(object sender, EventArgs e)
        {

            if (rbpaid.Checked == true)
            {
                sat = "Paid";
            }
            else
            {
                sat = "Un-Paid";
            }
            if (txtcostprice.Text == "" || txttotamt .Text  == "")
            {
                MessageBox.Show("Please Enter the Cost price", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (rbpaid.Checked == false && rbunpaid.Checked == false)
            {
                MessageBox.Show("Please Select the status", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            c = new connect();
            c.cmd.CommandText = "Select * from Purchase_bill where Pur_bill_no='" + lblpbillno.Text + "'";
            ds = new DataSet();
            adp.SelectCommand = c.cmd;
            adp.Fill(ds, "pbill");
            c.cmd.ExecuteNonQuery();
            if (ds.Tables["pbill"].Rows.Count > 0)
            {
                MessageBox.Show("Purchase bill id already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                c.cmd.CommandText = "Select * from Purchase_bill where Pur_ordr_no='" + lblpurno1.Text + "'";
                ds = new DataSet();
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "pbillo");
                c.cmd.ExecuteNonQuery();
                if (ds.Tables["pbillo"].Rows.Count > 0)
                {
                    MessageBox.Show("Purchase order id already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {

                        string query = "Select * from Inventory where Item_code='" + myglobalpur.it_code + "' ";
                        DataTable dt = new DataTable();
                        SqlCommand cmd = new SqlCommand(query, c.cnn);
                        SqlDataAdapter adp11 = new SqlDataAdapter(cmd);
                        adp11.Fill(dt);
                        foreach (DataRow dr in dt.Rows)
                        {
                            stock1 = Convert.ToInt64(dr["Quantity"]);
                        }
                        long reqty = 0;
                        reqty = stock1 + Convert.ToInt64(lblqty1.Text);
                        if (reqty <= 0)
                        {
                            MessageBox.Show("Operation Failed","Error",MessageBoxButtons .OK,MessageBoxIcon.Error);
                        }
                        else
                        {
                            if (reqty > 0)
                            {
                                sat1 = "Stock-In";
                            }
                            else
                            {
                                sat1 = "Stock-Out";
                            }
                            c.cmd.CommandText = "Update Inventory set Quantity=@Quantity,Cost_Price=@Cost_Price,Status=@Status where Item_code='" + myglobalpur.it_code + "'";
                            c.cmd.Parameters.Clear();
                            c.cmd.Parameters.Add("@Quantity", SqlDbType.BigInt).Value = Convert.ToInt64(reqty);
                            c.cmd.Parameters.Add("@Cost_Price", SqlDbType.BigInt).Value = Convert.ToInt64(txtcostprice.Text);
                            c.cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = sat1;
                            c.cmd.ExecuteNonQuery();

                            c.cmd.CommandText = "Insert into Purchase_bill values(@Pur_bill_no,@Pur_bill_date,@Pur_ordr_no,@Sup_no,@Sup_name,@Company,@Item_name,@Size_Type,@Brand,@Quantity,@Price,@Pur_amt,@Status)";
                            c.cmd.Parameters.Clear();
                            c.cmd.Parameters.Add("@Pur_bill_no", SqlDbType.NVarChar).Value = lblpbillno.Text;
                            c.cmd.Parameters.Add("@Pur_bill_date", SqlDbType.DateTime).Value = P_bill_dateTime.Text;
                            c.cmd.Parameters.Add("@Pur_ordr_no", SqlDbType.NVarChar).Value = lblpurno1.Text;
                            c.cmd.Parameters.Add("@Sup_no", SqlDbType.NVarChar).Value = lblsupid1.Text;
                            c.cmd.Parameters.Add("@Sup_name", SqlDbType.NVarChar).Value = lblsupname1.Text;
                            c.cmd.Parameters.Add("@Company", SqlDbType.NVarChar).Value = lblcompany1.Text;
                            c.cmd.Parameters.Add("@Item_name", SqlDbType.NVarChar).Value = lblitname1.Text;
                            c.cmd.Parameters.Add("@Size_Type", SqlDbType.NVarChar).Value = lblsizetype1.Text;
                            c.cmd.Parameters.Add("@Brand", SqlDbType.NVarChar).Value = lblbrand1.Text;
                            c.cmd.Parameters.Add("@Quantity", SqlDbType.BigInt).Value = Convert.ToInt64(lblqty1.Text);
                            c.cmd.Parameters.Add("@Price", SqlDbType.BigInt).Value = Convert.ToInt64(txtcostprice.Text);
                            c.cmd.Parameters.Add("@Pur_amt", SqlDbType.BigInt).Value = tota;
                            c.cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = sat.ToString();
                            c.cmd.ExecuteNonQuery();
                            clear();
                            MessageBox.Show("Items are added to stock and the bill is recorded successfully..!!!", "Successfull", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Generat_Pur_Bill_Id();
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
        }


        private void txtcostprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Character, Spaces and Special Characters are not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        long tota = 0;

        private void txtcostprice_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtcostprice.Text == "0")
                {
                    MessageBox.Show("Cost price cannot be zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtcostprice.Text = "";
                }
                else
                {
                    tota = Convert.ToInt64(lblqty1.Text) * Convert.ToInt64(txtcostprice.Text);
                    txttotamt.Text = tota.ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Cost price cannot be blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtcostprice.Text = "";
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtcostprice_TextChanged(object sender, EventArgs e)
        {
            if (txtcostprice.Text == "0")
            {
                DialogResult res1 = MessageBox.Show("Cost Price cannot be Zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (res1 == DialogResult.OK)
                {
                    txtcostprice.Clear();
                }
            }
        }
    }
}
           
  

