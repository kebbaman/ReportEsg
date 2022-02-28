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
    public class OrganizationCategoryPerApplicationsController : Controller
    {
        private readonly DatabaseContext _context;

        public OrganizationCategoryPerApplicationsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: OrganizationCategoryPerApplications
        public async Task<IActionResult> Index(int? applicationId)
        {
            if (applicationId == null)
            {
                return NotFound();
            }
            ViewBag.ApplicationId = applicationId;
            var databaseContext = _context.OrganizationCategoryPerApplication.Include(o => o.OrganizationCategory).Include(o => o.Application).Where(x => x.ApplicationId == applicationId);
            return View(await databaseContext.ToListAsync());
        }


        // GET: OrganizationCategoryPerApplications/Create
        public IActionResult Create(int? applicationId)
        {
            ViewData["OrganizationCategoryId"] = new SelectList(_context.OrganizationCategories, "Id", "Description");
            ViewBag.ApplicationId = applicationId;
            return View();
        }

        // POST: OrganizationCategoryPerApplications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ApplicationlId,OrganizationCategoryId")] OrganizationCategoryPerApplication organizationCategoryPerApplication, int? applicationId)
        {
            if (ModelState.IsValid)
            {
                organizationCategoryPerApplication.ApplicationId = (int) applicationId;
                _context.Add(organizationCategoryPerApplication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { applicationId = applicationId});
            }
            ViewData["OrganizationCategoryId"] = new SelectList(_context.OrganizationCategories, "Id", "Description", organizationCategoryPerApplication.OrganizationCategoryId);
            ViewBag.ApplicationId = applicationId;
            return View(organizationCategoryPerApplication);
        }

        // GET: OrganizationCategoryPerApplications/Edit/5
        public async Task<IActionResult> Edit(int? id, int? applicationId)
        {
            if (id == null || applicationId == null)
            {
                return NotFound();
            }

            var organizationCategoryPerApplication = await _context.OrganizationCategoryPerApplication.FindAsync(id);
            if (organizationCategoryPerApplication == null)
            {
                return NotFound();
            }
            ViewBag.ApplicationId = applicationId;
            ViewData["OrganizationCategoryId"] = new SelectList(_context.OrganizationCategories, "Id", "Id", organizationCategoryPerApplication.OrganizationCategoryId);
            return View(organizationCategoryPerApplication);
        }

        // POST: OrganizationCategoryPerApplications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ApplicationlId,OrganizationCategoryId")] OrganizationCategoryPerApplication organizationCategoryPerApplication, int? applicationId)
        {
            if (id != organizationCategoryPerApplication.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organizationCategoryPerApplication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganizationCategoryPerApplicationExists(organizationCategoryPerApplication.Id))
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
            ViewBag.ApplicationId = applicationId;
            ViewData["OrganizationCategoryId"] = new SelectList(_context.OrganizationCategories, "Id", "Id", organizationCategoryPerApplication.OrganizationCategoryId);
            return View(organizationCategoryPerApplication);
        }

        // GET: OrganizationCategoryPerApplications/Delete/5
        public async Task<IActionResult> Delete(int? id, int? applicationId)
        {
            if (id == null || applicationId == null)
            {
                return NotFound();
            }

            var organizationCategoryPerApplication = await _context.OrganizationCategoryPerApplication
                .Include(o => o.OrganizationCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organizationCategoryPerApplication == null)
            {
                return NotFound();
            }
            ViewBag.ApplicationId = applicationId;
            return View(organizationCategoryPerApplication);
        }

        // POST: OrganizationCategoryPerApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? applicationId)
        {
            var organizationCategoryPerApplication = await _context.OrganizationCategoryPerApplication.FindAsync(id);
            _context.OrganizationCategoryPerApplication.Remove(organizationCategoryPerApplication);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { applicationId = applicationId });
        }

        private bool OrganizationCategoryPerApplicationExists(int id)
        {
            return _context.OrganizationCategoryPerApplication.Any(e => e.Id == id);
        }
    }
}
