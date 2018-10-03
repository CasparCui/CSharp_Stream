using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caspar.CSharpTest
{
    class LinQToAdoDemo
    {
        protected static DataSet GetADataSetForLinqToSQL(string connectionString, string commandString = "Select * From [dbo].[Table]")
        {
            using (var conn = new DataBaseConnection(connectionString))
            {
                var command = conn.SqlConnection.CreateCommand();
                command.CommandText = commandString;
                var sqlda = new SqlDataAdapter(command);
                var ds = new DataSet();
                sqlda.Fill(ds);
                return ds;
            }
        }

        public static void UseLinQToSelect_Update_DeleteDataSet(string connectionString)
        {
            DataSet ds = GetADataSetForLinqToSQL(connectionString);
            var dt = ds.Tables[0];

            //Select data from DataSet
            var datas = from d in dt.AsEnumerable()
                       select new { Id = d[0], Name = d[1] };
            foreach (var data in datas)
            {
                Console.WriteLine("{0}, {1}", data.Id, data.Name);
            }

            //Update data from DataSet
            var datas1 = from d in dt.AsEnumerable()
                        where (int) d[0] == 2
                        select d;

            foreach (var row in datas1)
            {
                row.SetField<string>(1, "heihei");
            }
            dt.AcceptChanges();

            //Delete data from DataSet
            var datas2 = from d in dt.AsEnumerable()
                        where (string) d[1] == "heihei"
                        select d;
            foreach(var data in datas2)
            {
                dt.Rows.Remove(data);
            }

            //Add data into DataSet
            //用不上 LINQ

        }
        public static void UsingLinQToSelectDataFrom2DataTable(string connectionString,string commandString)
        {
            var ds = GetADataSetForLinqToSQL(connectionString, commandString);
            Dictionary<string, DataTable> dtDic = new Dictionary<string, DataTable>();
            foreach (DataTable table in ds.Tables)
            {
                dtDic.Add(table.TableName, table);
            }
            var dataCommu = from tablerow in dtDic["Table"].AsEnumerable()
                            from table1row in dtDic["Table1"].AsEnumerable()
                            where (string) tablerow["Name"] == "Hanwen Cui" && (int) table1row["Count"] > 0
                            select new { TableName = (string)tablerow["Name"], Table1Count = table1row["Count"] };
            foreach (var data in dataCommu)
            {
                Console.WriteLine("{0}, {1}", data.TableName, data.Table1Count);
            }

        }
        public static void UsingLinQToSelectDataAndCoypIntoTheOtherDataTable(string connectionString)
        {
            var ds = GetADataSetForLinqToSQL(connectionString);
            var dataRows = from dataRow in ds.Tables["Table"].AsEnumerable() 
                           where (int) dataRow[0] > 3
                           select dataRow;
            var dt = dataRows.CopyToDataTable();

        }
    }
}
