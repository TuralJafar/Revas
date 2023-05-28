using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Revas.DAL;
using Revas.Models;
using System.Diagnostics;

namespace Revas.Controllers
{
    public class HomeController : Controller
    {   private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {   List<Employee> employees = await _context.Employees.Include(p=>p.Position).ToListAsync(); 
            return View(employees);
        }

        
    }
}