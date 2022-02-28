using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReportEsg.Data;
using ReportEsg.Models;

namespace ReportEsg.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly DatabaseContext _context;

        public ApplicationsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Applications
        public async Task<IActionResult> Index()
        {
            return View(await _context.Applications.ToListAsync());
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // GET: Applications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description")] Application application)
        {
            if (ModelState.IsValid)
            {
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(application);
        }

        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description")] Application application)
        {
            if (id != application.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.Id))
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
            return View(application);
        }

        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.Id == id);
        }

        [Authorize(Roles = "Azienda")]
        // GET: Applications
        public async Task<IActionResult> ShowList()
        {
            //Ottengo lo username dell'azienda loggata e ne carico l'entità
            string username = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Organization organization = await _context.Organizations.Include(c => c.OrganizationCategory).FirstOrDefaultAsync(c => c.Username == username);

            //Controllo se ha prima compilato il questionario di anagrafica descrittiva
            if (!await _context.OrganizationDetailsSurveySessions.AnyAsync(s => s.Organization.Username == organization.Username))
                return View("OrganizationDetailsSurveyMissing");

            var applicationsPerCategory = await _context.OrganizationCategoryPerApplication.Where(o => o.OrganizationCategoryId == organization.OrganizationCategoryId).ToListAsync();
            var allApplications = await _context.Applications.ToListAsync();
            List<Application> applications = new List<Application>();
            foreach(Application app in allApplications)
            {
                if (applicationsPerCategory.Any(a => a.ApplicationId == app.Id))
                    applications.Add(app);
            }

            ViewBag.n = applications.Count;
            return View(applications);
        }

        public async Task<IActionResult> Execute(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Ottengo lo username dell'azienda loggata e ne carico l'entità
            string username = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Organization organization = await _context.Organizations.Include(c => c.OrganizationCategory).FirstOrDefaultAsync(c => c.Username == username);

            ApplicationSession session = new ApplicationSession();
            session.DateTime = DateTime.Now;
            session.ApplicationId = (int) id;
            session.Username = organization.Username;

            _context.Add(session);
            await _context.SaveChangesAsync();

            return RedirectToAction("SelectThemes", "ApplicationSessions", new { id = session.ID });
        }

    }
}
