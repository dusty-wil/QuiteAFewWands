using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace QuiteAFewWands
{
    public partial class ViewRatings : System.Web.UI.Page
    {
        private double totalRatings = 0;
        private double ratingsTotal = 0;

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
            GetWandRatings(WandId);
        }

        protected void RatingGV_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (((DataRowView)e.Row.DataItem).Row != null)
                {
                    try
                    {
                        ratingsTotal += int.Parse(((DataRowView)e.Row.DataItem).Row["Rating"].ToString());
                        totalRatings++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (totalRatings > 0)
                {

                    e.Row.Cells[1].Text = "Avg: " + Math.Round((ratingsTotal/totalRatings), 2).ToString();
                }
            }
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

            String sql = "INSERT INTO Rating ( " +
                "WandId, " +
                "UserId, " +
                "Value, " +
                "DateAdded " +
            " ) VALUES ( " +
                "@WandId, " +
                "@UserId, " +
                "@Value, " +
                "SYSDATETIME() " +
            " ) "
            ;
            SqlCommand cmd = new SqlCommand(sql, con);

            try
            {
                con.Open();

                cmd.Parameters.AddWithValue("@WandId", WandId);
                cmd.Parameters.AddWithValue("@UserId", UserId);

                int val = 1;
                int.TryParse(RatingDDL.SelectedValue, out val);

                cmd.Parameters.AddWithValue("@Value", val);

                cmd.ExecuteNonQuery();
                OkMsg.Visible = true;
                OkMsg.InnerText = "Rating has been added!";
                GetWandRatings(WandId);
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
                    WandTitle.InnerText = "Ratings for: " + rd["Name"].ToString();
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

        private void GetWandRatings(int WandId)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            DataSet ds = new DataSet();

            try
            {
                con.Open();

                String sql =
                    "SELECT " +
                        "R.DateAdded AS Date, " +
                        "R.Value AS Rating " +
                    "FROM Rating AS R " +
                    "INNER JOIN [User] AS U ON U.Id = R.UserId " +
                    "WHERE " +
                        "R.WandId = @WandId"
                ;

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@WandId", WandId);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                adp.Fill(ds, "RatingGrid");
                RatingGrid.DataSource = ds;
                RatingGrid.DataBind();
                RatingGrid.Visible = true;
            }
            catch (Exception Err)
            {
                DBErrMsg.InnerText = Err.ToString();
                DBErrMsg.Visible = true;
                RatingGrid.Visible = false;
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