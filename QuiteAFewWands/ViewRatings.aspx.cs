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

                    e.Row.Cells[2].Text = "Avg: " + Math.Round((ratingsTotal/totalRatings), 2).ToString();
                }
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
                        "CONCAT(U.FirstName, ' ', U.LastName) AS Name, " +
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
    }
}