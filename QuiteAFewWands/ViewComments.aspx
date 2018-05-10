<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ViewComments.aspx.cs" Inherits="QuiteAFewWands.ViewComments" %>
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
    
    <form id="CommentListForm" runat="server">
        <div id="OkMsg" class="message message-ok" runat="server"></div>
        <div id="DBErrMsg" class="message message-error" runat="server"></div> 

        <ul class="comment-list">
            <asp:Repeater runat="server" ID="CommentListRepeater">
                <ItemTemplate> 
                    <li>
                        <span class="comment-title"><%# Eval("Title") %></span> 
                        <span class="comment-author"><%# Eval("Name") %></span>
                        <span class="comment-date"><%# Eval("Date") %></span>
                        <span class="comment-text"><%# Eval("Comment") %></span>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </form>
</asp:Content>
