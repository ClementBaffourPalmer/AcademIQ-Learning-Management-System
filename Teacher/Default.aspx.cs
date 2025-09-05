using System;
using System.Linq;
using System.Web.UI;
using AcademIQ_LMS.Data;
using AcademIQ_LMS.Models;
using System.Data.Entity;

namespace AcademIQ_LMS.Teacher
{
    public partial class Default : Page
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

                LoadTeacherData();
            }
        }

        private void LoadTeacherData()
        {
            int teacherId = Convert.ToInt32(Session["UserId"]);
            
            using (var context = new ApplicationDbContext())
            {
                // Load teacher name
                var teacher = context.Users.FirstOrDefault(u => u.Id == teacherId);
                if (teacher != null)
                {
                    litTeacherName.Text = $"{teacher.FirstName} {teacher.LastName}";
                }

                // Load recent content
                var recentContent = context.Contents
                    .Where(c => c.UploadedById == teacherId && c.IsActive)
                    .OrderByDescending(c => c.UploadedAt)
                    .Take(5)
                    .ToList();
                gvRecentContent.DataSource = recentContent;
                gvRecentContent.DataBind();

                // Load recent quizzes
                var recentQuizzes = context.Quizzes
                    .Where(q => q.CreatedById == teacherId && q.IsActive)
                    .OrderByDescending(q => q.CreatedAt)
                    .Take(5)
                    .ToList();
                gvRecentQuizzes.DataSource = recentQuizzes;
                gvRecentQuizzes.DataBind();
            }
        }
    }
}
