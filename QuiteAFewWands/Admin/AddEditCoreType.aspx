<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddEditCoreType.aspx.cs" Inherits="QuiteAFewWands.Admin.AddEditCoreType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titlePlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContentPlaceholder" runat="server">
    <form id="AddEditCoreTypeForm" runat="server">
        
        <div class="admin-content-container">

            <asp:Button ID="insertButton" runat="server" OnClick="insertButton_Click" Text="Insert New Core Type" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="updateButton" runat="server" OnClick="updateButton_Click" Text="Update Existing Core Type" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="deleteButton" runat="server" OnClick="deleteButton_Click" Text="Delete Existing Core Type" />
            <br />
            <br />
            <br />
            <br />
            <asp:DropDownList ID="ddlCoreType" runat="server">
            </asp:DropDownList>
            <asp:Button ID="deleteCoreType" runat="server" Text="Delete" OnClick="deleteCoreType_Click" />
            <br />
            <br />
            <asp:Label ID="preTextBoxLabel" runat="server" Text="Label"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Button ID="saveButton" runat="server" Text="Save" OnClick="saveButton_Click" />
            <asp:Button ID="updateCoreType" runat="server" Text="Update" OnClick="updateCoreType_Click" />
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
