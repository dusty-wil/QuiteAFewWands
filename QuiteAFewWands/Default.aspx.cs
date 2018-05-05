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

        public String ProductGridFilterExpression
        {
            get
            {
                if (ViewState["productGridFilterExpression"] == null)
                {
                    ViewState["productGridFilterExpression"] = "";
                }
                return (String)ViewState["productGridFilterExpression"];
            }

            set
            {
                ViewState["productGridFilterExpression"] = value;
            }
        }

        public int ProductGridFilterVal
        {
            get
            {
                if (ViewState["productGridFilterVal"] == null)
                {
                    ViewState["productGridFilterVal"] = 0;
                }
                return (int)ViewState["productGridFilterVal"];
            }

            set
            {
                ViewState["productGridFilterVal"] = value;
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



        protected void Page_Load(object sender, EventArgs e)
        {

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
         * Base function to retrieve product data for the grid
         * returns a DataSet for manipulation
         */
        private DataSet GetProductData()
        {
            DBErrMsg.Visible = false;
            ProductGrid.Visible = false;

            DataSet ds = new DataSet();

            String connectionString = WebConfigurationManager.ConnectionStrings["Lab2"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            String sql =
                "SELECT " +
                    "I.Id AS ItemId, " +
                    "I.Name, " +
                    "I.Height, " +
                    "I.Width, " +
                    "I.Price, " +
                    "I.IntroDate AS Year, " +
                    "I.BrandId, " +
                    "B.Name AS Brand, " +
                    "I.CategoryId, " +
                    "C.Name AS Category, " +
                    "I.Description " +
                "FROM Item AS I " +
                "INNER JOIN Brand AS B ON B.Id = I.BrandId " +
                "INNER JOIN Category AS C ON C.Id = I.CategoryId "
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

            if (ProductGridFilterVal != 0 && ProductGridFilterExpression != "")
            {
                dv.RowFilter = ProductGridFilterExpression + " = " + ProductGridFilterVal;
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
         * This fills the wood type list.
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
         * This fills the wood type list.
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
    }
}
