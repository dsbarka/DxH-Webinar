<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="DxHWebinar._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
  <ul>
    <li><a href="ConnectionInfo.aspx">Connection Info</a></li>
    <li><a href="ObjectDefinitions.aspx">Object Definitions</a></li>
  </ul>
</asp:Content>
