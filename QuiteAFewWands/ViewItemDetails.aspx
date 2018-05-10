<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ViewItemDetails.aspx.cs" Inherits="QuiteAFewWands.ViewItemDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titlePlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContentPlaceholder" runat="server">
    <form id="ItemDetailForm" runat="server">
        <div id="OkMsg" class="message message-ok" runat="server"></div>
        <div id="DBErrMsg" class="message message-error" runat="server"></div> 

        <ul class="item-detail-list">
            <asp:Repeater runat="server" ID="ItemDetailListRepeater">
                <ItemTemplate> 
                    <li>
                        <div class="non-semantic-protector">
                            <h1 class="ribbon">
                                <span class="ribbon-content"><%# Eval("Name") %></span>
                            </h1>
                        </div>
                        <span class="item-detail-label">Wood Type:</span>
                        <span class="item-detail-value"><%# Eval("WoodTypeName") %></span> <br />
                        
                        <span class="item-detail-label">Core Type:</span>
                        <span class="item-detail-value"><%# Eval("CoreTypeName") %></span><br />
                        
                        <span class="item-detail-label">Flexibility:</span>
                        <span class="item-detail-value"><%# Eval("FlexibilityValue") %></span><br />
                        
                        <span class="item-detail-label">Country of Origin:</span>
                        <span class="item-detail-value"><%# Eval("CountryName") %></span><br />
                        
                        <span class="item-detail-label">Length:</span>
                        <span class="item-detail-value"><%# Eval("Length") %> in.</span><br />
                        
                        <span class="item-detail-label">Weight:</span>
                        <span class="item-detail-value"><%# Eval("Weight") %> lbs.</span><br />
                        
                        <span class="item-detail-label">Date Created:</span>
                        <span class="item-detail-value"><%# Eval("DateCreated") %></span><br />
                        
                        <span class="item-detail-label">Price:</span>
                        <span class="item-detail-value">$<%# Eval("Price") %></span><br />
                        
                        <span class="item-detail-btn">
                            <a href="ViewItemDetails.aspx?WandId=<%# Eval("WandId") %>&AddWandToCart=<%# Eval("WandId") %>" class="cta-btn">Add to Cart</a>
                        </span>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </form>
</asp:Content>
