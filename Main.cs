using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Style
{
    public partial class Main : Form
    {
        int currDgvPosition;

        public Main()
        {
            InitializeComponent();
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// получить текущую выбранную строку гриды "ПРочиеДокументы"
        /// </summary>
        public DataGridViewRow GetRowCurrRowInDGV
        {
            get { return dataGridView1.SelectedRows[0]; }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (findOpenForm("NewClient"))
                    return;

                NewClient newClient = new NewClient(true, this); //если режим изменения, то передаем на форму значение true
                newClient.ShowDialog();
                newClient.Dispose();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (findOpenForm("NewClient"))
                return;

            NewClient newClient = new NewClient(false, this); //если режим добавления, то передаем на форму значение false
            newClient.ShowDialog();
            newClient.Dispose();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            ViewVisits();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Удалить клиента?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    currDgvPosition = ((BindingSource)dataGridView1.DataSource).Position;
                    UInt32 currIdClient = Convert.ToUInt32(dataGridView1[dataGridView1.Columns["id_client"].Index, dataGridView1.CurrentRow.Index].Value.ToString());
                    Program.dbStyle.DeleteClient(currIdClient);
                    GetData();
                    try
                    {
                        dataGridView1.Rows[((BindingSource)dataGridView1.DataSource).Position].Selected = true; 
                    }
                    catch (Exception)
                    {
                    }

                }
                
            }
        }

        private void выхожToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Program.dbStyle = new DB();
            Program.dbStyle.conn.StateChange += conn_StateChange;
            Program.dbStyle.ConnectDB();

            dataGridView1.DataSource = clientsBindingSource;
            GetData();
            setNameColumnsDgvClients();
        }

        void conn_StateChange(object sender, StateChangeEventArgs e)
        {
            toolStripStatusLabel4.Text = "Соединение с БД: ";

            switch (Program.dbStyle.conn.State)
            {
                case ConnectionState.Broken:
                    toolStripStatusLabel4.Text += "Разрушено";
                    break;
                case ConnectionState.Closed:
                    toolStripStatusLabel4.Text += "Закрыто";
                    break;
                case ConnectionState.Connecting:
                    toolStripStatusLabel4.Text += "Подключение";
                    break;
                case ConnectionState.Executing:
                    toolStripStatusLabel4.Text += "Выполнение";
                    break;
                case ConnectionState.Fetching:
                    toolStripStatusLabel4.Text += "Получение";
                    break;
                case ConnectionState.Open:
                    toolStripStatusLabel4.Text += "Открыто";
                    break;
                default:
                    break;
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Выйти из программы?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dr == System.Windows.Forms.DialogResult.No)
                e.Cancel = true;
            else
                Program.dbStyle.DisconnectDB();

        }

        public void setStateDbOnForm(string str)
        {
            toolStripStatusLabel4.Text = "Status:" + str;
        }

        private BindingSource clientsBindingSource = new BindingSource();

        public void GetData()
        {
            DataSet data = new DataSet();

            data = Program.dbStyle.GetClients();

            clientsBindingSource.DataSource = data;
            clientsBindingSource.DataMember = "Clients";
        }

        private void setNameColumnsDgvClients()
        {
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["id_client"].Visible = false;
                
                dataGridView1.Columns["LastName"].HeaderText = "Фамилия";
                dataGridView1.Columns["LastName"].Width = 150;
                
                dataGridView1.Columns["FirstName"].HeaderText = "Имя";
                dataGridView1.Columns["FirstName"].Width = 150;
                
                dataGridView1.Columns["MiddleName"].HeaderText = "Отчество";
                dataGridView1.Columns["MiddleName"].Width = 150;
                
                dataGridView1.Columns["TelMobile"].HeaderText = "Мобильный тел.";
                dataGridView1.Columns["TelHome"].HeaderText = "Домашний тел.";
                
                dataGridView1.Columns["Address"].HeaderText = "Адрес";
                dataGridView1.Columns["Address"].Width = 300;
                
                dataGridView1.Columns["Birthday"].HeaderText = "Дата рождения";
                dataGridView1.Columns["DiscountConst"].HeaderText = "Скидка пост.";

                dataGridView1.Columns["Notes"].HeaderText = "Примечание";
                dataGridView1.Columns["Notes"].Width = 300;

            }
        }

        private void ToolStripMenuItemEmploye_Click(object sender, EventArgs e)
        {

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
            if (dataGridView1.SelectedRows.Count > 0)
            {
                ViewVisits();
            }

        }

        void ViewVisits()
        {
            Visits visits = new Visits(this);
            visits.ShowDialog();
            visits.Dispose();
        }

    }

}
