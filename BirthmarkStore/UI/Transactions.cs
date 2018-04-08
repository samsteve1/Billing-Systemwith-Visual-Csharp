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
    public partial class Transactions : Form
    {
        public Transactions()
        {
            InitializeComponent();
        }
        TransactionDal tDal = new TransactionDal();

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Transactions_Load(object sender, EventArgs e)
        {
            cmbTransactionType.SelectedIndex = 0;
            ShowAllTransactions();
        }
        private void ShowAllTransactions()
        {
            dgvTransaction.DataSource = tDal.GetAllTrans();
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            ShowAllTransactions();
        }

        private void cmbTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string transType = cmbTransactionType.Text;
            dgvTransaction.DataSource = tDal.GetTransactionByType(transType);
        }
    }
}
