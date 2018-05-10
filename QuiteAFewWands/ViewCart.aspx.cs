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
        private List<int> SessionCart;
        private List<CartItem> CartItems;


        protected void Page_Load(object sender, EventArgs e)
        {
            OkMsg.Visible = false;
            DBErrMsg.Visible = false;

            if (!IsPostBack)
            { 
                CartItems = new List<CartItem>();

                if (Session["cart"] != null)
                {
                    SessionCart = (List<int>)Session["cart"];
                    if (SessionCart.Capacity > 0)
                    {
                        int removeWand = 0;
                        if (int.TryParse(Request.QueryString["RemoveWandFromCart"], out removeWand))
                        {
                            if (removeWand > 0)
                            {
                                RemoveWandFromCart(removeWand);
                            }
                        }

                        GenerateCartItemsFromSession();   
                    }
                }
                else
                {
                    SessionCart = new List<int>();
                }
            }

            UpdateCartList();
            UpdateCartTotal();
        }


        /**
         * This is messy and I don't like it. But, I can't think of a better way right now
         * Session['cart'] is a list of ints. There are duplicates, IE more than one of that
         * item is being ordered. So, break them down to unique instances, get names and 
         * prices, store in a list of classes
         */ 
        private void GenerateCartItemsFromSession()
        {
            Dictionary<int, int> cartCounts = new Dictionary<int, int>();
            Dictionary<int, string> cartNames = new Dictionary<int, string>();
            Dictionary<int, float> cartPrices = new Dictionary<int, float>();

            foreach (int w in SessionCart)
            {
                if (cartCounts.ContainsKey(w))
                {
                    cartCounts[w] += 1;
                }
                else
                {
                    cartCounts.Add(w, 1);
                    GetNameAndPriceForWand(w, cartNames, cartPrices);
                }
            }

            foreach (int item in SessionCart.Distinct())
            {
                CartItem ci = new CartItem();
                ci.ItemId = item;
                ci.ItemName = cartNames[item];
                ci.ItemPrice = cartPrices[item];
                ci.Quantity = cartCounts[item];

                CartItems.Add(ci);
            }
        }


        private void GetNameAndPriceForWand(int WandId, Dictionary<int, string> cartNames, Dictionary<int, float> cartPrices)
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
                    cartNames.Add(WandId, rd["Name"].ToString());

                    float price = 0;

                    float.TryParse(rd["Price"].ToString(), out price);
                    cartPrices.Add(WandId, price);
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


        /**
         * this is probably unnecessary, but in case things get more complicated later...
         */
        private void RemoveWandFromCart(int WandId)
        {
            SessionCart.Remove(WandId);
        }


        private void UpdateCartTotal()
        {
            float total = 0;
            foreach (CartItem item in CartItems)
            {
                total += (item.Quantity * item.ItemPrice);
            }

            CheckoutBtn.Visible = (total > 0);

            TotalLbl.Text = String.Format("{0:C}", total.ToString());
        }


        private void UpdateCartList()
        {
            // if (SessionCart.Capacity == 0)
            // {
            //     return;
            // }

            // foreach (int item in SessionCart.Distinct())
            // {
                CartListRepeater.DataSource = CartItems;
                CartListRepeater.DataBind();
            // }
        }
    }
}