<%@ Page Title="Interests" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Interests.aspx.cs" Inherits="NetworkPortal.Interests" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <section class="row">
            <h1 id="aspnetTitle">Select Your Interests</h1>
        </section>
        <div class="row">
            <section class="col-md-6 offset-md-3">
                <asp:CheckBoxList ID="chkInterests" runat="server" CssClass="form-control">
                </asp:CheckBoxList>
                <div class="form-group">
                    <asp:Button ID="btnSaveInterests" runat="server" Text="Save Interests" CssClass="btn btn-primary" OnClick="btnSaveInterests_Click" />
                </div>
                <div class="form-group">
                    <asp:Label ID="lblMessage" runat="server" Text="" CssClass="text-success"></asp:Label>
                </div>
            </section>
        </div>
    </main>
</asp:Content>

