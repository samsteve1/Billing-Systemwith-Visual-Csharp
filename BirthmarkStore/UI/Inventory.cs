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

namespace BirthmarkStore.UI
{
    public partial class Inventory : Form
    {
        public Inventory()
        {
            InitializeComponent();
        }
        ProductDAL pDal = new ProductDAL();
        CategoriesDAL cDal = new CategoriesDAL();
        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Inventory_Load(object sender, EventArgs e)
        {
            ShowAllInventory();
            DataTable dt = cDal.Select();

            cmbCategories.DataSource = dt;
            cmbCategories.DisplayMember = "title";
            cmbCategories.ValueMember = "title";
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            ShowAllInventory();
        }
        private void ShowAllInventory()
        {
            dgvInventory.DataSource = pDal.Select();
        }

        private void cmbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            string category = cmbCategories.Text;
            dgvInventory.DataSource = pDal.GetProductByCategory(category);
        }
    }
}
