<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="QuiteAFewWands.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titlePlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContentPlaceholder" runat="server">

    <div class="non-semantic-protector">    
       <h1 class="ribbon">
           <span class="ribbon-content">Log in!</span>
       </h1>
   </div>
    <div class="body-content">
        
        <form id="LoginForm" runat="server">
            <div id="OkMsg" class="message message-ok" runat="server"></div>
            <div id="DBErrMsg" class="message message-error" runat="server"></div>

            <div class="login-panel">
                <asp:Label ID="Lbl_Username" CssClass="login-form-lbl" runat="server" Text="Username: "></asp:Label>
                <asp:TextBox ID="Txt_Username" CssClass="formfield-text" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Txt_Username" ForeColor="Red">*</asp:RequiredFieldValidator>
                <br />
                <asp:Label ID="Lbl_Password" CssClass="login-form-lbl" runat="server" Text="Password: "></asp:Label>
                <asp:TextBox ID="Txt_Psw" runat="server" CssClass="formfield-text" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2"  runat="server" ControlToValidate="Txt_Psw" ForeColor="Red">*</asp:RequiredFieldValidator>
                
                <br class="spacer"/>
                <br class="spacer"/>  

                <asp:Button ID="Btn_Enter" runat="server" Text="Enter" cssclass="cta-btn" OnClick="Btn_Enter_Click" /> 
                
                <p>Do not have an account? <a href="Register.aspx">Click here to register</a></p>
            </div>
        </form>
    </div>
</asp:Content>