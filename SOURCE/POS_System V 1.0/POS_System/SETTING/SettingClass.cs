using System.Data.SqlClient;

namespace POS_System
{
    class SettingClass
    {
        static string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        public void update_Company(int _id, string _name)
        {
            SqlCommand com = new SqlCommand("UPDATE tbl_Company SET Name='" + _name + "' WHERE ID='" + _id + "'", con);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
        }
    }
}
