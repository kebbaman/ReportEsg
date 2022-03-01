using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReportEsg.Data;
using ReportEsg.Models;

namespace ReportEsg.Controllers
{
    public class ApplicationSurveyQuestionsController : Controller
    {
        private readonly DatabaseContext _context;

        public ApplicationSurveyQuestionsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: ApplicationSurveyQuestions
        public async Task<IActionResult> Index(int? surveyId)
        {
            if (surveyId == null)
            {
                return NotFound();
            }
            var applicationSurvey = await _context.ApplicationSurveys.FindAsync(surveyId);
            ViewBag.ApplicationId = applicationSurvey.ApplicationId;
            ViewBag.SurveyId = surveyId;
            return View(await _context.ApplicationSurveyQuestions.Where(q => q.ApplicationSurveyId == surveyId).ToListAsync());
        }

        // GET: ApplicationSurveyQuestions/Details/5
        public async Task<IActionResult> Details(int? id, int? surveyId)
        {
            if (id == null || surveyId == null)
            {
                return NotFound();
            }
            var applicationSurveyQuestion = await _context.ApplicationSurveyQuestions.FirstOrDefaultAsync(m => m.Id == id);
            if (applicationSurveyQuestion == null)
            {
                return NotFound();
            }
            ViewBag.SurveyId = surveyId;
            return View(applicationSurveyQuestion);
        }

        // GET: ApplicationSurveyQuestions/Create
        public IActionResult Create(int? surveyId)
        {
            if (surveyId == null)
            {
                return NotFound();
            }
            ViewBag.SurveyId = surveyId;
            return View();
        }

        // POST: ApplicationSurveyQuestions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Title,Type,IsRequired")] ApplicationSurveyQuestion applicationSurveyQuestion, int surveyId)
        {
            if (ModelState.IsValid)
            {
                applicationSurveyQuestion.ApplicationSurveyId = surveyId;
                _context.Add(applicationSurveyQuestion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),new { surveyId = surveyId});
            }
            ViewBag.SurveyId = surveyId;
            return View(applicationSurveyQuestion);
        }

        // GET: ApplicationSurveyQuestions/Create
        public IActionResult CreateTextQuestion(int? surveyId)
        {
            if (surveyId == null)
            {
                return NotFound();
            }
            ViewBag.SurveyId = surveyId;
            ViewBag.Name = Guid.NewGuid().ToString();
            return View();
        }

        // POST: ApplicationSurveyQuestions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTextQuestion([Bind("Id,Name,Title,IsRequired,ApplicationSurveyId,Type")] TextApplicationSurveyQuestion applicationSurveyQuestion, int surveyId)
        {
            if (ModelState.IsValid)
            {
                applicationSurveyQuestion.ApplicationSurveyId = surveyId;
                applicationSurveyQuestion.Type = "text";
                _context.Add(applicationSurveyQuestion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { surveyId = surveyId });
            }
            ViewBag.SurveyId = surveyId;
            return View(applicationSurveyQuestion);
        }

        // GET: ApplicationSurveyQuestions/Create
        public IActionResult CreateBooleanQuestion(int? surveyId)
        {
            if (surveyId == null)
            {
                return NotFound();
            }
            ViewBag.SurveyId = surveyId;
            ViewBag.Name = Guid.NewGuid().ToString();
            return View();
        }

        // POST: ApplicationSurveyQuestions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBooleanQuestion([Bind("Id,Name,Title,IsRequired,ApplicationSurveyId,Type,Score")] BooleanApplicationSurveyQuestion applicationSurveyQuestion, int surveyId)
        {
            if (ModelState.IsValid)
            {
                applicationSurveyQuestion.ApplicationSurveyId = surveyId;
                applicationSurveyQuestion.Type = "boolean";
                _context.Add(applicationSurveyQuestion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { surveyId = surveyId });
            }
            ViewBag.SurveyId = surveyId;
            return View(applicationSurveyQuestion);
        }





        // GET: ApplicationSurveyQuestions/Create
        public IActionResult CreateRadioQuestion(int? surveyId)
        {
            if (surveyId == null)
            {
                return NotFound();
            }
            ViewBag.SurveyId = surveyId;
            ViewBag.Name = Guid.NewGuid().ToString();
            return View();
        }

        // POST: ApplicationSurveyQuestions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRadioQuestion([Bind("Id,Name,Title,IsRequired,ApplicationSurveyId,Type,Score")] BooleanApplicationSurveyQuestion applicationSurveyQuestion, int surveyId)
        {
            if (ModelState.IsValid)
            {
                applicationSurveyQuestion.ApplicationSurveyId = surveyId;
                applicationSurveyQuestion.Type = "radiogroup";
                _context.Add(applicationSurveyQuestion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { surveyId = surveyId });
            }
            ViewBag.SurveyId = surveyId;
            return View(applicationSurveyQuestion);
        }















        // GET: ApplicationSurveyQuestions/Edit/5
        public async Task<IActionResult> Edit(int? id, int? surveyId)
        {
            if (id == null || surveyId == null)
            {
                return NotFound();
            }

            var applicationSurveyQuestion = await _context.ApplicationSurveyQuestions.FindAsync(id);
            if (applicationSurveyQuestion == null)
            {
                return NotFound();
            }
            ViewBag.SurveyId = surveyId;
            return View(applicationSurveyQuestion);
        }

        // POST: ApplicationSurveyQuestions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Title,Type,IsRequired")] ApplicationSurveyQuestion applicationSurveyQuestion, int? surveyId)
        {
            if (id != applicationSurveyQuestion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationSurveyQuestion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationSurveyQuestionExists(applicationSurveyQuestion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { surveyId = surveyId });
            }
            ViewBag.SurveyId = surveyId;
            return View(applicationSurveyQuestion);
        }

        // GET: ApplicationSurveyQuestions/Delete/5
        public async Task<IActionResult> Delete(int? id, int? surveyId)
        {
            if (id == null || surveyId == null)
            {
                return NotFound();
            }

            var applicationSurveyQuestion = await _context.ApplicationSurveyQuestions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationSurveyQuestion == null)
            {
                return NotFound();
            }
            ViewBag.SurveyId = surveyId;
            return View(applicationSurveyQuestion);
        }

        // POST: ApplicationSurveyQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? surveyId)
        {
            var applicationSurveyQuestion = await _context.ApplicationSurveyQuestions.FindAsync(id);
            _context.ApplicationSurveyQuestions.Remove(applicationSurveyQuestion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { surveyId = surveyId });
        }

        private bool ApplicationSurveyQuestionExists(int id)
        {
            return _context.ApplicationSurveyQuestions.Any(e => e.Id == id);
        }
    }
}
