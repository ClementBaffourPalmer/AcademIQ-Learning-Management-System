<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AcademIQ_LMS.Admin.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h1>Admin Dashboard</h1>
        <p class="lead">Welcome, <asp:Literal ID="litAdminName" runat="server" />!</p>
        
        <div class="row">
            <div class="col-md-3">
                <div class="card text-white bg-primary">
                    <div class="card-body">
                        <h5 class="card-title">Total Users</h5>
                        <h2 class="card-text"><asp:Literal ID="litTotalUsers" runat="server" /></h2>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card text-white bg-success">
                    <div class="card-body">
                        <h5 class="card-title">Total Content</h5>
                        <h2 class="card-text"><asp:Literal ID="litTotalContent" runat="server" /></h2>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card text-white bg-info">
                    <div class="card-body">
                        <h5 class="card-title">Total Quizzes</h5>
                        <h2 class="card-text"><asp:Literal ID="litTotalQuizzes" runat="server" /></h2>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card text-white bg-warning">
                    <div class="card-body">
                        <h5 class="card-title">Active Sessions</h5>
                        <h2 class="card-text"><asp:Literal ID="litActiveSessions" runat="server" /></h2>
                    </div>
                </div>
            </div>
        </div>
        
        <div class="row mt-4">
            <div class="col-md-6">
                <div class="card">
                    <div class="card-header">
                        <h5>Recent Users</h5>
                    </div>
                    <div class="card-body">
                        <asp:GridView ID="gvRecentUsers" runat="server" CssClass="table table-striped" 
                            AutoGenerateColumns="false" EmptyDataText="No users found.">
                            <Columns>
                                <asp:BoundField DataField="Username" HeaderText="Username" />
                                <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                                <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                                <asp:BoundField DataField="Role" HeaderText="Role" />
                                <asp:BoundField DataField="CreatedAt" HeaderText="Created Date" DataFormatString="{0:MM/dd/yyyy}" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="card">
                    <div class="card-header">
                        <h5>System Statistics</h5>
                    </div>
                    <div class="card-body">
                        <ul class="list-group list-group-flush">
                            <li class="list-group-item d-flex justify-content-between align-items-center">
                                Teachers
                                <span class="badge badge-primary badge-pill"><asp:Literal ID="litTeacherCount" runat="server" /></span>
                            </li>
                            <li class="list-group-item d-flex justify-content-between align-items-center">
                                Students
                                <span class="badge badge-success badge-pill"><asp:Literal ID="litStudentCount" runat="server" /></span>
                            </li>
                            <li class="list-group-item d-flex justify-content-between align-items-center">
                                PDF Files
                                <span class="badge badge-info badge-pill"><asp:Literal ID="litPdfCount" runat="server" /></span>
                            </li>
                            <li class="list-group-item d-flex justify-content-between align-items-center">
                                Video Files
                                <span class="badge badge-warning badge-pill"><asp:Literal ID="litVideoCount" runat="server" /></span>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        
        <div class="row mt-4">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <h5>Quick Actions</h5>
                    </div>
                    <div class="card-body">
                        <a href="ManageUsers.aspx" class="btn btn-primary mr-2">Manage Users</a>
                        <a href="ManageContent.aspx" class="btn btn-success mr-2">Manage Content</a>
                        <a href="ManageQuizzes.aspx" class="btn btn-info mr-2">Manage Quizzes</a>
                        <a href="SystemSettings.aspx" class="btn btn-secondary">System Settings</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

