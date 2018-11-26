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
    public partial class FormSetupReport : Form
    {
        public FormSetupReport()
        {
            InitializeComponent();

            List<CmbBxType> employes = Program.dbStyle.GetEmployes(2); //0 - только не уволенные, 1 - только уволенные, 2 - все
            addItemsToCmbBx(employes);

        }

        void addItemsToCmbBx(List<CmbBxType> _usrsPrg)
        {
            foreach (CmbBxType item in _usrsPrg)
                cmbBxUsers.Items.Add(item);
        }

        private void FormSetupReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                if (cmbBxUsers.SelectedIndex != -1)
                {
                    Rep rpt = new Rep();
                    UInt32 _id = Convert.ToUInt32(((CmbBxType)cmbBxUsers.SelectedItem).id);
                    string _name = ((CmbBxType)cmbBxUsers.SelectedItem).name;
                    rpt.Generate(dateTimePicker1.Value, _id, _name);
                }
                else
                {
                    MessageBox.Show("Не выбран специалист","Внимание");
                    e.Cancel = true;
                }
            }
        }
    }
}
