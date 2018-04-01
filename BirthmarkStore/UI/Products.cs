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
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }
        UserBll user = new UserBll();
        static string loggedInUserName = Login.loggedInUser;
        ProductBll product = new ProductBll();
        ProductDAL productDal = new ProductDAL();
        UserDAL userDal = new UserDAL();

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string keywords = txtProductSearch.Text.ToString();
            if(keywords != "")
            {
                DataTable dt = productDal.Search(keywords);
                dataGridProducts.DataSource = dt;
            }
            else
            {
                LoadProducts();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        CategoriesDAL catDal = new CategoriesDAL();
        private void Products_Load(object sender, EventArgs e)
        {
            LoadProducts();
            DataTable cateDt = catDal.Select();
            cmbProductCat.DataSource = cateDt;
            cmbProductCat.DisplayMember = "title";
            cmbProductCat.ValueMember = "title";

            user = userDal.CurrentLoggedInUser(loggedInUserName);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtProductName.Text.Trim();
            string category = cmbProductCat.Text;
            string description = txtProductDesc.Text.Trim();
            string rate = txtproductRate.Text.Trim();
            decimal qty = 0;
            
           

            if((name != "") && (category != "")  && (description != "") && (rate != null))
            {
                product.Name = name;
                product.Category = category;
                product.Description = description;
                product.Rate = Decimal.Parse(rate);
                product.Qty = qty;
                product.Added_Date = DateTime.Now;
                product.Added_By = user.id;

                bool insert = productDal.Insert(product);

                if (insert)
                {
                    LoadProducts();
                    MessageBox.Show("Product Added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                }
                else
                {
                    MessageBox.Show("Could not Add new Product!", "Request Failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("All fields are required!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
        private void LoadProducts()
        {
            DataTable products = productDal.Select();
            dataGridProducts.DataSource = products;
        }
        private void Clear()
        {
            txtProductId.Text = "";
            txtProductName.Text = "";
            txtproductRate.Text = "";
            txtProductDesc.Text = "";
            txtProductSearch.Text = "";
        }

        private void dataGridProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtProductId.Text = dataGridProducts.Rows[rowIndex].Cells[0].Value.ToString();
            txtProductName.Text = dataGridProducts.Rows[rowIndex].Cells[1].Value.ToString();
            cmbProductCat.Text = dataGridProducts.Rows[rowIndex].Cells[2].Value.ToString();
            txtProductDesc.Text = dataGridProducts.Rows[rowIndex].Cells[3].Value.ToString();
            txtproductRate.Text = dataGridProducts.Rows[rowIndex].Cells[4].Value.ToString();
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string pId = txtProductId.Text.Trim();
            if(pId != "")
            {
                string pName = txtProductName.Text.Trim();
                string pCat = cmbProductCat.Text.Trim();
                string pDesc = txtProductDesc.Text.Trim();
                string pRade = txtproductRate.Text.Trim();

                if((pName != "") && (pCat != "") && (pDesc != "") && pRade != null)
                {
                    product.Id = Convert.ToInt32(pId);
                    product.Name = pName;
                    product.Category = pCat;
                    product.Description = pDesc;
                    product.Rate = Decimal.Parse(pRade);
                    product.Added_Date = DateTime.Now;
                    product.Added_By = user.id;

                    bool update = productDal.Update(product);
                    if (update)
                    {
                        LoadProducts();
                        MessageBox.Show("Product Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update product.", "Request Failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }

            }
            else
            {
                MessageBox.Show("Please select a product to update.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string pId = txtProductId.Text.Trim();
            if(pId != "")
            {
                product.Id = Convert.ToInt32(pId);
                bool delete = productDal.Delete(product);
                if (delete)
                {
                    LoadProducts();
                    MessageBox.Show("Product deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                }
            }
            else
            {
                MessageBox.Show("Please select a product to delete.", "Input Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
