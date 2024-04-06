using AdmissionManagement.Data;
using AdmissionManagement.Models;
using AdmissionsManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult AdmissionRegistrationForm()
        {
            ViewData["Id"] = new SelectList(_context.SubjectCombination, "Id", "Id");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CadidateId,FullName,PlaceOfBirth,DateOfBirth,CurrentAddress,DateOfGradution,CadidateType,PriorityArea,PhoneNumber,Gender,SubjectCombination,PaymentMethod,PaymentStatus,CadidateStatus,Email")] Cadidate cadidate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cadidate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cadidate);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}