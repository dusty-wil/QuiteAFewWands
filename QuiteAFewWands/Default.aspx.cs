﻿using System;
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
    public partial class Default : System.Web.UI.Page
    {
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";

        /**
         * Class properties are used to store the sort and
         * filter choices in the page's ViewState
         */

        public SortDirection ProductGridSortDirection
        {
            get
            {
                if (ViewState["productGridSortDirection"] == null)
                {
                    ViewState["productGridSortDirection"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["productGridSortDirection"];
            }

            set
            {
                ViewState["productGridSortDirection"] = value;
            }
        }

        public String ProductGridSortExpression
        {
            get
            {
                if (ViewState["productGridSortExpression"] == null)
                {
                    ViewState["productGridSortExpression"] = "";
                }
                return (String)ViewState["productGridSortExpression"];
            }

            set
            {
                ViewState["productGridSortExpression"] = value;
            }
        }

        public int ProductGridWoodTypeFilterVal
        {
            get
            {
                if (ViewState["productGridWoodTypeFilterVal"] == null)
                {
                    ViewState["productGridWoodTypeFilterVal"] = 0;
                }
                return (int)ViewState["productGridWoodTypeFilterVal"];
            }

            set
            {
                ViewState["productGridWoodTypeFilterVal"] = value;
            }
        }

        public int ProductGridCoreTypeFilterVal
        {
            get
            {
                if (ViewState["productGridCoreTypeFilterVal"] == null)
                {
                    ViewState["productGridCoreTypeFilterVal"] = 0;
                }
                return (int)ViewState["productGridCoreTypeFilterVal"];
            }

            set
            {
                ViewState["productGridCoreTypeFilterVal"] = value;
            }
        }

        public int ProductGridFlexibilityFilterVal
        {
            get
            {
                if (ViewState["productGridFlexibilityFilterVal"] == null)
                {
                    ViewState["productGridFlexibilityFilterVal"] = 0;
                }
                return (int)ViewState["productGridFlexibilityFilterVal"];
            }

            set
            {
                ViewState["productGridFlexibilityFilterVal"] = value;
            }
        }

        public int ProductGridCountryFilterVal
        {
            get
            {
                if (ViewState["productGridCountryFilterVal"] == null)
                {
                    ViewState["productGridCountryFilterVal"] = 0;
                }
                return (int)ViewState["productGridCountryFilterVal"];
            }

            set
            {
                ViewState["productGridCountryFilterVal"] = value;
            }
        }
       
        public int ProductGridPageIndex
        {
            get
            {
                if (ViewState["productGridPageIndex"] == null)
                {
                    ViewState["productGridPageIndex"] = 1;
                }
                return (int)ViewState["productGridPageIndex"];
            }

            set
            {
                ViewState["productGridPageIndex"] = value;
            }
        }


        /**
         * page load functions. fill the drop-downs and show the initial grid
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            OkMsg.Visible = false;
            DBErrMsg.Visible = false;

            if (!IsPostBack)
            {
                FillWoodTypeList();
                FillCoreTypeList();
                FillFlexibilityList();
                FillCountryList();

                UpdateProductGridView();

                if (Request.QueryString["AddWandToCart"] != null) {

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
         * This is triggered when someone picks a wood type to filter by.
         * Stores the filter variables then updates the grid
         */
        protected void WoodType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int selVal = int.Parse(((DropDownList)sender).SelectedValue);
                ProductGridWoodTypeFilterVal = selVal;
                UpdateProductGridView();
            }
            catch (FormatException fe)
            {
                Console.WriteLine(fe.ToString());
            }
        }


        /**
         * This is triggered when someone picks a core type to filter by.
         * Stores the filter variables then updates the grid
         */
        protected void CoreType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int selVal = int.Parse(((DropDownList)sender).SelectedValue);
                ProductGridCoreTypeFilterVal = selVal;
                UpdateProductGridView();
            }
            catch (FormatException fe)
            {
                Console.WriteLine(fe.ToString());
            }
        }


        /**
         * This is triggered when someone picks a flexibility to filter by.
         * Stores the filter variables then updates the grid
         */
        protected void Flexibility_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int selVal = int.Parse(((DropDownList)sender).SelectedValue);
                ProductGridFlexibilityFilterVal = selVal;
                UpdateProductGridView();
            }
            catch (FormatException fe)
            {
                Console.WriteLine(fe.ToString());
            }
        }


        /**
         * This is triggered when someone picks a country to filter by.
         * Stores the filter variables then updates the grid
         */
        protected void Country_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int selVal = int.Parse(((DropDownList)sender).SelectedValue);
                ProductGridCountryFilterVal = selVal;
                UpdateProductGridView();
            }
            catch (FormatException fe)
            {
                Console.WriteLine(fe.ToString());
            }
        }


        /**
         * This is the "on sort" action. Stores the sort info then
         * updates the grid
         */
        protected void ProductGV_Sorting(Object sender, GridViewSortEventArgs e)
        {
            ProductGridSortExpression = e.SortExpression;

            if (ProductGridSortDirection == SortDirection.Ascending)
            {
                ProductGridSortDirection = SortDirection.Descending;
            }
            else
            {
                ProductGridSortDirection = SortDirection.Ascending;
            }
            UpdateProductGridView();
        }


        /**
         * This is the "on page change" action. Stores the page number info then
         * updates the grid
         */
        protected void ProductGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ProductGrid.PageIndex = e.NewPageIndex;
            ProductGridPageIndex = e.NewPageIndex;

            UpdateProductGridView();
        }

        
        /**
         * TODO Handle showing stars for rating and/or hide comments link if none here
         *  
         */
        protected void ProductGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                
                // GetAvgRatingForWand(int WandId)
                // GetNumCommentsForWand(int WandId)
            }
        }


        /**
         * Base function to retrieve product data for the grid
         * returns a DataSet for manipulation
         */
        private DataSet GetProductData()
        {
            DBErrMsg.Visible = false;
            ProductGrid.Visible = false;

            DataSet ds = new DataSet();

            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

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
                    "w.Price,  " +
                    "ISNULL(AVG(r.Value), 0) AvgRating, " +
                    "COUNT(Distinct c.Id) CommentCount " +
                "FROM Wand AS w " +
                "INNER JOIN CoreType AS ct ON ct.Id = w.CoreId " +
                "INNER JOIN WoodType AS wt ON wt.Id = w.WoodId " +
                "INNER JOIN Flexibility AS f ON f.Id = w.FlexibilityId " +
                "INNER JOIN Country AS cn ON cn.Id = w.CountryId " +
                "LEFT JOIN Rating AS r ON r.WandId = w.Id " +
                "LEFT JOIN Comment AS c ON c.WandId = w.Id " +
                "GROUP BY " +
                    "w.Id, Name, WoodTypeName, WoodId, CoreTypeName, " +
                    "CoreId, FlexibilityValue, FlexibilityId, " +
                    "CountryName, CountryId, Price "
            ;

            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                con.Open();
                adp.Fill(ds);
            }
            catch (Exception Err)
            {
                DBErrMsg.InnerText = Err.ToString();
                DBErrMsg.Visible = true;
                ds = null;
            }
            finally
            {
                con.Close();
            }

            return ds;
        }


        /**
         * Update the Product GridView. First get the data. Then look at the
         * class properties to see if we need to sort/filter it.
         * Then, bind the data to the GridView.
         */
        private void UpdateProductGridView()
        {
            DataSet ds = GetProductData();

            if (ds == null)
            {
                DBErrMsg.InnerText = "Missing DataSet";
                DBErrMsg.Visible = true;
                ProductGrid.Visible = false;
                return;
            }
            DataTable dt = ds.Tables[0];
            DataView dv = new DataView(dt);

            String rf = "";

            if (ProductGridWoodTypeFilterVal != 0)
            {
                rf += " WoodId = " + ProductGridWoodTypeFilterVal;
            }
            if (ProductGridCoreTypeFilterVal != 0)
            {
                rf += ((rf != "") ? " AND " : "") + " CoreId = " + ProductGridCoreTypeFilterVal;
            }
           
            if (ProductGridFlexibilityFilterVal != 0)
            {
                rf += ((rf != "") ? " AND " : "") + " FlexibilityId = " + ProductGridFlexibilityFilterVal;
            }
            if (ProductGridCountryFilterVal != 0)
            {
                rf += ((rf != "") ? " AND " : "") + " CountryId = " + ProductGridCountryFilterVal;
            }
            
            if (rf != "")
            {
                dv.RowFilter = rf;
            }

            if (ProductGridSortExpression != "")
            {
                if (ProductGridSortDirection == SortDirection.Descending)
                {
                    dv.Sort = ProductGridSortExpression + DESCENDING;
                }
                else
                {
                    dv.Sort = ProductGridSortExpression + ASCENDING;
                }
            }

            ProductGrid.DataSource = dv;
            ProductGrid.DataBind();

            ProductGrid.Visible = true;
        }


        /**
         * This fills the wood type sort list.
         */
        private void FillWoodTypeList()
        {
            WoodTypeList.Items.Clear();

            ListItem newItem = new ListItem
            {
                Text = "All",
                Value = "0"
            };
            WoodTypeList.Items.Add(newItem);

            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            String sql = "SELECT Id, WoodTypeName FROM WoodType";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataReader rd;

            try
            {
                con.Open();
                rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    newItem = new ListItem
                    {
                        Text = rd["WoodTypeName"].ToString(),
                        Value = rd["Id"].ToString()
                    };
                    WoodTypeList.Items.Add(newItem);
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


        /**
         * This fills the wood type sort list.
         */
        private void FillCoreTypeList()
        {
            CoreTypeList.Items.Clear();

            ListItem newItem = new ListItem
            {
                Text = "All",
                Value = "0"
            };
            CoreTypeList.Items.Add(newItem);

            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            String sql = "SELECT Id, CoreTypeName FROM CoreType";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataReader rd;

            try
            {
                con.Open();
                rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    newItem = new ListItem
                    {
                        Text = rd["CoreTypeName"].ToString(),
                        Value = rd["Id"].ToString()
                    };
                    CoreTypeList.Items.Add(newItem);
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


        /**
         * This fills the flexibility sort list.
         */
        private void FillFlexibilityList()
        {
            FlexibilityList.Items.Clear();

            ListItem newItem = new ListItem
            {
                Text = "All",
                Value = "0"
            };
            FlexibilityList.Items.Add(newItem);

            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            String sql = "SELECT Id, FlexibilityValue FROM Flexibility";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataReader rd;

            try
            {
                con.Open();
                rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    newItem = new ListItem
                    {
                        Text = rd["FlexibilityValue"].ToString(),
                        Value = rd["Id"].ToString()
                    };
                    FlexibilityList.Items.Add(newItem);
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


        /**
         * This fills the country sort list.
         */
        private void FillCountryList()
        {
            CountryList.Items.Clear();

            ListItem newItem = new ListItem
            {
                Text = "All",
                Value = "0"
            };
            CountryList.Items.Add(newItem);

            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            String sql = "SELECT Id, CountryName FROM Country";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataReader rd;

            try
            {
                con.Open();
                rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    newItem = new ListItem
                    {
                        Text = rd["CountryName"].ToString(),
                        Value = rd["Id"].ToString()
                    };
                    CountryList.Items.Add(newItem);
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

        /**
         * maybe need this for something later
         * 
         */
        private float GetAvgRatingForWand(int WandId)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            String sql = "SELECT AVG(Value) AS AvgRating FROM Rating WHERE WandId = @WandId";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataReader rd;

            float avg = 0;

            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@WandId", WandId);
                rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    float.TryParse(rd["AvgRating"].ToString(), out avg);
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

            return avg;
        }


        /**
         * maybe need this for something later
         * 
         */ 
        private int GetNumCommentsForWand(int WandId)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            String sql = "SELECT COUNT(WandId) AS NumComments FROM Comment WHERE WandId = @WandId";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataReader rd;

            int cnt = 0;

            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@WandId", WandId);
                rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    int.TryParse(rd["NumComments"].ToString(), out cnt);
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

            return cnt;
        }

        /**
         * checks to see if session cart exists. If so, add
         * wand to cart. otherwise create new cart
         */
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

        private void AddUser()
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            String sql = "INSERT INTO [User] ( " + 
                "UserName, " + 
                "Password, " + 
                "FirstName, " + 
                "LastName, " + 
                "Email, " + 
                "HouseId, " + 
                "IsAdmin, " + 
                "DateAdded, " + 
                "DateLastLogin " +
            " ) VALUES ( " +
                "@UserName, " +
                "HASHBYTES('SHA2_256', @Password), " +
                "@FirstName, " +
                "@LastName, " +
                "@Email, " +
                "@HouseId, " +
                "@IsAdmin, " +
                "SYSDATETIME(), " +
                "SYSDATETIME() " +
            " ) "
            ;
            SqlCommand cmd = new SqlCommand(sql, con);

            try
            {
                con.Open();

                cmd.Parameters.AddWithValue("@UserName", "wil7722");
                cmd.Parameters.AddWithValue("@Password", "abc123");
                cmd.Parameters.AddWithValue("@FirstName", "Dusty");
                cmd.Parameters.AddWithValue("@LastName", "Williams");
                cmd.Parameters.AddWithValue("@Email", "wil7722@calu.edu");
                cmd.Parameters.AddWithValue("@HouseId", "2");
                cmd.Parameters.AddWithValue("@IsAdmin", "1");

                cmd.ExecuteNonQuery();
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
