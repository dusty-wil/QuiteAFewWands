<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="QuiteAFewWands.Default" %>
<asp:Content ID="title" ContentPlaceHolderID="titlePlaceholder" runat="server">Quite a few Wands!</asp:Content>
<asp:Content ID="head" ContentPlaceHolderID="headPlaceholder" runat="server"></asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="bodyContentPlaceholder" runat="server">
    <div class="non-semantic-protector">
        <h1 class="ribbon">
            <span class="ribbon-content">heading 2</span>
        </h1>
    </div>
    <div class="body-content">
    alskdasdjslkdjsdflk
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
            OnSelectedIndexChanged="WoodType_OnSelectedIndexChanged" 
            runat="server"
            AutoPostBack="true"
            CssClass="form-dropdown filter-list"></asp:DropDownList>
        
        <asp:Label 
            ID="CoreTypeFilterLbl" 
            runat="server" 
            Text="Filter By Core Type:" 
            CssClass="filter-lbl"></asp:Label>
        <asp:DropDownList 
            ID="CoreTypeList" 
            OnSelectedIndexChanged="CoreType_OnSelectedIndexChanged" 
            runat="server"
            AutoPostBack="true"
            CssClass="form-dropdown filter-list"></asp:DropDownList>

        <asp:Label 
            ID="Label2" 
            runat="server" 
            Text="Filter By Wood Type:" 
            CssClass="filter-lbl"></asp:Label>
        <asp:DropDownList 
            ID="DropDownList2" 
            OnSelectedIndexChanged="WoodType_OnSelectedIndexChanged" 
            runat="server"
            AutoPostBack="true"
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
            >
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name"/>
                <asp:BoundField DataField="Height" HeaderText="Height" SortExpression="Height"/>
                <asp:BoundField DataField="Width" HeaderText="Width" SortExpression="Width"/>
                <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" SortExpression="Price"/>
                <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year"/>
                <asp:BoundField DataField="Brand" HeaderText="Brand" SortExpression="Brand"/>
                <asp:BoundField DataField="Category" HeaderText="Category" SortExpression="Category"/>
                <asp:BoundField DataField="Description" HeaderText="Description" />
                <asp:HyperLinkField 
                    Text="View Comments" 
                    HeaderText="Comments" 
                    DataNavigateUrlFormatString="ViewComments.aspx?ItemId={0}"
                    DataNavigateUrlFields="ItemId"
                    />
                <asp:HyperLinkField 
                    Text="View Ratings" 
                    HeaderText="Ratings" 
                    DataNavigateUrlFormatString="ViewRatings.aspx?ItemId={0}"
                    DataNavigateUrlFields="ItemId"
                    />
            </Columns>
            <PagerStyle CssClass="product-grid-pager" />
        </asp:GridView>
    </form>
    </div>
</asp:Content>
