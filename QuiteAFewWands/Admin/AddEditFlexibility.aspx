<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddEditFlexibility.aspx.cs" Inherits="QuiteAFewWands.Admin.AddEditFlexibility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titlePlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContentPlaceholder" runat="server">
    <form id="AddEditFlexibilityForm" runat="server">

        <div class="admin-content-container">

            <asp:Button ID="insertButton" runat="server" OnClick="insertButton_Click" Text="Insert New Flexibility" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="updateButton" runat="server" Text="Update Existing Flexibility" OnClick="updateButton_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="deleteButton" runat="server" Text="Delete Existing Flexibility" OnClick="deleteButton_Click" />
            <br />
            <br />
            <br />
            <br />
            <asp:DropDownList ID="flexValueDDL" runat="server">
            </asp:DropDownList>
            <asp:Button ID="deleteFlex" runat="server" OnClick="deleteFlex_Click" style="margin-bottom: 0px" Text="Delete" />
            <br />
            <br />
            <asp:Label ID="preTextBoxLabel" runat="server" Text="Label"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Button ID="saveButton" runat="server" OnClick="saveButton_Click" Text="Save" />
            <asp:Button ID="updateFlex" runat="server" OnClick="updateFlex_Click" Text="Update" />
            <br />
            <br />
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="# records added/updated/deleted."></asp:Label>
            <br />
            <br />
            <asp:Label ID="DBErrorLabel" runat="server" Text="DB Error Message Here"></asp:Label>

        </div>
    </form>
</asp:Content>
