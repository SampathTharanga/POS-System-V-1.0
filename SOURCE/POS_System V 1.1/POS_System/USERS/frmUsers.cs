using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace POS_System
{
    public partial class frmUsers : Form
    {
        static string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);

        public frmUsers()
        {
            InitializeComponent();
        }

        UsersClass ObjUser = new UsersClass();

        //DATAGRIDVIEW LOAD.
        private void LoadDataForDgv()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbl_Users", con);
                con.Open();
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvUsers.DataSource = dt;
                con.Close();

                //DATAGRIDVIEW DESIGN SECTION.
                dgvUsers.Columns[0].HeaderText = "Username";
                dgvUsers.Columns[1].HeaderText = "Password";
                dgvUsers.Columns[2].HeaderText = "Security Question";
                dgvUsers.Columns[3].HeaderText = "Security Answer";

                dgvUsers.Columns[0].Width = 100;
                dgvUsers.Columns[1].Width = 150;
                dgvUsers.Columns[2].Width = 350;
                dgvUsers.Columns[3].Width = 200;

                dgvUsers.BorderStyle = BorderStyle.None;
                dgvUsers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dgvUsers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvUsers.DefaultCellStyle.SelectionBackColor = Color.RosyBrown;
                dgvUsers.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dgvUsers.BackgroundColor = Color.White;

                dgvUsers.EnableHeadersVisualStyles = false;
                dgvUsers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvUsers.MultiSelect = false;

                dgvUsers.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                dgvUsers.AllowUserToResizeRows = false;

                dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dgvUsers.AllowUserToResizeColumns = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void frmUsers_Load(object sender, EventArgs e)
        {
            txtUsername.Enabled = true;
            btnRegister.Enabled = true;
            BtnUpdate.Enabled = false;

            LoadDataForDgv();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text != "" && txtPassword.Text != "" && cbxSecQue.Text != "" && txtSecAns.Text != "")
                {
                    SqlCommand com = new SqlCommand("SELECT UserName FROM tbl_Users WHERE UserName='" + txtUsername.Text + "'", con);
                    con.Open();
                    SqlDataReader dr = com.ExecuteReader();
                    if (!dr.Read() == true)
                    {
                        con.Close();
                        ObjUser.userRegister(txtUsername.Text, txtPassword.Text, cbxSecQue.Text, txtSecAns.Text);
                        MessageBox.Show("User register successfully.", "REGISTER SUCCESSFULLY", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadDataForDgv();

                        txtSecAns.Clear();
                        txtPassword.Clear();
                        txtUsername.Clear();
                        cbxSecQue.SelectedIndex = -1;
                    }
                    else
                    {
                        con.Close();
                        MessageBox.Show("This username is exist!", "USER ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else { MessageBox.Show("Can not be empty!.", "DETAILS ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text != "" && txtPassword.Text != "" && cbxSecQue.Text != "" && txtSecAns.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("UPDATE tbl_Users SET Password='" + txtPassword.Text + "', SecQuestion='" + cbxSecQue.Text + "', SecAnswer='" + txtSecAns.Text + "' WHERE UserName='" + txtUsername.Text + "'", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Update completed!", "USER UPDATE", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadDataForDgv();

                    txtSecAns.Clear();
                    txtPassword.Clear();
                    txtUsername.Clear();
                    cbxSecQue.SelectedIndex = -1;
                }
                else { MessageBox.Show("Please enter details", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private void dgvUsers_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                txtUsername.Enabled = false;
                btnRegister.Enabled = false;
                BtnUpdate.Enabled = true;

                if (dgvUsers.CurrentRow.Index != -1)
                {
                    txtUsername.Text = dgvUsers.CurrentRow.Cells[0].Value.ToString();
                    txtPassword.Text = dgvUsers.CurrentRow.Cells[1].Value.ToString();
                    cbxSecQue.Text = dgvUsers.CurrentRow.Cells[2].Value.ToString();
                    txtSecAns.Text = dgvUsers.CurrentRow.Cells[3].Value.ToString();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtUsername.Enabled = true;
            btnRegister.Enabled = true;
            BtnUpdate.Enabled = false;

            txtSecAns.Clear();
            txtPassword.Clear();
            txtUsername.Clear();
            cbxSecQue.SelectedIndex = -1;
        }

        private void picBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picHelp_Click(object sender, EventArgs e)
        {
            frmHelp frmHlp = new frmHelp();
            frmHlp.ShowDialog();
        }

        private void picAbout_Click(object sender, EventArgs e)
        {
            frmAbout frmAbut = new frmAbout();
            frmAbut.ShowDialog();
        }
    }
}
