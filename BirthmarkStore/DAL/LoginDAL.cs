using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BirthmarkStore.BLL;
using System.Windows.Forms;

namespace BirthmarkStore.DAL
{
    class LoginDAL
    {
        static string myConString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        public bool LoginCheck(LoginBll login)
        {
            bool request = false;

            SqlConnection conn = new SqlConnection(myConString);

            try
            {
                string sql = "SELECT * FROM tbl_users WHERE username=@username AND password=@password AND user_type=@user_type";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@username", login.Username);
                cmd.Parameters.AddWithValue("@password", login.Password);
                cmd.Parameters.AddWithValue("@user_type", login.UserType);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if(dt.Rows.Count > 0)
                {
                    //login successful
                    request = true;
                }
                else
                {
                    //login failed
                    request = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
            return request;
        }
    }
}
