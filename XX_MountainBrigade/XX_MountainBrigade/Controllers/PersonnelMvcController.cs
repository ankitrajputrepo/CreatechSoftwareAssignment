using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using XX_MountainBrigade.Models;

namespace XX_MountainBrigade.Controllers
{
    public class PersonnelMvcController : Controller
    {
        private readonly XXMBContext _context;

        public PersonnelMvcController(XXMBContext context)
        {
            _context = context;
        }

        // GET: Personnel/Create
        public IActionResult Create()
        {
            PopulateDropDowns();
            return View();
        }

        // POST: Personnel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Personnel personnel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.tblPersonnel.Add(personnel);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Personnel created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "An error occurred while creating the personnel.");
                }
            }

            PopulateDropDowns();
            return View(personnel);
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

        // POST: Personnel/Delete
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var personnel = await _context.tblPersonnel.FindAsync(id);
                if (personnel == null)
                {
                    TempData["Error"] = "Personnel not found.";
                    return RedirectToAction(nameof(Index));
                }

                _context.tblPersonnel.Remove(personnel);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Personnel deleted successfully!";
            }
            catch
            {
                TempData["Error"] = "An error occurred while deleting the personnel.";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Personnel/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var personnel = await _context.tblPersonnel
                .Include(p => p.Company)
                .Include(p => p.Rank)
                .FirstOrDefaultAsync(p => p.PersId == id);

            if (personnel == null)
            {
                return NotFound();
            }

            PopulateDropDowns();
            return View(personnel);
        }

        // POST: Personnel/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Personnel updatedPersonnel)
        {
            if (id != updatedPersonnel.PersId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(updatedPersonnel);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Personnel updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.tblPersonnel.Any(p => p.PersId == updatedPersonnel.PersId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                catch
                {
                    ModelState.AddModelError("", "An error occurred while updating the personnel.");
                }
            }

            PopulateDropDowns();
            return View(updatedPersonnel);
        }

        // Populating dropdowns with Companies, Ranks, and Personnel Types
        private void PopulateDropDowns()
        {
            ViewBag.Companies = _context.tblCompany.ToList();
            ViewBag.Ranks = _context.tblRanks.ToList();
            ViewBag.PersonnelTypes = new List<string> { "JCO", "OR" };
        }
    }
}
