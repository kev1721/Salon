using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Data;

namespace Style
{
    public class DB
    {
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
                return true;
            }
            catch { }
            return false;
        }

        public bool DisconnectDB()
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                return true;
            }
            catch(Exception ex)
            {}
            
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
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Невозможно удалить клиента из базы данных \r\n");
                    return false;
                }
            }

        }

        public bool UpdateClient(UInt32 idClient, string LastName, string FirstName, string MiddleName,
            string telMobile, string telHome,
            string address, DateTime birthDay, uint discountConst, string notes)
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
                command.Parameters.Add("@birthDay", OleDbType.Date).Value = birthDay.Date;
                command.Parameters.Add("@discountConst", OleDbType.Integer).Value = discountConst;
                command.Parameters.Add("@notes", OleDbType.VarChar).Value = notes;
                command.Parameters.Add("@id_client", OleDbType.Integer).Value = idClient;

                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Невозможно изменить данные клиента в базе данных\r\n" + ex.ToString());
                    return false;
                }
            }

        }
        
        public bool InsertClient(string LastName, string FirstName, string MiddleName, 
            string telMobile, string telHome, 
            string address, DateTime birthDay, uint discountConst, string notes)
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch { return false; }
            }
            Int32 id = -1;

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
                command.Parameters.Add("@birthDay", OleDbType.Date).Value = birthDay.Date;
                command.Parameters.Add("@discountConst", OleDbType.Integer).Value = discountConst;
                command.Parameters.Add("@notes", OleDbType.VarChar).Value = notes;

                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Невозможно добавить клиента в базу данных\r\n" + ex.ToString());
                    return false;
                }
            }

        }

        public DataSet GetClients()
        {
            string cmdTxt = "Select * from [Clients]";
            DataSet ds = null;

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                using (OleDbCommand myCmd = conn.CreateCommand())
                {
                    ds = new DataSet();
                    myCmd.CommandText = cmdTxt;
                    OleDbDataAdapter da = new OleDbDataAdapter();
                    da.SelectCommand = myCmd;
                    da.Fill(ds, "Clients");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Невозможно получить клиентов из базы данных");
            }
            return ds;
        }


        public bool DeleteVisit(UInt32 id_visit)
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
                                  " WHERE id_visit = @id_visit ";
            using (OleDbCommand command = new OleDbCommand(deleteSQL))
            {
                command.Connection = conn;
                command.Parameters.Add("@id_visit", OleDbType.Integer).Value = id_visit;
                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Невозможно удалить посещение из базы данных\r\n" + ex.ToString());
                    return false;
                }
            }

        }

        public bool UpdateVisit(UInt32 id_visit, UInt32 id_client, DateTime dateVisit, int id_employ, int id_style,
            int costStyle, string consult, int costConsult, string note,
            int discountSeason, int costVisit)
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
                       " id_st = @id_st, " +
                       " id_e = @id_e, " +
                       " DateVisit = @DateVisit, " +
                       " CostStyle = @CostStyle, " +
                       " Consult = @Consult, " +
                       " CostConsult = @CostConsult, " +
                       " DiscountSeason = @DiscountSeason, " +
                       " CostVisit = @CostVisit, " +
                       " Note = @Note " +
                       " WHERE id_visit = @id_visit ";

            using (OleDbCommand command = new OleDbCommand(updateSQL))
            {
                command.Connection = conn;
                command.Parameters.Add("@id_client", OleDbType.Integer).Value = id_client;
                command.Parameters.Add("@id_st", OleDbType.Integer).Value = id_style;
                command.Parameters.Add("@id_e", OleDbType.Integer).Value = id_employ;
                command.Parameters.Add("@DateVisit", OleDbType.Date).Value = dateVisit.Date;
                command.Parameters.Add("@CostStyle", OleDbType.Integer).Value = costStyle;
                command.Parameters.Add("@Consult", OleDbType.VarChar).Value = consult;
                command.Parameters.Add("@CostConsult", OleDbType.Integer).Value = costConsult;
                command.Parameters.Add("@DiscountSeason", OleDbType.Integer).Value = discountSeason;
                command.Parameters.Add("@CostVisit", OleDbType.Integer).Value = costVisit;
                command.Parameters.Add("@Note", OleDbType.VarChar).Value = note;
                command.Parameters.Add("@id_visit", OleDbType.Integer).Value = id_visit;

                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Невозможно изменить данные посещения в базе данных\r\n" + ex.ToString());
                    return false;
                }
            }

        }

        public bool InsertVisit(UInt32 id_client, DateTime dateVisit, int id_employ, int id_style,
            int costStyle, string consult, int costConsult, string note,
            int discountSeason, int costVisit)
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch { return false; }
            }
            Int32 id_visit = -1;

            string selectSQL = "SELECT top 1 * from [Visits] order by id_visit desc";

            using (OleDbCommand commd = new OleDbCommand(selectSQL, conn))
            {
                using (OleDbDataReader dr = commd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        id_visit = (Int32)dr["id_visit"];
                    }
                }
            }

            id_visit++;
            string insertSQL = "INSERT INTO [Visits] " +
                                " ([id_visit], [id_client], [id_st], [id_e], [DateVisit], [CostStyle], [Consult], [CostConsult], [DiscountSeason], [CostVisit], [Note]) " +
                                " VALUES (@id_visit, @id_client, @id_st, @id_e, @DateVisit, @CostStyle, @Consult, @CostConsult, @DiscountSeason, @CostVisit, @Note) ";

            using (OleDbCommand command = new OleDbCommand(insertSQL))
            {
                command.Connection = conn;
                command.Parameters.Add("@id_visit", OleDbType.Integer).Value = id_visit;
                command.Parameters.Add("@id_client", OleDbType.Integer).Value = id_client;
                command.Parameters.Add("@id_st", OleDbType.Integer).Value = id_style;
                command.Parameters.Add("@id_e", OleDbType.Integer).Value = id_employ;
                command.Parameters.Add("@DateVisit", OleDbType.Date).Value = dateVisit.Date;
                command.Parameters.Add("@CostStyle", OleDbType.Integer).Value = costStyle;
                command.Parameters.Add("@Consult", OleDbType.VarChar).Value = consult;
                command.Parameters.Add("@CostConsult", OleDbType.Integer).Value = costConsult;
                command.Parameters.Add("@DiscountSeason", OleDbType.Integer).Value = discountSeason;
                command.Parameters.Add("@CostVisit", OleDbType.Integer).Value = costVisit;
                command.Parameters.Add("@Note", OleDbType.VarChar).Value = note;

                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Невозможно добавить посещение в базу данных\r\n" + ex.ToString());
                    return false;
                }
            }

        }

        public DataSet GetVisitsClient(int id_client)
        {
            string cmdTxt = "SELECT Visits.id_visit, Visits.id_client, Visits.id_st, Visits.id_e, " +
                            "Visits.DateVisit, " +
                            "Employ.FIO as FioEmploy, Styles.name_style as NameStyle, " + 
                            "Visits.CostStyle, " +
                            "Visits.Consult, Visits.CostConsult, " +
                            "Visits.DiscountSeason, Visits.CostVisit, Visits.Note " +
                            
                            "FROM [Visits], [Employ], [Styles] "+
                            "WHERE Visits.id_st = Styles.id_style AND Visits.id_e = Employ.id_employ AND Visits.id_client = @id_client";
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
            }
            catch (Exception e)
            {
                MessageBox.Show("Невозможно получить посещения из базы данных");
            }
            return ds;
        }

        /// <summary>
        /// Получает всех сотрудников
        /// </summary>
        /// <returns>Список типов</returns>
        public List<CmbBxType> GetEmployes()
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

                string selectSQL = "SELECT Distinct * From [Employ]";

                using (OleDbCommand commd = new OleDbCommand(selectSQL, conn))
                {
                    using (OleDbDataReader dr = commd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            str.Add(new CmbBxType((Int32)dr["id_employ"], (string)dr["FIO"] ));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Невозможно получить список специалистов из базы данных");
            }
            return str;
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
            }
            catch (Exception e)
            {
                MessageBox.Show("Невозможно получить список услуг из базы данных");
            }
            return str;
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
}
