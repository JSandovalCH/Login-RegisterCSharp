using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Connection
{
    public class CD_Conexion
    {
        private SqlConnection c = new SqlConnection(@"Data Source=DESKTOP-97ANFOC\SQLEXPRESS; Initial Catalog=LoginNCapasBD; Integrated Security=True");

        public SqlConnection OpenConnection()
        {
            if (c.State == ConnectionState.Closed) c.Open();
            return c;
        }
        public SqlConnection CloseConnection()
        {
            if (c.State == ConnectionState.Open) c.Close();
            return c;
        }
    }
}
