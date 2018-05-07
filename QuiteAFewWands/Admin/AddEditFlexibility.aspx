<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddEditFlexibility.aspx.cs" Inherits="QuiteAFewWands.Admin.AddEditFlexibility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titlePlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContentPlaceholder" runat="server">
    <form id="form1" runat="server">
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Insert New Flexibility" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Text="Update Existing Flexibility" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button3" runat="server" Text="Delete Existing Flexibility" />
        <br />
        <br />
        <br />
        <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
        <br />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="# records added/updated/deleted."></asp:Label>
        <br />
        <br />
        <asp:Label ID="DBErrorLabel" runat="server" Text="DB Error Message Here"></asp:Label>
    </form>
</asp:Content>
