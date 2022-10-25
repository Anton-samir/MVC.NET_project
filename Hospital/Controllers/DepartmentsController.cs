using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital.Data;
using Hospital.Models;

namespace Hospital.Controllers
{
    public class DepartmentsController : Controller
    {
        List<Department> dept = new List<Department>()
    {
        new Department(){
        Id = 2001,
        Name = "Anton",
        Description = "Developer",
        },
        new Department(){
        Id = 2002,
        Name = "Bavly",
        Description = "Tester",
        },
        new Department(){
        Id = 2003,
        Name = "John",
        Description = "Desginer",
        
        }
    };
        private readonly ApplicationDbContext _context;
        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult GetIndexView()
        {
            return View("Index", _context.Departments);
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(_context.Departments);
        }
        Department dept1 = new Department()
        {
            Id = 2001,
            Name = "Anton",
            Description = "Developer",
        };
        [HttpGet]
        public IActionResult GetDetailsView(int id)
        {
            Department dept2 = _context.Departments.Include(d => d.Doctors).FirstOrDefault(e => e.Id == id);
            if (dept2 == null)
            {
                return NotFound();
            }
            return View("Details", dept2);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            Department dept2 = _context.Departments.Include(d => d.Doctors).FirstOrDefault(e => e.Id == id);
            return View(dept2);
        }
        [HttpGet]
        public IActionResult GetCreateView()
        {
            
            return View("Create");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AddDepartment([Bind("Id,Name,Description")] Department dept3)
        {
            if (ModelState.IsValid == true)
            {
                _context.Departments.Add(dept3);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("Create");
            }
        }

        public ApplicationDbContext Get_context()
        {
            return _context;
        }

        public IActionResult GetEditView(int id)
        {
            Department dept3 = _context.Departments.FirstOrDefault(e => e.Id == id);
            if (dept3 == null)
            {
                return NotFound();
            }
            else
            {
                return View("Edit", dept3);
            }

        }
        public IActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCurrent(int id, [Bind("Id,Name,Description")] Department dept3)
        {
            if (dept3.Id != id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid == true)
            {
                _context.Departments.Update(dept3);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                return RedirectToAction("Edit");
            }
        }
        [HttpGet]
        public IActionResult GetDeleteView(int id)
        {
            Department dept3 = _context.Departments.FirstOrDefault(e => e.Id == id);
            if (dept3 == null)
            {
                return NotFound();
            }
            else
            {
                return View("Delete", dept3);
            }

        }
        public IActionResult Delete()
        {
            return View();
        }
        public IActionResult DeleteCurrent(int id)
        {
            Department dept3 = _context.Departments.FirstOrDefault(e => e.Id == id);
            _context.Departments.Remove(dept3);
            _context.SaveChanges();
            return RedirectToAction("GetIndexView");

        }
    }
}
