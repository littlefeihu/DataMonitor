using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DataMonitor.DQ.UI
{
    public partial class Login : Office2007Form
    {
        const string userprofilefile = "userprofile.xml";
        public Login()
        {
            InitializeComponent();
            this.EnableGlass = false;
            ReadUserProfile();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Hide();

            string username = txtUserName.Text;
            string password = txtPwd.Text;

            if (SignIn(username, password))
            {
                SaveUserProfile(username, password);
                MainForm mainForm = new MainForm();
                mainForm.WindowState = FormWindowState.Maximized;
                mainForm.ShowDialog();
            }
        }


        private void SaveUserProfile(string username, string password)
        {
            if (!chkRememberPassword.Checked)
            {
                username = string.Empty;
                password = string.Empty;
            }
            XDocument xdoc = new XDocument(
                                        new XDeclaration("1.0", "utf-8", "yes"),
                                        new XElement("root",
                                        new XElement("username", username),
                                        new XElement("password", password)
                                        ));
            xdoc.Save(userprofilefile);

        }
        private void ReadUserProfile()
        {
            if (File.Exists(userprofilefile))
            {
                XElement xele = XElement.Load(userprofilefile);
                XElement usernameEle = xele.Element("username");
                XElement pwdEle = xele.Element("password");
                txtUserName.Text = usernameEle.Value;
                txtPwd.Text = pwdEle.Value;
            }

        }

        private bool SignIn(string username, string password)
        {
            return true;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
