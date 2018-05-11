<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="QuiteAFewWands.Checkout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titlePlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContentPlaceholder" runat="server">
    <div class="non-semantic-protector">
        <h1 class="ribbon">
            <span class="ribbon-content">Found something you like?</span>
        </h1>
    </div>
    
    <div class="body-content">

        <form id="CheckoutForm" runat="server">

            <div class="checkout-form-content">
                <p>
                    When your order is placed, the total amount shown below will be 
                    deducted from your Gringott's Bank account.
                </p>
                <p>
                    Our delivery owls will find your address and deliver your package 
                    within 1-2 days.
                </p>
                <p>
                    If you're not satisfied with the product, we offer refund and 
                    exchange services. Simply fill out the refund form included with
                    your package and send it along with the unwanted product via
                    return-owl.
                </p>

                <div id="OkMsg" class="message message-ok" runat="server"></div>
                <div id="DBErrMsg" class="message message-error" runat="server"></div>

                <div class="checkout-col1" runat="server" id="CheckoutCol1">
                    <asp:Label CssClass="checkout-form-lbl" ID="FirstNameLbl" runat="server" Text="First Name:"></asp:Label>
                    <asp:TextBox CssClass="formfield-text" ID="FirstNameTxtBox" runat="server"></asp:TextBox><br />

                    <asp:Label CssClass="checkout-form-lbl" ID="LastNameLbl" runat="server" Text="Last Name:"></asp:Label>
                    <asp:TextBox CssClass="formfield-text" ID="LastNameTxtBox" runat="server"></asp:TextBox><br />
            
                    <asp:Label CssClass="checkout-form-lbl" ID="AccIdLbl" runat="server" Text="Gringott's Account ID:"></asp:Label>
                    <asp:TextBox CssClass="formfield-text" ID="AccIdTxtBox" runat="server"></asp:TextBox>
                </div>
                <div class="checkout-col2" runat="server" id="CheckoutCol2">
                    <asp:Label CssClass="checkout-form-lbl" ID="Addr1Lbl" runat="server" Text="Street Address:"></asp:Label>
                    <asp:TextBox CssClass="formfield-text" ID="Addr1TxtBox" runat="server"></asp:TextBox><br />

                    <asp:Label CssClass="checkout-form-lbl" ID="Addr2Lbl" runat="server" Text="Apt/House Number:"></asp:Label>
                    <asp:TextBox CssClass="formfield-text" ID="Addr2TxtBox" runat="server"></asp:TextBox><br />

                    <asp:Label CssClass="checkout-form-lbl" ID="CityLbl" runat="server" Text="City:"></asp:Label>
                    <asp:TextBox CssClass="formfield-text" ID="CityTxtBox" runat="server"></asp:TextBox><br />

                    <asp:Label CssClass="checkout-form-lbl" ID="StateLbl" runat="server" Text="State:"></asp:Label>
                    <asp:TextBox CssClass="formfield-text" ID="StateTxtBox" runat="server"></asp:TextBox><br />

                    <asp:Label CssClass="checkout-form-lbl" ID="ZipLbl" runat="server" Text="Zip:"></asp:Label>
                    <asp:TextBox CssClass="formfield-text" ID="ZipTxtBox" runat="server"></asp:TextBox>
                </div>
            </div>
            
            <ul class="checkout-list" runat="server" id="CheckoutListContainer">
                <asp:Repeater runat="server" ID="CartListRepeater">
                    <ItemTemplate> 
                        <li>
                            <span class="checkout-item-name"><%# Eval("ItemName") %></span> 
                            <span class="checkout-item-price"><%# Eval("ItemPrice") %></span>
                            <span class="checkout-item-num-spacer">x</span>
                            <span class="checkout-item-num"><%# Eval("Quantity") %></span>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>

            <div class="checkout-total-container" runat="server" id="CheckoutTotalContainer"> 
                Total: $<asp:Label ID="TotalLbl" runat="server" Text="0.00" CssClass="checkout-total-val"></asp:Label>
                <asp:Button ID="CompleteBtn" OnClick="CompleteBtn_Click" runat="server" Text="Complete Order" CssClass="cta-btn" />
            </div>

        </form>
    </div>
</asp:Content>
