using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReportEsg.Data;
using ReportEsg.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReportEsg.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseContext _context;
        public HomeController(ILogger<HomeController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if(User.Identity.IsAuthenticated && User.IsInRole("Azienda"))
            {
                //Ottengo lo username dell'azienda loggata e ne carico l'entità
                string username = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                Organization organization = await _context.Organizations.Include(c => c.OrganizationCategory).FirstOrDefaultAsync(c => c.Username == username);

                //Controllo se esiste una sessione aperta, se esiste la carico
                var existingSession = await _context.ApplicationSessions.OrderByDescending(s=>s.DateTime).FirstOrDefaultAsync(s => s.Username == organization.Username && s.Completed == false  && !string.IsNullOrEmpty(s.ChoosenThemes));

                if (existingSession != null)
                {
                    ViewBag.PendingSession = true;
                    ViewBag.SessionId = existingSession.ID;
                }
                else
                    ViewBag.PendingSession = false;

            }
            else
                ViewBag.PendingSession = false;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
