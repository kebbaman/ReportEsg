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
    public class ApplicationSessionsController : Controller
    {
        private readonly DatabaseContext _context;

        public ApplicationSessionsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: ApplicationSessions
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.ApplicationSessions.Include(a => a.Application).Include(a => a.Organization);
            return View(await databaseContext.ToListAsync());
        }

        // GET: ApplicationSessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationSession = await _context.ApplicationSessions
                .Include(a => a.Application)
                .Include(a => a.Organization)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (applicationSession == null)
            {
                return NotFound();
            }

            return View(applicationSession);
        }

        // GET: ApplicationSessions/Create
        public IActionResult Create()
        {
            ViewData["ApplicationId"] = new SelectList(_context.Applications, "Id", "Id");
            ViewData["Username"] = new SelectList(_context.Organizations, "Username", "Username");
            return View();
        }

        // POST: ApplicationSessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DateTime,ApplicationId,Username,ChoosenThemes")] ApplicationSession applicationSession)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationSession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationId"] = new SelectList(_context.Applications, "Id", "Id", applicationSession.ApplicationId);
            ViewData["Username"] = new SelectList(_context.Organizations, "Username", "Username", applicationSession.Username);
            return View(applicationSession);
        }

        // GET: ApplicationSessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationSession = await _context.ApplicationSessions.FindAsync(id);
            if (applicationSession == null)
            {
                return NotFound();
            }
            ViewData["ApplicationId"] = new SelectList(_context.Applications, "Id", "Id", applicationSession.ApplicationId);
            ViewData["Username"] = new SelectList(_context.Organizations, "Username", "Username", applicationSession.Username);
            return View(applicationSession);
        }

        // POST: ApplicationSessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DateTime,ApplicationId,Username,ChoosenThemes")] ApplicationSession applicationSession)
        {
            if (id != applicationSession.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationSessionExists(applicationSession.ID))
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
            ViewData["ApplicationId"] = new SelectList(_context.Applications, "Id", "Id", applicationSession.ApplicationId);
            ViewData["Username"] = new SelectList(_context.Organizations, "Username", "Username", applicationSession.Username);
            return View(applicationSession);
        }

        // GET: ApplicationSessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationSession = await _context.ApplicationSessions
                .Include(a => a.Application)
                .Include(a => a.Organization)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (applicationSession == null)
            {
                return NotFound();
            }

            return View(applicationSession);
        }

        // POST: ApplicationSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicationSession = await _context.ApplicationSessions.FindAsync(id);
            _context.ApplicationSessions.Remove(applicationSession);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationSessionExists(int id)
        {
            return _context.ApplicationSessions.Any(e => e.ID == id);
        }












        // GET: ApplicationSessions/SelectThemes
        public async Task<IActionResult> SelectThemes(int? id)
        {
            if(id==null)
                return NotFound();

            var applicationSession = await _context.ApplicationSessions.Include(s=>s.Application.Themes).FirstOrDefaultAsync(s=>s.ID == id);

            if (applicationSession == null)
                return NotFound();

            Dictionary<string, bool> chooseThemes = new Dictionary<string, bool>();

            foreach(Theme theme in applicationSession.Application.Themes)
            {
                chooseThemes.Add(theme.Description, false);
            }
            ViewBag.Id = id;
            return View(chooseThemes);
        }

        // POST: ApplicationSessions/SelectThemes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> SelectThemes(Dictionary<string,bool> chooseThemes,int id)
        {
            if (ModelState.IsValid)
            {
                var applicationSession = await _context.ApplicationSessions.FindAsync(id);
                string themesList = "";
                int i = 0;
                foreach(var choise in chooseThemes)
                {
                    if(choise.Value)
                    {
                        if (i > 0)
                            themesList += ",";
                        themesList += choise.Key;
                    }
                }
                applicationSession.ChoosenThemes = themesList;
                _context.Update(applicationSession);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Session), new { id });
            }
            ViewBag.Id = id;
            return View(chooseThemes);
        }


        public async Task<IActionResult> Session(int? id)
        {
            if (id == null)
                return NotFound();

            var applicationSession = await _context.ApplicationSessions.Include(s=>s.Application).ThenInclude(a=>a.ApplicationSurveys).FirstOrDefaultAsync(s=>s.ID == id);
            return View(applicationSession);
        }











    }
}
