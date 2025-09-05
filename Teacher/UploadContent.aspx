<%@ Page Title="Upload Content" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UploadContent.aspx.cs" Inherits="AcademIQ_LMS.Teacher.UploadContent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h1>Upload Content</h1>
        
        <asp:Panel ID="pnlSuccess" runat="server" Visible="false" CssClass="alert alert-success">
            <asp:Literal ID="litSuccess" runat="server" />
        </asp:Panel>
        
        <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-danger">
            <asp:Literal ID="litError" runat="server" />
        </asp:Panel>
        
        <div class="card">
            <div class="card-body">
                <div class="form-group">
                    <label for="txtTitle">Title</label>
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator ID="rfvTitle" runat="server" 
                        ControlToValidate="txtTitle" 
                        ErrorMessage="Title is required." 
                        CssClass="text-danger" 
                        Display="Dynamic" />
                </div>
                
                <div class="form-group">
                    <label for="txtDescription">Description</label>
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                    <asp:RequiredFieldValidator ID="rfvDescription" runat="server" 
                        ControlToValidate="txtDescription" 
                        ErrorMessage="Description is required." 
                        CssClass="text-danger" 
                        Display="Dynamic" />
                </div>
                
                <div class="form-group">
                    <label for="ddlContentType">Content Type</label>
                    <asp:DropDownList ID="ddlContentType" runat="server" CssClass="form-control">
                        <asp:ListItem Text="-- Select Type --" Value="" />
                        <asp:ListItem Text="PDF" Value="PDF" />
                        <asp:ListItem Text="Document" Value="Document" />
                        <asp:ListItem Text="Video" Value="Video" />
                        <asp:ListItem Text="Image" Value="Image" />
                        <asp:ListItem Text="Other" Value="Other" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvContentType" runat="server" 
                        ControlToValidate="ddlContentType" 
                        ErrorMessage="Content type is required." 
                        CssClass="text-danger" 
                        Display="Dynamic" />
                </div>
                
                <div class="form-group">
                    <label for="fileUpload">File</label>
                    <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control-file" />
                    <asp:RequiredFieldValidator ID="rfvFile" runat="server" 
                        ControlToValidate="fileUpload" 
                        ErrorMessage="Please select a file to upload." 
                        CssClass="text-danger" 
                        Display="Dynamic" />
                    <small class="form-text text-muted">Supported formats: PDF, DOC, DOCX, PPT, PPTX, ZIP, JPG, PNG, MP4</small>
                </div>
                
                <div class="form-group">
                    <asp:Button ID="btnUpload" runat="server" Text="Upload Content" CssClass="btn btn-primary" OnClick="btnUpload_Click" />
                    <a href="Default.aspx" class="btn btn-secondary">Back to Dashboard</a>
                </div>
            </div>
        </div>
        
        <div class="mt-4">
            <h3>Your Uploaded Content</h3>
            <asp:GridView ID="gvContent" runat="server" CssClass="table table-striped" 
                AutoGenerateColumns="false" EmptyDataText="No content uploaded yet."
                OnRowCommand="gvContent_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Title" HeaderText="Title" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="Type" HeaderText="Type" />
                    <asp:BoundField DataField="UploadedAt" HeaderText="Upload Date" DataFormatString="{0:MM/dd/yyyy HH:mm}" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDownload" runat="server" CommandName="Download" 
                                CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-outline-primary">Download</asp:LinkButton>
                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" 
                                CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-outline-danger"
                                OnClientClick="return confirm('Are you sure you want to delete this content?');">Delete</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
