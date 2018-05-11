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
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            OkMsg.Visible = false;
            DBErrMsg.Visible = false;

            if (!IsPostBack)
            {
                FillHouseList();
            }
        }

        protected void Btn_Register_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (UserExists())
                {
                    Txt_Username.Text = "";
                    Txt_Psw.Text = "";
                    Txt_FName.Text = "";
                    Txt_LName.Text = "";
                    Txt_Email.Text = "";
                    Txt_AccId.Text = "";

                    OkMsg.Visible = false;
                    DBErrMsg.Visible = true;
                    DBErrMsg.InnerText = "User account already exists! Login instead!";
                }
                else
                {
                    if (CreateUser())
                    {
                        Txt_Username.Text = "";
                        Txt_Psw.Text = "";
                        Txt_FName.Text = "";
                        Txt_LName.Text = "";
                        Txt_Email.Text = "";
                        Txt_AccId.Text = "";

                        OkMsg.Visible = true;
                        OkMsg.InnerText = "User account created! You may now log in!";
                    }
                }
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
            String sql = "SELECT Id FROM [User] WHERE UserName = @UserName";
            SqlDataReader rd;

            bool userFound = false;

            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@UserName", Txt_Username.Text);

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


        /**
         * create user account
         * @return boolean whether the acct was created
         */ 
        private bool CreateUser()
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            String sql = "INSERT INTO [User] ( " +
                    "UserName, " +
                    "Password, " +
                    "FirstName, " +
                    "LastName, " +
                    "AccId, " +
                    "Email, " +
                    "HouseId, " +
                    "DateAdded " +
                " ) VALUES ( " +
                    "@UserName, " +
                    "HASHBYTES('SHA2_256', @Password), " +
                    "@FirstName, " +
                    "@LastName, " +
                    "@AccId, " +
                    "@Email, " +
                    "@HouseId, " +
                    "SYSDATETIME() " +
                " ) "
                ;

            bool success = true;

            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                cmd.Parameters.AddWithValue("@UserName", Txt_Username.Text);
                cmd.Parameters.AddWithValue("@Password", Txt_Psw.Text);
                cmd.Parameters.AddWithValue("@FirstName", Txt_FName.Text);
                cmd.Parameters.AddWithValue("@LastName", Txt_LName.Text);
                cmd.Parameters.AddWithValue("@Email", Txt_Email.Text);

                int houseId = 5;
                int.TryParse(HouseId.SelectedValue, out houseId);
                cmd.Parameters.AddWithValue("@HouseId", houseId);

                cmd.Parameters.AddWithValue("@AccId", Txt_AccId.Text);

                cmd.ExecuteNonQuery();
            }
            catch (Exception Err)
            {
                DBErrMsg.InnerText = Err.ToString();
                DBErrMsg.Visible = true;
                success = false; 
            }
            finally
            {
                con.Close();
            }

            return success;
        }


        private void FillHouseList()
        {
            HouseId.Items.Clear();

            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            String sql = "SELECT Id, HouseName FROM House";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataReader rd;

            try
            {
                con.Open();
                rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    ListItem newItem = new ListItem
                    {
                        Text = rd["HouseName"].ToString(),
                        Value = rd["Id"].ToString()
                    };
                    HouseId.Items.Add(newItem);
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
    }
}
