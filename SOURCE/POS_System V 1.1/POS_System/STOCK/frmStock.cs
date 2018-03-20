using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace POS_System
{
    public partial class frmStock : Form
    {
        static string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);

        public frmStock()
        {
            InitializeComponent();
        }

        //OBJECT COLLECTIONS
        StockClass ObjStock = new StockClass();

        int iNo;
        private void frmStock_Load(object sender, EventArgs e)
        {
            try
            {
                //USER LEVEL HANDLE
                if (frmLogin.userNAME == "Admin" || frmLogin.userNAME == "admin")
                    btnAdd.Enabled = true;
                else
                    btnAdd.Enabled = false;

                    //ITEMS LOAD FOR DATAGRIDVIEW
                    LoadItemsDataForDgv();


                //ITEM NUMBER AUTO INCREES
                string rNo = ObjStock.ItemNoAuto().ToString();
                if (rNo.Equals("") || rNo == null)
                {
                    iNo = 00001;
                }
                else
                {
                    iNo = int.Parse(rNo);
                    iNo++;
                }
                if (iNo < 10) { lblItemNo.Text = "0000" + Convert.ToString(iNo); }
                else if (10 <= iNo && iNo < 100) { lblItemNo.Text = "000" + Convert.ToString(iNo); }
                else if (100 <= iNo && iNo < 1000) { lblItemNo.Text = "00" + Convert.ToString(iNo); }
                else if (1000 <= iNo && iNo < 10000) { lblItemNo.Text = "0" + Convert.ToString(iNo); }
                else lblItemNo.Text = Convert.ToString(iNo);
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //CLEAR ALL TEXTBOX FUNCTIONS.
        void ClearAllText(Control con)
        {
            foreach (Control c in con.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Clear();
                else
                    ClearAllText(c);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblItemNo.Text != "" && txtQty.Text != "" && int.Parse(txtQty.Text)!=0 && txtUnitPrice.Text != "" &&    decimal.Parse(txtUnitPrice.Text) != 0.00M)
                {
                    //NEW ITEMS ADD IN THE STOCK
                    if (btnAdd.Text == "Add")
                    {
                        //ITEM INSERT TO DATABSE
                        ObjStock.NewItemAdd(lblItemNo.Text, txtDecsription.Text, int.Parse(txtQty.Text), decimal.Parse(txtUnitPrice.Text));
                        MessageBox.Show("New item added successful!", "SUCCESSFUL MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //ITEM NUMBER AUTO INCREESE WITH DISPLAY LABEL.
                        string rNo = ObjStock.ItemNoAuto().ToString();
                        if (rNo.Equals("") || rNo == null)
                        {
                            iNo = 00001;
                        }
                        else
                        {
                            iNo = int.Parse(rNo);
                            iNo++;
                        }
                        if (iNo < 10) { lblItemNo.Text = "0000" + Convert.ToString(iNo); }
                        else if (10 <= iNo && iNo < 100) { lblItemNo.Text = "000" + Convert.ToString(iNo); }
                        else if (100 <= iNo && iNo < 1000) { lblItemNo.Text = "00" + Convert.ToString(iNo); }
                        else if (1000 <= iNo && iNo < 10000) { lblItemNo.Text = "0" + Convert.ToString(iNo); }
                        else lblItemNo.Text = Convert.ToString(iNo);

                        //CLEAR ALL TEXTBOX VALUES
                        ClearAllText(this);

                        //ITEMS LOAD FOR DATAGRIDVIEW
                        LoadItemsDataForDgv();

                        txtDecsription.Focus();
                    }
                    //UPDATE ITEMS IN THE STOCK
                    else if (btnAdd.Text == "Update")
                    {
                        //UPDATE ITEM LEVEL
                        int oldQty = 0;
                        SqlCommand com = new SqlCommand("SELECT Qty FROM tbl_Stock WHERE ItemNo='" + lblItemNo.Text + "'", con);
                        con.Open();
                        SqlDataReader dr = com.ExecuteReader();
                        if (dr.Read() == true)
                        {
                            oldQty = int.Parse(dr["Qty"].ToString());
                            oldQty += int.Parse(txtQty.Text);
                            con.Close();
                        }
                        else
                        {
                            con.Close();
                        }

                        //UPDATE ITEM
                        ObjStock.UpdateItem(lblItemNo.Text, txtDecsription.Text, oldQty, decimal.Parse(txtUnitPrice.Text));
                        MessageBox.Show("Update completed!", "UPDATE MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //CLEAR ALL TEXTBOX VALUES
                        ClearAllText(this);

                        //ITEM NUMBER AUTO INCREESE WITH DISPLAY LABEL.
                        string rNo = ObjStock.ItemNoAuto().ToString();
                        if (rNo.Equals("") || rNo == null)
                        {
                            iNo = 00001;
                        }
                        else
                        {
                            iNo = int.Parse(rNo);
                            iNo++;
                        }
                        if (iNo < 10) { lblItemNo.Text = "0000" + Convert.ToString(iNo); }
                        else if (10 <= iNo && iNo < 100) { lblItemNo.Text = "000" + Convert.ToString(iNo); }
                        else if (100 <= iNo && iNo < 1000) { lblItemNo.Text = "00" + Convert.ToString(iNo); }
                        else if (1000 <= iNo && iNo < 10000) { lblItemNo.Text = "0" + Convert.ToString(iNo); }
                        else lblItemNo.Text = Convert.ToString(iNo);

                        //ITEMS LOAD FOR DATAGRIDVIEW
                        LoadItemsDataForDgv();

                        //BUTTON TEXT CHANGE
                        btnAdd.Text = "Add";
                        btnAdd.TextAlign = ContentAlignment.MiddleCenter;

                        //FOCUS TXTBOX
                        txtDecsription.Focus();
                    }
                    else
                        MessageBox.Show("Have not like this button!", "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else { MessageBox.Show("Item details are not completed!", "NEW ITEM ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //DATAGRIDVIEW FOR ITEMS LOAD.
        private void LoadItemsDataForDgv()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tbl_Stock", con);
            con.Open();
            DataTable dt = new DataTable();// Datagridview  same data repeat. because Datatable object create top of this class file. therefore object create this place.
            da.Fill(dt);
            dgvStock.DataSource = dt;
            con.Close();

            //DATAGRIDVIEW DESIGN SECTION.
            dgvStock.Columns[0].HeaderText = "Item No";
            dgvStock.Columns[1].HeaderText = "Description";
            dgvStock.Columns[2].HeaderText = "Qty";
            dgvStock.Columns[3].HeaderText = "Unit Price";

            dgvStock.Columns[0].Width = 100;
            dgvStock.Columns[1].Width = 478;
            dgvStock.Columns[2].Width = 100;
            dgvStock.Columns[3].Width = 100;

            dgvStock.BorderStyle = BorderStyle.None;
            dgvStock.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvStock.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvStock.DefaultCellStyle.SelectionBackColor = Color.RosyBrown;
            dgvStock.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvStock.BackgroundColor = Color.White;

            dgvStock.EnableHeadersVisualStyles = false;
            dgvStock.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvStock.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvStock.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgvStock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStock.MultiSelect = false;

            dgvStock.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvStock.AllowUserToResizeRows = false;

            dgvStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvStock.AllowUserToResizeColumns = false;
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 127)
                e.Handled = true;
        }

        private void txtUnitPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 127 && e.KeyChar != 46)
                e.Handled = true;

            //Ensure validation for allow one "." value
            if (!char.IsControl(e.KeyChar))
            {
                int dotIndex = txtUnitPrice.Text.IndexOf('.');
                if (char.IsDigit(e.KeyChar))
                {
                    if (dotIndex != -1 && dotIndex < txtUnitPrice.SelectionStart && txtUnitPrice.Text.Substring(dotIndex + 1).Length >= 2)
                    {
                        e.Handled = true;
                    }
                }
                else
                    e.Handled = e.KeyChar != '.' || dotIndex != -1 || txtUnitPrice.Text.Length == 0 || txtUnitPrice.SelectionStart + 2 < txtUnitPrice.Text.Length;
            }
        }

        private void dgvStock_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                //Update item data
                if (dgvStock.CurrentRow.Index != -1)
                {
                    lblItemNo.Text = dgvStock.CurrentRow.Cells[0].Value.ToString();
                    txtDecsription.Text = dgvStock.CurrentRow.Cells[1].Value.ToString();
                    txtQty.Text = dgvStock.CurrentRow.Cells[2].Value.ToString();
                    txtUnitPrice.Text = dgvStock.CurrentRow.Cells[3].Value.ToString();

                    btnAdd.Text = "Update";
                    btnAdd.TextAlign = ContentAlignment.MiddleCenter;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private void txtSearchItem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //ITEM SEARCH
                string item_Filter = "SELECT * FROM tbl_Stock";
                SqlDataAdapter da = new SqlDataAdapter(item_Filter, con);
                con.Open();
                DataTable dtbl = new DataTable();
                da.Fill(dtbl);
                con.Close();
                BindingSource bnsue = new BindingSource();
                bnsue.DataSource = dtbl;
                dgvStock.DataSource = bnsue;
                da.Update(dtbl);


                DataView dv = new DataView(dtbl);
                dv.RowFilter = "ItemNo LIKE '%" + txtSearchItem.Text + "%' ";
                dgvStock.DataSource = dv;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }


        }

        private void txtSearchItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 127)
                e.Handled = true;
        }

        private void txtDecsription_Click(object sender, EventArgs e)
        {
            txtSearchItem.Clear();
        }

        private void txtQty_Click(object sender, EventArgs e)
        {
            txtSearchItem.Clear();
        }

        private void txtUnitPrice_Click(object sender, EventArgs e)
        {
            txtSearchItem.Clear();
        }

        private void picCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picHelp_Click(object sender, EventArgs e)
        {
            frmHelp frmHlp = new frmHelp();
            frmHlp.ShowDialog();
        }

        private void picAoubt_Click(object sender, EventArgs e)
        {
            frmAbout frmAbut = new frmAbout();
            frmAbut.ShowDialog();
        }


        //Mouse right click event selected
        private void dgvStock_MouseClick(object sender, MouseEventArgs e)
        {
            string inumber = string.Empty, idiscrip = string.Empty;
            if (e.Button == MouseButtons.Right)
            {
                if (dgvStock.CurrentRow.Index != -1)
                {
                    inumber = dgvStock.CurrentRow.Cells[0].Value.ToString();
                    idiscrip = dgvStock.CurrentRow.Cells[1].Value.ToString();


                    frmItemNameUpdate ObjInuf = new frmItemNameUpdate(inumber, idiscrip);
                    ObjInuf.ShowDialog();
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadItemsDataForDgv();
        }
    }
}
