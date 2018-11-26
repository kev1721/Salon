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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewVisit));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblWhoEdit = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblWhoAdd = new System.Windows.Forms.Label();
            this.txtBxDiscountSeason = new System.Windows.Forms.TextBox();
            this.txtBxNote = new System.Windows.Forms.TextBox();
            this.txtBxCostStyle = new System.Windows.Forms.TextBox();
            this.dgvMaterials = new System.Windows.Forms.DataGridView();
            this.visitsMaterialsVisitBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtBxCostVisit = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dTPDateVisit = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.lblCalc = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPgStyles = new System.Windows.Forms.TabPage();
            this.bindingNavigator2 = new System.Windows.Forms.BindingNavigator(this.components);
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.dgvStyles = new System.Windows.Forms.DataGridView();
            this.tabPgMaterials = new System.Windows.Forms.TabPage();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.tabPgConsults = new System.Windows.Forms.TabPage();
            this.bindingNavigator3 = new System.Windows.Forms.BindingNavigator(this.components);
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.dgvConsults = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.chBxAccept = new System.Windows.Forms.CheckBox();
            this.txtBxWhoAdd = new System.Windows.Forms.TextBox();
            this.txtBxWhoEdit = new System.Windows.Forms.TextBox();
            this.lblDiscountConst = new System.Windows.Forms.Label();
            this.txtBxDiscountConst = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBxCostConsults = new System.Windows.Forms.TextBox();
            this.txtBxClientName = new System.Windows.Forms.TextBox();
            this.txtBxCalc = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterials)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.visitsMaterialsVisitBindingSource)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPgStyles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator2)).BeginInit();
            this.bindingNavigator2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStyles)).BeginInit();
            this.tabPgMaterials.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            this.tabPgConsults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator3)).BeginInit();
            this.bindingNavigator3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsults)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(643, 555);
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
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(562, 555);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 22;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblWhoEdit
            // 
            this.lblWhoEdit.AutoSize = true;
            this.lblWhoEdit.Location = new System.Drawing.Point(484, 41);
            this.lblWhoEdit.Name = "lblWhoEdit";
            this.lblWhoEdit.Size = new System.Drawing.Size(53, 13);
            this.lblWhoEdit.TabIndex = 37;
            this.lblWhoEdit.Text = "Изменил";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 434);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "Скидка сез.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 486);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Примечание";
            // 
            // lblWhoAdd
            // 
            this.lblWhoAdd.AutoSize = true;
            this.lblWhoAdd.Location = new System.Drawing.Point(485, 15);
            this.lblWhoAdd.Name = "lblWhoAdd";
            this.lblWhoAdd.Size = new System.Drawing.Size(52, 13);
            this.lblWhoAdd.TabIndex = 31;
            this.lblWhoAdd.Text = "Добавил";
            // 
            // txtBxDiscountSeason
            // 
            this.txtBxDiscountSeason.Location = new System.Drawing.Point(114, 431);
            this.txtBxDiscountSeason.Name = "txtBxDiscountSeason";
            this.txtBxDiscountSeason.Size = new System.Drawing.Size(152, 20);
            this.txtBxDiscountSeason.TabIndex = 29;
            // 
            // txtBxNote
            // 
            this.txtBxNote.Location = new System.Drawing.Point(114, 486);
            this.txtBxNote.Multiline = true;
            this.txtBxNote.Name = "txtBxNote";
            this.txtBxNote.Size = new System.Drawing.Size(233, 63);
            this.txtBxNote.TabIndex = 28;
            // 
            // txtBxCostStyle
            // 
            this.txtBxCostStyle.Location = new System.Drawing.Point(547, 428);
            this.txtBxCostStyle.Name = "txtBxCostStyle";
            this.txtBxCostStyle.ReadOnly = true;
            this.txtBxCostStyle.Size = new System.Drawing.Size(171, 20);
            this.txtBxCostStyle.TabIndex = 26;
            // 
            // dgvMaterials
            // 
            this.dgvMaterials.AllowUserToAddRows = false;
            this.dgvMaterials.AllowUserToDeleteRows = false;
            this.dgvMaterials.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMaterials.Location = new System.Drawing.Point(0, 31);
            this.dgvMaterials.Name = "dgvMaterials";
            this.dgvMaterials.RowHeadersVisible = false;
            this.dgvMaterials.Size = new System.Drawing.Size(695, 301);
            this.dgvMaterials.TabIndex = 42;
            this.dgvMaterials.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvMaterials_DataError);
            // 
            // visitsMaterialsVisitBindingSource
            // 
            this.visitsMaterialsVisitBindingSource.DataMember = "VisitsMaterialsVisit";
            // 
            // txtBxCostVisit
            // 
            this.txtBxCostVisit.Location = new System.Drawing.Point(547, 480);
            this.txtBxCostVisit.Name = "txtBxCostVisit";
            this.txtBxCostVisit.Size = new System.Drawing.Size(171, 20);
            this.txtBxCostVisit.TabIndex = 49;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(424, 483);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 13);
            this.label8.TabIndex = 52;
            this.label8.Text = "Стоимость посещ.";
            // 
            // dTPDateVisit
            // 
            this.dTPDateVisit.Location = new System.Drawing.Point(114, 38);
            this.dTPDateVisit.Name = "dTPDateVisit";
            this.dTPDateVisit.Size = new System.Drawing.Size(152, 20);
            this.dTPDateVisit.TabIndex = 53;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 42);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 13);
            this.label11.TabIndex = 54;
            this.label11.Text = "Дата посещения";
            // 
            // btnCalculate
            // 
            this.btnCalculate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCalculate.Location = new System.Drawing.Point(466, 555);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(90, 23);
            this.btnCalculate.TabIndex = 56;
            this.btnCalculate.Text = "Рассчитать";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // lblCalc
            // 
            this.lblCalc.AutoSize = true;
            this.lblCalc.Location = new System.Drawing.Point(496, 509);
            this.lblCalc.Name = "lblCalc";
            this.lblCalc.Size = new System.Drawing.Size(45, 13);
            this.lblCalc.TabIndex = 58;
            this.lblCalc.Text = "Расчет:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPgStyles);
            this.tabControl1.Controls.Add(this.tabPgMaterials);
            this.tabControl1.Controls.Add(this.tabPgConsults);
            this.tabControl1.Location = new System.Drawing.Point(15, 64);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(703, 358);
            this.tabControl1.TabIndex = 60;
            // 
            // tabPgStyles
            // 
            this.tabPgStyles.Controls.Add(this.bindingNavigator2);
            this.tabPgStyles.Controls.Add(this.dgvStyles);
            this.tabPgStyles.Location = new System.Drawing.Point(4, 22);
            this.tabPgStyles.Name = "tabPgStyles";
            this.tabPgStyles.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgStyles.Size = new System.Drawing.Size(695, 332);
            this.tabPgStyles.TabIndex = 0;
            this.tabPgStyles.Text = "Оказанные услуги";
            this.tabPgStyles.UseVisualStyleBackColor = true;
            // 
            // bindingNavigator2
            // 
            this.bindingNavigator2.AddNewItem = null;
            this.bindingNavigator2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bindingNavigator2.CountItem = null;
            this.bindingNavigator2.DeleteItem = null;
            this.bindingNavigator2.Dock = System.Windows.Forms.DockStyle.None;
            this.bindingNavigator2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.bindingNavigator2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.bindingNavigator2.Location = new System.Drawing.Point(3, 3);
            this.bindingNavigator2.MoveFirstItem = null;
            this.bindingNavigator2.MoveLastItem = null;
            this.bindingNavigator2.MoveNextItem = null;
            this.bindingNavigator2.MovePreviousItem = null;
            this.bindingNavigator2.Name = "bindingNavigator2";
            this.bindingNavigator2.PositionItem = null;
            this.bindingNavigator2.Size = new System.Drawing.Size(49, 25);
            this.bindingNavigator2.TabIndex = 57;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.RightToLeftAutoMirrorImage = true;
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Add new";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.RightToLeftAutoMirrorImage = true;
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Delete";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // dgvStyles
            // 
            this.dgvStyles.AllowUserToAddRows = false;
            this.dgvStyles.AllowUserToDeleteRows = false;
            this.dgvStyles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStyles.Location = new System.Drawing.Point(0, 31);
            this.dgvStyles.Name = "dgvStyles";
            this.dgvStyles.RowHeadersVisible = false;
            this.dgvStyles.Size = new System.Drawing.Size(695, 301);
            this.dgvStyles.TabIndex = 56;
            this.dgvStyles.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvStyles_DataError);
            this.dgvStyles.Paint += new System.Windows.Forms.PaintEventHandler(this.dgvStyles_Paint);
            // 
            // tabPgMaterials
            // 
            this.tabPgMaterials.Controls.Add(this.bindingNavigator1);
            this.tabPgMaterials.Controls.Add(this.dgvMaterials);
            this.tabPgMaterials.Location = new System.Drawing.Point(4, 22);
            this.tabPgMaterials.Name = "tabPgMaterials";
            this.tabPgMaterials.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgMaterials.Size = new System.Drawing.Size(695, 332);
            this.tabPgMaterials.TabIndex = 1;
            this.tabPgMaterials.Text = "Расх. материалы";
            this.tabPgMaterials.UseVisualStyleBackColor = true;
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = null;
            this.bindingNavigator1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bindingNavigator1.CountItem = null;
            this.bindingNavigator1.DeleteItem = null;
            this.bindingNavigator1.Dock = System.Windows.Forms.DockStyle.None;
            this.bindingNavigator1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
            this.bindingNavigator1.Location = new System.Drawing.Point(3, 3);
            this.bindingNavigator1.MoveFirstItem = null;
            this.bindingNavigator1.MoveLastItem = null;
            this.bindingNavigator1.MoveNextItem = null;
            this.bindingNavigator1.MovePreviousItem = null;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = null;
            this.bindingNavigator1.Size = new System.Drawing.Size(49, 25);
            this.bindingNavigator1.TabIndex = 55;
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            this.bindingNavigatorAddNewItem.Click += new System.EventHandler(this.bindingNavigatorAddNewItem_Click);
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            this.bindingNavigatorDeleteItem.Click += new System.EventHandler(this.bindingNavigatorDeleteItem_Click);
            // 
            // tabPgConsults
            // 
            this.tabPgConsults.Controls.Add(this.bindingNavigator3);
            this.tabPgConsults.Controls.Add(this.dgvConsults);
            this.tabPgConsults.Location = new System.Drawing.Point(4, 22);
            this.tabPgConsults.Name = "tabPgConsults";
            this.tabPgConsults.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgConsults.Size = new System.Drawing.Size(695, 332);
            this.tabPgConsults.TabIndex = 2;
            this.tabPgConsults.Text = "Консультации";
            this.tabPgConsults.UseVisualStyleBackColor = true;
            // 
            // bindingNavigator3
            // 
            this.bindingNavigator3.AddNewItem = null;
            this.bindingNavigator3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bindingNavigator3.CountItem = null;
            this.bindingNavigator3.DeleteItem = null;
            this.bindingNavigator3.Dock = System.Windows.Forms.DockStyle.None;
            this.bindingNavigator3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.bindingNavigator3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.toolStripButton4});
            this.bindingNavigator3.Location = new System.Drawing.Point(3, 3);
            this.bindingNavigator3.MoveFirstItem = null;
            this.bindingNavigator3.MoveLastItem = null;
            this.bindingNavigator3.MoveNextItem = null;
            this.bindingNavigator3.MovePreviousItem = null;
            this.bindingNavigator3.Name = "bindingNavigator3";
            this.bindingNavigator3.PositionItem = null;
            this.bindingNavigator3.Size = new System.Drawing.Size(49, 25);
            this.bindingNavigator3.TabIndex = 58;
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.RightToLeftAutoMirrorImage = true;
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "Add new";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.RightToLeftAutoMirrorImage = true;
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "Delete";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // dgvConsults
            // 
            this.dgvConsults.AllowUserToAddRows = false;
            this.dgvConsults.AllowUserToDeleteRows = false;
            this.dgvConsults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvConsults.Location = new System.Drawing.Point(0, 31);
            this.dgvConsults.Name = "dgvConsults";
            this.dgvConsults.RowHeadersVisible = false;
            this.dgvConsults.Size = new System.Drawing.Size(695, 301);
            this.dgvConsults.TabIndex = 59;
            this.dgvConsults.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvConsults_DataError);
            this.dgvConsults.Paint += new System.Windows.Forms.PaintEventHandler(this.dgvConsults_Paint);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(434, 431);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 13);
            this.label6.TabIndex = 61;
            this.label6.Text = "Стоимость услуг";
            // 
            // chBxAccept
            // 
            this.chBxAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chBxAccept.AutoSize = true;
            this.chBxAccept.Location = new System.Drawing.Point(617, 532);
            this.chBxAccept.Name = "chBxAccept";
            this.chBxAccept.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chBxAccept.Size = new System.Drawing.Size(101, 17);
            this.chBxAccept.TabIndex = 62;
            this.chBxAccept.Text = "Подтверждено";
            this.chBxAccept.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chBxAccept.UseVisualStyleBackColor = true;
            // 
            // txtBxWhoAdd
            // 
            this.txtBxWhoAdd.Location = new System.Drawing.Point(543, 12);
            this.txtBxWhoAdd.Name = "txtBxWhoAdd";
            this.txtBxWhoAdd.ReadOnly = true;
            this.txtBxWhoAdd.Size = new System.Drawing.Size(171, 20);
            this.txtBxWhoAdd.TabIndex = 63;
            // 
            // txtBxWhoEdit
            // 
            this.txtBxWhoEdit.Location = new System.Drawing.Point(543, 38);
            this.txtBxWhoEdit.Name = "txtBxWhoEdit";
            this.txtBxWhoEdit.ReadOnly = true;
            this.txtBxWhoEdit.Size = new System.Drawing.Size(171, 20);
            this.txtBxWhoEdit.TabIndex = 64;
            // 
            // lblDiscountConst
            // 
            this.lblDiscountConst.AutoSize = true;
            this.lblDiscountConst.Location = new System.Drawing.Point(35, 460);
            this.lblDiscountConst.Name = "lblDiscountConst";
            this.lblDiscountConst.Size = new System.Drawing.Size(73, 13);
            this.lblDiscountConst.TabIndex = 65;
            this.lblDiscountConst.Text = "Скидка пост.";
            // 
            // txtBxDiscountConst
            // 
            this.txtBxDiscountConst.Location = new System.Drawing.Point(114, 457);
            this.txtBxDiscountConst.Name = "txtBxDiscountConst";
            this.txtBxDiscountConst.ReadOnly = true;
            this.txtBxDiscountConst.Size = new System.Drawing.Size(152, 20);
            this.txtBxDiscountConst.TabIndex = 66;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(434, 457);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 68;
            this.label1.Text = "Стоимость конс.";
            // 
            // txtBxCostConsults
            // 
            this.txtBxCostConsults.Location = new System.Drawing.Point(547, 454);
            this.txtBxCostConsults.Name = "txtBxCostConsults";
            this.txtBxCostConsults.ReadOnly = true;
            this.txtBxCostConsults.Size = new System.Drawing.Size(171, 20);
            this.txtBxCostConsults.TabIndex = 67;
            // 
            // txtBxClientName
            // 
            this.txtBxClientName.Location = new System.Drawing.Point(22, 12);
            this.txtBxClientName.Name = "txtBxClientName";
            this.txtBxClientName.ReadOnly = true;
            this.txtBxClientName.Size = new System.Drawing.Size(244, 20);
            this.txtBxClientName.TabIndex = 69;
            // 
            // txtBxCalc
            // 
            this.txtBxCalc.Location = new System.Drawing.Point(547, 506);
            this.txtBxCalc.Name = "txtBxCalc";
            this.txtBxCalc.ReadOnly = true;
            this.txtBxCalc.Size = new System.Drawing.Size(171, 20);
            this.txtBxCalc.TabIndex = 70;
            // 
            // frmNewVisit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(730, 590);
            this.Controls.Add(this.txtBxCalc);
            this.Controls.Add(this.txtBxClientName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBxCostConsults);
            this.Controls.Add(this.txtBxDiscountConst);
            this.Controls.Add(this.lblDiscountConst);
            this.Controls.Add(this.txtBxWhoEdit);
            this.Controls.Add(this.txtBxWhoAdd);
            this.Controls.Add(this.chBxAccept);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblCalc);
            this.Controls.Add(this.txtBxCostVisit);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.dTPDateVisit);
            this.Controls.Add(this.lblWhoEdit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblWhoAdd);
            this.Controls.Add(this.txtBxDiscountSeason);
            this.Controls.Add(this.txtBxNote);
            this.Controls.Add(this.txtBxCostStyle);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNewVisit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Новое посещение";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNewVisit_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterials)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.visitsMaterialsVisitBindingSource)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPgStyles.ResumeLayout(false);
            this.tabPgStyles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator2)).EndInit();
            this.bindingNavigator2.ResumeLayout(false);
            this.bindingNavigator2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStyles)).EndInit();
            this.tabPgMaterials.ResumeLayout(false);
            this.tabPgMaterials.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            this.tabPgConsults.ResumeLayout(false);
            this.tabPgConsults.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator3)).EndInit();
            this.bindingNavigator3.ResumeLayout(false);
            this.bindingNavigator3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblWhoEdit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblWhoAdd;
        private System.Windows.Forms.TextBox txtBxDiscountSeason;
        private System.Windows.Forms.TextBox txtBxNote;
        private System.Windows.Forms.TextBox txtBxCostStyle;
        private System.Windows.Forms.DataGridView dgvMaterials;
        private System.Windows.Forms.DataGridViewComboBoxColumn Name1;
        private System.Windows.Forms.TextBox txtBxCostVisit;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dTPDateVisit;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.BindingSource visitsMaterialsVisitBindingSource;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Label lblCalc;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPgStyles;
        private System.Windows.Forms.TabPage tabPgMaterials;
        private System.Windows.Forms.BindingNavigator bindingNavigator2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.DataGridView dgvStyles;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chBxAccept;
        private System.Windows.Forms.TextBox txtBxWhoAdd;
        private System.Windows.Forms.TextBox txtBxWhoEdit;
        private System.Windows.Forms.Label lblDiscountConst;
        private System.Windows.Forms.TextBox txtBxDiscountConst;
        private System.Windows.Forms.TabPage tabPgConsults;
        private System.Windows.Forms.DataGridView dgvConsults;
        private System.Windows.Forms.BindingNavigator bindingNavigator3;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBxCostConsults;
        private System.Windows.Forms.TextBox txtBxClientName;
        private System.Windows.Forms.TextBox txtBxCalc;
    }
}