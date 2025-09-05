<%@ Page Title="Teacher Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AcademIQ_LMS.Teacher.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h1>Teacher Dashboard</h1>
        <p class="lead">Welcome, <asp:Literal ID="litTeacherName" runat="server" />!</p>
        
        <div class="row">
            <div class="col-md-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Upload Content</h5>
                        <p class="card-text">Upload learning materials for your students.</p>
                        <a href="UploadContent.aspx" class="btn btn-primary">Upload Content</a>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Create Quiz</h5>
                        <p class="card-text">Create quizzes to test your students' knowledge.</p>
                        <a href="CreateQuiz.aspx" class="btn btn-primary">Create Quiz</a>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">View Results</h5>
                        <p class="card-text">View quiz results and student performance.</p>
                        <a href="ViewResults.aspx" class="btn btn-primary">View Results</a>
                    </div>
                </div>
            </div>
        </div>
        
        <div class="row mt-4">
            <div class="col-md-6">
                <div class="card">
                    <div class="card-header">
                        <h5>Recent Content</h5>
                    </div>
                    <div class="card-body">
                        <asp:GridView ID="gvRecentContent" runat="server" CssClass="table table-striped" 
                            AutoGenerateColumns="false" EmptyDataText="No content uploaded yet.">
                            <Columns>
                                <asp:BoundField DataField="Title" HeaderText="Title" />
                                <asp:BoundField DataField="UploadedAt" HeaderText="Upload Date" DataFormatString="{0:MM/dd/yyyy}" />
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <a href='<%# Eval("FilePath") %>' class="btn btn-sm btn-outline-primary">Download</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="card">
                    <div class="card-header">
                        <h5>Recent Quizzes</h5>
                    </div>
                    <div class="card-body">
                        <asp:GridView ID="gvRecentQuizzes" runat="server" CssClass="table table-striped" 
                            AutoGenerateColumns="false" EmptyDataText="No quizzes created yet.">
                            <Columns>
                                <asp:BoundField DataField="Title" HeaderText="Title" />
                                <asp:BoundField DataField="CreatedAt" HeaderText="Created Date" DataFormatString="{0:MM/dd/yyyy}" />
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <a href='ViewResults.aspx?quizId=<%# Eval("Id") %>' class="btn btn-sm btn-outline-primary">View Results</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

