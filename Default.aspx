<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AcademIQ_LMS.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="jumbotron">
            <h1 class="display-4">Welcome to AcademIQ LMS</h1>
            <p class="lead">A comprehensive Learning Management System for educational institutions.</p>
            <hr class="my-4">
            <p>Access your courses, content, and educational resources all in one place.</p>
            <asp:Button ID="btnGetStarted" runat="server" Text="Get Started" CssClass="btn btn-primary btn-lg" OnClick="btnGetStarted_Click" />
        </div>
        
        <div class="row">
            <div class="col-md-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">For Students</h5>
                        <p class="card-text">Access course materials, take quizzes, and track your progress.</p>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">For Teachers</h5>
                        <p class="card-text">Upload content, create quizzes, and manage your courses.</p>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">For Administrators</h5>
                        <p class="card-text">Manage users, courses, and system settings.</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
