using AdmissionManagement.Data;
using AdmissionManagement.Data;
using AdmissionManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ExamManagement.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AdmissionManagement2Context _context;


        public AdminController(UserManager<IdentityUser> userManager, AdmissionManagement2Context context)
        {
            _userManager = userManager;
            _context = context;

        }
      
        public IActionResult Index()
        {
            return View();
        }

        
        public async Task<IActionResult> Students()
        {
            var students = _context.ApplicationUser
                .ToList();

            return View(students);
        }
        public async Task<IActionResult> PaymentDetails()
        {
            List<PaymentDetails> paymentDetails = await _context.PaymentDetails.ToListAsync();
            return View(paymentDetails);
        }


    }
}
