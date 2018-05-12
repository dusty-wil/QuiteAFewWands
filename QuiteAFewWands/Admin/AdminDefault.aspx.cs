using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuiteAFewWands.Admin
{
    public partial class AdminDefault : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsAdminCheck())
            {
                Response.Redirect("../Default.aspx");
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