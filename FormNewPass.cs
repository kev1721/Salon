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
    public partial class FormNewPass : Form
    {
        public FormNewPass()
        {
            InitializeComponent();
        }

        private void FormNewPass_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                if (txtBxPassNew.Text.Length > 0)
                {
                    if (txtBxPassOld.Text.Equals(Program.PassAdmin))
                    {
                        Program.savePass(txtBxPassNew.Text);
                        Program.PassAdmin = txtBxPassNew.Text;
                    }
                    else
                    {
                        MessageBox.Show("Неверно введен старый пароль", "Внимание");
                        txtBxPassOld.Text = "";
                        txtBxPassNew.Text = "";
                        e.Cancel = true;
                    }
                }
                else
                {
                    MessageBox.Show("Новый пароль не может быть пустым", "Внимание");
                    txtBxPassOld.Text = "";
                    txtBxPassNew.Text = "";
                    e.Cancel = true;
                }
            }
        }
    }
}
