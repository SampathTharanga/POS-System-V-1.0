using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System
{
    class StockClass
    {
        static string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);

        //ITEM NUMBER AUTO INCREESE FUNCTION
        string ItemNo;
        public string ItemNoAuto()
        {
            SqlCommand com = new SqlCommand("SELECT MAX(ItemNo) FROM tbl_Stock", con);
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            if(dr.Read() == true)
            {
                ItemNo = dr[0].ToString();//CAN USE ONLY 0 INDEX.
            }
            con.Close();
            return ItemNo;
        }

        //NEW ITEM ADD
        public void NewItemAdd(string _itNo, string _descrip, int _qty, decimal _unitPrice)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO tbl_Stock VALUES('" + _itNo + "','" + _descrip + "','" + _qty + "','" + _unitPrice + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        //ITEMS UPDATE
        public void UpdateItem(string _itemNo, string _descrip, int _qty, decimal _unitPrice)
        {
            SqlCommand cmd = new SqlCommand("UPDATE tbl_Stock SET Description='" + _descrip + "', Qty='" + _qty + "', UnitPrice='" + _unitPrice + "' WHERE ItemNo='" + _itemNo + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
