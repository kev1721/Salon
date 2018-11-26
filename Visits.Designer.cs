namespace Style
{
    partial class Visits
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvVisits = new System.Windows.Forms.DataGridView();
            this.contextMenuStripVisits = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripVisits = new System.Windows.Forms.ToolStrip();
            this.tlStrpBtnAddVisit = new System.Windows.Forms.ToolStripButton();
            this.tlStrpBtnEditVisit = new System.Windows.Forms.ToolStripButton();
            this.tlStrpBtnDeleteVisit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVisits)).BeginInit();
            this.contextMenuStripVisits.SuspendLayout();
            this.toolStripVisits.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvVisits
            // 
            this.dgvVisits.AllowUserToAddRows = false;
            this.dgvVisits.AllowUserToDeleteRows = false;
            this.dgvVisits.AllowUserToResizeRows = false;
            this.dgvVisits.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVisits.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvVisits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVisits.ContextMenuStrip = this.contextMenuStripVisits;
            this.dgvVisits.Location = new System.Drawing.Point(40, 56);
            this.dgvVisits.MultiSelect = false;
            this.dgvVisits.Name = "dgvVisits";
            this.dgvVisits.ReadOnly = true;
            this.dgvVisits.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dgvVisits.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvVisits.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVisits.Size = new System.Drawing.Size(616, 471);
            this.dgvVisits.TabIndex = 4;
            this.dgvVisits.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVisits_CellDoubleClick);
            this.dgvVisits.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVisits_CellContentClick);
            // 
            // contextMenuStripVisits
            // 
            this.contextMenuStripVisits.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemAdd,
            this.ToolStripMenuItemEdit,
            this.ToolStripMenuItemDelete});
            this.contextMenuStripVisits.Name = "contextMenuStripVisits";
            this.contextMenuStripVisits.Size = new System.Drawing.Size(125, 70);
            // 
            // ToolStripMenuItemAdd
            // 
            this.ToolStripMenuItemAdd.Name = "ToolStripMenuItemAdd";
            this.ToolStripMenuItemAdd.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItemAdd.Text = "Добавить";
            this.ToolStripMenuItemAdd.Click += new System.EventHandler(this.ToolStripMenuItemAdd_Click);
            // 
            // ToolStripMenuItemEdit
            // 
            this.ToolStripMenuItemEdit.Name = "ToolStripMenuItemEdit";
            this.ToolStripMenuItemEdit.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItemEdit.Text = "Изменить";
            this.ToolStripMenuItemEdit.Click += new System.EventHandler(this.ToolStripMenuItemEdit_Click);
            // 
            // ToolStripMenuItemDelete
            // 
            this.ToolStripMenuItemDelete.Name = "ToolStripMenuItemDelete";
            this.ToolStripMenuItemDelete.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItemDelete.Text = "Удалить";
            this.ToolStripMenuItemDelete.Click += new System.EventHandler(this.ToolStripMenuItemDelete_Click);
            // 
            // toolStripVisits
            // 
            this.toolStripVisits.Font = new System.Drawing.Font("Tahoma", 8.32F);
            this.toolStripVisits.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStripVisits.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlStrpBtnAddVisit,
            this.tlStrpBtnEditVisit,
            this.tlStrpBtnDeleteVisit,
            this.toolStripSeparator3,
            this.toolStripButton4,
            this.toolStripSeparator1,
            this.toolStripTextBox1,
            this.toolStripButton5,
            this.toolStripSeparator2,
            this.toolStripButton6,
            this.toolStripSeparator4});
            this.toolStripVisits.Location = new System.Drawing.Point(0, 0);
            this.toolStripVisits.Name = "toolStripVisits";
            this.toolStripVisits.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStripVisits.Size = new System.Drawing.Size(1358, 39);
            this.toolStripVisits.TabIndex = 5;
            this.toolStripVisits.Text = "toolStrip1";
            // 
            // tlStrpBtnAddVisit
            // 
            this.tlStrpBtnAddVisit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlStrpBtnAddVisit.Image = global::Style.Properties.Resources.application_form_add;
            this.tlStrpBtnAddVisit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlStrpBtnAddVisit.Name = "tlStrpBtnAddVisit";
            this.tlStrpBtnAddVisit.Size = new System.Drawing.Size(36, 36);
            this.tlStrpBtnAddVisit.Text = "Добавить посещение";
            this.tlStrpBtnAddVisit.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tlStrpBtnEditVisit
            // 
            this.tlStrpBtnEditVisit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlStrpBtnEditVisit.Image = global::Style.Properties.Resources.application_form_edit;
            this.tlStrpBtnEditVisit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlStrpBtnEditVisit.Name = "tlStrpBtnEditVisit";
            this.tlStrpBtnEditVisit.Size = new System.Drawing.Size(36, 36);
            this.tlStrpBtnEditVisit.Text = "Изменить данные посещения";
            this.tlStrpBtnEditVisit.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // tlStrpBtnDeleteVisit
            // 
            this.tlStrpBtnDeleteVisit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlStrpBtnDeleteVisit.Image = global::Style.Properties.Resources.application_form_delete;
            this.tlStrpBtnDeleteVisit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlStrpBtnDeleteVisit.Name = "tlStrpBtnDeleteVisit";
            this.tlStrpBtnDeleteVisit.Size = new System.Drawing.Size(36, 36);
            this.tlStrpBtnDeleteVisit.Text = "Удалить посещение";
            this.tlStrpBtnDeleteVisit.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::Style.Properties.Resources.magnifier;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton4.Text = "toolStripButton4";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(150, 39);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::Style.Properties.Resources.filter;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton5.Text = "toolStripButton5";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = global::Style.Properties.Resources.printer;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton6.Text = "toolStripButton6";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 719);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1358, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Visits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1358, 741);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStripVisits);
            this.Controls.Add(this.dgvVisits);
            this.Name = "Visits";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Посещения клиента";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dgvVisits)).EndInit();
            this.contextMenuStripVisits.ResumeLayout(false);
            this.toolStripVisits.ResumeLayout(false);
            this.toolStripVisits.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvVisits;
        private System.Windows.Forms.ToolStrip toolStripVisits;
        private System.Windows.Forms.ToolStripButton tlStrpBtnAddVisit;
        private System.Windows.Forms.ToolStripButton tlStrpBtnEditVisit;
        private System.Windows.Forms.ToolStripButton tlStrpBtnDeleteVisit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripVisits;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAdd;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemEdit;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDelete;
    }
}