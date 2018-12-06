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
using System.Reflection;

namespace Style
{
    public partial class Main : Form
    {

        /// <summary>
        /// Изменение свойства DoubleBuffered контрола.
        /// </summary>
        /// <param name="c">Ссылка на контрол.</param>
        /// <param name="value">Значение, которое надо установить DoubleBuffered.</param>
        void SetDoubleBuffered(Control c, bool value)
        {
            PropertyInfo pi = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic);
            if (pi != null)
            {
                pi.SetValue(c, value, null);
            }
        }

        public static readonly ILog log = LogManager.GetLogger(typeof(Main));
        
        int currDgvPositionClients;
        int currDgvPositionVisitsToClient;

        public Main()
        {
            InitializeComponent();
        }

        
        /// <summary>
        /// получить текущую выбранную строку гриды "ПРочиеДокументы"
        /// </summary>
        public DataGridViewRow GetCurrRowClientInDGV
        {
            get 
            {
                if (dgvClients.SelectedRows.Count > 0)
                    return dgvClients.SelectedRows[0];
                else
                    return null;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (dgvClients.SelectedRows.Count > 0)
                EditClient();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddClient();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            ViewVisits();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DeleteClient();
        }

        private void выхожToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (!Program.isAdminMode)
            {
                toolStripButton2.Enabled = false;
                tlStrpBtnDeleteVisit.Enabled = false;
                contextMenuStripClients.Items["ToolStripMenuItemDelete"].Enabled = false;
                contextMenuStripVisits.Items["toolStripMenuItemDeleteVisit"].Enabled = false;
                menuStrip1.Items["setupToolStripMenuItem"].Visible = false;
            }

            //init_tsPeriod();
            
            dgvClients.DataSource = clientsBindingSource;
            //dgvClients.VirtualMode = false;
            dgvVisits.DataSource = visitsBindingSource;
            //dgvVisits.VirtualMode = true;

            SetDoubleBuffered(dgvClients, true);
            SetDoubleBuffered(dgvVisits, true);

            GetDataClients();
            setNameColumnsDgvClients();

            GetDataVisits();
            setNameColumnsDgvVisits();
        }

        string nameColumnOrderClients = "LastName";
        ListSortDirection sortColumnOrderClients = ListSortDirection.Ascending;

        string nameColumnOrderVisits = "DateVisit";
        ListSortDirection sortColumnOrderVisits = ListSortDirection.Ascending;

        private void OrderByVisits(string _nameCol, ListSortDirection _sortOrder)
        {
            dgvVisits.Sort(dgvVisits.Columns[_nameCol], _sortOrder);
        }

        private void OrderByClients(string _nameCol, ListSortDirection _sortOrder)
        {
            dgvClients.Sort(dgvClients.Columns[_nameCol], _sortOrder);
        }



        //void conn_StateChange(object sender, StateChangeEventArgs e)
        //{
        //    toolStripStatusLabel4.Text = "Соединение с БД: ";

        //    switch (Program.dbStyle.conn.State)
        //    {
        //        case ConnectionState.Broken:
        //            toolStripStatusLabel4.Text += "Разрушено";
        //            break;
        //        case ConnectionState.Closed:
        //            toolStripStatusLabel4.Text += "Завершено";
        //            break;
        //        case ConnectionState.Connecting:
        //            toolStripStatusLabel4.Text += "Подключение";
        //            break;
        //        case ConnectionState.Executing:
        //            toolStripStatusLabel4.Text += "Выполнение";
        //            break;
        //        case ConnectionState.Fetching:
        //            toolStripStatusLabel4.Text += "Получение";
        //            break;
        //        case ConnectionState.Open:
        //            toolStripStatusLabel4.Text += "Установлено";
        //            break;
        //        default:
        //            break;
        //    }
        //}

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            DataSet dsUnAccept = Program.dbStyle.GetVisitsAccept(false);
            if (dsUnAccept != null && dsUnAccept.Tables["Visits"].Rows.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Присутствуют неподтвержденные посещения! Закрыть программу?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == System.Windows.Forms.DialogResult.No)
                    e.Cancel = true;
                else
                    Program.dbStyle.DisconnectDB();
            }
        }

        public void setStateDbOnForm(string str)
        {
            toolStripStatusLabel4.Text = "Status:" + str;
        }

        private BindingSource clientsBindingSource = new BindingSource();
        DataSet _birthdayClient = new DataSet();

        public void GetDataClients()
        {
            object isLock = new object();


            lock (isLock)
            {

                bool fl = false;
                int iii = 0;
                if (dgvClients.SelectedRows.Count > 0)
                {
                    iii = dgvClients.FirstDisplayedScrollingRowIndex;

                    currDgvPositionClients = dgvClients.SelectedRows[0].Index;
                    fl = true;
                }

                toolStripStatusLabel3.Text = "";
                DataSet data = new DataSet();
                DateTime? dt = null;
                //if (toolStripButton7.Checked)
                //    dt = dTP_BirthDay.Value;
                data = Program.dbStyle.GetClients(dt);

                _birthdayClient = Program.dbStyle.GetClients(DateTime.Now.Date);
                toolStripStatusLabelBallon.Enabled = _birthdayClient.Tables[0].Rows.Count > 0;
                setLabelBirthDays(_birthdayClient);

                clientsBindingSource.DataSource = data;
                clientsBindingSource.DataMember = "Clients";

                OrderByClients(nameColumnOrderClients, sortColumnOrderClients);

                dgvClients.FirstDisplayedScrollingRowIndex = iii;
                tsStatLbl_CntClient.Text = dgvClients.RowCount.ToString("000000");

                //управление видимостью иконки неакцептированных визитов
                toolStripStatusLabelUnAccept.Visible = false;
                for (int i = 0; i < dgvClients.RowCount; i++)
                {
                    if ((bool)dgvClients.Rows[i].Cells["Accept"].Value != true &&
                        (int)dgvClients.Rows[i].Cells["CountVisits"].Value > 0)
                    {
                        toolStripStatusLabelUnAccept.Visible = true;
                        break;
                    }
                }


            }
        }

        private void scrollDgv(DataGridView dgv)
        {
            int halfWay = (dgv.DisplayedRowCount(false) / 2);
            if (dgv.FirstDisplayedScrollingRowIndex + halfWay > dgv.SelectedRows[0].Index ||
                (dgv.FirstDisplayedScrollingRowIndex + dgv.DisplayedRowCount(false) - halfWay) <= dgv.SelectedRows[0].Index)
            {
                int targetRow = dgv.SelectedRows[0].Index;

                targetRow = Math.Max(targetRow - halfWay, 0);
                dgv.FirstDisplayedScrollingRowIndex = targetRow;

            }
        }

        void setLabelBirthDays(DataSet _birthdayClient)
        {
            int i = 0;
            toolStripStatusLabelBirthDay.Text = "";
            foreach (DataRow item in _birthdayClient.Tables[0].Rows)
            {
                i++;
                if (i>4)
                {
                    toolStripStatusLabelBirthDay.Text = toolStripStatusLabelBirthDay.Text + " . . .";
                    break;
                }
                StringBuilder str = new StringBuilder();
                str.Append(" " + item.Field<string>("LastName"));
                str.Append(" " + item.Field<string>("FirstName").Substring(0,1) + ".");
                str.Append(" " + item.Field<string>("MiddleName").Substring(0, 1) + ".");
                toolStripStatusLabelBirthDay.Text = toolStripStatusLabelBirthDay.Text + str.ToString() + ";";
            }
        }

        private void setNameColumnsDgvClients()
        {
            if (dgvClients.Columns.Count > 0)
            {
                dgvClients.Columns["id_client"].Visible = false;
                dgvClients.Columns["Accept"].Visible = false;
                dgvClients.Columns["CountVisits"].Visible = false;

                dgvClients.Columns["Address"].HeaderText = "Адрес";
                //dataGridView1.Columns["Address"].Width = 300;
                dgvClients.Columns["Address"].Visible = false;

                dgvClients.Columns["Notes"].HeaderText = "Примечание";
                //dataGridView1.Columns["Notes"].Width = 300;
                dgvClients.Columns["Notes"].Visible = false;

                dgvClients.Columns["LastName"].HeaderText = "Фамилия";
                //dataGridView1.Columns["LastName"].Width = 90;
                
                dgvClients.Columns["FirstName"].HeaderText = "Имя";
                //dataGridView1.Columns["FirstName"].Width = 90;
                
                dgvClients.Columns["MiddleName"].HeaderText = "Отчество";
                //dataGridView1.Columns["MiddleName"].Width = 90;
                
                dgvClients.Columns["TelMobile"].HeaderText = "Моб. тел.";
                //dataGridView1.Columns["MiddleName"].Width = 40;

                dgvClients.Columns["TelHome"].HeaderText = "Дом. тел.";
                dgvClients.Columns["TelHome"].Width = 50;

                dgvClients.Columns["Birthday"].HeaderText = "Дата рождения";
                dgvClients.Columns["Birthday"].Width = 60;
                
                dgvClients.Columns["DiscountConst"].HeaderText = "Скидка пост.";
                dgvClients.Columns["DiscountConst"].Width = 60;

            }
        }

        public bool findOpenForm(string name)
        {
            List<Form> result = new List<Form>();
            foreach (Form form in Application.OpenForms)
            {
                if (form.Name == name)
                    return true;
            }

            return false;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvClients.SelectedRows.Count > 0)
                EditClient();
        }

        void ViewVisits()
        {
            Visits visits = new Visits(this);
            visits.ShowDialog();
            visits.Dispose();
        }

        void AddClient()
        {
            if (findOpenForm("NewClient"))
                return;

            NewClient newClient = new NewClient(false, this); //если режим добавления, то передаем на форму значение false
            if (newClient.ShowDialog() == DialogResult.OK)
            {
                GetDataClients();
                if (newClient.idNewClient > 0)
                    for (int i = 0; i < dgvClients.Rows.Count; i++)
                    {
                        if (dgvClients.Rows[i].Cells["id_client"].Value != null &&
                            (int)dgvClients.Rows[i].Cells["id_client"].Value == newClient.idNewClient)
                        {
                            dgvClients.Rows[i].Selected = true;
                            dgvClients.FirstDisplayedScrollingRowIndex = i;
                            dgvClients.CurrentCell = dgvClients.SelectedRows[0].Cells["FirstName"];

                            int id_clientNew = (int)GetCurrRowClientInDGV.Cells["id_client"].Value;
                            GetDataVisits();
                            if (id_client != id_clientNew && dgvVisits.RowCount > 0)
                                dgvVisits.Rows[0].Selected = true;

                            id_client = (int)GetCurrRowClientInDGV.Cells["id_client"].Value;
                            discountConst = (int)GetCurrRowClientInDGV.Cells["DiscountConst"].Value;
                            FirstName = (string)GetCurrRowClientInDGV.Cells["FirstName"].Value;
                            LastName = (string)GetCurrRowClientInDGV.Cells["LastName"].Value;
                            MiddleName = (string)GetCurrRowClientInDGV.Cells["MiddleName"].Value;
                            if (GetCurrRowClientInDGV.Cells["BirthDay"].Value != DBNull.Value)
                                BirthDay = (DateTime)GetCurrRowClientInDGV.Cells["BirthDay"].Value;

                        }
                    }
            }
            newClient.Dispose();
        }

        void EditClient()
        {
            if (dgvClients.SelectedRows.Count > 0)
            {
                if (findOpenForm("NewClient"))
                    return;
                
                NewClient newClient = new NewClient(true, this); //если режим изменения, то передаем на форму значение true
                if (newClient.ShowDialog() == DialogResult.OK)
                {
                    GetDataClients();
                }
                newClient.Dispose();
            }
        }

        void DeleteClient()
        {
            if (dgvClients.SelectedRows.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Удалить клиента?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    UInt32 currIdClient = Convert.ToUInt32(dgvClients[dgvClients.Columns["id_client"].Index, dgvClients.SelectedRows[0].Index].Value.ToString());
                    
                    log.Info("-----------------------------------------------------------------------------------------");
                    log.Info("Attempt delete client...");
                    log.Info(" id_client = " + currIdClient);
                    
                    Program.dbStyle.DeleteClient(currIdClient);
                    GetDataClients();                    
                    GetDataVisits();
                }
            }
        }

        private void ToolStripMenuItemVisits_Click(object sender, EventArgs e)
        {
            ViewVisits();
        }

        private void ToolStripMenuItemAddClient_Click(object sender, EventArgs e)
        {
            AddClient();
        }

        private void ToolStripMenuItemEdit_Click(object sender, EventArgs e)
        {
            EditClient();
        }

        private void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            DeleteClient();
        }
        
        private void ToolStripMenuItemEmploye_Click(object sender, EventArgs e)
        {
            ReferenceForm frmRef = new ReferenceForm(References.Employes, false);
            frmRef.StartPosition = FormStartPosition.CenterScreen;
            frmRef.Text = "Справочник";
            frmRef.ShowDialog();
            frmRef.Dispose();
        }

        private void ToolStripMenuItemMaterials_Click(object sender, EventArgs e)
        {
            ReferenceForm frmRef = new ReferenceForm(References.Materials, false);
            frmRef.StartPosition = FormStartPosition.CenterScreen;
            frmRef.Text = "Справочник";
            frmRef.ShowDialog();
            frmRef.Dispose();
        }

        private void ToolStripMenuItemService_Click(object sender, EventArgs e)
        {
            ReferenceForm frmRef = new ReferenceForm(References.Styles, false);
            frmRef.StartPosition = FormStartPosition.CenterScreen;
            frmRef.Text = "Справочник";
            frmRef.ShowDialog();
            frmRef.Dispose();
        }

        DataTable clientsBirthday ;


        private void toolStripDropDownButtonBirthDay_Click(object sender, EventArgs e)
        {
            //ReferenceForm frmRef = new ReferenceForm(References.ClientsWidthBirthday, false);
            //frmRef.StartPosition = FormStartPosition.CenterScreen;
            //frmRef.Text = "Дни рождения клиентов сегодня";
            //frmRef.ShowDialog();
            //frmRef.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tsStatLb_DateTime.Text = DateTime.Now.ToString();
        }


        private BindingSource visitsBindingSource = new BindingSource();

        public void GetDataVisits()
        {
            object isLock = new object();


            lock (isLock)
            {
                int id_client = 0;
                int currDgvPositionVisits = 0;
                int iii = 0;

                if (GetCurrRowClientInDGV != null)
                    id_client = (int)GetCurrRowClientInDGV.Cells["id_client"].Value;

                if (dgvVisits.SelectedRows.Count > 0)
                {
                    iii = dgvVisits.FirstDisplayedScrollingRowIndex;
                    currDgvPositionVisits = dgvVisits.SelectedRows[0].Index;
                }

                DataSet data = new DataSet();

                data = Program.dbStyle.GetVisitsClient(id_client);

                visitsBindingSource.DataSource = data;
                visitsBindingSource.DataMember = "Visits";

                OrderByVisits(nameColumnOrderVisits, sortColumnOrderVisits);

                if (iii < dgvVisits.RowCount)
                    dgvVisits.FirstDisplayedScrollingRowIndex = iii;

                if (currDgvPositionVisits < dgvVisits.RowCount)
                    dgvVisits.Rows[currDgvPositionVisits].Selected = true;
                else
                    dgvVisits.Rows[dgvVisits.RowCount - 1].Selected = true;
            }
        }

        private void setNameColumnsDgvVisits()
        {
            if (dgvVisits.Columns.Count > 0)
            {
                dgvVisits.Columns["id"].Visible = false;
                dgvVisits.Columns["id_client"].Visible = false;
                dgvVisits.Columns["whoAdd"].Visible = false;
                dgvVisits.Columns["whoEdit"].Visible = false;

                dgvVisits.Columns["DateVisit"].HeaderText = "Дата посещения";
                //dgvVisits.Columns["DateVisit"].Width = 150;

                dgvVisits.Columns["CostStyle"].HeaderText = "Стоимость услуг";
                //dgvVisits.Columns["CostStyle"].Width = 150;

                dgvVisits.Columns["Consult"].HeaderText = "Консультация";
                dgvVisits.Columns["Consult"].Visible = false;
                //dgvVisits.Columns["Consult"].Width = 150;

                dgvVisits.Columns["CostConsult"].HeaderText = "Стоимость консультаций";
                //dgvVisits.Columns["CostConsult"].Width = 50;

                dgvVisits.Columns["DiscountSeason"].HeaderText = "Скидка сезонная";
                dgvVisits.Columns["DiscountSeason"].Width = 60;

                dgvVisits.Columns["CostVisit"].HeaderText = "Стоимость посещения";
                //dgvVisits.Columns["CostVisit"].Width = 50;

                dgvVisits.Columns["Notes"].HeaderText = "Примечание";
                dgvVisits.Columns["Notes"].Visible = false;
                //dgvVisits.Columns["Note"].Width = 300;

                dgvVisits.Columns["Accept"].HeaderText = "Подтверждение";
                dgvVisits.Columns["Accept"].Visible = true;
                //dgvVisits.Columns["Note"].Width = 300;

                //++++++++ порядок отображения столбцов
                dgvVisits.Columns["DateVisit"].DisplayIndex = 0;
                dgvVisits.Columns["DiscountSeason"].DisplayIndex = 1;
                //dgvVisits.Columns["FioEmploy"].DisplayIndex = 2;
                //dgvVisits.Columns["NameStyle"].DisplayIndex = 3;



            }
        }

        public int id_client;
        public int discountConst = 0;

        public string FirstName = "";
        public string LastName = "";
        public string MiddleName = "";
        public DateTime? BirthDay ;

        //выбор клиента ЛКМ
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id_clientNew = (int)GetCurrRowClientInDGV.Cells["id_client"].Value;
            GetDataVisits();
            if (id_client != id_clientNew && dgvVisits.RowCount > 0)
                dgvVisits.Rows[0].Selected = true;

            FillClientInfo();
        }

        private void dgvVisits_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvVisits.SelectedRows.Count > 0)
            {
                FillClientInfo();
                EditVisit();
            }
        }

        private void FillClientInfo()
        {
            id_client = (int)GetCurrRowClientInDGV.Cells["id_client"].Value;
            discountConst = (int)GetCurrRowClientInDGV.Cells["DiscountConst"].Value;
            FirstName = (string)GetCurrRowClientInDGV.Cells["FirstName"].Value;
            LastName = (string)GetCurrRowClientInDGV.Cells["LastName"].Value;
            MiddleName = (string)GetCurrRowClientInDGV.Cells["MiddleName"].Value;

            if (GetCurrRowClientInDGV.Cells["BirthDay"].Value != DBNull.Value)
                BirthDay = (DateTime)GetCurrRowClientInDGV.Cells["BirthDay"].Value;
        }

        /// <summary>
        /// получить текущую выбранную строку гриды "ПРочиеДокументы"
        /// </summary>
        public DataGridViewRow GetCurrRowVisitInDGV
        {
            get
            {
                if (dgvVisits.SelectedRows.Count > 0)
                    return dgvVisits.SelectedRows[0];
                else
                    return null;
            }
        }

        private void tlStrpBtnAddVisit_Click(object sender, EventArgs e)
        {
            FillClientInfo(); 
            AddVisit();
        }

        private void tlStrpBtnEditVisit_Click(object sender, EventArgs e)
        {
            if (dgvVisits.SelectedRows.Count > 0)
            {
                FillClientInfo();
                EditVisit();
            }
        }

        private void tlStrpBtnDeleteVisit_Click(object sender, EventArgs e)
        {
            DeleteVisit();
        }

        void AddVisit()
        {
            if (findOpenForm("NewVisit"))
                return;

            id_client = (int)GetCurrRowClientInDGV.Cells["id_client"].Value;

            frmNewVisit newVisit = new frmNewVisit(false, this);
            if (newVisit.ShowDialog() == DialogResult.OK)
            {
                GetDataVisits();
                GetDataClients();

                if (newVisit.idNewVisit > 0)
                    for (int i = 0; i < dgvVisits.Rows.Count; i++)
                    {
                        if (dgvVisits.Rows[i].Cells["id"].Value != null &&
                            (int)dgvVisits.Rows[i].Cells["id"].Value == newVisit.idNewVisit)
                        {
                            dgvVisits.Rows[i].Selected = true;
                            dgvVisits.FirstDisplayedScrollingRowIndex = i;
                        }
                    }
            }
            newVisit.Dispose();
        }

        void EditVisit()
        {
            int curPosClient = 0;
            int curPosVisit = 0;

            if (dgvVisits.SelectedRows.Count > 0)
            {
                frmNewVisit newVisite = new frmNewVisit(true, this);
                if (newVisite.ShowDialog() == DialogResult.OK)
                {
                    if (dgvClients.SelectedRows.Count > 0)
                        curPosClient = dgvClients.SelectedRows[0].Index;
                    
                    if (dgvVisits.SelectedRows.Count > 0)
                        curPosVisit = dgvVisits.SelectedRows[0].Index;
                    
                    GetDataClients();
                    GetDataVisits();

                }
                newVisite.Dispose();
            }
        }

        void DeleteVisit()
        {
            if (dgvVisits.SelectedRows.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Удалить посещение?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    UInt32 currIdVisit = Convert.ToUInt32(dgvVisits[dgvVisits.Columns["id"].Index, dgvVisits.SelectedRows[0].Index].Value.ToString());
                    log.Info("-----------------------------------------------------------------------------------------");
                    log.Info("Attempt delete visit...");
                    log.Info("id_visit = " + currIdVisit +" id_client = " +id_client);
                    
                    Program.dbStyle.DeleteVisit(currIdVisit);
                    GetDataVisits();
                }
            }
        }

        private void ToolStripMenuItemAddVisit_Click(object sender, EventArgs e)
        {
            FillClientInfo();
            AddVisit();
        }

        private void ToolStripMenuItemEditVisit_Click(object sender, EventArgs e)
        {
            FillClientInfo();
            EditVisit();
        }

        private void ToolStripMenuItemDeleteVisit_Click(object sender, EventArgs e)
        {
            DeleteVisit();
        }

        private void dgvClients_Sorted(object sender, EventArgs e)
        {
            nameColumnOrderClients = (sender as DataGridView).SortedColumn.Name;
            sortColumnOrderClients = (sender as DataGridView).SortOrder == SortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending;
        }

        private void dgvVisits_Sorted(object sender, EventArgs e)
        {
            nameColumnOrderVisits = (sender as DataGridView).SortedColumn.Name;
            sortColumnOrderVisits = (sender as DataGridView).SortOrder == SortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending;
        }

        DateTimePicker dTP_BirthDay;
        private void init_tsPeriod()
        {
            //ToolStripLabel tsLbl_begin = new ToolStripLabel();
            //tsLbl_begin.Text = "Период ";
            //toolStripMain.Items.Add(tsLbl_begin);
            //toolStripMain.Items[toolStripMain.Items.Count - 1].Alignment = ToolStripItemAlignment.Left;

            dTP_BirthDay = new DateTimePicker();
            dTP_BirthDay.Name = "dtpBeg";
            dTP_BirthDay.Size = new Size(100, 20);
            //dTP_BirthDay.ShowCheckBox = true;
            dTP_BirthDay.Format = DateTimePickerFormat.Short;
            dTP_BirthDay.Value = DateTime.Now.Date;
            //dTP_BirthDay.Checked = false;
            dTP_BirthDay.ValueChanged += new EventHandler(dTP_BirthDay_ValueChanged);
            toolStripMain.Items.Add(new ToolStripControlHost(dTP_BirthDay));
            toolStripMain.Items[toolStripMain.Items.Count - 1].Alignment = ToolStripItemAlignment.Left;

            ToolStripSeparator tsSeparator = new ToolStripSeparator();
            toolStripMain.Items.Add(tsSeparator);
            toolStripMain.Items[toolStripMain.Items.Count - 1].Alignment = ToolStripItemAlignment.Left;

        }

        private void dTP_BirthDay_ValueChanged(object sender, EventArgs e)
        {
            //if (toolStripButton7.Checked)
            //    GetDataClients();
        }

        private void toolStripButton7_CheckedChanged(object sender, EventArgs e)
        {
            //GetDataClients();
        }

        //событие, что курсор в таблице сместился на другую строку
        private void dgvClients_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvClients.SelectedRows.Count > 0)
            {
                FillClientInfo();
                tsStatLb_NumClient.Text = (dgvClients.CurrentRow.Index + 1).ToString();
            }
        }

        //кнопка поиска клиента
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (dgvClients.SelectedRows.Count < 1) return;

            if (searchClients(dgvClients.SelectedRows[0].Index))
            {
                scrollDgv(dgvClients);
                return;
            }

            if (dgvClients.SelectedRows[0].Index > 0)
            {
                searchClients(-1);
                scrollDgv(dgvClients);
            }

        }

        bool searchClients(int selIndex)
        {
            for (int idx = selIndex + 1; idx < dgvClients.Rows.Count; idx++)
            {
                for (int column = 0; column < dgvClients.Columns.Count; column++)
                {
                    if (dgvClients.Columns[column].Name.Equals("LastName") ||
                        dgvClients.Columns[column].Name.Equals("FirstName") ||
                        dgvClients.Columns[column].Name.Equals("TelMobile") ||
                        dgvClients.Columns[column].Name.Equals("TelHome") ||
                        dgvClients.Columns[column].Name.Equals("Birthday"))
                    {
                        if (dgvClients.Rows[idx].Cells[column].Value != null)
                        {
                            string str1 = dgvClients.Rows[idx].Cells[column].Value.ToString().ToLower().Trim();
                            string str2 = tsTxtBxFind.Text.ToLower().Trim();

                            if (str1.Contains(str2))
                            {
                                dgvClients.Rows[idx].Selected = true;                                
                                //dgvClients.FirstDisplayedScrollingRowIndex = idx;
                                dgvClients.CurrentCell = dgvClients.SelectedRows[0].Cells["FirstName"];
                                int id_clientNew = (int)GetCurrRowClientInDGV.Cells["id_client"].Value;
                                GetDataVisits();
                                if (id_client != id_clientNew && dgvVisits.RowCount > 0)
                                    dgvVisits.Rows[0].Selected = true;

                                FillClientInfo();

                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        //кнопка поиска визита
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (dgvVisits.SelectedRows.Count < 1) return;


            if (searchVisits(dgvVisits.SelectedRows[0].Index))
            {
                scrollDgv(dgvVisits);
                return;
            }

            if (dgvVisits.SelectedRows[0].Index > 0)
            {
                searchVisits(-1);
                scrollDgv(dgvVisits);
            }
        }

        bool searchVisits(int selIndex)
        {
            for (int idx = selIndex + 1; idx < dgvVisits.Rows.Count; idx++)
            {
                for (int column = 0; column < dgvVisits.Columns.Count; column++)
                {
                    if (dgvVisits.Columns[column].Name.Equals("DateVisit") ||
                        dgvVisits.Columns[column].Name.Equals("CostStyle") ||
                        dgvVisits.Columns[column].Name.Equals("CostConsult") ||
                        dgvVisits.Columns[column].Name.Equals("CostVisit"))
                    {
                        if (dgvVisits.Rows[idx].Cells[column].Value != null)
                        {
                            string str1 = dgvVisits.Rows[idx].Cells[column].Value.ToString().ToLower().Trim();
                            string str2 = tsTxtBxFindVisits.Text.ToLower().Trim();

                            if (str1.Contains(str2))
                            {
                                dgvVisits.Rows[idx].Selected = true;
                                dgvVisits.FirstDisplayedScrollingRowIndex = idx;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void ToolStripMenuItemAdmining_Click(object sender, EventArgs e)
        {

        }

        private void dgvVisits_SelectionChanged(object sender, EventArgs e)
        {
        }

        //подкрашиваем не акцептированные визиты 
        private void dgvVisits_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex > -1 && e.RowIndex < dgvVisits.RowCount )
            {
                if ((bool)dgvVisits.Rows[e.RowIndex].Cells["Accept"].Value != true)
                    ((DataGridView)sender).Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightPink;
                else
                    ((DataGridView)sender).Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                
            }
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReferenceForm frmRef = new ReferenceForm(References.UsersPrg, false);
            frmRef.StartPosition = FormStartPosition.CenterScreen;
            frmRef.Text = "Справочник";
            frmRef.ShowDialog();
            frmRef.Dispose();
        }

        private void accessLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReferenceForm frmRef = new ReferenceForm(References.AccessLevel, false);
            frmRef.StartPosition = FormStartPosition.CenterScreen;
            frmRef.Text = "Справочник";
            frmRef.ShowDialog();
            frmRef.Dispose();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {
            openReferenceFormClientsWidthBirthday();
        }

        private void toolStripStatusLabelBirthDay_Click(object sender, EventArgs e)
        {
            openReferenceFormClientsWidthBirthday();
        }


        void openReferenceFormClientsWidthBirthday()
        {
            ReferenceForm frmRef = new ReferenceForm(References.ClientsWidthBirthday, false);
            frmRef.StartPosition = FormStartPosition.CenterScreen;
            frmRef.Text = "Дни рождения клиентов";
            frmRef.ShowDialog();
            frmRef.Dispose();
        }

        private void toolStripStatusLabelBallon_Click(object sender, EventArgs e)
        {
            openReferenceFormClientsWidthBirthday();
        }

        //подкрашиваем клиентов с неакцептированными визитами
        private void dgvClients_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex > -1 && e.RowIndex < dgvClients.RowCount)
            {
                if ((bool)dgvClients.Rows[e.RowIndex].Cells["Accept"].Value != true &&
                    (int)dgvClients.Rows[e.RowIndex].Cells["CountVisits"].Value > 0)
                    ((DataGridView)sender).Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightPink;
                else
                    ((DataGridView)sender).Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;                
            }
        }

        private void ReportMonthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormSetupReport frm = new FormSetupReport(TypeReport.Month))
            {
                frm.ShowDialog();
            }
        }

        private void ReportYearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormSetupReport frm = new FormSetupReport(TypeReport.Year))
            {
                frm.ShowDialog();
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа является собственностью салона красоты \"Кристи\". Копирование и распространение запрещено!",
                "О программе...", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1); 
        }

        private void tsTxtBxFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (dgvClients.SelectedRows.Count < 1) return;

                if (searchClients(dgvClients.SelectedRows[0].Index))
                {
                    scrollDgv(dgvClients);
                    return;
                }

                if (dgvClients.SelectedRows[0].Index > 0)
                {
                    searchClients(-1);
                    scrollDgv(dgvClients);
                }
            }
        }

        private void tsTxtBxFindVisits_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (dgvVisits.SelectedRows.Count < 1) 
                    return;
                if (searchVisits(dgvVisits.SelectedRows[0].Index))
                {
                    scrollDgv(dgvVisits);
                    return;
                }
                if (dgvVisits.SelectedRows[0].Index > 0)
                {
                    searchVisits(-1);
                    scrollDgv(dgvVisits);
                }
            }
        }


        private void toolStripStatusLabelUnAccept_Click(object sender, EventArgs e)
        {
            if (dgvClients.SelectedRows.Count < 1) return;

            if (searchClientUnAccept(dgvClients.SelectedRows[0].Index))
            {
                scrollDgv(dgvClients);
                return;
            }

            if (dgvClients.SelectedRows[0].Index > 0)
            {
                searchClientUnAccept(-1);
                scrollDgv(dgvClients);
            }
        }

        bool searchClientUnAccept(int selIndex)
        {
            for (int idx = selIndex + 1; idx < dgvClients.Rows.Count; idx++)
            {
                if ((bool)dgvClients.Rows[idx].Cells["Accept"].Value != true &&
                    (int)dgvClients.Rows[idx].Cells["CountVisits"].Value > 0)
                {
                    dgvClients.Rows[idx].Selected = true;
                    //dgvClients.FirstDisplayedScrollingRowIndex = idx;
                    dgvClients.CurrentCell = dgvClients.SelectedRows[0].Cells["FirstName"];
                    int id_clientNew = (int)GetCurrRowClientInDGV.Cells["id_client"].Value;
                    GetDataVisits();
                    if (id_client != id_clientNew && dgvVisits.RowCount > 0)
                        dgvVisits.Rows[0].Selected = true;

                    FillClientInfo();

                    return true;
                }

            }
            return false;
        }

        private void toolStripBtnFindCursor_Click(object sender, EventArgs e)
        {
            scrollDgv(dgvClients);
        }

        private void toolStripBtnFindCursorV_Click(object sender, EventArgs e)
        {
            scrollDgv(dgvVisits);

        }


    }

}
