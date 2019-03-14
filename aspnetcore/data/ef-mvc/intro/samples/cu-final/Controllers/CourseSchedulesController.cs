using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Controllers
{
    public class CourseSchedulesController : Controller
    {
        private readonly SchoolContext _context;

        public CourseSchedulesController(SchoolContext context)
        {
            _context = context;
        }

        // GET: CourseSchedules
        public async Task<IActionResult> Index()
        {
            var allSchedule = await _context.CourseSchedule.ToListAsync();
            var allScheduleVM = (from s in allSchedule
                                 from i in _context.Instructors
                                 from c in _context.Courses
                                 where s.CourseID == c.CourseID && s.InstructorID == i.ID
                                 select new CourseScheduleVM
                                 {
                                     CourseScheduleID = s.CourseScheduleID,
                                     CourseName = c.Title,
                                     InstructorName = i.Name,
                                     ScheduleDate = s.ScheduleDate
                                 }).ToList();
            return View(allScheduleVM);
        }

        // GET: CourseSchedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseSchedule = await _context.CourseSchedule
                .SingleOrDefaultAsync(m => m.CourseScheduleID == id);
            if (courseSchedule == null)
            {
                return NotFound();
            }

            return View(courseSchedule);
        }

        // GET: CourseSchedules/Create
        public async Task<IActionResult> Create()
        {
            var allEnrollment = await _context.Enrollments.Include(c => c.Course).Include(c => c.Student).ToListAsync();
            var allCourseAssiment = await _context.CourseAssignments.Include(c => c.Course).Include(c => c.Instructor).ToListAsync();
            ViewData["allEnrollment"] = allEnrollment;
            ViewData["allEnrollment"] = allCourseAssiment;
            return View();
        }
        public JsonResult GetCourseDropDownList()
        {
            return Json(new
            {
                allEnrollment = _context.Enrollments.Include(c => c.Course).Include(c => c.Student).ToList(),
                allCourseAssiment = _context.CourseAssignments.Include(c => c.Course).Include(c => c.Instructor).ToList()
            });
        }

        // POST: CourseSchedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseScheduleID,CourseID,InstructorID,StudentID,IsAskForLeave,ScheduleDate")] CourseSchedule courseSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courseSchedule);
        }

        // GET: CourseSchedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseSchedule = await _context.CourseSchedule.SingleOrDefaultAsync(m => m.CourseScheduleID == id);
            if (courseSchedule == null)
            {
                return NotFound();
            }
            return View(courseSchedule);
        }

        // POST: CourseSchedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseScheduleID,CourseID,InstructorID,StudentID,IsAskForLeave,ScheduleDate")] CourseSchedule courseSchedule)
        {
            if (id != courseSchedule.CourseScheduleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseScheduleExists(courseSchedule.CourseScheduleID))
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
            return View(courseSchedule);
        }

        // GET: CourseSchedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseSchedule = await _context.CourseSchedule
                .SingleOrDefaultAsync(m => m.CourseScheduleID == id);
            if (courseSchedule == null)
            {
                return NotFound();
            }

            return View(courseSchedule);
        }

        // POST: CourseSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseSchedule = await _context.CourseSchedule.SingleOrDefaultAsync(m => m.CourseScheduleID == id);
            _context.CourseSchedule.Remove(courseSchedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseScheduleExists(int id)
        {
            return _context.CourseSchedule.Any(e => e.CourseScheduleID == id);
        }
    }
}
