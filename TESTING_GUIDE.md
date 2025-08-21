# 🧪 AcademIQ LMS Testing Guide

## 🚀 Quick Start

The application is now running with sample data! Here's how to test all features:

### 📋 **Sample User Accounts**

The system comes with pre-configured test accounts:

| Role | Username | Password | Description |
|------|----------|----------|-------------|
| **Admin** | `admin` | `admin123` | Full system access |
| **Teacher** | `teacher` | `teacher123` | Can create courses and content |
| **Student** | `student` | `student123` | Can access enrolled courses |

## 🧪 **Testing Scenarios**

### 1. **Admin Testing**
1. **Login as Admin**
   - Go to: `https://localhost:5001`
   - Click "Login"
   - Username: `admin`, Password: `admin123`

2. **Admin Features to Test**
   - ✅ View all courses (including teacher-created ones)
   - ✅ Create new courses
   - ✅ Upload content to any course
   - ✅ Create quizzes for any course
   - ✅ View all student attempts and progress

### 2. **Teacher Testing**
1. **Login as Teacher**
   - Username: `teacher`, Password: `teacher123`

2. **Teacher Features to Test**
   - ✅ View your created courses
   - ✅ Create new courses
   - ✅ Upload educational content (PDFs, documents, etc.)
   - ✅ Create quizzes with multiple question types
   - ✅ View student quiz attempts and scores

### 3. **Student Testing**
1. **Login as Student**
   - Username: `student`, Password: `student123`

2. **Student Features to Test**
   - ✅ View enrolled courses
   - ✅ Download course content
   - ✅ Take quizzes and see results
   - ✅ View quiz scores and progress

## 📁 **Sample Data Included**

### **Courses**
- "Introduction to Programming" (Teacher: John Teacher)
- "Web Development Fundamentals" (Teacher: John Teacher)

### **Content**
- Programming Basics PDF (1MB sample file)
- Web Development Guide (2MB sample document)

### **Quizzes**
- "Programming Fundamentals Quiz" with:
  - Multiple choice question about C# syntax
  - True/false question about C# being OOP

### **Enrollments**
- Sarah Student is enrolled in "Introduction to Programming"

## 🔄 **Complete Testing Workflow**

### **Step 1: Admin Testing**
1. Login as `admin` / `admin123`
2. Navigate to "Courses" - should see all courses
3. Create a new course
4. Navigate to "Content" - should see all content
5. Upload a test file
6. Navigate to "Quizzes" - should see all quizzes

### **Step 2: Teacher Testing**
1. Login as `teacher` / `teacher123`
2. Navigate to "Courses" - should see your courses only
3. Create a new course
4. Upload content to your course
5. Create a quiz with questions
6. View student attempts

### **Step 3: Student Testing**
1. Login as `student` / `student123`
2. Navigate to "Courses" - should see enrolled courses only
3. Download course content
4. Take the "Programming Fundamentals Quiz"
5. View your quiz results

## 🎯 **Key Features to Test**

### **File Upload System**
- Try uploading different file types (PDF, DOC, TXT, etc.)
- Verify files are stored in `wwwroot/uploads/`
- Test file download functionality

### **Quiz System**
- Create quizzes with different question types
- Test multiple choice questions
- Test true/false questions
- Verify automatic grading works
- Check quiz results display

### **Role-Based Access**
- Verify admins can access everything
- Verify teachers can only access their own content
- Verify students can only access enrolled courses

### **Session Management**
- Test login/logout functionality
- Verify session persistence across pages
- Test session timeout (30 minutes)

## 🐛 **Common Test Cases**

### **Authentication**
- ✅ Login with correct credentials
- ✅ Login with incorrect credentials (should show error)
- ✅ Logout functionality
- ✅ Session persistence

### **Authorization**
- ✅ Admin can access all features
- ✅ Teacher can only access their own content
- ✅ Student can only access enrolled courses
- ✅ Unauthorized access attempts are blocked

### **Data Validation**
- ✅ Required fields validation
- ✅ Email format validation
- ✅ File upload validation
- ✅ Quiz submission validation

### **Error Handling**
- ✅ 404 errors for non-existent resources
- ✅ 403 errors for unauthorized access
- ✅ Validation errors display properly
- ✅ Database errors are handled gracefully

## 📊 **Performance Testing**

### **Database Performance**
- ✅ Page load times are reasonable
- ✅ Database queries are optimized
- ✅ No N+1 query problems

### **File Upload Performance**
- ✅ File uploads work correctly
- ✅ Large files are handled properly
- ✅ File downloads are fast

## 🔒 **Security Testing**

### **Input Validation**
- ✅ SQL injection prevention
- ✅ XSS prevention
- ✅ File upload security
- ✅ CSRF protection

### **Authentication Security**
- ✅ Password hashing (SHA256)
- ✅ Session security
- ✅ Role-based access control

## 🎉 **Success Criteria**

Your LMS is working correctly if you can:

1. ✅ Login with all three user types
2. ✅ Create and manage courses as teacher/admin
3. ✅ Upload and download content
4. ✅ Create and take quizzes
5. ✅ View quiz results and scores
6. ✅ Access is properly restricted by role
7. ✅ All forms validate correctly
8. ✅ Error messages display properly

## 🚨 **Troubleshooting**

### **If the application won't start:**
1. Check if port 5001/5000 is available
2. Verify .NET 9.0 is installed
3. Run `dotnet restore` and `dotnet build`

### **If database errors occur:**
1. Delete `AcademIQ_LMS.db` file
2. Run `dotnet ef database update`
3. Restart the application

### **If file uploads don't work:**
1. Check `wwwroot/uploads/` directory exists
2. Verify file permissions
3. Check file size limits

## 📝 **Next Steps After Testing**

Once you've verified everything works:

1. **Add more features** (see README.md for ideas)
2. **Improve the UI/UX**
3. **Add more comprehensive tests**
4. **Deploy to production**
5. **Add real content and users**

---

**Happy Testing! 🎓** 