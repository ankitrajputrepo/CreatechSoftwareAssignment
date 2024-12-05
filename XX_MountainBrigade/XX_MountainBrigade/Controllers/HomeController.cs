using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XX_MountainBrigade.Models;

namespace XX_MountainBrigade.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly XXMBContext _context;
        public HomeController(ILogger<HomeController> logger,XXMBContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> Index(string searchQuery)
        {
            // Fetching personnel data with related Company and Rank
            var personnelList = _context.tblPersonnel
                .Include(p => p.Company)
                .Include(p => p.Rank)
                .AsQueryable();

            // Apply search query filtering if provided
            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.ToLower(); // Make it case-insensitive

                personnelList = personnelList.Where(p =>
                    (p.FirstName != null && EF.Functions.Like(p.FirstName.ToLower(), $"%{searchQuery}%")) ||
                    (p.LastName != null && EF.Functions.Like(p.LastName.ToLower(), $"%{searchQuery}%")) ||
                    (p.PersNo != null && EF.Functions.Like(p.PersNo.ToLower(), $"%{searchQuery}%")) ||
                    (p.Company != null && p.Company.CoyName != null && EF.Functions.Like(p.Company.CoyName.ToLower(), $"%{searchQuery}%")) ||
                    (p.Rank != null && p.Rank.RankName != null && EF.Functions.Like(p.Rank.RankName.ToLower(), $"%{searchQuery}%"))
                );
            }

            // Fetch the personnel data from the database
            var result = await personnelList.ToListAsync();

            // Check if no data was found
            if (result == null || !result.Any())
            {
                TempData["Error"] = "No personnel found.";
            }

            // Pass the result to the view
            return View(result);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
