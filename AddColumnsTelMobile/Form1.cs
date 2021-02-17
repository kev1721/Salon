using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AddColumnsTelMobile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void SetLabels(string lbl1, string lbl2, string lbl3)
        {
            this.label1.Text = lbl1;
            this.label2.Text = lbl2;
            this.label3.Text = lbl3;
        }
    }
}
