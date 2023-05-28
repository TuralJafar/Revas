using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Revas.DAL;
using Revas.Models;

namespace Revas.Areas.RevasAdmin.Controllers
{
    [Area("RevasAdmin")]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(AppDbContext context,IWebHostEnvironment env )
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Employee> employees = await _context.Employees.Include(e => e.Position).ToListAsync();
            return View(employees);
        }
        public async Task<IActionResult> Create()
        {

            ViewBag.Positions = await _context.Positions.ToListAsync();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {

            bool result = await _context.Positions.AnyAsync(p => p.Id == employee.PositionId);
            if (!result)
            {
                ModelState.AddModelError("PositionId", "There is no position with this Id");
                ViewBag.Positions = await _context.Positions.ToListAsync();
                return View();
            }
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Employee existed = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);

            if (existed == null) return NotFound();

            ViewBag.Positions = await _context.Positions.ToListAsync();

            return View(existed);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Employee employee)
        {

            if (id == null || id < 1) return BadRequest();

            Employee existed = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);

            if (existed == null) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Positions = await _context.Positions.ToListAsync();
                return View(existed);
            }

            if (existed.PositionId != employee.PositionId)
            {
                bool result = await _context.Positions.AnyAsync(p => p.Id == employee.PositionId);
                if (!result)
                {
                    ModelState.AddModelError("PositionId", "There is no position with this Id");
                    ViewBag.Positions = await _context.Positions.ToListAsync();

                    return View(existed);
                }
                existed.PositionId = employee.PositionId;
            }


            existed.Name = employee.Name;
            

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Employee employee = await _context.Employees.FirstOrDefaultAsync(s => s.Id == id);

            if (employee == null) return NotFound();

           

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


    }
}
