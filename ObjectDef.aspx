<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ObjectDef.aspx.cs" Inherits="DxHWebinar.ObjectDef" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ListView ID="lvFields" runat="server">
        <LayoutTemplate>
            <table>
                <thead>
                    <tr>
                        <th>DisplayName</th>
                        <th>Id</th>
                        <th>Type</th>
                        <th>IsRequired</th>
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
                <td><%# ((DxHWebinar.ContextBusService.FieldType)Eval("DataType")).Type%></td> 
                <td><%# Eval("IsRequired") %></td> 
            </tr>
        </ItemTemplate>
    </asp:ListView>

</asp:Content>
