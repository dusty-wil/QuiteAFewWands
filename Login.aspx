<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="QuiteAFewWands.Login1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titlePlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContentPlaceholder" runat="server">
    <div class="non-semantic-protector">    

       <h1 class="ribbon">

           <span class="ribbon-content">Log In Form</span>

       </h1>

   </div>
    <form id="form1" runat="server">

       
        <asp:Panel ID="Panel1" runat="server"></asp:Panel>

        <asp:Panel ID="Panel2" runat="server" BorderStyle="Solid" style="margin-left: 120px" Width="889px" Font-Size="Large">
               <br /><br />
                <asp:Label style="margin-left: 70px" ID="Lbl_Username" runat="server" Text="UserName" Font-Size="Medium"></asp:Label>
                <asp:TextBox style="margin-left: 40px" ID="Txt_Username" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Txt_Username" ForeColor="Red">*</asp:RequiredFieldValidator>
                <asp:Label style="margin-left: 40px" ID="Lbl_Password" runat="server" Text="Password" Font-Size="Medium"></asp:Label>
                <asp:TextBox style="margin-left: 40px" ID="Txt_Psw" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Txt_Psw" ForeColor="Red">*</asp:RequiredFieldValidator>
                <br />
                <br />
                <asp:Button style="margin-left: 600px" ID="Btn_Enter" runat="server" Text="Enter" OnClick="Btn_Enter_Click" />
            <br />
                <br />    
            <asp:Label style="margin-left: 140px" ID="Label1" runat="server" Text="Do not have an account ? " Font-Size="Medium"></asp:Label> 
                <a href="Register.aspx">Click here to register</a>
                <br /> 
                <br /> 
            </asp:Panel>
    </form>
</asp:Content>
