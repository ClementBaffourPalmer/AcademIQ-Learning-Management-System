using System;
using System.Linq;
using System.Web.UI;
using AcademIQ_LMS.Data;
using AcademIQ_LMS.Models;
using System.Security.Cryptography;
using System.Text;

namespace AcademIQ_LMS.Account
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user is already logged in
                if (Session["UserId"] != null)
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                using (var context = new ApplicationDbContext())
                {
                    // Check if username or email already exists
                    var existingUser = context.Users.FirstOrDefault(u => 
                        u.Username == txtUsername.Text.Trim() || 
                        u.Email == txtEmail.Text.Trim());

                    if (existingUser != null)
                    {
                        ShowError("Username or email already exists.");
                        return;
                    }

                    // Create new user
                    var user = new User
                    {
                        FirstName = txtFirstName.Text.Trim(),
                        LastName = txtLastName.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        Username = txtUsername.Text.Trim(),
                        Password = HashPassword(txtPassword.Text),
                        Role = (UserRole)Enum.Parse(typeof(UserRole), ddlRole.SelectedValue),
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    };

                    context.Users.Add(user);
                    context.SaveChanges();

                    ShowSuccess("Registration successful! Please log in.");
                    ClearForm();
                }
            }
        }

        private void ShowError(string message)
        {
            litError.Text = message;
            pnlError.Visible = true;
            pnlSuccess.Visible = false;
        }

        private void ShowSuccess(string message)
        {
            litSuccess.Text = message;
            pnlSuccess.Visible = true;
            pnlError.Visible = false;
        }

        private void ClearForm()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            ddlRole.SelectedIndex = 0;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
