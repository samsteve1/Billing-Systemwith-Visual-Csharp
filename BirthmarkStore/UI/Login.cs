using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BirthmarkStore.BLL;
using BirthmarkStore.DAL;

namespace BirthmarkStore.UI
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        LoginBll login = new LoginBll();
        LoginDAL loginDal = new LoginDAL();
        public static string loggedInUser;
        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string userType = cmbUserType.Text.ToString();
           if((username != "") && (password != "") && (userType != ""))
            {
                login.Username = username;
                login.Password = password;
                login.UserType = userType;

                bool status = loginDal.LoginCheck(login);
                if (status)
                {
                    loggedInUser = login.Username;
                    switch (login.UserType)
                    {
                        case "Admin":
                            adminDashboard admin = new adminDashboard();
                            admin.Show();
                            this.Hide();
                            //Application.Exit();
                            break;
                        case "User":
                            userDashboard user = new userDashboard();
                            user.Show();
                            this.Hide();
                            //Application.Exit();
                            break;
                        default:
                            lblError.Text = "Unknow User Type.";
                            lblError.Visible = true;
                            break;
                    }
                }
                else
                {
                    lblError.Text = "Invalid Username or Password.";
                    lblError.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("All Fields are Required!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            cmbUserType.SelectedIndex = 0;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
