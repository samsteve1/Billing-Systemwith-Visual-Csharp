using BirthmarkStore.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BirthmarkStore.DAL;
using BirthmarkStore.BLL;

namespace BirthmarkStore
{
    public partial class adminDashboard : Form
    {
        public adminDashboard()
        {
            InitializeComponent();
        }
        private UserDAL dal = new UserDAL();
        private UserBll loggedInUser = new UserBll();
        private string username;
        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            User userFrom = new User();
            userFrom.Show();
        }

        private void adminDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {

            Login login = new Login();
            login.Show();
            this.Hide();

        }

        private void adminDashboard_Load(object sender, EventArgs e)
        {

            username = Login.loggedInUser;
            lblUsername.Text = username;
            loggedInUser = dal.CurrentLoggedInUser(username);
           
           

        }

        private void adminDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void categoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Categories category = new Categories();
            category.Show();
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Products productForm = new Products();
            productForm.Show();
        }

        private void dealerCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeaCust delaerCus = new DeaCust();
            delaerCus.Show();
        }

        private void transactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Transactions transaction = new Transactions();
            transaction.Show();
            
        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Inventory inventory = new Inventory();
            inventory.Show();
        }
    }
}
