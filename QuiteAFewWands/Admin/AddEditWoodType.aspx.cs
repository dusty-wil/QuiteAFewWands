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
    public partial class AddEditWoodType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox1.Visible = false;
            Label1.Visible = false;
            DBErrorLabel.Visible = false;
            preTextBoxLabel.Visible = false;
            saveButton.Visible = false;
            ddlWoodType.Visible = false;
            deleteWood.Visible = false;
            updateWood.Visible = false;
        }


        /* ----- IF "INSERT WOOD TYPE" BUTTON IS CLICKED ----- */
        protected void insertButton_Click(object sender, EventArgs e)
        {
            TextBox1.Visible = true; //make textbox visible
            preTextBoxLabel.Visible = true; //make label before textbox visible
            saveButton.Visible = true; //make save button visible
            preTextBoxLabel.Text = "Wood Type Name:";
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            // create connection object
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            //create as CommandBehavior object
            String cmdString = "INSERT INTO [WOODTYPE] (WoodTypeName) VALUES (@WTypeName)";
            SqlCommand cmd = new SqlCommand(cmdString, con);

            //create parameter object and add its value

            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@WTypeName";
            param1.Value = TextBox1.Text;
            cmd.Parameters.Add(param1);

            int added = 0;
            
            try
            {
                con.Open();
                added = cmd.ExecuteNonQuery();
                Label1.Text = "Added " + added.ToString() + " records.";
                Label1.Visible = true;
                TextBox1.Text = "";
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


        /* ----- IF "DELETE WOOD TYPE" BUTTON IS CLICKED ----- */
        protected void deleteButton_Click(object sender, EventArgs e)
        {
            ddlWoodType.Visible = true; // make ddlCoreType visible
            deleteWood.Visible = true; // make delete button visible


            //fill DDL from DB values
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


        protected void deleteWood_Click(object sender, EventArgs e)
        {
            // create connection object
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            //create as CommandBehavior object
            String cmdString = "DELETE FROM [WOODTYPE] WHERE WoodTypeName = @WTypeName";
            SqlCommand cmd = new SqlCommand(cmdString, con);

            //create parameter object and add its value
            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@WTypeName";
            param1.Value = ddlWoodType.SelectedItem.Text;
            cmd.Parameters.Add(param1);

            int deleted = 0;
            try
            {
                con.Open();
                deleted = cmd.ExecuteNonQuery();
                Label1.Text = "Deleted " + deleted.ToString() + " records.";
                Label1.Visible = true;
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


        /* ----- IF "UPDATE WOOD TYPE" BUTTON IS CLICKED ----- */
        protected void updateButton_Click(object sender, EventArgs e)
        {
            ddlWoodType.Visible = true; // make DDL visible
            preTextBoxLabel.Visible = true;
            preTextBoxLabel.Text = "Updated Wood Type Name: ";
            TextBox1.Visible = true;
            updateWood.Visible = true;

            //fill DDL from DB values
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


        protected void updateWood_Click(object sender, EventArgs e)
        {
            // create connection object
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            //create as CommandBehavior object
            String cmdString = "UPDATE [WOODTYPE] SET WoodTypeName = @WTypeName WHERE Id = @WTypeID";
            SqlCommand cmd = new SqlCommand(cmdString, con);

            //create param object and add its value
            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@WTypeID";
            param1.Value = ddlWoodType.SelectedItem.Value;
            cmd.Parameters.Add(param1);

            SqlParameter param2 = new SqlParameter();
            param2.ParameterName = "@WTypeName";
            param2.Value = TextBox1.Text;
            cmd.Parameters.Add(param2);

            int updated = 0;
            try
            {
                con.Open();
                updated = cmd.ExecuteNonQuery();
                Label1.Text = "Updated " + updated.ToString() + " records.";
                Label1.Visible = true;
                TextBox1.Text = "";
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