using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace POS_System
{
    public partial class frmHistory : Form
    {
        static string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        public frmHistory()
        {
            InitializeComponent();
        }

        private void LoadDataForDgv()
        {
            try
            {
                //DATAGRIDVIEW DESIGN SECTION.
                dgvInvoView.Columns[0].HeaderText = "Invoice No";
                dgvInvoView.Columns[1].HeaderText = "Date";
                dgvInvoView.Columns[2].HeaderText = "Seller";
                dgvInvoView.Columns[3].HeaderText = "Bill Total";
                dgvInvoView.Columns[4].HeaderText = "Discount";
                dgvInvoView.Columns[5].HeaderText = "Payment";
                dgvInvoView.Columns[6].HeaderText = "Balance";
                dgvInvoView.Columns[7].HeaderText = "No Of Qty";
                dgvInvoView.Columns[8].HeaderText = "Payable Total";

                dgvInvoView.Columns[0].Width = 90;
                dgvInvoView.Columns[1].Width = 90;
                dgvInvoView.Columns[2].Width = 80;
                dgvInvoView.Columns[3].Width = 90;
                dgvInvoView.Columns[4].Width = 70;
                dgvInvoView.Columns[5].Width = 90;
                dgvInvoView.Columns[6].Width = 90;
                dgvInvoView.Columns[7].Width = 90;
                dgvInvoView.Columns[8].Width = 100;

                dgvInvoView.BorderStyle = BorderStyle.None;
                dgvInvoView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dgvInvoView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvInvoView.DefaultCellStyle.SelectionBackColor = Color.RosyBrown;
                dgvInvoView.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dgvInvoView.BackgroundColor = Color.White;

                dgvInvoView.EnableHeadersVisualStyles = false;
                dgvInvoView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvInvoView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgvInvoView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                dgvInvoView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvInvoView.MultiSelect = false;

                dgvInvoView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                dgvInvoView.AllowUserToResizeRows = false;

                dgvInvoView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dgvInvoView.AllowUserToResizeColumns = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void frmHistory_Load(object sender, EventArgs e)
        {
            try
            {
                txtInvoNo.Focus();

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbl_Invoice", con);
                con.Open();
                DataTable dt = new DataTable();// Datagridview  same data repeat. because Datatable object create top of this class file. therefore object create this place.
                da.Fill(dt);
                dgvInvoView.DataSource = dt;
                con.Close();

                LoadDataForDgv();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private void txtInvoNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if ((txtInvoNo.Text != ""))
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbl_Invoice", con);
                    con.Open();
                    DataTable dtbl = new DataTable();
                    da.Fill(dtbl);
                    con.Close();
                    BindingSource bnsue = new BindingSource();
                    bnsue.DataSource = dtbl;
                    dgvInvoView.DataSource = bnsue;
                    da.Update(dtbl);

                    DataView dv = new DataView(dtbl);
                    dv.RowFilter = "InvoiceNo LIKE '%" + txtInvoNo.Text + "%' ";
                    dgvInvoView.DataSource = dv;

                    LoadDataForDgv();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private void dgvInvoView_DoubleClick(object sender, EventArgs e)
        {
            frmInvoiceMore frmMor = new frmInvoiceMore(dgvInvoView.CurrentRow.Cells[0].Value.ToString());
            frmMor.ShowDialog();
        }

        private void txtInvoNo_Click(object sender, EventArgs e)
        {
            try
            {
                txtInvoNo.Clear();

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbl_Invoice", con);
                DataTable dt = new DataTable();// Datagridview  same data repeat. because Datatable object create top of this class file. therefore object create this place.
                da.Fill(dt);
                dgvInvoView.DataSource = dt;
                con.Close();

                LoadDataForDgv();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void picBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picStock_Click(object sender, EventArgs e)
        {
            frmStock frmSto = new frmStock();
            frmSto.ShowDialog();
        }

        private void picHelp_Click(object sender, EventArgs e)
        {
            frmHelp frmHlp = new frmHelp();
            frmHlp.ShowDialog();
        }

        private void picAbout_Click(object sender, EventArgs e)
        {
            frmAbout frmAbut = new frmAbout();
            frmAbut.Show();
        }

        private void txtInvoNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 127)
                e.Handled = true;
        }

        private void btnSellItems_Click(object sender, EventArgs e)
        {
            frmSellItems frmSellItm = new frmSellItems();
            frmSellItm.ShowDialog();
        }
    }
}
