<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddEditWand.aspx.cs" Inherits="QuiteAFewWands.Admin.AddEditWand" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titlePlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContentPlaceholder" runat="server">
    <form id="form1" runat="server">
        <br />
        <br />
        Select Wand to Edit or Add New:
        <asp:DropDownList ID="WandsDDL" OnSelectedIndexChanged="WandsDDL_SelectedIndexChanged" AutoPostBack="true" runat="server" >
        </asp:DropDownList>
        <br />
        <br />
        Name:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        Wood Type:&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="ddlWoodType" runat="server">
        </asp:DropDownList>
        <br />
        Core Type:&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="coreTypeDDL" runat="server">
        </asp:DropDownList>
        <br />
        Flexibility Type:&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="flexTypeDDL" runat="server">
        </asp:DropDownList>
        <br />
        Country:&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="countryDDL" runat="server">
        </asp:DropDownList>
        <br />
        Length:&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br />
        Weight:&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <br />
        Price:&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
        <br />
        Description:&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Save" />
&nbsp;&nbsp;&nbsp;
        <br />
        <br />
        <asp:Label ID="DBErrorLabel" runat="server" Text="DB Error Message Here"></asp:Label>
    </form>
</asp:Content>
