<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConnectionInfo.aspx.cs" Inherits="DxHWebinar.ConnectionInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ListView ID="lvParams" runat="server">
        <LayoutTemplate>
            <ul>
                <asp:PlaceHolder ID="ItemPlaceholder" runat="server"></asp:PlaceHolder>
            </ul>
        </LayoutTemplate>
        <ItemTemplate>
            <li><%# Eval("DisplayName") %>: <%# Eval("Id") %></li>
        </ItemTemplate>
    </asp:ListView>

</asp:Content>
