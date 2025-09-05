using System;
using System.Linq;
using System.Web.UI;
using AcademIQ_LMS.Data;
using AcademIQ_LMS.Models;
using System.Data.Entity;

namespace AcademIQ_LMS.Student
{
    public partial class Default : Page
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

                LoadStudentData();
            }
        }

        private void LoadStudentData()
        {
            int studentId = Convert.ToInt32(Session["UserId"]);
            
            using (var context = new ApplicationDbContext())
            {
                // Load student name
                var student = context.Users.FirstOrDefault(u => u.Id == studentId);
                if (student != null)
                {
                    litStudentName.Text = $"{student.FirstName} {student.LastName}";
                }

                // Load recent content (all content for now, since we don't have course enrollment yet)
                var recentContent = context.Contents
                    .Where(c => c.IsActive)
                    .OrderByDescending(c => c.UploadedAt)
                    .Take(5)
                    .ToList();
                gvRecentContent.DataSource = recentContent;
                gvRecentContent.DataBind();

                // Load available quizzes
                var availableQuizzes = context.Quizzes
                    .Where(q => q.IsActive)
                    .OrderByDescending(q => q.CreatedAt)
                    .Take(5)
                    .ToList();
                gvAvailableQuizzes.DataSource = availableQuizzes;
                gvAvailableQuizzes.DataBind();
            }
        }
    }
}
