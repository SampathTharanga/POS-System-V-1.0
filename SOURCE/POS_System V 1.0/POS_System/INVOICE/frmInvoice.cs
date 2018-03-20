using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace POS_System
{
    public partial class frmInvoice : Form
    {
        static string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);

        public frmInvoice()
        {
            InitializeComponent();
        }

        decimal totalPrice = 0.00M, newTot = 0.00M, disc = 0.00M, balance = 0.00M;
        int iNo = 0, totQty = 0;
        InvoiceClass ObjInvoice = new InvoiceClass();

        //CALCULATE TOTAL PRICE
        public void TotalPrice()
        {
            try
            {
                totalPrice = 0.00M;
                //int totQty = 0;
                decimal unitPrice = 0.00M, qty = 0.00M;
                for (int i = 0; i < dgvInvoice.Rows.Count; ++i)
                {
                    unitPrice = Convert.ToDecimal(dgvInvoice.Rows[i].Cells["UnitPrice"].Value.ToString());
                    qty = Convert.ToDecimal(dgvInvoice.Rows[i].Cells["Qty"].Value.ToString());
                    totalPrice += qty * unitPrice;
                    //totQty += int.Parse(dgvInvoice.Rows[i].Cells["Qty"].Value.ToString());
                }
                lblTotalPrice.Text = "Rs. " + totalPrice.ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        //DATAGRIDVIEW LOAD.
        private void LoadDataForDgv()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT ItemNo,Description,Qty,UnitPrice FROM tbl_Bill_Items", con);
                con.Open();
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvInvoice.DataSource = dt;
                con.Close();

                //DATAGRIDVIEW DESIGN SECTION.
                dgvInvoice.Columns[0].HeaderText = "Item No";
                dgvInvoice.Columns[1].HeaderText = "Description";
                dgvInvoice.Columns[2].HeaderText = "Qty";
                dgvInvoice.Columns[3].HeaderText = "Unit Price";

                dgvInvoice.Columns[0].Width = 100;
                dgvInvoice.Columns[1].Width = 355;
                dgvInvoice.Columns[2].Width = 100;
                dgvInvoice.Columns[3].Width = 100;

                dgvInvoice.BorderStyle = BorderStyle.None;
                dgvInvoice.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dgvInvoice.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvInvoice.DefaultCellStyle.SelectionBackColor = Color.RosyBrown;
                dgvInvoice.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dgvInvoice.BackgroundColor = Color.White;

                dgvInvoice.EnableHeadersVisualStyles = false;
                dgvInvoice.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvInvoice.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgvInvoice.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                dgvInvoice.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvInvoice.MultiSelect = false;

                dgvInvoice.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                dgvInvoice.AllowUserToResizeRows = false;

                dgvInvoice.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dgvInvoice.AllowUserToResizeColumns = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void frmInvoice_Load(object sender, EventArgs e)
        {
            try
            {
                //USERNAME
                lblSeller.Text = frmLogin.userNAME;

                //CLEAR ALL BILL ITEMS
                SqlCommand cmdBillItems = new SqlCommand("TRUNCATE TABLE " + "tbl_Bill_Items", con);
                con.Open();
                cmdBillItems.ExecuteNonQuery();
                con.Close();

                //CLEAR ALL BILL ITEMS
                SqlCommand cmdBillInvo = new SqlCommand("TRUNCATE TABLE " + "tbl_Bill_Invoice", con);
                con.Open();
                cmdBillInvo.ExecuteNonQuery();
                con.Close();

                //INVOICE NUMBER AUTO INCREES
                string rNo = ObjInvoice.SelectMaxInvoNo().ToString();
                if (rNo.Equals("") || rNo == null)
                {
                    iNo = 00001;
                }
                else
                {
                    iNo = int.Parse(rNo);
                    iNo++;
                }
                if (iNo < 10) { lblInvoiceNo.Text = "0000" + Convert.ToString(iNo); }
                else if (10 <= iNo && iNo < 100) { lblInvoiceNo.Text = "000" + Convert.ToString(iNo); }
                else if (100 <= iNo && iNo < 1000) { lblInvoiceNo.Text = "00" + Convert.ToString(iNo); }
                else if (1000 <= iNo && iNo < 10000) { lblInvoiceNo.Text = "0" + Convert.ToString(iNo); }
                else lblInvoiceNo.Text = Convert.ToString(iNo);

                //DATAGRIDVIEW LOAD
                LoadDataForDgv();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmStock ObjStock = new frmStock();
            ObjStock.ShowDialog();
        }

        //PAYMENT METHOD
        public void btnPayMethod()
        {
            try
            {
                if (dgvInvoice.Rows.Count >= 0)
                {

                    //TOTAL ITEMS
                    totQty = dgvInvoice.Rows.Count;
                    lblTotItemsDisplay.Text = "Total Items : " + totQty;

                    if (frmPay.paymentVal != null && frmPay.paymentVal != "")
                    {
                        //PAYMENT FORM VALUE GET
                        lblPayment.Text = "Rs. " + decimal.Parse(frmPay.paymentVal).ToString("#.00");
                        lblDiscDisplay.Text = "Discount : " + frmPay.discountVal + "%";

                        //NEW TOTAL PRICE CALCULATE
                        disc = totalPrice * (decimal.Parse(frmPay.discountVal) / 100);
                        newTot = totalPrice - disc;
                        balance = decimal.Parse(frmPay.paymentVal) - newTot;
                        lblBalance.Text = "Rs. " + balance.ToString("0.00");

                        /*
                         * How do I round a decimal value to 2 decimal places (for output on a page)
                         * 
                         * decimalVar.ToString ("#.##"); // returns "" when decimalVar == 0
                         * decimalVar.ToString ("0.##"); // returns "0"  when decimalVar == 0
                         *decimalVar.ToString ("#.00");  100 -100 = 0 --> .00
                         * decimalVar.ToString ("0.00"); 100 -100 = 0 --> 0.00
                         * 
                         */

                        lblNewTotPrice.Text = "Payable Total : Rs. " + newTot.ToString("#.00");
                    }
                }
                else { MessageBox.Show("Qty must be greater than zero!", "STOCK LEVEL", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (dgvInvoice.Rows.Count > 0)
            {
                frmPay ObjPay = new frmPay(totalPrice.ToString());
                ObjPay.ShowDialog();

                btnSell.Focus();

                //PAYMENT METHOD
                btnPayMethod();
            }
        }

        string description = "";
        //decimal UnitQtSellPrice = 0.00M;
        private void txtItemNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //string UnitSellPrice1 = "";
                SqlCommand cmd = new SqlCommand("SELECT Description,Qty FROM tbl_Stock WHERE ItemNo='" + txtItemNo.Text + "'", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read() == true)
                {
                    description = dr["Description"].ToString();// This one can use only 0 inedex -->  dr[0].ToString()
                    //UnitSellPrice1 = dr["Qty"].ToString();
                    //UnitQtSellPrice = Convert.ToDecimal(UnitSellPrice1);
                    con.Close();
                    txtDescription.Text = description;
                    txtQty.Enabled = true;
                }
                else
                {
                    con.Close();
                    txtDescription.Clear();
                    txtQty.Clear();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        public void CheckAvailableItem()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_Stock WHERE ItemNo='" + txtItemNo.Text + "'", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read() == true)
                {
                    con.Close();
                    txtQty.Focus();
                }
                else
                {
                    con.Close();
                    MessageBox.Show("Item not exist in the database!", "STOCK MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtItemNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                //CHECK AVAILABLE ITEM IN THE STOCK
                CheckAvailableItem();
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

        public void SelectedItemsDetails()
        {
            try
            {
                //SELECTED ITEM DETAILS GET
                string UnitSellPrice1 = "";
                decimal UniSellPrice = 0.00M;
                int itemQty = 0;

                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_Stock WHERE ItemNo='" + txtItemNo.Text + "'", con);
                con.Open();
                SqlDataReader dr1 = cmd.ExecuteReader();
                if (dr1.Read() == true)
                {
                    SqlCommand cmd2 = new SqlCommand("SELECT * FROM tbl_Bill_Items WHERE ItemNo='" + txtItemNo.Text + "'", con);
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    if (!dr2.Read() == true)
                    {
                        itemQty = int.Parse(dr1["Qty"].ToString());
                        if (int.Parse(txtQty.Text) != 0)
                        {
                            if (int.Parse(txtQty.Text) <= itemQty)
                            {
                                
                                description = dr1["Description"].ToString();// This one can use only 0 inedex -->  dr[0].ToString()

                                UnitSellPrice1 = dr1["UnitPrice"].ToString();
                                UniSellPrice = Convert.ToDecimal(UnitSellPrice1);
                                con.Close();

                                //SELECTED ITEMS ADD NEW INVOICE ITEMS TABLE tbl_Bill_Items
                                ObjInvoice.SelectedItemAdd(lblInvoiceNo.Text, txtItemNo.Text, description, int.Parse(txtQty.Text), UniSellPrice, (UniSellPrice * int.Parse(txtQty.Text)));

                                //CLEAR TXTBOX VALUE
                                ClearAllText(this);

                                //FOCUS ITEM NO TXTBOX
                                txtItemNo.Focus();

                                //DATAGRIDVIEW REFRESH
                                LoadDataForDgv();

                                //CALCULATE TOTAL PRICE
                                TotalPrice();

                                //DISABLE QTY TXTBOX
                                txtQty.Enabled = false;
                            }
                            else { MessageBox.Show("Not enought Quantity in the stock!. Available Quantity are " + itemQty.ToString(), "STOCK LEVEL", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                        }
                        else { MessageBox.Show("Qty must be greater than zero!", "STOCK LEVEL", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                    }
                    else
                    {
                        MessageBox.Show("This item is exist!", "ITEM EXIST", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        con.Close();
                    }
                }
                else
                {
                    con.Close();
                    MessageBox.Show("Does not exist this item!.", "ITEM MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter Qty Number! \n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                    SelectedItemsDetails();

            }
            catch (Exception ex) { MessageBox.Show("Please enter Qty Number! \n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtItemNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 127)
                e.Handled = true;
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 127)
                e.Handled = true;
        }

        private void picHelp_Click(object sender, EventArgs e)
        {
            frmHelp ObjHlp = new frmHelp();
            ObjHlp.ShowDialog();
        }

        private void picAbout_Click(object sender, EventArgs e)
        {
            frmAbout ObjAbut = new frmAbout();
            ObjAbut.ShowDialog();
        }

        private void txtQty_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                //CHECK AVAILABLE ITEM IN THE STOCK
                CheckAvailableItem();
            }
            catch (Exception ex) { MessageBox.Show("Please enter Qty Number! \n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtDescription_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                //CHECK AVAILABLE ITEM IN THE STOCK
                CheckAvailableItem();
                txtItemNo.Focus();
            }
            catch (Exception ex) { MessageBox.Show("Please enter Qty Number! \n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dgvInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Delete)
                {
                    if (dgvInvoice.Rows.Count != 0)
                    {
                        //Delete item in invoice item table
                        SqlCommand cmd = new SqlCommand("DELETE FROM tbl_Bill_Items WHERE ItemNo='" + dgvInvoice.CurrentRow.Cells["ItemNo"].Value.ToString() + "'", con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        //DATAGRIDVIEW REFRESH
                        LoadDataForDgv();

                        //CALCULATE TOTAL PRICE
                        TotalPrice();

                        //PAYMENT METHOD
                        btnPayMethod();
                    }
                    else { MessageBox.Show("Items are empty!. Please add items.", "SELL ITEMS ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            try
            {


                if (dgvInvoice.Rows.Count > 0)
                {
                    if (newTot != 0.00M)
                    {
                        //COPY tbl_Bill_Items DATA TO tbl_InvoiceItems
                        SqlCommand cmd = new SqlCommand("INSERT INTO tbl_InvoiceItems SELECT * FROM tbl_Bill_Items", con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        //NEW INVOICE
                        ObjInvoice.NewInvoce(
                                                lblInvoiceNo.Text,
                                                DateTime.Today,
                                                lblSeller.Text,
                                                totalPrice,
                                                int.Parse(frmPay.discountVal),
                                                decimal.Parse(frmPay.paymentVal),
                                                balance,
                                                totQty,
                                                newTot
                                            );

                        //NEW INVOICE TEMP
                        ObjInvoice.NewInvoceBill(
                                                lblInvoiceNo.Text,
                                                DateTime.Today,
                                                lblSeller.Text,
                                                totalPrice,
                                                int.Parse(frmPay.discountVal),
                                                decimal.Parse(frmPay.paymentVal),
                                                balance,
                                                totQty,
                                                newTot
                                            );

                        MessageBox.Show("Invoice Successful!", "SUCCESSFUL", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //SELL ITEMS REMOVE IN THE STOCK
                        SqlDataAdapter iget = new SqlDataAdapter("SELECT * FROM tbl_Bill_Items", con);
                        con.Open();
                        DataTable idt = new DataTable();
                        iget.Fill(idt);
                        con.Close();
                        for (int i = 0; i < idt.Rows.Count; i++)
                        {
                            int nowAllQty = 0;

                            string item_no = idt.Rows[i]["ItemNo"].ToString();
                            int noOfQuantity = int.Parse(idt.Rows[i]["Qty"].ToString());
                            //con.Open();

                            string csql = "SELECT * FROM tbl_Stock WHERE ItemNo='" + item_no + "'";
                            SqlCommand cmdi = new SqlCommand(csql, con);
                            con.Open();
                            SqlDataReader dr1 = cmdi.ExecuteReader();
                            if (dr1.Read() == true)
                            {
                                int stock_noOfQty = int.Parse(dr1["Qty"].ToString());
                                // decimal old_OneQtySellPrice = decimal.Parse(dr1[""].ToString());
                                con.Close();

                                //NOW STOCK ITEMS
                                nowAllQty = stock_noOfQty - noOfQuantity;

                                //STOCK UPDATE
                                ObjInvoice.update_Quntity(item_no, nowAllQty);

                            }
                            else con.Close();
                        }//FOR LOOP END

                        //CrystalReportBill OBjcryBill = new CrystalReportBill();
                        //OBjcryBill.PrintToPrinter(1, false, 0, 0);
                        CrystalReport1 OBjcryBill1 = new CrystalReport1();
                        OBjcryBill1.SetDatabaseLogon("genesip", "genesip");//Fix databse logon fail error
                        OBjcryBill1.PrintToPrinter(1, false, 0, 0);
                        
                        //OBjcryBill1.DataSourceConnections.Clear();
                        //OBjcryBill1.Refresh();

                        //CLEAR ALL BILL ITEMS
                        SqlCommand cmdBillItems = new SqlCommand("TRUNCATE TABLE " + "tbl_Bill_Items", con);
                        con.Open();
                        cmdBillItems.ExecuteNonQuery();
                        con.Close();

                        //CLEAR ALL BILL ITEMS
                        SqlCommand cmdBillInvo = new SqlCommand("TRUNCATE TABLE " + "tbl_Bill_Invoice", con);
                        con.Open();
                        cmdBillInvo.ExecuteNonQuery();
                        con.Close();

                        this.Close();
                    }
                    else MessageBox.Show("Please pay your bill!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else MessageBox.Show("Please add some items in the invoice!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
