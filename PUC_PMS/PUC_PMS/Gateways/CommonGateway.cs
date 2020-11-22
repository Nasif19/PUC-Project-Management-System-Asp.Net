using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace PUC_PMS.Gateways
{
    public class CommonGateway
    {
        private string connectionString = "Data Source=localhost;Initial Catalog=PUDB;Integrated Security=True";

        public SqlConnection connection { get; set; }

        public SqlDataReader Reader { get; set; }

        public SqlCommand Command { get; set; }

        public CommonGateway()
        {
            connection = new SqlConnection(connectionString);
        }
    }
}