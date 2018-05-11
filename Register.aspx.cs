using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace QuiteAFewWands
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String sql = "INSERT INTO [User] ( " +
    "UserName, " +
    "Password, " +
    "FirstName, " +
    "LastName, " +
    "Email, " +
    "HouseId, " +
    "DateAdded " +
" ) VALUES ( " +
    "@UserName, " +
    "HASHBYTES('SHA2_256', @Password), " +
    "@FirstName, " +
    "@LastName, " +
    "@Email, " +
    "@HouseId, " +
    "SYSDATETIME() " +
" ) "
;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qafw"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            
            // the parameter name here ------V  refers to the parameter name in your SQL
            cmd.Parameters.AddWithValue("@UserName", Txt_Username.Text);
            cmd.Parameters.AddWithValue("@Password", Txt_Psw.Text);
            cmd.Parameters.AddWithValue("@FirstName", Txt_FName.Text);
            cmd.Parameters.AddWithValue("@LastName", Txt_LName.Text);
            cmd.Parameters.AddWithValue("@Email", Txt_Email.Text);

            // this is the new "none" option I added to houses. It's a fall-back if things go screwy
            int houseId = 5;
            int.TryParse(HouseId.SelectedValue, out houseId);
            cmd.Parameters.AddWithValue("@HouseId", houseId);

            cmd.Connection = con;
            cmd.ExecuteNonQuery();

            Label7.Visible = true;
            Label7.Text = "User registered successfully";


            Txt_Username.Text = "";
            Txt_Psw.Text = "";
            Txt_FName.Text = "";
            Txt_LName.Text = "";
            Txt_Email.Text = "";
            Txt_FName.Focus();
            con.Close();
        }
    }
}