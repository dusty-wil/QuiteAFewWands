<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="QuiteAFewWands.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titlePlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContentPlaceholder" runat="server">
    <div class="non-semantic-protector">    

       <h1 class="ribbon">
           <span class="ribbon-content">Registration Form</span>
       </h1>

   </div>
    <form id="Registration" runat="server">
        <p style="margin-left: 40px">
        <p ></p>
        <div style="margin-left: 160px">
            <asp:Panel runat="server" BorderStyle="Solid" Height="398px" style="margin-top: 0px" Width="662px">
                <br />
                <asp:Label style="margin-left: 20px" ID="Label1" runat="server" Text="Please complete all fields to create an account." Font-Bold="True"></asp:Label>
                <br /><br />
                <asp:Label style="margin-left: 60px" ID="LblF_Name" runat="server" Text="First Name" Font-Size="Medium"></asp:Label>
                <asp:TextBox style="margin-left: 60px" ID="Txt_FName" runat="server" Width="229px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Txt_FName" ForeColor="Red">*</asp:RequiredFieldValidator>
                <br />
                <br />
                <asp:Label style="margin-left: 60px" ID="Lbl_L_Name" runat="server" Text="Last Name" Font-Size="Medium"></asp:Label>
                <asp:TextBox style="margin-left: 63px" ID="Txt_LName" runat="server" Width="226px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Txt_LName" ForeColor="Red">*</asp:RequiredFieldValidator>
                <br />
                <br />
                <asp:Label style="margin-left: 60px" ID="Lbl_User_Name" runat="server" Text="UserName" Font-Size="Medium"></asp:Label>
                <asp:TextBox  style="margin-left: 64px" ID="Txt_Username" runat="server" Width="227px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Txt_Username" ForeColor="Red">*</asp:RequiredFieldValidator>
                <br />
                <br />
                <asp:Label style="margin-left: 60px" ID="Lbl_Psw" runat="server" Text="Password" Font-Size="Medium"></asp:Label>
                <asp:TextBox style="margin-left: 72px" ID="Txt_Psw" runat="server" Width="227px" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Txt_Psw" ForeColor="Red">*</asp:RequiredFieldValidator>
                <br />
                <br />
                <asp:Label style="margin-left: 60px" ID="Lbl_Email" runat="server" Text="Email" Font-Size="Medium"></asp:Label>
               
                <asp:TextBox  style="margin-left: 95px" ID="Txt_Email" runat="server" Width="227px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Txt_Email" ForeColor="Red">*</asp:RequiredFieldValidator>
                <br />
                <br />
                <asp:Label style="margin-left: 60px" ID="HouseName" runat="server" Text="House Name" Font-Size="Medium"></asp:Label>
               
                <asp:DropDownList style="margin-left: 50px" ID="HouseId" runat="server" DataSourceID="SqlDataSource1" DataTextField="HouseName" DataValueField="HouseName">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:QuiteAFewWandsConnectionString %>" SelectCommand="SELECT [HouseName] FROM [House]"></asp:SqlDataSource>
                <br />
               
                <br />
                <asp:Label style="margin-left: 10px" ID="Label7" runat="server" ForeColor="Red"></asp:Label>
                <asp:Button style="margin-left: 460px" ID="Btn_Register" runat="server" OnClick="Button1_Click" Text="Register" Font-Bold="True" />
                <br />
                <br />
              
            </asp:Panel>
        </div>
    </form>
</asp:Content>
