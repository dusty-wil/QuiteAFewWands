using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace QuiteAFewWands
{
    public partial class Login1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Btn_Enter_Click(object sender, EventArgs e)
        {
            SqlConnection SQLcon = new SqlConnection(ConfigurationManager.ConnectionStrings["qafw"].ConnectionString);
            SQLcon.Open();

            SqlCommand cmd = new SqlCommand("select * from [User] where UserName = '" + Txt_Username.Text + "' and Password = '" + Txt_Psw.Text + "'", SQLcon);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {

                Txt_Username.Text = "";
                Txt_Psw.Text = "";
            }
            SQLcon.Close();
        }
    }
}
