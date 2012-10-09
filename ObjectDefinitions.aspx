<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ObjectDefinitions.aspx.cs" Inherits="DxHWebinar.ObjectDefinitions" %>
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
                        <th>Definition</th>
                        <th>Data</th>
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
                <td><a href='ObjectDef.aspx?id=<%# Eval("Id") %>'>View</a></td> 
                <td><a href='ObjectInstances.aspx?id=<%# Eval("Id") %>'>View</a></td> 
            </tr>
        </ItemTemplate>
    </asp:ListView>

</asp:Content>
