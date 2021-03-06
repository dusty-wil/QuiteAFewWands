﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddEditWoodType.aspx.cs" Inherits="QuiteAFewWands.Admin.AddEditWoodType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titlePlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContentPlaceholder" runat="server">
    
    <form id="form1" runat="server">

        <div class="admin-content-container">

            <asp:Button ID="insertButton" runat="server" OnClick="insertButton_Click" Text="Insert New Wood Type" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="updateButton" runat="server" OnClick="updateButton_Click" Text="Update Existing Wood Type" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="deleteButton" runat="server" OnClick="deleteButton_Click" Text="Delete Existing Wood Type" />
            <br />
            <br />
            <br />
            <br />
            <asp:DropDownList ID="ddlWoodType" runat="server">
            </asp:DropDownList>
            <asp:Button ID="deleteWood" runat="server" OnClick="deleteWood_Click" Text="Delete" />
            <br />
            <br />
            <asp:Label ID="preTextBoxLabel" runat="server" Text="Label"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Button ID="saveButton" runat="server" OnClick="saveButton_Click" Text="Save" />
            <asp:Button ID="updateWood" runat="server" OnClick="updateWood_Click" Text="Update" />
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
