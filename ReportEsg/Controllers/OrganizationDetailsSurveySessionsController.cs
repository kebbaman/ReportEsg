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
    public class OrganizationDetailsSurveySessionsController : Controller
    {
        private readonly DatabaseContext _context;

        public OrganizationDetailsSurveySessionsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: OrganizationDetailsSurveySessions
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.OrganizationDetailsSurveySessions.Include(o => o.Organization).Include(o => o.OrganizationDetailsSurvey);
            return View(await databaseContext.ToListAsync());
        }

        // GET: OrganizationDetailsSurveySessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizationDetailsSurveySession = await _context.OrganizationDetailsSurveySessions
                .Include(o => o.Organization)
                .Include(o => o.OrganizationDetailsSurvey)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (organizationDetailsSurveySession == null)
            {
                return NotFound();
            }

            return View(organizationDetailsSurveySession);
        }

        // GET: OrganizationDetailsSurveySessions/Create
        public IActionResult Create()
        {
            ViewData["Username"] = new SelectList(_context.Organizations, "Username", "Username");
            ViewData["OrganizationDetailsSurveyId"] = new SelectList(_context.OrganizationDetailsSurveys, "Id", "Discriminator");
            return View();
        }

        // POST: OrganizationDetailsSurveySessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DateTime,SurveyResult,OrganizationDetailsSurveyId,Username")] OrganizationDetailsSurveySession organizationDetailsSurveySession)
        {
            if (ModelState.IsValid)
            {
                _context.Add(organizationDetailsSurveySession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Username"] = new SelectList(_context.Organizations, "Username", "Username", organizationDetailsSurveySession.Username);
            ViewData["OrganizationDetailsSurveyId"] = new SelectList(_context.OrganizationDetailsSurveys, "Id", "Discriminator", organizationDetailsSurveySession.OrganizationDetailsSurveyId);
            return View(organizationDetailsSurveySession);
        }

        

        private bool OrganizationDetailsSurveySessionExists(int id)
        {
            return _context.OrganizationDetailsSurveySessions.Any(e => e.ID == id);
        }

        public async Task<IActionResult> ViewResults(int? id)
        {
            if (id == null)
                return NotFound();

            var organizationDetailsSurveySession = await _context.OrganizationDetailsSurveySessions.Include(p => p.OrganizationDetailsSurvey.OrganizationDetailsSurveyQuestions)
                                                                         .FirstOrDefaultAsync(m => m.ID == id);

            if (organizationDetailsSurveySession == null)
                return NotFound();

            List<OrganizationDetailsSurveyResultEntry> surveyResultEntries = OrganizationDetailsSurveyResultEntry.GetSurveyResultEntries(organizationDetailsSurveySession);
            return View(surveyResultEntries);

        }
    }
}
