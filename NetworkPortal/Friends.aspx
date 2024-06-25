<%@ Page Title="Friends" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Friends.aspx.cs" Inherits="NetworkPortal.Friends" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Friends</h2>
    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    <asp:DropDownList ID="ddlUsers" runat="server"></asp:DropDownList>
    <asp:Button ID="btnSendRequest" runat="server" Text="Send Friend Request" OnClick="btnSendRequest_Click" />
    <asp:GridView ID="gvFriends" runat="server"></asp:GridView>
</asp:Content>
