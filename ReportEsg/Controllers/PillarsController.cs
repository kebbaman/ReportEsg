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
    public class PillarsController : Controller
    {
        private readonly DatabaseContext _context;

        public PillarsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Pillars
        public async Task<IActionResult> Index(int? applicationId)
        {
            if (applicationId == null)
            {
                return NotFound();
            }
            var databaseContext = _context.Pillars.Include(p => p.Application).Where(p => p.ApplicationId == applicationId);
            ViewBag.AppId = applicationId;
            return View(await databaseContext.ToListAsync());
        }

        // GET: Pillars/Create
        public IActionResult Create(int? applicationId)
        {
            if (applicationId == null)
            {
                return NotFound();
            }
            ViewBag.AppId = applicationId;
            ViewData["ApplicationId"] = applicationId;
            return View();
        }

        // POST: Pillars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,ApplicationId")] Pillar pillar, int? applicationId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pillar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),new {applicationId = applicationId});
            }
            ViewData["ApplicationId"] = applicationId;
            ViewBag.AppId = applicationId;
            return View(pillar);
        }

        // GET: Pillars/Edit/5
        public async Task<IActionResult> Edit(int? id, int? applicationId)
        {
            if (id == null || applicationId == null)
            {
                return NotFound();
            }

            var pillar = await _context.Pillars.FindAsync(id);
            if (pillar == null)
            {
                return NotFound();
            }
            ViewData["ApplicationId"] = applicationId;
            ViewBag.AppId = applicationId;
            return View(pillar);
        }

        // POST: Pillars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,ApplicationId")] Pillar pillar, int? applicationId)
        {
            if (id != pillar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pillar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PillarExists(pillar.Id))
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
            ViewBag.AppId = applicationId;
            return View(pillar);
        }

        // GET: Pillars/Delete/5
        public async Task<IActionResult> Delete(int? id, int? applicationId)
        {
            if (id == null || applicationId == null)
            {
                return NotFound();
            }

            var pillar = await _context.Pillars
                .Include(p => p.Application)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pillar == null)
            {
                return NotFound();
            }
            ViewBag.AppId = applicationId;
            return View(pillar);
        }

        // POST: Pillars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? applicationId)
        {
            var pillar = await _context.Pillars.FindAsync(id);
            _context.Pillars.Remove(pillar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { applicationId = applicationId});
        }

        private bool PillarExists(int id)
        {
            return _context.Pillars.Any(e => e.Id == id);
        }
    }
}
