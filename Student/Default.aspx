<%@ Page Title="Student Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AcademIQ_LMS.Student.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h1>Student Dashboard</h1>
        <p class="lead">Welcome, <asp:Literal ID="litStudentName" runat="server" />!</p>
        
        <div class="row">
            <div class="col-md-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">View Content</h5>
                        <p class="card-text">Access learning materials uploaded by your teachers.</p>
                        <a href="ViewContent.aspx" class="btn btn-primary">View Content</a>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Take Quizzes</h5>
                        <p class="card-text">Take quizzes to test your knowledge.</p>
                        <a href="TakeQuiz.aspx" class="btn btn-primary">Take Quiz</a>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">My Results</h5>
                        <p class="card-text">View your quiz results and grades.</p>
                        <a href="MyResults.aspx" class="btn btn-primary">View Results</a>
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
                            AutoGenerateColumns="false" EmptyDataText="No content available yet.">
                            <Columns>
                                <asp:BoundField DataField="Title" HeaderText="Title" />
                                <asp:BoundField DataField="Type" HeaderText="Type" />
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
                        <h5>Available Quizzes</h5>
                    </div>
                    <div class="card-body">
                        <asp:GridView ID="gvAvailableQuizzes" runat="server" CssClass="table table-striped" 
                            AutoGenerateColumns="false" EmptyDataText="No quizzes available yet.">
                            <Columns>
                                <asp:BoundField DataField="Title" HeaderText="Title" />
                                <asp:BoundField DataField="Description" HeaderText="Description" />
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <a href='TakeQuiz.aspx?quizId=<%# Eval("Id") %>' class="btn btn-sm btn-outline-primary">Take Quiz</a>
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

