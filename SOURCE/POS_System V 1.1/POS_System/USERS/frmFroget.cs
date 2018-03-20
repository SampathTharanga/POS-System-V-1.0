using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POS_System
{
    public partial class frmFroget : Form
    {
        static string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);

        public frmFroget()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRecover_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text != "" && txtAnswer.Text != "" && cbxSecQue.Text != "")
                {
                    SqlCommand com = new SqlCommand("SELECT Password FROM tbl_Users WHERE UserName='" + txtUsername.Text + "' AND SecQuestion='" + cbxSecQue.Text + "' AND SecAnswer='" + txtAnswer.Text + "'", con);
                    con.Open();
                    SqlDataReader dr = com.ExecuteReader();
                    if (dr.Read())
                    {
                        MessageBox.Show("Your password is \n \"" + dr["password"].ToString() + "\"", "PASSWORD RECOVERY", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                        this.Close();
                    }
                    else
                    {
                        con.Close();
                        MessageBox.Show("Please enter corrrect details.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUsername.Clear();
                        txtAnswer.Clear();
                        cbxSecQue.SelectedIndex = -1;//CLEAR COMBO BOX SELECTED VALUE
                    }
                }
                else { MessageBox.Show("Please enter detail", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }
    }
}
