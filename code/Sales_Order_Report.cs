using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace store_management
{
    public partial class Sales_Order_Report : Form
    {
        public Sales_Order_Report()
        {
            InitializeComponent();
        }

        private void Sales_Order_Report_Load(object sender, EventArgs e)
        {
            flag = 0;
            // TODO: This line of code loads data into the 'managementDataSet8.Sales_order' table. You can move, or remove it, as needed.
            this.sales_orderTableAdapter.Fill(this.managementDataSet8.Sales_order);

        }
        int flag = 0;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                dataGridView1.CurrentRow.Selected = true;
                myglobal.order_id= dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                myglobal .total= Convert.ToInt64(dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
                flag = 1;
            }
        }

        private void btngenaratebill_Click(object sender, EventArgs e)
        {
            if (flag == 0)
            {
                MessageBox.Show("Select the field to generate bill ","Error",MessageBoxButtons .OK,MessageBoxIcon.Error);
            }
            else
            {
                Sales_Bill sbb1 = new Sales_Bill();
                sbb1.Show();
            }
        }
    }
}
public static class myglobal
{
    public static long total=0;
    public static string order_id="";
}