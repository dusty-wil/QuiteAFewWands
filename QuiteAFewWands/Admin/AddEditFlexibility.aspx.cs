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
    public partial class AddEditFlexibility : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsAdminCheck())
            {
                Response.Redirect("../Default.aspx");
            }

            TextBox1.Visible = false;
            Label1.Visible = false;
            DBErrorLabel.Visible = false;
            preTextBoxLabel.Visible = false;
            saveButton.Visible = false;
            flexValueDDL.Visible = false;
            deleteFlex.Visible = false;
            updateFlex.Visible = false;
        }


        /* ----- IF "INSERT FLEXIBILITY" BUTTON IS CLICKED ----- */
        protected void insertButton_Click(object sender, EventArgs e)
        {
            TextBox1.Visible = true; //make textbox visible
            preTextBoxLabel.Visible = true; //make label before textbox visible
            saveButton.Visible = true; //make save button visible
            preTextBoxLabel.Text = "Flexibility Value:";
            
        }


        protected void saveButton_Click(object sender, EventArgs e)
        {
            // create connection object
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            //create as CommandBehavior object
            String cmdString = "INSERT INTO [FLEXIBILITY] (FlexibilityValue) VALUES (@FlexValue)";
            SqlCommand cmd = new SqlCommand(cmdString, con);

            //create parameter object and add its value
            
            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@FlexValue";
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


        /* ----- IF "DELETE FLEXIBILITY" BUTTON IS CLICKED ----- */
        protected void deleteButton_Click(object sender, EventArgs e)
        {
            flexValueDDL.Visible = true; // make flexValueDDL visible
            deleteFlex.Visible = true; // make delete button visible
            

            //fill DDL from DB values
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
                flexValueDDL.DataSource = ds;
                flexValueDDL.DataTextField = "FlexibilityValue";
                flexValueDDL.DataValueField = "Id";
                flexValueDDL.DataBind();

                flexValueDDL.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception err)
            {
                DBErrorLabel.Visible=true; 
                DBErrorLabel.Text = err.Message;
            }
            finally
            {
                con.Close();
            }
        }


        protected void deleteFlex_Click(object sender, EventArgs e)
        {
            // create connection object
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            //create as CommandBehavior object
            String cmdString = "DELETE FROM [FLEXIBILITY] WHERE FlexibilityValue = @flexValue";
            SqlCommand cmd = new SqlCommand(cmdString, con);

            //create parameter object and add its value
            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@flexValue";
            param1.Value = flexValueDDL.SelectedItem.Text;
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


        /* ----- IF "UPDATE FLEXIBILITY" BUTTON IS CLICKED ----- */
        protected void updateButton_Click(object sender, EventArgs e)
        {
            flexValueDDL.Visible = true; // make flexValueDDL visible
            preTextBoxLabel.Visible = true;
            preTextBoxLabel.Text = "Updated Flexibility Value: ";
            TextBox1.Visible = true;
            updateFlex.Visible = true;

            //fill DDL from DB values
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
                flexValueDDL.DataSource = ds;
                flexValueDDL.DataTextField = "FlexibilityValue";
                flexValueDDL.DataValueField = "Id";
                flexValueDDL.DataBind();

                flexValueDDL.Items.Insert(0, new ListItem("--Select--", "0"));
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



        protected void updateFlex_Click(object sender, EventArgs e)
        {
            // create connection object
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            //create as CommandBehavior object
            String cmdString = "UPDATE [FLEXIBILITY] SET FlexibilityValue = @FlexValue WHERE Id = @FlexID";
            SqlCommand cmd = new SqlCommand(cmdString, con);

            //create param object and add its value
            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@FlexID";
            param1.Value = flexValueDDL.SelectedItem.Value;
            cmd.Parameters.Add(param1);

            SqlParameter param2 = new SqlParameter();
            param2.ParameterName = "@FlexValue";
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