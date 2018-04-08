using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using BirthmarkStore.BLL;

namespace BirthmarkStore.DAL
{
    class TransactionDal
    {
        static string myConString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        #region Insert Transaction
        public bool Insert_Transaction(TransactionBll transaction, out int transactionId)
        {
            bool insert = false;
            transactionId = -1;
            SqlConnection conn = new SqlConnection(myConString);
            try
            {
                string sql = "INSERT INTO tbl_transaction(type, dea_cust_id, grandTotal, transaction_date, tax, discount, added_by)"+
                                "VALUES(@type, @dea_cust_id, @grandTotal, @transaction_date, @tax, @discount, @added_by); SELECT @@IDENTITY;";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@type", transaction.type);
                cmd.Parameters.AddWithValue("@dea_cust_id", transaction.dea_cust_id);
                cmd.Parameters.AddWithValue("@grandTotal", transaction.grandTotal);
                cmd.Parameters.AddWithValue("@transaction_date", transaction.transaction_date);
                cmd.Parameters.AddWithValue("@tax", transaction.tax);
                cmd.Parameters.AddWithValue("@discount", transaction.discount);
                cmd.Parameters.AddWithValue("@added_by", transaction.added_by);

                conn.Open();
                object o = cmd.ExecuteScalar();
                if(o != null)
                {
                    transactionId = int.Parse(o.ToString());
                    insert = true;
                }
                else
                {
                    insert = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
            return insert;

        }
        #endregion

        #region Get all transactions
        public DataTable GetAllTrans()
        {
            DataTable transaction = new DataTable();

            SqlConnection conn = new SqlConnection(myConString);
            try
            {
                string sql = "SELECT * FROM tbl_transaction";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();

                adapter.Fill(transaction);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
            return transaction;
        }
        #endregion

        #region Get Transactions based on Type
        public DataTable GetTransactionByType(string type)
        {
            DataTable transactions = new DataTable();

            SqlConnection conn = new SqlConnection(myConString);

            try
            {
                string sql = "SELECT * FROM tbl_transaction WHERE type=@type";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@type", type);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                adapter.Fill(transactions);
                conn.Open();
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
            return transactions;

        }
        #endregion
    }
}
