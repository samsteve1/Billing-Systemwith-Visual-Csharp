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
    class DeaCustDAL
    {
        
        static string myConnString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        #region Select
        public DataTable Select()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(myConnString);

            try
            {
                string sql = "SELECT * FROM tbl_dea_cust";
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
        public bool Insert(DeaCustBll deaCust)
        {
            bool insert = false;
            SqlConnection conn = new SqlConnection(myConnString);
            try
            {
                string sql = "INSERT INTO tbl_dea_cust (type, name, email, contact, address, added_date, added_by) VALUES(@type, @name, @email, @contact, @address, @added_date, @added_by)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@type", deaCust.Type);
                cmd.Parameters.AddWithValue("@name", deaCust.Name);
                cmd.Parameters.AddWithValue("@email", deaCust.Email);
                cmd.Parameters.AddWithValue("@contact", deaCust.Contact);
                cmd.Parameters.AddWithValue("@address", deaCust.Address);
                cmd.Parameters.AddWithValue("@added_date", deaCust.Added_date);
                cmd.Parameters.AddWithValue("@added_by", deaCust.Added_by);

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

        #region Update
        public bool Update(DeaCustBll deaCust)
        {
            bool update = false;

            SqlConnection conn = new SqlConnection(myConnString);
            try
            {
                string sql = "UPDATE tbl_dea_cust SET type=@type, name=@name, email=@email, contact=@contact, address=@address WHERE id=@id";
                SqlCommand cmd  = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@type", deaCust.Type);
                cmd.Parameters.AddWithValue("@name", deaCust.Name);
                cmd.Parameters.AddWithValue("@email", deaCust.Email);
                cmd.Parameters.AddWithValue("@contact", deaCust.Contact);
                cmd.Parameters.AddWithValue("@address", deaCust.Address);
                cmd.Parameters.AddWithValue("@id", deaCust.Id);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if(rows > 0)
                {
                    update = true;
                }
                else
                {
                    update = false;
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
            return update;
        }
        #endregion

        #region Delete
        public bool Delete(DeaCustBll deaCust)
        {
            bool delete = false;

            SqlConnection conn = new SqlConnection(myConnString);

            try
            {
                string sql = "DELETE FROM tbl_dea_cust WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", deaCust.Id);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if(rows > 0)
                {
                    delete = true;

                }
                else
                {
                    delete = false;
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
            return delete;
        }
        #endregion

        #region Search
        public DataTable Seach(string keyword)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(myConnString);

            try
            {
                string sql = "SELECT * FROM tbl_dea_cust WHERE id LIKE '%" + keyword + "%' OR name LIKE '%" + keyword + "%' OR type LIKE '%" + keyword + "%' OR contact LIKE '%"+keyword+"%' OR address LIKE '%"+keyword+"%'";

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
        #region Search Dealer or Customer
        public DeaCustBll SearchDealerCustomer(string keyword)
        {
            DeaCustBll dc = new DeaCustBll();

            SqlConnection conn = new SqlConnection(myConnString);

            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT name, email, contact, address FROM tbl_dea_cust WHERE id LIKE '%"+keyword+"%' OR name LIKE '%"+keyword+"%'";

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();

                adapter.Fill(dt);

                if(dt.Rows.Count > 0)
                {
                    dc.Name = dt.Rows[0]["name"].ToString();
                    dc.Email = dt.Rows[0]["email"].ToString();
                    dc.Contact = dt.Rows[0]["contact"].ToString();
                    dc.Address = dt.Rows[0]["address"].ToString();
                }
                else
                {
                    dc.Name = "Not Found";
                    dc.Email = "Not Found"; 
                    dc.Contact = "Not Found";
                    dc.Address = "Not Found";

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
            return dc;
        }
        #endregion
        #region Get ID
        public DeaCustBll GetDeaCust(string name)
        {
            DeaCustBll dc = new DeaCustBll();
            SqlConnection conn = new SqlConnection(myConnString);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT id FROM tbl_dea_cust WHERE name = '"+name+"'";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();

                adapter.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    dc.Id = int.Parse(dt.Rows[0]["id"].ToString());

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
            return dc;

        }
        #endregion
    }
}
