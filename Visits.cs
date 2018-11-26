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
    public partial class Visits : Form
    {
        public Visits()
        {
            InitializeComponent();
        }

        public Visits(Main _mainForm)
        {
            InitializeComponent();

            mainForm = _mainForm;
            id_client = (int)mainForm.GetCurrRowClientInDGV.Cells["id_client"].Value;
            this.Text = this.Text + " - " + mainForm.GetCurrRowClientInDGV.Cells["LastName"].Value.ToString() +
                                    " " + mainForm.GetCurrRowClientInDGV.Cells["FirstName"].Value.ToString() +
                                    " " + mainForm.GetCurrRowClientInDGV.Cells["MiddleName"].Value.ToString();
            dgvVisits.DataSource = visitsBindingSource;
            GetDataVisits();
            setNameColumnsDgvVisits();
        }

        private BindingSource visitsBindingSource = new BindingSource();
        Main mainForm;
        public int id_client;
        int currDgvPosition;

        public void GetDataVisits()
        {
            DataSet data = new DataSet();

            data = Program.dbStyle.GetVisitsClient(id_client);

            visitsBindingSource.DataSource = data;
            visitsBindingSource.DataMember = "Visits";
        }

        /// <summary>
        /// получить текущую выбранную строку гриды "ПРочиеДокументы"
        /// </summary>
        public DataGridViewRow GetCurrRowVisitInDGV
        {
            get {
                if (dgvVisits.SelectedRows.Count > 0)
                    return dgvVisits.SelectedRows[0];
                else
                    return null;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddVisit();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DeleteVisit();

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            EditVisit();
        }

        private void setNameColumnsDgvVisits()
        {
            if (dgvVisits.Columns.Count > 0)
            {
                dgvVisits.Columns["id_visit"].Visible = false;
                dgvVisits.Columns["id_client"].Visible = false;
                dgvVisits.Columns["id_st"].Visible = false;
                dgvVisits.Columns["id_e"].Visible = false;

                dgvVisits.Columns["DateVisit"].HeaderText = "Дата посещения";
                //dgvVisits.Columns["DateVisit"].Width = 150;

                dgvVisits.Columns["FioEmploy"].HeaderText = "Специалист";

                dgvVisits.Columns["NameStyle"].HeaderText = "Оказанная услуга";

                dgvVisits.Columns["CostStyle"].HeaderText = "Стоимость услуги";
                //dgvVisits.Columns["CostStyle"].Width = 150;

                dgvVisits.Columns["Consult"].HeaderText = "Консультация";
                //dgvVisits.Columns["Consult"].Width = 150;

                dgvVisits.Columns["CostConsult"].HeaderText = "Сумма за консульт.";
                //dgvVisits.Columns["CostConsult"].Width = 50;

                dgvVisits.Columns["DiscountSeason"].HeaderText = "Скидка сезонная";
                //dgvVisits.Columns["DiscountSeason"].Width = 50;
                
                
                dgvVisits.Columns["CostVisit"].HeaderText = "Сумма за посещение";
                //dgvVisits.Columns["CostVisit"].Width = 50;

                dgvVisits.Columns["Notes"].HeaderText = "Примечание";
                //dgvVisits.Columns["Note"].Width = 300;

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

        private void dgvVisits_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgvVisits.SelectedRows.Count > 0)
            //{
            //    frmNewVisit newVisite = new frmNewVisit(true, this);
            //    newVisite.ShowDialog();
            //    newVisite.Dispose();
            //}
        }

        void AddVisit()
        {
            //if (findOpenForm("NewVisit"))
            //    return;

            //frmNewVisit newVisit = new frmNewVisit(false, this);
            //newVisit.ShowDialog();
            //newVisit.Dispose();
        }

        void EditVisit()
        {
            //if (dgvVisits.SelectedRows.Count > 0)
            //{
            //    frmNewVisit newVisite = new frmNewVisit(true, this);
            //    newVisite.ShowDialog();
            //    newVisite.Dispose();
            //}
        }

        void DeleteVisit()
        {
            if (dgvVisits.SelectedRows.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Удалить посещение?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    currDgvPosition = ((BindingSource)dgvVisits.DataSource).Position;
                    UInt32 currIdVisit = Convert.ToUInt32(dgvVisits[dgvVisits.Columns["id_visit"].Index, dgvVisits.CurrentRow.Index].Value.ToString());
                    Program.dbStyle.DeleteVisit(currIdVisit);
                    GetDataVisits();
                    try
                    {
                        dgvVisits.Rows[((BindingSource)dgvVisits.DataSource).Position].Selected = true;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void ToolStripMenuItemAdd_Click(object sender, EventArgs e)
        {
            AddVisit();
        }

        private void ToolStripMenuItemEdit_Click(object sender, EventArgs e)
        {
            EditVisit();
        }

        private void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            DeleteVisit();
        }

        private void dgvVisits_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }
}
