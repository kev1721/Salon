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
using FastReport;

namespace Style
{
    public class RepMonth
    {
        public void Generate(DateTime dt, UInt32 id_employ, string fio_employ)
        {
            //id_employ = 2; 

            //int countDaysInMonth = DateTime.DaysInMonth(dt.Year, dt.Month); //число дней за месяц
            int countDaysInMonth = 31;
            int[] countVisitsInDay = new int[countDaysInMonth]; //массив с числами посещений за каждый день
            int totalCountVisitsInMonth = 0; //общее число посещений за месяц (является суммой всех countVisitsInDay)
            
            List<styleInfo> infoStylesInMonth = new List<styleInfo>(); //список услуг за месяц с подробной информацией
            int fullTotalCountStylesInMonth = 0; //общее число услуг, выполненных за месяц (является суммой всех totalCountStyleInMonth)
            int[] countStylesInDay = new int[countDaysInMonth]; //общее число услуг, выполненных за каждый день

            DateTime _dt1 = new DateTime(dt.Year, dt.Month, 1);
            DateTime _dt2 = new DateTime(dt.Year, dt.Month + 1, 1).AddSeconds(-1);

            Program.dbStyle.GetCountVisitsInMonth(_dt1, _dt2, id_employ, countDaysInMonth, out countVisitsInDay, out totalCountVisitsInMonth);
            Program.dbStyle.GetTotalListStylesInMonth(_dt1, _dt2, id_employ, countDaysInMonth, ref infoStylesInMonth);
            
            fullTotalCountStylesInMonth = getFullTotalCountStyles(infoStylesInMonth);

            for (int i = 0; i < infoStylesInMonth.Count; i++)
                infoStylesInMonth[i].countStyleInDay = Program.dbStyle.GetInfoStylesInMonth(_dt1, _dt2, id_employ, infoStylesInMonth[i].sv.Id_style, countDaysInMonth);

            TotalDown ttldnw = new TotalDown();
            ttldnw.name = "Итоги";
            ttldnw.totalStylesInDay = getTotalDownCountStyles(infoStylesInMonth, countDaysInMonth);

            DataView dvVisits = getDataTableVisits(countDaysInMonth, countVisitsInDay, totalCountVisitsInMonth);
            DataView dvMain = getDataTable(countDaysInMonth, infoStylesInMonth, countVisitsInDay, totalCountVisitsInMonth, countStylesInDay, fullTotalCountStylesInMonth);
            DataView dvTotalDown = getDataTableTotalDown(countDaysInMonth, ttldnw.totalStylesInDay, fullTotalCountStylesInMonth);

            using (Report report = new Report())
            {

                report.Load(Application.StartupPath + "\\EmployStylesInMonth.frx");
                report.RegisterData(dvVisits, "EmployVisits");
                report.RegisterData(dvMain, "EmployStylesInMonth");
                report.RegisterData(dvTotalDown, "EmployStylesInMonthTotalDown");


                report.SetParameterValue("UserWork", Program.currUserWorking);
                report.SetParameterValue("DateGenerateReport", DateTime.Now.Date);
                report.SetParameterValue("Employ", fio_employ);
                report.SetParameterValue("dt", dt);

                //report.Design();
                report.Show();
            }
        }

        private DataView getDataTableVisits(int countDaysInMonth, int[] countVisitsInDay, int totalCountVisitsInMonth)
        {
            DataTable table = new DataTable("EmployVisits");

            // Declare DataColumn and DataRow variables.
            DataColumn column;
            DataRow row;

            // Создаем колонку Наименование
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Name";
            table.Columns.Add(column);

            //создаем массив колонок - дней
            for (int i = 0; i < countDaysInMonth; i++)
            {
                // Create second column.
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = (i + 1).ToString();
                table.Columns.Add(column);
            }

            // Создаем колонку Итог. Подвал справа.
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Total";
            table.Columns.Add(column);

            //Создаем строку с информацией о количестве посещений за каждый день
            row = table.NewRow();
            row["Name"] = "Посещения";
            for (int j = 0; j < countDaysInMonth; j++)
            {
                if (countVisitsInDay[j] > 0)
                    row[(j + 1).ToString()] = countVisitsInDay[j];
                else
                    row[(j + 1).ToString()] = "";
            }
            if (totalCountVisitsInMonth > 0)
                row["Total"] = totalCountVisitsInMonth;
            else
                row["Total"] = "";
            table.Rows.Add(row);

            return new DataView(table);
             
        }

        int getFullTotalCountStyles(List<styleInfo> infoStylesInMonth)
        {
            int totalCount = 0;

            foreach (styleInfo item in infoStylesInMonth)
            {
                totalCount += item.totalCountStyleInMonth;    
            }
            return totalCount;
        }

        int[] getTotalDownCountStyles(List<styleInfo> infoStylesInMonth, int countDaysInMonth)
        {
            int[] totalCount = new int[countDaysInMonth];

            for (int i = 0; i < countDaysInMonth; i++)
            {
                foreach (styleInfo item in infoStylesInMonth)
                {
                    totalCount[i] += item.countStyleInDay[i];
                }
            }

            return totalCount;
        }

        DataView getDataTable(int countDaysInMonth, List<styleInfo> infoStylesInMonth, int[] countVisitsInDay, int totalCountVisitsInMonth, int[] countStylesInDay, int fullTotalCountStylesInMonth)
        {
            DataTable table = new DataTable("EmployStylesInMonth");

            // Declare DataColumn and DataRow variables.
            DataColumn column;
            DataRow row;

            // Создаем колонку Наименование
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Name";
            table.Columns.Add(column);

            //создаем массив колонок - дней
            for (int i = 0; i < countDaysInMonth; i++)
            {
                // Create second column.
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = (i + 1).ToString();
                table.Columns.Add(column);
            }

            // Создаем колонку Итог. Подвал справа.
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Total";
            table.Columns.Add(column);

            // Создаем строки с услугами
            for (int i = 0; i < infoStylesInMonth.Count; i++)
            {
                row = table.NewRow();
                row["Name"] = infoStylesInMonth[i].sv.Name_st;
                for (int j = 0; j < countDaysInMonth; j++)
                {
                    if (infoStylesInMonth[i].countStyleInDay[j] > 0)
                        row[(j + 1).ToString()] = infoStylesInMonth[i].countStyleInDay[j];
                    else
                        row[(j + 1).ToString()] = "";
                }
                if (infoStylesInMonth[i].totalCountStyleInMonth > 0)
                    row["Total"] = infoStylesInMonth[i].totalCountStyleInMonth;
                else
                    row["Total"] = "";
                table.Rows.Add(row);
            }

            return new DataView(table);
        }

        DataView getDataTableTotalDown(int countDaysInMonth, int[] countStylesInDay, int fullTotalCountStylesInMonth)
        {
            DataTable table = new DataTable("EmployStylesInMonthTotalDown");

            // Declare DataColumn and DataRow variables.
            DataColumn column;
            DataRow row;

            // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Name";
            table.Columns.Add(column);

            for (int i = 0; i < countDaysInMonth; i++)
            {
                // Create second column.
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = (i + 1).ToString();
                table.Columns.Add(column);
            }

            // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Total";
            table.Columns.Add(column);

            // Create new DataRow objects and add to DataTable.    
            row = table.NewRow();
            row["Name"] = "Итоги услуг за день";
            for (int j = 0; j < countDaysInMonth; j++)
            {
                if (countStylesInDay[j] > 0)
                    row[(j + 1).ToString()] = countStylesInDay[j];
                else
                    row[(j + 1).ToString()] = "";
            }
            if (fullTotalCountStylesInMonth > 0)
                row["Total"] = fullTotalCountStylesInMonth;
            else
                row["Total"] = "";
            table.Rows.Add(row);

            return new DataView(table);
        }

    }

    public class styleInfo
    {
        public StyleVisit sv; //услуга
        public int[] countStyleInDay; //сколько раз услуга была выполнена за каждый день месяца
        public int totalCountStyleInMonth; //общее число выполнения услуги за месяц
    }

    public struct TotalDown
    {
        public string name;
        public int[] totalStylesInDay;
    }

    public class RepYear
    {
        public void Generate(DateTime dt, UInt32 id_employ, string fio_employ)
        {
            //id_employ = 2; 

            //int countDaysInMonth = DateTime.DaysInMonth(dt.Year, dt.Month); //число дней за месяц
            int countMonthIsYear = 12;
            int[] countVisitsInMonth = new int[countMonthIsYear]; //массив с числами посещений месяц
            int totalCountVisitsInYear = 0; //общее число посещений за год (является суммой всех countVisitsInMonth)

            List<styleInfoMonth> infoStylesInMonth = new List<styleInfoMonth>(); //список услуг за месяц с подробной информацией
            int fullTotalCountStylesInYear = 0; //общее число услуг, выполненных за год (является суммой всех totalCountVisitsInYear)
            int[] countStylesInMonth = new int[countMonthIsYear]; //общее число услуг, выполненных за каждый месяц

            //DateTime _dt1 = new DateTime(dt.Year, dt.Month, 1);
            //DateTime _dt2 = new DateTime(dt.Year, dt.Month + 1, 1).AddSeconds(-1);
            DateTime _dt1 = new DateTime(dt.Year, 1, 1);
            DateTime _dt2 = new DateTime(dt.Year+1, 1, 1).AddSeconds(-1);

            Program.dbStyle.GetCountVisitsInYear(_dt1, _dt2, id_employ, countMonthIsYear, out countVisitsInMonth, out totalCountVisitsInYear);
            Program.dbStyle.GetTotalListStylesInYear(_dt1, _dt2, id_employ, countMonthIsYear, ref infoStylesInMonth);
            
            fullTotalCountStylesInYear = getFullTotalCountStyles(infoStylesInMonth);

            for (int i = 0; i < infoStylesInMonth.Count; i++)
                infoStylesInMonth[i].countStyleInMonth = Program.dbStyle.GetInfoStylesInYear(_dt1, _dt2, id_employ, infoStylesInMonth[i].sv.Id_style, countMonthIsYear);

            TotalDown ttldnw = new TotalDown();
            ttldnw.name = "Итоги";
            ttldnw.totalStylesInDay = getTotalDownCountStyles(infoStylesInMonth, countMonthIsYear);

            DataView dvVisits = getDataTableVisits(countMonthIsYear, countVisitsInMonth, totalCountVisitsInYear);
            DataView dvMain = getDataTable(countMonthIsYear, infoStylesInMonth, countVisitsInMonth, totalCountVisitsInYear, countStylesInMonth, fullTotalCountStylesInYear);
            DataView dvTotalDown = getDataTableTotalDown(countMonthIsYear, ttldnw.totalStylesInDay, fullTotalCountStylesInYear);

            using (Report report = new Report())
            {

                report.Load(Application.StartupPath + "\\EmployStylesInYear.frx");
                report.RegisterData(dvVisits, "EmployVisits");
                report.RegisterData(dvMain, "EmployStylesInYear");
                report.RegisterData(dvTotalDown, "EmployStylesInYearTotalDown");


                report.SetParameterValue("UserWork", Program.currUserWorking);
                report.SetParameterValue("DateGenerateReport", DateTime.Now.Date);
                report.SetParameterValue("Employ", fio_employ);
                report.SetParameterValue("dt", dt);

                //report.Design();
                report.Show();
            }
        }


        private DataView getDataTableVisits(int countMonthInYear, int[] countVisitsInMonth, int totalCountVisitsInYear)
        {
            DataTable table = new DataTable("EmployVisits");

            // Declare DataColumn and DataRow variables.
            DataColumn column;
            DataRow row;

            // Создаем колонку Наименование
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Name";
            table.Columns.Add(column);

            //создаем массив колонок - дней
            for (int i = 0; i < countMonthInYear; i++)
            {
                // Create second column.
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = (i + 1).ToString();
                table.Columns.Add(column);
            }

            // Создаем колонку Итог. Подвал справа.
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Total";
            table.Columns.Add(column);

            //Создаем строку с информацией о количестве посещений за каждый день
            row = table.NewRow();
            row["Name"] = "Посещения";
            for (int j = 0; j < countMonthInYear; j++)
            {
                if (countVisitsInMonth[j] > 0)
                    row[(j + 1).ToString()] = countVisitsInMonth[j];
                else
                    row[(j + 1).ToString()] = "";
            }
            if (totalCountVisitsInYear > 0)
                row["Total"] = totalCountVisitsInYear;
            else
                row["Total"] = "";
            table.Rows.Add(row);

            return new DataView(table);

        }

        int getFullTotalCountStyles(List<styleInfoMonth> infoStylesInYear)
        {
            int totalCount = 0;

            foreach (styleInfoMonth item in infoStylesInYear)
            {
                totalCount += item.totalCountStyleInYear;
            }
            return totalCount;
        }

        int[] getTotalDownCountStyles(List<styleInfoMonth> infoStylesInYear, int countMonthInYear)
        {
            int[] totalCount = new int[countMonthInYear];

            for (int i = 0; i < countMonthInYear; i++)
            {
                foreach (styleInfoMonth item in infoStylesInYear)
                {
                    totalCount[i] += item.countStyleInMonth[i];
                }
            }

            return totalCount;
        }

        DataView getDataTable(int countMonthInYear, List<styleInfoMonth> infoStylesInYear, int[] countVisitsInMonth, int totalCountVisitsInYear, int[] countStylesInMonth, int fullTotalCountStylesInYear)
        {
            DataTable table = new DataTable("EmployStylesInYear");

            // Declare DataColumn and DataRow variables.
            DataColumn column;
            DataRow row;

            // Создаем колонку Наименование
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Name";
            table.Columns.Add(column);

            //создаем массив колонок - месяцев
            for (int i = 0; i < countMonthInYear; i++)
            {
                // Create second column.
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = (i + 1).ToString();
                table.Columns.Add(column);
            }

            // Создаем колонку Итог. Подвал справа.
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Total";
            table.Columns.Add(column);

            // Создаем строки с услугами
            for (int i = 0; i < infoStylesInYear.Count; i++)
            {
                row = table.NewRow();
                row["Name"] = infoStylesInYear[i].sv.Name_st;
                for (int j = 0; j < countMonthInYear; j++)
                {
                    if (infoStylesInYear[i].countStyleInMonth[j] > 0)
                        row[(j + 1).ToString()] = infoStylesInYear[i].countStyleInMonth[j];
                    else
                        row[(j + 1).ToString()] = "";
                }
                if (infoStylesInYear[i].totalCountStyleInYear > 0)
                    row["Total"] = infoStylesInYear[i].totalCountStyleInYear;
                else
                    row["Total"] = "";
                table.Rows.Add(row);
            }

            return new DataView(table);
        }

        DataView getDataTableTotalDown(int countMonthInYear, int[] countStylesInMonth, int fullTotalCountStylesInYear)
        {
            DataTable table = new DataTable("EmployStylesInYearTotalDown");

            // Declare DataColumn and DataRow variables.
            DataColumn column;
            DataRow row;

            // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Name";
            table.Columns.Add(column);

            for (int i = 0; i < countMonthInYear; i++)
            {
                // Create second column.
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = (i + 1).ToString();
                table.Columns.Add(column);
            }

            // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Total";
            table.Columns.Add(column);

            // Create new DataRow objects and add to DataTable.    
            row = table.NewRow();
            row["Name"] = "Итоги услуг за месяц";
            for (int j = 0; j < countMonthInYear; j++)
            {
                if (countStylesInMonth[j] > 0)
                    row[(j + 1).ToString()] = countStylesInMonth[j];
                else
                    row[(j + 1).ToString()] = "";
            }
            if (fullTotalCountStylesInYear > 0)
                row["Total"] = fullTotalCountStylesInYear;
            else
                row["Total"] = "";
            table.Rows.Add(row);

            return new DataView(table);
        }
    }

    public class styleInfoMonth
    {
        public StyleVisit sv; //услуга
        public int[] countStyleInMonth; //сколько раз услуга была выполнена за каждый месяц
        public int totalCountStyleInYear; //общее число выполнения услуги за год
    }

    public struct TotalDownMonth
    {
        public string name;
        public int[] totalStylesInMonth;
    }

}