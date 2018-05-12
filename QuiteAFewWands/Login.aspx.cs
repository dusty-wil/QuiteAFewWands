using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace QuiteAFewWands
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBErrMsg.Visible = false;
            OkMsg.Visible = false;

            int logout = 0;
            int.TryParse(Request.QueryString["LogOut"], out logout);
            if (logout == 1)
            {
                LogOut();
                OkMsg.InnerText = "You have been logged out!";
                OkMsg.Visible = true;
            }
        }


        protected void Btn_Enter_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (UserExists())
                {
                    LogIn();
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    DBErrMsg.InnerText = "That user isn't in the system. Try registering?";
                    DBErrMsg.Visible = true;
                }

                Txt_Username.Text = "";
                Txt_Psw.Text = "";

            }
        }


        /**
         * check to see if user already exists
         * @return boolean whether the user exists
         */
        private bool UserExists()
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            String sql = "SELECT Id FROM [User] WHERE UserName = @UserName AND Password = HASHBYTES('SHA2_256', @Password)";
            SqlDataReader rd;

            bool userFound = false;

            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@UserName", Txt_Username.Text);
                cmd.Parameters.AddWithValue("@Password", Txt_Psw.Text);

                con.Open();

                rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    userFound = true;
                }
                rd.Close();
            }
            catch (Exception Err)
            {
                DBErrMsg.InnerText += Err.ToString();
                DBErrMsg.Visible = true;
            }
            finally
            {
                con.Close();
            }

            return userFound;
        }


        private void LogOut()
        {
            Session.Remove("user_username");
            Session.Remove("user_firstname");
            Session.Remove("user_lastname");
            Session.Remove("user_id");
            Session.Remove("user_isadmin");
            Session.Remove("user_accid");
        }


        private void LogIn()
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            String sql = "SELECT " +
                "Id, " +
                "UserName, " +
                "FirstName, " +
                "LastName, " +
                "IsAdmin, " +
                "AccId " +
            "FROM [User] " +
            "WHERE " +
                "UserName = @UserName " +
                "AND Password = HASHBYTES('SHA2_256', @Password)"
            ;
            SqlDataReader rd;

            int UserId = 0;

            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@UserName", Txt_Username.Text);
                cmd.Parameters.AddWithValue("@Password", Txt_Psw.Text);

                con.Open();

                rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    LogOut();

                    Session["user_id"] = rd["Id"].ToString();
                    Session["user_username"] = rd["UserName"].ToString();
                    Session["user_firstname"] = rd["FirstName"].ToString();
                    Session["user_lastname"] = rd["LastName"].ToString();
                    Session["user_isadmin"] = rd["IsAdmin"].ToString();
                    Session["user_accid"] = rd["AccId"].ToString();

                    if (int.TryParse(rd["Id"].ToString(), out UserId))
                    {
                        UpdateLastLogin(UserId);
                    }

                }
                rd.Close();
            }
            catch (Exception Err)
            {
                DBErrMsg.InnerText += Err.ToString();
                DBErrMsg.Visible = true;
            }
            finally
            {
                con.Close();
            }
        }


        private void UpdateLastLogin(int UserId)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            String sql = "UPDATE [User] SET DateLastLogin = SYSDATE() WHERE Id = @UserId";
            
            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception Err)
            {
                DBErrMsg.InnerText = Err.ToString();
                DBErrMsg.Visible = true;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
