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
    public partial class ReferenceForm : Form
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(ReferenceForm));

        public ReferenceForm(References _refs, bool _isSelectMode)
        {
            InitializeComponent();

            refs = _refs;
            isSelectMode = _isSelectMode;
            setupReference(refs);
            
            getData();

            setInvisibleColumnsDGV();
            setNameColumnsDGV();

            if (refs == References.ClientsWidthBirthday)
            {
                btnSaveData.Visible = false;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
                dataGridView1.AllowUserToResizeRows = false;
                dataGridView1.ReadOnly = true;
                bindingNavigator1.Visible = false;
            }

            flagEditCell = false;
            btnSaveData.Enabled = false;

            if (_isSelectMode)
            {
                btnSaveData.Text = "Ok";
                btnSaveData.Enabled = true;
                btnSaveData.DialogResult = DialogResult.OK;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
                dataGridView1.AllowUserToResizeRows = false;
                dataGridView1.ReadOnly = true;
                bindingNavigator1.Visible = false;
            }

            if (refs == References.UsersPrg) 
                addCmbBxToDGV(dataGridView1, "id_access", "Уровень доступа", "id", "NameAccess", "AccessLevel");

            setOrderDGV();

            if ((refs == References.Styles || refs == References.Materials) && _isSelectMode)
            {
                dataGridView1.MultiSelect = true;
            }
        }

        bool isSelectMode = false;
        References refs;
        TableInfo ti;
        BindingSource bindingSource1 = new BindingSource();
        bool flagEditCell;

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
            dgvCmbBx.Name = displayMember;
            dgvCmbBx.DataPropertyName = dataPropertyName;
            dgvCmbBx.HeaderText = headerText;
            dgvCmbBx.DropDownWidth = 160;
            //            dgvCmbBx.Width = 90;
            //            dgvCmbBx.MaxDropDownItems = 2;
            dgvCmbBx.FlatStyle = FlatStyle.Flat;

            dgvCmbBx.DataSource = Program.dbStyle.GetDataReference("Select distinct " + valueMember + ", " + displayMember + " from [" + tableMember + "]", true);
            dgvCmbBx.ValueMember = valueMember;
            dgvCmbBx.DisplayMember = displayMember;

            //            dataGridView1.Columns.Insert(dataGridView1.ColumnCount, dgvCmbBx);
            if (refs == References.Styles && isSelectMode)
                dgvCmbBx.ReadOnly = true;
            _dgv.Columns.Add(dgvCmbBx);
        }

        void setInvisibleColumnsDGV()
        {
            if (ti.ColumnsNotVisible != null)
                foreach (string colName in ti.ColumnsNotVisible)
                {
                    if (dataGridView1.Columns[colName] != null)
                        dataGridView1.Columns[colName].Visible = false;
                }
        }

        void setOrderDGV()
        {
            switch (refs)
            {
                case References.Materials:
                    dataGridView1.Sort(dataGridView1.Columns["name_m"], ListSortDirection.Ascending);
                    break;
                case References.Styles:
                    dataGridView1.Sort(dataGridView1.Columns["name_style"], ListSortDirection.Ascending);
                    break;
                case References.Employes:
                    dataGridView1.Sort(dataGridView1.Columns["FIO"], ListSortDirection.Ascending);
                    break;
                case References.ClientsWidthBirthday:
                    dataGridView1.Sort(dataGridView1.Columns["LastName"], ListSortDirection.Ascending);
                    break;
                case References.UsersPrg:
                    dataGridView1.Sort(dataGridView1.Columns["UserName"], ListSortDirection.Ascending);
                    break;
                case References.AccessLevel:
                    dataGridView1.Sort(dataGridView1.Columns["NameAccess"], ListSortDirection.Ascending);
                    break;
                default:
                    break;
            }
        }

        void setNameColumnsDGV()
        {
            switch (refs)
            {
                case References.Materials:
                    dataGridView1.Columns["name_m"].HeaderText = "Наименование";
                    dataGridView1.Columns["unused"].HeaderText = "Не используется";
                    break;
                case References.Styles:
                    dataGridView1.Columns["name_style"].HeaderText = "Наименование";
                    break;
                case References.Employes:
                    dataGridView1.Columns["FIO"].HeaderText = "Фамилия И.О.";
                    dataGridView1.Columns["isLeave"].HeaderText = "Уволен";
                    break;
                case References.ClientsWidthBirthday:
                    dataGridView1.Columns["LastName"].HeaderText = "Фамилия";
                    dataGridView1.Columns["FirstName"].HeaderText = "Имя";
                    dataGridView1.Columns["MiddleName"].HeaderText = "Отчество";
                    dataGridView1.Columns["DiscountConst"].HeaderText = "Скидка пост";
                    dataGridView1.Columns["TelMobile"].HeaderText = "Моб. тел.";
                    dataGridView1.Columns["TelHome"].HeaderText = "Дом. тел.";
                    break;
                case References.UsersPrg:
                    dataGridView1.Columns["UserName"].HeaderText = "Имя пользователя";
                    dataGridView1.Columns["Pass"].HeaderText = "Пароль";
                    dataGridView1.Columns["isLeave"].HeaderText = "Уволен";
                    //dataGridView1.Columns["UserName"].HeaderText = "Имя пользователя";

                    break;
                case References.AccessLevel:
                    dataGridView1.Columns["NameAccess"].HeaderText = "Уровень доступа";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Получает данные из БД. Связывает полученные данные с визуальным компонентом для отображения
        /// </summary>
        private void getData()
        {
            if (refs != References.ClientsWidthBirthday)
            {
                if (refs == References.Materials && isSelectMode)
                    bindingSource1.DataSource = Program.dbStyle.GetDataReference("Select * from " + "[" + ti.TableName + "] where [" + ti.TableName+"].unused = false", false);
                else if (refs == References.Styles)
                    bindingSource1.DataSource = Program.dbStyle.GetDataReference("Select * from " + "[" + ti.TableName + "]", false);
                else
                    bindingSource1.DataSource = Program.dbStyle.GetDataReference("Select * from " + "[" + ti.TableName + "]", false);
            }
            else
                bindingSource1.DataSource = Program.dbStyle.GetClientsBirthday(DateTime.Now);

            dataGridView1.DataSource = bindingSource1;
            bindingNavigator1.BindingSource = bindingSource1;
        }

        private void btnSaveData_Click(object sender, EventArgs e)
        {
            if (!isSelectMode)
            {
                if (refs == References.UsersPrg)
                {
                    bool isAdminExist = false;

                    foreach (DataGridViewRow item in dataGridView1.Rows)
                    {
                        if (item.Cells[5].Value != null && (int)item.Cells[5].Value == 2)
                        {
                            isAdminExist = true;
                            break;
                        }
                    }

                    if (!isAdminExist)
                        MessageBox.Show("В таблице должен присутствовать хотя бы один пользователь с уровнем доступа 'Администратор'", "Внимание");
                    else
                    {
                        log.Info("Attempt update references...");
                        log.Info("TableName = " + ti.TableName);
                        Program.dbStyle.UpdateReference((DataTable)bindingSource1.DataSource);
                        getData();
                        flagEditCell = false;
                        btnSaveData.Enabled = false;
                    }
                }
                else
                {
                    log.Info("Attempt update references...");
                    log.Info("TableName = " + ti.TableName);
                    Program.dbStyle.UpdateReference((DataTable)bindingSource1.DataSource);
                    getData();
                    flagEditCell = false;
                    btnSaveData.Enabled = false;
                }
            }
            else
            {
                if (refs == References.Materials)
                {
                    fillCurrMaterial();
                }
                else if (refs == References.Styles)
                {
                    fillCurrStyle();
                }
                Close();    
            }
            
        }

        void fillCurrMaterial()
        {
            currMaterial = new MaterialVisit();
            currMaterial.Id = -1;
            currMaterial.Id_mat = (int)dataGridView1.CurrentRow.Cells["id"].Value;
            currMaterial.Name_m = dataGridView1.CurrentRow.Cells["name_m"].Value.ToString();
            currMaterial.Amount = 0;
            currMaterial.Ed_izm = "";
        }

        void fillCurrStyle()
        {
            currStyle = new StyleVisit();
            currStyle.Id = -1;
            currStyle.Id_style = (int)dataGridView1.CurrentRow.Cells["id"].Value;
            currStyle.Name_st = dataGridView1.CurrentRow.Cells["name_style"].Value.ToString();            
            currStyle.Id_employ = -1;
            currStyle.Cost = 0;
        }


        public MaterialVisit currMaterial = null;
        public StyleVisit currStyle = null;
        /// <summary>
        /// Настройка характеристик (заголовок, видимость полей) справочника в зависимости от его имени
        /// </summary>
        /// <param name="refs">Имя справочника из списка</param>
        private void setupReference(References refs)
        {
            ti = new TableInfo();

            switch (refs)
            {
                case References.Materials:
                    if (isSelectMode)
                        setupTableInfo("Materials", "Материалы", new string[3] { "id", "id_mt", "unused" });    
                    else
                        setupTableInfo("Materials", "Материалы", new string[2] { "id", "id_mt" });    
                    break;
                case References.Employes:
                    setupTableInfo("Employ", "Специалисты", new string[1] {"id" });
                    break;
                case References.Styles:
                    setupTableInfo("Styles", "Услуги", new string[1] {"id"  });
                    break;
                case References.ClientsWidthBirthday:
                    setupTableInfo("Clients", "Клиенты", new string[4] { "id_client",  "Address", "Birthday", "Notes" });
                    break;
                case References.UsersPrg:
                    setupTableInfo("Users", "Пользователи", new string[2] { "id", "id_access"});
                    break;
                case References.AccessLevel:
                    setupTableInfo("AccessLevel", "Уровни доступа", new string[1] { "id"});
                    break;
            }

            label7.Text = ti.HeaderTableName;
        }

        /// <summary>
        /// установка характеристик справочника
        /// </summary>
        /// <param name="tName">Название таблицы (англ.)</param>
        /// <param name="head_tName">Заголовок таблицы (рус.)</param>
        /// <param name="colNotVis">Массив невидимых полей</param>
        private void setupTableInfo(string tName, string head_tName, string[] colNotVis)
        {
            ti.TableName = tName;
            ti.HeaderTableName = head_tName;
            ti.ColumnsNotVisible = colNotVis;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            flagEditCell = true;
            if (!Program.isAdminMode)
                btnSaveData.Enabled = false;
            else
                btnSaveData.Enabled = true;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            flagEditCell = true;
            if (!Program.isAdminMode)
                btnSaveData.Enabled = false;
            else
                btnSaveData.Enabled = true;
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("В ячейке неверный тип значения!", "Внимание");
        }

        private void ReferenceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isSelectMode)
            {
                if (Program.isAdminMode)
                {
                    if (flagEditCell)
                    {
                        DialogResult dr = MessageBox.Show("Данные справочника изменены. Закрыть окно без сохранения?", "Внимание", MessageBoxButtons.OKCancel);

                        if (dr == DialogResult.Cancel)
                            e.Cancel = true;
                    }


                }
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            flagEditCell = true;
            if (!Program.isAdminMode)
                btnSaveData.Enabled = false;
            else
                btnSaveData.Enabled = true;
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (isSelectMode)
            {
                if (refs == References.Materials)
                {
                    fillCurrMaterial();
                }
                else if (refs == References.Styles)
                {
                    fillCurrStyle();
                }
                this.DialogResult = DialogResult.OK;
                Close();
            }
            
        }
    }


    /// <summary>
    /// Список справочников
    /// </summary>
    public enum References
    {
        Materials,
        Styles,
        Employes,
        ClientsWidthBirthday,
        UsersPrg,
        AccessLevel

    }

    /// <summary>
    /// Информация о справочнике. Невидимые поля, имя таблицы на русском, имя таблицы в базе
    /// </summary>
    public struct TableInfo
    {
        public string[] ColumnsNotVisible;
        public string HeaderTableName;
        public string TableName;
    }

    /// <summary>
    /// Информация о поле справочника
    /// </summary>
    public struct sNameColumn
    {
        public string headerColumn; //заголовок поля (русскоязычный)
        public string nameColumn; //имя поля (англоязычное)
    }
}
