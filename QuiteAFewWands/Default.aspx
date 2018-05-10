<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="QuiteAFewWands.Default" %>
<asp:Content ID="title" ContentPlaceHolderID="titlePlaceholder" runat="server">Quite a few Wands!</asp:Content>
<asp:Content ID="head" ContentPlaceHolderID="headPlaceholder" runat="server"></asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="bodyContentPlaceholder" runat="server">
    <div class="non-semantic-protector">
        <h1 class="ribbon">
            <span class="ribbon-content">Look through our selection!</span>
        </h1>
    </div>
    <div class="body-content">
        <form id="ProductGridForm" runat="server">
            <div id="OkMsg" class="message message-ok" runat="server"></div>
            <div id="DBErrMsg" class="message message-error" runat="server"></div>
        
            <asp:Label 
                ID="WoodTypeFilterLbl" 
                runat="server" 
                Text="Filter By Wood Type:" 
                CssClass="filter-lbl"></asp:Label>
            <asp:DropDownList 
                ID="WoodTypeList" 
                runat="server"
                AutoPostBack="true"
                OnSelectedIndexChanged="WoodType_OnSelectedIndexChanged"
                CssClass="form-dropdown filter-list"></asp:DropDownList>
        
            <asp:Label 
                ID="CoreTypeFilterLbl" 
                runat="server" 
                Text="Filter By Core Type:" 
                CssClass="filter-lbl"></asp:Label>
            <asp:DropDownList 
                ID="CoreTypeList" 
                runat="server"
                AutoPostBack="true"
                OnSelectedIndexChanged="CoreType_OnSelectedIndexChanged"
                CssClass="form-dropdown filter-list"></asp:DropDownList>

            <asp:Label 
                ID="FlexibilityFilterLbl" 
                runat="server" 
                Text="Filter By Flexibility:"
            
                CssClass="filter-lbl"></asp:Label>
            <asp:DropDownList 
                ID="FlexibilityList" 
                runat="server"
                AutoPostBack="true"
                OnSelectedIndexChanged="Flexibility_OnSelectedIndexChanged"
                CssClass="form-dropdown filter-list"></asp:DropDownList>

            <asp:Label 
                ID="CountryFilterLbl" 
                runat="server" 
                Text="Filter By Country:" 
                CssClass="filter-lbl"></asp:Label>
            <asp:DropDownList 
                ID="CountryList" 
                runat="server"
                AutoPostBack="true"
                OnSelectedIndexChanged="Country_OnSelectedIndexChanged"
                CssClass="form-dropdown filter-list"></asp:DropDownList>
                
            <asp:GridView 
                ID="ProductGrid" 
                runat="server" 
                AutoGenerateColumns="False" 
                CssClass="product-grid"
                AllowSorting="true"
                AllowPaging="true"
                PageSize="5"
                OnPageIndexChanging="ProductGV_PageIndexChanging"
                OnSorting="ProductGV_Sorting"
                            OnRowDataBound="ProductGV_RowDataBound"
                >
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name"/>
                    <asp:BoundField DataField="WoodTypeName" HeaderText="Wood Type" SortExpression="WoodTypeName"/>
                    <asp:BoundField DataField="CoreTypeName" HeaderText="Core Type" SortExpression="CoreTypeName"/>
                    <asp:BoundField DataField="FlexibilityValue" HeaderText="Flexibility" SortExpression="FlexibilityValue"/>
                    <asp:BoundField DataField="CountryName" HeaderText="Crafted In" SortExpression="CountryName"/>
                    <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" DataFormatString="{0:C}" />
                    <asp:HyperLinkField 
                        HeaderText="Comments"
                        DataTextField="CommentCount"
                        DataTextFormatString="{0}"
                        SortExpression="AvgRating"
                        DataNavigateUrlFormatString="ViewComments.aspx?WandId={0}"
                        DataNavigateUrlFields="WandId"
                        />
                    <asp:HyperLinkField 
                        HeaderText="Avg. Rating" 
                        DataTextField="AvgRating"
                        SortExpression="CommentCount"
                        DataTextFormatString="{0}"
                        DataNavigateUrlFormatString="ViewRatings.aspx?WandId={0}"
                        DataNavigateUrlFields="WandId"
                        />
                    <asp:HyperLinkField 
                        Text="View Item" 
                        HeaderText="Item Details" 
                        DataNavigateUrlFormatString="ViewItemDetails.aspx?WandId={0}"
                        DataNavigateUrlFields="WandId"
                        />
                    <asp:HyperLinkField 
                        Text="Add" 
                        HeaderText="Add To Cart" 
                        DataNavigateUrlFormatString="Default.aspx?AddWandToCart={0}"
                        DataNavigateUrlFields="WandId"
                        />
                </Columns>
                <PagerStyle CssClass="product-grid-pager" />
                <HeaderStyle CssClass="product-grid-header" />
            </asp:GridView>
        </form>
    </div>
</asp:Content>
