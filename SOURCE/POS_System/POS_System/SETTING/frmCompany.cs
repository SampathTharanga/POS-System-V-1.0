using System;
using System.Windows.Forms;

namespace POS_System
{
    public partial class frmCompany : Form
    {
        public frmCompany()
        {
            InitializeComponent();
        }

        SettingClass ObjSetting = new SettingClass();

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCompanyName.Text != "")
                {
                    ObjSetting.update_Company(1, txtCompanyName.Text);
                    MessageBox.Show("Update successful!", "UPDATE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Restart();
                }
                else MessageBox.Show("Please enter Company name!", "COMPANY MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }catch(Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
