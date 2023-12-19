namespace store_management
{
    partial class Purchase_bill_record
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Purchase_bill_record));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.purbillnoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.purbilldateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.purordrnoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supnoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.companyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sizeTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.brandDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.puramtDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.purchasebillBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.managementDataSet_Pur_bill = new store_management.ManagementDataSet_Pur_bill();
            this.purchase_billTableAdapter = new store_management.ManagementDataSet_Pur_billTableAdapters.Purchase_billTableAdapter();
            this.btneditsat = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.purchasebillBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.managementDataSet_Pur_bill)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.pictureBox7);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(920, 100);
            this.panel1.TabIndex = 50;
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox7.Image")));
            this.pictureBox7.Location = new System.Drawing.Point(114, 4);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(143, 93);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox7.TabIndex = 9;
            this.pictureBox7.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 48F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkOrange;
            this.label1.Location = new System.Drawing.Point(263, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(512, 85);
            this.label1.TabIndex = 8;
            this.label1.Text = "Hardware Bazar";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.purbillnoDataGridViewTextBoxColumn,
            this.purbilldateDataGridViewTextBoxColumn,
            this.purordrnoDataGridViewTextBoxColumn,
            this.supnoDataGridViewTextBoxColumn,
            this.supnameDataGridViewTextBoxColumn,
            this.companyDataGridViewTextBoxColumn,
            this.itemnameDataGridViewTextBoxColumn,
            this.sizeTypeDataGridViewTextBoxColumn,
            this.brandDataGridViewTextBoxColumn,
            this.quantityDataGridViewTextBoxColumn,
            this.priceDataGridViewTextBoxColumn,
            this.puramtDataGridViewTextBoxColumn,
            this.statusDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.purchasebillBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 168);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Size = new System.Drawing.Size(886, 454);
            this.dataGridView1.TabIndex = 51;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // purbillnoDataGridViewTextBoxColumn
            // 
            this.purbillnoDataGridViewTextBoxColumn.DataPropertyName = "Pur_bill_no";
            this.purbillnoDataGridViewTextBoxColumn.HeaderText = "Pur_bill_no";
            this.purbillnoDataGridViewTextBoxColumn.Name = "purbillnoDataGridViewTextBoxColumn";
            this.purbillnoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // purbilldateDataGridViewTextBoxColumn
            // 
            this.purbilldateDataGridViewTextBoxColumn.DataPropertyName = "Pur_bill_date";
            this.purbilldateDataGridViewTextBoxColumn.HeaderText = "Pur_bill_date";
            this.purbilldateDataGridViewTextBoxColumn.Name = "purbilldateDataGridViewTextBoxColumn";
            this.purbilldateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // purordrnoDataGridViewTextBoxColumn
            // 
            this.purordrnoDataGridViewTextBoxColumn.DataPropertyName = "Pur_ordr_no";
            this.purordrnoDataGridViewTextBoxColumn.HeaderText = "Pur_ordr_no";
            this.purordrnoDataGridViewTextBoxColumn.Name = "purordrnoDataGridViewTextBoxColumn";
            this.purordrnoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // supnoDataGridViewTextBoxColumn
            // 
            this.supnoDataGridViewTextBoxColumn.DataPropertyName = "Sup_no";
            this.supnoDataGridViewTextBoxColumn.HeaderText = "Sup_no";
            this.supnoDataGridViewTextBoxColumn.Name = "supnoDataGridViewTextBoxColumn";
            this.supnoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // supnameDataGridViewTextBoxColumn
            // 
            this.supnameDataGridViewTextBoxColumn.DataPropertyName = "Sup_name";
            this.supnameDataGridViewTextBoxColumn.HeaderText = "Sup_name";
            this.supnameDataGridViewTextBoxColumn.Name = "supnameDataGridViewTextBoxColumn";
            this.supnameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // companyDataGridViewTextBoxColumn
            // 
            this.companyDataGridViewTextBoxColumn.DataPropertyName = "Company";
            this.companyDataGridViewTextBoxColumn.HeaderText = "Company";
            this.companyDataGridViewTextBoxColumn.Name = "companyDataGridViewTextBoxColumn";
            this.companyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // itemnameDataGridViewTextBoxColumn
            // 
            this.itemnameDataGridViewTextBoxColumn.DataPropertyName = "Item_name";
            this.itemnameDataGridViewTextBoxColumn.HeaderText = "Item_name";
            this.itemnameDataGridViewTextBoxColumn.Name = "itemnameDataGridViewTextBoxColumn";
            this.itemnameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sizeTypeDataGridViewTextBoxColumn
            // 
            this.sizeTypeDataGridViewTextBoxColumn.DataPropertyName = "Size_Type";
            this.sizeTypeDataGridViewTextBoxColumn.HeaderText = "Size_Type";
            this.sizeTypeDataGridViewTextBoxColumn.Name = "sizeTypeDataGridViewTextBoxColumn";
            this.sizeTypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // brandDataGridViewTextBoxColumn
            // 
            this.brandDataGridViewTextBoxColumn.DataPropertyName = "Brand";
            this.brandDataGridViewTextBoxColumn.HeaderText = "Brand";
            this.brandDataGridViewTextBoxColumn.Name = "brandDataGridViewTextBoxColumn";
            this.brandDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // quantityDataGridViewTextBoxColumn
            // 
            this.quantityDataGridViewTextBoxColumn.DataPropertyName = "Quantity";
            this.quantityDataGridViewTextBoxColumn.HeaderText = "Quantity";
            this.quantityDataGridViewTextBoxColumn.Name = "quantityDataGridViewTextBoxColumn";
            this.quantityDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // priceDataGridViewTextBoxColumn
            // 
            this.priceDataGridViewTextBoxColumn.DataPropertyName = "Price";
            this.priceDataGridViewTextBoxColumn.HeaderText = "Price";
            this.priceDataGridViewTextBoxColumn.Name = "priceDataGridViewTextBoxColumn";
            this.priceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // puramtDataGridViewTextBoxColumn
            // 
            this.puramtDataGridViewTextBoxColumn.DataPropertyName = "Pur_amt";
            this.puramtDataGridViewTextBoxColumn.HeaderText = "Pur_amt";
            this.puramtDataGridViewTextBoxColumn.Name = "puramtDataGridViewTextBoxColumn";
            this.puramtDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // statusDataGridViewTextBoxColumn
            // 
            this.statusDataGridViewTextBoxColumn.DataPropertyName = "Status";
            this.statusDataGridViewTextBoxColumn.HeaderText = "Status";
            this.statusDataGridViewTextBoxColumn.Name = "statusDataGridViewTextBoxColumn";
            this.statusDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // purchasebillBindingSource
            // 
            this.purchasebillBindingSource.DataMember = "Purchase_bill";
            this.purchasebillBindingSource.DataSource = this.managementDataSet_Pur_bill;
            // 
            // managementDataSet_Pur_bill
            // 
            this.managementDataSet_Pur_bill.DataSetName = "ManagementDataSet_Pur_bill";
            this.managementDataSet_Pur_bill.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // purchase_billTableAdapter
            // 
            this.purchase_billTableAdapter.ClearBeforeFill = true;
            // 
            // btneditsat
            // 
            this.btneditsat.BackColor = System.Drawing.Color.Black;
            this.btneditsat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btneditsat.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btneditsat.ForeColor = System.Drawing.Color.White;
            this.btneditsat.Location = new System.Drawing.Point(384, 628);
            this.btneditsat.Name = "btneditsat";
            this.btneditsat.Size = new System.Drawing.Size(150, 40);
            this.btneditsat.TabIndex = 106;
            this.btneditsat.Text = "Edit Status";
            this.btneditsat.UseVisualStyleBackColor = false;
            this.btneditsat.Click += new System.EventHandler(this.btneditsat_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 21.75F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic)
                            | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(337, 123);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(271, 33);
            this.label6.TabIndex = 51;
            this.label6.Text = "Purchase Bill Record";
            // 
            // Purchase_bill_record
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(910, 689);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btneditsat);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "Purchase_bill_record";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Purchase_bill_record";
            this.Load += new System.EventHandler(this.Purchase_bill_record_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.purchasebillBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.managementDataSet_Pur_bill)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private ManagementDataSet_Pur_bill managementDataSet_Pur_bill;
        private System.Windows.Forms.BindingSource purchasebillBindingSource;
        private store_management.ManagementDataSet_Pur_billTableAdapters.Purchase_billTableAdapter purchase_billTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn purbillnoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn purbilldateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn purordrnoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn supnoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn supnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn companyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sizeTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn brandDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn puramtDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btneditsat;
        private System.Windows.Forms.Label label6;
    }
}