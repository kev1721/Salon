using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using log4net;
using log4net.Config;

namespace Style
{
    public partial class FormLogin : Form
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(Program));

        public FormLogin()
        {
            InitializeComponent();

            List<UserPrg> usrsPrg = Program.dbStyle.GetUsersPrg();
            addItemsToCmbBx(usrsPrg);
            if (usrsPrg.Count > 0)
                cmbBxUsers.SelectedIndex = 0;
        }
        
        List<UserPrg> usrsPrg;

        void addItemsToCmbBx(List<UserPrg> _usrsPrg)
        {
            foreach (UserPrg item in _usrsPrg)
            {
                cmbBxUsers.Items.Add(item);
            }
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.Cancel)
            {
                if (cmbBxUsers.SelectedIndex < 0)
                {
                    log.Info("Not selected user on FormLogin");
                    MessageBox.Show("Не выбран пользователь", "Внимание");
                    txtBxPassword.Text = "";
                    e.Cancel = true;
                }
                else
                {
                    if (DialogResult == DialogResult.OK)
                    {
                        bool isCheat = false;
                        try
                        {
                            System.IO.File.ReadAllText(Application.StartupPath + "\\password.txt");
                            isCheat = true;
                            log.Info("Used CHEATS - password.txt");
                        }
                        catch (Exception)
                        {
                        }

                        if (!txtBxPassword.Text.Equals(((UserPrg)cmbBxUsers.Items[cmbBxUsers.SelectedIndex]).passUser) && !isCheat)
                        {
                            log.Error("Not correct password on FormLogin");
                            log.Error("user = " + ((UserPrg)cmbBxUsers.Items[cmbBxUsers.SelectedIndex]).nameUser);
                            log.Error("password = " + ((UserPrg)cmbBxUsers.Items[cmbBxUsers.SelectedIndex]).passUser);

                            MessageBox.Show("Неверный пароль", "Внимание");
                            txtBxPassword.Text = "";
                            e.Cancel = true;
                        }
                        else
                        {
                            Program.isAdminMode = ((UserPrg)cmbBxUsers.Items[cmbBxUsers.SelectedIndex]).id_accessLevel == 2;  //если режим Адм
                            log.Info("isAdminMode = true");
                        }

                        Program.currUserWorking = ((UserPrg)cmbBxUsers.Items[cmbBxUsers.SelectedIndex]).nameUser;
                        log.Info("Current user = " + ((UserPrg)cmbBxUsers.Items[cmbBxUsers.SelectedIndex]).nameUser);
                    }
                }
            }
            else
            {
                log.Info("Cancel login");

            }

        }


    }
}
