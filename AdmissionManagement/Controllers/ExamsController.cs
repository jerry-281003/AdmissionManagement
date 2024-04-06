using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdmissionManagement.Data;
using AdmissionsManagement.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Text.RegularExpressions;
using iText.Layout.Element;

namespace AdmissionManagement.Controllers
{
    public class ExamsController : Controller
    {
        private readonly AdmissionManagement2Context _context;

        public ExamsController(AdmissionManagement2Context context)
        {
            _context = context;
        }

		// GET: Exams
		public async Task<IActionResult> Index()
		{
			return _context.Exam != null ?
						View(await _context.Exam.ToListAsync()) :
						Problem("Entity set 'ExamManagementContext.Exam'  is null.");
		}

		// GET: Exams/Details/5
		
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Exam == null)
			{
				return NotFound();
			}

			var exam = await _context.Exam
				.FirstOrDefaultAsync(m => m.ExamID == id);
			if (exam == null)
			{
				return NotFound();
			}

			return View(exam);
		}
	
		// GET: Exams/Create
		public IActionResult Create()
		{
			ViewData["CourseName"] = new SelectList(_context.Course, "CourseName", "CourseName");
			return View();
		}

		// POST: Exams/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ExamID,CourseName,Date,ExamName,Duration")] Exam exam)
		{
			if (ModelState.IsValid)
			{
				_context.Add(exam);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(exam);
		}
		
		public async Task<IActionResult> CreateExamQuestions(int? id)
		{
			ViewData["CourseName"] = new SelectList(_context.Course, "CourseName", "CourseName");
			if (id == null || _context.Exam == null)
			{
				return NotFound();
			}

			var exam = await _context.Exam.FindAsync(id);
			if (exam == null)
			{
				return NotFound();
			}
			var viewModel = new CreateExamQuestionsViewModel();

			return View(viewModel);
		}
	
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateExamQuestions(int id, CreateExamQuestionsViewModel viewModel)
		{
			if (id == null || _context.Exam == null)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				// Retrieve course ID based on the selected CourseName
				var courseId = _context.Course.FirstOrDefault(c => c.CourseName == viewModel.CourseName)?.CourseID;

				if (courseId != null)
				{
					// Retrieve questions related to the selected CourseName
					var questions = _context.Question.Where(q => q.CourseID == courseId).ToList();

					// Shuffle the questions to randomize selection
					questions = questions.OrderBy(q => Guid.NewGuid()).ToList();

					// Take the specified number of questions
					if (questions.Count < viewModel.NumberOfQuestions)
					{
						ModelState.AddModelError(string.Empty, "There are not enough questions available for this course.");
						return View();
					}
					var selectedQuestions = questions.Take(viewModel.NumberOfQuestions).ToList();

					// Create ExamQuestion instances for each selected question
					foreach (var question in selectedQuestions)
					{
						var examQuestion = new ExamQuestion
						{
							ExamID = id,
							QuestionID = question.QuestionID
						};
						_context.ExamQuestion.Add(examQuestion);
					}

					await _context.SaveChangesAsync();

					return RedirectToAction(nameof(Index));
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Course not found.");
				}
			}

			// If ModelState is not valid, redisplay the form with errors
			return View(viewModel);
		}
		
		public IActionResult ExportToPdf(int id)
		{
			var examName = _context.Exam
				.Where(e => e.ExamID == id)
				.Select(e => e.ExamName)
				.FirstOrDefault();
			var questionModels = _context.ExamQuestion
				.Where(eq => eq.ExamID == id)
				.Select(eq => new QuestionModel
				{
					QuestionId = eq.QuestionID,
					QuestionText = _context.Question
						.Where(q => q.QuestionID == eq.QuestionID)
						.Select(q => q.QuestionText)
						.FirstOrDefault(),
					Options = _context.Option
						.Where(o => o.QuestionID == eq.QuestionID)
						.ToList()
				})
				.ToList();

			var sanitizedExamName = RemoveSpecialCharacters(examName);
			var pdfFileName = $"{sanitizedExamName}.pdf";
			// Create a new PDF document
			var pdfPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), pdfFileName);
			var pdfDoc = new PdfDocument(new PdfWriter(pdfPath));

			// Create a document to add content
			var document = new Document(pdfDoc);
			var questionNumber = 1;


			// Add content to the PDF document based on your questionModels
			foreach (var question in questionModels)
			{
				// Add question number
				document.Add(new Paragraph($"Question {questionNumber}: {question.QuestionText}"));

				// Counter for option letter
				var optionLetter = 'a';

				foreach (var option in question.Options)
				{
					// Add option with letter
					document.Add(new Paragraph($"{optionLetter++}. {option.OptionText}"));
				}

				// Increment question number
				questionNumber++;

				// Add a new line after each question
				document.Add(new Paragraph("\n"));
			}

			// Close the document
			document.Close();

			// Return the file as a file download response
			return RedirectToAction("Index");



		}
		private string RemoveSpecialCharacters(string str)
		{
			return Regex.Replace(str, "[^a-zA-Z0-9]+", "", RegexOptions.Compiled);
		}


		[Authorize(Roles = "Admin")]
		// GET: Exams/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Exam == null)
			{
				return NotFound();
			}

			var exam = await _context.Exam.FindAsync(id);
			if (exam == null)
			{
				return NotFound();
			}
			return View(exam);
		}

		// POST: Exams/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(int id, [Bind("ExamID,CourseName,Date,ExamName,Duration")] Exam exam)
		{
			if (id != exam.ExamID)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(exam);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ExamExists(exam.ExamID))
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
			return View(exam);
		}

		// GET: Exams/Delete/5
		
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Exam == null)
			{
				return NotFound();
			}

			var exam = await _context.Exam
				.FirstOrDefaultAsync(m => m.ExamID == id);
			if (exam == null)
			{
				return NotFound();
			}

			return View(exam);
		}

		// POST: Exams/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Exam == null)
			{
				return Problem("Entity set 'ExamManagementContext.Exam'  is null.");
			}
			var exam = await _context.Exam.FindAsync(id);
			if (exam != null)
			{
				_context.Exam.Remove(exam);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ExamExists(int id)
		{
			return (_context.Exam?.Any(e => e.ExamID == id)).GetValueOrDefault();
		}
	}
}
