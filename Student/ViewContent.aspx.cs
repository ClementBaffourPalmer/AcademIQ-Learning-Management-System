using System;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using AcademIQ_LMS.Data;
using AcademIQ_LMS.Models;
using System.Data.Entity;

namespace AcademIQ_LMS.Student
{
    public partial class ViewContent : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user is authenticated and is a student
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/Account/Login.aspx");
                    return;
                }

                string userRole = Session["UserRole"]?.ToString();
                if (userRole != "Student")
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                LoadContent();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadContent();
        }

        protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadContent();
        }

        protected void gvContent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                int contentId = Convert.ToInt32(e.CommandArgument);
                DownloadContent(contentId);
            }
        }

        protected void gvContent_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvContent.PageIndex = e.NewPageIndex;
            LoadContent();
        }

        private void LoadContent()
        {
            using (var context = new ApplicationDbContext())
            {
                var query = context.Contents
                    .Include("UploadedBy")
                    .Where(c => c.IsActive);

                // Apply search filter
                if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                {
                    string searchTerm = txtSearch.Text.Trim().ToLower();
                    query = query.Where(c => 
                        c.Title.ToLower().Contains(searchTerm) || 
                        c.Description.ToLower().Contains(searchTerm));
                }

                // Apply type filter
                if (!string.IsNullOrEmpty(ddlFilter.SelectedValue))
                {
                    ContentType filterType = (ContentType)Enum.Parse(typeof(ContentType), ddlFilter.SelectedValue);
                    query = query.Where(c => c.Type == filterType);
                }

                // Order by upload date (newest first)
                var content = query.OrderByDescending(c => c.UploadedAt).ToList();
                gvContent.DataSource = content;
                gvContent.DataBind();
            }
        }

        private void DownloadContent(int contentId)
        {
            using (var context = new ApplicationDbContext())
            {
                var content = context.Contents.FirstOrDefault(c => c.Id == contentId);
                if (content != null)
                {
                    string filePath = Server.MapPath(content.FilePath);
                    if (File.Exists(filePath))
                    {
                        try
                        {
                            Response.Clear();
                            Response.ContentType = "application/octet-stream";
                            Response.AddHeader("Content-Disposition", $"attachment; filename=\"{content.FileName}\"");
                            Response.TransmitFile(filePath);
                            Response.End();
                        }
                        catch (Exception ex)
                        {
                            // Log error or show message
                            Response.Write("Error downloading file: " + ex.Message);
                        }
                    }
                    else
                    {
                        Response.Write("File not found on server.");
                    }
                }
            }
        }
    }
}
