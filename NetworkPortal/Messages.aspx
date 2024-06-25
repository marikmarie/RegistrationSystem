<%@ Page Title="Messages" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Messages.aspx.cs" Inherits="NetworkPortal.Messages" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   
    <h2>Messages</h2>
    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    <asp:DropDownList ID="ddlUsers" runat="server"></asp:DropDownList>
    <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
    <asp:Button ID="btnSendMessage" runat="server" Text="Send" OnClick="btnSendMessage_Click" />
    <asp:GridView ID="gvMessages" runat="server"></asp:GridView>
</asp:Content>
