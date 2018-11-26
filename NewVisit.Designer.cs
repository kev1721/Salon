namespace Style
{
    partial class frmNewVisit
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBxNumber = new System.Windows.Forms.TextBox();
            this.txtBxDiscountSeason = new System.Windows.Forms.TextBox();
            this.txtBxNote = new System.Windows.Forms.TextBox();
            this.txtBxConsult = new System.Windows.Forms.TextBox();
            this.txtBxCostStyle = new System.Windows.Forms.TextBox();
            this.cmbBxEmploy = new System.Windows.Forms.ComboBox();
            this.cmbBxStyle = new System.Windows.Forms.ComboBox();
            this.dgvMaterials = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBxCostConsult = new System.Windows.Forms.TextBox();
            this.txtBxCostVisit = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dTPDateVisit = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterials)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(465, 312);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 23;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(368, 312);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 22;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 67);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(99, 13);
            this.label10.TabIndex = 37;
            this.label10.Text = "Оказанная услуга";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(284, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 13);
            this.label9.TabIndex = 36;
            this.label9.Text = "Стоимость услуг(и)";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 215);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "Консультация";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 281);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "Скидка сез.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(284, 268);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Примечание";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Порядковый №";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(315, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Специалист";
            // 
            // txtBxNumber
            // 
            this.txtBxNumber.Enabled = false;
            this.txtBxNumber.Location = new System.Drawing.Point(116, 12);
            this.txtBxNumber.Name = "txtBxNumber";
            this.txtBxNumber.Size = new System.Drawing.Size(68, 20);
            this.txtBxNumber.TabIndex = 30;
            // 
            // txtBxDiscountSeason
            // 
            this.txtBxDiscountSeason.Location = new System.Drawing.Point(116, 278);
            this.txtBxDiscountSeason.Name = "txtBxDiscountSeason";
            this.txtBxDiscountSeason.Size = new System.Drawing.Size(89, 20);
            this.txtBxDiscountSeason.TabIndex = 29;
            // 
            // txtBxNote
            // 
            this.txtBxNote.Location = new System.Drawing.Point(360, 252);
            this.txtBxNote.Multiline = true;
            this.txtBxNote.Name = "txtBxNote";
            this.txtBxNote.Size = new System.Drawing.Size(180, 46);
            this.txtBxNote.TabIndex = 28;
            // 
            // txtBxConsult
            // 
            this.txtBxConsult.Location = new System.Drawing.Point(115, 212);
            this.txtBxConsult.Multiline = true;
            this.txtBxConsult.Name = "txtBxConsult";
            this.txtBxConsult.Size = new System.Drawing.Size(425, 34);
            this.txtBxConsult.TabIndex = 27;
            // 
            // txtBxCostStyle
            // 
            this.txtBxCostStyle.Location = new System.Drawing.Point(388, 65);
            this.txtBxCostStyle.Name = "txtBxCostStyle";
            this.txtBxCostStyle.Size = new System.Drawing.Size(152, 20);
            this.txtBxCostStyle.TabIndex = 26;
            // 
            // cmbBxEmploy
            // 
            this.cmbBxEmploy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxEmploy.FormattingEnabled = true;
            this.cmbBxEmploy.Location = new System.Drawing.Point(388, 37);
            this.cmbBxEmploy.Name = "cmbBxEmploy";
            this.cmbBxEmploy.Size = new System.Drawing.Size(152, 21);
            this.cmbBxEmploy.TabIndex = 38;
            // 
            // cmbBxStyle
            // 
            this.cmbBxStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxStyle.FormattingEnabled = true;
            this.cmbBxStyle.Location = new System.Drawing.Point(115, 64);
            this.cmbBxStyle.Name = "cmbBxStyle";
            this.cmbBxStyle.Size = new System.Drawing.Size(152, 21);
            this.cmbBxStyle.TabIndex = 39;
            // 
            // dgvMaterials
            // 
            this.dgvMaterials.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaterials.Location = new System.Drawing.Point(115, 91);
            this.dgvMaterials.Name = "dgvMaterials";
            this.dgvMaterials.RowHeadersVisible = false;
            this.dgvMaterials.Size = new System.Drawing.Size(425, 115);
            this.dgvMaterials.TabIndex = 42;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(43, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 39);
            this.label6.TabIndex = 43;
            this.label6.Text = "Расходные материалы";
            // 
            // txtBxCostConsult
            // 
            this.txtBxCostConsult.Location = new System.Drawing.Point(116, 252);
            this.txtBxCostConsult.Name = "txtBxCostConsult";
            this.txtBxCostConsult.Size = new System.Drawing.Size(89, 20);
            this.txtBxCostConsult.TabIndex = 47;
            // 
            // txtBxCostVisit
            // 
            this.txtBxCostVisit.Location = new System.Drawing.Point(115, 304);
            this.txtBxCostVisit.Name = "txtBxCostVisit";
            this.txtBxCostVisit.Size = new System.Drawing.Size(90, 20);
            this.txtBxCostVisit.TabIndex = 49;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 255);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 51;
            this.label7.Text = "Сумма за конс.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(4, 307);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 13);
            this.label8.TabIndex = 52;
            this.label8.Text = "Итоговая сумма";
            // 
            // dTPDateVisit
            // 
            this.dTPDateVisit.Location = new System.Drawing.Point(115, 38);
            this.dTPDateVisit.Name = "dTPDateVisit";
            this.dTPDateVisit.Size = new System.Drawing.Size(152, 20);
            this.dTPDateVisit.TabIndex = 53;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 41);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 13);
            this.label11.TabIndex = 54;
            this.label11.Text = "Дата посещения";
            // 
            // frmNewVisit
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(552, 347);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.dTPDateVisit);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtBxCostVisit);
            this.Controls.Add(this.txtBxCostConsult);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dgvMaterials);
            this.Controls.Add(this.cmbBxStyle);
            this.Controls.Add(this.cmbBxEmploy);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBxNumber);
            this.Controls.Add(this.txtBxDiscountSeason);
            this.Controls.Add(this.txtBxNote);
            this.Controls.Add(this.txtBxConsult);
            this.Controls.Add(this.txtBxCostStyle);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Name = "frmNewVisit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Новое посещение";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNewVisit_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmNewVisit_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterials)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBxNumber;
        private System.Windows.Forms.TextBox txtBxDiscountSeason;
        private System.Windows.Forms.TextBox txtBxNote;
        private System.Windows.Forms.TextBox txtBxConsult;
        private System.Windows.Forms.TextBox txtBxCostStyle;
        private System.Windows.Forms.ComboBox cmbBxEmploy;
        private System.Windows.Forms.ComboBox cmbBxStyle;
        private System.Windows.Forms.DataGridView dgvMaterials;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewComboBoxColumn Name1;
        private System.Windows.Forms.TextBox txtBxCostConsult;
        private System.Windows.Forms.TextBox txtBxCostVisit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dTPDateVisit;
        private System.Windows.Forms.Label label11;
    }
}