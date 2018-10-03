using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Caspar.CSharpTest
{
    public class DataBaseConnection : IDisposable
    {
        private string sqlConnectionString = string.Empty;
        private SqlConnectionStringBuilder sqlConnectionStringBuilder;
        private SqlConnection sqlConnection;

        public string SqlConnectionString { get => sqlConnectionString; private set => sqlConnectionString = value; }
        public SqlConnectionStringBuilder SqlConnectionStringBuilder { get => sqlConnectionStringBuilder; private set => sqlConnectionStringBuilder = value; }

        public SqlConnection SqlConnection
        {
            get
            {
                if (sqlConnection == null || disposedValue == true)
                {
                    if (sqlConnectionString.Equals(string.Empty) || sqlConnectionString == null)
                    {
                        throw new NullReferenceException("SQL Connection String is empty");
                    }
                    sqlConnection = new SqlConnection(sqlConnectionString);
                    sqlConnection.Open();
                }
                return sqlConnection;
            }

            private set
            {
                sqlConnection = value;
            }
        }

        public DataBaseConnection(string sqlConnectionString)
        {
            //sqlConnection = new SqlConnection(sqlConnectionString);
            SqlConnectionString = sqlConnectionString;
        }

        public DataBaseConnection(SqlConnectionStringBuilder sqlConnenctionStringBuilder)
        {
            //sqlConnection = new SqlConnection(sqlConnenctionStringBuilder.ToString());
            SqlConnectionString = sqlConnenctionStringBuilder.ToString();
            sqlConnectionStringBuilder = sqlConnenctionStringBuilder;
        }

        #region IDisposable Support

        private bool disposedValue; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    SqlConnection.Close();
                    SqlConnection.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable Support
    }

    public class DataSetDemo
    {
        public static void DataSetAddDeleteSelectUpdateDemo(string sqlConnectionString)
        {
            using (var dbConnection = new DataBaseConnection(sqlConnectionString))
            {
                SqlCommand cmd = dbConnection.SqlConnection.CreateCommand();
                cmd.CommandText = "select * from [TestaADO.Net].[dbo].[Table]" + "\r\n" + "select id,name from [TestaADO.Net].[dbo].[Table] ";
                SqlDataAdapter dbAdapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                dbAdapter.Fill(ds);
                ds.ToString();
                // 获取数据：使用索引
                int id = (int) ds.Tables[0].Rows[0][0];
                int id2 = (int) cmd.ExecuteScalar();
                bool ideid2 = id == id2;
                var dt = ds.Tables[0];
                //新列追加：使用 SqlCommandBuilder 类
                DataRow dr = dt.NewRow();
                dr["Id"] = 6;
                dr["Name"] = "hehe";
                dr["Sex"] = "male";
                dr["Birthday"] = dt.Rows[0][3];
                dt.Rows.Add(dr);
                SqlCommandBuilder scb = new SqlCommandBuilder(dbAdapter);
                dbAdapter.Update(dt.GetChanges());
                dt.AcceptChanges();

                //数据修改：使用索引 + SqlCommandBuilder 类
                var dr2 = dt.Rows[3];
                dr2["Name"] = "Hanwen Cui";
                //SqlCommandBuilder scb = new SqlCommandBuilder(dbAdapter);
                dbAdapter.Update(ds.Tables[0].GetChanges());
                dt.AcceptChanges();

                //数据删除： 使用索引 + SqlCommandBuilder 类
                var dr3 = dt.Rows[4];//删掉xi
                dr3.Delete();
                /*注意！：Delete 方法是在 DataTable 中做了一个删除标记，也就是说会出现在 GetChanges 方法的变化中，这个变化可以被更新到 DB 表中。*/
                dt.Rows.RemoveAt(3);
                /* 注意！：RemoveAt 方法是彻底在 DataTable 中删除该列，这一列在表中会不存在。而更新 DB 观察的是数据表的变化，该列已经被删除，所以更新表不会有任何变化。*/
                //SqlCommandBuilder scb = new SqlCommandBuilder(dbAdapter);
                dbAdapter.Update(ds.Tables[0].GetChanges());
                dt.AcceptChanges();
            }
        }

        public static void SqlDataReaderDemo(string sqlConnnectionString)
        {
            using (DataBaseConnection conn = new DataBaseConnection(sqlConnnectionString))
            {
                var sqlCommand = conn.SqlConnection.CreateCommand();
                sqlCommand.CommandText = "select * from [TestaADO.Net].[dbo].[Table]";
                var sqlReader = sqlCommand.ExecuteReader(CommandBehavior.Default);
                while (sqlReader.Read())
                {
                    var row = sqlReader as IDataReader;
                    for (int i = 0; i < row.FieldCount; i++)
                    {
                        Console.Write("Data: {0},", row[i]);
                    }
                    Console.WriteLine(string.Empty);
                }
                sqlReader.Close();
            }
        }

        public static void SqlTransactionDemo(string sqlConnectionString)
        {
            using (DataBaseConnection conn = new DataBaseConnection(sqlConnectionString))
            {
                var sqlTransaction = conn.SqlConnection.BeginTransaction();
                try
                {
                    var sqlCommand = new SqlCommand("UPDATE [dbo].[TestTransaction] set Count = Count - 10 where AccountName = 'User1'", conn.SqlConnection, sqlTransaction);
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand = new SqlCommand("UPDATE [dbo].[TestTransaction] set Count = Count + 10 where AccountName = 'User2'", conn.SqlConnection, sqlTransaction);
                    //throw new Exception();
                    sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception e)
                {
                    sqlTransaction.Rollback();
                    Console.WriteLine(e);
                }
            }
        }

        public static void SqlDataSetToXmlDemo(string sqlConnectionString)
        {
            using (DataBaseConnection conn = new DataBaseConnection(sqlConnectionString))
            {
                SqlCommand cmd = conn.SqlConnection.CreateCommand();
                cmd.CommandText = "select * from [TestaADO.Net].[dbo].[Table]";
                SqlDataAdapter dbAdapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                dbAdapter.Fill(ds);
                using (var ms = new System.IO.MemoryStream())
                {
                    ds.Tables[0].WriteXmlSchema(ms);//输出表结构
                    ds.Tables[0].WriteXml(ms);//输出表内容
                    var array = ms.ToArray();
                    var s = Encoding.UTF8.GetString(array, 0, array.Length);
                }
            }
        }


    }
}