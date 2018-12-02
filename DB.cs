using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Data;
using System.ComponentModel;
using log4net;
using log4net.Config;

namespace Style
{
    public class DB
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(DB));

        public DB()
        {
            //MS Access
            //string connStr = @"Provider=MS Access Database; Data Source=" + Application.StartupPath + "\\Database02.mdb";

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
            catch(Exception ex)
            {
                log.Error("Error - disconnect from database. "+ex.ToString());
            }
            
            return false;
        }

        public bool DeleteClient(UInt32 id_client)
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch { return false; }
            }

            string deleteSQL = "Delete From [Clients] " +
                                  " WHERE id_client = @id_client ";
            using (OleDbCommand command = new OleDbCommand(deleteSQL))
            {
                command.Connection = conn;
                command.Parameters.Add("@id_client", OleDbType.Integer).Value = id_client;
                try
                {
                    command.ExecuteNonQuery();
                    log.Info("SQL. OK - delete client id = " + id_client);
                    return true;
                }
                catch (Exception ex)
                {
                    log.Error("SQL. Error - delete client id = " + id_client +". " +ex.ToString());
                    MessageBox.Show("Невозможно удалить клиента из базы данных \r\n");
                    return false;
                }
            }

        }

        public bool UpdateClient(UInt32 idClient, string LastName, string FirstName, string MiddleName,
            string telMobile, string telHome,
            string address, DateTime? birthDay, uint discountConst, string notes)
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch { return false; }
            }

            string updateSQL = "UPDATE [Clients] " +
                    "SET LastName = @LastName, " +
                       " FirstName = @FirstName, " +
                       " MiddleName = @MiddleName, " +
                       " telMobile = @telMobile, " +
                       " telHome = @telHome, " +
                       " address = @address, " +
                       " birthDay = @birthDay, " +
                       " discountConst = @discountConst, " +
                       " notes = @notes " +
                       " WHERE id_client = @id_client ";

            using (OleDbCommand command = new OleDbCommand(updateSQL))
            {
                command.Connection = conn;
                command.Parameters.Add("@LastName", OleDbType.VarChar).Value = LastName;
                command.Parameters.Add("@FirstName", OleDbType.VarChar).Value = FirstName;
                command.Parameters.Add("@MiddleName", OleDbType.VarChar).Value = MiddleName;
                command.Parameters.Add("@telMobile", OleDbType.VarChar).Value = telMobile;
                command.Parameters.Add("@telHome", OleDbType.VarChar).Value = telHome;
                command.Parameters.Add("@address", OleDbType.VarChar).Value = address;
                if (birthDay != null)
                    command.Parameters.Add("@birthDay", OleDbType.Date).Value = birthDay.Value.Date;
                else
                    command.Parameters.Add("@birthDay", OleDbType.Integer).Value = DBNull.Value;
                command.Parameters.Add("@discountConst", OleDbType.Integer).Value = discountConst;
                command.Parameters.Add("@notes", OleDbType.VarChar).Value = notes;
                command.Parameters.Add("@id_client", OleDbType.Integer).Value = idClient;

                try
                {
                    command.ExecuteNonQuery();
                    log.Info("SQL. OK - update client id = " + idClient);
                    return true;
                }
                catch (Exception ex)
                {
                    log.Error("SQL. Error - update client id = " + idClient + ". "+ ex.ToString());
                    MessageBox.Show("Невозможно изменить данные клиента в базе данных\r\n" + ex.ToString());
                    return false;
                }
            }

        }
        
        public bool InsertClient(string LastName, string FirstName, string MiddleName, 
            string telMobile, string telHome, 
            string address, DateTime? birthDay, uint discountConst, string notes, out int idNewclient)
        {
            Int32 id = idNewclient = -1;
            if (conn.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch { return false; }
            }


            string selectSQL = "SELECT top 1 * from [Clients] order by id_client desc";

            using (OleDbCommand commd = new OleDbCommand(selectSQL, conn))
            {
                using (OleDbDataReader dr = commd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        id = (Int32)dr["id_client"];
                    }
                }
            }

            id++;
            string insertSQL =  "INSERT INTO [Clients] " +
                                " (id_client, LastName, FirstName, MiddleName, telMobile, telHome, address, birthDay, discountConst, notes) " +
                                " VALUES (@id_client, @LastName, @FirstName, @MiddleName, @telMobile, @telHome, @address, @birthDay, @discountConst, @notes) ";

            using (OleDbCommand command = new OleDbCommand(insertSQL))
            {

                command.Connection = conn;
                command.Parameters.Add("@id_client", OleDbType.Integer).Value = id;
                command.Parameters.Add("@LastName", OleDbType.VarChar).Value = LastName;
                command.Parameters.Add("@FirstName", OleDbType.VarChar).Value = FirstName;
                command.Parameters.Add("@MiddleName", OleDbType.VarChar).Value = MiddleName;
                command.Parameters.Add("@telMobile", OleDbType.VarChar).Value = telMobile;
                command.Parameters.Add("@telHome", OleDbType.VarChar).Value = telHome;
                command.Parameters.Add("@address", OleDbType.VarChar).Value = address;
                if (birthDay != null)
                    command.Parameters.Add("@birthDay", OleDbType.Date).Value = birthDay.Value.Date;
                else
                    command.Parameters.Add("@birthDay",OleDbType.Integer).Value = DBNull.Value;

                command.Parameters.Add("@discountConst", OleDbType.Integer).Value = discountConst;
                command.Parameters.Add("@notes", OleDbType.VarChar).Value = notes;

                try
                {
                    command.ExecuteNonQuery();
                    idNewclient = id;
                    log.Info("SQL. OK - insert client id = " + id);
                    return true;
                }
                catch (Exception ex)
                {
                    log.Error("SQL. Error - insert client id = " + id +". "+ ex.ToString());
                    MessageBox.Show("Невозможно добавить клиента в базу данных\r\n" + ex.ToString());
                    return false;
                }
            }

        }

        public DataSet GetClients(DateTime? dt)
        {
            string cmdTxt = "Select *, (SELECT top 1 visits.accept " +
                                " from visits " +
                                " where clients.id_client = visits.id_client " +
                                " group by accept order by accept desc) as Accept, " +
                                " (SELECT  count(*) from visits " +
                                " where clients.id_client = visits.id_client) as CountVisits " +
                                " from [Clients] ";
            string cmdPeriod = " where BirthDay = @dt " ;
            DataSet ds = new DataSet();
            OleDbCommand cmd;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                if (dt != null)
                {
                    cmd = new OleDbCommand(cmdTxt + cmdPeriod, conn);
                    cmd.Parameters.Add("@dt", OleDbType.Date).Value = dt;
                }
                else
                    cmd = new OleDbCommand(cmdTxt, conn);

                (new OleDbDataAdapter(cmd)).Fill(ds, "Clients");
                //log.Info("SQL. OK - get clients");
                
                //using (OleDbCommand myCmd = conn.CreateCommand())
                //{
                //    ds = new DataSet();
                //    myCmd.CommandText = cmdTxt;
                //    OleDbDataAdapter da = new OleDbDataAdapter();
                //    da.SelectCommand = myCmd;
                //    da.Fill(ds, "Clients");
                //}
            }
            catch (Exception ex)
            {
                log.Error("SQL. Error - get clients. " + ex.ToString());
                MessageBox.Show("Невозможно получить клиентов из базы данных");
            }
            return ds;
        }


        internal DataTable GetClientsBirthday(DateTime dateBirthday)
        {
            DataTable dt = new DataTable();

            if (conn.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch { return dt; }
            }

            string selectSQL = "Select * from [Clients] where Birthday = @dateBirthday";
            try
            {
                using (OleDbCommand commd = new OleDbCommand(selectSQL, conn))
                {
                    commd.Parameters.Add("@dateBirthday", OleDbType.Date).Value = dateBirthday.Date;

                    OleDbDataAdapter da = new OleDbDataAdapter(commd);
                    da.Fill(dt);
                }
               // log.Info("SQL. OK - get clients birthday dateBirthday = " + dateBirthday.Date.ToShortDateString());

            }
            catch (Exception ex) 
            {
                log.Error("SQL. Error - get clients dateBirthday = " + dateBirthday.Date.ToShortDateString() + " . " + ex.ToString());
            }

            return dt;
        }

        public bool DeleteVisit(UInt32 id)
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch { return false; }
            }

            string deleteSQL = "Delete From [Visits] " +
                                  " WHERE id = @id ";
            using (OleDbCommand command = new OleDbCommand(deleteSQL))
            {
                command.Connection = conn;
                command.Parameters.Add("@id", OleDbType.Integer).Value = id;
                try
                {
                    command.ExecuteNonQuery();
                    log.Info("SQL. OK - delete visit id = " + id);
                    return true;
                }
                catch (Exception ex)
                {
                    log.Error("SQL. Error - delete visit id = " + id + ". " + ex.ToString());
                    MessageBox.Show("Невозможно удалить посещение из базы данных\r\n" + ex.ToString());
                    return false;
                }
            }
        }

        public bool DeleteMaterialsVisit(Int32 id_visit)
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch { return false; }
            }

            string deleteSQL = "Delete From [MaterialsVisit] " +
                                  " WHERE id_visit = @id_visit ";
            using (OleDbCommand command = new OleDbCommand(deleteSQL))
            {
                command.Connection = conn;
                command.Parameters.Add("@id_visit", OleDbType.Integer).Value = id_visit;
                try
                {
                    command.ExecuteNonQuery();
                    log.Info("SQL. OK - delete all materials from visit id_visit = " + id_visit);
                    return true;
                }
                catch (Exception ex)
                {
                    log.Error("SQL. Error - delete all materials from visit id_visit = " + id_visit + ex.ToString());
                    return false;
                }
            }
        }

        public bool DeleteStylesVisit(Int32 id_visit)
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch { return false; }
            }

            string deleteSQL = "Delete From [StylesVisit] " +
                                  " WHERE id_visit = @id_visit ";
            using (OleDbCommand command = new OleDbCommand(deleteSQL))
            {
                command.Connection = conn;
                command.Parameters.Add("@id_visit", OleDbType.Integer).Value = id_visit;
                try
                {
                    command.ExecuteNonQuery();
                    log.Info("SQL. OK - delete all styles from visit id_visit = " + id_visit);
                    return true;
                }
                catch (Exception ex)
                {
                    log.Error("SQL. Error - delete all styles from visit id_visit = " + id_visit + ex.ToString());
                    return false;
                }
            }
        }

        public bool DeleteConsultsVisit(Int32 id_visit)
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch { return false; }
            }

            string deleteSQL = "Delete From [ConsultsVisit] " +
                                  " WHERE id_visit = @id_visit ";
            using (OleDbCommand command = new OleDbCommand(deleteSQL))
            {
                command.Connection = conn;
                command.Parameters.Add("@id_visit", OleDbType.Integer).Value = id_visit;
                try
                {
                    command.ExecuteNonQuery();
                    log.Info("SQL. OK - delete all consults from visit id_visit = " + id_visit);
                    return true;
                }
                catch (Exception ex)
                {
                    log.Error("SQL. Error - delete all consults from visit id_visit = " + id_visit + ex.ToString());
                    return false;
                }
            }
        }

        public bool UpdateVisit(Int32 id, UInt32 id_client, string whoEdit, DateTime dateVisit, 
            float costStyle, string consult, float costConsult, string note,
            int discountSeason, float costVisit, bool accept)
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch { return false; }
            }

            string updateSQL = "UPDATE [Visits] " +
                    "SET id_client = @id_client, " +
                       //" id_st = @id_st, " +
                       " whoEdit = @whoEdit, " +
                       " DateVisit = @DateVisit, " +
                       " CostStyle = @CostStyle, " +
                       " Consult = @Consult, " +
                       " CostConsult = @CostConsult, " +
                       " DiscountSeason = @DiscountSeason, " +
                       " CostVisit = @CostVisit, " +
                       " Notes = @Notes, " +
                       " Accept = @Accept " +
                       " WHERE id = @id ";

            using (OleDbCommand command = new OleDbCommand(updateSQL))
            {
                command.Connection = conn;
                command.Parameters.Add("@id_client", OleDbType.Integer).Value = id_client;
                //command.Parameters.Add("@id_st", OleDbType.Integer).Value = id_style;
                command.Parameters.Add("@id_e", OleDbType.VarChar).Value = whoEdit;
                command.Parameters.Add("@DateVisit", OleDbType.Date).Value = dateVisit.Date;
                command.Parameters.Add("@CostStyle", OleDbType.Single).Value = costStyle;
                command.Parameters.Add("@Consult", OleDbType.VarChar).Value = consult;
                command.Parameters.Add("@CostConsult", OleDbType.Single).Value = costConsult;
                command.Parameters.Add("@DiscountSeason", OleDbType.Integer).Value = discountSeason;
                command.Parameters.Add("@CostVisit", OleDbType.Single).Value = costVisit;
                command.Parameters.Add("@Notes", OleDbType.VarChar).Value = note;
                command.Parameters.Add("@Accept", OleDbType.Boolean).Value = accept;
                command.Parameters.Add("@id", OleDbType.Integer).Value = id;

                try
                {
                    command.ExecuteNonQuery();
                    log.Info("SQL. OK - update visit id = " + id);
                    return true;
                }
                catch (Exception ex)
                {
                    log.Error("SQL. Error - update visit id = " + id +". "+ ex.ToString());
                    MessageBox.Show("Невозможно изменить данные посещения в базе данных\r\n" + ex.ToString());
                    return false;
                }
            }

        }

        public Int32 InsertVisit(UInt32 id_client, DateTime dateVisit, string whoAdd,
            float costStyle, string consult, float costConsult, string note,
            uint discountSeason, float costVisit, bool accept)
        {
            Int32 id = -1;

            if (conn.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch { return id; }
            }
            

            string selectSQL = "SELECT top 1 * from [Visits] order by id desc";

            using (OleDbCommand commd = new OleDbCommand(selectSQL, conn))
            {
                using (OleDbDataReader dr = commd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        id = (Int32)dr["id"];
                    }
                }
            }

            id++;
            string insertSQL = "INSERT INTO [Visits] " +
                                " ([id], [id_client], [whoAdd], [DateVisit], [CostStyle], [Consult], [CostConsult], [DiscountSeason], [CostVisit], [Notes], [Accept]) " +
                                " VALUES (@id, @id_client, @whoAdd, @DateVisit, @CostStyle, @Consult, @CostConsult, @DiscountSeason, @CostVisit, @Notes, @Accept) ";

            using (OleDbCommand command = new OleDbCommand(insertSQL))
            {
                command.Connection = conn;
                command.Parameters.Add("@id", OleDbType.Integer).Value = id;
                command.Parameters.Add("@id_client", OleDbType.Integer).Value = id_client;
                command.Parameters.Add("@whoAdd", OleDbType.VarChar).Value = whoAdd;
                command.Parameters.Add("@DateVisit", OleDbType.Date).Value = dateVisit.Date;
                command.Parameters.Add("@CostStyle", OleDbType.Single).Value = costStyle;
                command.Parameters.Add("@Consult", OleDbType.VarChar).Value = consult;
                command.Parameters.Add("@CostConsult", OleDbType.Single).Value = costConsult;
                command.Parameters.Add("@DiscountSeason", OleDbType.Integer).Value = discountSeason;
                command.Parameters.Add("@CostVisit", OleDbType.Single).Value = costVisit;
                command.Parameters.Add("@Notes", OleDbType.VarChar).Value = note;
                command.Parameters.Add("@Accept", OleDbType.Boolean).Value = accept;

                try
                {
                    command.ExecuteNonQuery();
                    log.Info("SQL. OK - insert visit id = " + id);
                    return  id;
                }
                catch (Exception ex)
                {
                    log.Error("SQL. Error - insert visit id = " + id + ". " +ex.ToString());
                    MessageBox.Show("Невозможно добавить посещение в базу данных\r\n" + ex.ToString());
                    return -1;
                }
            }

        }

        public DataSet GetVisitsClient(int id_client)
        {
            string cmdTxt = "SELECT Visits.id, Visits.id_client, Visits.whoAdd, Visits.whoEdit, " +
                            "Visits.DateVisit, "+
                            "Visits.CostStyle, " +
                            "Visits.Consult, Visits.CostConsult, " +
                            "Visits.DiscountSeason, Visits.CostVisit, Visits.Notes, Visits.Accept " +
                            
                            "FROM [Visits]"+
                            "WHERE Visits.id_client = @id_client";
            DataSet ds = null;
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                using (OleDbCommand command = conn.CreateCommand())
                {

                    ds = new DataSet();
                    command.CommandText = cmdTxt;
                    command.Parameters.Add("@id_client", OleDbType.Integer).Value = id_client;

                    OleDbDataAdapter da = new OleDbDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(ds, "Visits");
                }

               // log.Info("SQL. OK - get visits client id_client = " + id_client);
            }
            catch (Exception ex)
            {
                log.Error("SQL. Error - get visits client id_client = " + id_client + ". " + ex.ToString());
                MessageBox.Show("Невозможно получить посещения из базы данных");
            }
            return ds;
        }

        public DataSet GetVisitsAccept(bool isAccept)
        {
            string cmdTxt = "SELECT Visits.id, Visits.id_client, Visits.whoAdd, Visits.whoEdit, " +
                            "Visits.DateVisit, " +
                            "Visits.CostStyle, " +
                            "Visits.Consult, Visits.CostConsult, " +
                            "Visits.DiscountSeason, Visits.CostVisit, Visits.Notes, Visits.Accept " +

                            "FROM [Visits]" +
                            "WHERE Visits.Accept = @accept";
            DataSet ds = null;
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                using (OleDbCommand command = conn.CreateCommand())
                {
                    ds = new DataSet();
                    command.CommandText = cmdTxt;
                    command.Parameters.Add("@id_client", OleDbType.Boolean).Value = isAccept;

                    OleDbDataAdapter da = new OleDbDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(ds, "Visits");
                }

                // log.Info("SQL. OK - get visits client id_client = " + id_client);
            }
            catch (Exception ex)
            {
                log.Error("SQL. Error - get visits Accept = " + isAccept.ToString() + ". " + ex.ToString());
                MessageBox.Show("Невозможно получить посещения из базы данных");
            }
            return ds;
        }

        /// <summary>
        /// Выбрать специалистов
        /// </summary>
        /// <param name="_flag">0-только не уволенные, 1-только уволенные, 2-все</param>
        /// <returns></returns>
        public List<CmbBxType> GetEmployes(int _flag)
        {
            List<CmbBxType> str = new List<CmbBxType>();

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    try
                    {
                        conn.Open();
                    }
                    catch { return str; }

                string selectSQL = "SELECT Distinct * From [Employ] ";

                if (_flag == 0 || _flag == 1)
                    selectSQL = " where isLeave = @isLeave";

                using (OleDbCommand commd = new OleDbCommand(selectSQL, conn))
                {
                    if (_flag == 0 )
                        commd.Parameters.Add("@isLeave", OleDbType.Boolean).Value = false;
                    if (_flag == 1)
                        commd.Parameters.Add("@isLeave", OleDbType.Boolean).Value = true;


                    using (OleDbDataReader dr = commd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int id = (Int32)dr["id"];
                            string fio = dr["FIO"] == DBNull.Value ? "" : (string)dr["FIO"];

                            str.Add(new CmbBxType(id, fio));
                        }
                    }
                }
                //log.Info("SQL. OK - get Employes ");
            }
            catch (Exception ex)
            {
                log.Error("SQL. Error - get Employes. " + ex.ToString());
                MessageBox.Show("Невозможно получить список специалистов из базы данных");
            }
            return str;
        }

        /// <summary>
        /// Получает всех пользователей 
        /// </summary>
        /// <returns>Список типов</returns>
        public List<UserPrg> GetUsersPrg()
        {
            List<UserPrg> usrsPrg = new List<UserPrg>();

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    try
                    {
                        conn.Open();
                    }
                    catch { return usrsPrg; }

                string selectSQL = "SELECT Distinct * From [Users] where [Users].isLeave = false";

                using (OleDbCommand commd = new OleDbCommand(selectSQL, conn))
                {
                    using (OleDbDataReader dr = commd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string usrname = dr["UserName"] == DBNull.Value ? "" : (string)dr["UserName"];
                            string usrpass = dr["Pass"] == DBNull.Value ? "" : (string)dr["Pass"];
                            usrsPrg.Add(new UserPrg((Int32)dr["id"], usrname, usrpass, (Int32)dr["id_access"], (bool)dr["isLeave"]));
                        }
                    }
                }
                log.Info("SQL. OK - get users");
            }
            catch (Exception ex)
            {
                log.Error("SQL. Error - get users. " + ex.ToString());
                MessageBox.Show("Невозможно получить список пользователей из базы данных");
            }
            return usrsPrg;
        }

        /// <summary>
        /// Получает все виды услуг
        /// </summary>
        /// <returns>Список типов</returns>
        public List<CmbBxType> GetStyles()
        {
            List<CmbBxType> str = new List<CmbBxType>();

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    try
                    {
                        conn.Open();
                    }
                    catch { return str; }

                string selectSQL = "SELECT Distinct * From [Styles]";

                using (OleDbCommand commd = new OleDbCommand(selectSQL, conn))
                {
                    using (OleDbDataReader dr = commd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            str.Add(new CmbBxType((Int32)dr["id_style"], (string)dr["name_style"] ));
                        }
                    }
                }
                //log.Info("SQL. OK - get styles ");

            }
            catch (Exception ex)
            {
                log.Error("SQL. Error - get styles. " + ex.ToString());
                MessageBox.Show("Невозможно получить список услуг из базы данных");
            }
            return str;
        }

        OleDbDataAdapter dataAdapterReference;

        /// <summary>
        /// Получает таблицу из БД
        /// </summary>
        /// <param name="cmd">Строка запроса</param>
        /// <param name="cmbBx">true - запрос для combobox, false - запрос для datagridview</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataReference(string cmd, bool cmbBx)
        {
            DataTable dt = new DataTable();

            if (conn.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch { return dt; }
            }

            try
            {
                if (cmbBx)
                {
                    OleDbDataAdapter dataAdapter1 = new OleDbDataAdapter(cmd, conn);
                    OleDbCommandBuilder commandBuilder1 = new OleDbCommandBuilder(dataAdapter1);
                    dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
                    dataAdapter1.Fill(dt);
                }
                else
                {
                    dataAdapterReference = new OleDbDataAdapter(cmd, conn);
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(dataAdapterReference);

                    dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
                    dataAdapterReference.Fill(dt);
                }
                //log.Info("SQL. OK - get data reference ");

                return dt;
            }
            catch (OleDbException ex)
            {
                log.Error("SQL. Error - get data reference. " + ex.ToString());
                MessageBox.Show("Не удается получить данные справочника из базы!");
                return dt;
            }
        }

        /// <summary>
        /// Обновляет таблицу в БД
        /// </summary>
        /// <param name="dt1">Таблица в памяти</param>
        public void UpdateReference(DataTable dt1)
        {
            try
            {
                dataAdapterReference.Update(dt1);
                log.Info("SQL. OK - update reference ");
            }
            catch (OleDbException ex)
            {
                log.Error("SQL. Error - update reference. " + ex.ToString());
                MessageBox.Show("Невозможно сохранить измененные данные справочника", "Предупреждение");
            }
        }


        OleDbDataAdapter dataAdapterMaterials;

        public OleDbDataAdapter CreateCustomerAdapter(int id_visit, int id_m)
        {
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
            OleDbCommand command;
            OleDbParameter parameter;

            // Create the SelectCommand.
            command = new OleDbCommand("SELECT * FROM MaterialsVisit " +
                "WHERE id_visit = @id_visit", conn);

            command.Parameters.Add("id_visit", OleDbType.Integer).Value = id_visit;

            dataAdapter.SelectCommand = command;

            // Create the UpdateCommand.
            command = new OleDbCommand(
                "UPDATE MaterialsVisit SET NameMat = @NameMat " +
                "WHERE id_visit = @id_visit AND id_m = @id_m", conn);

            command.Parameters.Add("id_visit", OleDbType.Integer).Value = id_visit;
            command.Parameters.Add("id_m", OleDbType.Integer).Value = id_m;

           // parameter.SourceVersion = DataRowVersion.Original;

            dataAdapter.UpdateCommand = command;

            command = new OleDbCommand("DELETE * FROM MaterialsVisit " +
               "WHERE id_visit = @id_visit", conn);

            return dataAdapter;
        }

        /// <summary>
        /// Получает таблицу из БД
        /// </summary>
        /// <param name="cmd">Строка запроса</param>
        /// <param name="cmbBx">true - запрос для combobox, false - запрос для datagridview</param>
        /// <returns>DataTable</returns>
        public MaterialsVisit GetDataMaterialsVisit(string id_visit, bool cmbBx)
        {
            DataTable dt = new DataTable();
            MaterialsVisit materialsVisit = new MaterialsVisit();
            MaterialVisit material = null;
            string querySQL = "Select MaterialsVisit.*, Materials.name_m " +
                 " from [MaterialsVisit], [Materials] " +
                 " WHERE  MaterialsVisit.id_mat = Materials.id " +
                 " AND MaterialsVisit.id_visit=" + id_visit;
            
            if (conn.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch { return materialsVisit; }
            }

            try
            {
                using (OleDbCommand cmd = new OleDbCommand(querySQL,conn))
                {
                    using (OleDbDataReader r = cmd.ExecuteReader(CommandBehavior.Default))
                    {
                        while (r.Read())
                        {
                            material = new MaterialVisit();
                            material.Id = (int)r["id"];
                            material.Id_mat = (int)r["id_mat"];
                            material.Amount = (float)r["amount"];
                            material.Ed_izm = r["ed_izm"] == DBNull.Value ? "" : (string)r["ed_izm"];
                            material.Name_m = r["name_m"] == DBNull.Value ? "" : (string)r["name_m"];

                            materialsVisit.Add(material);
                        }
                    }
                }
 //             log.Info("SQL. OK - get materials visit id_visit = " + id);
                
                return materialsVisit;
            }
            catch (OleDbException ex)
            {
                log.Error("SQL. Error - get materials visit id_visit = " + id_visit + " . " + ex.ToString());
                MessageBox.Show("Не удается получить данные о материалах из базы!");
                return materialsVisit;
            }
        }


        /// <summary>
        /// Получает таблицу из БД
        /// </summary>
        /// <param name="cmd">Строка запроса</param>
        /// <param name="cmbBx">true - запрос для combobox, false - запрос для datagridview</param>
        /// <returns>DataTable</returns>
        public StylesVisit GetDataStylesVisit(string id_visit, bool cmbBx)
        {
            DataTable dt = new DataTable();
            StylesVisit stylesVisit = new StylesVisit();
            StyleVisit style = null;
            string querySQL = "Select StylesVisit.*, Styles.name_style " +
                " from [StylesVisit], [Styles] " +
                " WHERE StylesVisit.id_style = Styles.id " +
                " AND StylesVisit.id_visit=" + id_visit;

            if (conn.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch { return stylesVisit; }
            }

            try
            {
                using (OleDbCommand cmd = new OleDbCommand(querySQL, conn))
                {
                    using (OleDbDataReader r = cmd.ExecuteReader(CommandBehavior.Default))
                    {
                        while (r.Read())
                        {
                            style = new StyleVisit();
                            style.Id = (int)r["id"];
                            style.Id_style = (int)r["id_style"];
                            style.Id_employ = (int)r["id_employ"];
                            style.Cost = (float)r["cost"];
                            style.Name_st = r["name_style"] == DBNull.Value ? "" : (string)r["name_style"];
                            
                            stylesVisit.Add(style);
                        }
                    }
                }
                //             log.Info("SQL. OK - get styles visit id_visit = " + id);

                return stylesVisit;
            }
            catch (OleDbException ex)
            {
                log.Error("SQL. Error - get styles visit id_visit = " + id_visit + " . " + ex.ToString());
                MessageBox.Show("Не удается получить данные об услугах из базы!");
                return stylesVisit;
            }
        }

        /// <summary>
        /// Получает таблицу из БД
        /// </summary>
        /// <param name="cmd">Строка запроса</param>
        /// <param name="cmbBx">true - запрос для combobox, false - запрос для datagridview</param>
        /// <returns>DataTable</returns>
        public ConsultsVisit GetDataConsultsVisit(string id_visit, bool cmbBx)
        {
            DataTable dt = new DataTable();
            ConsultsVisit consultsVisit = new ConsultsVisit();
            ConsultVisit consult = null;
            string querySQL = "Select * from [ConsultsVisit] WHERE id_visit=" + id_visit;

            if (conn.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch { return consultsVisit; }
            }

            try
            {
                using (OleDbCommand cmd = new OleDbCommand(querySQL, conn))
                {
                    using (OleDbDataReader r = cmd.ExecuteReader(CommandBehavior.Default))
                    {
                        while (r.Read())
                        {
                            consult = new ConsultVisit();
                            consult.Id = (int)r["id"];
                            consult.ConsultName = r["ConsultName"] == DBNull.Value ? "" : (string)r["ConsultName"];
                            consult.Cost = (float)r["Cost"];

                            consultsVisit.Add(consult);
                        }
                    }
                }
                //             log.Info("SQL. OK - get styles visit id_visit = " + id);

                return consultsVisit;
            }
            catch (OleDbException ex)
            {
                log.Error("SQL. Error - get consults visit id_visit = " + id_visit + " . " + ex.ToString());
                MessageBox.Show("Не удается получить данные о консультациях из базы!");
                return consultsVisit;
            }
        }

        /// <summary>
        /// Обновляет таблицу в БД
        /// </summary>
        /// <param name="dt1">Таблица в памяти</param>
        public bool InsertMaterialsVisit(int idVisit, MaterialsVisit _matVisit)
        {
            MaterialsVisit matVis = _matVisit;
            Int32 id;
            string selectSQL = "SELECT top 1 * from [MaterialsVisit] order by id desc";
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

                foreach (MaterialVisit mat in matVis)
                {
                    id = -1;
                    using (OleDbCommand commd = new OleDbCommand(selectSQL, conn))
                    {
                        using (OleDbDataReader dr = commd.ExecuteReader())
                        {
                            try
                            {
                                while (dr.Read())
                                {
                                    id = (Int32)dr["id"];
                                }
                            }
                            catch
                            {
                                flag = false;
                            }
                        }
                    }

                    id++;
                    string insertSQL = "INSERT INTO [MaterialsVisit] " +
                                        " (id, id_visit, id_mat, amount, ed_izm) " +
                                        " VALUES (@id, @id_visit, @id_mat, @amount, @ed_izm) ";

                    using (OleDbCommand command = new OleDbCommand(insertSQL))
                    {
                        command.Connection = conn;
                        command.Parameters.Add("@id", OleDbType.Integer).Value = id;
                        command.Parameters.Add("@id_visit", OleDbType.Integer).Value = idVisit;
                        command.Parameters.Add("@id_mat", OleDbType.Integer).Value = mat.Id_mat;
                        command.Parameters.Add("@amount", OleDbType.Single).Value = mat.Amount;
                        command.Parameters.Add("@ed_izm", OleDbType.VarChar).Value = mat.Ed_izm;
                        try
                        {
                            command.ExecuteNonQuery();
                            log.Info("SQL. OK - insert material to visit id_visit = " + idVisit + " id_material = "+id);
                           // log.Info("id_visit = " + idVisit + " id_material = " + id + " name_material = " + mat.Name_m + " amount = " + mat.Amount + " ed_izm = " + mat.Ed_izm);
                        }
                        catch (Exception ex)
                        {
                            log.Error("SQL. Error - insert material to visit id_visit = " + idVisit +" id_material = "+ id +". " + ex.ToString());

                            flag = false;
                        }
                    }
                }
                return flag;
        }

        /// <summary>
        /// Обновляет таблицу в БД
        /// </summary>
        /// <param name="dt1">Таблица в памяти</param>
        public bool InsertStylesVisit(int idVisit, StylesVisit _stylesVisit)
        {
            StylesVisit stylesVis = _stylesVisit;
            Int32 id;
            string selectSQL = "SELECT top 1 * from [StylesVisit] order by id desc";
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

            foreach (StyleVisit style in stylesVis)
            {
                id = -1;
                using (OleDbCommand commd = new OleDbCommand(selectSQL, conn))
                {
                    using (OleDbDataReader dr = commd.ExecuteReader())
                    {
                        try
                        {
                            while (dr.Read())
                            {
                                id = (Int32)dr["id"];
                            }
                        }
                        catch
                        {
                            flag = false;
                        }
                    }
                }

                id++;
                string insertSQL = "INSERT INTO [StylesVisit] " +
                                    " (id, id_visit, id_style, id_employ, cost) " +
                                    " VALUES (@id, @id_visit, @id_style, @id_employ, @cost) ";

                using (OleDbCommand command = new OleDbCommand(insertSQL))
                {
                    command.Connection = conn;
                    command.Parameters.Add("@id", OleDbType.Integer).Value = id;
                    command.Parameters.Add("@id_visit", OleDbType.Integer).Value = idVisit;
                    command.Parameters.Add("@id_style", OleDbType.Integer).Value = style.Id_style;
                    command.Parameters.Add("@id_employ", OleDbType.Integer).Value = style.Id_employ;
                    command.Parameters.Add("@cost", OleDbType.Single).Value = style.Cost;
                    try
                    {
                        command.ExecuteNonQuery();
                        log.Info("SQL. OK - insert style to visit id_visit = " + idVisit + " id_style = "+ id);
                        //log.Info("id_visit = " + idVisit + " id_style = " + id + " name_style = " + style.Name_st + " id_employ = " + style.Id_employ + " cost = " + style.Cost);
                    }
                    catch (Exception ex)
                    {
                        log.Error("SQL. Error - insert style to visit id_visit = " + idVisit +" id_style = "+id +". "+ ex.ToString());

                        flag = false;
                    }
                }
            }
            return flag;
        }

        /// <summary>
        /// Обновляет таблицу в БД
        /// </summary>
        /// <param name="dt1">Таблица в памяти</param>
        public bool InsertConsultsVisit(int idVisit, ConsultsVisit _consultsVisit)
        {
            ConsultsVisit consultsVis = _consultsVisit;
            Int32 id;
            string selectSQL = "SELECT top 1 * from [ConsultsVisit] order by id desc";
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

            foreach (ConsultVisit consult in consultsVis)
            {
                id = -1;
                using (OleDbCommand commd = new OleDbCommand(selectSQL, conn))
                {
                    using (OleDbDataReader dr = commd.ExecuteReader())
                    {
                        try
                        {
                            while (dr.Read())
                            {
                                id = (Int32)dr["id"];
                            }
                        }
                        catch
                        {
                            flag = false;
                        }
                    }
                }

                id++;
                string insertSQL = "INSERT INTO [ConsultsVisit] " +
                                    " (id, id_visit, ConsultName, Cost) " +
                                    " VALUES (@id, @id_visit, @ConsultName, @Cost) ";

                using (OleDbCommand command = new OleDbCommand(insertSQL))
                {
                    command.Connection = conn;
                    command.Parameters.Add("@id", OleDbType.Integer).Value = id;
                    command.Parameters.Add("@id_visit", OleDbType.Integer).Value = idVisit;
                    command.Parameters.Add("@ConsultName", OleDbType.VarChar).Value = consult.ConsultName;
                    command.Parameters.Add("@Cost", OleDbType.Single).Value = consult.Cost;
                    try
                    {
                        command.ExecuteNonQuery();
                        log.Info("SQL. OK - insert consult to visit id_visit = " + idVisit + " id_style = " + id);
                        //log.Info("id_visit = " + idVisit + " id_style = " + id + " name_style = " + style.Name_st + " id_employ = " + style.Id_employ + " cost = " + style.Cost);
                    }
                    catch (Exception ex)
                    {
                        log.Error("SQL. Error - insert consult to visit id_visit = " + idVisit + " id_style = " + id + ". " + ex.ToString());

                        flag = false;
                    }
                }
            }
            return flag;
        }


        /// <summary>
        /// Удаляет все старые записи о материалах и добавляет новые
        /// </summary>
        /// <param name="dt1">Таблица в памяти</param>
        public bool UpdateMaterialsVisit(Int32 idVisit, MaterialsVisit _matVisit)
        {
            if (DeleteMaterialsVisit(idVisit))
                return InsertMaterialsVisit(idVisit, _matVisit);
            else
            {
                MessageBox.Show("Невозможно обновить данные о материалах в БД, использованные при посещении", "Внимание");
                return false;
            }
        }

        /// <summary>
        /// Удаляет все старые записи об услугах и добавляет новые
        /// </summary>
        /// <param name="dt1">Таблица в памяти</param>
        public bool UpdateStylesVisit(Int32 idVisit, StylesVisit _stylesVisit)
        {
            if (DeleteStylesVisit(idVisit))
                return InsertStylesVisit(idVisit, _stylesVisit);
            else
            {
                MessageBox.Show("Невозможно обновить данные об услугах в БД, использованные при посещении", "Внимание");
                return false;
            }
        }

        /// <summary>
        /// Удаляет все старые записи об услугах и добавляет новые
        /// </summary>
        /// <param name="dt1">Таблица в памяти</param>
        public bool UpdateConsultsVisit(Int32 idVisit, ConsultsVisit _consultsVisit)
        {
            if (DeleteConsultsVisit(idVisit))
                return InsertConsultsVisit(idVisit, _consultsVisit);
            else
            {
                MessageBox.Show("Невозможно обновить данные о консультациях в БД, полученные при посещении", "Внимание");
                return false;
            }
        }

        public void GetCountVisitsInMonth(DateTime dt1, DateTime dt2, UInt32 id_employ, int countsDayInMonth, out int[] countVisits, out int totalCountVisits)
        {
            //string selectSQL = "SELECT Vis.DateVisit, count(Vis.id) AS cntVis " +
            //    " FROM StylesVisit AS SVis, Visits AS Vis, Employ AS Emp " +
            //    " WHERE Emp.id = SVis.id_employ " +
            //    " AND SVis.id_visit = Vis.id " +
            //    " AND Vis.DateVisit >= @dt1 AND Vis.DateVisit <= @dt2 " +
            //    " AND Emp.id = @id_employ " +
            //    " GROUP BY Vis.DateVisit ";

            string selectSQL = "SELECT vis1.datevisit as DateVis, count(Vis1.id) as cntVis " +
                " FROM   Visits AS Vis1, " +
                " (SELECT  distinct( Vis.id) " +
                " FROM StylesVisit AS SVis, Visits AS Vis, Employ AS Emp " +
                " WHERE Emp.id = SVis.id_employ " +
                " AND SVis.id_visit = Vis.id " +
                " AND Vis.DateVisit >= @dt1 AND Vis.DateVisit <= @dt2 " +
                " AND Emp.id = @id_employ " +
                " )  AS [buffer] " +
                " WHERE Vis1.id = Vis.id " +
                " and Vis1.accept = true " +
                " group by vis1.datevisit ";


            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
            }
            catch (Exception)
            {
            }

            countVisits = new int[countsDayInMonth];
            totalCountVisits = 0;
            
            using (OleDbCommand commd = new OleDbCommand(selectSQL, conn))
            {
                commd.Parameters.Add("@dt1", OleDbType.Date).Value = dt1;
                commd.Parameters.Add("@dt2", OleDbType.Date).Value = dt2;
                commd.Parameters.Add("@id_employ", OleDbType.Integer).Value = id_employ;

                using (OleDbDataReader dr = commd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DateTime _dt = (DateTime)dr["DateVis"];
                        Int32 _cntVis = Convert.ToInt32((Int32)dr["cntVis"]);

                        countVisits[_dt.Day - 1] = _cntVis;
                        totalCountVisits += _cntVis;
                    }
                }
            }
        }

        public void GetTotalListStylesInMonth(DateTime dt1, DateTime dt2, UInt32 id_employ, int countDaysInMonth, ref List<styleInfo> listStyles)
        {
            string selectSQL = " SELECT  SVis.id_style, St.name_style, count(SVis.id_style) as CntStyles " +
                " FROM StylesVisit AS SVis, Visits AS Vis, Employ AS Emp, Styles AS St " +
                " WHERE (((Emp.id)=[SVis].[id_employ] " +
                " AND ((Vis.DateVisit)>= @dt1)  AND ((Vis.DateVisit)<= @dt2) " +
                " AND (Emp.id)=@id_employ) " +
                " AND ((SVis.id_visit)=[Vis].[id]) " +
                " AND ((St.id)=[SVis].[id_style]) " +
                " AND ((Vis.accept) = true)) " +
                " Group by  SVis.id_style, St.name_style ";
            
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
            }
            catch (Exception)
            {
            }

            using (OleDbCommand commd = new OleDbCommand(selectSQL, conn))
            {
                commd.Parameters.Add("@dt1", OleDbType.Date).Value = dt1;
                commd.Parameters.Add("@dt2", OleDbType.Date).Value = dt2;
                commd.Parameters.Add("@id_employ", OleDbType.Integer).Value = id_employ;

                using (OleDbDataReader dr = commd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        styleInfo st = new styleInfo();
                        st.sv = new StyleVisit();
                        st.sv.Id_style = (Int32)dr["id_style"];
                        st.sv.Name_st = (string)dr["name_style"];
                        st.totalCountStyleInMonth = (Int32)dr["cntStyles"];
                        st.countStyleInDay = new int[countDaysInMonth];
                        listStyles.Add(st);
                    }
                }
            }



        }

        public int[] GetInfoStylesInMonth(DateTime dt1, DateTime dt2, UInt32 id_employ, int id_style, int countDaysInMonth)
        {
            string selectSQL = " SELECT  Vis.DateVisit,  count(SVis.id_style) as CntStyles " +
                 " FROM StylesVisit AS SVis, Visits AS Vis, Employ AS Emp, Styles AS St " +
                 " WHERE (((Emp.id)=[SVis].[id_employ] " +
                 " AND ((Vis.DateVisit)>=@dt1)  AND ((Vis.DateVisit)<=@dt2) " +
                 " AND (Emp.id)=@id_employ) " +
                 " AND ((SVis.id_visit)=[Vis].[id]) " +
                 " AND ((St.id)=SVis.id_style)) " +
                 " AND St.id = @id_style " +
                 " AND Vis.accept = true " +
                 " Group by  Vis.DateVisit ";

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
            }
            catch (Exception)
            {
            }

            int[] abc = new int[countDaysInMonth];

            using (OleDbCommand commd = new OleDbCommand(selectSQL, conn))
            {
                commd.Parameters.Add("@dt1", OleDbType.Date).Value = dt1;
                commd.Parameters.Add("@dt2", OleDbType.Date).Value = dt2;
                commd.Parameters.Add("@id_employ", OleDbType.Integer).Value = id_employ;
                commd.Parameters.Add("@id_style", OleDbType.Integer).Value = id_style;

                using (OleDbDataReader dr = commd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DateTime _dt = (DateTime)dr["DateVisit"];
                        Int32 _cntSt = (Int32)dr["CntStyles"];

                        abc[_dt.Day - 1] = _cntSt;
                    }
                }
            }
            return abc;
        }


        public void GetCountVisitsInYear(DateTime dt1, DateTime dt2, UInt32 id_employ, int countsMonthInYear, out int[] countVisits, out int totalCountVisits)
        {
            //string selectSQL = "SELECT Vis.DateVisit, count(Vis.id) AS cntVis " +
            //    " FROM StylesVisit AS SVis, Visits AS Vis, Employ AS Emp " +
            //    " WHERE Emp.id = SVis.id_employ " +
            //    " AND SVis.id_visit = Vis.id " +
            //    " AND Vis.DateVisit >= @dt1 AND Vis.DateVisit <= @dt2 " +
            //    " AND Emp.id = @id_employ " +
            //    " GROUP BY Vis.DateVisit ";

            string selectSQL = "SELECT vis1.datevisit as DateVis, count(Vis1.id) as cntVis " +
                " FROM   Visits AS Vis1, " +
                " (SELECT  distinct( Vis.id) " +
                " FROM StylesVisit AS SVis, Visits AS Vis, Employ AS Emp " +
                " WHERE Emp.id = SVis.id_employ " +
                " AND SVis.id_visit = Vis.id " +
                " AND Vis.DateVisit >= @dt1 AND Vis.DateVisit <= @dt2 " +
                " AND Emp.id = @id_employ " +
                " )  AS [buffer] " +
                " WHERE Vis1.id = Vis.id " +
                " and Vis1.accept = true " +
                " group by vis1.datevisit ";


            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
            }
            catch (Exception)
            {
            }

            countVisits = new int[countsMonthInYear];
            totalCountVisits = 0;

            using (OleDbCommand commd = new OleDbCommand(selectSQL, conn))
            {
                commd.Parameters.Add("@dt1", OleDbType.Date).Value = dt1;
                commd.Parameters.Add("@dt2", OleDbType.Date).Value = dt2;
                commd.Parameters.Add("@id_employ", OleDbType.Integer).Value = id_employ;

                using (OleDbDataReader dr = commd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DateTime _dt = (DateTime)dr["DateVis"];
                        Int32 _cntVis = Convert.ToInt32((Int32)dr["cntVis"]);

                        countVisits[_dt.Month - 1] = countVisits[_dt.Month - 1] + _cntVis;
                        totalCountVisits += _cntVis;
                    }
                }
            }
        }

        public void GetTotalListStylesInYear(DateTime dt1, DateTime dt2, UInt32 id_employ, int countsMonthInYear, ref List<styleInfoMonth> listStyles)
        {
            string selectSQL = " SELECT  SVis.id_style, St.name_style, count(SVis.id_style) as CntStyles " +
                " FROM StylesVisit AS SVis, Visits AS Vis, Employ AS Emp, Styles AS St " +
                " WHERE (((Emp.id)=[SVis].[id_employ] " +
                " AND ((Vis.DateVisit)>= @dt1)  AND ((Vis.DateVisit)<= @dt2) " +
                " AND (Emp.id)=@id_employ) " +
                " AND ((SVis.id_visit)=[Vis].[id]) " +
                " AND ((St.id)=[SVis].[id_style]) " +
                " AND ((Vis.accept) = true)) " +
                " Group by  SVis.id_style, St.name_style ";

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
            }
            catch (Exception)
            {
            }

            using (OleDbCommand commd = new OleDbCommand(selectSQL, conn))
            {
                commd.Parameters.Add("@dt1", OleDbType.Date).Value = dt1;
                commd.Parameters.Add("@dt2", OleDbType.Date).Value = dt2;
                commd.Parameters.Add("@id_employ", OleDbType.Integer).Value = id_employ;

                using (OleDbDataReader dr = commd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        styleInfoMonth st = new styleInfoMonth();
                        st.sv = new StyleVisit();
                        st.sv.Id_style = (Int32)dr["id_style"];
                        st.sv.Name_st = (string)dr["name_style"];
                        st.totalCountStyleInYear = (Int32)dr["cntStyles"];
                        st.countStyleInMonth = new int[countsMonthInYear];
                        listStyles.Add(st);
                    }
                }
            }
        }

        public int[] GetInfoStylesInYear(DateTime dt1, DateTime dt2, UInt32 id_employ, int id_style, int countMonthInYear)
        {
            string selectSQL = " SELECT  Vis.DateVisit,  count(SVis.id_style) as CntStyles " +
                 " FROM StylesVisit AS SVis, Visits AS Vis, Employ AS Emp, Styles AS St " +
                 " WHERE (((Emp.id)=[SVis].[id_employ] " +
                 " AND ((Vis.DateVisit)>=@dt1)  AND ((Vis.DateVisit)<=@dt2) " +
                 " AND (Emp.id)=@id_employ) " +
                 " AND ((SVis.id_visit)=[Vis].[id]) " +
                 " AND ((St.id)=SVis.id_style)) " +
                 " AND St.id = @id_style " +
                 " AND Vis.accept = true " +
                 " Group by  Vis.DateVisit ";

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
            }
            catch (Exception)
            {
            }

            int[] abc = new int[countMonthInYear];

            using (OleDbCommand commd = new OleDbCommand(selectSQL, conn))
            {
                commd.Parameters.Add("@dt1", OleDbType.Date).Value = dt1;
                commd.Parameters.Add("@dt2", OleDbType.Date).Value = dt2;
                commd.Parameters.Add("@id_employ", OleDbType.Integer).Value = id_employ;
                commd.Parameters.Add("@id_style", OleDbType.Integer).Value = id_style;

                using (OleDbDataReader dr = commd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DateTime _dt = (DateTime)dr["DateVisit"];
                        Int32 _cntSt = (Int32)dr["CntStyles"];

                        abc[_dt.Month - 1] = abc[_dt.Month - 1] + _cntSt;
                    }
                }
            }
            return abc;
        }
    }

    public class CmbBxType
    {
        public CmbBxType(Int32 _id, string _name)
        {
            id = _id;
            name = _name;
        }
        public Int32 id;
        public string name;

        public override string ToString()
        {
            return name;
        }

    }

    public class MaterialVisit : ICloneable
    {
        int id = 0;
        int id_mat = 0;
        string name_m = "";
        float amount = 0;
        string ed_izm = "";

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Id_mat
        {
            get { return id_mat; }
            set { id_mat = value; }
        }

        public string Name_m
        {
            get { return name_m; }
            set { name_m = value; }
        }

        public float Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public string Ed_izm
        {
            get { return ed_izm; }
            set { ed_izm = value; }
        }

        #region ICloneable Members

        public object Clone()
        {
            MaterialVisit clone = new MaterialVisit();
            clone.id = this.id;
            clone.id_mat = this.id_mat;
            clone.name_m = this.name_m;
            clone.amount = this.amount;
            clone.ed_izm = this.ed_izm;
            return clone;
        }

        #endregion
    }

    public class StyleVisit : ICloneable
    {
        int id = 0;
        int id_style = 0;
        string name_st = "";
        int id_employ = 0;
        float cost = 0;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Id_style
        {
            get { return id_style; }
            set { id_style = value; }
        }

        public string Name_st
        {
            get { return name_st; }
            set { name_st = value; }
        }

        public int Id_employ
        {
            get { return id_employ; }
            set { id_employ = value; }
        }

        public float Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        #region ICloneable Members

        public object Clone()
        {
            StyleVisit clone = new StyleVisit();
            clone.id = this.id;
            clone.id_style = this.id_style;
            clone.name_st = this.name_st;
            clone.id_employ = this.id_employ;
            clone.cost = this.cost;
            return clone;
        }

        #endregion
    }
    
    public class ConsultVisit : ICloneable
    {
        int id = 0;
        string consultName = "";
        float cost = 0;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string ConsultName
        {
            get { return consultName; }
            set { consultName = value; }
        }

        public float Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        #region ICloneable Members

        public object Clone()
        {
            ConsultVisit clone = new ConsultVisit();
            clone.id = this.id;
            clone.consultName = this.consultName;
            clone.cost = this.cost;
            return clone;
        }

        #endregion
    }
    

    public class MaterialsVisit : BindingList<MaterialVisit>, ICloneable 
    {
        #region ICloneable Members
        
        public object Clone()
        {
            MaterialsVisit clone = new MaterialsVisit();
            foreach (MaterialVisit item in this)
            {
                clone.Add((MaterialVisit)item.Clone());
            }
            return clone;
        }
        #endregion
    }

    public class StylesVisit : BindingList<StyleVisit>, ICloneable
    {
        public float GetCostStyles
        {
            get
            {
                float summ = 0;
                foreach (StyleVisit item in this)
                {
                    summ = summ + Convert.ToSingle(Math.Round(item.Cost, 2));
                }
                return summ;
            }
        }

        #region ICloneable Members

        public object Clone()
        {
            StylesVisit clone = new StylesVisit();
            foreach (StyleVisit item in this)
            {
                clone.Add((StyleVisit)item.Clone());
            }
            return clone;
        }
        #endregion
    }

    public class ConsultsVisit : BindingList<ConsultVisit>, ICloneable
    {
        public float GetCostConsults
        {
            get
            {
                float summ = 0;
                foreach (ConsultVisit item in this)
                {
                    summ = summ + Convert.ToSingle(Math.Round(item.Cost, 2));
                }
                return summ;
            }
        }

        #region ICloneable Members

        public object Clone()
        {
            ConsultsVisit clone = new ConsultsVisit();
            foreach (ConsultVisit item in this)
            {
                clone.Add((ConsultVisit)item.Clone());
            }
            return clone;
        }
        #endregion
    }

    public class UserPrg
    {
        public int id;
        public int id_accessLevel;
        public string nameUser;
        public string passUser;
        public bool isLeave;

        public UserPrg(int _id, string _nameuser, string _passUser, int _idAccessLevel, bool _isLeave)
        {
            id = _id;
            id_accessLevel = _idAccessLevel;
            nameUser = _nameuser;
            passUser = _passUser;
            isLeave = _isLeave;
        }

        public override string ToString()
        {
            return nameUser;
        }
    }

}
