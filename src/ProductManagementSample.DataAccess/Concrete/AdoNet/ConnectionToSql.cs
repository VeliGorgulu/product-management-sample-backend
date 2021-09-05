using ProductManagementSample.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSample.DataAccess.Concrete.AdoNet
{
    public class ConnectionToSql
    {
        public SqlConnection Connect()
        {
            return new SqlConnection(@"Server=(localdb)\mssqllocaldb;Database=ProductManagementDb;Trusted_Connection=true");
        }
    }
}
