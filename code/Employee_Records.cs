using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace store_management
{
    public partial class Employee_Records : Form
    {
        connect c;
        DataSet ds;
        SqlDataAdapter adp = new SqlDataAdapter();
        public Employee_Records()
        {
            InitializeComponent();
        }
        public void populate()
        {
            try
            {
                c = new connect();
                string myquery = "Select * from Emp_details";
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
        public void agecal()
        {
            int DOB1= 0,DOJ1=0,result=0;
            DOB1 = dob.Value.Year;
            DOJ1 = doj.Value.Year;
            result = DOJ1 - DOB1;
            if ((result<=0 ||result >0) && result <18)
            {
                MessageBox.Show("Invalid Age", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtage.Text = "";
            }
            
            else if (result> 75)
            {
                MessageBox.Show("Invalid, Age cannot be greater than 75", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtage.Text = "";
            }
            else
            {
                txtage.Text = result.ToString();
            }
            
        }
        public void clear()
        {
            txteid.Text = "";
            txtename.Text = "";
            //dob.Text = "";
            gender.SelectedIndex = 0;
            txtecont.Text = "";
            txtage.Text = "";
            txteadd.Text = "";
            txteemail.Text = "";
            //doj.Text = "";
            txtsal.Text = "";
        }
        public void Generat_Emp_Id()
        {
            c = new connect();
            string emp_id;
            string queryEmp = "select Emp_id from Emp_details order by Emp_id Desc";
            SqlCommand cmdE = new SqlCommand(queryEmp, c.cnn);
            SqlDataReader drE = cmdE.ExecuteReader();
            if (drE.Read())
            {
                int i = int.Parse(drE[0].ToString()) + 1;
                emp_id = i.ToString("0000");
            }
            else if (Convert.IsDBNull(drE))
            {
                emp_id = ("0001");
            }
            else
            {
                emp_id = ("0001");
            }
            txteid.Text = emp_id.ToString();
        }
        private void Employee_Records_Load(object sender, EventArgs e)
        {
            Generat_Emp_Id();
            gender.SelectedIndex = 0;
            // TODO: This line of code loads data into the 'managementDataSet2.Emp_details' table. You can move, or remove it, as needed.
            this.emp_detailsTableAdapter.Fill(this.managementDataSet2.Emp_details);
            doj.MaxDate = DateTime.Today;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (txteid.Text == "" || txtename.Text == "" || dob .Text == "" || gender.SelectedItem .ToString()=="<<Select>>" || txtecont.Text  ==""||txtage.Text  ==""||txteemail .Text ==""||txteadd.Text ==""||doj.Text ==""||txtsal .Text =="")
            {
                MessageBox.Show("Please enter required fields", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Convert.ToInt64(txtsal.Text) < 3000)
            {
                MessageBox.Show("Salary cannot be less than 3,000", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtsal.Text = "";
            }
            else if (Convert.ToInt64(txtsal.Text) > 20000 || Convert.ToInt64(txtsal.Text) < 0)
            {
                MessageBox.Show("Salary cannot be out of Range", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtsal.Text = "";
            }
            else if (txtecont.TextLength < 10)
            {
                MessageBox.Show("Contact Number cannot be Less than 10", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                c = new connect();
                c.cmd.CommandText = "Select * from Emp_details where Contact='" + Convert.ToInt64(txtecont.Text) + "'or Email= '" + txteemail.Text + "'";
                ds = new DataSet();
                adp.SelectCommand = c.cmd;
                adp.Fill(ds, "same2");
                c.cmd.ExecuteNonQuery();
                if (ds.Tables["same2"].Rows.Count > 0)
                {
                    MessageBox.Show("Phone number or E-mail already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        c.cmd.CommandText = "Insert into Emp_details values(@Emp_id,@Emp_name,@Gender,@Dob,@Contact,@Age,@Email,@Address,@Doj,@Salary)";
                        c.cmd.Parameters.Clear();
                        c.cmd.Parameters.Add("@Emp_id", SqlDbType.NVarChar).Value = txteid.Text;
                        c.cmd.Parameters.Add("@Emp_name", SqlDbType.NVarChar).Value = txtename.Text;
                        c.cmd.Parameters.Add("@Gender", SqlDbType.NVarChar).Value = gender.SelectedItem.ToString();
                        c.cmd.Parameters.Add("@Dob", SqlDbType.DateTime).Value = dob.Text;
                        c.cmd.Parameters.Add("@Contact", SqlDbType.BigInt).Value = Convert.ToInt64(txtecont.Text);
                        c.cmd.Parameters.Add("@Age", SqlDbType.BigInt).Value = Convert.ToInt64(txtage.Text);
                        c.cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = txteemail.Text;
                        c.cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = txteadd.Text;
                        c.cmd.Parameters.Add("@Doj", SqlDbType.DateTime).Value = doj.Text;
                        c.cmd.Parameters.Add("@Salary", SqlDbType.BigInt).Value = Convert.ToInt64(txtsal.Text);
                        c.cmd.ExecuteNonQuery();
                        MessageBox.Show("Record Inserted..!!!", "Inserted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        populate();
                        clear();
                        Generat_Emp_Id();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Employee Id already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        c.cnn.Close();
                    }
                }
            }
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.CurrentRow.Selected = true;
                txteid.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                txtename.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                gender.SelectedItem = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                dob.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                txtecont .Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString(); 
                txtage.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString(); 
                txteemail.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString(); 
                txteadd.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                doj.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
                txtsal.Text = dataGridView1.SelectedRows[0].Cells[9].Value.ToString();
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (txteid.Text == "")
            {
                MessageBox.Show("Please enter Employee Id", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    c = new connect();
                    c.cmd.CommandText = "Select * from Emp_details where Emp_id='" + txteid.Text+"'";
                    ds = new DataSet();
                    adp.SelectCommand = c.cmd;
                    adp.Fill(ds, "del");
                    if (ds.Tables["del"].Rows.Count > 0)
                    {

                        c.cmd.CommandText = "Delete  from Emp_details where Emp_id='" + txteid.Text+"'";
                        c.cmd.ExecuteNonQuery();
                        MessageBox.Show("Record Deleted..!!!","Deleted",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        populate();
                        clear();
                        Generat_Emp_Id();
                    }
                    else
                    {
                        MessageBox.Show("Record not found");
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


        private void btnedit_Click(object sender, EventArgs e)
        {
            if (txteid.Text == "" || txtename.Text == "" || dob.Text == "" ||gender .SelectedItem .ToString() =="<<Select>>" || txtecont.Text == "" || txtage.Text == "" || txteemail.Text == "" || txteadd.Text == "" || doj.Text == "" || txtsal.Text == "")
            {
                MessageBox.Show("Please enter required fields", "Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtsal.Text == "0")
            {
                MessageBox.Show("Salary cannot be Zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtsal.Text = "";
            }
            else if (Convert.ToInt64(txtsal.Text) < 3000)
            {
                MessageBox.Show("Salary cannot be less than 3,000", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtsal.Text = "";
            }
            else if (Convert.ToInt64(txtsal.Text) > 20000 || Convert.ToInt64(txtsal.Text) < 0)
            {
                MessageBox.Show("Salary cannot be out of Range", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtsal.Text = "";
            }
            else if (txtecont.TextLength < 10)
            {
                MessageBox.Show("Contact Number cannot be Less than 10", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    c = new connect();
                    c.cmd.CommandText = "Select * from Emp_details where Emp_id='" + txteid.Text + "'";
                    ds = new DataSet();
                    adp.SelectCommand = c.cmd;
                    adp.Fill(ds, "emp");
                    c.cmd.ExecuteNonQuery();
                    if (ds.Tables["emp"].Rows.Count > 0)
                    {
                        c.cmd.CommandText = "Update Emp_details set Emp_name=@Emp_name,Gender=@Gender,Dob=@Dob,Contact=@Contact,Age=@Age,Email=@Email,Address=@Address,Doj=@Doj,Salary=@Salary where Emp_id='" + txteid.Text + "'";
                        c.cmd.Parameters.Clear();
                        c.cmd.Parameters.Add("@Emp_id", SqlDbType.NVarChar).Value = txteid.Text;
                        c.cmd.Parameters.Add("@Emp_name", SqlDbType.NVarChar).Value = txtename.Text;
                        c.cmd.Parameters.Add("@Gender", SqlDbType.NVarChar).Value = gender.SelectedItem.ToString();
                        c.cmd.Parameters.Add("@Dob", SqlDbType.DateTime).Value = Convert.ToDateTime(dob.Text);
                        c.cmd.Parameters.Add("@Contact", SqlDbType.BigInt).Value = Convert.ToInt64(txtecont.Text);
                        c.cmd.Parameters.Add("@Age", SqlDbType.BigInt).Value = Convert.ToInt64(txtage.Text);
                        c.cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = txteemail.Text;
                        c.cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = txteadd.Text;
                        c.cmd.Parameters.Add("@Doj", SqlDbType.DateTime).Value = Convert.ToDateTime(doj.Text);
                        c.cmd.Parameters.Add("@Salary", SqlDbType.BigInt).Value = Convert.ToInt64(txtsal.Text);
                        c.cmd.ExecuteNonQuery();
                        MessageBox.Show("Updated..!!!","Updated",MessageBoxButtons .OK,MessageBoxIcon.Information);
                        populate();
                        clear();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Employee Id!!!");
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

        private void txtename_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Numbers and Special Characters are not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txteemail_Leave(object sender, EventArgs e)
        {
            //string pattern="^[a-zA-Z][\\w\\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\\w\\.-]*[a-zA-Z0-9]\\.[a-zA-z][a-zA-z\\.]*[a-zA-Z]$";
            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (Regex.IsMatch(txteemail.Text, pattern))
            {
                //errorProvider1.Clear();
            }
            else
            {
                //errorProvider1.SetError(this.txteemail, "Please Provide Valid Email Address");
                //return;
                MessageBox.Show("Please Provide Valid Email Address","ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txteemail.Text = "";
            }
        }

        private void txtecont_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Character, Spaces and Special Characters are not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtecont_TextChanged(object sender, EventArgs e)
        {
            if (txtecont.TextLength == 10)
            {
                txtecont.ForeColor = Color.Black;
            }
            else
            {
                txtecont.ForeColor = Color.Red;
            }
            if (txtecont.Text == "0")
            {
                DialogResult res1 = MessageBox.Show("Contact Number cannot be Zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (res1 == DialogResult.OK)
                {
                    txtecont.Clear();
                }
            }
        }

        private void txtsal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
               MessageBox.Show("Enter Numeric Value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }
        }

        private void dob_ValueChanged(object sender, EventArgs e)
        {
            agecal();
        }

        private void doj_ValueChanged(object sender, EventArgs e)
        {
            //doj.MaxDate = DateTime.Today;
        }

        private void txtsal_TextChanged(object sender, EventArgs e)
        {
            if (txtsal.Text == "0")
            {
                DialogResult res1 = MessageBox.Show("Salary cannot be Zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (res1 == DialogResult.OK)
                {
                    txtsal.Clear();
                }
            }
        }
    }
}