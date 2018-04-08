using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BirthmarkStore.UI;
namespace BirthmarkStore
{
    public partial class userDashboard : Form
    {
        public userDashboard()
        {
            InitializeComponent();
        }
       public static string transactionType;
        private void userDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void userDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void userDashboard_Load(object sender, EventArgs e)
        {
            lblUsername.Text = Login.loggedInUser;
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transactionType = "Purchase";
            Purchase purchase = new Purchase();
            purchase.Show();
           
        }

        private void salesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transactionType = "Sales";
            Purchase purchase = new Purchase();
            purchase.Show();
        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Inventory inventory = new Inventory();
            inventory.Show();
        }
    }
}
