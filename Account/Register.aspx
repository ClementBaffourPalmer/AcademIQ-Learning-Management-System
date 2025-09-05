<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="AcademIQ_LMS.Account.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center mt-5">
        <div class="col-md-8">
            <div class="card">
                <div class="card-header">
                    <h2 class="text-center">Register</h2>
                </div>
                <div class="card-body">
                    <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-danger">
                        <asp:Literal ID="litError" runat="server" />
                    </asp:Panel>
                    
                    <asp:Panel ID="pnlSuccess" runat="server" Visible="false" CssClass="alert alert-success">
                        <asp:Literal ID="litSuccess" runat="server" />
                    </asp:Panel>
                    
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtFirstName">First Name</label>
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" 
                                    ControlToValidate="txtFirstName" 
                                    ErrorMessage="First name is required." 
                                    CssClass="text-danger" 
                                    Display="Dynamic" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtLastName">Last Name</label>
                                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" 
                                    ControlToValidate="txtLastName" 
                                    ErrorMessage="Last name is required." 
                                    CssClass="text-danger" 
                                    Display="Dynamic" />
                            </div>
                        </div>
                    </div>
                    
                    <div class="form-group">
                        <label for="txtEmail">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" 
                            ControlToValidate="txtEmail" 
                            ErrorMessage="Email is required." 
                            CssClass="text-danger" 
                            Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" 
                            ControlToValidate="txtEmail" 
                            ErrorMessage="Please enter a valid email address." 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                            CssClass="text-danger" 
                            Display="Dynamic" />
                    </div>
                    
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
                        <label for="txtConfirmPassword">Confirm Password</label>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" 
                            ControlToValidate="txtConfirmPassword" 
                            ErrorMessage="Confirm password is required." 
                            CssClass="text-danger" 
                            Display="Dynamic" />
                        <asp:CompareValidator ID="cvPassword" runat="server" 
                            ControlToValidate="txtConfirmPassword" 
                            ControlToCompare="txtPassword" 
                            ErrorMessage="Passwords do not match." 
                            CssClass="text-danger" 
                            Display="Dynamic" />
                    </div>
                    
                    <div class="form-group">
                        <label for="ddlRole">Role</label>
                        <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control">
                            <asp:ListItem Text="-- Select Role --" Value="" />
                            <asp:ListItem Text="Student" Value="Student" />
                            <asp:ListItem Text="Teacher" Value="Teacher" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvRole" runat="server" 
                            ControlToValidate="ddlRole" 
                            ErrorMessage="Role is required." 
                            CssClass="text-danger" 
                            Display="Dynamic" />
                    </div>
                    
                    <div class="form-group">
                        <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-primary btn-block" OnClick="btnRegister_Click" />
                    </div>
                    
                    <div class="text-center">
                        <p>Already have an account? <a href="Login.aspx">Login here</a></p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
