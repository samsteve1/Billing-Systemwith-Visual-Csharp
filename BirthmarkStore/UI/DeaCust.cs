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

namespace BirthmarkStore.UI
{
    public partial class DeaCust : Form
    {
        public DeaCust()
        {
            InitializeComponent();
        }
        private DeaCustBll deaCust = new DeaCustBll();
        private DeaCustDAL deaCustDal = new DeaCustDAL();
        private UserBll user = new UserBll();
        private UserDAL userDal = new UserDAL();
        static string loggedInUserName = Login.loggedInUser;

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtDeaCusName.Text.Trim();
            string type = cmbDeaCusType.Text.Trim();
            string email = txtDeaCusEmail.Text.Trim();
            string contact = txtDeaCusContact.Text.Trim();
            string address = txtDeaCusAddress.Text.Trim();

            if((name != "") && (type != "") && (email != "") && (contact != "") && (address != ""))
            {
                deaCust.Name = name;
                deaCust.Type = type;
                deaCust.Email = email;
                deaCust.Contact = contact;
                deaCust.Address = address;
                deaCust.Added_date = DateTime.Now;
                deaCust.Added_by = user.id;

                bool insert = deaCustDal.Insert(deaCust);

                if (insert)
                {
                    LoadDeaCust();
                    MessageBox.Show("Dealer/Customer Added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                }
                else
                {
                    MessageBox.Show("Failed to Add Customer", "Request Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("All Fields are required.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Clear()
        {
            txtDeaCusAddress.Text = "";
            txtDeaCusContact.Text = "";
            txtDeaCusEmail.Text = "";
            txtDeaCusId.Text = "";
            txtDeaCusName.Text = "";
            txtSearch.Text = "";
        }
        private void LoadDeaCust()
        {
            DataTable dt = deaCustDal.Select();
            dataGridDeaCus.DataSource = dt;

        }

        private void DeaCust_Load(object sender, EventArgs e)
        {
            LoadDeaCust();
            user = userDal.CurrentLoggedInUser(loggedInUserName);
            cmbDeaCusType.SelectedIndex = 0;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string deaCustId = txtDeaCusId.Text.Trim();
            if(deaCustId != "")
            {
                string name = txtDeaCusName.Text.Trim();
                string type = cmbDeaCusType.Text.Trim();
                string email = txtDeaCusEmail.Text.Trim();
                string contact = txtDeaCusContact.Text.Trim();
                string address = txtDeaCusAddress.Text.Trim();
                if ((name != "") && (type != "") && (email != "") && (contact != "") && (address != ""))
                {
                    deaCust.Id = Int32.Parse(deaCustId);
                    deaCust.Name = name;
                    deaCust.Type = type;
                    deaCust.Email = email;
                    deaCust.Contact = contact;
                    deaCust.Address = address;

                    bool update = deaCustDal.Update(deaCust);
                    if (update)
                    {
                        LoadDeaCust();

                        MessageBox.Show("Customer / Dealer Updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("Update Failed", "Request Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("All fields are required.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Please Select Customer \n or Dealer to Update", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridDeaCus_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtDeaCusId.Text = dataGridDeaCus.Rows[rowIndex].Cells[0].Value.ToString();
            cmbDeaCusType.Text = dataGridDeaCus.Rows[rowIndex].Cells[1].Value.ToString();
            txtDeaCusName.Text = dataGridDeaCus.Rows[rowIndex].Cells[2].Value.ToString();
            txtDeaCusEmail.Text = dataGridDeaCus.Rows[rowIndex].Cells[3].Value.ToString();
            txtDeaCusContact.Text = dataGridDeaCus.Rows[rowIndex].Cells[4].Value.ToString();
            txtDeaCusAddress.Text = dataGridDeaCus.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string cusId = txtDeaCusId.Text.Trim();
            if(cusId != "")
            {
                deaCust.Id = Int32.Parse(cusId);

                bool delete = deaCustDal.Delete(deaCust);
                if (delete)
                {
                    LoadDeaCust();
                    MessageBox.Show("Dealer / Customer Deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                }
                else
                {
                    MessageBox.Show("Unable to Delete Item.", "Request Failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an Item to Delete.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keywords = txtSearch.Text.Trim();
            if(keywords != "")
            {
                DataTable dt = deaCustDal.Seach(keywords);
                dataGridDeaCus.DataSource = dt;
            }
            else
            {
                LoadDeaCust();
            }
        }
    }
}
