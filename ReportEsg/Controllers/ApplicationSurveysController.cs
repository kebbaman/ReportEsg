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
    public class ApplicationSurveysController : Controller
    {
        private readonly DatabaseContext _context;
        public ApplicationSurveysController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: ApplicationSurveys
        public async Task<IActionResult> Index(int? applicationId)
        {
            if (applicationId == null)
            {
                return NotFound();
            }
            ViewBag.AppId = applicationId;
            var databaseContext = _context.ApplicationSurveys.Include(a => a.Application).Include(a => a.Theme).Where(s => s.ApplicationId == applicationId);
            return View(await databaseContext.ToListAsync());
        }

        // GET: ApplicationSurveys/Create
        public IActionResult Create(int? applicationId)
        {
            if (applicationId == null)
            {
                return NotFound();
            }
            ViewData["ApplicationId"] = applicationId;
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Description");
            ViewBag.AppId = applicationId;
            return View();
        }

        // POST: ApplicationSurveys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicationId,ThemeId,Id,Title")] ApplicationSurvey applicationSurvey,int? applicationId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationSurvey);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { applicationId = applicationId });
            }
            ViewData["ApplicationId"] = applicationId;
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Description", applicationSurvey.ThemeId);
            return View(applicationSurvey);
        }

        // GET: ApplicationSurveys/Edit/5
        public async Task<IActionResult> Edit(int? id, int? applicationId)
        {
            if (id == null || applicationId == null)
            {
                return NotFound();
            }

            var applicationSurvey = await _context.ApplicationSurveys.FindAsync(id);
            if (applicationSurvey == null)
            {
                return NotFound();
            }
            ViewData["ApplicationId"] = applicationId;
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Description", applicationSurvey.ThemeId);
            //ViewBag.AppId = this.applicationId;
            return View(applicationSurvey);
        }

        // POST: ApplicationSurveys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicationId,ThemeId,Id,Title")] ApplicationSurvey applicationSurvey, int? applicationId)
        {
            if (id != applicationSurvey.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationSurvey);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationSurveyExists(applicationSurvey.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { applicationId = applicationId });
            }
            ViewData["ApplicationId"] = applicationId;
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Description", applicationSurvey.ThemeId);
            //ViewBag.AppId = this.applicationId;
            return View(applicationSurvey);
        }

        // GET: ApplicationSurveys/Delete/5
        public async Task<IActionResult> Delete(int? id, int? applicationId)
        {
            if (id == null || applicationId == null)
            {
                return NotFound();
            }

            var applicationSurvey = await _context.ApplicationSurveys
                .Include(a => a.Application)
                .Include(a => a.Theme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationSurvey == null)
            {
                return NotFound();
            }
            ViewData["ApplicationId"] = applicationId;
            return View(applicationSurvey);
        }

        // POST: ApplicationSurveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? applicationId)
        {
            var applicationSurvey = await _context.ApplicationSurveys.FindAsync(id);
            _context.ApplicationSurveys.Remove(applicationSurvey);
            await _context.SaveChangesAsync();
            ViewData["ApplicationId"] = applicationId;
            return RedirectToAction(nameof(Index), new { applicationId = applicationId });
        }

        private bool ApplicationSurveyExists(int id)
        {
            return _context.ApplicationSurveys.Any(e => e.Id == id);
        }
    }
}
