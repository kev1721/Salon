using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text;
using log4net;
using log4net.Config;

namespace Style
{
    static class Program
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(Program));
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            log4net.Config.DOMConfigurator.Configure();
            log.Info("=========================================================================================");
            log.Info("Start programm");
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Program.dbStyle = new DB();
            Program.dbStyle.ConnectDB();

            using (FormLogin frm = new FormLogin())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new Main());
                }
                else
                    Application.Exit();
            }
        }


        static public bool isAdminMode = false;
        static public DB dbStyle = null;
        static public string PassAdmin = "";
        static public string currUserWorking = "";

        static public void savePass(string _str)
        {
            Random rnd = new Random();
            int i1 = rnd.Next(255);
            int i2 = rnd.Next(255);
            int i3 = rnd.Next(255);

            string[] strsEncode = new string[1] { Char.ConvertFromUtf32(i1) + Char.ConvertFromUtf32(i2) + Char.ConvertFromUtf32(i3) };
            for (int i = 0; i < _str.Length; i++)
                strsEncode[0] = strsEncode[0] + Char.ConvertFromUtf32((int)_str[i] + 1);

            File.WriteAllLines(Application.StartupPath + "\\setup.ini", strsEncode, Encoding.ASCII);
        }

        static public void readPass(out string[] _strs)
        {
            _strs = File.ReadAllLines(Application.StartupPath + "\\setup.ini", Encoding.ASCII);
            string strsDecode = "";

            for (int i = 3; i < _strs[0].Length; i++)
                strsDecode = strsDecode + Char.ConvertFromUtf32((int)_strs[0][i] - 1);
            _strs[0] = strsDecode;
        }
    }
}
