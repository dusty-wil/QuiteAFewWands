﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Text;

namespace QuiteAFewWands.Admin
{
    public partial class AddEditWand : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsAdminCheck())
            {
                Response.Redirect("../Default.aspx");
            }

            Label1.Visible = false;

            DBErrorLabel.Visible = false;

            if (!IsPostBack)
            {
                FillWoodTypeDDL();
                FillCoreTypeDDL();
                FillFlexTypeDDL();
                FillCountryDDL();
                FillWandDDL();
            }

        }


        /**
         * fill current wand DDL
         */
        private void FillWandDDL()
        {
            WandsDDL.Items.Clear();

            ListItem newItem = new ListItem
            {
                Text = "----Add New----",
                Value = "0"
            };
            WandsDDL.Items.Add(newItem);

            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            String sql = "SELECT Id, Name FROM [WAND]";
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
                        Text = rd["Name"].ToString(),
                        Value = rd["Id"].ToString()
                    };
                    WandsDDL.Items.Add(newItem);
                }
                rd.Close();
            }
            catch (Exception err)
            {
                DBErrorLabel.Visible = true;
                DBErrorLabel.Text = err.Message;
            }
            finally
            {
                con.Close();
            }
        }


        /**
         * fill the wood type DDL
         */
        private void FillWoodTypeDDL()
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            String cmdString = "SELECT Id, WoodTypeName FROM [WOODTYPE] ";
            SqlCommand cmd = new SqlCommand(cmdString, con);
            try
            {
                con.Open();


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                da.Fill(ds);
                ddlWoodType.DataSource = ds;
                ddlWoodType.DataTextField = "WoodTypeName";
                ddlWoodType.DataValueField = "Id";
                ddlWoodType.DataBind();

                ddlWoodType.Items.Insert(0, new ListItem("--Select--", "0"));

            }

            catch (Exception err)
            {
                DBErrorLabel.Visible = true;
                DBErrorLabel.Text = err.Message;
            }

            finally
            {
                con.Close();
            }
        }
           

        /**
         * fill the core type DDL
         */
        private void FillCoreTypeDDL()
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            String cmdString = "SELECT Id, CoreTypeName FROM [CORETYPE] ";
            SqlCommand cmd = new SqlCommand(cmdString, con);
            try
            {
                con.Open();


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                da.Fill(ds);
                coreTypeDDL.DataSource = ds;
                coreTypeDDL.DataTextField = "CoreTypeName";
                coreTypeDDL.DataValueField = "Id";
                coreTypeDDL.DataBind();

                coreTypeDDL.Items.Insert(0, new ListItem("--Select--", "0"));

            }

            catch (Exception err)
            {
                DBErrorLabel.Visible = true;
                DBErrorLabel.Text = err.Message;
            }

            finally
            {
                con.Close();
            }
        }


        /**
         * fill the flex type DDL
         */
        private void FillFlexTypeDDL()
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            String cmdString = "SELECT Id, FlexibilityValue FROM [FLEXIBILITY] ";
            SqlCommand cmd = new SqlCommand(cmdString, con);
            try
            {
                con.Open();


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                da.Fill(ds);
                flexTypeDDL.DataSource = ds;
                flexTypeDDL.DataTextField = "FlexibilityValue";
                flexTypeDDL.DataValueField = "Id";
                flexTypeDDL.DataBind();

                flexTypeDDL.Items.Insert(0, new ListItem("--Select--", "0"));

            }

            catch (Exception err)
            {
                DBErrorLabel.Visible = true;
                DBErrorLabel.Text = err.Message;
            }

            finally
            {
                con.Close();
            }
        }


        /**
         * fill the country DDL
         */
        private void FillCountryDDL()
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            String cmdString = "SELECT Id, CountryName FROM [COUNTRY] ";
            SqlCommand cmd = new SqlCommand(cmdString, con);
            try
            {
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                da.Fill(ds);
                countryDDL.DataSource = ds;
                countryDDL.DataTextField = "CountryName";
                countryDDL.DataValueField = "Id";
                countryDDL.DataBind();

                countryDDL.Items.Insert(0, new ListItem("--Select--", "0"));

            }

            catch (Exception err)
            {
                DBErrorLabel.Visible = true;
                DBErrorLabel.Text = err.Message;
            }

            finally
            {
                con.Close();
            }
        }


        protected void WandsDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            int WandID = 0;

            int.TryParse(((DropDownList)sender).SelectedValue, out WandID);

            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            String cmdString = "SELECT " +
                "Id, " +
                "Name, " +
                "WoodId, " +
                "CoreId, " +
                "FlexibilityId, " +
                "CountryId, " +
                "Length, " +
                "Weight, " +
                "Price, " +
                "Description " +
            "FROM Wand " +
                "WHERE Id = @WandId"
            ;
            SqlCommand cmd = new SqlCommand(cmdString, con);

            SqlDataReader rd;

            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@WandId", WandID);
                rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    TextBox1.Text = rd["Name"].ToString();
                    ddlWoodType.SelectedValue = rd["WoodId"].ToString();
                    coreTypeDDL.SelectedValue = rd["CoreId"].ToString();
                    flexTypeDDL.SelectedValue = rd["FlexibilityId"].ToString();
                    countryDDL.SelectedValue = rd["CountryId"].ToString();
                    TextBox2.Text = rd["Length"].ToString();
                    TextBox3.Text = rd["Weight"].ToString();
                    TextBox4.Text = rd["Price"].ToString();
                    TextBox5.Text = rd["Description"].ToString();
                }
                rd.Close();
            }

            catch (Exception err)
            {
                DBErrorLabel.Visible = true;
                DBErrorLabel.Text = err.Message;
            }

            finally
            {
                con.Close();
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            String cmdString;
            bool adding = (WandsDDL.SelectedIndex == 0);

            if (adding)
            {
                cmdString = "INSERT INTO [WAND] (" +
                    "Name, " +
                    "WoodId, " +
                    "CoreId, " +
                    "FlexibilityId, " +
                    "CountryId, " +
                    "Length, " +
                    "Weight, " +
                    "Price, " +
                    "DateCreated," +
                    "Description " +
                ") VALUES ( " +
                    "@wandName, " +
                    "@woodID, " +
                    "@coreID, " +
                    "@flexID, " +
                    "@countryID, " +
                    "@length, " +
                    "@weight, " +
                    "@price, " +
                    "SYSDATETIME(), " +
                    "@desc " +
                ")";
            }
            else
            {
                cmdString = "UPDATE [WAND] SET " +
                    "Name = @wandName," +
                    "WoodId = @woodID," +
                    "CoreId = @coreID," +
                    "FlexibilityId = @flexID, " +
                    "CountryId = @countryID, " +
                    "Length = @length, " +
                    "Weight = @weight, " +
                    "Price = @price, " +
                    "DateCreated = SYSDATETIME()," +
                    "Description = @desc " +
                   "WHERE Id = @wandId";
            }

            SqlCommand cmd = new SqlCommand(cmdString, con);

            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@wandName";
            param1.Value = TextBox1.Text;
            cmd.Parameters.Add(param1);

            SqlParameter param2 = new SqlParameter();
            param2.ParameterName = "@woodID";
            param2.Value = ddlWoodType.SelectedValue;
            cmd.Parameters.Add(param2);

            SqlParameter param3 = new SqlParameter();
            param3.ParameterName = "@coreID";
            param3.Value = coreTypeDDL.SelectedValue;
            cmd.Parameters.Add(param3);

            SqlParameter param4 = new SqlParameter();
            param4.ParameterName = "@flexID";
            param4.Value = flexTypeDDL.SelectedValue;
            cmd.Parameters.Add(param4);

            SqlParameter param5 = new SqlParameter();
            param5.ParameterName = "@countryID";
            param5.Value = countryDDL.SelectedValue;
            cmd.Parameters.Add(param5);

            SqlParameter param6 = new SqlParameter();
            param6.ParameterName = "@length";
            param6.Value = TextBox2.Text;
            cmd.Parameters.Add(param6);


            SqlParameter param7 = new SqlParameter();
            param7.ParameterName = "@weight";
            param7.Value = TextBox3.Text;
            cmd.Parameters.Add(param7);

            SqlParameter param8 = new SqlParameter();
            param8.ParameterName = "@price";
            param8.Value = TextBox4.Text;
            cmd.Parameters.Add(param8);

            SqlParameter param9 = new SqlParameter();
            param9.ParameterName = "@desc";
            param9.Value = TextBox5.Text;
            cmd.Parameters.Add(param9);

            if (!adding)
            {
                SqlParameter param10 = new SqlParameter();
                param10.ParameterName = "@wandID";
                param10.Value = WandsDDL.SelectedItem.Value;
                cmd.Parameters.Add(param10);
            }
            
            int recCount = 0;
            
            try
            {
                con.Open();
                recCount = cmd.ExecuteNonQuery();

                Label1.Text = ((adding) ? "Added " : "Edited ") + recCount.ToString() + " record.";
                Label1.Visible = true;

                TextBox1.Text = "";
                TextBox2.Text = "";
                TextBox3.Text = "";
                TextBox4.Text = "";
                TextBox5.Text = "";
                ddlWoodType.SelectedIndex = 0;
                coreTypeDDL.SelectedIndex = 0;
                flexTypeDDL.SelectedIndex = 0;
                countryDDL.SelectedIndex = 0;

                FillWandDDL();
            }

            catch (Exception err)
            {
                DBErrorLabel.Visible = true;
                DBErrorLabel.Text = err.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            if (WandsDDL.SelectedIndex == 0) return;

            int WandId = 0;
            if (!int.TryParse(WandsDDL.SelectedItem.Value, out WandId)) return;

            DeleteOrderLine(WandId);
            DeleteRating(WandId);
            DeleteComment(WandId);
            DeleteWand(WandId);

            Label1.Text = "Deleted record.";
            Label1.Visible = true;

            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            ddlWoodType.SelectedIndex = 0;
            coreTypeDDL.SelectedIndex = 0;
            flexTypeDDL.SelectedIndex = 0;
            countryDDL.SelectedIndex = 0;

            FillWandDDL();
        }

        private void DeleteWand(int WandId)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            String cmdString = "DELETE FROM Wand WHERE Id = @WandId";

            SqlCommand cmd = new SqlCommand(cmdString, con);

            try
            {
                con.Open();

                cmd.Parameters.AddWithValue("@WandId", WandId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception Err)
            {
                DBErrorLabel.Visible = true;
                DBErrorLabel.Text = Err.Message;
            }
            finally
            {
                con.Close();
            }
        }

        private void DeleteComment(int WandId)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            String cmdString = "DELETE FROM Comment WHERE WandId = @WandId";

            SqlCommand cmd = new SqlCommand(cmdString, con);

            try
            {
                con.Open();

                cmd.Parameters.AddWithValue("@WandId", WandId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception Err)
            {
                DBErrorLabel.Visible = true;
                DBErrorLabel.Text = Err.Message;
            }
            finally
            {
                con.Close();
            }
        }

        private void DeleteRating(int WandId)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            String cmdString = "DELETE FROM Rating WHERE WandId = @WandId";

            SqlCommand cmd = new SqlCommand(cmdString, con);

            try
            {
                con.Open();

                cmd.Parameters.AddWithValue("@WandId", WandId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception Err)
            {
                DBErrorLabel.Visible = true;
                DBErrorLabel.Text = Err.Message;
            }
            finally
            {
                con.Close();
            }
        }


        private void DeleteOrderLine(int WandId)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            String cmdString = "DELETE FROM OrderLine WHERE WandId = @WandId";

            SqlCommand cmd = new SqlCommand(cmdString, con);

            try
            {
                con.Open();

                cmd.Parameters.AddWithValue("@WandId", WandId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception Err)
            {
                DBErrorLabel.Visible = true;
                DBErrorLabel.Text = Err.Message;
            }
            finally
            {
                con.Close();
            }
        }


        private bool IsAdminCheck()
        {
            int IsAdmin = 0;

            if (Session["user_isadmin"] != null)
            {
                int.TryParse(Session["user_isadmin"].ToString(), out IsAdmin);
            }

            return IsAdmin == 1;
        }
    }
}