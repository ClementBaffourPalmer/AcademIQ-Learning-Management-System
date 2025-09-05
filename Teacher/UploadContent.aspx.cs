using System;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using AcademIQ_LMS.Data;
using AcademIQ_LMS.Models;
using System.Data.Entity;

namespace AcademIQ_LMS.Teacher
{
    public partial class UploadContent : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user is authenticated and is a teacher
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/Account/Login.aspx");
                    return;
                }

                string userRole = Session["UserRole"]?.ToString();
                if (userRole != "Teacher")
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                LoadContent();
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (Page.IsValid && fileUpload.HasFile)
            {
                try
                {
                    // Validate file type
                    string fileName = fileUpload.FileName;
                    string extension = Path.GetExtension(fileName).ToLower();
                    string[] allowedExtensions = { ".pdf", ".doc", ".docx", ".ppt", ".pptx", ".zip", ".jpg", ".jpeg", ".png", ".mp4" };

                    if (!allowedExtensions.Contains(extension))
                    {
                        ShowError("Invalid file type. Please upload a supported file format.");
                        return;
                    }

                    // Validate file size (10MB limit)
                    if (fileUpload.FileBytes.Length > 10 * 1024 * 1024)
                    {
                        ShowError("File size too large. Maximum size is 10MB.");
                        return;
                    }

                    // Create uploads directory if it doesn't exist
                    string uploadPath = Server.MapPath("~/Uploads/Content/");
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    // Generate unique filename
                    string uniqueFileName = Guid.NewGuid().ToString() + extension;
                    string filePath = Path.Combine(uploadPath, uniqueFileName);

                    // Save file
                    fileUpload.SaveAs(filePath);

                    // Save to database
                    using (var context = new ApplicationDbContext())
                    {
                        var content = new Content
                        {
                            Title = txtTitle.Text.Trim(),
                            Description = txtDescription.Text.Trim(),
                            Type = (ContentType)Enum.Parse(typeof(ContentType), ddlContentType.SelectedValue),
                            FileName = fileName,
                            FilePath = "~/Uploads/Content/" + uniqueFileName,
                            FileSize = fileUpload.FileBytes.Length,
                            UploadedById = Convert.ToInt32(Session["UserId"]),
                            UploadedAt = DateTime.UtcNow,
                            IsActive = true
                        };

                        context.Contents.Add(content);
                        context.SaveChanges();
                    }

                    ShowSuccess("Content uploaded successfully!");
                    ClearForm();
                    LoadContent();
                }
                catch (Exception ex)
                {
                    ShowError("An error occurred while uploading the file: " + ex.Message);
                }
            }
        }

        protected void gvContent_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int contentId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Download")
            {
                DownloadContent(contentId);
            }
            else if (e.CommandName == "Delete")
            {
                DeleteContent(contentId);
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
                        Response.Clear();
                        Response.ContentType = "application/octet-stream";
                        Response.AddHeader("Content-Disposition", $"attachment; filename=\"{content.FileName}\"");
                        Response.TransmitFile(filePath);
                        Response.End();
                    }
                }
            }
        }

        private void DeleteContent(int contentId)
        {
            using (var context = new ApplicationDbContext())
            {
                var content = context.Contents.FirstOrDefault(c => c.Id == contentId);
                if (content != null)
                {
                    // Soft delete
                    content.IsActive = false;
                    context.SaveChanges();

                    ShowSuccess("Content deleted successfully!");
                    LoadContent();
                }
            }
        }

        private void LoadContent()
        {
            int teacherId = Convert.ToInt32(Session["UserId"]);
            
            using (var context = new ApplicationDbContext())
            {
                var content = context.Contents
                    .Where(c => c.UploadedById == teacherId && c.IsActive)
                    .OrderByDescending(c => c.UploadedAt)
                    .ToList();
                gvContent.DataSource = content;
                gvContent.DataBind();
            }
        }

        private void ShowSuccess(string message)
        {
            litSuccess.Text = message;
            pnlSuccess.Visible = true;
            pnlError.Visible = false;
        }

        private void ShowError(string message)
        {
            litError.Text = message;
            pnlError.Visible = true;
            pnlSuccess.Visible = false;
        }

        private void ClearForm()
        {
            txtTitle.Text = string.Empty;
            txtDescription.Text = string.Empty;
            ddlContentType.SelectedIndex = 0;
        }
    }
}
