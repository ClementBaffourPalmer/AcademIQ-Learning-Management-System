using System;
using System.Linq;
using System.Web.UI;
using System.Web.Security;
using AcademIQ_LMS.Data;
using AcademIQ_LMS.Models;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity;

namespace AcademIQ_LMS.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user is already logged in
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    RedirectToDashboard();
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ShowError("Username and password are required.");
                return;
            }

            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == username && u.IsActive);

                if (user == null || !VerifyPassword(password, user.Password))
                {
                    ShowError("Invalid username or password.");
                    return;
                }

                // Create authentication ticket
                FormsAuthentication.SetAuthCookie(user.Username, false);
                
                // Store user info in session
                Session["UserId"] = user.Id;
                Session["Username"] = user.Username;
                Session["UserRole"] = user.Role.ToString();
                Session["UserFullName"] = $"{user.FirstName} {user.LastName}";

                RedirectToDashboard();
            }
        }

        private void RedirectToDashboard()
        {
            string userRole = Session["UserRole"]?.ToString();
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
                default:
                    Response.Redirect("~/Default.aspx");
                    break;
            }
        }

        private void ShowError(string message)
        {
            litError.Text = message;
            pnlError.Visible = true;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string inputPassword, string storedPassword)
        {
            var hashedInput = HashPassword(inputPassword);
            return hashedInput == storedPassword;
        }
    }
}
