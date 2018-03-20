using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System
{
    class ConnectionDB
    {
        public static SqlConnection connection()
        {
            //string connectionString = @"Data Source=DESKTOP-QANEGN2;Initial Catalog=pos_system;Integrated Security=True";
            //string connectionString = @"Server = .\SQLEXPRESS; Database = abhimana_db; User Id = Abhimana; Password = Abhimna@123;Connect Timeout=45; pooling=false";
            //string connectionString = @"Server = .\; Database = abhimana_db; User Id = test2; Password = test2;";


            string ConnectionString = @"Server = .\; Database = pos_system; User Id = genesip; Password = genesip;";
            //string ConnectionString = @"Server = .\; Database = pos_system;";


            //string connectionString = @"Data Source=DESKTOP-QANEGN2;Initial Catalog=pos_system;Integrated Security=True";
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            return con;
        }
    }
}
