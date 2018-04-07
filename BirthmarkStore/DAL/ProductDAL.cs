using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using BirthmarkStore.BLL;

namespace BirthmarkStore.DAL
{
    class ProductDAL
    {
        static string myConnString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        #region Select
        public DataTable Select()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(myConnString);
            try
            {
              
                string sql = "SELECT * FROM tbl_products";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();

                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
        #endregion
        #region Insert
        public bool Insert(ProductBll product)
        {
            bool status = false;

            SqlConnection conn = new SqlConnection(myConnString);
            try
            {
                string sql = "INSERT INTO tbl_products(name, category, rate, added_date, added_by, description) VALUES(@name, @category, @rate,  @added_date, @added_by, @description)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@category", product.Category);
                cmd.Parameters.AddWithValue("@rate", product.Rate);
               // cmd.Parameters.AddWithValue("@qty", product.Qty);
                cmd.Parameters.AddWithValue("@added_date", product.Added_Date);
                cmd.Parameters.AddWithValue("@added_by", product.Added_By);
                cmd.Parameters.AddWithValue("@description", product.Description);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if(rows > 0)
                {
                    status = true;
                }
                else
                {
                    status = false;
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
            return status;
        }
        #endregion
        #region Update Product
        public bool Update(ProductBll product)
        {
            bool status = false;
            SqlConnection conn = new SqlConnection(myConnString);

            try
            {
                string sql = "UPDATE tbl_products SET name=@name, category=@category, rate=@rate, added_date=@added_date, added_by=@added_by, description=@description WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", product.Id);
                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@category", product.Category);
                cmd.Parameters.AddWithValue("@rate", product.Rate);
                
                cmd.Parameters.AddWithValue("@added_date", product.Added_Date);
                cmd.Parameters.AddWithValue("@added_by", product.Added_By);
                cmd.Parameters.AddWithValue("@description", product.Description);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if(rows > 0)
                {
                    status = true;
                }
                else
                {
                    status = false;
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
            return status;
        }
        #endregion
        #region Delete Product
        public bool Delete(ProductBll produt)
        {
            bool status = false;
            SqlConnection conn = new SqlConnection(myConnString);
            try
            {
                string sql = "DELETE FROM tbl_products WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", produt.Id);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if(rows > 0)
                {
                    status = true;
                }
                else
                {
                    status = false;
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
            return status;
        }
        #endregion
        #region Search products
        public DataTable Search(string keywords)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(myConnString);

            try
            {
                string sql = "SELECT * FROM tbl_products WHERE id LIKE '%" + keywords + "%' OR name LIKE '%" + keywords + "%' OR category LIKE '%" + keywords + "%' OR rate LIKE '%" + keywords + "%'";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();

                adapter.Fill(dt);



            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        #endregion
        #region Search for Products
        public ProductBll Seachproduct(string keyword)
        {
            ProductBll product = new ProductBll();

            SqlConnection conn = new SqlConnection(myConnString);

            try
            {
                string sql = "SELECT name, rate, qty FROM tbl_products WHERE id LIKE '%"+keyword+"%' OR name LIKE '%"+keyword+"%'";
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();

                adapter.Fill(dt);

                if(dt.Rows.Count > 0)
                {
                    product.Name = dt.Rows[0]["name"].ToString();
                    product.Rate = decimal.Parse(dt.Rows[0]["rate"].ToString());
                    product.Qty = decimal.Parse(dt.Rows[0]["qty"].ToString());
                }
                else
                {
                    product.Name = "Not Found";
                    product.Rate = 0M;
                    product.Qty = 0M;

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

            return product;
        }

        #endregion
        #region Get Product
        public ProductBll GetProduct(string pName)
        {
            ProductBll product = new ProductBll();
            SqlConnection conn = new SqlConnection(myConnString);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT id FROM tbl_products WHERE name = '" + pName + "'";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();

                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    product.Id = int.Parse(dt.Rows[0]["id"].ToString());

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
            return product;

        }
        #endregion
    }
}
