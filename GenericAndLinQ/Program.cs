namespace Caspar.CSharpTest
{
    internal class Program
    {
#pragma warning disable RECS0154 // Parameter is never used

        private static void Main(string[] args)
#pragma warning restore RECS0154 // Parameter is never used
        {
            LinQToAdoDemo.SetADataTableIntoADataView
                (@"Data Source = (localdb)\ProjectsV13; Initial Catalog = TestaADO.Net; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False"
                );
        }
    }
}