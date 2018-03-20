using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace POS_System
{
    public partial class frmSetting : Form
    {
        public frmSetting()
        {
            InitializeComponent();
        }

        string proceName = string.Empty;
        private void picBox_Company_Click(object sender, EventArgs e)
        {
            frmCompany ObjComp = new frmCompany();
            ObjComp.ShowDialog();
        }

        private void picCal_Click(object sender, EventArgs e)
        {
            try
            {
                proceName = string.Empty;
                proceName = "Calc";
                IsSysProFileOpened();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "SETTING ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void picWord_Click(object sender, EventArgs e)
        {
            try
            {
                proceName = string.Empty;
                proceName = "WINWORD";
                IsSysProFileOpened();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "SETTING ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


        private void picSticky_Click(object sender, EventArgs e)
        {
            try
            {
                proceName = string.Empty;
                proceName = "Notepad.exe";
                IsSysProFileOpened();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "SETTING ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void picBrowser_Click(object sender, EventArgs e)
        {
            try
            {
                proceName = string.Empty;
                proceName = "http://google.com";
                IsSysProFileOpened();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "SETTING ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        //SYSTEM PROGRAME OPEN FUNCTION
        private void IsSysProFileOpened()
        {
            try
            {
                if (proceName == "Calc")
                    Process.Start("Calc");
                else if (proceName == "WINWORD")
                    Process.Start("WINWORD");
                else if (proceName == "Notepad.exe")
                    Process.Start("Notepad.exe");
                else if (proceName == "http://google.com")
                    Process.Start("http://google.com");
                else
                    proceName = string.Empty;
            }
            catch(Exception ex) { MessageBox.Show(ex.Message, "SETTING ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

    }
}
