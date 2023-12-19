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
    public partial class Attendance1 : Form
    {
        connect c;
        DataSet ds;
        SqlDataAdapter adp = new SqlDataAdapter();
        public Attendance1()
        {
            InitializeComponent();
        }
        public void populate()
        {
            try
            {
                c = new connect();
                string myquery = "Select * from Emp_attendance";
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
            txteid.Text = "";
            txtyear.Text = "";
            months.SelectedIndex = 0;
            lbldays.Text = "<<Click>>";
            lbltotalwork.Text = "";
            txtwork.Text = "";
            lbllassign.Text = "";
            lblltaken.Text = "<<Click>>";
        }
        public void Monthdays()
        {
            if (txtyear.Text == "")
            {
                MessageBox.Show("Please Enter the Year");
            }
            else
            {
                try
                {
                    int month = months.SelectedIndex;
                    int year = Convert.ToInt16(txtyear.Text);
                    int day_in_month = 0;
                    day_in_month = System.DateTime.DaysInMonth(year, month);
                    lbldays.Text = day_in_month.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (txteid.Text == "" || txtyear.Text == "" || lbldays.Text == "" || txtwork.Text == "" || lbllassign.Text == "" || lblltaken.Text == "")
            {
                MessageBox.Show("Please enter required fields", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                c = new connect();
                c.cmd.CommandText = "Select * from Emp_attendance where Emp_id='" + txteid.Text + "' and Year='" + txtyear.Text + "'and Month='" + months.SelectedItem + "'";
                ds = new DataSet();
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "atn");
                c.cmd.ExecuteNonQuery();
                if (ds.Tables["atn"].Rows.Count > 0)
                {
                    MessageBox.Show("Employee Attendance record already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (int.Parse(txtwork.Text) + int.Parse(lblltaken.Text) == int.Parse(lbldays.Text) || (int.Parse(txtwork.Text) + int.Parse(lblltaken.Text) >= int.Parse(lbltotalwork.Text) && int.Parse(txtwork.Text) + int.Parse(lblltaken.Text) <= int.Parse(lbldays.Text)))
                    {
                        try
                        {
                            c.cmd.CommandText = "Insert into Emp_attendance values(@Emp_id,@Year,@Month,@Total_days,@Working_days,@Leave_assign,@Leave_taken)";
                            c.cmd.Parameters.Clear();
                            c.cmd.Parameters.Add("@Emp_id", SqlDbType.NVarChar).Value = txteid.Text;
                            c.cmd.Parameters.Add("@Year", SqlDbType.BigInt).Value = Convert.ToInt64(txtyear.Text);
                            c.cmd.Parameters.Add("@Month", SqlDbType.NVarChar).Value = months.SelectedItem.ToString();
                            c.cmd.Parameters.Add("@Total_days", SqlDbType.BigInt).Value = Convert.ToInt64(lbltotalwork.Text);
                            c.cmd.Parameters.Add("@Working_days", SqlDbType.BigInt).Value = Convert.ToInt64(txtwork.Text);
                            c.cmd.Parameters.Add("@Leave_assign", SqlDbType.BigInt).Value = Convert.ToInt64(lbllassign.Text);
                            c.cmd.Parameters.Add("@Leave_taken", SqlDbType.BigInt).Value = Convert.ToInt64(lblltaken.Text);
                            c.cmd.ExecuteNonQuery();
                            MessageBox.Show("Record Inserted..!!!", "Inserted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            populate();
                            clear();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Employee Id Does not Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            c.cnn.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid number of days", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /*private void btnedit_Click(object sender, EventArgs e)
        {
            if (txteid.Text == "" || lbldays.Text == "<<Click>>" || txtyear.Text == "" || txtwork.Text == "" || lbllassign.Text == "" || lblltaken.Text == "<<Click>>")
            {
                MessageBox.Show("Please enter required fields", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (int.Parse(txtwork.Text) + int.Parse(lblltaken.Text) == int.Parse(lbldays.Text) || (int.Parse(txtwork.Text) + int.Parse(lblltaken.Text) >= int.Parse(lbltotalwork.Text) && int.Parse(txtwork.Text) + int.Parse(lblltaken.Text) <= int.Parse(lbldays.Text)))
                {
                    try
                    {
                        c = new connect();
                        c.cmd.CommandText = "Select * from Emp_attendance where Emp_id='" + txteid.Text + "' and Year='" + txtyear.Text + "'and Month='" + months.SelectedItem + "'";
                        ds = new DataSet();
                        adp.SelectCommand = c.cmd;
                        adp.Fill(ds, "aedit");
                        c.cmd.ExecuteNonQuery();
                        if (ds.Tables["aedit"].Rows.Count > 0)
                        {
                            MessageBox.Show("Employee Attendance record already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (ds.Tables["aedit"].Rows.Count < 0)
                        {
                            c.cmd.CommandText = "Update Emp_attendance set Year=@Year,Month=@Month,Total_days=@Total_days,Working_days=@Working_days,Leave_assign=@Leave_assign,Leave_taken=@Leave_taken where Emp_id='" + txteid.Text + "'";
                            c.cmd.Parameters.Clear();
                            c.cmd.Parameters.Add("@Emp_id", SqlDbType.NVarChar).Value = txteid.Text;
                            c.cmd.Parameters.Add("@Year", SqlDbType.BigInt).Value = Convert.ToInt64(txtyear.Text);
                            c.cmd.Parameters.Add("@Month", SqlDbType.NVarChar).Value = months.SelectedItem;
                            c.cmd.Parameters.Add("@Total_days", SqlDbType.BigInt).Value = Convert.ToInt64(lbltotalwork.Text);
                            c.cmd.Parameters.Add("@Working_days", SqlDbType.BigInt).Value = Convert.ToInt64(txtwork.Text);
                            c.cmd.Parameters.Add("@Leave_assign", SqlDbType.BigInt).Value = Convert.ToInt64(lbllassign.Text);
                            c.cmd.Parameters.Add("@Leave_taken", SqlDbType.BigInt).Value = Convert.ToInt64(lblltaken.Text);
                            c.cmd.ExecuteNonQuery();
                            MessageBox.Show("Attendance Record Updated..!!!", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            populate();
                            clear();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Employee Id!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        }*/
        int flag = 0;
        private void Attendance1_Load(object sender, EventArgs e)
        {
            flag = 0;
            // TODO: This line of code loads data into the 'managementDataSet.Emp_attendance' table. You can move, or remove it, as needed.
            this.emp_attendanceTableAdapter.Fill(this.managementDataSet.Emp_attendance);
            months.SelectedIndex = 0;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.CurrentRow.Selected = true;
                lbldays.Text = "<<Click>>";
                myempattnd.empid =dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                myempattnd .yr = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                myempattnd .month = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                lbltotalwork.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                myempattnd.wrdy = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
                lbllassign.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                myempattnd.ltaken = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[6].Value.ToString());
                flag = 1;
                txteid.Text = myempattnd.empid.ToString ();
                txtyear.Text = myempattnd.yr.ToString ();
                months.SelectedItem = myempattnd.month;
                txtwork.Text = myempattnd.wrdy.ToString ();
                lblltaken.Text = "<<Click>>";
            }
        }

        private void txtyear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Character, Spaces and Special Characters are not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txteid.Text == "")
            {
                MessageBox.Show("Please Enter the Employee Id", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtyear.Text = "";
            }
        }

        private void txtwork_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Character, Spaces and Special Characters are not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txteid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Character, Spaces and Special Characters are not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lbldays_Click_1(object sender, EventArgs e)
        {
            Monthdays();
            try
            {
                int dy = 0;
                dy = int.Parse(lbldays.Text) - 2;
                lbllassign.Text = "2";
                lbltotalwork.Text = dy.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Please Select Month and Year!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblltaken_Click(object sender, EventArgs e)
        {
            int leave = 0;
            try
            {
                if (int.Parse(txtwork.Text) > int.Parse(lbldays.Text))
                {
                    MessageBox.Show("Invalid Worked Days!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtwork.Text = "";
                }
                else if (int.Parse(txtwork.Text) < int.Parse(lbltotalwork.Text))
                {
                    leave = int.Parse(lbltotalwork.Text) - int.Parse(txtwork.Text);
                    lblltaken.Text = leave.ToString();
                }
                else if (int.Parse(txtwork.Text) >= int.Parse(lbltotalwork.Text) || int.Parse(txtwork.Text) <= int.Parse(lbldays.Text))
                {
                    leave = 0;
                    lblltaken.Text = leave.ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please click on the field Days in month...!!", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtyear_TextChanged(object sender, EventArgs e)
        {
            if (txtyear.Text == "0")
            {
                DialogResult res1 = MessageBox.Show("Year cannot be Zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (res1 == DialogResult.OK)
                {
                    txtyear.Clear();
                }
            }
        }

        private void txtwork_TextChanged(object sender, EventArgs e)
        {
            if (txtwork.Text == "0")
            {
                DialogResult res1 = MessageBox.Show("Workind days cannot be Zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (res1 == DialogResult.OK)
                {
                    txtwork.Clear();
                }
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void lnklblsal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (flag == 1)
            {
                Employee_Salary ess = new Employee_Salary();
                ess.Show();
            }
            else
            {
                MessageBox.Show("Select the Record to generate Salary", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      /*  private void btndelete_Click(object sender, EventArgs e)
        {
            if (flag == 0)
            {
                MessageBox.Show("Select the Record to be Deleted", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                c = new connect();
                c.cmd.CommandText = "Select * from Emp_attendance where Emp_id='" + txteid.Text + "'";
                ds = new DataSet();
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "del");
                if (ds.Tables["del"].Rows.Count > 0)
                {
                    c.cmd.CommandText = "Delete  from Emp_attendance where Emp_id='" + txteid.Text + "'";
                    c.cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Deleted..!!!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    populate();
                    clear();
                }
                else
                {
                    MessageBox.Show("Record not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }*/
    }
}
public static class myempattnd
{
    public static int yr=0,wrdy=0,ltaken=0;
    
    public static string month = "",empid="";
}
