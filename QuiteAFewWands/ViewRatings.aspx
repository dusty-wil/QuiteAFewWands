<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ViewRatings.aspx.cs" Inherits="QuiteAFewWands.ViewRatings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titlePlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContentPlaceholder" runat="server">
    <div class="non-semantic-protector">
        <h1 class="ribbon">
            <span class="ribbon-content" id="WandTitle" runat="server"></span>
        </h1>
    </div>
    <div class="body-content">
        <form id="RatingGridForm" runat="server">
            <div id="OkMsg" class="message message-ok" runat="server"></div>
            <div id="DBErrMsg" class="message message-error" runat="server"></div>
            <div class="rating-spacer">
                <asp:GridView 
                    ID="RatingGrid" 
                    runat="server"  
                    CssClass="rating-grid" 
                    ShowFooter="true"
                    OnRowDataBound="RatingGV_RowDataBound">
                    <FooterStyle CssClass="rating-grid-footer" />
                    <HeaderStyle CssClass="rating-grid-header" />
                </asp:GridView>
                <br />
                <div class="add-rating-container">
                    <asp:Label ID="RatingLbl" CssClass="rating-label" runat="server" Text="How would you rate this wand?"></asp:Label>
                    <asp:DropDownList ID="RatingDDL" runat="server" CssClass="rating-ddl">
                        <asp:ListItem Text="1 Star" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2 Stars" Value="2"></asp:ListItem>
                        <asp:ListItem Text="3 Stars" Value="3"></asp:ListItem>
                        <asp:ListItem Text="4 Stars" Value="4"></asp:ListItem>
                        <asp:ListItem Text="5 Stars" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                    <br class="spacer" />
                    <asp:Button ID="Btn_Submit" cssclass="cta-btn" runat="server" Text="Submit" OnClick="Btn_Submit_Click" />
                </div>
            </div>
        </form>
    </div>
</asp:Content>
