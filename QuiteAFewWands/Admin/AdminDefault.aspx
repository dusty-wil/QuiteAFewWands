<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AdminDefault.aspx.cs" Inherits="QuiteAFewWands.Admin.AdminDefault" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titlePlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContentPlaceholder" runat="server">
    <div class="non-semantic-protector">
        <h1 class="ribbon">
            <span class="ribbon-content">Admin Area!</span>
        </h1>
    </div>
    <div class="body-content">
           <p class="admin-blurb">
               This is the admin area. Select one of the options 
               above to make changes to product categories and add new products.
           </p>
    </div>
</asp:Content>
