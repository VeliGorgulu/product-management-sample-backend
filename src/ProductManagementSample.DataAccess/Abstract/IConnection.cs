using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSample.DataAccess.Abstract
{
    interface IConnection
    {
        SqlConnection Connect();
    }
}
