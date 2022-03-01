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

            var applicationSession = await _context.ApplicationSessions.Include(s=>s.Application).ThenInclude(a=>a.ApplicationSurveys).ThenInclude(s=>s.Theme).FirstOrDefaultAsync(s=>s.ID == id);
            return View(applicationSession);
        }










        //[Authorize(Roles = "Azienda")]
        public async Task<IActionResult> TakeSurvey(int applicationSessionId, int surveyId)
        {
            //Ottengo lo username dell'azienda loggata e ne carico l'entità
            string username = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Organization organization = await _context.Organizations.Include(c => c.OrganizationCategory).FirstOrDefaultAsync(c => c.Username == username);

            //Carico il questionario
            var applicationSurvey = await _context.ApplicationSurveys
               .Include(s => s.ApplicationSurveyQuestions)
               .FirstOrDefaultAsync(s => s.Id == surveyId);

            string survey = JsonConvert.SerializeObject(applicationSurvey, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            ViewBag.Survey = survey;
            ViewBag.SessionId = applicationSessionId;
            ViewBag.SurveyID = surveyId;
            return View();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> TakeSurvey(int applicationSessionId, int surveyId,string surveyResult)
        {
            if (ModelState.IsValid)
            {
                //Ottengo lo username dell'azienda loggata e ne carico l'entità
                string username = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                Organization organization = await _context.Organizations.Include(c => c.OrganizationCategory).FirstOrDefaultAsync(c => c.Username == username);


                //Carico la sessione
                var applicationSession = await _context.ApplicationSessions.Include(s => s.Application).ThenInclude(a => a.ApplicationSurveys).ThenInclude(s => s.Theme).FirstOrDefaultAsync(s => s.ID == applicationSessionId);

                string surveyName = applicationSession.Application.ApplicationSurveys.FirstOrDefault(s => s.Id == surveyId).Title;

                var applicationSurveyResult = new ApplicationSurveyResult();
                applicationSurveyResult.SurveyResultJson = surveyResult;
                applicationSurveyResult.ApplicationSessionId = applicationSessionId;
                applicationSurveyResult.ApplicationSurveyId = surveyId;
                _context.Add(applicationSurveyResult);
                await _context.SaveChangesAsync();

                applicationSession.CompletedSurveys += surveyName;

                _context.Update(applicationSession);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Session), new { id = applicationSessionId });
                //return new OkResult();
            }
            return View();
        }




    }
}
