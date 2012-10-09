﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdventureWorks.aspx.cs" Inherits="DxHWebinar.AdventureWorks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

  <asp:ListView ID="lvObjects" runat="server">
        <LayoutTemplate>
            <table>
                <thead>
                    <tr>
                        <th>Object DisplayName</th>
                        <th>Object Id</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:PlaceHolder ID="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                </tbody>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("DisplayName") %></td>
                <td><%# Eval("Id") %></td> 
            </tr>
        </ItemTemplate>
    </asp:ListView>

    
    <asp:ListView ID="lvObjectInstances" runat="server">
        <LayoutTemplate>
            <ul>
                <asp:PlaceHolder ID="ItemPlaceholder" runat="server"></asp:PlaceHolder>
            </ul> 
        </LayoutTemplate>
        <ItemTemplate>
            <li>
                <%# Eval("Id") %> : <%# Eval("DisplayName") %>
                <asp:ListView ID="lvFields" runat="server" DataSource='<%# Eval("Fields") %>'>
                    <LayoutTemplate>
                        <h3>Fields</h3>
                        <ul>
                            <asp:PlaceHolder ID="ItemPlaceholder" runat="server"></asp:PlaceHolder>
                        </ul> 
                    </LayoutTemplate>
                    <ItemTemplate>
                        <li> <%# Eval("DisplayName") %> : <%# Eval("Value") %> </li>
                    </ItemTemplate>
                </asp:ListView>
            </li>
        </ItemTemplate>
    </asp:ListView>

</asp:Content>
