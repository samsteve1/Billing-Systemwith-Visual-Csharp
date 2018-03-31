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
    class UserDAL
    {
        static string myConString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        #region Select User data from Database
        public DataTable Select()
        {
            SqlConnection conn = new SqlConnection(myConString);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM tbl_users";
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
        #region Insert User Data
        public bool Insert(UserBll user)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myConString);

            try
            {
                string sql = "INSERT INTO tbl_users (first_name, last_name, email, username, password, contact, address, gender, user_type, added_date, added_by )" +
                                "VALUES(@first_name, @last_name, @email, @username, @password, @contact, @address, @gender, @user_type, @added_date, @added_by)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@first_name", user.first_name);
                cmd.Parameters.AddWithValue("@last_name", user.last_name);
                cmd.Parameters.AddWithValue("@email", user.email);
                cmd.Parameters.AddWithValue("@username", user.username);
                cmd.Parameters.AddWithValue("@password", user.password);
                cmd.Parameters.AddWithValue("@contact", user.contact);
                cmd.Parameters.AddWithValue("@address", user.address);
                cmd.Parameters.AddWithValue("@gender", user.gender);
                cmd.Parameters.AddWithValue("@user_type", user.user_type);
                cmd.Parameters.AddWithValue("@added_date", user.added_date);
                cmd.Parameters.AddWithValue("@added_by", user.added_by);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                if(rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
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
            return isSuccess;
        }
        #endregion
        #region Update User Data
        public bool Update(UserBll user)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myConString);

            try
            {
                String sql = "UPDATE tbl_users SET first_name=first_name, last_name=@last_name, email=@email, username=@username, password=@password, contact=@contact, address=@address, gender=@gender, user_type=@user_type, added_date=@added_date, added_by=@added_by WHERE id=@user_id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@first_name", user.first_name);
                cmd.Parameters.AddWithValue("@last_name", user.last_name);
                cmd.Parameters.AddWithValue("@email", user.email);
                cmd.Parameters.AddWithValue("@username", user.username);
                cmd.Parameters.AddWithValue("@password", user.password);
                cmd.Parameters.AddWithValue("@contact", user.contact);
                cmd.Parameters.AddWithValue("@address", user.address);
                cmd.Parameters.AddWithValue("@gender", user.gender);
                cmd.Parameters.AddWithValue("@user_type", user.user_type);
                cmd.Parameters.AddWithValue("@added_date", user.added_date);
                cmd.Parameters.AddWithValue("@added_by", user.added_by);
                cmd.Parameters.AddWithValue("@user_id", user.id);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if(rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
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
            return isSuccess;
        }
        #endregion
        #region Delete User
        public bool Delete(UserBll user)
        {
            SqlConnection conn = new SqlConnection(myConString);
            bool isSuccess = false;
            try
            {
                string sql = "DELETE FROM tbl_users WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", user.id);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if(rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
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
            return isSuccess;
        }
        #endregion
        #region Search User
        public DataTable Search(string kywords)
        {
            SqlConnection conn = new SqlConnection(myConString);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM tbl_users WHERE id LIKE '%" + kywords + "%' OR first_name LIKE '%" + kywords + "%' or last_name LIKE '%" + kywords + "%' OR username LIKE '%" + kywords + "%'";
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
        #region GetLoggedInUser
        public UserBll CurrentLoggedInUser(string username)
        {
            UserBll loggedInUser = new UserBll();
            SqlConnection conn = new SqlConnection(myConString);

            DataTable dt = new DataTable();
            
            try
            {
                string sql = "SELECT * FROM tbl_users WHERE username=@username";

                SqlCommand cmd = new SqlCommand(sql, conn);             
  
                cmd.Parameters.AddWithValue("@username", username.ToLower());

                SqlDataAdapter adapater = new SqlDataAdapter(cmd);
                conn.Open();
                adapater.Fill(dt);

                if(dt.Rows.Count > 0)
                {
                    loggedInUser.id = int.Parse(dt.Rows[0]["id"].ToString());
                    loggedInUser.username = (string)dt.Rows[0]["username"].ToString();
                    loggedInUser.email = (string)dt.Rows[0]["email"].ToString();
                    loggedInUser.first_name = (string)dt.Rows[0]["first_name"].ToString();
                    loggedInUser.last_name = (string)dt.Rows[0]["last_name"].ToString();
                    loggedInUser.password = (string)dt.Rows[0]["password"].ToString();
                    loggedInUser.contact = (string)dt.Rows[0]["contact"].ToString();
                    loggedInUser.address = (string)dt.Rows[0]["address"].ToString();
                    loggedInUser.added_date = (DateTime)dt.Rows[0]["added_date"];
                    loggedInUser.added_by = Convert.ToInt32(dt.Rows[0]["added_by"]);
                    

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
            return loggedInUser;
        }
        #endregion



    }
}
