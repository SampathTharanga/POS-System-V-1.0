using System;
using System.Data.SqlClient;


namespace POS_System
{
    class InvoiceClass
    {
        static string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);

        string itemNo = "";
        //MAX ITEM NUMBER
        public string MaxItemNo()
        {
            SqlCommand com = new SqlCommand("SELECT MAX(ItemNo) FROM tbl_Stock", con);
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read() == true)
            {
                itemNo = dr[0].ToString();
            }
            con.Close();
            return itemNo;
        }

        //MAX INVOICE NO SELECT
        string tblInvo = "";
        public string SelectMaxInvoNo()
        {
            SqlCommand com = new SqlCommand("SELECT MAX(InvoiceNo) FROM tbl_Invoice", con);
            con.Open();
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read() == true)
            {
                tblInvo = dr[0].ToString();
            }
            con.Close();
            return tblInvo;
        }

        //SELECT ITEMS ADD BILL ITEMS TABLE
        public void SelectedItemAdd(string _invoNo, string _itemNo, string descrip, int _qty, decimal _unitPrice, decimal _amount)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO tbl_Bill_Items VALUES('" + _invoNo + "','" + _itemNo + "','" + descrip + "','" + _qty + "','" + _unitPrice + "','" + _amount + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        ////SELECT NO OF QTY
        //public SqlDataReader NoOfQty(string _itemNo)
        //{
        //    SqlCommand com = new SqlCommand("SELECT Qty FROM tbl_Stock WHERE ItemNo='" + _itemNo + "'", con);
        //    con.Open();
        //    SqlDataReader dr = com.ExecuteReader();
        //    return dr;
        //}

        //NEW INVOICE
        public void NewInvoce(string _invoNo, DateTime _date, string _seller, decimal _totPrice, int _discount, decimal _payment, decimal _balance, int _noOfQty, decimal _payableTot)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO tbl_Invoice VALUES('" + _invoNo + "','" + _date + "','" + _seller + "','" + _totPrice + "','" + _discount + "','" + _payment + "','" + _balance + "','" + _noOfQty + "','" + _payableTot + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        //NEW INVOICE TEMP
        public void NewInvoceBill(string _invoNo, DateTime _date, string _seller, decimal _totPrice, int _discount, decimal _payment, decimal _balance, int _noOfQty, decimal _payableTot)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO tbl_Bill_Invoice VALUES('" + _invoNo + "','" + _date + "','" + _seller + "','" + _totPrice + "','" + _discount + "','" + _payment + "','" + _balance + "','" + _noOfQty + "','" + _payableTot + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        //ITEM BUY AFTER UPDATE STOCK
        public void update_Quntity(string _itemNo, int _qty)
        {
            string sql = "UPDATE tbl_Stock SET Qty='" + _qty + "' WHERE ItemNo='" + _itemNo + "'";
            SqlCommand com = new SqlCommand(sql, con);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
        }

        ////SELECT HOW MANY QTY IN THE STOCK
        //public SqlDataReader SelectNoOFQty(string _itemNo)
        //{
        //    SqlCommand com = new SqlCommand("SELECT noOfQty FROM tbl_Item WHERE itemNo='" + _itemNo + "'", con);
        //    con.Open();
        //    SqlDataReader dr = com.ExecuteReader();
        //    return dr;
        //}
    }
}
