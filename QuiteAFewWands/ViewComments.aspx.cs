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
        protected void Page_Load(object sender, EventArgs e)
        {
            DBErrMsg.Visible = false;
            OkMsg.Visible = false;

            int WandId = 0;

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

    }
}