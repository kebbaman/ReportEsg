using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReportEsg.Data;
using ReportEsg.Models;

namespace ReportEsg.Controllers
{
    public class OrganizationsController : Controller
    {
        private readonly DatabaseContext _context;

        public OrganizationsController(DatabaseContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin,Consulente")]
        // GET: Organizations
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.Organizations.Include(o => o.Role).Include(o => o.OrganizationCategory);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Organizations/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organization = await _context.Organizations
                .Include(o => o.Role)
                .Include(o => o.OrganizationCategory)
                .FirstOrDefaultAsync(m => m.Username == id);
            if (organization == null)
            {
                return NotFound();
            }

            return View(organization);
        }

        // GET: Organizations/Create
        public IActionResult Create()
        {
            ViewData["OrganizationCategoryId"] = new SelectList(_context.OrganizationCategories, "Id", "Id");
            return View();
        }

        // POST: Organizations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyName,PhoneNumber,Indirizzo,Cap,Località,Provincia,PartitaIva,CompanyCategoryId,Username,Email")] CompanyViewModel newCompany)
        {
            if (ModelState.IsValid)
            {
                var crypto = new SimpleCrypto.PBKDF2();
                Organization company = new Organization();
                company.Username = newCompany.Username;
                company.Email = newCompany.Email;

                company.CompanyName = newCompany.CompanyName;
                company.PhoneNumber = newCompany.PhoneNumber;
                company.Indirizzo = newCompany.Indirizzo;
                company.Cap = newCompany.Cap;
                company.Località = newCompany.Località;
                company.Provincia = newCompany.Provincia;
                company.PartitaIva = newCompany.PartitaIva;
                company.OrganizationCategoryId = newCompany.OrganizationCategoryId;

                string pwd = "prova123"; //Generare password random
                company.Password = crypto.Compute(pwd);
                company.PasswordSalt = crypto.Salt;
                company.RoleId = _context.Roles.FirstOrDefault(r => r.Description == "Azienda").Id;
                _context.Add(company);
                await _context.SaveChangesAsync();
                SmtpHelper.SendEmail(company.Email, company.CompanyName, "Iscrizione ad Esg reporting assistant", "Username = indirizzo email, Password =" + pwd);
                return RedirectToAction(nameof(Index));

            }
            ViewData["OrganizationCategoryId"] = new SelectList(_context.OrganizationCategories, "Id", "Description");
            return View(newCompany);
        }

        // GET: Organizations/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organization = await _context.Organizations.FindAsync(id);
            if (organization == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Description", organization.RoleId);
            ViewData["OrganizationCategoryId"] = new SelectList(_context.OrganizationCategories, "Id", "Id", organization.OrganizationCategoryId);
            return View(organization);
        }

        // POST: Organizations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CompanyName,PhoneNumber,Indirizzo,Cap,Località,Provincia,PartitaIva,OrganizationCategoryId,Username,Email,Password,PasswordSalt,RoleId")] Organization organization)
        {
            if (id != organization.Username)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organization);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganizationExists(organization.Username))
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
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Description", organization.RoleId);
            ViewData["OrganizationCategoryId"] = new SelectList(_context.OrganizationCategories, "Id", "Id", organization.OrganizationCategoryId);
            return View(organization);
        }

        // GET: Organizations/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organization = await _context.Organizations
                .Include(o => o.Role)
                .Include(o => o.OrganizationCategory)
                .FirstOrDefaultAsync(m => m.Username == id);
            if (organization == null)
            {
                return NotFound();
            }

            return View(organization);
        }

        // POST: Organizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var organization = await _context.Organizations.FindAsync(id);
            _context.Organizations.Remove(organization);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrganizationExists(string id)
        {
            return _context.Organizations.Any(e => e.Username == id);
        }

        // GET: Companies/Create
        public IActionResult Register()
        {
            ViewData["OrganizationCategoryId"] = new SelectList(_context.OrganizationCategories, "Id", "Description");
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register([Bind("CompanyName,PhoneNumber,Indirizzo,Cap,Località,Provincia,PartitaIva,OrganizationCategoryId,Username,Email")] CompanyViewModel newCompany)
        {
            if (ModelState.IsValid)
            {
                var crypto = new SimpleCrypto.PBKDF2();
                Organization company = new Organization();
                company.Username = newCompany.Username;
                company.Email = newCompany.Email;

                company.CompanyName = newCompany.CompanyName;
                company.PhoneNumber = newCompany.PhoneNumber;
                company.Indirizzo = newCompany.Indirizzo;
                company.Cap = newCompany.Cap;
                company.Località = newCompany.Località;
                company.Provincia = newCompany.Provincia;
                company.PartitaIva = newCompany.PartitaIva;
                company.OrganizationCategoryId = newCompany.OrganizationCategoryId;

                string pwd = "prova123"; //Generare password random
                company.Password = crypto.Compute(pwd);
                company.PasswordSalt = crypto.Salt;
                company.RoleId = _context.Roles.FirstOrDefault(r => r.Description == "Azienda").Id;
                _context.Add(company);
                await _context.SaveChangesAsync();
                SmtpHelper.SendEmail(company.Email, company.CompanyName, "Iscrizione ad Esg reporting assistant", "Username = indirizzo email, Password =" + pwd);
                return RedirectToAction("Index", "Home");

            }
            ViewData["OrganizationCategoryId"] = new SelectList(_context.OrganizationCategories, "Id", "Description");
            return View(newCompany);
        }
    }
}
