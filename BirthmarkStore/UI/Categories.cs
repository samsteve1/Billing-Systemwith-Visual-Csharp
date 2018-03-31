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
    public partial class Categories : Form
    {
        public Categories()
        {
            InitializeComponent();
        }
        CategoryBll category = new CategoryBll();
        CategoriesDAL dal = new CategoriesDAL();
        UserBll user = new UserBll();
        UserDAL userDal = new UserDAL();
        
        string loggedInUsername = Login.loggedInUser;
        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string catTitle = txtCategoryTitle.Text.Trim();
            string catDesc = txtCategoryDesc.Text.Trim();

            if((catTitle != "") && (catDesc != ""))
            {
                category.title = catTitle;
                category.description = catDesc;
                category.added_by = user.id;
                category.added_date = DateTime.Now;

                bool status = dal.Insert(category);

                if (status)
                {
                    LoadCategories();
                    MessageBox.Show("Category Added Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                }
                else
                {
                    MessageBox.Show("Unable to Add Category", "Request Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("All Fields are required!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Categories_Load(object sender, EventArgs e)
        {
            LoadCategories();
            user = userDal.CurrentLoggedInUser(loggedInUsername);
        }
        private void Clear()
        {
            txtCategoryDesc.Text = "";
            txtCategoryTitle.Text = "";
            txtCategoryId.Text = "";
            txtCatSearch.Text = "";
        }
        private void LoadCategories()
        {
            DataTable dt = dal.Select();
            dataGridCat.DataSource = dt;
        }

        private void dataGridCat_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtCategoryId.Text = dataGridCat.Rows[rowIndex].Cells[0].Value.ToString();
            txtCategoryTitle.Text = dataGridCat.Rows[rowIndex].Cells[1].Value.ToString();
            txtCategoryDesc.Text = dataGridCat.Rows[rowIndex].Cells[2].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string catId = txtCategoryId.Text;
            string catTitle = txtCategoryTitle.Text;
            string catDesc = txtCategoryDesc.Text;
            if(catId != "")
            {
                if((catTitle != "") && (catDesc != ""))
                {
                    category.title = catTitle;
                    category.description = catDesc;
                    category.id = Convert.ToInt32(catId);
                    category.added_date = DateTime.Now;

                    bool status = dal.Update(category);
                    if (status)
                    {
                        LoadCategories();
                        MessageBox.Show("Category Updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("Unable to Update Category!", "Request Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("All Fields are required!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Select an Item to update!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string catId = txtCategoryId.Text;
            if(catId != "")
            {
                category.id = Convert.ToInt32(catId);
                bool status = dal.Delete(category);
                if (status)
                {
                    LoadCategories();
                    MessageBox.Show("Category deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                }
                else
                {
                    MessageBox.Show("Unablto delete category", "Request Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("select a Category to delete.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
