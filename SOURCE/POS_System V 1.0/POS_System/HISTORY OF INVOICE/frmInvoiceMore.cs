using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace POS_System
{
    public partial class frmInvoiceMore : Form
    {
        static string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);

        public frmInvoiceMore(string invoVal)
        {
            InitializeComponent();
            invoNo = invoVal;
        }

        string invoNo = string.Empty;

        private void LoadDataForDgv()
        {
            try
            {
                //DATAGRIDVIEW DESIGN SECTION.
                dgvMore.Columns[0].HeaderText = "Item No";
                dgvMore.Columns[1].HeaderText = "Description";
                dgvMore.Columns[2].HeaderText = "Qty";
                dgvMore.Columns[3].HeaderText = "Unit Price";
                dgvMore.Columns[4].HeaderText = "Amount";

                dgvMore.Columns[0].Width = 100;
                dgvMore.Columns[1].Width = 250;
                dgvMore.Columns[2].Width = 100;
                dgvMore.Columns[3].Width = 100;
                dgvMore.Columns[4].Width = 100;

                dgvMore.BorderStyle = BorderStyle.None;
                dgvMore.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dgvMore.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvMore.DefaultCellStyle.SelectionBackColor = Color.RosyBrown;
                dgvMore.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dgvMore.BackgroundColor = Color.White;

                dgvMore.EnableHeadersVisualStyles = false;
                dgvMore.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvMore.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgvMore.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                dgvMore.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvMore.MultiSelect = false;

                dgvMore.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                dgvMore.AllowUserToResizeRows = false;

                dgvMore.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dgvMore.AllowUserToResizeColumns = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void frmInvoiceMore_Load(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT ItemNo,Description,Qty,UnitPrice,Amount FROM tbl_InvoiceItems WHERE InvoiceNo ='" + invoNo + "'", con);
                con.Open();
                DataTable dt = new DataTable();// Datagridview  same data repeat. because Datatable object create top of this class file. therefore object create this place.
                da.Fill(dt);
                dgvMore.DataSource = dt;
                con.Close();

                LoadDataForDgv();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }
    }
}
