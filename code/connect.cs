using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace store_management
{
    class connect
    {
        public SqlCommand cmd = new SqlCommand();
        public SqlConnection cnn = new SqlConnection();
        public connect()
        {
            cnn.ConnectionString = "Data Source=DESKTOP-62VLQNM;Initial Catalog=Management;Integrated Security=true";
            cnn.Open();
            cmd.Connection = cnn;
        }
    }
}
