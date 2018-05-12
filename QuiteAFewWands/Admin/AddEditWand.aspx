<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddEditWand.aspx.cs" Inherits="QuiteAFewWands.Admin.AddEditWand" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titlePlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContentPlaceholder" runat="server">
    <form id="form1" runat="server">
        
        <div class="admin-content-container">

            Select Wand to Edit or Add New:
            <asp:DropDownList ID="WandsDDL" OnSelectedIndexChanged="WandsDDL_SelectedIndexChanged" AutoPostBack="true" runat="server" >
            </asp:DropDownList>
            <br />
            <br />
            <span class="edit-wand-lbl">Name:</span><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
            <span class="edit-wand-lbl">Wood Type:</span><asp:DropDownList ID="ddlWoodType" runat="server"></asp:DropDownList><br />
            <span class="edit-wand-lbl">Core Type:</span><asp:DropDownList ID="coreTypeDDL" runat="server"></asp:DropDownList><br />
            <span class="edit-wand-lbl">Flexibility Type:</span><asp:DropDownList ID="flexTypeDDL" runat="server"></asp:DropDownList><br />
            <span class="edit-wand-lbl">Country:</span><asp:DropDownList ID="countryDDL" runat="server"></asp:DropDownList><br />
            <span class="edit-wand-lbl">Length:</span><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><br />
            <span class="edit-wand-lbl">Weight:</span><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox><br />
            <span class="edit-wand-lbl">Price:</span><asp:TextBox ID="TextBox4" runat="server"></asp:TextBox><br />
            <span class="edit-wand-lbl">Description:</span><asp:TextBox ID="TextBox5" runat="server" TextMode="MultiLine"></asp:TextBox><br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="Save" OnClick="Button1_Click" />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label><br />
            <asp:Label ID="DBErrorLabel" runat="server" Text="DB Error Message Here"></asp:Label>

        </div>
    </form>
</asp:Content>
