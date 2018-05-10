<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ViewCart.aspx.cs" Inherits="QuiteAFewWands.ViewCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titlePlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContentPlaceholder" runat="server">
    <div class="non-semantic-protector">    
        <h1 class="ribbon">
            <span class="ribbon-content">View Cart!</span>
        </h1>
    </div>
    <div class="body-content">
        <form id="CartForm" runat="server">
            <div id="OkMsg" class="message message-ok" runat="server"></div>
            <div id="DBErrMsg" class="message message-error" runat="server"></div>
            
            <ul class="cart-list">
                <asp:Repeater runat="server" ID="CartListRepeater">
                    <ItemTemplate> 
                        <li>
                            <span class="cart-item-name"><%# Eval("ItemName") %></span> 
                            <span class='cart-item-price-spacer'>. . . . . . . . . . . . . . . . . . . . . .</span>
                            <span class="cart-item-price"><%# Eval("ItemPrice") %></span>
                            <span class="cart-item-num-spacer">x</span>
                            <span class="cart-item-num"><%# Eval("Quantity") %></span>
                            <span class="cart-item-remove">
                                <a href="ViewCart.aspx?RemoveWandFromCart=<%# Eval("ItemId") %>">Remove</a>
                            </span>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
            <div class="cart-total-container">
                Total: $<asp:Label ID="TotalLbl" runat="server" Text="0.00" CssClass="cart-total-val"></asp:Label>
                <a href="Checkout.aspx" class="cta-btn">Checkout</a>
            </div>
            <div class="cart-checkout-container"></div>
        </form>
    </div>

</asp:Content>
