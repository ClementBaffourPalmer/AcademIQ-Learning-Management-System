<%@ Page Title="View Content" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewContent.aspx.cs" Inherits="AcademIQ_LMS.Student.ViewContent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h1>Learning Materials</h1>
        
        <div class="row mb-3">
            <div class="col-md-6">
                <div class="input-group">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search content..." />
                    <div class="input-group-append">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-outline-secondary" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <asp:DropDownList ID="ddlFilter" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged">
                    <asp:ListItem Text="All Types" Value="" />
                    <asp:ListItem Text="PDF" Value="PDF" />
                    <asp:ListItem Text="Document" Value="Document" />
                    <asp:ListItem Text="Video" Value="Video" />
                    <asp:ListItem Text="Image" Value="Image" />
                    <asp:ListItem Text="Other" Value="Other" />
                </asp:DropDownList>
            </div>
        </div>
        
        <asp:GridView ID="gvContent" runat="server" CssClass="table table-striped table-hover" 
            AutoGenerateColumns="false" EmptyDataText="No content available."
            OnRowCommand="gvContent_RowCommand" AllowPaging="true" PageSize="10"
            OnPageIndexChanging="gvContent_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:BoundField DataField="Description" HeaderText="Description" />
                <asp:BoundField DataField="Type" HeaderText="Type" />
                <asp:BoundField DataField="UploadedBy.FirstName" HeaderText="Uploaded By" />
                <asp:BoundField DataField="UploadedAt" HeaderText="Upload Date" DataFormatString="{0:MM/dd/yyyy HH:mm}" />
                <asp:BoundField DataField="FileSize" HeaderText="Size" DataFormatString="{0:N0} bytes" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDownload" runat="server" CommandName="Download" 
                            CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-outline-primary">
                            <i class="fas fa-download"></i> Download
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pagination" />
        </asp:GridView>
        
        <div class="mt-3">
            <a href="Default.aspx" class="btn btn-secondary">Back to Dashboard</a>
        </div>
    </div>
</asp:Content>

