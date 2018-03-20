using System;
using System.Windows.Forms;

namespace POS_System
{
    public partial class frmPay : Form
    {
        string totPrice = string.Empty;
        public frmPay(string TotPriceVal)
        {
            InitializeComponent();
            totPrice = TotPriceVal;
            this.lblTotPrice.Text = "Rs. " + TotPriceVal;
        }

        public static string paymentVal = string.Empty, discountVal = string.Empty;

        private void txtPayment_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {
                    if ((txtPayment.Text != null) && (txtPayment.Text != string.Empty))
                    {
                        if ((decimal.Parse(totPrice) <= decimal.Parse(txtPayment.Text)))
                            txtDiscount.Focus();
                        else
                        {
                            MessageBox.Show("Please pay your full payment!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtPayment.Clear();
                            txtPayment.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please pay your full payment!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPayment.Clear();
                        txtPayment.Focus();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //VALUE PUBLIC FOR GET INVOICE FORM
                    paymentVal = txtPayment.Text;
                    discountVal = txtDiscount.Text;
                    this.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private void txtDiscount_Click(object sender, EventArgs e)
        {
            txtDiscount.SelectAll();
        }

        private void txtPayment_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 127 && e.KeyChar != 46)
                    e.Handled = true;

                //Ensure validation for allow one "." value
                if (!char.IsControl(e.KeyChar))
                {
                    int dotIndex = txtPayment.Text.IndexOf('.');
                    if (char.IsDigit(e.KeyChar))
                    {
                        if (dotIndex != -1 && dotIndex < txtPayment.SelectionStart && txtPayment.Text.Substring(dotIndex + 1).Length >= 2)
                        {
                            e.Handled = true;
                        }
                    }
                    else
                        e.Handled = e.KeyChar != '.' || dotIndex != -1 || txtPayment.Text.Length == 0 || txtPayment.SelectionStart + 2 < txtPayment.Text.Length;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 127 && e.KeyChar != 46)
                e.Handled = true;
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            try
            {
                if ((txtPayment.Text != null) && (txtPayment.Text != string.Empty))
                {
                    if (decimal.Parse(totPrice) <= decimal.Parse(txtPayment.Text))
                    {

                        //VALUE PUBLIC FOR GET INVOICE FORM
                        paymentVal = txtPayment.Text;
                        discountVal = txtDiscount.Text;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please pay your full payment!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPayment.Clear();
                        txtPayment.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Please pay your full payment!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPayment.Clear();
                    txtPayment.Focus();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
