using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdmissionManagement.Data;
using AdmissionsManagement.Models;

namespace AdmissionManagement.Controllers
{
    public class CadidatesController : Controller
    {
        private readonly AdmissionManagement2Context _context;

        public CadidatesController(AdmissionManagement2Context context)
        {
            _context = context;
        }

        // GET: Cadidates
        public async Task<IActionResult> Index()
        {
            return _context.Cadidate != null ?
                        View(await _context.Cadidate.Where(c => c.CadidateStatus == 1).ToListAsync()) :
                          Problem("Entity set 'AdmissionManagement2Context.Cadidate'  is null.");
        }



        public async Task<IActionResult> Approved(int? id)
        {
            var cadidate = await _context.Cadidate.FindAsync(id);
            cadidate.CadidateStatus = 2;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> Declined(int? id)
        {
            var cadidate = await _context.Cadidate.FindAsync(id);
            cadidate.CadidateStatus = 3;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Cadidates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cadidate == null)
            {
                return NotFound();
            }

            var cadidate = await _context.Cadidate
                .FirstOrDefaultAsync(m => m.CadidateId == id);
            if (cadidate == null)
            {
                return NotFound();
            }

            return View(cadidate);
        }

        // GET: Cadidates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cadidates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CadidateId,FullName,PlaceOfBirth,DateOfBirth,CurrentAddress,DateOfGradution,CadidateType,PriorityArea,PhoneNumber,Gender,SubjectCombination,PaymentMethod,PaymentStatus,CadidateStatus,Email,UserId")] Cadidate cadidate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cadidate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cadidate);
        }

        // GET: Cadidates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cadidate == null)
            {
                return NotFound();
            }

            var cadidate = await _context.Cadidate.FindAsync(id);
            if (cadidate == null)
            {
                return NotFound();
            }
            return View(cadidate);
        }

        // POST: Cadidates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CadidateId,FullName,PlaceOfBirth,DateOfBirth,CurrentAddress,DateOfGradution,CadidateType,PriorityArea,PhoneNumber,Gender,SubjectCombination,PaymentMethod,PaymentStatus,CadidateStatus,Email,UserId")] Cadidate cadidate)
        {
            if (id != cadidate.CadidateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cadidate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CadidateExists(cadidate.CadidateId))
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
            return View(cadidate);
        }

        // GET: Cadidates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cadidate == null)
            {
                return NotFound();
            }

            var cadidate = await _context.Cadidate
                .FirstOrDefaultAsync(m => m.CadidateId == id);
            if (cadidate == null)
            {
                return NotFound();
            }

            return View(cadidate);
        }

        // POST: Cadidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cadidate == null)
            {
                return Problem("Entity set 'AdmissionManagement2Context.Cadidate'  is null.");
            }
            var cadidate = await _context.Cadidate.FindAsync(id);
            if (cadidate != null)
            {
                _context.Cadidate.Remove(cadidate);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CadidateExists(int id)
        {
          return (_context.Cadidate?.Any(e => e.CadidateId == id)).GetValueOrDefault();
        }
    }
}
