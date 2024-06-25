<%@ Page Title="Networks" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Networks.aspx.cs" Inherits="NetworkPortal.Networks" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Networks</h2>
    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    <asp:DropDownList ID="ddlNetworks" runat="server"></asp:DropDownList>
    <asp:Button ID="btnJoinNetwork" runat="server" Text="Join Network" OnClick="btnJoinNetwork_Click" />
    <asp:GridView ID="gvNetworks" runat="server"></asp:GridView>
</asp:Content>
