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

            //make error lable not visible on page load
            Label1.Visible = false;

            //fill in CoreType DDL
            fillCoreTypeList();

            // create connection object
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);


            try
            {
                //open connection
                con.Open();
                Label1.Text = "connection is now: " + con.State.ToString();
                
            }
            catch (Exception err)
            {
                Label1.Text = err.Message;
            }

            finally
            {
                con.Close();
                Label1.Text += "now connection is: " + con.State.ToString();
            }
        }




        /* ----- FILL CATEGORY LIST ----- */
        private void fillCoreTypeList()
        {
            ddlCoreType.Items.Clear();

            ListItem newItem = new ListItem
            {
                Text = "All",
                Value = "0"
            };

            ddlCoreType.Items.Add(newItem);


            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            String selectSQL = "SELECT Id, CoreTypeName FROM [CoreType]";


            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader reader;

            try
            {
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    newItem = new ListItem
                    {

                        Text = reader["CoreTypeName"].ToString(),
                        Value = reader["Id"].ToString()
                    };
                    ddlCoreType.Items.Add(newItem);

                }
                reader.Close();
            }
            catch (Exception err)
            {
                Label1.Visible = true;
                Label1.Text = err.Message;
            }
            finally
            {
                con.Close();
            }
        }













        /* ----- ADD ----- */
        protected void Button1_Click(object sender, EventArgs e)
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
                Label2.Text = "Added " + added.ToString() + " records.";
            }

            catch (Exception err)
            {
                Label1.Text = err.Message;
            }

            finally
            {
                con.Close();
            }
        }




        /* ----- UPDATE (not working) ----- */
        protected void Button2_Click(object sender, EventArgs e)
        {
            // create connection object
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            //create as CommandBehavior object
            String cmdString = "UPDATE [CORETYPE] SET CoreType = @CTypeName WHERE CoreTypeName = @CTypeName ";
            SqlCommand cmd = new SqlCommand(cmdString, con);

            //create parameter object and add its value
            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@CTypeName";
            param1.Value = TextBox1.Text;
            cmd.Parameters.Add(param1);

            int updated = 0;
            try
            {
                con.Open();
                updated = cmd.ExecuteNonQuery();
                Label2.Text = "Updated " + updated.ToString() + " records.";
            }

            catch (Exception err)
            {
                Label1.Text = err.Message;
            }

            finally
            {
                con.Close();
            }
        }

       


        /* ----- DELETE ----- */
        protected void Button3_Click(object sender, EventArgs e)
        {
            // create connection object
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            //create as CommandBehavior object
            String cmdString = "DELETE FROM [CORETYPE] WHERE CoreTypeName = @CTypeName ";
            SqlCommand cmd = new SqlCommand(cmdString, con);

            //create parameter object and add its value
            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@CTypeName";
            param1.Value = TextBox1.Text;
            cmd.Parameters.Add(param1);

            int deleted = 0;
            try
            {
                con.Open();
                deleted = cmd.ExecuteNonQuery();
                Label2.Text = "Deleted " + deleted.ToString() + " records.";
            }

            catch (Exception err)
            {
                Label1.Text = err.Message;
            }

            finally
            {
                con.Close();
            }
            
        }


        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }


    }
}