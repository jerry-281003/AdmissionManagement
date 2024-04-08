using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdmissionManagement.Data;
using AdmissionManagement.Models;
using AdmissionsManagement.Models;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AdmissionManagement.Controllers
{
    public class LocationCadidatesController : Controller
    {
        private readonly AdmissionManagement2Context _context;

        public LocationCadidatesController(AdmissionManagement2Context context)
        {
            _context = context;
        }

        // GET: LocationCadidates
        public async Task<IActionResult> Index()
        {
              return _context.LocationCadidate != null ? 
                          View(await _context.LocationCadidate.ToListAsync()) :
                          Problem("Entity set 'AdmissionManagement2Context.LocationCadidate'  is null.");
        }

        // GET: LocationCadidates/Details/5


        public IActionResult Create(string Id)
        {
            if (Id != null)
            {
                var locations = _context.Location.Where(l => l.Quality < l.Capcity).ToList();
                var cadidate = _context.Cadidate.Where(c => c.UserId == Id).FirstOrDefault();
                var locationcadidates = new LocationCadidateViewModel();
                locationcadidates.Cadidate = cadidate;

                var subjectcombination = _context.SubjectCombination.Where(s => s.Id == cadidate.SubjectCombination).FirstOrDefault();
                foreach (var location in locations)
                {
                    if (subjectcombination.Subject1 == location.Subject || subjectcombination.Subject2 == location.Subject || subjectcombination.Subject3 == location.Subject)
                    {
                        
                        locationcadidates.Location.Add(location);
                        var locationcadidate = new LocationCadidate();
                        locationcadidate.CadidateId = Id;
                        locationcadidate.LocationId = location.LocationId;
                        location.Quality = location.Quality+1;

                        _context.LocationCadidate.Add(locationcadidate);
                        _context.SaveChanges(); 
                    }
                }
                var user = _context.ApplicationUser.Where(a => a.Email == Id).FirstOrDefault();
                Random rand = new Random();
                int randomNumber = rand.Next(10000000, 99999999);
                user.CadidateExamId = randomNumber.ToString();
                _context.ApplicationUser.Update(user);
                _context.SaveChanges();
                
            }
            else
            {
                return NotFound();
            }
            return RedirectToAction("Index", "Home"); // Redirect to the Index action of Home controller
        }




        private bool LocationCadidateExists(int id)
        {
          return (_context.LocationCadidate?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
