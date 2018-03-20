using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;

namespace POS_System
{
    public partial class frmSellItems : Form
    {
        static string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);

        public frmSellItems()
        {
            InitializeComponent();
        }

        InvoiceClass ObjInvo = new InvoiceClass();

        private void LoadDataForDgv()
        {
            try
            {
                //DATAGRIDVIEW DESIGN SECTION.
                dgvSellItems.Columns[0].HeaderText = "Item No";
                dgvSellItems.Columns[1].HeaderText = "Description";
                dgvSellItems.Columns[2].HeaderText = "Total Qty";

                dgvSellItems.Columns[0].Width = 100;
                dgvSellItems.Columns[1].Width = 295;
                dgvSellItems.Columns[2].Width = 100;

                dgvSellItems.BorderStyle = BorderStyle.None;
                dgvSellItems.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dgvSellItems.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvSellItems.DefaultCellStyle.SelectionBackColor = Color.RosyBrown;
                dgvSellItems.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dgvSellItems.BackgroundColor = Color.White;

                dgvSellItems.EnableHeadersVisualStyles = false;
                dgvSellItems.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvSellItems.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgvSellItems.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                dgvSellItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvSellItems.MultiSelect = false;

                dgvSellItems.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                dgvSellItems.AllowUserToResizeRows = false;

                dgvSellItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dgvSellItems.AllowUserToResizeColumns = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void frmSellItems_Load(object sender, EventArgs e)
        {
            try
            {
                //CLEAR tbl_TempSellItems TABLE
                
                SqlCommand cmdBillItems = new SqlCommand("TRUNCATE TABLE " + "tbl_TempSellItems", con);
                con.Open();
                cmdBillItems.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                btnView.Enabled = false;
                
                //CLEAR tbl_TempSellItems TABLE
                SqlCommand cmdBillItems = new SqlCommand("TRUNCATE TABLE " + "tbl_TempSellItems", con);
                con.Open();
                cmdBillItems.ExecuteNonQuery();
                con.Close();

                //SELECTED INVOICE NO DATE RANGE
                SqlDataAdapter dadp = new SqlDataAdapter("SELECT InvoiceNo FROM tbl_Invoice WHERE Date BETWEEN '" + dtpFrom.Value + "' AND '" + dtpTo.Value + "'", con);
                con.Open();
                DataTable dt = new DataTable();
                dadp.Fill(dt);
                con.Close();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int oldTotQty = 0, newTotQty = 0;
                    string selectedItemNo = string.Empty, selectedInvoNo = string.Empty;

                    selectedInvoNo = dt.Rows[i]["InvoiceNo"].ToString();

                    //SELECT ITEMS THAT INVOICE NO
                    SqlDataAdapter daItems = new SqlDataAdapter("SELECT * FROM tbl_InvoiceItems WHERE InvoiceNo='" + selectedInvoNo + "'", con);
                    con.Open();
                    DataTable dtItems = new DataTable();
                    daItems.Fill(dtItems);
                    con.Close();

                    for (int x = 0; x < dtItems.Rows.Count; x++)
                    {
                        selectedItemNo = dtItems.Rows[x]["ItemNo"].ToString();

                        //CHECK ITEM EXIST IN THE tbl_TempSellItems
                        SqlCommand com = new SqlCommand("SELECT * FROM tbl_TempSellItems WHERE ItemNo='" + selectedItemNo + "'", con);
                        con.Open();
                        SqlDataReader dr = com.ExecuteReader();

                        //EXIST
                        if (dr.Read() == true)
                        {
                            oldTotQty = int.Parse(dr["TotalQty"].ToString());
                            newTotQty = oldTotQty + int.Parse(dtItems.Rows[x]["Qty"].ToString());
                            con.Close();

                            
                            SqlCommand cmd1 = new SqlCommand("UPDATE tbl_TempSellItems SET TotalQty='" + newTotQty + "' WHERE ItemNo='" + selectedItemNo + "'", con);
                            con.Open();
                            cmd1.ExecuteNonQuery();
                            con.Close();
                        }
                        //NOT EXIST
                        else
                        {
                            con.Close();
                            SqlCommand cmd2 = new SqlCommand(@"INSERT INTO tbl_TempSellItems VALUES('" + dtItems.Rows[x]["ItemNo"].ToString() + "','" + dtItems.Rows[x]["Description"].ToString() + "','" + int.Parse(dtItems.Rows[x]["Qty"].ToString()) + "')", con);
                            con.Open();
                            cmd2.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }

                //TABLE VALUE LOAD DATAGRIDVIEW
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbl_TempSellItems", con);
                con.Open();
                DataTable dtl = new DataTable();// Datagridview  same data repeat. because Datatable object create top of this class file. therefore object create this place.
                da.Fill(dtl);
                dgvSellItems.DataSource = dtl;
                con.Close();

                LoadDataForDgv();

                btnView.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
