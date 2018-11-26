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
    public partial class NewClient : Form
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(NewClient));

        bool isEditMode = false;
        Main mainForm;

        public NewClient()
        {
            InitializeComponent();
        }

        public NewClient(bool _isEditMode, Main _mainForm)
        {
            InitializeComponent();
            
            // TODO: Complete member initialization
            isEditMode = _isEditMode;
            if (_isEditMode)
            {
                this.Text = "Изменение данных клиента";
                FillTxtBx(_mainForm.GetCurrRowClientInDGV); //заполнение полей
                FillBufFields(); //заполнение буфера 

                btnSave.DialogResult = DialogResult.OK;
            }
            else
            {
                this.Text = "Добавление нового клиента";
                this.btnSave.Text = "Добавить";
                FillBufFields(); //заполнение буфера 

                btnSave.DialogResult = DialogResult.OK;
            }
            mainForm = _mainForm;
            
        }

        UInt32 id_client;
        BufFields bufData;

        struct BufFields
        {
            public string FirstName;
            public string LastName;
            public string MiddleName;
            public string Address;
            public string Notes;
            public string DiscountConst;
            public string TelMobile;
            public string TelHome;
            public DateTime Birthday;
        }
        /// <summary>
        /// заполнить поля формы используя данные из выделенной строки в таблице основной формы
        /// </summary>
        /// <param name="_row"></param>
        /// <returns>строка гриды</returns>
        public void FillTxtBx(DataGridViewRow _row)
        {
            
            id_client = Convert.ToUInt32(_row.Cells["id_client"].Value);
            txtBxFirstName.Text = _row.Cells["FirstName"].Value.ToString();
            txtBxLastName.Text = _row.Cells["LastName"].Value.ToString();
            txtBxMiddleName.Text = _row.Cells["MiddleName"].Value.ToString();
            txtBxAddress.Text = _row.Cells["Address"].Value.ToString();
            txtBxNotes.Text = _row.Cells["Notes"].Value.ToString();
            txtBxDiscountConst.Text = _row.Cells["DiscountConst"].Value.ToString();

            maskTxtBxTelMobile.Text = _row.Cells["TelMobile"].Value.ToString();
            maskTxtBxTelHome.Text = _row.Cells["TelHome"].Value.ToString();

            if (_row.Cells["Birthday"].Value != DBNull.Value)
            {
                dateTPBirthday.Checked = true;
                dateTPBirthday.Value = (DateTime)_row.Cells["Birthday"].Value;
            }

        }

        public void FillBufFields()
        {
            bufData = new BufFields();

            bufData.LastName = txtBxLastName.Text.Trim(' ');
            bufData.FirstName = txtBxFirstName.Text.Trim(' ');
            bufData.MiddleName = txtBxMiddleName.Text.Trim(' ');
            bufData.Address = txtBxAddress.Text.Trim(' ');
            bufData.Notes = txtBxNotes.Text.Trim(' ');
            bufData.DiscountConst = txtBxDiscountConst.Text.Trim(' ');

            bufData.TelMobile = maskTxtBxTelMobile.Text;
            bufData.TelHome = maskTxtBxTelHome.Text;

            bufData.Birthday = dateTPBirthday.Value;
        }

        public bool IsChangesData
        {
            get 
            {
                if (!bufData.LastName.Equals(txtBxLastName.Text.Trim(' ')) ||
                    !bufData.FirstName.Equals(txtBxFirstName.Text.Trim(' ')) ||
                    !bufData.MiddleName.Equals(txtBxMiddleName.Text.Trim(' ')) ||
                    !bufData.Address.Equals(txtBxAddress.Text.Trim(' ')) ||
                    !bufData.Notes.Equals(txtBxNotes.Text.Trim(' ')) ||
                    !bufData.DiscountConst.Equals(txtBxDiscountConst.Text.Trim(' ')) ||

                    !bufData.TelMobile.Equals(maskTxtBxTelMobile.Text.Trim(' ')) ||
                    !bufData.TelHome.Equals(maskTxtBxTelHome.Text.Trim(' ')) ||

                    !bufData.Birthday.Date.Equals(dateTPBirthday.Value.Date))
                    return true;
                else
                    return false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this.Close();
            
        }

        bool isTrueClientData() //проверка данных клиента
        {
            bool _ret = false;

            if (txtBxLastName.Text.Length == 0)
            {
                MessageBox.Show("Пустое поле 'Фамилия'!");
                return false;
            }

            if (txtBxFirstName.Text.Length == 0)
            {
                MessageBox.Show("Пустое поле 'Имя'!");
                return false;
            }

            return true;
        }



        private void NewClient_FormClosed(object sender, FormClosedEventArgs e)
        {
          //  mainForm.GetDataClients();
        }

        bool editClient()
        {
            DateTime? dt1 = null;
            if (dateTPBirthday.Checked)
                dt1 = dateTPBirthday.Value.Date;

            uint discount = 0;
            try
            {
                discount = Convert.ToUInt16(txtBxDiscountConst.Text.Trim(' '));
            }
            catch (Exception)
            {
            }

            log.Info("/////////////////////////////////////////////////////////////////////////////////////////");
            
            log.Info("Attempt update client...");

            log.Info("id_client = " + id_client + " LastName = " + txtBxLastName.Text.Trim(' ') + " FirstName = " + txtBxFirstName.Text.Trim(' ') +
                " MiddleName = " + txtBxMiddleName.Text.Trim(' ') + " TelMobile = " + maskTxtBxTelMobile.Text.Trim(' ') + " TelHome = " + maskTxtBxTelHome.Text.Trim(' ') +
                " Address = " + txtBxAddress.Text.Trim(' ') + " Birthday = " + dt1 + " discount = " + discount + " Notes = " + txtBxNotes.Text.Trim(' ')
                );

            return Program.dbStyle.UpdateClient(id_client,
                  txtBxLastName.Text.Trim(' '), txtBxFirstName.Text.Trim(' '), txtBxMiddleName.Text.Trim(' '),
                  maskTxtBxTelMobile.Text.Trim(' '), maskTxtBxTelHome.Text.Trim(' '),
                  txtBxAddress.Text.Trim(' '),
                  dt1,
                  discount,
                  txtBxNotes.Text.Trim(' ')
                  );
        }

        public int idNewClient = 0;

        void addClient()
        {
            DateTime? dt1 = null;
            if (dateTPBirthday.Checked)
                dt1 = dateTPBirthday.Value.Date;

            uint discount = 0;
            try
            {
                discount = Convert.ToUInt16(txtBxDiscountConst.Text.Trim(' '));
            }
            catch (Exception)
            {
            }

            log.Info("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");

            log.Info("Attempt insert client...");

            log.Info("id_client = " + id_client + " LastName = " + txtBxLastName.Text.Trim(' ') + " FirstName = " + txtBxFirstName.Text.Trim(' ')+
                " MiddleName = " + txtBxMiddleName.Text.Trim(' ') + " TelMobile = " + maskTxtBxTelMobile.Text.Trim(' ') + " TelHome = " + maskTxtBxTelHome.Text.Trim(' ') +
                " Address = " + txtBxAddress.Text.Trim(' ') + " Birthday = " + dt1 + " discount = " + discount + " Notes = " + txtBxNotes.Text.Trim(' ')
                );

            Program.dbStyle.InsertClient(txtBxLastName.Text.Trim(' '), txtBxFirstName.Text.Trim(' '), txtBxMiddleName.Text.Trim(' '),
                maskTxtBxTelMobile.Text.Trim(' '), maskTxtBxTelHome.Text.Trim(' '),
                txtBxAddress.Text.Trim(' '),
                dt1,
                discount,
                txtBxNotes.Text.Trim(' '),
                out idNewClient
                );
        }

        bool isValidDiscountConst()
        {
            try
            {
                if (txtBxDiscountConst.Text.Length == 0)
                    txtBxDiscountConst.Text = "0";
                if (Convert.ToInt16(txtBxDiscountConst.Text) < 0 || Convert.ToInt16(txtBxDiscountConst.Text) > 100)
                    return false;
                else
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        bool isValidLengthAddress()
        {
            if (txtBxAddress.Text.Length>250)
                return false;
            else
                return true;
        }

        bool isValidLengthNote()
        {
            if (txtBxNotes.Text.Length > 250)
                return false;
            else
                return true;
        }

        bool isValid()
        {
            if (!isValidDiscountConst())
            {
                DialogResult dr = MessageBox.Show("Скидка должна быть в диапазоне от 0 до 100 %", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (!isValidLengthAddress())
            {
                DialogResult dr = MessageBox.Show("Поле 'Адрес' не может быть больше 250 символов", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;                
            }
            if (!isValidLengthNote())
            {
                DialogResult dr = MessageBox.Show("Поле 'Примечание' не может быть больше 250 символов", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            return true;
        }

        private void NewClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                if (!isValid())
                {
                    e.Cancel = true;
                }
                else
                {
                    if (!isEditMode) //если режим добавления
                    {
                        addClient();
                        FillBufFields();
                    }
                    else
                    {
                        if (editClient())
                            FillBufFields();
                    }
                }
                //mainForm.GetDataClients();

            }
            else
            {
                if (IsChangesData)
                {
                    if (isEditMode) //если режим изменения и данные изменены, то перед выходом спрашиваем
                    {
                        DialogResult dr = MessageBox.Show("Данные клиента изменены. Закрыть окно без сохранения?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (dr == System.Windows.Forms.DialogResult.No)
                            e.Cancel = true;
                    }
                    else
                    {
                        DialogResult dr = MessageBox.Show("Клиент не добавлен в базу данных. Закрыть окно без добавления?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (dr == System.Windows.Forms.DialogResult.No)
                            e.Cancel = true;
                    }
                }
            }
        }
    }
}
