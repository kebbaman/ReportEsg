using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ReportEsg.Data;
using ReportEsg.Models;

namespace ReportEsg.Controllers
{
    public class OrganizationDetailsSurveysController : Controller
    {
        private readonly DatabaseContext _context;

        public OrganizationDetailsSurveysController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: OrganizationDetailsSurveys
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.OrganizationDetailsSurveys.Include(o => o.OrganizationCategory);
            return View(await databaseContext.ToListAsync());
        }

        // GET: OrganizationDetailsSurveys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizationDetailsSurvey = await _context.OrganizationDetailsSurveys
                .Include(o => o.OrganizationCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organizationDetailsSurvey == null)
            {
                return NotFound();
            }

            return View(organizationDetailsSurvey);
        }

        // GET: OrganizationDetailsSurveys/Create
        public IActionResult Create()
        {
            ViewData["OrganizationCategoryId"] = new SelectList(_context.OrganizationCategories, "Id", "Description");
            return View();
        }

        // POST: OrganizationDetailsSurveys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrganizationCategoryId,Id,Title")] OrganizationDetailsSurvey organizationDetailsSurvey)
        {
            if (ModelState.IsValid)
            {
                _context.Add(organizationDetailsSurvey);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrganizationCategoryId"] = new SelectList(_context.OrganizationCategories, "Id", "Description", organizationDetailsSurvey.OrganizationCategoryId);
            return View(organizationDetailsSurvey);
        }

        // GET: OrganizationDetailsSurveys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizationDetailsSurvey = await _context.OrganizationDetailsSurveys.FindAsync(id);
            if (organizationDetailsSurvey == null)
            {
                return NotFound();
            }
            ViewData["OrganizationCategoryId"] = new SelectList(_context.OrganizationCategories, "Id", "Description", organizationDetailsSurvey.OrganizationCategoryId);
            return View(organizationDetailsSurvey);
        }

        // POST: OrganizationDetailsSurveys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrganizationCategoryId,Id,Title")] OrganizationDetailsSurvey organizationDetailsSurvey)
        {
            if (id != organizationDetailsSurvey.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organizationDetailsSurvey);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganizationDetailsSurveyExists(organizationDetailsSurvey.Id))
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
            ViewData["OrganizationCategoryId"] = new SelectList(_context.OrganizationCategories, "Id", "Description", organizationDetailsSurvey.OrganizationCategoryId);
            return View(organizationDetailsSurvey);
        }

        // GET: OrganizationDetailsSurveys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizationDetailsSurvey = await _context.OrganizationDetailsSurveys
                .Include(o => o.OrganizationCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organizationDetailsSurvey == null)
            {
                return NotFound();
            }

            return View(organizationDetailsSurvey);
        }

        // POST: OrganizationDetailsSurveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var organizationDetailsSurvey = await _context.OrganizationDetailsSurveys.FindAsync(id);
            _context.OrganizationDetailsSurveys.Remove(organizationDetailsSurvey);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrganizationDetailsSurveyExists(int id)
        {
            return _context.OrganizationDetailsSurveys.Any(e => e.Id == id);
        }

        
        //[Authorize(Roles = "Azienda")]
        // GET: PreAssessmentSurveys/Take
        public async Task<IActionResult> Take()
        {
            //Ottengo lo username dell'azienda loggata e ne carico l'entità
            string username = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Organization organization = await _context.Organizations.Include(c => c.OrganizationCategory).FirstOrDefaultAsync(c => c.Username == username);

            var organizationDetailsSurvey = await _context.OrganizationDetailsSurveys
                .Include(o => o.OrganizationCategory)
                .Include(s => s.OrganizationDetailsSurveyQuestions)
                .FirstOrDefaultAsync(s => s.OrganizationCategoryId == organization.OrganizationCategoryId);


            OrganizationDetailsSurveySession organizationDetailsSurveySession = new OrganizationDetailsSurveySession();
            organizationDetailsSurveySession.Organization = organization;
            organizationDetailsSurveySession.OrganizationDetailsSurvey = organizationDetailsSurvey;


            string survey = JsonConvert.SerializeObject(organizationDetailsSurvey, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            ViewBag.Survey = survey;
            return View();
        }
        
        // POST: PreAssessmentSurveys/Take
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Take(string surveyResult)
        {
            if (ModelState.IsValid)
            {
                //Ottengo lo username dell'azienda loggata e ne carico l'entità
                string username = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                Organization organization = await _context.Organizations.Include(c => c.OrganizationCategory).FirstOrDefaultAsync(c => c.Username == username);

                var organizationDetailsSurvey = await _context.OrganizationDetailsSurveys
                .Include(o => o.OrganizationCategory)
                .Include(s => s.OrganizationDetailsSurveyQuestions)
                .FirstOrDefaultAsync(s => s.OrganizationCategoryId == organization.OrganizationCategoryId);

                OrganizationDetailsSurveySession organizationDetailsSurveySession = new OrganizationDetailsSurveySession();
                organizationDetailsSurveySession.DateTime = DateTime.Now;
                organizationDetailsSurveySession.SurveyResult = surveyResult;
                organizationDetailsSurveySession.OrganizationDetailsSurveyId = organizationDetailsSurvey.Id;
                organizationDetailsSurveySession.Username = organization.Username;

                _context.Add(organizationDetailsSurveySession);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
                //return new OkResult();
            }
            return View();
        }


        /*
        public async Task<IActionResult> ViewResults(int? id)
        {
            if (id == null)
                return NotFound();

            var companyDetailsSurvey = await _context.CompanyDetailsSurveys.Include(p => p.SurveyModel.Questions)
                                                                         .FirstOrDefaultAsync(m => m.ID == id);

            if (companyDetailsSurvey == null)
                return NotFound();

            List<SurveyResultEntry> surveyResultEntries = SurveyResultEntry.GetSurveyResultEntries(companyDetailsSurvey);

            decimal score = surveyResultEntries.Select(e => e.Score).Sum();

            ViewBag.Score = score;

            return View(surveyResultEntries);

        }
        */
    }
}
