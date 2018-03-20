using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POS_System
{
    public partial class frmLogin : Form
    {
        static string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);

        public frmLogin()
        {
            InitializeComponent();
        }

        public static string userNAME = string.Empty;
        UsersClass ObjUser = new UsersClass();

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataReader drm = ObjUser.userLogin(txtUsername.Text, txtPassword.Text);
                if (drm.Read())
                {
                    con.Close();
                    userNAME = txtUsername.Text;
                    this.Hide();
                    frmMain ObjMn = new frmMain();
                    ObjMn.ShowDialog();
                }
                else
                {
                    con.Close();
                    MessageBox.Show("Please enter correct details", "USER ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsername.Clear();
                    txtPassword.Clear();
                    txtUsername.Focus();
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            frmFroget ObjFro = new frmFroget();
            ObjFro.ShowDialog();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SqlDataReader drm = ObjUser.userLogin(txtUsername.Text, txtPassword.Text);
                    if (drm.Read())
                    {
                        con.Close();
                        userNAME = txtUsername.Text;
                        this.Hide();
                        frmMain ObjMn = new frmMain();
                        ObjMn.ShowDialog();
                    }
                    else
                    {
                        con.Close();
                        MessageBox.Show("Please enter correct details", "USER ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUsername.Clear();
                        txtPassword.Clear();
                        txtUsername.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

        }
    }
}
