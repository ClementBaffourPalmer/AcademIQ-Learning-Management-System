using System;
using System.Web.UI;

namespace AcademIQ_LMS
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UpdateNavigation();
            }
        }

        private void UpdateNavigation()
        {
            bool isAuthenticated = HttpContext.Current.User.Identity.IsAuthenticated;
            
            pnlAuthenticated.Visible = isAuthenticated;
            pnlNotAuthenticated.Visible = !isAuthenticated;
            pnlUserInfo.Visible = isAuthenticated;

            if (isAuthenticated)
            {
                string userName = HttpContext.Current.User.Identity.Name;
                litUserName.Text = userName;
            }
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/Default.aspx");
        }
    }
}
