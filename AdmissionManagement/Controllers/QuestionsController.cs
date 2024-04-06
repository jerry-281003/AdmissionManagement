using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdmissionManagement.Data;
using AdmissionsManagement.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace AdmissionManagement.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly AdmissionManagement2Context _context;

        public QuestionsController(AdmissionManagement2Context context)
        {
            _context = context;
        }

		// GET: Questions
		public async Task<IActionResult> Index()
		{
			return _context.Question != null ?
						View(await _context.Question.ToListAsync()) :
						Problem("Entity set 'ExamManagementContext.Question'  is null.");
		}
		
		// GET: Questions/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			try
			{
				var question = await _context.Question.FindAsync(id);
				if (question == null)
				{
					return NotFound(); // Handle case when question is not found
				}

				var options = await _context.Option
					.Where(o => o.QuestionID == id)
					.ToListAsync();

				var correctAnswerId = await _context.CorrectAnswer
					.Where(ca => ca.QuestionID == id)
					.Select(ca => ca.OptionID)
					.FirstOrDefaultAsync();

				var questionViewModel = new QuestionViewModel
				{
					QuestionId = question.QuestionID,
					QuestionText = question.QuestionText,
					Options = options,
					CorrectAnswerId = correctAnswerId
				};

				return View(questionViewModel);

			}
			catch (Exception ex)
			{
				// Log thehe exception or handle it appropriately
				return StatusCode(500, "An error occurred while processing the request.");
			}
		}
		
		// GET: Questions/Create
		public IActionResult Create()
		{
			ViewData["CourseID"] = new SelectList(_context.Course, "CourseID", "CourseID");
			return View();
		}

		// POST: Questions/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		
		public async Task<IActionResult> Create(Question question, List<Option> options, int correctOption)
		{
			if (ModelState.IsValid)
			{

				_context.Add(question);
				await _context.SaveChangesAsync();

				foreach (var option in options)
				{
					option.QuestionID = question.QuestionID;
					_context.Add(option);
					await _context.SaveChangesAsync();
				}

				var correctAnswer = new CorrectAnswer
				{
					QuestionID = question.QuestionID,
					OptionID = options[correctOption].OptionID
				};
				_context.Add(correctAnswer);
				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}
			return View(question);
		}

		
		// GET: Questions/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var question = await _context.Question.FindAsync(id);

			if (question == null)
			{
				return NotFound();
			}

			return View(question);
		}

		// POST: Questions/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		
		public async Task<IActionResult> Edit(int id, Question question)
		{
			if (id != question.QuestionID)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(question);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!QuestionExists(question.QuestionID))
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
			return View(question);
		}


		// GET: Questions/Delete/5
		
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Question == null)
			{
				return NotFound();
			}

			var question = await _context.Question
				.FirstOrDefaultAsync(m => m.QuestionID == id);
			if (question == null)
			{
				return NotFound();
			}

			return View(question);
		}

		// POST: Questions/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Question == null)
			{
				return Problem("Entity set 'ExamManagementContext.Question'  is null.");
			}
			var question = await _context.Question.FindAsync(id);
			if (question != null)
			{
				_context.Question.Remove(question);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool QuestionExists(int id)
		{
			return (_context.Question?.Any(e => e.QuestionID == id)).GetValueOrDefault();
		}
	}
}
