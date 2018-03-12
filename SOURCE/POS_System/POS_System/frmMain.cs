using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace POS_System
{
    public partial class frmMain : Form
    {
        /*             DEVELOPED : "GENESIP SOLUTIONS"
         *             
         *             BARANCHES : ANURADHAPRA
         *                         COLOMBO
         *                         MATARA
         * 
         *             SYSTEM    : POINT OF SALE SYSTEM (POS SYSTEM)
         *             
         *             VERSION   : V 1.0
         *             
         *             RELEASE   : YEAR OF 2018
         * 
         *             WEBSITE   : WWW.GENESIP.COM
         */

        static string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(conStr);
        public frmMain()
        {
            InitializeComponent();

            timer1.Enabled = true;
            timer1.Interval = 1000;
        }

        private void pictureBox6_MouseClick(object sender, MouseEventArgs e)
        {
            this.pictureBox6.BackColor = Color.DarkGreen;
        }

        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox6.BackColor = Color.Green;
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox6.BackColor = Color.White;
        }

        private void pictureBox5_MouseClick(object sender, MouseEventArgs e)
        {
            this.pictureBox5.BackColor = Color.DarkGreen;
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox5.BackColor = Color.Green;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox5.BackColor = Color.White;
        }

        private void pictureBox7_MouseClick(object sender, MouseEventArgs e)
        {
            this.pictureBox7.BackColor = Color.DarkGreen;
        }

        private void pictureBox8_MouseClick(object sender, MouseEventArgs e)
        {
            this.pictureBox8.BackColor = Color.DarkGreen;
        }

        private void pictureBox9_MouseClick(object sender, MouseEventArgs e)
        {
            this.pictureBox9.BackColor = Color.DarkGreen;
        }

        private void pictureBox10_MouseClick(object sender, MouseEventArgs e)
        {
            this.pictureBox10.BackColor = Color.DarkGreen;
        }

        private void pictureBox7_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox7.BackColor = Color.Green;
        }

        private void pictureBox8_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox8.BackColor = Color.Green;
        }

        private void pictureBox9_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox9.BackColor = Color.Green;
        }

        private void pictureBox10_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox10.BackColor = Color.Green;
        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox7.BackColor = Color.White;
        }

        private void pictureBox8_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox8.BackColor = Color.White;
        }

        private void pictureBox9_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox9.BackColor = Color.White;
        }

        private void pictureBox10_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox10.BackColor = Color.White;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {



                //USER LEVEL HANDLE
                    if (frmLogin.userNAME == "Admin" || frmLogin.userNAME == "admin")
                    pictureBox7.Enabled = true;
                else
                    pictureBox7.Enabled = false;


                //COMPANY NAME LOAD
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_Company", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read() == true)
                {
                    lblCompanyName.Text = dr["Name"].ToString();
                    con.Close();
                }
                con.Close();

                //Display date format
                lblDate.Text = DateTime.Now.ToString(DateTime.Now.Day.ToString() + " MMMM, " + DateTime.Now.Year.ToString() + "  -  " + "dddd");


                //Display Good Morning,Good Afternoon and Good Evening Messages
                if (DateTime.Now.Hour < 12) lblDayEvent.Text = "Good Morning";
                else if (DateTime.Now.Hour < 17) lblDayEvent.Text = "Good Afternoon";
                else lblDayEvent.Text = "Good Evening";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Time display in label
            lblTime.Text = DateTime.Now.ToLongTimeString();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            frmInvoice ObjInvo = new frmInvoice();
            ObjInvo.ShowDialog();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            frmHistory ObjHis = new frmHistory();
            ObjHis.ShowDialog();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            frmStock ObjStock = new frmStock();
            ObjStock.ShowDialog();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            frmUsers ObjUser = new frmUsers();
            ObjUser.ShowDialog();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            frmSetting ObjSett = new frmSetting();
            ObjSett.ShowDialog();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            frmHelp ObjHelp = new frmHelp();
            ObjHelp.ShowDialog();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            frmAbout ObjAb = new frmAbout();
            ObjAb.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            frmHelp ObjHelp = new frmHelp();
            ObjHelp.ShowDialog();
        }

        private void frmMain_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox6.BackColor = Color.White;
            this.pictureBox5.BackColor = Color.White;
            this.pictureBox7.BackColor = Color.White;
            this.pictureBox8.BackColor = Color.White;
            this.pictureBox9.BackColor = Color.White;
            this.pictureBox10.BackColor = Color.White;
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("www.genesip.com");  
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "INTERNET ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
