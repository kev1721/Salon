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
    public partial class frmNewVisit : Form
    {
        public frmNewVisit()
        {
            InitializeComponent();
        }

        public frmNewVisit(bool _isEditMode, Visits _visitForm)
        {
            InitializeComponent();

            isEditMode = _isEditMode;
            if (_isEditMode)
            {
                this.Text = "Изменение данных посещения";
                FillItmsCmbBx();
                FillTxtBx(_visitForm.GetRowCurrRowInDGV); //заполнение полей
                FillBufFields(); //заполнение буфера 
            }
            else
            {
                this.Text = "Добавление нового посещения";
                this.btnSave.Text = "Добавить";
                FillBufFields(); //заполнение буфера 
                FillItmsCmbBx();
                btnSave.DialogResult = DialogResult.OK;

            }

            id_client = Convert.ToUInt32(_visitForm.id_client);

            visitForm = _visitForm;
        }

        private void FillItmsCmbBx()
        {
            List <CmbBxType> listEmployes = Program.dbStyle.GetEmployes();
            foreach (CmbBxType item in listEmployes)
            {
                cmbBxEmploy.Items.Add(item);
            }

            List<CmbBxType> listStyles = Program.dbStyle.GetStyles();
            foreach (CmbBxType item in listStyles)
            {
                cmbBxStyle.Items.Add(item);
            }
        }
        
        bool isEditMode = false;
        Visits visitForm;
        UInt32 id_client;
        UInt32 id_visit;

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        BufFields bufData;
        struct BufFields
        {
            public DateTime DateVisit;
            public int Employ;
            public int Style;
            public string CostStyle;
            public string Consult;
            public string CostConsult;
            public string Note;
            public string DiscountSeason;
            public string CostVisit;

        }

        public void FillBufFields()
        {
            bufData = new BufFields();

            bufData.DateVisit = dTPDateVisit.Value;
            bufData.Employ = cmbBxEmploy.SelectedIndex;
            bufData.Style =  cmbBxStyle.SelectedIndex;
            bufData.CostStyle = txtBxCostStyle.Text.Trim(' ');
            bufData.Consult = txtBxConsult.Text.Trim(' ');
            bufData.CostConsult = txtBxCostConsult.Text.Trim(' ');
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

            cmbBxEmploy.SelectedIndex = (int)_row.Cells["id_e"].Value - 1;
            cmbBxStyle.SelectedIndex = (int)_row.Cells["id_st"].Value - 1;
            txtBxCostStyle.Text = _row.Cells["CostStyle"].Value.ToString();

            txtBxConsult.Text = _row.Cells["Consult"].Value.ToString();
            txtBxCostConsult.Text = _row.Cells["CostConsult"].Value.ToString();
            txtBxNote.Text = _row.Cells["Note"].Value.ToString();
            txtBxDiscountSeason.Text = _row.Cells["DiscountSeason"].Value.ToString();
            txtBxCostVisit.Text = _row.Cells["CostVisit"].Value.ToString();

            id_visit = Convert.ToUInt32(_row.Cells["id_visit"].Value);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbBxEmploy.SelectedIndex == -1 || cmbBxStyle.SelectedIndex == -1)
            {
                DialogResult dr = MessageBox.Show("Не заполнены поля: оказанная услуга, специалист", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                 
            }
            else
            {

                if (!isEditMode) //если режим добавления
                {

                    addVisit();
                    FillBufFields();
                }
                else
                {
                    if (editVisit())
                        FillBufFields();
                }
            }
            visitForm.GetData();
        }


        bool editVisit()
        {
            int discountSeason = 0;
            int costStyle = 0;
            int costConsult = 0;
            int costVisit = 0;

            int.TryParse(txtBxDiscountSeason.Text.Trim(' '), out discountSeason);
            int.TryParse(txtBxCostStyle.Text.Trim(' '), out costStyle);
            int.TryParse(txtBxCostConsult.Text.Trim(' '), out costConsult);
            int.TryParse(txtBxCostVisit.Text.Trim(' '), out costVisit);

            return Program.dbStyle.UpdateVisit(id_visit, id_client, dTPDateVisit.Value.Date, cmbBxEmploy.SelectedIndex + 1, cmbBxStyle.SelectedIndex + 1,
            costStyle, txtBxConsult.Text.Trim(' '), costConsult, txtBxNote.Text.Trim(' '),
            discountSeason, costVisit);

        }


        void addVisit()
        {
            int discountSeason = 0;
            int costStyle = 0;
            int costConsult = 0;
            int costVisit = 0;

            int.TryParse(txtBxDiscountSeason.Text.Trim(' '), out discountSeason);
            int.TryParse(txtBxCostStyle.Text.Trim(' '), out costStyle);
            int.TryParse(txtBxCostConsult.Text.Trim(' '), out costConsult);
            int.TryParse(txtBxCostVisit.Text.Trim(' '), out costVisit);

            Program.dbStyle.InsertVisit(id_client, dTPDateVisit.Value.Date, cmbBxEmploy.SelectedIndex + 1, cmbBxStyle.SelectedIndex + 1,
            costStyle, txtBxConsult.Text.Trim(' '), costConsult, txtBxNote.Text.Trim(' '),
            discountSeason, costVisit);
        }

        private void frmNewVisit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsChangesData)
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
        }

        public bool IsChangesData
        {
            get
            {
                if (!bufData.Consult.Equals(txtBxConsult.Text.Trim(' ')) ||
                    !bufData.CostConsult.Equals(txtBxCostConsult.Text.Trim(' ')) ||
                    !bufData.CostStyle.Equals(txtBxCostStyle.Text.Trim(' ')) ||
                    !bufData.CostVisit.Equals(txtBxCostVisit.Text.Trim(' ')) ||
                    !bufData.DateVisit.Date.Equals(dTPDateVisit.Value.Date) ||
                    !bufData.DiscountSeason.Equals(txtBxDiscountSeason.Text.Trim(' ')) ||

                    !bufData.Employ.Equals(cmbBxEmploy.SelectedIndex) ||
                    !bufData.Style.Equals(cmbBxStyle.SelectedIndex) ||

                    !bufData.Note.Equals(txtBxNote.Text.Trim(' '))
                    ) 

                    return true;
                else
                    return false;
            }
        }

        private void frmNewVisit_FormClosed(object sender, FormClosedEventArgs e)
        {
            visitForm.GetData();
        }
    }
}
