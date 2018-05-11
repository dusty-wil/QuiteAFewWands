<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="QuiteAFewWands.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titlePlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContentPlaceholder" runat="server">

    <div class="non-semantic-protector">    
       <h1 class="ribbon">
           <span class="ribbon-content">Sign up for an account!</span>
       </h1>
   </div>
    <div class="body-content">
        
        <form id="RegistrationForm" runat="server">
            <div id="OkMsg" class="message message-ok" runat="server"></div>
            <div id="DBErrMsg" class="message message-error" runat="server"></div>
  
            <div class="registration-panel">
                <p>
                    Please complete all fields to create an account.
                </p>
                
                <asp:Label ID="LblF_Name" CssClass="registration-form-lbl" runat="server" Text="First Name:"></asp:Label>
                <asp:TextBox ID="Txt_FName" CssClass="formfield-text" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Txt_FName" ForeColor="Red">*</asp:RequiredFieldValidator>
                
                <br class="spacer" />
                
                <asp:Label ID="Lbl_L_Name" CssClass="registration-form-lbl" runat="server" Text="Last Name:"></asp:Label>
                <asp:TextBox ID="Txt_LName" CssClass="formfield-text" runat="server" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Txt_LName" ForeColor="Red">*</asp:RequiredFieldValidator>
                
                <br class="spacer"/>
                
                <asp:Label ID="Lbl_User_Name" CssClass="registration-form-lbl" runat="server" Text="Username:"></asp:Label>
                <asp:TextBox ID="Txt_Username" CssClass="formfield-text" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Txt_Username" ForeColor="Red">*</asp:RequiredFieldValidator>
                
                <br class="spacer"/>
                
                <asp:Label ID="Lbl_Psw" runat="server" CssClass="registration-form-lbl" Text="Password:"></asp:Label>
                <asp:TextBox ID="Txt_Psw" CssClass="formfield-text" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Txt_Psw" ForeColor="Red">*</asp:RequiredFieldValidator>
                
                <br class="spacer"/>
                
                <asp:Label ID="Lbl_Email" runat="server" CssClass="registration-form-lbl" Text="Email:" ></asp:Label>
                <asp:TextBox  ID="Txt_Email" CssClass="formfield-text" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Txt_Email" ForeColor="Red">*</asp:RequiredFieldValidator>
                
                <br class="spacer"/>
                
                <asp:Label ID="HouseName" runat="server" CssClass="registration-form-lbl" Text="House Name:"></asp:Label>
                <asp:DropDownList ID="HouseId" runat="server"></asp:DropDownList>
                
                <br class="spacer"/>
                
                <asp:Label ID="Lbl_AccId" runat="server" CssClass="registration-form-lbl" Text="Gringott's Account ID:" ></asp:Label>
                <asp:TextBox  ID="Txt_AccId" CssClass="formfield-text" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="Txt_AccId" ForeColor="Red">*</asp:RequiredFieldValidator>
                
                <br class="spacer"/>
                
                <asp:Button ID="Btn_Register" runat="server" OnClick="Btn_Register_Click" Text="Register" cssclass="cta-btn" />
            </div>

        </form>
    </div>

</asp:Content>
