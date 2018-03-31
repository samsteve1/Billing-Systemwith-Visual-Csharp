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
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
        }
        private string loggedInUsername = Login.loggedInUser;
        UserBll user = new UserBll();
        UserBll LoggedIn = new UserBll();
        
        UserDAL dal = new UserDAL();
        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {            
            string id = txtUserId.Text.Trim();
            if(id != "")
            {
                user.id = Convert.ToInt32(id);
                user.first_name = txtFirstName.Text;
                user.last_name = txtLastName.Text;
                user.email = txtEmail.Text;
                user.username = txtUsername.Text.ToLower();
                user.password = txtPassword.Text;
                user.user_type = cmbAccType.Text;
                user.added_date = DateTime.Now;
                user.address = txtAddress.Text;
                user.contact = txtContact.Text;
                user.gender = cmbGender.Text;
                user.added_by = 1;

                bool status = dal.Update(user);
                if(status)
                {
                    RefreshDataGrid();
                    MessageBox.Show("User Updated!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Unable to Update User!", "Error Processing Request", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Nothing to Update!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            LoggedIn = dal.CurrentLoggedInUser(loggedInUsername);
            string first_name = txtFirstName.Text.Trim();
            string last_name = txtLastName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string contact = txtContact.Text.Trim();
            string address = txtAddress.Text.Trim();
            string gender = cmbGender.Text;
            string userType = cmbAccType.Text;
            //DateTime added_date = DateTime.Now;
            if((first_name != "") && (last_name != "") && (email != "") && (username != "") 
                && (password != "") && (contact != "") && (address != ""))
            {
                user.first_name = first_name;
                user.last_name = last_name;
                user.email = email;
                user.username = username.ToLower();
                user.password = password;
                user.contact = contact;
                user.address = address;
                user.gender = gender;
                user.user_type = userType;
                user.added_date = DateTime.Now;
                user.added_by = LoggedIn.id;
                
                //insert user to db
                bool success = dal.Insert(user);
                if(success)
                {
                    RefreshDataGrid();
                    MessageBox.Show("User Account Created Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                }
                else
                {
                    MessageBox.Show("Unable to Create User Account!", "Request Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("All Fields are Required!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void User_Load(object sender, EventArgs e)
        {
            RefreshDataGrid();
            cmbAccType.SelectedIndex = 0;
            cmbGender.SelectedIndex = 1;
        }
        public void RefreshDataGrid()
        {
            DataTable dt = dal.Select();
            dataGridView1.DataSource = dt;
        }
        public void Clear()
        {
            txtAddress.Text = "";
            txtContact.Text = "";
            txtEmail.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPassword.Text = "";
            txtUsername.Text = "";
            txtUserId.Text = "";
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get the index of a particluar row
            int rowIndex = e.RowIndex;
            txtUserId.Text = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dataGridView1.Rows[rowIndex].Cells[3].Value.ToString();
            txtUsername.Text = dataGridView1.Rows[rowIndex].Cells[4].Value.ToString();
            txtPassword.Text = dataGridView1.Rows[rowIndex].Cells[5].Value.ToString();
            txtContact.Text = dataGridView1.Rows[rowIndex].Cells[6].Value.ToString();
            txtAddress.Text = dataGridView1.Rows[rowIndex].Cells[7].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id  = txtUserId.Text.Trim();
            if(id != "")
            {
                user.id = Convert.ToInt32(id);
                bool status = dal.Delete(user);
                if (status)
                {
                    RefreshDataGrid();
                    MessageBox.Show("User Deleted", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information );
                    Clear();
                }
            }
            else
            {
                MessageBox.Show("Please Select a User to Delete!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keywords = txtSearch.Text;
            if(keywords != null)
            {
                DataTable dt = dal.Search(keywords);
                dataGridView1.DataSource = dt;
            }
            else
            {
                RefreshDataGrid();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
