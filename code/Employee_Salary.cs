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
    public partial class Employee_Salary : Form
    {
        connect c;
        DataSet ds;
        SqlDataAdapter adp = new SqlDataAdapter();
        string p = "";
        public Employee_Salary()
        {
            InitializeComponent();
        }
       public void populate()
        {
            try
            {
                c = new connect();
                string myquery = "Select * from Emp_salary";
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
        private void fetchdata()
        {
            c = new connect();
            string query1 = "Select * from Emp_details where Emp_Id='" + txtempid .Text + "' ";
            DataTable dt1 = new DataTable();
            SqlCommand cmd1 = new SqlCommand(query1, c.cnn);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            adp1.Fill(dt1);
            foreach (DataRow dr1 in dt1.Rows)
            {
                txtempid.Text = dr1["Emp_id"].ToString();
                lblname .Text = dr1["Emp_name"].ToString();
            }
            /*string query2 = "Select * from Emp_attendance where Emp_Id='" + txtempid.Text + "' ";
            DataTable dt2 = new DataTable();
            SqlCommand cmd2 = new SqlCommand(query2, c.cnn);
            SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
            adp2.Fill(dt2);
            foreach (DataRow dr2 in dt2.Rows)
            {
                lblyear.Text = dr2["Year"].ToString();
                lblmonth.Text = dr2["Month"].ToString();
                lblwork.Text = dr2["Working_days"].ToString();
                lblleave.Text = dr2["Leave_taken"].ToString();
            } */
        }
        public void clear()
        {
            txtempid.Text = "";
            lblname.Text = "";
            lblyear.Text = "";
            lblmonth.Text = "";
            lblwork.Text = "";
            lblleave.Text = "";
            lblper.Text = "500";
            lbldeduct.Text = "";
            lblnet.Text = "";
           
        }

        private void Employee_Salary_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'managementDataSetEmp_sal.Emp_salary' table. You can move, or remove it, as needed.
            this.emp_salaryTableAdapter.Fill(this.managementDataSetEmp_sal.Emp_salary);
            txtempid.Text = myempattnd.empid.ToString() ;
            lblyear.Text = myempattnd.yr.ToString() ;
            lblmonth.Text = myempattnd.month;
            lblwork.Text = myempattnd.wrdy.ToString() ;
            lblleave.Text = myempattnd.ltaken.ToString() ;

           
            lblper.Text = "500";
            btncal.Enabled = false;
           
        }

        private void btnfetch_Click(object sender, EventArgs e)
        {
            if (txtempid.Text == "")
            {
                MessageBox.Show("Please enter Employee ID", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    c = new connect();
                    c.cmd.CommandText = "Select * from Emp_attendance where Emp_id='" + txtempid.Text + "'";
                    ds = new DataSet();
                    adp.SelectCommand = c.cmd;
                    adp.Fill(ds, "sal1");
                    c.cmd.ExecuteNonQuery();
                    if (ds.Tables["sal1"].Rows.Count > 0)
                    {
                        fetchdata();
                        lbldeduct.Text = "";
                        lblnet.Text = "";
                        btncal.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Please Select the Records from Attendance Tables", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btncal_Click(object sender, EventArgs e)
        {
            if (txtempid.Text == "" || lblleave.Text == "" || lblwork.Text == "")
            {
                MessageBox .Show("Fetch The Data First...!!!");
            }
            else
            {
                int ded = 0, net = 0;
                ded = int.Parse(lblleave.Text) * int.Parse(lblper.Text);
                lbldeduct.Text = ded.ToString();
                net = int.Parse(lblwork.Text) * int.Parse(lblper.Text);
                lblnet.Text = net.ToString();
            }

        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (txtempid.Text == "")
            {
                MessageBox.Show("Please enter Employee ID or Enter Status", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(lblnet.Text =="")
            {
                MessageBox.Show ("Please calculate the Net Pay","Required",MessageBoxButtons.OK ,MessageBoxIcon.Error); 
            }
          
            else
            {
               try
                {
                    c = new connect();
                    c.cmd.CommandText = "Select * from Emp_salary where Emp_id='" + txtempid.Text + "' and Year='" +lblyear.Text + "'and Month='" +lblmonth .Text+ "'";
                    ds = new DataSet();
                    adp.SelectCommand = c.cmd;
                    adp.Fill(ds, "sall");
                    c.cmd.ExecuteNonQuery();
                    if (ds.Tables["sall"].Rows.Count > 0)
                    {
                        MessageBox.Show("Employee Salary record already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                    else
                    {
                        c.cmd.CommandText = "Insert into Emp_salary values(@Emp_id,@Emp_name,@Year,@Month,@Worked_days,@Leave_taken,@Sal_deduct,@Net)";
                        c.cmd.Parameters.Clear();
                        c.cmd.Parameters.Add("@Emp_id", SqlDbType.NVarChar).Value = txtempid.Text;
                        c.cmd.Parameters.Add("@Emp_name", SqlDbType.NVarChar).Value = lblname.Text;
                        c.cmd.Parameters.Add("@Year", SqlDbType.BigInt).Value = Convert.ToInt64(lblyear.Text);
                        c.cmd.Parameters.Add("@Month", SqlDbType.NVarChar).Value = lblmonth.Text;
                        c.cmd.Parameters.Add("@Worked_days", SqlDbType.BigInt).Value = Convert.ToInt64(lblwork.Text);
                        c.cmd.Parameters.Add("@Leave_taken", SqlDbType.BigInt).Value = Convert.ToInt64(lblleave.Text);
                        c.cmd.Parameters.Add("@Sal_deduct", SqlDbType.BigInt).Value = Convert.ToInt64(lbldeduct.Text);
                        c.cmd.Parameters.Add("@Net", SqlDbType.BigInt).Value = Convert.ToInt64(lblnet.Text);

                        c.cmd.ExecuteNonQuery();
                        MessageBox.Show("Record Inserted..!!!", "Inserted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        populate();
                        clear();
                    }
                }
                catch (Exception)
                 {
                    MessageBox.Show("Employee Id doesn't exists","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                 }
                finally
                {
                    c.cnn.Close();
                }
            }
        }


        private void btnclear_Click(object sender, EventArgs e)
        {
            clear();
        }

       

        /*private void btndelete_Click(object sender, EventArgs e)
        {
            if (txtempid.Text == "")
            {
                MessageBox.Show("Please enter Employee Id", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    c = new connect();
                    c.cmd.CommandText = "Select * from Emp_salary where Emp_id='" + txtempid.Text+"'";
                    ds = new DataSet();
                    adp.SelectCommand = c.cmd;
                    adp.Fill(ds, "del");
                    if (ds.Tables["del"].Rows.Count > 0)
                    {
                        //int rowindex = dataGridView1.CurrentCell.RowIndex;
                        //dataGridView1.Rows.RemoveAt(rowindex);
                        c.cmd.CommandText = "Delete  from Emp_salary where Emp_id='" + txtempid.Text+"'";
                        c.cmd.ExecuteNonQuery();
                        MessageBox.Show("Record Deleted..!!!","Deleted",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        populate();
                        clear();
                    }
                    else
                    {
                        MessageBox.Show("Record not found","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
        }*/

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.CurrentRow.Selected = true;
                txtempid.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                lblname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                lblyear.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                lblmonth.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                lblwork.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                lblleave.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                lbldeduct.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                lblnet.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            }

        }

        private void txtempid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Character, Spaces and Special Characters are not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
            clear();
            this.Close();
        }

       

    }
}
