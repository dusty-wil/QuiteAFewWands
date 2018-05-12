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
    public partial class AddEditCoreType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox1.Visible = false;
            Label1.Visible = false;
            DBErrorLabel.Visible = false;
            preTextBoxLabel.Visible = false;
            saveButton.Visible = false;
            ddlCoreType.Visible = false;
            deleteCoreType.Visible = false;
            updateCoreType.Visible = false;
        }


        /* ----- IF "INSERT CORE TYPE" BUTTON IS CLICKED ----- */
        protected void insertButton_Click(object sender, EventArgs e)
        {
            TextBox1.Visible = true; //make textbox visible
            preTextBoxLabel.Visible = true; //make label before textbox visible
            saveButton.Visible = true; //make save button visible
            preTextBoxLabel.Text = "Core Type Value:";

        }


        protected void saveButton_Click(object sender, EventArgs e)
        {
            // create connection object
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            //create as CommandBehavior object
            String cmdString = "INSERT INTO [CORETYPE] (CoreTypeName) VALUES (@CTypeName)";
            SqlCommand cmd = new SqlCommand(cmdString, con);

            //create parameter object and add its value
            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@CTypeName";
            param1.Value = TextBox1.Text;
            cmd.Parameters.Add(param1);

            int added = 0;

            try
            {
                con.Open();
                added = cmd.ExecuteNonQuery();
                Label1.Text = "Added " + added.ToString() + " records.";
                TextBox1.Text = "";
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


        /* ----- IF "DELETE CORE TYPE" BUTTON IS CLICKED ----- */
        protected void deleteButton_Click(object sender, EventArgs e)
        {
            ddlCoreType.Visible = true; // make ddlCoreType visible
            deleteCoreType.Visible = true; // make delete button visible

            //fill DDL from DB values
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
                ddlCoreType.DataSource = ds;
                ddlCoreType.DataTextField = "CoreTypeName";
                ddlCoreType.DataValueField = "Id";
                ddlCoreType.DataBind();

                ddlCoreType.Items.Insert(0, new ListItem("--Select--", "0"));
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


        protected void deleteCoreType_Click(object sender, EventArgs e)
        {
            // create connection object
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            //create as CommandBehavior object
            String cmdString = "DELETE FROM [CORETYPE] WHERE CoreTypeName = @CTypeName";
            SqlCommand cmd = new SqlCommand(cmdString, con);

            //create parameter object and add its value
            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@CTypeName";
            param1.Value = ddlCoreType.SelectedItem.Text;
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


        /* ----- IF "UPDATE CORE TYPE" BUTTON IS CLICKED ----- */
        protected void updateButton_Click(object sender, EventArgs e)
        {
            ddlCoreType.Visible = true; // make DDL visible
            preTextBoxLabel.Visible = true;
            preTextBoxLabel.Text = "Updated Core Type Value: ";
            TextBox1.Visible = true;
            updateCoreType.Visible = true;

            //fill DDL from DB values
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
                ddlCoreType.DataSource = ds;
                ddlCoreType.DataTextField = "CoreTypeName";
                ddlCoreType.DataValueField = "Id";
                ddlCoreType.DataBind();

                ddlCoreType.Items.Insert(0, new ListItem("--Select--", "0"));
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


        protected void updateCoreType_Click(object sender, EventArgs e)
        {
            // create connection object
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);


            //create as CommandBehavior object
            String cmdString = "UPDATE [CORETYPE] SET CoreTypeName = @CTypeName WHERE Id = @CTypeID";
            SqlCommand cmd = new SqlCommand(cmdString, con);

            //create param object and add its value
            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@CTypeID";
            param1.Value = ddlCoreType.SelectedItem.Value;
            cmd.Parameters.Add(param1);

            SqlParameter param2 = new SqlParameter();
            param2.ParameterName = "@CTypeName";
            param2.Value = TextBox1.Text;
            cmd.Parameters.Add(param2);

            int updated = 0;
            try
            {
                con.Open();
                updated = cmd.ExecuteNonQuery();

                Label1.Visible = true;
                Label1.Text = "Updated " + updated.ToString() + " records.";
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