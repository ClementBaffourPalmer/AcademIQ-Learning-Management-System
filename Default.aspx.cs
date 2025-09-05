using System;
using System.Web.UI;

namespace AcademIQ_LMS
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user is logged in
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    // Redirect to appropriate dashboard based on role
                    string userRole = GetUserRole();
                    switch (userRole)
                    {
                        case "Admin":
                            Response.Redirect("~/Admin/Default.aspx");
                            break;
                        case "Teacher":
                            Response.Redirect("~/Teacher/Default.aspx");
                            break;
                        case "Student":
                            Response.Redirect("~/Student/Default.aspx");
                            break;
                    }
                }
            }
        }

        protected void btnGetStarted_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/Login.aspx");
        }

        private string GetUserRole()
        {
            // This would typically come from the database or session
            // For now, we'll use a simple approach
            if (HttpContext.Current.User.IsInRole("Admin"))
                return "Admin";
            else if (HttpContext.Current.User.IsInRole("Teacher"))
                return "Teacher";
            else if (HttpContext.Current.User.IsInRole("Student"))
                return "Student";
            else
                return "";
        }
    }
}
