using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XX_MountainBrigade.Models;
using System.Threading.Tasks;

namespace XX_MountainBrigade.Controllers
{

    public class AccountController : Controller
    {
        private readonly XXMBContext _context;

        public AccountController(XXMBContext context)
        {
            _context = context;
        }

        // GET: Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string persNo)
        {
            var personnel = await _context.tblPersonnel
                .Include(p => p.Company)
                .FirstOrDefaultAsync(p => p.PersNo == persNo);

            if (personnel != null)
            {
                // Store the user's Company ID in the session
                HttpContext.Session.SetInt32("CompanyId", personnel.CoyId);

                return RedirectToAction("RegimentAndCompany", "PersonnelMvc");
            }
            else
            {
                TempData["Message"] = "Invalid Personnel Number.";
                return View();
            }
        }

        // GET: Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("CompanyId");
            return RedirectToAction("Login");
        }
    }


}
