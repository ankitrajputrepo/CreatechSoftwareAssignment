using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using XXMountainBrigade.Models;
using System.Threading.Tasks;
using System.ComponentModel.Design;

namespace XXMountainBrigade.Controllers
{
    public class PersonnelController : Controller
    {
        private readonly XXMBContext _context;

        public PersonnelController(XXMBContext context)
        {
            _context = context;
        }

        // Login Page
        [HttpGet]
        public IActionResult Login() => View();

        // Login Action
       
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            Console.WriteLine(loginModel.Username);
            // Check if username is provided
            if (string.IsNullOrEmpty(loginModel.Username))
            {
                ModelState.AddModelError("", "Username is required.");
                return View(loginModel);
            }

            // Validate the login credentials
            var personnel = await _context.tblPersonnel
                .Include(p => p.Company)
                .FirstOrDefaultAsync(p => p.PersNo == loginModel.Username); // Find user by PersNo
            Console.WriteLine(personnel?.FirstName);
            // Check if personnel exists
            if (personnel == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(loginModel);
            }

            // Assuming you have stored passwords securely, use the correct password check.
            if (VerifyPassword(personnel.PersNo, loginModel.Password))
            {
                // Successful login
                HttpContext.Session.SetInt32("CompanyId", personnel.CoyId);  // Store CompanyId

              
                HttpContext.Session.SetString("Username", personnel.PersNo);  // Store Username (PersNo)

                TempData["Message"] = "Login successful!";
                return RedirectToAction("List");  // Redirect to List view after login
            }

            // If login fails, return to login page with error message
            ModelState.AddModelError("", "Invalid username or password.");
            return View(loginModel);
        }


        
        private bool VerifyPassword(string storedPassword, string inputPassword)
        {
            // This is a simple example; in production, you should hash the password and compare.
            return storedPassword == inputPassword; // Replace with actual password comparison logic
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // Personnel List (Filtered by Company)
        public async Task<IActionResult> List()
        {
            var companyId = HttpContext.Session.GetInt32("CompanyId");
            if (companyId == null) return RedirectToAction("Login");

            var personnel = await _context.tblPersonnel
                .Include(p => p.Company)
                .Include(p => p.Rank)
                .Where(p => p.CoyId == companyId)
                .ToListAsync();

            return View(personnel);
        }

        // Create Personnel
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Companies = _context.tblCompany.ToList();
            ViewBag.Ranks = _context.tblRanks.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Personnel personnel)
        {
            if (ModelState.IsValid)
            {
                _context.tblPersonnel.Add(personnel);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Personnel added successfully!";
                return RedirectToAction("List");
            }
            ViewBag.Companies = _context.tblCompany.ToList();
            ViewBag.Ranks = _context.tblRanks.ToList();
            return View(personnel);
        }

        // Edit Personnel
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var personnel = await _context.tblPersonnel.FindAsync(id);
            if (personnel == null) return NotFound();

            ViewBag.Companies = _context.tblCompany.ToList();
            ViewBag.Ranks = _context.tblRanks.ToList();
            return View(personnel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Personnel personnel)
        {
            if (ModelState.IsValid)
            {
                _context.tblPersonnel.Update(personnel);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Personnel updated successfully!";
                return RedirectToAction("List");
            }
            ViewBag.Companies = _context.tblCompany.ToList();
            ViewBag.Ranks = _context.tblRanks.ToList();
            return View(personnel);
        }

        // Delete Personnel
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var personnel = await _context.tblPersonnel.FindAsync(id);
            if (personnel == null) return NotFound();

            _context.tblPersonnel.Remove(personnel);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Personnel deleted successfully!";
            return RedirectToAction("List");
        }
    }
}
