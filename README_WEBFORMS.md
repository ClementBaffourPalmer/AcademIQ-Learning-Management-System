# AcademIQ LMS - Web Forms Version

This is the ASP.NET Web Forms version of the AcademIQ Learning Management System, converted from the original ASP.NET Core MVC application.

## Architecture Changes

### From ASP.NET Core MVC to ASP.NET Web Forms

The application has been completely converted from ASP.NET Core MVC to ASP.NET Web Forms (.NET Framework 4.8). Here are the key changes:

#### 1. **Project Structure**
- **Old**: Controllers, Views, Program.cs
- **New**: .aspx pages, .aspx.cs code-behind files, Global.asax

#### 2. **Configuration**
- **Old**: appsettings.json, Program.cs startup
- **New**: Web.config, Global.asax.cs

#### 3. **Database**
- **Old**: SQLite with Entity Framework Core
- **New**: SQL Server LocalDB with Entity Framework 6

#### 4. **Authentication**
- **Old**: Session-based authentication
- **New**: Forms Authentication with Session support

## Key Features

### ✅ User Management
- User registration and login
- Role-based access control (Admin, Teacher, Student)
- Session management

### ✅ Content Management
- Upload educational content (PDF, Documents, Videos, Images)
- Content categorization by course
- File download with access control
- Content deletion (soft delete)

### ✅ Course Management
- Course creation and management
- Teacher-course assignments
- Student enrollment

### ✅ Security Features
- Password hashing (SHA256)
- Role-based authorization
- File upload validation
- Access control for content and courses

## File Structure

```
AcademIQ-LMS/
├── Account/
│   ├── Login.aspx
│   ├── Login.aspx.cs
│   ├── Register.aspx
│   └── Register.aspx.cs
├── Content/
│   ├── Content.aspx
│   ├── Content.aspx.cs
│   ├── CreateContent.aspx
│   ├── CreateContent.aspx.cs
│   └── Site.css
├── Data/
│   ├── ApplicationDbContext.cs
│   └── DbInitializer.cs
├── Models/
│   ├── Content.cs
│   ├── Course.cs
│   ├── Enrollment.cs
│   └── User.cs
├── wwwroot/
│   └── uploads/
├── Default.aspx
├── Default.aspx.cs
├── Global.asax
├── Global.asax.cs
├── Site.Master
├── Site.master.cs
├── Web.config
└── AcademIQ_LMS.csproj
```

## Setup Instructions

### Prerequisites
- Visual Studio 2019/2022
- .NET Framework 4.8
- SQL Server LocalDB

### Installation Steps

1. **Open the project in Visual Studio**
   ```
   Open AcademIQ_LMS.csproj in Visual Studio
   ```

2. **Restore NuGet packages**
   ```
   Right-click on the project → Restore NuGet Packages
   ```

3. **Build the project**
   ```
   Build → Build Solution (Ctrl+Shift+B)
   ```

4. **Run the application**
   ```
   Debug → Start Debugging (F5)
   ```

### Default Users

The application comes with pre-seeded users:

- **Admin**: username: `admin`, password: `admin123`
- **Teacher**: username: `teacher`, password: `teacher123`
- **Student**: username: `student`, password: `student123`

## Key Differences from MVC Version

### 1. **Page Lifecycle**
- Web Forms uses a different page lifecycle (Page_Load, PostBack, etc.)
- ViewState management for form data persistence
- Server-side controls instead of HTML helpers

### 2. **Data Binding**
- Repeater controls instead of Razor foreach loops
- DataSource and DataBind() methods
- Eval() and Bind() expressions

### 3. **Form Handling**
- ASP.NET Web Controls (TextBox, DropDownList, etc.)
- Validation controls (RequiredFieldValidator, etc.)
- PostBack mechanism

### 4. **Navigation**
- Response.Redirect() instead of RedirectToAction()
- Server.MapPath() for file operations
- Session management through HttpContext.Session

## Security Considerations

1. **Authentication**: Forms Authentication with session support
2. **Authorization**: Role-based access control
3. **File Upload**: Extension validation and secure file storage
4. **SQL Injection**: Entity Framework parameterized queries
5. **XSS Protection**: Server controls provide built-in protection

## Database

The application uses SQL Server LocalDB with Entity Framework 6. The database is automatically created on first run with sample data.

### Connection String
```xml
<connectionStrings>
  <add name="DefaultConnection" 
       connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\AcademIQ_LMS.mdf;Integrated Security=True" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

## Troubleshooting

### Common Issues

1. **Build Errors**: Ensure .NET Framework 4.8 is installed
2. **Database Errors**: Check LocalDB installation
3. **File Upload Issues**: Ensure uploads folder has write permissions
4. **Session Issues**: Check Web.config session configuration

### Performance Tips

1. Enable ViewState only where necessary
2. Use DataBind() efficiently
3. Implement proper caching strategies
4. Optimize database queries

## Migration Notes

This conversion maintains all the original functionality while adapting to Web Forms patterns:

- ✅ User authentication and authorization
- ✅ Content upload and management
- ✅ Course management
- ✅ File handling and security
- ✅ Role-based access control
- ✅ Database operations
- ✅ Session management

The application is now ready for deployment in traditional Windows hosting environments that support ASP.NET Web Forms.
