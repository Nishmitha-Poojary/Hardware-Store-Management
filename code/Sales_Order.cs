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
    public partial class Sales_Order : Form
    {
        connect c;
        DataSet ds;
        SqlDataAdapter adp = new SqlDataAdapter();
        DataTable tables = new DataTable();

        public Sales_Order()
        {
            InitializeComponent();
        }
        public void populateStock()
        {
            try
            {
                c = new connect();
                string myquery = "Select * from Inventory";
                SqlDataAdapter da = new SqlDataAdapter(myquery, c.cnn);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds);
                dataGridView2.DataSource = ds.Tables[0];

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
        public void clear()
        {
            txtqty.Text = "";
            txtphno.Text = "";
            txtorderid.Text = "";
            lblcustit.Text = "";
            lblcustname.Text = "";
            Order_date.Text = "";
            lbltotalamt.Text = "Rs. 0";
        }
        private void fetchdata()
        {
            try
            {
                c = new connect();
                string query1 = "Select * from Cust_details where Phone_no='" + txtphno.Text + "' ";
                DataTable dt1 = new DataTable();
                SqlCommand cmd1 = new SqlCommand(query1, c.cnn);
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                adp1.Fill(dt1);
                foreach (DataRow dr1 in dt1.Rows)
                {
                    lblcustit.Text = dr1["Cust_id"].ToString();
                    lblcustname.Text = dr1["Cust_name"].ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(" Phone Number Does Not Exits","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            finally
            {
                c.cnn.Close();
            }
        }
        public void updateproduct()
        {
            string sat = "";
            c = new connect();
            string id = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
            long newqty = stock - Convert.ToInt64(txtqty.Text);
            c.cmd.CommandText = "Update Inventory set Quantity=@Quantity,Status=@Status where Item_code='" + id + "'";
            c.cmd.Parameters.Clear();
            c.cmd.Parameters.Add("@Quantity", SqlDbType.BigInt).Value = Convert.ToInt64(newqty);
            if (newqty== 0)
            {
                sat = "Stock-Out";
            }
            else
            {
                sat = "Stock-In";
            }
            c.cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = sat;
            c.cmd.ExecuteNonQuery();
            populateStock();

        }
        public void Generat_Order_Id()
        {
            c = new connect();
            string ordr_id;
            string queryOrdr = "select Sales_id from Sales_order order by Sales_id Desc";
            SqlCommand cmdO = new SqlCommand(queryOrdr, c.cnn);
            SqlDataReader drO = cmdO.ExecuteReader();
            if (drO.Read())
            {
                int i = int.Parse(drO[0].ToString()) + 1;
                ordr_id = i.ToString("0000");
            }
            else if (Convert.IsDBNull(drO))
            {
                ordr_id = ("0001");
            }
            else
            {
                ordr_id = ("0001");
            }
            txtorderid.Text= ordr_id.ToString();
        }


        private void Sales_Order_Load(object sender, EventArgs e)
        {
            populateStock();
            Generat_Order_Id();
            flag = 0;
            lbltotalamt.Text = "Rs. 0";
            // TODO: This line of code loads data into the 'managementDataSet9.Inventory' table. You can move, or remove it, as needed.
            this.inventoryTableAdapter.Fill(this.managementDataSet9.Inventory);
            tables.Columns.Add("Order_id", typeof(int));
            tables.Columns.Add("Item_code", typeof(string));
            tables.Columns.Add("Selling_price", typeof(int));
            tables.Columns.Add("Qty", typeof(int));
            tables.Columns.Add("Total_price", typeof(int));
            dataGridView1.DataSource = tables;
        }

        long sum=0;
        long uprice, totprice, qty;
        string product,num;
        int flag = 0;
        private void btnadd_Click(object sender, EventArgs e)
        {

            if (txtqty.Text == ""||txtqty.Text =="0")
            {
                MessageBox.Show("Enter the Quantity of the Product", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtqty.Text = "";
            }
            else if (txtqty.Text == "0")
            {
                MessageBox.Show("Quantity cannot be Zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtqty.Text = "";
            }
            else if (flag == 0)
            {
                MessageBox.Show("Select the Product", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtorderid.Text == "" || lblcustname.Text == "" || lblcustit.Text == "")
            {
                MessageBox.Show("Enter the Fields", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Convert.ToInt64(txtqty.Text) > stock)
            {
                MessageBox.Show("Not enough Stock Available", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtqty.Text = "";
            }

            else
            {
                c = new connect();
                c.cmd.CommandText = "Select * from Sales_order where Sales_id='" + txtorderid.Text + "'";
                ds = new DataSet();
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "order1");
                c.cmd.ExecuteNonQuery();
                if (ds.Tables["order1"].Rows.Count > 0)
                {
                    MessageBox.Show("Sales Id already exists","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else
                {
                    num = txtorderid.Text;
                    qty = Convert.ToInt64(txtqty.Text);
                    totprice = qty * uprice;
                    c = new connect();
                    tables.Rows.Add(num, product, uprice, qty, totprice);
                    dataGridView1.DataSource = tables;
                    flag = 0;
                    updateproduct();
                    txtqty.Text = "";
                    sum = sum + totprice;
                    lbltotalamt.Text = "Rs. " + sum.ToString();
                  
                }
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            if (txtphno.Text == "")
            {
                MessageBox.Show("Please enter Phone Number", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtphno.TextLength < 10)
            {
                MessageBox.Show("Contact Number cannot be Less than 10", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                c = new connect();
                c.cmd.CommandText = "Select * from Cust_details where Phone_no='" + txtphno.Text + "' ";
                ds = new DataSet();
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "phono");
                c.cmd.ExecuteNonQuery();
                if (ds.Tables["phono"].Rows.Count > 0)
                {
                    try
                    {
                        fetchdata();
                        lbltotalamt.Text = "Rs. 0";
                        txtqty.Text = "";
                        Order_date.Text = "";
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
                    else
                    {
                        myphone.telephone =txtphno.Text;
                        MessageBox.Show(" Phone Number Does Not Exits", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Customer_Records cust = new Customer_Records();
                        cust.Show();
                    }
                }
                
            }
        long stock = 0;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView2.CurrentRow.Selected = true;
                product = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                uprice = Convert.ToInt64(dataGridView2.SelectedRows[0].Cells[4].Value.ToString());
                stock = Convert.ToInt64(dataGridView2.SelectedRows[0].Cells[5].Value.ToString());
                flag = 1;
            }
        }

        private void btninsert_Click_1(object sender, EventArgs e)
        {
            if (txtorderid.Text == "" || lblcustname.Text == "" || lblcustit.Text == "")
            {
                MessageBox.Show("Enter the Fields", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                c = new connect();
                c.cmd.CommandText = "Select * from Sales_order where Sales_id='" + txtorderid.Text + "'";
                ds = new DataSet();
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "order");
                c.cmd.ExecuteNonQuery();
                if (ds.Tables["order"].Rows.Count > 0)
                {
                    MessageBox.Show("Sales Id already exists","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        c.cmd.CommandText = "Insert into Sales_order values(@Sales_id,@Cust_id,@Cust_name,@Ordr_date,@Total_amount)";
                        c.cmd.Parameters.Clear();
                        c.cmd.Parameters.Add("@Sales_id", SqlDbType.NVarChar).Value =txtorderid.Text;
                        c.cmd.Parameters.Add("@Cust_id", SqlDbType.NVarChar).Value = lblcustit.Text;
                        c.cmd.Parameters.Add("@Cust_name", SqlDbType.NVarChar).Value = lblcustname.Text;
                        c.cmd.Parameters.Add("@Ordr_date", SqlDbType.DateTime).Value = Order_date.Text;
                        c.cmd.Parameters.Add("@Total_amount", SqlDbType.BigInt).Value = sum;
                        c.cmd.ExecuteNonQuery();
                        MessageBox.Show("Order Inserted Successfull..!!!","Inserted",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        clear();
                        Generat_Order_Id();
                        tables.Rows.Clear();
                        dataGridView1.DataSource = tables;
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

        private void btnview_Click(object sender, EventArgs e)
        {
            Sales_Order_Report sor = new Sales_Order_Report();
            sor.Show();
        }

        string prod = "",orid="";
        long sp = 0, q = 0, tot = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.CurrentRow.Selected = true;
                orid=dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                prod =dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                sp = Convert.ToInt64(dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
                q = Convert.ToInt64(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
                tot = Convert.ToInt64(dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
                flag = 1;
            }
        }

        long st1;
        private void btndelete_Click(object sender, EventArgs e)
        {
            if (flag == 0)
            {
                MessageBox.Show("Select the order to delete","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                c = new connect();
                string query5 = "Select * from Inventory where Item_code='" + prod + "' ";
                DataTable dt5 = new DataTable();
                SqlCommand cmd5 = new SqlCommand(query5, c.cnn);
                SqlDataAdapter adp5 = new SqlDataAdapter(cmd5);
                adp5.Fill(dt5);
                foreach (DataRow dr5 in dt5.Rows)
                {
                    st1 = Convert.ToInt64(dr5["Quantity"]);
                }
                long reqty = 0;
                reqty = st1 + q;
                c.cmd.CommandText = "Update Inventory set Quantity=@Quantity where Item_code='" + prod + "'";
                c.cmd.Parameters.Clear();
                c.cmd.Parameters.Add("@Quantity", SqlDbType.BigInt).Value = Convert.ToInt64(reqty);
                c.cmd.ExecuteNonQuery();
                populateStock();
                int rowindex = dataGridView1.CurrentCell.RowIndex;
                dataGridView1.Rows.RemoveAt(rowindex);
                sum = sum - (q * sp);
                lbltotalamt.Text = "Rs. " + sum.ToString();
                flag = 0;
            }
        }

        private void txtphno_TextChanged(object sender, EventArgs e)
        {
            if (txtphno.TextLength== 10)
            {
                txtphno.ForeColor = Color.Black;
            }
            else
            {
                txtphno.ForeColor = Color.Red;
            }
        }

        private void txtcustid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Character, Spaces and Special Characters are not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtcustname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Numbers and Special Characters are not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txttotal_KeyPress(object sender, KeyPressEventArgs e)
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
                DialogResult res1 = MessageBox.Show("Quantity cannot be Zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (res1 == DialogResult.OK)
                {
                    txtqty.Clear();
                }
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            if (lbltotalamt.Text== "Rs. 0")
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Please Remove the Order Before Closing the Form", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}

public class myphone
{
    public static string telephone="";
}



