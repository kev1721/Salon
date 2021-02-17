using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;
using log4net;
using log4net.Config;

namespace AddColumnsTelMobile
{
    class Program
    {
        static void Main(string[] args)
        {
            Form1 frm = new Form1();

            DBUtil dbut = new DBUtil();
            var res1 = dbut.ConnectDB();
            var res2 = dbut.AddColumnsTelMobile();
            var res3 = dbut.DisconnectDB();

            frm.SetLabels(res1.ToString(), res2.ToString(), res3.ToString());
 
            Application.Run(frm);
        }
    }

    public class DBUtil
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(DBUtil));

        public DBUtil()
        {
            string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Application.StartupPath + "\\Database02.mdb";
            conn = new OleDbConnection(connStr);
        }

        public OleDbConnection conn;

        public bool ConnectDB()
        {
            try
            {
                conn.Open();
                log.Info("OK - connect to database");
                return true;
            }
            catch
            {
                log.Error("Error - connect to database");
            }
            return false;
        }

        public bool DisconnectDB()
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                log.Info("OK - disconnect from database");
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error - disconnect from database. " + ex.ToString());
            }

            return false;
        }

        /// <summary>
        /// Add columns TelMobile2, TelMobile3
        /// </summary>
        /// <param name="idVisit"></param>
        /// <param name="_matVisit"></param>
        /// <returns></returns>
        public bool AddColumnsTelMobile()
        {
            bool flag = true;

            if (conn.State != System.Data.ConnectionState.Open)
                try
                {
                    conn.Open();
                }
                catch
                {
                    return false;
                }

                string addColumnsSQL = "ALTER TABLE [Clients] ADD COLUMN TelMobile2 TEXT(255), TelMobile3 TEXT(255) ";

                using (OleDbCommand command = new OleDbCommand(addColumnsSQL))
                {
                    command.Connection = conn;
                    try
                    {
                        command.ExecuteNonQuery();
                        log.Info("SQL. OK - Adding columns TelMobile2 and TelMobile into Clients table");
                    }
                    catch (Exception ex)
                    {
                        log.Error("SQL. Error - Adding columns TelMobile2 and TelMobile into Clients table");

                        flag = false;
                    }
                }
            
            return flag;
        }
        
    }
}
