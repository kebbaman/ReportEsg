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
    public class OrganizationDetailsSurveyQuestionsController : Controller
    {
        private readonly DatabaseContext _context;

        public OrganizationDetailsSurveyQuestionsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: OrganizationDetailsSurveyQuestions
        public async Task<IActionResult> Index(int? surveyId)
        {
            if (surveyId == null)
            {
                return NotFound();
            }
            var databaseContext = _context.OrganizationDetailsSurveyQuestions.Include(o => o.OrganizationDetailsSurvey).Where(s => s.OrganizationDetailsSurveyId == surveyId);
            ViewBag.SurveyId = surveyId;
            return View(await databaseContext.ToListAsync());
        }

        // GET: OrganizationDetailsSurveyQuestions/Details/5
        public async Task<IActionResult> Details(int? id, int? surveyId)
        {
            if (id == null || surveyId == null)
            {
                return NotFound();
            }

            var organizationDetailsSurveyQuestion = await _context.OrganizationDetailsSurveyQuestions
                .Include(o => o.OrganizationDetailsSurvey)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organizationDetailsSurveyQuestion == null)
            {
                return NotFound();
            }
            ViewBag.SurveyId = surveyId;
            return View(organizationDetailsSurveyQuestion);
        }

        // GET: OrganizationDetailsSurveyQuestions/Create
        public IActionResult Create(int? surveyId)
        {
            ViewData["OrganizationDetailsSurveyId"] = new SelectList(_context.OrganizationDetailsSurveys, "Id", "Title");
            ViewBag.SurveyId = surveyId;
            return View();
        }

        // POST: OrganizationDetailsSurveyQuestions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrganizationDetailsSurveyId,Id,Name,Title,Type,IsRequired")] OrganizationDetailsSurveyQuestion organizationDetailsSurveyQuestion, int? surveyId)
        {
            if (ModelState.IsValid)
            {
                //organizationDetailsSurveyQuestion.
                _context.Add(organizationDetailsSurveyQuestion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { surveyId = surveyId });
            }
            ViewData["OrganizationDetailsSurveyId"] = new SelectList(_context.OrganizationDetailsSurveys, "Id", "Title", organizationDetailsSurveyQuestion.OrganizationDetailsSurveyId);
            ViewBag.SurveyId = surveyId;
            return View(organizationDetailsSurveyQuestion);
        }

        // GET: OrganizationDetailsSurveyQuestions/CreateTextQuestion
        public IActionResult CreateTextQuestion(int? surveyId)
        {
            if (surveyId == null)
            {
                return NotFound();
            }
            ViewBag.SurveyId = surveyId;
            ViewBag.Name = Guid.NewGuid().ToString();
            ViewBag.OrganizationDetailsSurveyId = surveyId;
            return View();
        }

        // POST: OrganizationDetailsSurveyQuestions/CreateTextQuestion
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTextQuestion([Bind("OrganizationDetailsSurveyId,Id,Name,Title,Type,IsRequired")] OrganizationDetailsSurveyQuestion organizationDetailsSurveyQuestion, int? surveyId)
        {
            if (ModelState.IsValid)
            {
                organizationDetailsSurveyQuestion.OrganizationDetailsSurveyId = (int) surveyId;
                organizationDetailsSurveyQuestion.Type = "text";
                _context.Add(organizationDetailsSurveyQuestion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { surveyId = surveyId });
            }
            ViewBag.OrganizationDetailsSurveyId = surveyId;
            ViewBag.SurveyId = surveyId;
            return View(organizationDetailsSurveyQuestion);
        }

        // GET: OrganizationDetailsSurveyQuestions/Edit/5
        public async Task<IActionResult> Edit(int? id, int? surveyId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizationDetailsSurveyQuestion = await _context.OrganizationDetailsSurveyQuestions.FindAsync(id);
            if (organizationDetailsSurveyQuestion == null)
            {
                return NotFound();
            }
            ViewBag.OrganizationDetailsSurveyId = surveyId;
            ViewBag.SurveyId = surveyId;
            return View(organizationDetailsSurveyQuestion);
        }

        // POST: OrganizationDetailsSurveyQuestions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrganizationDetailsSurveyId,Id,Name,Title,Type,IsRequired")] OrganizationDetailsSurveyQuestion organizationDetailsSurveyQuestion, int? surveyId)
        {
            if (id != organizationDetailsSurveyQuestion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organizationDetailsSurveyQuestion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganizationDetailsSurveyQuestionExists(organizationDetailsSurveyQuestion.Id))
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
            ViewBag.OrganizationDetailsSurveyId = surveyId;
            ViewBag.SurveyId = surveyId;
            return View(organizationDetailsSurveyQuestion);
        }

        // GET: OrganizationDetailsSurveyQuestions/Delete/5
        public async Task<IActionResult> Delete(int? id, int? surveyId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizationDetailsSurveyQuestion = await _context.OrganizationDetailsSurveyQuestions
                .Include(o => o.OrganizationDetailsSurvey)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organizationDetailsSurveyQuestion == null)
            {
                return NotFound();
            }
            ViewBag.SurveyId = surveyId;
            return View(organizationDetailsSurveyQuestion);
        }

        // POST: OrganizationDetailsSurveyQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? surveyId)
        {
            var organizationDetailsSurveyQuestion = await _context.OrganizationDetailsSurveyQuestions.FindAsync(id);
            _context.OrganizationDetailsSurveyQuestions.Remove(organizationDetailsSurveyQuestion);
            await _context.SaveChangesAsync();
            ViewBag.SurveyId = surveyId;
            return RedirectToAction(nameof(Index), new { surveyId = surveyId });
        }

        private bool OrganizationDetailsSurveyQuestionExists(int id)
        {
            return _context.OrganizationDetailsSurveyQuestions.Any(e => e.Id == id);
        }
    }
}
