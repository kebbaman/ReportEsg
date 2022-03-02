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
    public class ChoicesController : Controller
    {
        private readonly DatabaseContext _context;

        public ChoicesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Choices
        public async Task<IActionResult> Index(int? questionId, int? surveyId)
        {
            var databaseContext = _context.Choices.Include(c => c.ApplicationSurveyQuestion).Where(c=>c.ApplicationSurveyQuestionId == questionId);
            ViewBag.QuestionId = questionId;
            ViewBag.SurveyId = surveyId;
            return View(await databaseContext.ToListAsync());
        }

        // GET: Choices/Create
        public IActionResult Create(int? questionId, int? surveyId)
        {
            ViewData["ApplicationSurveyQuestionId"] = new SelectList(_context.ApplicationSurveyQuestions, "Id", "Title");
            ViewBag.QuestionId = questionId;
            ViewBag.SurveyId = surveyId;
            return View();
        }

        // POST: Choices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Score,ApplicationSurveyQuestionId")] Choice choice, int? questionId, int? surveyId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(choice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { questionId = questionId, surveyId = surveyId });
            }
            ViewData["ApplicationSurveyQuestionId"] = new SelectList(_context.ApplicationSurveyQuestions, "Id", "Discriminator", choice.ApplicationSurveyQuestionId);
            ViewBag.QuestionId = questionId;
            ViewBag.SurveyId = surveyId;
            return View(choice);
        }

        // GET: Choices/Edit/5
        public async Task<IActionResult> Edit(int? id, int? questionId, int? surveyId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var choice = await _context.Choices.FindAsync(id);
            if (choice == null)
            {
                return NotFound();
            }
            ViewData["ApplicationSurveyQuestionId"] = new SelectList(_context.ApplicationSurveyQuestions, "Id", "Discriminator", choice.ApplicationSurveyQuestionId);
            ViewBag.QuestionId = questionId;
            ViewBag.SurveyId = surveyId;
            return View(choice);
        }

        // POST: Choices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Score,ApplicationSurveyQuestionId")] Choice choice, int? questionId, int? surveyId)
        {
            if (id != choice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(choice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChoiceExists(choice.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { questionId = questionId, surveyId = surveyId });
            }
            ViewData["ApplicationSurveyQuestionId"] = new SelectList(_context.ApplicationSurveyQuestions, "Id", "Discriminator", choice.ApplicationSurveyQuestionId);
            ViewBag.QuestionId = questionId;
            ViewBag.SurveyId = surveyId;
            return View(choice);
        }

        // GET: Choices/Delete/5
        public async Task<IActionResult> Delete(int? id, int? questionId, int? surveyId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var choice = await _context.Choices
                .Include(c => c.ApplicationSurveyQuestion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (choice == null)
            {
                return NotFound();
            }
            ViewBag.QuestionId = questionId;
            ViewBag.SurveyId = surveyId;
            return View(choice);
        }

        // POST: Choices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? questionId, int? surveyId)
        {
            var choice = await _context.Choices.FindAsync(id);
            _context.Choices.Remove(choice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { questionId = questionId, surveyId = surveyId });
        }

        private bool ChoiceExists(int id)
        {
            return _context.Choices.Any(e => e.Id == id);
        }
    }
}
