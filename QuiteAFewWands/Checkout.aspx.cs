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
    public partial class Checkout : System.Web.UI.Page
    {
        private List<int> SessionCart;
        private List<CartItem> CartItems;

        private float TotalAmt = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            OkMsg.Visible = false;
            DBErrMsg.Visible = false;

            
            CartItems = new List<CartItem>();

            if (Session["cart"] != null)
            {
                SessionCart = (List<int>)Session["cart"];
                if (SessionCart.Capacity > 0)
                {
                    GenerateCartItemsFromSession();
                }
                else
                {
                    Response.Redirect("ViewCart.aspx");
                }
            }
            else
            {
                Response.Redirect("ViewCart.aspx");
            }
            
            UpdateCartList();
            UpdateCartTotal();

            if (Session["user_firstname"] != null)
            {
                FirstNameTxtBox.Text = Session["user_firstname"].ToString();
            }
            if (Session["user_lastname"] != null)
            {
                LastNameTxtBox.Text = Session["user_lastname"].ToString();
            }
            if (Session["user_accid"] != null)
            {
                AccIdTxtBox.Text = Session["user_accid"].ToString();
            }
        }


        protected void CompleteBtn_Click(Object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int userId = 0;

                if (Session["user_id"] != null)
                {
                    if (!int.TryParse(Session["user_id"].ToString(), out userId))
                    {
                        userId = CreateAnonUser(
                            FirstNameTxtBox.Text,
                            LastNameTxtBox.Text,
                            AccIdTxtBox.Text
                        );
                    }
                }
                else
                {
                    userId = CreateAnonUser(
                        FirstNameTxtBox.Text,
                        LastNameTxtBox.Text,
                        AccIdTxtBox.Text
                    );
                }

                if (userId > 0)
                {
                    int orderId = CreateOrder(
                        userId,
                        CartItems
                    );

                    if (orderId > 0)
                    {
                        bool checkoutSuccessful = CreateOrderAddress(
                            Addr1TxtBox.Text,
                            Addr2TxtBox.Text,
                            CityTxtBox.Text,
                            StateTxtBox.Text,
                            ZipTxtBox.Text,
                            orderId
                        );

                        if (checkoutSuccessful)
                        {
                            OkMsg.InnerText = "Checkout complete! Expect our owl within 1-2 days!";
                            OkMsg.Visible = true;
                            Session.Remove("cart");
                        }
                        else
                        {
                            DBErrMsg.InnerText = "Unable to complete checkout at this time. Please try again later!";
                            DBErrMsg.Visible = true;
                        }

                        CheckoutCol1.Visible = false;
                        CheckoutCol2.Visible = false;
                        CheckoutListContainer.Visible = false;
                        CheckoutTotalContainer.Visible = false;
                    }
                    else
                    {
                        DBErrMsg.InnerText = "Unable to complete checkout at this time. Please try again later!";
                        DBErrMsg.Visible = true;

                        CheckoutCol1.Visible = false;
                        CheckoutCol2.Visible = false;
                        CheckoutListContainer.Visible = false;
                        CheckoutTotalContainer.Visible = false;
                    }

                }
                else
                {
                    DBErrMsg.InnerText = "Unable to complete checkout at this time. Please try again later!";
                    DBErrMsg.Visible = true;

                    CheckoutCol1.Visible = false;
                    CheckoutCol2.Visible = false;
                    CheckoutListContainer.Visible = false;
                    CheckoutTotalContainer.Visible = false;
                }

            }
            
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


        private void UpdateCartTotal()
        {
            float total = 0;
            foreach (CartItem item in CartItems)
            {
                total += (item.Quantity * item.ItemPrice);
            }

            TotalAmt = total;
            TotalLbl.Text = String.Format("{0:C}", total.ToString());
        }


        private void UpdateCartList()
        {
            CartListRepeater.DataSource = CartItems;
            CartListRepeater.DataBind();
        }


        /**
         * create an anonamous user account for checkout
         * returns created user id
         */
        private int CreateAnonUser(string FirstName, string LastName, string AccId)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            int userId = 0;

            String sql = "INSERT INTO [User] ( " +
                "FirstName, " +
                "LastName, " +
                "AccId, " +
                "HouseId, " +
                "IsAdmin, " +
                "DateAdded " +
            " ) OUTPUT INSERTED.Id VALUES ( " +
                "@FirstName, " +
                "@LastName, " +
                "@AccId, " +
                "5, " +
                "0, " +
                "SYSDATETIME() " +
            " ) "
            ;
            SqlCommand cmd = new SqlCommand(sql, con);

            try
            {
                con.Open();

                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@AccId", AccId);
                
                userId = (int)cmd.ExecuteScalar();
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

            return userId;
        }


        /**
         * creates an order and order lines
         * returns order id
         */ 
        private int CreateOrder(int UserId, List<CartItem> CartItems)
        {
            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            int orderId = 0;

            String sql = "INSERT INTO [Order] ( " +
                "UserId, " +
                "SaleDate " +
            " ) OUTPUT INSERTED.Id VALUES ( " +
                "@UserId, " +
                "SYSDATETIME() " +
            " ) "
            ;

            SqlCommand cmd = new SqlCommand(sql, con);

            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@UserId", UserId);

                orderId = (int)cmd.ExecuteScalar();
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

            if (orderId < 0)
            {
                DBErrMsg.InnerText = "Issue creating order";
                DBErrMsg.Visible = true;
                return 0;
            }

            sql = "INSERT INTO OrderLine ( " +
                "OrderId, " +
                "WandId, " +
                "AmountOrdered, " +
                "UnitPrice " +
            " ) VALUES ( " +
                "@OrderId, " +
                "@WandId, " +
                "@AmountOrdered, " +
                "@UnitPrice " +
            " ) "
            ;

            cmd = new SqlCommand(sql, con);

            SqlParameter OrderIdParam = new SqlParameter();
            OrderIdParam.ParameterName = "@OrderId";
            cmd.Parameters.Add(OrderIdParam);
            OrderIdParam.Value = orderId;

            SqlParameter WandIdParam = new SqlParameter();
            WandIdParam.ParameterName = "@WandId";
            cmd.Parameters.Add(WandIdParam);

            SqlParameter AmtParam = new SqlParameter();
            AmtParam.ParameterName = "@AmountOrdered";
            cmd.Parameters.Add(AmtParam);

            SqlParameter PriceParam = new SqlParameter();
            PriceParam.ParameterName = "@UnitPrice";
            cmd.Parameters.Add(PriceParam);

            foreach (CartItem item in CartItems)
            {
                try
                {
                    con.Open();

                    WandIdParam.Value = item.ItemId;
                    AmtParam.Value = item.Quantity;
                    PriceParam.Value = item.ItemPrice;

                    cmd.ExecuteNonQuery();
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

            return orderId;
        }

        private bool CreateOrderAddress(
            string addrLine1, string addrLine2, string city, 
            string state, string zip, int orderId
        )
        {
            bool success = true;

            String connectionString = WebConfigurationManager.ConnectionStrings["qafw"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            String sql = "INSERT INTO OrderAddress ( " +
                "AddrLine1, " +
                "AddrLine2, " +
                "City, " +
                "State, " +
                "Zip, " +
                "OrderId " +
            " ) VALUES ( " +
                "@AddrLine1, " +
                "@AddrLine2, " +
                "@City, " +
                "@State, " +
                "@Zip, " +
                "@OrderId " +
            " ) "
            ;
            SqlCommand cmd = new SqlCommand(sql, con);

            try
            {
                con.Open();

                cmd.Parameters.AddWithValue("@AddrLine1", addrLine1);
                cmd.Parameters.AddWithValue("@AddrLine2", addrLine2);
                cmd.Parameters.AddWithValue("@City", city);
                cmd.Parameters.AddWithValue("@State", state);
                cmd.Parameters.AddWithValue("@Zip", zip);
                cmd.Parameters.AddWithValue("@OrderId", orderId);

                cmd.ExecuteNonQuery();
            }
            catch (Exception Err)
            {
                DBErrMsg.InnerText += Err.ToString();
                DBErrMsg.Visible = true;
                success = false;
            }
            finally
            {
                con.Close();
            }

            return success;
        }
    }
}