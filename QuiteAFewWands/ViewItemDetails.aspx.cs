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
    public partial class ViewItemDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBErrMsg.Visible = false;
            OkMsg.Visible = false;

            int WandId = 0;

            if (!int.TryParse(Request.QueryString["WandId"], out WandId))
            {
                DBErrMsg.InnerText = "Invalid Wand ID";
                DBErrMsg.Visible = true;
                return;
            }

            GetWandData(WandId);

            if (!IsPostBack)
            {
                if (Request.QueryString["AddWandToCart"] != null)
                {

                    int pickedWand = 0;
                    if (int.TryParse(Request.QueryString["AddWandToCart"], out pickedWand))
                    {
                        if (pickedWand > 0)
                        {
                            AddWandToCart(pickedWand);
                        }
                    }
                }
            }
        }


        /**
         * I'm using a repeater here, even though it's only one record.
         * That's because I want to leave the option open to get more 
         * than one record on this page later if necessary.
         * 
         */ 
        private void GetWandData(int WandId)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            DataSet ds = new DataSet();

            try
            {
                con.Open();

                String sql =
                    "SELECT " +
                        "w.Id WandId, " +
                        "w.Name,  " +
                        "wt.WoodTypeName, " +
                        "w.WoodId, " +
                        "ct.CoreTypeName, " +
                        "w.CoreId, " +
                        "f.FlexibilityValue, " +
                        "w.FlexibilityId, " +
                        "cn.CountryName,  " +
                        "w.CountryId, " +
                        "w.Length, " +
                        "w.Weight, " +
                        "w.DateCreated, " +
                        "w.Price  " +
                    "FROM Wand AS w " +
                    "INNER JOIN CoreType AS ct ON ct.Id = w.CoreId " +
                    "INNER JOIN WoodType AS wt ON wt.Id = w.WoodId " +
                    "INNER JOIN Flexibility AS f ON f.Id = w.FlexibilityId " +
                    "INNER JOIN Country AS cn ON cn.Id = w.CountryId " +
                    "WHERE " +
                        "w.Id = @WandId"
                ;

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@WandId", WandId);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                adp.Fill(ds, "WandDeets");
                ItemDetailListRepeater.DataSource = ds;
                ItemDetailListRepeater.DataBind();
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


        private void AddWandToCart(int WandId)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            String sql = "SELECT Name FROM Wand WHERE Id = @WandId";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataReader rd;

            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@WandId", WandId);
                rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    // yep we found a wand
                    List<int> tmpList = (Session["cart"] != null) ?
                        (List<int>)Session["cart"] :
                        new List<int>()
                    ;

                    tmpList.Add(WandId);
                    Session["cart"] = tmpList;

                    OkMsg.InnerText = "Added wand " + rd["Name"] + " to cart";
                    OkMsg.Visible = true;
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