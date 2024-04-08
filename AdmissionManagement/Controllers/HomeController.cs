using AdmissionManagement.Data;
using AdmissionManagement.Models;
using AdmissionsManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AdmissionManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AdmissionManagement2Context _context;

       
        public HomeController(ILogger<HomeController> logger, AdmissionManagement2Context context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> MyAdmissionForm()
        {
            return _context.Cadidate != null ?
                        View(await _context.Cadidate.Where(c => c.UserId == User.Identity.Name).ToListAsync()) :
                          Problem("Entity set 'AdmissionManagement2Context.Cadidate'  is null.");
        }
        public IActionResult AdmissionRegistrationForm()
        {
            ViewData["Id"] = new SelectList(_context.SubjectCombination, "Id", "Id");
            return View();
        }
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
        public async Task<IActionResult> AdmissionDetails(int? id)
        {
           
            if (id == null || _context.Cadidate == null)
            {
                return NotFound();
            }

            ViewData["Id"] = new SelectList(_context.SubjectCombination, "Id", "Id");
            var cadidate = await _context.Cadidate.FindAsync(id);

            if (cadidate == null)
            {
                return NotFound();
            }
            return View(cadidate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdmissionDetails(int id, [Bind("CadidateId,FullName,PlaceOfBirth,DateOfBirth,CurrentAddress,DateOfGradution,CadidateType,PriorityArea,PhoneNumber,Gender,SubjectCombination,PaymentMethod,PaymentStatus,CadidateStatus,Email,UserId")] Cadidate cadidate)
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
                return RedirectToAction(nameof(MyAdmissionForm));
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
      
        

        private bool CadidateExists(int id)
        {
            return (_context.Cadidate?.Any(e => e.CadidateId == id)).GetValueOrDefault();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}