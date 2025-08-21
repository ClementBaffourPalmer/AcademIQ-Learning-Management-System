using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // <-- Needed for SelectListItem
using Microsoft.EntityFrameworkCore;
using AcademIQ_LMS.Data;
using AcademIQ_LMS.Models;

namespace AcademIQ_LMS.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Course/Create
        public async Task<IActionResult> Create()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Admin" && userRole != "Teacher")
            {
                return Forbid();
            }

            if (userRole == "Admin")
            {
                var availableCourses = await _context.Users
                    .Where(u => u.Role == UserRole.Teacher && u.IsActive)
                    .OrderBy(u => u.FirstName)
                    .ThenBy(u => u.LastName)
                    .ToListAsync();

              ViewBag.AvailableCourses = availableCourses
    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.FirstName + " " + c.LastName })
    .ToList(); 
            }

            return View();
        }

        // POST: Course/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,TeacherId")] Course course)
        {
            var userId = HttpContext.Session.GetString("UserId");
            var userRole = HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var userIdInt = int.Parse(userId);

            if (userRole == "Admin")
            {
                if (course.TeacherId <= 0)
                {
                    ModelState.AddModelError("TeacherId", "Please select a teacher for this course.");

                    var availableCourses = await _context.Users
                        .Where(u => u.Role == UserRole.Teacher && u.IsActive)
                        .OrderBy(u => u.FirstName)
                        .ThenBy(u => u.LastName)
                        .ToListAsync();

                    ViewBag.AvailableCourses = availableCourses
                        .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.FirstName + " " + c.LastName })
                        .ToList();

                    return View(course);
                }
            }
            else if (userRole == "Teacher")
            {
                course.TeacherId = userIdInt;
            }

            if (string.IsNullOrWhiteSpace(course.Title))
            {
                ModelState.AddModelError("Title", "Course title is required.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    course.CreatedAt = DateTime.UtcNow;
                    course.IsActive = true;

                    _context.Add(course);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Course created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred while creating the course: {ex.Message}");
                }
            }

            if (userRole == "Admin")
            {
                var availableCourses = await _context.Users
                    .Where(u => u.Role == UserRole.Teacher && u.IsActive)
                    .OrderBy(u => u.FirstName)
                    .ThenBy(u => u.LastName)
                    .ToListAsync();

                ViewBag.AvailableCourses = availableCourses
    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.FirstName + " " + c.LastName })
    .ToList();
            }

            return View(course);
        }

        // GET: Course/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            var userRole = HttpContext.Session.GetString("UserRole");
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var userIdInt = int.Parse(userId);

            if (userRole != "Admin" && course.TeacherId != userIdInt)
            {
                return Forbid();
            }

            if (userRole == "Admin")
            {
                var availableCourses = await _context.Users
                    .Where(u => u.Role == UserRole.Teacher && u.IsActive)
                    .OrderBy(u => u.FirstName)
                    .ThenBy(u => u.LastName)
                    .ToListAsync();

                ViewBag.AvailableCourses = availableCourses
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.FirstName + " " + c.LastName })
                    .ToList();
            }

            return View(course);
        }

        // POST: Course/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,TeacherId")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingCourse = await _context.Courses.FindAsync(id);
                    if (existingCourse == null)
                    {
                        return NotFound();
                    }

                    var userRole = HttpContext.Session.GetString("UserRole");
                    var userId = HttpContext.Session.GetString("UserId");

                    if (string.IsNullOrEmpty(userId))
                    {
                        return RedirectToAction("Login", "Account");
                    }

                    var userIdInt = int.Parse(userId);

                    if (userRole != "Admin" && existingCourse.TeacherId != userIdInt)
                    {
                        return Forbid();
                    }

                    existingCourse.Title = course.Title;
                    existingCourse.Description = course.Description;

                    if (userRole == "Admin")
                    {
                        existingCourse.TeacherId = course.TeacherId;
                    }

                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Course updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            var userRoleForError = HttpContext.Session.GetString("UserRole");
            if (userRoleForError == "Admin")
            {
                var availableCourses = await _context.Users
                    .Where(u => u.Role == UserRole.Teacher && u.IsActive)
                    .OrderBy(u => u.FirstName)
                    .ThenBy(u => u.LastName)
                    .ToListAsync();

                ViewBag.AvailableCourses = availableCourses
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.FirstName + " " + c.LastName })
                    .ToList();
            }

            return View(course);
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
