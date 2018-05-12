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
    public partial class ViewComments : System.Web.UI.Page
    {
        private int WandId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            DBErrMsg.Visible = false;
            OkMsg.Visible = false;

            if (!int.TryParse(Request.QueryString["WandId"], out WandId))
            {
                WandTitle.InnerText = "Wand Not Found";
                DBErrMsg.InnerText = "Invalid Wand ID";
                DBErrMsg.Visible = true;
                return;
            }

            GetWandName(WandId);
            GetWandComments(WandId);
        }


        protected void Btn_Submit_Click(object sender, EventArgs e)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            int UserId = 0;

            if (Session["user_id"] != null)
            {
                if (!int.TryParse(Session["user_id"].ToString(), out UserId))
                {
                    UserId = CreateAnonUser("Anon", "User");
                }
            }
            else
            {
                UserId = CreateAnonUser("Anon", "User");
            }

            String sql = "INSERT INTO Comment ( " +
                "WandId, " +
                "UserId, " +
                "Title, " +
                "Text," +
                "DateAdded " +
            " ) VALUES ( " +
                "@WandId, " +
                "@UserId, " +
                "@Title, " +
                "@Text, " +
                "SYSDATETIME() " +
            " ) "
            ;
            SqlCommand cmd = new SqlCommand(sql, con);

            try
            {
                con.Open();

                cmd.Parameters.AddWithValue("@WandId", WandId);
                cmd.Parameters.AddWithValue("@UserId", UserId);


                cmd.Parameters.AddWithValue("@Title", CommentTitleTxt.Text);
                cmd.Parameters.AddWithValue("@Text", CommentTxt.Text);

                cmd.ExecuteNonQuery();
                OkMsg.Visible = true;
                OkMsg.InnerText = "Comment has been added!";

                CommentTitleTxt.Text = "";
                CommentTxt.Text = "";

                GetWandComments(WandId);
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


        private void GetWandName(int WandId)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            SqlDataReader rd;

            try
            {
                con.Open();

                String sql = "SELECT Name FROM Wand WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Id", WandId);

                rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    WandTitle.InnerText = "Comments for: " + rd["Name"].ToString();
                }
                else
                {
                    WandTitle.InnerText = "Wand Not Found";
                }
                rd.Close();

            }
            catch (Exception Err)
            {
                WandTitle.InnerText = "Wand Not Found";
                DBErrMsg.InnerText = Err.ToString();
                DBErrMsg.Visible = true;
            }
            finally
            {
                con.Close();
            }
        }


        private void GetWandComments(int WandId)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            DataSet ds = new DataSet();

            try
            {
                con.Open();

                String sql =
                    "SELECT " +
                        "CONCAT(U.FirstName, ' ', U.LastName) AS Name, " +
                        "C.DateAdded AS Date, " +
                        "C.Title AS Title, " +
                        "C.Text AS Comment " +
                    "FROM Comment AS C " +
                    "INNER JOIN [User] AS U ON U.Id = C.UserId " +
                    "WHERE " +
                        "C.WandId = @WandId"
                ;
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@WandId", WandId);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                adp.Fill(ds, "CommentGrid");
                CommentListRepeater.DataSource = ds;
                CommentListRepeater.DataBind();
            }
            catch (Exception Err)
            {
                WandTitle.InnerText = "Product Not Found";
                DBErrMsg.InnerText = Err.ToString();
                DBErrMsg.Visible = true;
            }
            finally
            {
                con.Close();
            }
        }


        private int CreateAnonUser(string FirstName, string LastName)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            int userId = 0;

            String sql = "INSERT INTO [User] ( " +
                "FirstName, " +
                "LastName, " +
                "HouseId, " +
                "IsAdmin, " +
                "DateAdded " +
            " ) OUTPUT INSERTED.Id VALUES ( " +
                "@FirstName, " +
                "@LastName, " +
                "5, " +
                "0, " +
                "SYSDATETIME() " +
            " ) "
            ;
            SqlCommand cmd = new SqlCommand(sql, con);

            try
            {
                con.Open();

                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@LastName", LastName);

                userId = (int)cmd.ExecuteScalar();
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

            return userId;
        }
    }
}