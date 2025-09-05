using System;
using System.Linq;
using System.Web.UI;
using AcademIQ_LMS.Data;
using AcademIQ_LMS.Models;
using System.Data.Entity;

namespace AcademIQ_LMS.Admin
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user is authenticated and is an admin
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/Account/Login.aspx");
                    return;
                }

                string userRole = Session["UserRole"]?.ToString();
                if (userRole != "Admin")
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                LoadDashboardData();
            }
        }

        private void LoadDashboardData()
        {
            using (var context = new ApplicationDbContext())
            {
                // Load admin name
                int adminId = Convert.ToInt32(Session["UserId"]);
                var admin = context.Users.FirstOrDefault(u => u.Id == adminId);
                if (admin != null)
                {
                    litAdminName.Text = $"{admin.FirstName} {admin.LastName}";
                }

                // Load statistics
                litTotalUsers.Text = context.Users.Count(u => u.IsActive).ToString();
                litTotalContent.Text = context.Contents.Count(c => c.IsActive).ToString();
                litTotalQuizzes.Text = context.Quizzes.Count(q => q.IsActive).ToString();
                litActiveSessions.Text = "0"; // This would need session tracking

                // Load user counts by role
                litTeacherCount.Text = context.Users.Count(u => u.Role == UserRole.Teacher && u.IsActive).ToString();
                litStudentCount.Text = context.Users.Count(u => u.Role == UserRole.Student && u.IsActive).ToString();

                // Load content counts by type
                litPdfCount.Text = context.Contents.Count(c => c.Type == ContentType.PDF && c.IsActive).ToString();
                litVideoCount.Text = context.Contents.Count(c => c.Type == ContentType.Video && c.IsActive).ToString();

                // Load recent users
                var recentUsers = context.Users
                    .Where(u => u.IsActive)
                    .OrderByDescending(u => u.CreatedAt)
                    .Take(10)
                    .ToList();
                gvRecentUsers.DataSource = recentUsers;
                gvRecentUsers.DataBind();
            }
        }
    }
}
