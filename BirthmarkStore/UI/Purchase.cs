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
using System.Transactions;
namespace BirthmarkStore.UI
{
    public partial class Purchase : Form
    {
        public Purchase()
        {
            InitializeComponent();
        }
        DeaCustDAL dal = new DeaCustDAL();
        ProductDAL pDal = new ProductDAL();
        DataTable transactionDt = new DataTable();
        UserDAL uDal = new UserDAL();
        TransactionDal tDal = new TransactionDal();
        TransactionDetailDal tdDal = new TransactionDetailDal();
        static string username = Login.loggedInUser;
        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Purchase_Load(object sender, EventArgs e)
        {
            lblType.Text = userDashboard.transactionType;

            //specify dataTable Columns
            transactionDt.Columns.Add("Product Name");
            transactionDt.Columns.Add("Rate");
            transactionDt.Columns.Add("Quantity");
            transactionDt.Columns.Add("Total");

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string keyWord = txtCusSearch.Text.Trim();
            if(keyWord != "")
            {
                DeaCustBll cus = dal.SearchDealerCustomer(keyWord);
                txtcusName.Text = cus.Name;
                txtAddress.Text = cus.Address;
                txtContact.Text = cus.Contact;
                txtEmail.Text = cus.Email;
            }
            else
            {
                ClearUserFields();
            }
        }
        private void ClearUserFields()
        {
            txtcusName.Text = "";
            txtAddress.Text = "";
            txtContact.Text = "";
            txtEmail.Text = "";
        }

        private void txtProductSearch_TextChanged(object sender, EventArgs e)
        {
            string keyWord = txtProductSearch.Text.ToString().Trim();
            if(keyWord != "")
            {
                ProductBll product = pDal.Seachproduct(keyWord);
                txtProductName.Text = product.Name;
                txtRate.Text = product.Rate.ToString();
                txtInventory.Text = product.Qty.ToString();

            }
            else
            {
                ClearProFields();
            }
        }
        private void ClearProFields()
        {
            txtProductName.Text = "";
            txtRate.Text = "";
            txtQty.Text = "";
            txtInventory.Text = "";
            txtProductSearch.Text = "";
        }

        private void ClearCalFields()
        {
            txtSubTotal.Text = "";
            txtGrandTotal.Text = "";
            txtVat.Text = "";
            txtDiscount.Text = "";
            txtPaidAmount.Text = "";
            txtReturnAmount.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string productName = txtProductName.Text.Trim();
            

            if(productName != "")
            {
                decimal rate = decimal.Parse(txtRate.Text.Trim());
                string qty = txtQty.Text.Trim();
                decimal quantity = 0M;
                if(qty != "")
                {
                    if(decimal.TryParse(qty, out quantity))
                    {
                        decimal total = quantity * rate;

                        decimal subtotal = decimal.Parse(txtSubTotal.Text);

                        subtotal = subtotal + total;

                        transactionDt.Rows.Add(productName, rate, quantity, total);

                        datProducts.DataSource = transactionDt;

                        txtSubTotal.Text = subtotal.ToString();

                        ClearProFields();
                    }
                    else
                    {
                        MessageBox.Show("Quantity can only be a number.");
                    }
                }
                else
                {
                    MessageBox.Show("Enter a quantity.");
                }
                

                

            }
            else
            {
                MessageBox.Show("Please select a product.");


            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            string inValue = txtDiscount.Text.ToString();
            if(inValue != "")
            {
                decimal subTotal = decimal.Parse(txtSubTotal.Text);
                decimal discount;
                if(decimal.TryParse(inValue, out discount))
                {
                    decimal grandTotal = ((100 - discount) / 100) * subTotal;

                    txtGrandTotal.Text = grandTotal.ToString();
                }
                else
                {
                    MessageBox.Show("Discount can only be number", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                txtGrandTotal.Text = "";
            }
        }

        private void txtVat_TextChanged(object sender, EventArgs e)
        {
            string inValue = txtVat.Text.Trim();
            if(inValue != "")
            {
                decimal vat;
                string  prevGrand = txtGrandTotal.Text.Trim();
                decimal recentGrand;
                if(decimal.TryParse(prevGrand, out recentGrand))
                {
                    if(decimal.TryParse(inValue, out vat))
                    {
                        decimal grandVAT = ((100 + vat) / 100) * recentGrand;
                        txtGrandTotal.Text = grandVAT.ToString();
                    }
                    else
                    {
                        MessageBox.Show("VAT can only be numbers.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Add Discount first", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            string inValue = txtPaidAmount.Text.Trim();
            if(inValue != "")
            {
                decimal grandTotal = decimal.Parse(txtGrandTotal.Text);
                decimal paidAmount;
                if(decimal.TryParse(inValue, out paidAmount))
                {
                    if(paidAmount > grandTotal)
                    {
                        decimal returnAmount = paidAmount - grandTotal;
                        txtReturnAmount.Text = returnAmount.ToString();
                    }
                    else
                    {
                        txtReturnAmount.Text = "";
                    }
                }

                else
                {
                    MessageBox.Show("Only Numbers are Allowed!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtReturnAmount.Text = "";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TransactionBll transaction = new TransactionBll();

            transaction.type = lblType.Text;

            string deaCustName = txtcusName.Text;
            DeaCustBll dc = dal.GetDeaCust(deaCustName);

            transaction.dea_cust_id = dc.Id;

            transaction.grandTotal = Math.Round(decimal.Parse(txtGrandTotal.Text), 2);
            transaction.transaction_date = DateTime.Now;
            transaction.tax = decimal.Parse(txtVat.Text);
            transaction.discount = decimal.Parse(txtDiscount.Text);

            UserBll u = uDal.CurrentLoggedInUser(username);
            transaction.added_by = u.id;
            transaction.transactionDetails = transactionDt;
            bool insert = false;

            using(TransactionScope scope = new TransactionScope())
            {
                int transactionId = -1;
                bool w = tDal.Insert_Transaction(transaction, out transactionId);

                for(int i = 0; i < transactionDt.Rows.Count; i++)
                {

                    string productName = transactionDt.Rows[i][0].ToString();
                    TransactionDetailsBll transactionDetail = new TransactionDetailsBll();
                    ProductBll product = pDal.GetProduct(productName);

                    transactionDetail.product_id = product.Id;
                    transactionDetail.rate = decimal.Parse(transactionDt.Rows[i][1].ToString());
                    transactionDetail.qty = decimal.Parse(transactionDt.Rows[i][2].ToString());
                    transactionDetail.total = Math.Round(decimal.Parse(transactionDt.Rows[i][3].ToString()),2);
                    transactionDetail.dea_cust_id = dc.Id;
                    transactionDetail.added_date = DateTime.Now;
                    transactionDetail.added_by = u.id;

                    //insert trans details in db.
                    bool y = tdDal.Insert(transactionDetail);
                    insert = w && y;         
                    
               }
                if (insert)
                {
                    //done
                    scope.Complete();
                    MessageBox.Show("Transaction Completed Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    datProducts.DataSource = null;
                    datProducts.Rows.Clear();
                    ClearProFields();
                    ClearUserFields();

                }
                else
                {
                    MessageBox.Show("Transaction Failed!", "Request Failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}
