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
    class CategoriesDAL
    {
        static string myConnString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        
        
        #region Select Categories
        public DataTable Select()
        {
           
            SqlConnection conn = new SqlConnection(myConnString);

            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM tbl_categories";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        #endregion
        #region Insert Category
        public bool Insert(CategoryBll category)
        {
            bool status = false;

            SqlConnection conn = new SqlConnection(myConnString);

            try
            {
                string sql = "INSERT INTO tbl_categories(title, description, added_date, aded_by) VALUES(@title, @description, @added_date, @added_by)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@title", category.title);
                cmd.Parameters.AddWithValue("@description", category.description);
                cmd.Parameters.AddWithValue("@added_date", category.added_date);
                cmd.Parameters.AddWithValue("@added_by", category.added_by);
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
        #region Update Category
        public bool Update(CategoryBll category)
        {
            bool status = false;
            SqlConnection conn = new SqlConnection(myConnString);

            try
            {
                string sql = "UPDATE tbl_categories SET title=@title, description=@description, added_date=@added_date WHERE id=@id ";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@title", category.title);
                cmd.Parameters.AddWithValue("@description", category.description);
                cmd.Parameters.AddWithValue("@added_date", category.added_date);
                cmd.Parameters.AddWithValue("@id", category.id);

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
        #region Delete Category
        public bool Delete(CategoryBll category)
        {
            bool status = false;

            SqlConnection conn = new SqlConnection(myConnString);

            try
            {
                string sql = "DELETE FROM tbl_categories WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", category.id);
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

    }
}
