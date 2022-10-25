using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Hospital.Data;
using Hospital.Models;

namespace Hospital.Controllers
{

    public class DoctorsController : Controller
    {

        /*public string GreetVistor()
        {
            return "Welcom In My First Project In ITI";
        }
        public string GreetUser(string name)
        {
            return "Hi, " + name + "!";
        }
        public double GetPrice(double cost, double profitratio)
        {
            return cost + cost * profitratio;
        }*/
        List<Doctor> emp = new List<Doctor>()
    {
        new Doctor(){
        Id = 2001,
        FullName = "Anton",
        Postion = "Developer",
        Salary = 800
        },
        new Doctor(){
        Id = 2002,
        FullName = "Bavly",
        Postion = "Tester",
        Salary = 8800
        },
        new Doctor(){
        Id = 2003,
        FullName = "John",
        Postion = "Desginer",
        Salary = 6800
        }
    };

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DoctorsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        [Route("[controller]/Staff")]
        [HttpGet]
        public ActionResult GetIndexView(string? sortType, string? search, string? sortOrder, int pageSize = 2, int pageNumber = 1)
        {
            IQueryable<Doctor> emps = _context.Doctors.AsQueryable();
            if (String.IsNullOrWhiteSpace(search) == false)
            {
                search = search.Trim();
                emps = _context.Doctors.Where(e => e.FullName.Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(sortType) && !string.IsNullOrWhiteSpace(sortOrder))
            {
                if (sortType == "FullName")
                {
                    if (sortOrder == "asc")
                    {
                        emps = emps.OrderBy(e => e.FullName);
                    }
                    else if (sortOrder == "desc")
                    {
                        emps = emps.OrderByDescending(e => e.FullName);
                    }

                }
                else if (sortType == "Postion")
                {
                    if (sortOrder == "asc")
                    {
                        emps = emps.OrderBy(e => e.Postion);
                    }
                    else if (sortOrder == "desc")
                    {
                        emps = emps.OrderByDescending(e => e.Postion);
                    }

                }
                else if (sortType == "Salary")
                {
                    if (sortOrder == "asc")
                    {
                        emps = emps.OrderBy(e => e.Salary);
                    }
                    else if (sortOrder == "desc")
                    {
                        emps = emps.OrderByDescending(e => e.Salary);
                    }

                }
            }
            emps = emps.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            ViewBag.CurrentSearch = search;
            ViewBag.PageSize = pageSize;
            ViewBag.pageNumber = pageNumber;
            return View("Index", emps);
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(_context.Doctors);
        }
        Doctor emp1 = new Doctor()
        {
            Id = 2001,
            FullName = "Anton",
            Postion = "Developer",
            Salary = 800
        };
        [HttpGet]
        public IActionResult GetDetailsView(int id)
        {
            Doctor emp2 = _context.Doctors.Include(e => e.Department).FirstOrDefault(e => e.Id == id);
            if (emp2 == null)
            {
                return NotFound();
            }
            return View("Details", emp2);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            Doctor emp2 = _context.Doctors.Include(e => e.Department).FirstOrDefault(e => e.Id == id);
            return View(emp2);
        }
        [HttpGet]
        public IActionResult GetCreateView()
        {
            ViewBag.AllDepartments = _context.Departments.ToList();
            return View("Create");
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.AllDepartments = _context.Departments.ToList();
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AddEmployee([Bind("Id,FullName,Postion,Salary,Bonus,PhoneNumber,EmailAddress,ConfirmEmail,Password,ConfirmPassword,HiringDateTime,BirthDate,AttendanceTime,LeavingTime,CreatedAt,LastUpdatedAt,DepartmentId")] Doctor emp3, IFormFile? imageFile)
        {
            if (ModelState.IsValid == true)
            {
                if (imageFile == null)
                {
                    emp3.ImageUrl = "\\Image\\No_Image.png";
                }
                else
                {
                    string imgExtendsion = Path.GetExtension(imageFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtendsion;
                    string imgUrl = "\\Image\\" + imgName;
                    emp3.ImageUrl = imgUrl;

                    string imgPath = _webHostEnvironment.WebRootPath + imgUrl;

                    FileStream imgStream = new FileStream(imgPath, FileMode.Create);
                    imageFile.CopyTo(imgStream);
                    imgStream.Dispose();

                }
                emp3.CreatedAt = DateTime.Now;

                _context.Doctors.Add(emp3);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.AllDepartments = _context.Departments.ToList();
                return View("Create");
            }
        }

        public ApplicationDbContext Get_context()
        {
            return _context;
        }

        public IActionResult GetEditView(int id)
        {
            Doctor emp3 = _context.Doctors.FirstOrDefault(e => e.Id == id);
            if (emp3 == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.AllDepartments = _context.Departments.ToList();
                return View("Edit", emp3);
            }

        }
        public IActionResult Edit()
        {
            ViewBag.AllDepartments = _context.Departments.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCurrent(int id, [Bind("Id,FullName,Postion,Salary,Bonus,PhoneNumber,EmailAddress,ConfirmEmail,Password,ConfirmPassword,HiringDateTime,BirthDate,AttendanceTime,LeavingTime,CreatedAt,LastUpdatedAt,DepartmentId")] Doctor emp3, IFormFile? imageFile)
        {
            if (emp3.Id != id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid == true)
            {
                emp3.LastUpdatedAt = DateTime.Now;
                _context.Doctors.Update(emp3);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                ViewBag.AllDepartments = _context.Departments.ToList();
                return View("Edit");
            }
        }
        [HttpGet]
        public IActionResult GetDeleteView(int id)
        {
            Doctor emp3 = _context.Doctors.FirstOrDefault(e => e.Id == id);
            if (emp3 == null)
            {
                return NotFound();
            }
            else
            {
                return View("Delete", emp3);
            }

        }
        public IActionResult Delete()
        {
            return View();
        }
        public IActionResult DeleteCurrent(int id)
        {
            Doctor emp3 = _context.Doctors.FirstOrDefault(e => e.Id == id);
            if (emp3.ImageUrl != "\\Image\\No_Image.png")
            {
                string imgPath = _webHostEnvironment.WebRootPath + emp3.ImageUrl;
                if (System.IO.File.Exists(imgPath))
                {
                    System.IO.File.Delete(imgPath);
                }
            }
            _context.Doctors.Remove(emp3);
            _context.SaveChanges();
            return RedirectToAction("GetIndexView");

        }

    }
}
