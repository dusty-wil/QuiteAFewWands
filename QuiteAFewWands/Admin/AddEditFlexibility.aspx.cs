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
            TextBox1.Visible = false;
            Label1.Visible = false;
            DBErrorLabel.Visible = false;
            
            if (!IsPostBack)
            {

            }
        }




        /* ----- IF INSERT BUTTON IS CLICKED ----- */
        protected void Button1_Click(object sender, EventArgs e)
        {
            // create connection object
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            //create as CommandBehavior object
            String cmdString = "INSERT INTO [FLEXIBILITY] (FlexibilityValue) VALUES (@FlexValue)";
            SqlCommand cmd = new SqlCommand(cmdString, con);

            //create parameter object and add its value
            TextBox1.Visible = true; //make textbox visible
            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@FlexValue";
            param1.Value = TextBox1.Text;
            cmd.Parameters.Add(param1);

            int added = 0;
            Label1.Visible = true;

            try
            {
                con.Open();
                added = cmd.ExecuteNonQuery();
                Label1.Text = "Added " + added.ToString() + " records.";
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

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
    
}