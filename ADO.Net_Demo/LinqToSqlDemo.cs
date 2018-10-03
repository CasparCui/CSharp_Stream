using Demo;
using System.Linq;

namespace Caspar.CSharpTest
{
    public class LinqToSqlDemo
    {
        public static void DoLinqToSqlDemo()
        {
            var context = new DataClasses1DataContext();
            var dataRow = from data in context.Table
                          select data;
        }

        public static void DoUpdateSQLbyLinQDemo()
        {
            var context = new DataClasses1DataContext();
            var dataRow = from data in context.Table
                          where data.ID == 2
                          select data;
            foreach (var data in dataRow)
            {
                data.Name = "Chuyin Weilai";
            }

            context.SubmitChanges(System.Data.Linq.ConflictMode.FailOnFirstConflict);
        }
    }
}