using System;
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
            fillWoodTypeDDL();
            fillCoreTypeDDL();
            fillFlexTypeDDL();
            fillCountryDDL();
            DBErrorLabel.Visible = false;
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





        //fill the wood type DDL
        private void fillWoodTypeDDL()
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
           
        //fill the core type DDL
        private void fillCoreTypeDDL()
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



        //fill the flex type DDL
        private void fillFlexTypeDDL()
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


        //fill the country DDL
        private void fillCountryDDL()
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
            int wandID = 0;

            int.TryParse(WandsDDL.SelectedValue, out wandID);

            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            String cmdString = "SELECT Id, Name, WoodId, CoreId, FlexibilityId, CountryId, Length, Weight, Price, " +
                "Description FROM [WAND] WHERE id = @id";
            SqlCommand cmd = new SqlCommand(cmdString, con);

            SqlDataReader rd;

            try
            {
                
                con.Open();
                cmd.Parameters.AddWithValue("@id", wandID);
                rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    TextBox1.Text = rd["Name"].ToString();
                    ddlWoodType.SelectedValue = rd["WoodId"].ToString();
                   
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
    }
}