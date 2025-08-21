# AcademIQ LMS - Learning Management System

## 📚 Overview

AcademIQ LMS is a modern, responsive Learning Management System built with ASP.NET Core MVC that provides a comprehensive platform for educational institutions to manage courses, content, and assessments. The system supports multiple user roles including Administrators, Teachers, and Students, with a beautiful, professional interface designed for optimal user experience across all devices.

## ✨ Features

### 🎯 Core Functionality
- **Multi-Role User Management**: Admin, Teacher, and Student roles with appropriate permissions
- **Course Management**: Create, edit, and manage courses with detailed information
- **Content Management**: Upload and organize educational materials (PDFs, documents, videos, etc.)
- **Quiz System**: Create and take assessments with multiple-choice questions
- **Enrollment System**: Students can enroll in courses with proper access control
- **Progress Tracking**: Monitor student progress and quiz attempts

### 🎨 User Interface
- **Responsive Design**: Optimized for desktop, tablet, and mobile devices
- **Modern UI/UX**: Professional design with Bootstrap 5 and custom styling
- **Background Themes**: Beautiful background images with overlay effects
- **Interactive Elements**: Hover effects, animations, and smooth transitions
- **Accessibility**: High contrast mode support and keyboard navigation

### 🔐 Security & Access Control
- **Session Management**: Secure user sessions with role-based access
- **Permission System**: Granular permissions based on user roles
- **Data Validation**: Comprehensive input validation and sanitization
- **SQLite Database**: Lightweight, secure database for data storage

## 🛠️ Technology Stack

### Backend
- **ASP.NET Core 9.0**: Modern web framework
- **Entity Framework Core**: Object-relational mapping
- **SQLite**: Lightweight database
- **C#**: Primary programming language

### Frontend
- **Bootstrap 5**: Responsive CSS framework
- **Bootstrap Icons**: Professional icon library
- **Custom CSS**: Enhanced styling and animations
- **JavaScript**: Interactive functionality

### Development Tools
- **Visual Studio 2022** / **VS Code**: IDE support
- **Git**: Version control
- **NuGet**: Package management

## 📋 Prerequisites

Before running this project, ensure you have the following installed:

- **.NET 9.0 SDK** or later
- **Visual Studio 2022** (Community/Professional/Enterprise) or **VS Code**
- **Git** for version control

## 🚀 Installation & Setup

### 1. Clone the Repository
```bash
git clone https://github.com/yourusername/AcademIQ-LMS.git
cd AcademIQ-LMS/AcademIQ_LMS
```

### 2. Restore Dependencies
```bash
dotnet restore
```

### 3. Run Database Migrations
```bash
dotnet ef database update
```

### 4. Build the Project
```bash
dotnet build
```

### 5. Run the Application
```bash
dotnet run
```

### 6. Access the Application
Open your browser and navigate to: `https://localhost:7001` or `http://localhost:5000`

## 👥 User Roles & Permissions

### 🔧 Administrator
- **Full System Access**: Manage all courses, users, and content
- **User Management**: Create and manage teacher and student accounts
- **System Configuration**: Configure system-wide settings
- **Analytics**: View comprehensive system reports

### 👨‍🏫 Teacher
- **Course Management**: Create and manage their own courses
- **Content Upload**: Add educational materials to their courses
- **Quiz Creation**: Create assessments for their students
- **Student Progress**: Monitor enrolled student progress

### 👨‍🎓 Student
- **Course Enrollment**: Enroll in available courses
- **Content Access**: Download and view course materials
- **Quiz Taking**: Participate in course assessments
- **Progress Tracking**: View their own progress and results

## 📱 Responsive Design

The application is fully responsive and optimized for:

### 🖥️ Desktop (1200px+)
- Full-featured interface with all elements visible
- Optimal spacing and typography
- Enhanced hover effects and animations

### 📱 Tablet (768px - 1199px)
- Adapted layout for medium screens
- Collapsible navigation menu
- Optimized card layouts

### 📱 Mobile (320px - 767px)
- Touch-friendly interface
- Simplified navigation
- Optimized for portrait and landscape orientations
- Reduced font sizes and spacing

## 🎨 Design Features

### Visual Elements
- **Professional Color Scheme**: Blue-based theme with proper contrast
- **Background Images**: Beautiful field trip imagery with overlay effects
- **Card-Based Layout**: Clean, organized content presentation
- **Gradient Effects**: Modern gradient backgrounds and buttons
- **Icon Integration**: Bootstrap Icons throughout the interface

### Interactive Features
- **Hover Effects**: Smooth transitions on interactive elements
- **Loading States**: Visual feedback for user actions
- **Form Validation**: Real-time input validation
- **Responsive Tables**: Mobile-friendly data presentation

## 📊 Database Schema

### Core Entities
- **Users**: User accounts with role-based permissions
- **Courses**: Educational courses with metadata
- **Content**: Course materials and resources
- **Quizzes**: Assessment components
- **Enrollments**: Student-course relationships
- **Quiz Attempts**: Student assessment records

## 🔧 Configuration

### App Settings
The application uses `appsettings.json` for configuration:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=AcademIQ_LMS.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

### Environment Variables
- `ASPNETCORE_ENVIRONMENT`: Set to "Development" for local development
- `ASPNETCORE_URLS`: Configure application URLs

## 🧪 Testing

### Manual Testing
1. **User Registration**: Test account creation for different roles
2. **Course Management**: Create, edit, and delete courses
3. **Content Upload**: Test file upload functionality
4. **Quiz System**: Create and take quizzes
5. **Responsive Design**: Test on various screen sizes

### Browser Compatibility
- ✅ Chrome (Latest)
- ✅ Firefox (Latest)
- ✅ Safari (Latest)
- ✅ Edge (Latest)

## 🚀 Deployment

### Local Development
```bash
dotnet run --environment Development
```

### Production Deployment
1. Build the application: `dotnet publish -c Release`
2. Deploy to your hosting provider
3. Configure production database
4. Set environment variables

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch: `git checkout -b feature-name`
3. Commit your changes: `git commit -m 'Add feature'`
4. Push to the branch: `git push origin feature-name`
5. Submit a pull request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- **Bootstrap Team**: For the excellent CSS framework
- **Microsoft**: For ASP.NET Core and Entity Framework
- **Bootstrap Icons**: For the comprehensive icon library

## 📞 Support

For support and questions:
- Create an issue in the GitHub repository
- Contact: [your-email@example.com]
- Documentation: [Link to documentation]

---

**Built with ❤️ using ASP.NET Core and Bootstrap** 