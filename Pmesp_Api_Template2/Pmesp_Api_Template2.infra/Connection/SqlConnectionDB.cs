using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pmesp_Api_Template2.infra.Connection
{
    public class SqlConnectionDB
    {
        public IDbConnection ConnectionDB(string conn)
        {
            return new SqlConnection(conn);
        }
    }
}
