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
                        i++;
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
               .ThenInclude(q=>q.Choices)
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
                var applicationSession =  _context.ApplicationSessions.Include(s => s.Application).ThenInclude(a => a.ApplicationSurveys).ThenInclude(s => s.Theme).FirstOrDefault(s => s.ID == applicationSessionId);

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


        public async Task<IActionResult> Complete(int? applicationSessionId)
        {
            if (applicationSessionId == null)
                return NotFound();

            //Ottengo lo username dell'azienda loggata e ne carico l'entità
            string username = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Organization organization = await _context.Organizations.Include(c => c.OrganizationCategory).FirstOrDefaultAsync(c => c.Username == username);


            //Ottengo il questionario di anagrafica descrittiva compilato dall'azienda
            var organizationDetailsSurvey = await _context.OrganizationDetailsSurveySessions
                .Include(s=>s.OrganizationDetailsSurvey)
                .ThenInclude(s=>s.OrganizationDetailsSurveyQuestions)
                .FirstOrDefaultAsync(s => s.Username == organization.Username);



            //Genero la lista delle risposte date al questionario di anagrafica descrittiva
            List<Answer> organizationDetails = new List<Answer>();

            foreach (var item in organizationDetailsSurvey.SurveyResultObject)
            {
                var question = organizationDetailsSurvey.OrganizationDetailsSurvey.OrganizationDetailsSurveyQuestions.FirstOrDefault(q => q.Name == item.Key);

                string domanda = question.Title;

                string risposta = StringifyAnswer(question.Type,item.Value);

                organizationDetails.Add(new Answer(item.Key,domanda,risposta));
            }

            ViewBag.OrganizationDetails = organizationDetails;


            //Genero la lista delle risposte date ai vari questionari divise per temi
            Dictionary<string, List<Answer>> applicationReport = new Dictionary<string, List<Answer>>();

            //Carico la sessione appena terminata
            var applicationSession = await _context.ApplicationSessions.Include(s=>s.Application).Include(s=>s.SurveyResults).ThenInclude(r=>r.ApplicationSurvey).ThenInclude(s=>s.Theme).Include("SurveyResults.ApplicationSurvey.ApplicationSurveyQuestions").FirstOrDefaultAsync(s => s.ID == applicationSessionId);

            //Ottengo la lista dei questionari completati
            List <ApplicationSurveyResult> surveys = applicationSession.SurveyResults;

            //Carico la lista dei temi scelti dall'utente nell'esecuzione dell'applicativo
            List<string> chosen = applicationSession.ChoosenThemes.Split(",").ToList();

            //Ottengo gli oggetti Answer Per ogni tema
            foreach(string theme in chosen)
            {
                //Genero la lista delle risposte date ai questionari del tema
                List<Answer> answers = new List<Answer>();


                List<ApplicationSurveyResult> results = surveys.Where(s => s.ApplicationSurvey.Theme.Description == theme).ToList();
                foreach(ApplicationSurveyResult result in results)
                {
                    foreach (var item in result.SurveyResultObject)
                    {

                        var question = result.ApplicationSurvey.ApplicationSurveyQuestions.FirstOrDefault(q => q.Name == item.Key);

                        string domanda = question.Title;

                        string risposta = StringifyAnswer(question.Type, item.Value);

                        answers.Add(new Answer(item.Key,domanda,risposta));
                    }
                }


                applicationReport.Add(theme, answers);
            }

            ViewBag.ApplicationReport = applicationReport;

            ViewBag.ApplicationSessionId = applicationSessionId;


            ViewBag.ApplicationTitle = applicationSession.Application.Title;
            ViewBag.CompanyName = organization.CompanyName;
            return View();
        }

        private string StringifyAnswer(string type, object answer)
        {
            switch (type)
            {
                case "boolean":

                    //Answer
                    if ((bool)answer)
                    {
                        return "Vero";
                    }
                    else
                    {
                        return "Falso";
                    }
                case "text":
                    return (string)answer;
                case "checkbox":
                    List<string> choosed = ((Newtonsoft.Json.Linq.JArray)answer).ToObject<List<string>>();

                    string s = "Scelte: ";
                    int i = 0;
                    foreach (string item in choosed)
                    {
                        if (i > 0)
                            s += ",";
                        s += item;
                        i++;
                    }
                    return s;
                case "radiogroup":
                    return (string)answer;
                default:
                    return "";
            }
        }



    }
}
