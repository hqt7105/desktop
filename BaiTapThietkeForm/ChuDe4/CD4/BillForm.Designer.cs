namespace CD4
{
    partial class BillForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.TuNgay = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.DenNgay = new System.Windows.Forms.DateTimePicker();
            this.lvBill = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(255, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Danh sách hóa đơn từ ngày";
            // 
            // TuNgay
            // 
            this.TuNgay.Location = new System.Drawing.Point(289, 13);
            this.TuNgay.Name = "TuNgay";
            this.TuNgay.Size = new System.Drawing.Size(200, 22);
            this.TuNgay.TabIndex = 1;
            this.TuNgay.ValueChanged += new System.EventHandler(this.TuNgay_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(505, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 22);
            this.label2.TabIndex = 0;
            this.label2.Text = "Đến\r\n";
            // 
            // DenNgay
            // 
            this.DenNgay.Location = new System.Drawing.Point(569, 13);
            this.DenNgay.Name = "DenNgay";
            this.DenNgay.Size = new System.Drawing.Size(200, 22);
            this.DenNgay.TabIndex = 1;
            this.DenNgay.ValueChanged += new System.EventHandler(this.DenNgay_ValueChanged);
            // 
            // lvBill
            // 
            this.lvBill.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lvBill.HideSelection = false;
            this.lvBill.Location = new System.Drawing.Point(17, 70);
            this.lvBill.Name = "lvBill";
            this.lvBill.Size = new System.Drawing.Size(752, 341);
            this.lvBill.TabIndex = 2;
            this.lvBill.UseCompatibleStateImageBehavior = false;
            this.lvBill.View = System.Windows.Forms.View.Details;
            this.lvBill.DoubleClick += new System.EventHandler(this.lvBill_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Mã hóa đơn";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Ngày lập hóa đơn";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Bàn";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Tổng tiền";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Trạng thái";
            // 
            // BillForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 450);
            this.Controls.Add(this.lvBill);
            this.Controls.Add(this.DenNgay);
            this.Controls.Add(this.TuNgay);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "BillForm";
            this.Text = "BillForm";
            this.Load += new System.EventHandler(this.BillForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker TuNgay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker DenNgay;
        private System.Windows.Forms.ListView lvBill;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
    }
}