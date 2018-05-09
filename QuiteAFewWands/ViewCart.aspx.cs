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
    public partial class ViewCart : System.Web.UI.Page
    {
        private List<int> _cart;
        private Dictionary<int, int> _cartCounts;
        private Dictionary<int, string> _cartNames;
        private Dictionary<int, float> _cartPrices;


        protected void Page_Load(object sender, EventArgs e)
        {
            OkMsg.Visible = false;
            DBErrMsg.Visible = false;

            if (!IsPostBack)
            {
                _cartCounts = new Dictionary<int, int>();
                _cartNames = new Dictionary<int, string>();
                _cartPrices = new Dictionary<int, float>();

                if (Session["cart"] != null)
                {
                    _cart = (List<int>)Session["cart"];
                    if (_cart.Capacity > 0)
                    {
                        foreach (int w in _cart)
                        {
                            if (_cartCounts.ContainsKey(w))
                            {
                                _cartCounts[w] += 1;
                            }
                            else
                            {
                                _cartCounts.Add(w, 1);
                                getNameAndPriceForWand(w);
                            }
                        }
                    }
                }
                else
                {
                    _cart = new List<int>();
                }
            }

            UpdateCartList();
        }

        private void getNameAndPriceForWand(int WandId)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            String sql = "SELECT Name, Price FROM Wand WHERE Id = @WandId";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataReader rd;

            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@WandId", WandId);
                rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    _cartNames.Add(WandId, rd["Name"].ToString());

                    float price = 0;

                    float.TryParse(rd["Price"].ToString(), out price);
                    _cartPrices.Add(WandId, price);
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

        private void UpdateCartList()
        {
            if (_cart.Capacity == 0)
            {
                return;
            }

            foreach (int item in _cart.Distinct())
            {
                ListItem li = new ListItem();
                li.Text = _cartNames[item] + " - " + _cartPrices[item] + " x " + _cartCounts[item];
                CartList.Items.Add(li);
            }
        }
    }
}