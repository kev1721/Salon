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
    public class Rep
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
            ttldnw.name = "Итог";
            ttldnw.totalStylesInDay = getTotalDownCountStyles(infoStylesInMonth, countDaysInMonth);

            DataView dvMain = getDataTable(countDaysInMonth, infoStylesInMonth, countVisitsInDay, totalCountVisitsInMonth, countStylesInDay, fullTotalCountStylesInMonth);
            DataView dvTotalDown = getDataTableTotalDown(countDaysInMonth, ttldnw.totalStylesInDay, fullTotalCountStylesInMonth);

            using (Report report = new Report())
            {

                report.Load(Application.StartupPath + "\\EmployStylesInMonth.frx");
                report.RegisterData(dvMain, "EmployStylesInMonth");
                report.RegisterData(dvTotalDown, "EmployStylesInMonthTotalDown");


                report.SetParameterValue("UserWork", Program.currUserWorking);
                report.SetParameterValue("DateGenerateReport", DateTime.Now.Date);
                report.SetParameterValue("Employ", fio_employ);
                report.SetParameterValue("dt", dt);

                report.Design();
                //report.Show();
            }
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
            row["Name"] = "Итог";
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

}