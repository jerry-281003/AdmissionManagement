using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdmissionManagement.Data;
using AdmissionManagement.Models;

namespace AdmissionManagement.Controllers
{
    public class SubjectCombinationsController : Controller
    {
        private readonly AdmissionManagement2Context _context;

        public SubjectCombinationsController(AdmissionManagement2Context context)
        {
            _context = context;
        }

        // GET: SubjectCombinations
        public async Task<IActionResult> Index()
        {
              return _context.SubjectCombination != null ? 
                          View(await _context.SubjectCombination.ToListAsync()) :
                          Problem("Entity set 'AdmissionManagement2Context.SubjectCombination'  is null.");
        }

        // GET: SubjectCombinations/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.SubjectCombination == null)
            {
                return NotFound();
            }

            var subjectCombination = await _context.SubjectCombination
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subjectCombination == null)
            {
                return NotFound();
            }

            return View(subjectCombination);
        }

        // GET: SubjectCombinations/Create
        public IActionResult Create()
        {
            ViewData["CourseName"] = new SelectList(_context.Course, "CourseName", "CourseName");
            return View();
        }

        // POST: SubjectCombinations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Subject1,Subject2,Subject3")] SubjectCombination subjectCombination)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subjectCombination);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subjectCombination);
        }

        // GET: SubjectCombinations/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.SubjectCombination == null)
            {
                return NotFound();
            }

            var subjectCombination = await _context.SubjectCombination.FindAsync(id);
            if (subjectCombination == null)
            {
                return NotFound();
            }
            return View(subjectCombination);
        }

        // POST: SubjectCombinations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Subject1,Subject2,Subject3")] SubjectCombination subjectCombination)
        {
            if (id != subjectCombination.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subjectCombination);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectCombinationExists(subjectCombination.Id))
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
            return View(subjectCombination);
        }

        // GET: SubjectCombinations/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.SubjectCombination == null)
            {
                return NotFound();
            }

            var subjectCombination = await _context.SubjectCombination
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subjectCombination == null)
            {
                return NotFound();
            }

            return View(subjectCombination);
        }

        // POST: SubjectCombinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.SubjectCombination == null)
            {
                return Problem("Entity set 'AdmissionManagement2Context.SubjectCombination'  is null.");
            }
            var subjectCombination = await _context.SubjectCombination.FindAsync(id);
            if (subjectCombination != null)
            {
                _context.SubjectCombination.Remove(subjectCombination);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectCombinationExists(string id)
        {
          return (_context.SubjectCombination?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
