
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class GetDbConnections
    {
        private static IConfiguration _configuration;
        public  GetDbConnections(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static SqlConnection FnGetSqlConnection_DbPcmBackOffice(IConfiguration configuration)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = configuration.GetConnectionString("Con_PcmBackOffice");
            return sqlConnection;
        }
    }
}
