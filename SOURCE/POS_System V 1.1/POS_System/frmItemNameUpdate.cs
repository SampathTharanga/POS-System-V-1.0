using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_System
{
    public partial class frmItemNameUpdate : Form
    {
        static string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);

        string inum = string.Empty, idis = string.Empty;

        private void frmItemNameUpdate_Load(object sender, EventArgs e)
        {
            txtInumber.Text = inum;
            txtDis.Text = idis;
        }

        StockClass ObjStock = new StockClass();
        private void btnRenew_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT Description FROM tbl_Stock WHERE ItemNo = '" + inum + "'", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read() == true)
            {
                ObjStock.UpdateDescription(txtInumber.Text, txtDis.Text);
                MessageBox.Show("Description Updated completed!", "Update message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else con.Close();
        }

        public frmItemNameUpdate(string v1, string v2)
        {
            InitializeComponent();
            inum = v1;
            idis = v2;
        }
    }
}
