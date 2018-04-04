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
    class TransactionDetailDal
    {
        static string myConString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        #region Insert
        public bool Insert(TransactionDetailsBll td)
        {
            bool insert = false;
            SqlConnection conn = new SqlConnection(myConString);
            try
            {
                string sql = "INSERT INTO tbl_transaction_details(product_id, rate, qty, total, dea_cust_id, added_date, added_by)" + 
                                "VALUES(@product_id, @rate, @qty, @total, @dea_cust_id, @added_date, @added_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@product_id", td.product_id);
                cmd.Parameters.AddWithValue("@rate", td.rate);
                cmd.Parameters.AddWithValue("@qty", td.qty);
                cmd.Parameters.AddWithValue("@total", td.total);
                cmd.Parameters.AddWithValue("@dea_cust_id", td.dea_cust_id);
                cmd.Parameters.AddWithValue("@added_date", td.added_date);
                cmd.Parameters.AddWithValue("@added_by", td.added_by);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if(rows > 0)
                {
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
    }
}
