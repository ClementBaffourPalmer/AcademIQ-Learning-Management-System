<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AcademIQ_LMS.Account.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center mt-5">
        <div class="col-md-6">
            <div class="card">
                <div class="card-header">
                    <h2 class="text-center">Login</h2>
                </div>
                <div class="card-body">
                    <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-danger">
                        <asp:Literal ID="litError" runat="server" />
                    </asp:Panel>
                    
                    <div class="form-group">
                        <label for="txtUsername">Username</label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvUsername" runat="server" 
                            ControlToValidate="txtUsername" 
                            ErrorMessage="Username is required." 
                            CssClass="text-danger" 
                            Display="Dynamic" />
                    </div>
                    
                    <div class="form-group">
                        <label for="txtPassword">Password</label>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" 
                            ControlToValidate="txtPassword" 
                            ErrorMessage="Password is required." 
                            CssClass="text-danger" 
                            Display="Dynamic" />
                    </div>
                    
                    <div class="form-group">
                        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary btn-block" OnClick="btnLogin_Click" />
                    </div>
                    
                    <div class="text-center">
                        <p>Don't have an account? <a href="Register.aspx">Register here</a></p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

