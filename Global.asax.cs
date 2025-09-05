using System;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using AcademIQ_LMS.Data;
using System.Data.Entity;

namespace AcademIQ_LMS
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Initialize database
            Database.SetInitializer(new CreateDatabaseIfNotExists<ApplicationDbContext>());
            using (var context = new ApplicationDbContext())
            {
                context.Database.Initialize(false);
                DbInitializer.Initialize(context);
            }
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Session start logic
        }

        void Application_BeginRequest(object sender, EventArgs e)
        {
            // Begin request logic
        }

        void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            // Authentication logic
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Error handling logic
        }

        void Session_End(object sender, EventArgs e)
        {
            // Session end logic
        }

        void Application_End(object sender, EventArgs e)
        {
            // Application end logic
        }
    }
}
