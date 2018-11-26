using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net;
using log4net.Config;

namespace Style
{
    public partial class frmNewVisit : Form
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(frmNewVisit));

        public frmNewVisit()
        {
            InitializeComponent();
        }

        public frmNewVisit(bool _isEditMode, Main _mainForm)
        {
            InitializeComponent();

            isEditMode = _isEditMode;
            id_visit = -1;

            mv = new MaterialsVisit();
            styles = new StylesVisit();
            consults = new ConsultsVisit();

            if (_isEditMode)
            {
                this.Text = "Изменение данных посещения";
                //FillItmsCmbBx();
                FillTxtBx(_mainForm.GetCurrRowVisitInDGV); //заполнение полей
                FillDgvMaterials();
                FillDgvStyles();
                FillDgvConsults();
                FillBufFields(); //заполнение буфера 
            }
            else
            {
                this.Text = "Добавление нового посещения";
                this.btnSave.Text = "Добавить";

                //FillItmsCmbBx();
                FillDgvMaterials();
                FillDgvStyles();
                FillDgvConsults();
                FillBufFields(); //заполнение буфера 
            }

            addCmbBxToDGV(dgvStyles, "id_employ", "Исполнитель", "id", "FIO", "Employ");

            setDgvStylesOrderColumns();
            //setupColumnsDGV(dgvMaterials);
            id_client = Convert.ToUInt32(_mainForm.id_client);
            discountConst = Convert.ToUInt32(_mainForm.discountConst);
            FirstName = _mainForm.FirstName;
            LastName = _mainForm.LastName;
            MiddleName = _mainForm.MiddleName;
            BirthDay = _mainForm.BirthDay;

            txtBxDiscountConst.Text = discountConst.ToString();
            txtBxClientName.Text = LastName + " " + FirstName + " " + MiddleName;

            visitForm = _mainForm;

            if (!Program.isAdminMode )
            {
                dTPDateVisit.Enabled = false;
                if (chBxAccept.Checked)
                {
                    chBxAccept.Enabled = false;
                    btnSave.Enabled = false;
                    btnCalculate.Enabled = false;
                }
            }

            //chBxAccept.CheckedChanged += new System.EventHandler(this.chBxAccept_CheckedChanged);

        }

        ConsultsVisit consults;
        ConsultsVisit consultsOld;
        private void FillDgvConsults()
        {
            dgvConsults.AutoGenerateColumns = false;
            dgvConsults.Columns.Add("id", "id");
            dgvConsults.Columns[0].Visible = false;
            dgvConsults.Columns.Add("ConsultName", "Наименование");
            dgvConsults.Columns[1].ReadOnly = false;
            dgvConsults.Columns.Add("cost", "Стоимость");

            consults = Program.dbStyle.GetDataConsultsVisit(id_visit.ToString(), false);
            consultsOld = (ConsultsVisit)consults.Clone();
            txtBxCostConsults.Text = consults.GetCostConsults.ToString();

            //после получения нового списка материалов, подписываемся на событие о его изменении
            //styles.ListChanged += new ListChangedEventHandler(styles_ListChanged);

            dgvConsults.DataSource = consults;
            foreach (DataGridViewColumn item in dgvConsults.Columns)
            {
                item.DataPropertyName = item.Name;
            }
        }

        private void setDgvStylesOrderColumns()
        {
            dgvStyles.Columns[2].DisplayIndex = 0;
            dgvStyles.Columns[3].DisplayIndex = 2;
            dgvStyles.Columns[4].DisplayIndex = 1;
        }

        /// <summary>
        /// Добавляет поле типа ComboBox в DataGridView
        /// </summary>
        /// <param name="dataPropertyName">Имя колонки в основной таблице</param>
        /// <param name="headerText">Отображаемое имя колонки (заголовок)</param>
        /// <param name="valueMember">Имя колонки из связанной таблицы</param>
        /// <param name="displayMember">Отображаемая колонка из связанной таблицы</param>
        /// <param name="tableMember">Связанная таблица</param>
        private void addCmbBxToDGV(DataGridView _dgv, string dataPropertyName, string headerText, string valueMember, string displayMember, string tableMember)
        {
            _dgv.AutoGenerateColumns = false;
            DataGridViewComboBoxColumn dgvCmbBx = new DataGridViewComboBoxColumn();
            dgvCmbBx.DataPropertyName = dataPropertyName;
            dgvCmbBx.HeaderText = headerText;
            dgvCmbBx.DropDownWidth = 160;
            //            dgvCmbBx.Width = 90;
            //            dgvCmbBx.MaxDropDownItems = 2;
            dgvCmbBx.FlatStyle = FlatStyle.Flat;

            if (tableMember.Equals("Employ") && !chBxAccept.Checked) 
                dgvCmbBx.DataSource = Program.dbStyle.GetDataReference("Select distinct " + valueMember + ", " + displayMember + " from [" + tableMember + "] where isLeave = false", true);
            else
                dgvCmbBx.DataSource = Program.dbStyle.GetDataReference("Select distinct " + valueMember + ", " + displayMember + " from [" + tableMember + "]", true);

            dgvCmbBx.ValueMember = valueMember;
            dgvCmbBx.DisplayMember = displayMember;

            //            dataGridView1.Columns.Insert(dataGridView1.ColumnCount, dgvCmbBx);
            _dgv.Columns.Add(dgvCmbBx);
        }

        void addColumnAmount(DataGridView _dgv) 
        {
            DataGridViewTextBoxColumn dgvTxtBx = new DataGridViewTextBoxColumn();
            dgvTxtBx.Name = "amount";
            dgvTxtBx.HeaderText = "Количество";
            _dgv.Columns.Add(dgvTxtBx);
        }
        
        MaterialsVisit mv;
        MaterialsVisit mvOld;
        private void FillDgvMaterials()
        {
            dgvMaterials.AutoGenerateColumns = false;
            dgvMaterials.Columns.Add("id", "id");
            dgvMaterials.Columns[0].Visible = false;

            dgvMaterials.Columns.Add("id_mat", "id_mat");
            dgvMaterials.Columns[1].Visible = false;

            dgvMaterials.Columns.Add("name_m", "Наименование");
            dgvMaterials.Columns[2].ReadOnly = true;
            
            dgvMaterials.Columns.Add("amount", "Количество");
            dgvMaterials.Columns.Add("ed_izm", "Ед. изм.");
            
            mv = Program.dbStyle.GetDataMaterialsVisit(id_visit.ToString(), false);
            mvOld = (MaterialsVisit)mv.Clone();
            //после получения нового списка материалов, подписываемся на событие о его изменении
            //mv.ListChanged +=new ListChangedEventHandler(mv_ListChanged);

            dgvMaterials.DataSource = mv;
            foreach (DataGridViewColumn item in dgvMaterials.Columns)
            {
                item.DataPropertyName = item.Name;
            }
        }

        StylesVisit styles;
        StylesVisit stylesOld;

        private void FillDgvStyles()
        {
            dgvStyles.AutoGenerateColumns = false;
            dgvStyles.Columns.Add("id", "id");
            dgvStyles.Columns[0].Visible = false;
            
            dgvStyles.Columns.Add("id_style", "id_style");
            dgvStyles.Columns[1].Visible = false;

            dgvStyles.Columns.Add("name_st", "Услуга");
            dgvStyles.Columns[2].ReadOnly = true;
            
            //dgvStyles.Columns.Add("id_employ", "Исполнитель");
            //dgvStyles.Columns[2].Visible = false;
            dgvStyles.Columns.Add("cost", "Стоимость");

            styles = Program.dbStyle.GetDataStylesVisit(id_visit.ToString(), false);
            stylesOld = (StylesVisit)styles.Clone();
            txtBxCostStyle.Text = styles.GetCostStyles.ToString();

            //после получения нового списка материалов, подписываемся на событие о его изменении
            //styles.ListChanged += new ListChangedEventHandler(styles_ListChanged);

            dgvStyles.DataSource = styles;
            foreach (DataGridViewColumn item in dgvStyles.Columns)
            {
                item.DataPropertyName = item.Name;
            }
        }

        void mv_ListChanged(object sender, ListChangedEventArgs e)
        {
        }

        //private void FillItmsCmbBx()
        //{
        //    List <CmbBxType> listEmployes = Program.dbStyle.GetEmployes();
        //    foreach (CmbBxType item in listEmployes)
        //    {
        //        cmbBxEmploy.Items.Add(item);
        //    }

        //    List<CmbBxType> listStyles = Program.dbStyle.GetStyles();
        //    foreach (CmbBxType item in listStyles)
        //    {
        //        cmbBxStyle.Items.Add(item);
        //    }
        //}
        
        bool isEditMode = false;
        Main visitForm;
        UInt32 id_client;
        Int32 id_visit;
        UInt32 discountConst;
        float costVisit = 0;
        bool isCancelExit = false;
        BufFields bufData;

        public string FirstName = "";
        public string LastName = "";
        public string MiddleName = "";
        public DateTime? BirthDay ;

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        struct BufFields
        {
            public DateTime DateVisit;
            public int Employ;
            public int Style;
            public string CostStyle;
            public string Consult;
            public string CostConsults;
            public string Note;
            public string DiscountSeason;
            public string CostVisit;
        }

        public void FillBufFields()
        {
            bufData = new BufFields();

            bufData.DateVisit = dTPDateVisit.Value;
            //bufData.Employ = cmbBxEmploy.SelectedIndex;
            //bufData.Style =  cmbBxStyle.SelectedIndex;
            bufData.CostStyle = txtBxCostStyle.Text.Trim(' ');

            bufData.CostConsults = txtBxCostConsults.Text.Trim(' ');
            bufData.Note = txtBxNote.Text.Trim(' ');
            bufData.DiscountSeason = txtBxDiscountSeason.Text.Trim(' ');
            bufData.CostVisit = txtBxCostVisit.Text.Trim(' ');
        }

        /// <summary>
        /// заполнить поля формы используя данные из выделенной строки в таблице основной формы
        /// </summary>
        /// <param name="_row"></param>
        /// <returns>строка гриды</returns>
        public void FillTxtBx(DataGridViewRow _row)
        {
            if (_row.Cells["DateVisit"].Value != System.DBNull.Value)
                dTPDateVisit.Value = (DateTime)_row.Cells["DateVisit"].Value;
            else
                dTPDateVisit.Value = new DateTime(1900, 01, 01);

            //cmbBxEmploy.SelectedIndex = (int)_row.Cells["id_e"].Value - 1;
            //cmbBxStyle.SelectedIndex = (int)_row.Cells["id_st"].Value - 1;
            txtBxCostStyle.Text = _row.Cells["CostStyle"].Value.ToString();

            txtBxNote.Text = _row.Cells["Notes"].Value.ToString();
            txtBxDiscountSeason.Text = _row.Cells["DiscountSeason"].Value.ToString();
            txtBxCostVisit.Text = _row.Cells["CostVisit"].Value.ToString();
            txtBxWhoAdd.Text = _row.Cells["whoAdd"].Value.ToString();
            txtBxWhoEdit.Text = _row.Cells["whoEdit"].Value.ToString();

            id_visit = Convert.ToInt32(_row.Cells["id"].Value);
            chBxAccept.Checked = (bool)_row.Cells["Accept"].Value;
        }

        bool isValidRangeMaterialsEDZIM()
        {
            foreach (MaterialVisit item in mv)
                if (item.Ed_izm.Length > 50)
                    return false;

            return true;
        }

        bool isValidMaterialsEDZIM()
        {
            foreach (MaterialVisit item in mv)
                if (item.Ed_izm.Length == 0)
                    return false;

            return true;
        }

        bool isValidMaterialsAmount()
        {
            foreach (MaterialVisit item in mv)
                if (item.Amount < 0 || item.Amount > 2147483600)
                    return false;

            return true;
        }

        bool isValidRangeDiscountSeason()
        {
            try
            {
                if (txtBxDiscountSeason.Text.Length == 0)
                    txtBxDiscountSeason.Text = "0";
                if (Convert.ToInt16(txtBxDiscountSeason.Text) < 0 || Convert.ToInt16(txtBxDiscountSeason.Text) > 100)
                    return false;
                else
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        bool isValidDiscountSeason()
        {
            try
            {
                
                if (txtBxDiscountSeason.Text.Length == 0)
                    txtBxDiscountSeason.Text = "0";
                if (discountConst > 0 && Convert.ToInt16(txtBxDiscountSeason.Text) > 0 && Convert.ToInt16(txtBxDiscountSeason.Text) <= 100)
                    return false;
                else
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        bool isValidEmploy()
        {
            foreach (StyleVisit item in styles)
                if (item.Id_employ <= 0)
                    return false;
            return true;
        }

        bool isValidConsultsCost()
        {
            foreach (ConsultVisit item in consults)
                if (item.Cost < 0 || item.Cost > 2147483600)
                    return false;

            return true;
        }

        bool isValidStylesCost()
        {
            foreach (StyleVisit item in styles)
                if (item.Cost < 0 || item.Cost > 2147483600)
                    return false;

            return true;
        }

        bool isValidVisitCost()
        {
            foreach (StyleVisit item in styles)
                if (item.Cost < 0 || item.Cost > 2147483600)
                    return false;

            return true;
        }



        bool isValid()
        {

            //if (cmbBxEmploy.SelectedIndex == -1 || cmbBxStyle.SelectedIndex == -1)
            //{
            //    DialogResult dr = MessageBox.Show("Не заполнены поля: оказанная услуга, специалист", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}
            if (!isValidEmploy())
            {
                DialogResult dr = MessageBox.Show("Колонка 'Специалист' в списке оказанных услуг не может быть пустой", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else if (!isValidStylesCost())
            {
                DialogResult dr = MessageBox.Show("Колонка 'Стоимость' в списке оказанных услуг содержит неверные данные", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else if (!isValidMaterialsAmount())
            {
                DialogResult dr = MessageBox.Show("Колонка 'Количество' в списке расходных материалов содержит неверные данные", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else if (!isValidRangeMaterialsEDZIM())
            {
                DialogResult dr = MessageBox.Show("Колонка 'Ед. изм.' в списке расходных материалов не может быть больше 50 символов", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            //else if (!isValidMaterialsEDZIM())
            //{
            //    DialogResult dr = MessageBox.Show("Колонка 'Ед. изм.' в списке расходных материалов не может быть пустой", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}
            else if (!isValidConsultsCost())
            {
                DialogResult dr = MessageBox.Show("Колонка 'Стоимость' в списке консультаций содержит неверные данные", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else if (!isValidRangeDiscountSeason())
            {
                DialogResult dr = MessageBox.Show("Сезонная скидка должна быть в диапазоне от 0 до 100%", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else if (!isValidDiscountSeason())
            {
                DialogResult dr = MessageBox.Show("Сезонная скидка не может быть больше 0% если постоянная скидка больше 0%", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else if (!isValidVisitCost())
            {
                DialogResult dr = MessageBox.Show("Стоимость посещения содержит неверные данные", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            //else if (!isValidVisitCostUnChange())
            //{
            //    DialogResult dr = MessageBox.Show("Стоимость посещения отличается от расчетной", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}
            return true;
        }

        void  setIdVisitInRowsDGV()
        {
            //foreach (DataGridViewRow item in dgvMaterials.Rows)
            //{
            //    if (item.DataBoundItem != null && item.Cells != null)
            //        item.Cells["id_v"].Value = id_visit;
            //}
        }

        bool updateVisit()
        {
            int discountSeason = 0;
            float costStyle = 0;
            float costConsult = 0;
            float costVisit = 0;

            int.TryParse(txtBxDiscountSeason.Text.Trim(' ').Replace(".", ","), out discountSeason);
            float.TryParse(txtBxCostStyle.Text.Trim(' ').Replace(".", ","), out costStyle);
            float.TryParse(txtBxCostConsults.Text.Trim(' ').Replace(".", ","), out costConsult);
            float.TryParse(txtBxCostVisit.Text.Trim(' ').Replace(".", ","), out costVisit);
            
            setIdVisitInRowsDGV();

            log.Info("/////////////////////////////////////////////////////////////////////////////////////////");

            log.Info("Attempt update visit...");

            log.Info("id_visit = " + id_visit+ " id_client = " + id_client + " date visit = " + dTPDateVisit.Value.Date + " currUserWorking = " + Program.currUserWorking +
                " costStyle = " + costStyle + " TxtConsult = " + "" + " costConsult = " + costConsult +
                " TxtNote = " + txtBxNote.Text.Trim(' ') + " discountSeason = " + discountSeason + " costVisit = " + costVisit +
                " Accept = " + chBxAccept.Checked);

            return Program.dbStyle.UpdateVisit(id_visit, id_client, Program.currUserWorking, dTPDateVisit.Value.Date,
            costStyle, "", costConsult, txtBxNote.Text.Trim(' '),
            discountSeason, costVisit, chBxAccept.Checked) &&
                    Program.dbStyle.UpdateMaterialsVisit(id_visit, mv) &&
                    Program.dbStyle.UpdateStylesVisit(id_visit, styles) &&
                    Program.dbStyle.UpdateConsultsVisit(id_visit, consults);
        }

        public int idNewVisit = 0;
        void addVisit()
        {
            uint discountSeason = 0;
            float costStyle = 0;
            float costConsults = 0;


            uint.TryParse(txtBxDiscountSeason.Text.Trim(' '), out discountSeason);
            float.TryParse(txtBxCostStyle.Text.Trim(' ').Replace(".",","), out costStyle);
            float.TryParse(txtBxCostConsults.Text.Trim(' ').Replace(".", ","), out costConsults);
            float.TryParse(txtBxCostVisit.Text.Trim(' ').Replace(".", ","), out costVisit);

            log.Info("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");

            log.Info("Attempt insert visit...");

            log.Info("id_client = " + id_client + " date visit = " + dTPDateVisit.Value.Date + " currUserWorking = " + Program.currUserWorking +
                " costStyle = " + costStyle + " TxtConsult = " + "" + " costConsult = " + costConsults +
                " TxtNote = " + txtBxNote.Text.Trim(' ') + " discountSeason = " + discountSeason + " costVisit = " + costVisit +
                " Accept = " + chBxAccept.Checked);

            Int32 _id = Program.dbStyle.InsertVisit(id_client, dTPDateVisit.Value.Date, Program.currUserWorking,
            costStyle, "", costConsults, txtBxNote.Text.Trim(' '),
            discountSeason, costVisit, chBxAccept.Checked);

            idNewVisit = _id;
            if (_id >= 0)
            {
                log.Info("Attempt insert materials to visit...");

                id_visit = Convert.ToInt32(_id);
                Program.dbStyle.InsertMaterialsVisit(_id, mv);

                log.Info("Attempt insert styles to visit...");

                Program.dbStyle.InsertStylesVisit(_id, styles);

                log.Info("Attempt insert consults to visit...");

                Program.dbStyle.InsertConsultsVisit(_id, consults);
            }
            setIdVisitInRowsDGV();

            //Program.dbStyle.UpdateMaterialsVisit((DataTable)bindingSource1.DataSource);
            
        }


        private void frmNewVisit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                if (!isValid())
                {
                    e.Cancel = true;
                    return;
                }
                else
                {
                    if (isChangesCostVisit())
                    {
                        DialogResult dr = MessageBox.Show("Стоимость посещения не совпадает с расчетной. Продолжить?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (dr == System.Windows.Forms.DialogResult.No)
                        {
                            e.Cancel = true;
                            return;
                        }
                    }

                    if (!Program.isAdminMode && chBxAccept.Checked)
                    {
                        DialogResult dr1 = MessageBox.Show("Вы уверены что хотите сохранить и подтвердить посещение и его стоимость? Дальнейшее изменение посещения будет невозможно", "Внимание", MessageBoxButtons.OKCancel);
                        if (dr1 == DialogResult.Cancel)
                        {
                            e.Cancel = true;
                            return;
                        }
                    }

                    if (!isEditMode) //если режим добавления
                    {
                        addVisit();
                        FillBufFields();
                    }
                    else
                    {
                        if (updateVisit())
                            FillBufFields();
                        else
                        {
                            MessageBox.Show("При сохранении изменений произошла ошибка");
                            e.Cancel = true;
                        }
                    }
                }
            }
            else
            {
                if (IsChangesData) //если изменились данные
                {
                    if (isEditMode) //если режим изменения и данные изменены, то перед выходом спрашиваем
                    {
                        DialogResult dr = MessageBox.Show("Данные посещения изменены. Закрыть окно без сохранения?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (dr == System.Windows.Forms.DialogResult.No)
                            e.Cancel = true;
                    }
                    else
                    {
                        DialogResult dr = MessageBox.Show("Посещение не добавлено в базу данных. Закрыть окно без добавления?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (dr == System.Windows.Forms.DialogResult.No)
                            e.Cancel = true;
                    }
                }
                else //если не изменились данные
                {
                    if (isChangesCostVisit() && !chBxAccept.Checked)  //если расчетная стоимость визита не совпадает с существующей и неподтв. посещ. то выдаем предупрежд.
                    { 
                        DialogResult dr = MessageBox.Show("Стоимость посещения не совпадает с расчетной. Закрыть окно без изменений?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (dr == System.Windows.Forms.DialogResult.No)
                            e.Cancel = true;
                    }
                }
            }
        }

        public bool IsChangesData
        {
            get
            {
                if (!bufData.CostConsults.Equals(txtBxCostConsults.Text.Trim(' ')) ||
                    !bufData.CostStyle.Equals(txtBxCostStyle.Text.Trim(' ')) ||
                    !bufData.CostVisit.Equals(txtBxCostVisit.Text.Trim(' ')) ||
                    !bufData.DateVisit.Date.Equals(dTPDateVisit.Value.Date) ||
                    !bufData.DiscountSeason.Equals(txtBxDiscountSeason.Text.Trim(' ')) ||

                    //!bufData.Employ.Equals(cmbBxEmploy.SelectedIndex) ||
                    //!bufData.Style.Equals(cmbBxStyle.SelectedIndex) ||

                    !bufData.Note.Equals(txtBxNote.Text.Trim(' ')) ||
                    isChangesMaterials() ||
                    isChangesConsults() ||
                    isChangesStyles() 
                    )
                {
                    return true;
                }
                else
                    return false;
            }
        }

        bool isChangesMaterials()
        {
            MaterialsVisit mvNew = (MaterialsVisit)mv.Clone();
            if (mvOld.Count == mvNew.Count)
            {
                for (int i = 0; i < mvOld.Count; i++)
                {
                    if (!mvOld[i].Id_mat.Equals(mvNew[i].Id_mat) ||
                        !mvOld[i].Amount.Equals(mvNew[i].Amount) ||
                        !mvOld[i].Ed_izm.Equals(mvNew[i].Ed_izm))
                        return true;
                }
            }
            else
            {
                return true;
            }

            return false;
        }

        bool isChangesConsults()
        {
            ConsultsVisit consultsNew = (ConsultsVisit)consults.Clone();
            if (consultsOld.Count == consultsNew.Count)
            {
                for (int i = 0; i < consultsOld.Count; i++)
                {
                    if (!consultsOld[i].ConsultName.Equals(consultsNew[i].ConsultName) ||
                        !consultsOld[i].Cost.Equals(consultsNew[i].Cost))
                            return true;
                }
            }
            else
            {
                return true;
            }

            return false;
        }

        bool isChangesStyles()
        {
            StylesVisit stylesNew = (StylesVisit)styles.Clone();
            if (stylesOld.Count == stylesNew.Count)
            {
                for (int i = 0; i < stylesOld.Count; i++)
                {
                    if (!stylesOld[i].Id_style.Equals(stylesNew[i].Id_style) ||
                        !stylesOld[i].Cost.Equals(stylesNew[i].Cost) ||
                        !stylesOld[i].Id_employ.Equals(stylesNew[i].Id_employ))
                        return true;
                }
            }
            else
            {
                return true;
            }

            return false;
        }

        bool isChangesCostVisit()
        {
            float.TryParse(txtBxCostVisit.Text.Trim(' ').Replace(".", ","), out costVisit);

            if (costVisit != calcCostVisit(false))
                return true;
            else
                return false;
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            costVisit = calcCostVisit(true);

            if (isValid())
                txtBxCostVisit.Text = costVisit.ToString();
        }

        float calcCostVisit(bool isShowCalc)
        {
            try
            {
                uint discountSeason = 0;
                uint discount = 0;
                float costStyle = 0;
                float costConsults = 0;
                float costV = 0;

                if (isValidRangeDiscountSeason())
                    uint.TryParse(txtBxDiscountSeason.Text.Trim(' ').Replace(".", ","), out discountSeason);

                float.TryParse(txtBxCostStyle.Text.Trim(' ').Replace(".", ","), out costStyle);
                float.TryParse(txtBxCostConsults.Text.Trim(' ').Replace(".", ","), out costConsults);
                float.TryParse(txtBxCostVisit.Text.Trim(' ').Replace(".", ","), out costVisit);

                if (discountSeason > 0)
                    discount = discountSeason;
                else if (discountConst > 0)
                    discount = discountConst;

                costV = (costStyle - (discount * costStyle) / 100) + costConsults;

                costStyle = Convert.ToSingle(Math.Round(costStyle, 2));
                if (isShowCalc)
                    txtBxCalc.Text = costStyle.ToString() + " - " + ((discount * costStyle) / 100).ToString() + " + " + costConsults.ToString();
                return costV;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            using (ReferenceForm spr = new ReferenceForm(References.Materials, true))
            {
                if (spr.ShowDialog() == DialogResult.OK)
                {
                        //foreach (Material item in mv)
                        //{
                        //    if (item.Name_m.Equals(spr.currMaterial.Name_m))
                        //        MessageBox.Show("");
                        //}

                    mv.Add(spr.currMaterial);
                    //((MaterialsVisit)dgvMaterials.DataSource).Add();
                }
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            mv.RemoveAt(dgvMaterials.CurrentRow.Index);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void dgvMaterials_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                float amountMaterial = 0;
                string val = (sender as DataGridView).EditingControl.Text;
                //string val = (sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                //string _buf = val.Trim(' ').Replace(".", ",");
                //_buf = _buf.Substring(0, _buf.IndexOf(','));
                float.TryParse(val.Trim(' ').Replace(".", ","), out amountMaterial);
                //(sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex].Value = (object)costStyle;
                (sender as DataGridView).EditingControl.Text = amountMaterial.ToString();

            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            using (ReferenceForm spr = new ReferenceForm(References.Styles, true))
            {
                if (spr.ShowDialog() == DialogResult.OK)
                {
                    //foreach (Material item in mv)
                    //{
                    //    if (item.Name_m.Equals(spr.currMaterial.Name_m))
                    //        MessageBox.Show("");
                    //}

                    styles.Add(spr.currStyle);
                    txtBxCostStyle.Text = styles.GetCostStyles.ToString();
                    //((MaterialsVisit)dgvMaterials.DataSource).Add();
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            styles.RemoveAt(dgvStyles.CurrentRow.Index);
            txtBxCostStyle.Text = styles.GetCostStyles.ToString();
        }


        float calcCostStyles()
        {
            float summ = 0;
            foreach (StyleVisit item in styles)
            {
                summ = summ + item.Cost;
            }

            return summ;
        }

        private void dgvStyles_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                float costStyle = 0;
                string val = (sender as DataGridView).EditingControl.Text;
                //string val = (sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                //string _buf = val.Trim(' ').Replace(".", ",");
                //_buf = _buf.Substring(0, _buf.IndexOf(','));
                float.TryParse(val.Trim(' ').Replace(".", ","), out costStyle);
                //(sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex].Value = (object)costStyle;
                (sender as DataGridView).EditingControl.Text = costStyle.ToString();

            }
          //  
                //MessageBox.Show(e.Exception.InnerException.ToString());
        }

        private void dgvStyles_Paint(object sender, PaintEventArgs e)
        {
            txtBxCostStyle.Text = styles.GetCostStyles.ToString();
        }

        private void dgvConsults_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                float costConsult = 0;
                string val = (sender as DataGridView).EditingControl.Text;
                //string val = (sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                //string _buf = val.Trim(' ').Replace(".", ",");
                //_buf = _buf.Substring(0, _buf.IndexOf(','));
                float.TryParse(val.Trim(' ').Replace(".", ","), out costConsult);
                //(sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex].Value = (object)costStyle;
                (sender as DataGridView).EditingControl.Text = costConsult.ToString();

            }
        }

        private void dgvConsults_Paint(object sender, PaintEventArgs e)
        {
            txtBxCostConsults.Text = consults.GetCostConsults.ToString();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {

            ConsultVisit currConsult = new ConsultVisit();
            currConsult.Id = -1;
            currConsult.ConsultName = "";
            currConsult.Cost = 0;

            consults.Add(currConsult);
            txtBxCostConsults.Text = consults.GetCostConsults.ToString();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            consults.RemoveAt(dgvConsults.CurrentRow.Index);
            txtBxCostConsults.Text = consults.GetCostConsults.ToString();
        }



    }
}
