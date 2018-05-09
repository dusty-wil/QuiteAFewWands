<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ViewCart.aspx.cs" Inherits="QuiteAFewWands.ViewCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titlePlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContentPlaceholder" runat="server">
    <div class="non-semantic-protector">    
        <h1 class="ribbon">
            <span class="ribbon-content">Browse Wands!</span>
        </h1>
    </div>
    <div class="body-content">
        <form id="CartForm" runat="server">
            <div id="OkMsg" class="message message-ok" runat="server"></div>
            <div id="DBErrMsg" class="message message-error" runat="server"></div>
            <asp:BulletedList ID="CartList" runat="server"></asp:BulletedList>
        </form>
    </div>

</asp:Content>
